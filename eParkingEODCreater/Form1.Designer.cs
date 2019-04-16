namespace eParkingEODCreater
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dpTXDate_S = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSEQSnCount = new System.Windows.Forms.TextBox();
            this.dpTXDate_E = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dpEODTime = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEpkCheckNoPass = new System.Windows.Forms.TextBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.txtAccNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dpTXDate_S
            // 
            this.dpTXDate_S.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dpTXDate_S.Location = new System.Drawing.Point(189, 100);
            this.dpTXDate_S.Name = "dpTXDate_S";
            this.dpTXDate_S.Size = new System.Drawing.Size(236, 29);
            this.dpTXDate_S.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "開單日區間";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.83799F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.07821F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtSEQSnCount, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dpTXDate_S, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.dpTXDate_E, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dpEODTime, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtEpkCheckNoPass, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtAccNo, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(716, 382);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "資料筆數";
            // 
            // txtSEQSnCount
            // 
            this.txtSEQSnCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSEQSnCount.Location = new System.Drawing.Point(189, 176);
            this.txtSEQSnCount.MaxLength = 6;
            this.txtSEQSnCount.Name = "txtSEQSnCount";
            this.txtSEQSnCount.Size = new System.Drawing.Size(236, 29);
            this.txtSEQSnCount.TabIndex = 3;
            // 
            // dpTXDate_E
            // 
            this.dpTXDate_E.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dpTXDate_E.Location = new System.Drawing.Point(432, 100);
            this.dpTXDate_E.Name = "dpTXDate_E";
            this.dpTXDate_E.Size = new System.Drawing.Size(280, 29);
            this.dpTXDate_E.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 36);
            this.label3.TabIndex = 10;
            this.label3.Text = "檔案接收日(EODTime)";
            // 
            // dpEODTime
            // 
            this.dpEODTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dpEODTime.Location = new System.Drawing.Point(189, 24);
            this.dpEODTime.Name = "dpEODTime";
            this.dpEODTime.Size = new System.Drawing.Size(236, 29);
            this.dpEODTime.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 248);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(178, 36);
            this.label4.TabIndex = 12;
            this.label4.Text = "每x筆包含一筆不通過EPK檢核資料";
            // 
            // txtEpkCheckNoPass
            // 
            this.txtEpkCheckNoPass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEpkCheckNoPass.Location = new System.Drawing.Point(189, 252);
            this.txtEpkCheckNoPass.MaxLength = 6;
            this.txtEpkCheckNoPass.Name = "txtEpkCheckNoPass";
            this.txtEpkCheckNoPass.Size = new System.Drawing.Size(236, 29);
            this.txtEpkCheckNoPass.TabIndex = 4;
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("新細明體", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnExport.Location = new System.Drawing.Point(19, 436);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(709, 165);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(19, 401);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(709, 29);
            this.progressBar.TabIndex = 1;
            // 
            // txtAccNo
            // 
            this.txtAccNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAccNo.Location = new System.Drawing.Point(189, 328);
            this.txtAccNo.Name = "txtAccNo";
            this.txtAccNo.Size = new System.Drawing.Size(236, 29);
            this.txtAccNo.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(65, 334);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 18);
            this.label5.TabIndex = 14;
            this.label5.Text = "AccNo";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 613);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnExport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "eParkingDebitEODCreater";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dpTXDate_S;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DateTimePicker dpTXDate_E;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSEQSnCount;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dpEODTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEpkCheckNoPass;
        private System.Windows.Forms.TextBox txtAccNo;
        private System.Windows.Forms.Label label5;
    }
}

