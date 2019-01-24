namespace DK
{
    partial class form
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form));
            this.btnStart = new System.Windows.Forms.Button();
            this.timerStart = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.groupBoxAuto = new System.Windows.Forms.GroupBox();
            this.groupBoxNoAuto = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.radioBtnYes = new System.Windows.Forms.RadioButton();
            this.radioBtnNo = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.TimerProc = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(735, 388);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(111, 33);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "发送手工请求";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // timerStart
            // 
            this.timerStart.Interval = 2000;
            this.timerStart.Tick += new System.EventHandler(this.timerStart_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(527, 373);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 24);
            this.button1.TabIndex = 1;
            this.button1.Text = "刷新配置";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // groupBoxAuto
            // 
            this.groupBoxAuto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAuto.Location = new System.Drawing.Point(12, 12);
            this.groupBoxAuto.Name = "groupBoxAuto";
            this.groupBoxAuto.Size = new System.Drawing.Size(891, 164);
            this.groupBoxAuto.TabIndex = 2;
            this.groupBoxAuto.TabStop = false;
            this.groupBoxAuto.Text = "自动请求列表";
            // 
            // groupBoxNoAuto
            // 
            this.groupBoxNoAuto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxNoAuto.Location = new System.Drawing.Point(12, 202);
            this.groupBoxNoAuto.Name = "groupBoxNoAuto";
            this.groupBoxNoAuto.Size = new System.Drawing.Size(891, 164);
            this.groupBoxNoAuto.TabIndex = 3;
            this.groupBoxNoAuto.TabStop = false;
            this.groupBoxNoAuto.Text = "手工请求列表";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(252, 376);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "延时时间（毫秒）：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(400, 372);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 25);
            this.textBox1.TabIndex = 8;
            // 
            // radioBtnYes
            // 
            this.radioBtnYes.AutoSize = true;
            this.radioBtnYes.Location = new System.Drawing.Point(123, 374);
            this.radioBtnYes.Name = "radioBtnYes";
            this.radioBtnYes.Size = new System.Drawing.Size(43, 19);
            this.radioBtnYes.TabIndex = 4;
            this.radioBtnYes.TabStop = true;
            this.radioBtnYes.Text = "是";
            this.radioBtnYes.UseVisualStyleBackColor = true;
            // 
            // radioBtnNo
            // 
            this.radioBtnNo.AutoSize = true;
            this.radioBtnNo.Location = new System.Drawing.Point(172, 374);
            this.radioBtnNo.Name = "radioBtnNo";
            this.radioBtnNo.Size = new System.Drawing.Size(43, 19);
            this.radioBtnNo.TabIndex = 5;
            this.radioBtnNo.TabStop = true;
            this.radioBtnNo.Text = "否";
            this.radioBtnNo.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 376);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "是否启用延时:";
            // 
            // TimerProc
            // 
            this.TimerProc.Tick += new System.EventHandler(this.TimerProc_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.richTextBoxLog);
            this.groupBox1.Location = new System.Drawing.Point(15, 438);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(891, 310);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "日志信息";
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxLog.Location = new System.Drawing.Point(-3, 25);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(891, 279);
            this.richTextBoxLog.TabIndex = 0;
            this.richTextBoxLog.Text = "";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 52);
            // 
            // 显示ToolStripMenuItem
            // 
            this.显示ToolStripMenuItem.Name = "显示ToolStripMenuItem";
            this.显示ToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.显示ToolStripMenuItem.Text = "显示";
            this.显示ToolStripMenuItem.Click += new System.EventHandler(this.显示ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(915, 838);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBoxNoAuto);
            this.Controls.Add(this.radioBtnYes);
            this.Controls.Add(this.groupBoxAuto);
            this.Controls.Add(this.radioBtnNo);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "请求发送工具";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.form_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Timer timerStart;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBoxAuto;
        private System.Windows.Forms.GroupBox groupBoxNoAuto;
        private System.Windows.Forms.RadioButton radioBtnYes;
        private System.Windows.Forms.RadioButton radioBtnNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer TimerProc;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
    }
}

