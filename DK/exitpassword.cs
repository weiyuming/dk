using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DK
{
    public partial class exitpassword : Form
    {
        public exitpassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (exittxtbox.Text == null || "".Equals(exittxtbox.Text))
            {
                MessageBox.Show("请输入退出密码");
            }
            else
            {
                if ("123456".Equals(exittxtbox.Text))
                {
                    DialogResult result;
                    result = MessageBox.Show("密码输入正确，确定退出吗？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.OK)
                    {
                        Application.ExitThread();//退出所有

                    }
                }
                else
                {
                    MessageBox.Show("退出密码输入不正确");
                }
            }
        }

        private void exitpassword_FormClosing(object sender, FormClosingEventArgs e)
        {

            //this.Close();

           
        }

        private void exitpassword_Load(object sender, EventArgs e)
        {
            //this.StartPosition = FormStartPosition.Manual;
            this.StartPosition = FormStartPosition.CenterParent;

            exitpassword f = new exitpassword();
            //设置窗体居中FormStartPosition
            f.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}
