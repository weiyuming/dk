namespace DK
{
    partial class exitpassword
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.exitOk = new System.Windows.Forms.Button();
            this.exitlab = new System.Windows.Forms.Label();
            this.exittxtbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // exitOk
            // 
            this.exitOk.Location = new System.Drawing.Point(295, 128);
            this.exitOk.Name = "exitOk";
            this.exitOk.Size = new System.Drawing.Size(75, 23);
            this.exitOk.TabIndex = 0;
            this.exitOk.Text = "确定";
            this.exitOk.UseVisualStyleBackColor = true;
            this.exitOk.Click += new System.EventHandler(this.button1_Click);
            // 
            // exitlab
            // 
            this.exitlab.AutoSize = true;
            this.exitlab.Location = new System.Drawing.Point(12, 52);
            this.exitlab.Name = "exitlab";
            this.exitlab.Size = new System.Drawing.Size(127, 15);
            this.exitlab.TabIndex = 1;
            this.exitlab.Text = "请输入退出密码：";
            // 
            // exittxtbox
            // 
            this.exittxtbox.Location = new System.Drawing.Point(145, 49);
            this.exittxtbox.Name = "exittxtbox";
            this.exittxtbox.Size = new System.Drawing.Size(230, 25);
            this.exittxtbox.TabIndex = 2;
            // 
            // exitpassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 177);
            this.Controls.Add(this.exittxtbox);
            this.Controls.Add(this.exitlab);
            this.Controls.Add(this.exitOk);
            this.Name = "exitpassword";
            this.Text = "退出密码";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.exitpassword_FormClosing);
            this.Load += new System.EventHandler(this.exitpassword_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exitOk;
        private System.Windows.Forms.Label exitlab;
        private System.Windows.Forms.TextBox exittxtbox;
    }
}