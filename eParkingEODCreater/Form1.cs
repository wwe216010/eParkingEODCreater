using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Collections;
using log4net;
using log4net.Config;

namespace eParkingEODCreater
{
    public partial class Form1 : Form
    {
        private static readonly ILog KLog = LogManager.GetLogger(typeof(Form1));//使用log4net的Log
        private static List<ManualResetEvent> _waitHandles = null;
        static private int MaxThCount;//最大允許數量
        static Queue sucessQue = new Queue();
        static int MaxSEQSn = 0;
        static int SEQSnCount = 0;
        static object lockObj = new object();
        static string ExportPath = string.Empty;
        static string EODDate = string.Empty;
        static string EODDateTime = string.Empty;
        static string TXDate_S = string.Empty;
        static string TXDate_E = string.Empty;
        static int dtRowCount = 0;
        static int EpkNoPassMod = 0;
        static int Trailer_Count;// = 0;
        static int Trailer_Amount;// = 0;


        public Form1()
        {
            InitializeComponent();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                KLog.Info("===========================================開始排程===========================================");
                ExportPath = System.Configuration.ConfigurationManager.AppSettings["ExportPath"].ToString();
                EODDate = dpEODTime.Value.ToString("yyyyMMdd");
                EODDateTime = dpEODTime.Value.ToString("yyyyMMddHHmmss");
                TXDate_S = dpTXDate_S.Value.ToString("yyyy/MM/dd");
                TXDate_E = dpTXDate_E.Value.ToString("yyyy/MM/dd");
                MaxThCount = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MaxThreadCount"].ToString());
                int number = 0;
                SEQSnCount = int.TryParse(txtSEQSnCount.Text, out number) ? Convert.ToInt32(txtSEQSnCount.Text) : 0;
                int mod = 0;
                EpkNoPassMod = int.TryParse(txtEpkCheckNoPass.Text, out mod) ? Convert.ToInt32(txtEpkCheckNoPass.Text) : 0;
                Trailer_Count = 0;
                Trailer_Amount = 0;

                if (!Directory.Exists(ExportPath))
                {
                    Directory.CreateDirectory(ExportPath);
                }

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BMSContext"].ToString();
                KLog.Info(connectionString);
                using (SqlConnection conn = new SqlConnection(connectionString + ";MultipleActiveResultSets = true;Connect Timeout = 10800;"))
                {
                    conn.Open();
                    string strSQL = string.Empty;

                    if (!string.IsNullOrEmpty(txtAccNo.Text))
                    {
                        strSQL = string.Format(@"
	SELECT  
        ACC_NO, 
        (SELECT LPR_NUMBER FROM BMS_PRO_VEHICLE WITH(NOLOCK) WHERE VEHICLE_ID = ACT.VEHICLE_ID) LPR_NUMBER
    FROM BMS_ACC_ACCOUNT ACT WITH(NOLOCK) WHERE ACC_NO = {0}
                    ", txtAccNo.Text);                         
                    }
                    else
                    {
                    strSQL = string.Format(@"
SELECT *FROM 
(
	SELECT TOP {0}  
        ACC_NO, 
        (SELECT LPR_NUMBER FROM BMS_PRO_VEHICLE WITH(NOLOCK) WHERE VEHICLE_ID = ACT.VEHICLE_ID) LPR_NUMBER
    FROM BMS_ACC_ACCOUNT ACT WITH(NOLOCK) WHERE ACC_NO IS NOT NULL ORDER BY NEWID()
) A
WHERE A.LPR_NUMBER IS NOT NULL
                    ", SEQSnCount);
                    }

                    KLog.Info(strSQL);

                    using (SqlCommand cmd = new SqlCommand(strSQL, conn))
                    {
                        using (SqlDataAdapter adpter = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            adpter.Fill(ds);
                            DataTable dt = ds.Tables[0];
                            DataTable dt2 = new DataTable();

                            if (!string.IsNullOrEmpty(txtAccNo.Text) && SEQSnCount > 0)
                            {

                                dt2.Columns.Add(new DataColumn("ACC_NO"));
                                dt2.Columns.Add(new DataColumn("LPR_NUMBER"));

                                for (int i = 0; i < SEQSnCount; i++)
                                {
                                    DataRow dr = dt2.NewRow();
                                    dr["ACC_NO"] = string.IsNullOrEmpty(dt.Rows[0]["ACC_NO"].ToString()) ? "" : dt.Rows[0]["ACC_NO"].ToString();
                                    dr["LPR_NUMBER"] = string.IsNullOrEmpty(dt.Rows[0]["LPR_NUMBER"].ToString()) ? "" : dt.Rows[0]["LPR_NUMBER"].ToString();
                                    dt2.Rows.Add(dr);                                     
                                }
                                dt = dt2;
                            }

                            dtRowCount = dt.Rows.Count;
                            KLog.Info("DataTable查詢筆數：" + dtRowCount);

                            if (dtRowCount > 0)
                            {
                                int defaultMaxworkerThreads = 0;
                                int defaultmaxIOThreads = 0;
                                int waitHandleIndex = 0;
                                int maxThreadsAtOnce = MaxThCount;
                                _waitHandles = new List<ManualResetEvent>();
                                ThreadPool.SetMaxThreads(maxThreadsAtOnce, maxThreadsAtOnce);
                                ThreadPool.GetMaxThreads(out defaultMaxworkerThreads, out defaultmaxIOThreads);

                                MaxSEQSn = GetMaxSEQSn();
                                RandomDateTime RDT = new RandomDateTime();
                                DateTime time;
                                progressBar.Value = 0;
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    Application.DoEvents();
                                    //進度條
                                    progressBar.Visible = true;
                                    progressBar.Minimum = 0;
                                    progressBar.Maximum = dt.Rows.Count;
                                    progressBar.BackColor = Color.Green;
                                    progressBar.Value++;

                                    ContentObject content = new ContentObject();
                                    content.SN = i + 1;
                                    content.ACC_NO = string.IsNullOrEmpty(dt.Rows[i]["ACC_NO"].ToString()) ? "" : dt.Rows[i]["ACC_NO"].ToString();
                                    content.lprNumber = string.IsNullOrEmpty(dt.Rows[i]["LPR_NUMBER"].ToString()) ? "" : dt.Rows[i]["LPR_NUMBER"].ToString();
                                    time = RDT.Next();
                                    content.TransactionTime = time.ToString("yyyyMMddHHmmss");
                                    content.DueDate = time.AddDays(30).ToString("yyyyMMdd");

                                    var resetEvent = new ManualResetEvent(false);//執行緒狀態
                                    object param1 = new object[] { content };
                                    var controller = new MyJob(waitHandleIndex, param1);

                                    //委派方法OnThreadedDataRequest指派給執行緒執行，controller會當成是OnThreadedDataRequest的參數傳入
                                    ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(OnThreadedDataRequest), controller);

                                    waitHandleIndex++;
                                    _waitHandles.Add(resetEvent);

                                    if (_waitHandles.Count >= maxThreadsAtOnce)
                                    {
                                        System.Threading.WaitHandle.WaitAll(_waitHandles.ToArray<ManualResetEvent>());
                                        _waitHandles = new List<ManualResetEvent>();
                                        waitHandleIndex = 0;
                                    }
                                }
                                if (_waitHandles.Count > 0)
                                    System.Threading.WaitHandle.WaitAll(_waitHandles.ToArray<ManualResetEvent>());

                                string FileName = string.Format("PSACCTX_EPK_BMS_{0}_{1}_1.txt", DateTime.Now.ToString("yyyyMMdd"), EODDate);
                                write(Path.Combine(ExportPath, FileName));

                                KLog.Info("===========================================結束排程===========================================");                
                                MessageBox.Show("檔案匯出完成~~");
                            }
                            else
                            {
                                MessageBox.Show("查無資料~~");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                KLog.Error(ex);
                MessageBox.Show(ex.ToString());
            }
        }

        private static void write(string FullFileName)
        {
            KLog.Info("佇列資料筆數" + sucessQue.Count);

            byte[] BufferContent;
            using (BufferedStream bufferedOutputStream = new BufferedStream(new FileStream(FullFileName, FileMode.Create, FileAccess.Write)))
            {
                string txt = string.Empty;

                sucessQue.Enqueue(Trailer_Count.ToString() +","+ Trailer_Amount.ToString());
                while (sucessQue.Count > 0)
                {
                    try
                    {
                        txt = sucessQue.Dequeue().ToString();
                        BufferContent = System.Text.Encoding.UTF8.GetBytes(txt);//取得編碼                        
                        int intByte = BufferContent.Length;//取得長度                        
                        bufferedOutputStream.Write(BufferContent, 0, intByte);//寫資料      
                    }
                    catch (Exception ex)
                    {
                        KLog.Error(txt + "。" + ex.ToString());
                        continue;
                    }
                }
            }
        }

        private int GetMaxSEQSn()
        {
            string Connection = System.Configuration.ConfigurationManager.ConnectionStrings["BMSContext"].ToString();            
            using (SqlConnection cn = new SqlConnection(Connection + ";MultipleActiveResultSets = true;Connect Timeout = 10800;"))
            {
                cn.Open();
                string sql = @"
SELECT ISNULL(MAX(SUBSTRING(EODSEQ,19,8)), 0)
FROM BMS_EXT_TRANS_DETAIL WITH(NOLOCK) 
--WHERE SUBSTRING(EODSEQ,1,18) = '7077155720180619RS' 
                ";
                KLog.Info(sql);
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    using (SqlDataAdapter adpter = new SqlDataAdapter(cmd))
                    {                     
                        //int rtvalue = Convert.ToInt32(cmd.ExecuteScalar());
                        int rtvalue = 0;
                        KLog.Info("目前最大單號：" + cmd.ExecuteScalar().ToString());
                        object qq = cmd.ExecuteScalar();
                        rtvalue = Convert.ToInt32(qq);
                        //rtvalue = int.TryParse(qq.ToString(), out rtvalue) ? (int)cmd.ExecuteScalar() : (int)qq;
                        KLog.Info("目前最大單號" + rtvalue);
                        return rtvalue;
                    }
                }
            }
        }

        public static void OnThreadedDataRequest(object sender)
        {
            var controller = sender as MyJob;
            try
            {
                if (controller == null) return;
                //KLog.Error(_waitHandles.Count + "," + controller.WaitHandleIndex);
                Thread.Sleep(25);
                controller.Execute();//執行緒執行被指派的作業，執行完後執行緒休息
                _waitHandles[controller.WaitHandleIndex].Set();//.WaitOne();//執行緒已經執行完被指派的作業，開始閒置。Set()喚醒閒置執行緒，讓執行緒可以再被使用
            }
            catch (Exception ex)
            {
                KLog.Error(ex);
                throw ex;
            }
        }

        public static void doMultiThread(ContentObject obj)
        {
            //obj.id = Thread.CurrentThread.ManagedThreadId;
            string fileData = string.Empty;
            try
            {
                //1.ACC_NO 帳戶編號
                fileData += obj.ACC_NO + ",";
                //2.lprNumber 車牌號碼
                fileData += obj.lprNumber + ",";
                //3.PROVIDERID 縣市政府
                fileData += "12803,";
                //4.ServiceID 服務類型
                fileData += "RS,";
                //5.PaymentType 扣款類型
                fileData += "01,";
                //6.EODSEQ 交易日結序號
                lock (lockObj)
                {
                    Interlocked.Increment(ref MaxSEQSn);
                    fileData += string.Format("70771557{0}RS{1},", DateTime.Now.ToString("yyyyMMdd"), string.Format("{0:00000000}", MaxSEQSn + 1));
                }
                //7.SLIPNO 停車單號(70771557+交易日結序號後八碼)
                fileData += "70771557" + string.Format("{0:00000000}", MaxSEQSn) + ",";
                //8.LocateSite 停車地點
                fileData += ",";
                //9.LocateNo 停車編號
                fileData += ",";
                //10.EODTime 檔案接收日
                fileData += EODDateTime + ",";
                //11.TransactionTime 開單日期
                fileData += obj.TransactionTime + ",";
                //12.AMOUNT 開單金額
                fileData += "20,";
                //13.DueDate 繳費期限
                fileData += obj.DueDate + ",";
                //14.FailMsg 錯誤訊息 & 15.EPKVerifyCode EPK檢核狀態代碼
                if (EpkNoPassMod > 0 && obj.SN % EpkNoPassMod == 0)
                {
                    fileData += "其它失敗,E99";
                }
                else
                {
                    fileData += ",E00";                  
                }
                fileData += "\r\n";
                //if (dtRowCount != obj.SN)
                //{
                //    fileData += "\r\n";
                //}

                lock (sucessQue.SyncRoot)
                {
                    sucessQue.Enqueue(fileData);
                    Trailer_Count++;
                    Trailer_Amount += 20;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public class MyJob
        {
            public int WaitHandleIndex = 0;
            public ContentObject obj;

            public MyJob(int waitHandleIndex, object param)
            {
                WaitHandleIndex = waitHandleIndex;
                // Add input parameters to the constructor to store input variables in class level variables
                // for use by the Execute method below.
                object[] oa = (object[])param;
                obj = (ContentObject)oa[0];
            }

            public void Execute()
            {
                try
                {
                    doMultiThread(obj);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        class RandomDateTime
        {
            DateTime start;
            Random gen;
            int range;

            public RandomDateTime()
            {
                start = Convert.ToDateTime(TXDate_S);//new DateTime(1995, 1, 1);
                gen = new Random();
                range = (Convert.ToDateTime(TXDate_E) - start).Days + 1;
            }

            public DateTime Next()
            {
                return start.AddDays(gen.Next(range)).AddHours(gen.Next(0, 24)).AddMinutes(gen.Next(0, 60)).AddSeconds(gen.Next(0, 60));
            }
        }

        public class ContentObject
        {
            public int SN { get; set; }
            public string ACC_NO { get; set; }
            public string lprNumber { get; set; }
            public string TransactionTime { get; set; }
            public string DueDate { get; set; }
        }
    }
}