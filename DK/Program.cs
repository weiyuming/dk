using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DK
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            



            //if (RunningInstance() == null)
            //{
            //    Application.EnableVisualStyles();
            //    Application.SetCompatibleTextRenderingDefault(false);
            //    Application.Run(new form());
            //}
            //else
            //{
            //    MessageBox.Show(null, "有一个和本程序相同的应用程序已经在运行，请不要同时运行多个本程序。\n\n这个程序即将退出。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    //   提示信息，可以删除。   
            //    Application.Exit();//退出程序   
            //} 



            bool ret;
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out ret);
            if (ret)
            {

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new form());

                System.Windows.Forms.Application.EnableVisualStyles();   //这两行实现   XP   可视风格   
                System.Windows.Forms.Application.DoEvents();             //这两行实现   XP   可视风格   
                //System.Windows.Forms.Application.Run(new LamBrowser());
                //   Main   为你程序的主窗体，如果是控制台程序不用这句   
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show(null, "有一个和本程序相同的应用程序已经在运行，请不要同时运行多个本程序。\n\n这个程序即将退出。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //   提示信息，可以删除。   
                Application.ExitThread();//退出程序   
            }
        }









        public static System.Diagnostics.Process RunningInstance()
        {
            System.Diagnostics.Process current = System.Diagnostics.Process.GetCurrentProcess();
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process process in processes) //查找相同名称的进程 
            {
                if (process.Id != current.Id) //忽略当前进程 
                { //确认相同进程的程序运行位置是否一样. 
                    if (System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("/", @"/") == current.MainModule.FileName)
                    { //Return the other process instance. 
                        return process;
                    }
                }
            } //No other instance was found, return null. 
            return null;
        } 




    }
}
