using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DK.Helper;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace DK
{
    public partial class form : Form
    {

        List<User> list = new List<User>();
        String setMapDateTime = DateTime.Now.ToString("yyyy-MM-dd");
        int am = 8;
        int pm = 17;

        int amMinStart = 15;//上午开始时间
        int amMinEnd = 45;//上午结束时间


        int pmMinStart = 32;//下午开始时间
        int pmMinEnd = 57;//下午结束时间


        Dictionary<String, int> amTimeMap = new Dictionary<String, int>();
        Dictionary<String, int> pmTimeMap = new Dictionary<String, int>();



        ArrayList nameList = new ArrayList();//手工请求的List

        //HashTable<String, int> amTimeMap = new HashTable<string, int>();







        public form()
        {
            InitializeComponent();
        }

        //手工发送按钮方法
        private void btnStart_Click(object sender, EventArgs e)
        {
            //设置按钮为禁用
            btnStart.Enabled = false;
            nameList = new ArrayList();
            foreach (CheckBox chk in groupBoxNoAuto.Controls)//获取指定控件下的checkBox
            {
                if (chk.Checked)//判断checkBox是否被选中
                {
                    nameList.Add(chk.Text);//获取名称
                }

            }
            NoAutoRequest(nameList);
            //设置按钮为启用
            btnStart.Enabled = true;
        }


        //自动发送的定时器
        private void timerStart_Tick(object sender, EventArgs e)
        {
            // 得到 hour minute second  如果等于某个值就开始执行某个程序。  
            int intHour = DateTime.Now.Hour;      //获取当前时间的小时部分
            int intMinute = DateTime.Now.Minute;    //获取当前时间的分钟部分
            int intSecond = DateTime.Now.Second;    //获取当前时间的秒部分



            String nowTime = DateTime.Now.ToString("yyyy-MM-dd");
            TimeSpan ts = DateTime.Parse(nowTime) - DateTime.Parse(setMapDateTime);//判读两个时间的间隔
            int isRstartTime = ts.Days;//如果相差不是0天，则认为应该重新生成时间了

            if (isRstartTime != 0)
            {
                outputLog("时间相差1天，重新执行用户时间生成方法");
                setTimeMap();
                setMapDateTime = nowTime;
            }




            //String currentDate  = DateTime.Now.ToString("yyyy-MM-dd");// 2008-09-04 获取当前日期并格式化
            //Boolean isHoliday = DateHelper.IsHolidayByDate(currentDate);
            //if ((intHour == 8 && intMinute == 15) || (intHour == 18 && intMinute == 20)) //8点15执行
            //{
            //    outputLog("【自动】当前日期为" + currentDate + "是否法定假日（true为节假日）：" + isHoliday);
            //    if (!isHoliday)//非法定节假日  
            //    {
            //        AutoRequest();
            //    }

            //}

            //获取用户自定义时间
            List<int> amHourList = new List<int>();
            List<int> pmHourList = new List<int>();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    User hourUser = list[i];
                    if ("1".Equals(hourUser.IsTime))
                    {
                        amHourList.Add(hourUser.Am);
                        pmHourList.Add(hourUser.Pm);
                    }
                }
            }




            if (intHour == am || amHourList.Contains(intHour))//控制上午的循环
            {
                AutoRequest2(intMinute, amTimeMap, intHour);
            }

            if (intHour == pm || pmHourList.Contains(intHour))//控制下午循环
            {
                AutoRequest2(intMinute, pmTimeMap, intHour);
            }

        }






        //请求发送方法  type 1标示自动请求
        private String request(User tempUser, String type)
        {

            int intHour = DateTime.Now.Hour;      //获取当前时间的小时部分

            DKService dkservice = new DKService();
            String token = dkservice.getToken(tempUser.QzId, tempUser.MemberId, tempUser.UserAgent);

            outputLog("获取到的token为" + token);
            String success = dkservice.requestDk(token, tempUser.EncryptedAttentance, tempUser.UserAgent);
            //未知原因 json转换错误，直接用截取的方式
            //JObject json = JObject.Parse(success);
            //string result = JObject.Parse(success);


            String[] strs = success.Split(',');
            String[] code = strs[0].Split(':');
            success = code[1];
            //success = success.Replace("\"","");
            if (!"0".Equals(success))
            {
                System.DateTime currentTime = new System.DateTime();
                currentTime = System.DateTime.Now; //获取当前时间
                String msg = currentTime + "  " + "请求发送失败请及时处理";
                outputLog(msg, Color.Red, true);
                if ("1".Equals(tempUser.IsEmail) && tempUser.Email != null && !"".Equals(tempUser.Email.Trim()))
                {
                    EmailHelper.sendMail(tempUser.Email, msg, "每日请求发送失败");
                }
                if (intHour <= 9)//节省短信，控制只是早晨发短信
                {
                    if ("1".Equals(tempUser.IsSMS) && tempUser.Mobile != null && !"".Equals(tempUser.Mobile.Trim()) && "1".Equals(type))
                    {
                        SMSHelper.sendSMSFalse(tempUser.Mobile.Trim());
                    }
                }

            }
            else
            {
                outputLog(tempUser.UserName + "  请求发送成功！");
                if (intHour <= 9)//节省短信，控制只是早晨发短信
                {
                    if ("1".Equals(tempUser.IsSMS) && tempUser.Mobile != null && !"".Equals(tempUser.Mobile.Trim()) && "1".Equals(type))
                    {
                        SMSHelper.sendSMSTrue(tempUser.Mobile.Trim());
                    }
                }

            }


            return success;
        }


        //自动发送 总方法  全量发送，不进行时间判断
        private void AutoRequest()
        {
            if (list == null || list.Count <= 0)
            {
                list = XmlHelper.getUserListByXml();
            }

            for (int i = 0; i < list.Count; i++)//循环
            {
                User tempUser = list[i];
                if ("1".Equals(tempUser.IsAuto))//判断是否自动发送
                {
                    outputLog("【自动】开始发送" + tempUser.UserName);
                    request(tempUser, "1");
                }
            }
        }

        //自动发送 新   
        private void AutoRequest2(int intMinute, Dictionary<String, int> timeMap, int intHour)
        {
            Boolean isStop = false;

            if (timeMap != null && timeMap.Count > 0)
            {

                if (timeMap.ContainsValue(intMinute))//如果当前的分钟数有符合要求的，理论上应该只有一个符合要求
                {
                    //双重循环获取时间相符的人 进行发送
                    Dictionary<string, int>.KeyCollection keyCol = timeMap.Keys;
                    foreach (string key in keyCol)
                    {
                        //timeMap.Remove(key); //测试用
                        if (timeMap[key] == intMinute)//如果时间相等
                        {
                            for (int i = 0; i < list.Count; i++)//循环所有人
                            {
                                User tempUser = list[i];
                                if ("1".Equals(tempUser.IsAuto) && (tempUser.UserName).Equals(key))//判断是否自动发送  并且当前人和map中当前时间需要发送的人相匹配
                                {
                                    if (
                                           ("0".Equals(tempUser.IsTime) && intHour == am) //不是指定时间，当前和am相同
                                        || ("0".Equals(tempUser.IsTime) && intHour == pm)//不是指定时间，当前和pm相同
                                        || ("1".Equals(tempUser.IsTime) && intHour == tempUser.Am)//是指定时间，当前和指定am相同
                                        || ("1".Equals(tempUser.IsTime) && intHour == tempUser.Pm)//是指定时间，当前和指定pm相同

                                        )//如果不是指定时间，当前时间必须和am或者pm相同，如果是指定时间，当前时间必须和指定时间相同
                                    {
                                        String currentDate = DateTime.Now.ToString("yyyyMMdd");// 20080904 获取当前日期并格式化
                                        //Boolean isHoliday = DateHelper.IsHolidayByDate(currentDate);
                                        Boolean isHoliday = DateHelper.IsHolidayByDateLocal(currentDate);//使用本地配置判断是否节假日
                                        outputLog("【自动】当前日期为" + currentDate + "是否法定假日（true为节假日）：" + isHoliday);
                                        if (!isHoliday)//非法定节假日  
                                        {
                                            outputLog("【自动】开始发送" + tempUser.UserName);
                                            request(tempUser, "1");

                                            //清空发送人,避免重复发送
                                            timeMap.Remove(key);
                                            isStop = true;
                                            break;
                                        }

                                    }

                                }
                            }
                        }

                        if (isStop)//如果发送过一次了，后续不执行
                        {
                            break;
                        }
                    }
                }
            }

        }







        //手动发送 总方法
        private void NoAutoRequest(ArrayList nameList)
        {
            outputLog("开始进行手工发送");
            if (list == null || list.Count <= 0)
            {
                list = XmlHelper.getUserListByXml();
            }

            if (nameList == null || nameList.Count <= 0)
            {
                String error = "请选择要发送的人员，至少选择一个！";
                outputLog(error);
                MessageBox.Show(error);
                return;

            }

            if (nameList != null || nameList.Count > 0)
            {
                int countTemp = 0;
                for (int i = 0; i < nameList.Count; i++)
                {
                    if ((nameList[i].ToString()).IndexOf("尉彧溟") >= 0)
                    {
                        countTemp++;
                    }
                }

                if (countTemp >= 2)
                {
                    String error = "尉彧溟有两个地点，只能选择其中一个";
                    outputLog(error);
                    MessageBox.Show(error);
                    return;
                }
            }



            if (radioBtnNo.Checked == false && (textBox1.Text == null || "".Equals(textBox1.Text.Trim())))
            {
                String error = "您选择了启用延迟，但是未设置延时时间！";
                outputLog(error);
                MessageBox.Show(error);
                return;

            }
            else
            {
                TimerProc.Interval = 1;//设置初始值为0
                TimerProc.Enabled = true;//启动定时任务

            }



        }

        ////被定时器调用
        //private void timerCall(object obj)
        //{
        //    request(tempUser, "0");
        //}


        private void Form1_Load(object sender, EventArgs e)
        {

            //this.IsMdiContainer = true;

            form f = new form();

            //设置图标
            Icon icon = new Icon("cat16.ico");
            f.Icon = icon;
            //设置窗体居中FormStartPosition
            f.StartPosition = FormStartPosition.CenterScreen;


            richTextBoxLog.ReadOnly = true;//设置日志框为只读
            richTextBoxLog.BackColor = Color.White;//设置背景色


            timerStart.Interval = 45000;//定时任务间隔45秒
            outputLog("【系统启动中】 设置主定时任务的时间间隔为" + timerStart.Interval + "毫秒");

            timerStart.Enabled = true;//启动定时任务
            outputLog("【系统启动中】 设置主定时是否启动为" + timerStart.Enabled);

            list = XmlHelper.getUserListByXml();//初始化数据
            outputLog("【系统启动中】 初始化User.Xml 获得用户人数为" + list.Count);


            CreateAutoCheckBoxs();
            outputLog("【系统启动中】 初始化自动发送人员列表");
            CreateNoAutoCheckBoxs();
            outputLog("【系统启动中】 初始化手工发送人员列表");


            //延时时间默认为是
            radioBtnYes.Checked = true;
            //延迟时间 默认为 47秒
            textBox1.Text = "47000";
            outputLog("【系统启动中】 初始化手工发送延迟时间为" + textBox1.Text + "毫秒");

            //生成人员的时间
            setTimeMap();
            //设置起始时间，用于判断是否需要重新生成人员的时间
            setMapDateTime = DateTime.Now.ToString("yyyy-MM-dd");
            outputLog("【系统启动中】 设置日期为" + setMapDateTime);

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            //LogHelper.setlog("111");
            //LogHelper.setlog("222");
            //LogHelper.setlog("333");
            //LogHelper.setlog("444");
            //LogHelper.setlog("555");
            //EmailHelper.sendMail("weiym@yonyou.com", "测试1111","失败");  
            //outputLog("1111");

            //String success = "{\"code\":0,\"data\":{\"score\":0,\"signTime\":\"18:45\",\"state\":0,\"message\":\"世上无难事，只要肯攀登。\",\"attendanceId\":677811}";
            //String [] strs = success.Split(',');
            //String[] code = strs[0].Split(':');
            //success = code[1];
            ////success = success.Replace("\"","");
            //if ("0".Equals(success))
            //{
            //    MessageBox.Show(success);
            //}

            //JObject result = JObject.Parse(success);
            //string result = JObject.Parse(success)["code"].ToString();

            //SMSHelper.sendSMSFalse("18600091814");
            //SMSHelper.sendSMSTrue("18600091814");

            //TimerProc.Interval = 10000;//定时任务间隔30秒
            //TimerProc.Enabled = true;//启动定时任务
            //outputLog("启动定时任务");
            //TimerProc.Enabled = false;//启动定时任务
            //outputLog("暂停定时任务");

            //setTimeMap();

            //Dictionary<String, int> timeMap = new Dictionary<string,int>();
            //timeMap.Add("尉彧溟",30);

            //AutoRequest2(30, timeMap);

            //String currentDate = "20170520";// 2008-09-04 获取当前日期并格式化
            //currentDate = DateTime.Now.ToString("yyyyMMdd");// 2008-09-04 获取当前日期并格式化
            //Boolean isHoliday = DateHelper.IsHolidayByDate(currentDate);
            //outputLog("【自动】当前日期为" + currentDate + "是否法定假日（true为节假日）：" + isHoliday);





            //String currentDate = "20171005";// 2008-09-04 获取当前日期并格式化
            ////currentDate = DateTime.Now.ToString("yyyyMMdd");// 2008-09-04 获取当前日期并格式化
            //bool isHolidayStr = DateHelper.IsHolidayByDateLocal(currentDate);

            //outputLog("当前日期为" + currentDate + "是否法定假日（true为节假日）：" + isHolidayStr);

            ////Boolean isHoliday = DateHelper.getWeek(currentDate);
            ////outputLog("当前日期为" + currentDate + "是否法定假日（true为节假日）：" + isHoliday);


            list = XmlHelper.getUserListByXml();//初始化数据
            outputLog("【刷新配置】 重新加载User.Xml 获得用户人数为" + list.Count);

            CreateAutoCheckBoxs();
            outputLog("【刷新配置】 重新获取自动发送人员列表");


            CreateNoAutoCheckBoxs();
            outputLog("【刷新配置】 重新获取手工发送人员列表");

            //生成人员的时间
            setTimeMap();
            outputLog("【刷新配置】 重新生成人员的时间");


        }




        // 非自动发送部分
        private void CreateNoAutoCheckBoxs()
        {
            int x = -70;
            int y = 0;


            // 清空一次原有数据，以便重新加载
            groupBoxNoAuto.Controls.Clear();

            for (int i = 0; i < list.Count; i++)
            {
                if (i % 6 == 0)
                {
                    x = -70;
                    y += 30;
                }
                x += 100;
                User user = list[i];
                CheckBox chk = new CheckBox();
                chk.AutoSize = true;
                chk.Location = new System.Drawing.Point(x, y);//位置 
                chk.Size = new System.Drawing.Size(78, 16);//大小 
                chk.Text = user.UserName;//内容 
                groupBoxNoAuto.Controls.Add(chk);

            }
        }


        //自动部分的多选框
        private void CreateAutoCheckBoxs()
        {
            int x = -70;
            int y = 0;

            int coutn = 0;

            // 清空一次原有数据，以便重新加载
            groupBoxAuto.Controls.Clear();

            for (int i = 0; i < list.Count; i++)
            {

                User user = list[i];
                if ("1".Equals(user.IsAuto))
                {
                    if (coutn % 6 == 0)
                    {
                        x = -70;
                        y += 30;
                    }
                    x += 100;
                    CheckBox chk = new CheckBox();
                    chk.AutoSize = true;
                    chk.Location = new System.Drawing.Point(x, y);//位置 
                    chk.Size = new System.Drawing.Size(78, 16);//大小 
                    chk.Text = user.UserName;//内容 
                    chk.Checked = true;
                    chk.Enabled = false;
                    groupBoxAuto.Controls.Add(chk);
                    coutn++;
                }
            }
        }

        //日志记录
        private void outputLog(String msg)
        {
            outputLog(msg, Color.Black, false);
        }


        //日志记录
        private void outputLog(String msg, Color color, Boolean isBold)
        {


            //让文本框获取焦点，不过注释这行也能达到效果
            this.richTextBoxLog.Focus();
            //设置光标的位置到文本尾   
            this.richTextBoxLog.Select(this.richTextBoxLog.TextLength, 0);
            //滚动到控件光标处   
            this.richTextBoxLog.ScrollToCaret();
            //设置字体颜色
            this.richTextBoxLog.SelectionColor = color;
            if (isBold)
            {
                this.richTextBoxLog.SelectionFont = new Font(Font, FontStyle.Bold);
            }



            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now; //获取当前时间
            msg = currentTime + "  " + msg;
            this.richTextBoxLog.AppendText(msg);//输出到界面
            this.richTextBoxLog.AppendText(Environment.NewLine);




            LogHelper.setlog(msg);//写入日志文件
        }



        //手工发送间隔定时器
        private void TimerProc_Tick(object sender, EventArgs e)
        {

            if (nameList == null || nameList.Count <= 0)
            {
                btnStart.Enabled = true;//设置手工按钮为启用
                TimerProc.Enabled = false;//停止定时器
            }
            else
            {
                btnStart.Enabled = false;//设置手工按钮为禁用
                String tempName = (String)nameList[0];
                for (int i = 0; i < list.Count; i++)//循环
                {
                    User tempUser = list[i];
                    //bool exists = nameList.Contains(tempUser.UserName);//判断是否存在
                    if (tempName.Equals(tempUser.UserName))//存在
                    {
                        outputLog("【手动】开始发送" + tempUser.UserName);
                        request(tempUser, "0");
                        //线程休眠
                        if (radioBtnYes.Checked)
                        {
                            outputLog("使用定时任务进行休眠" + textBox1.Text + "毫秒，开始");
                            TimerProc.Interval = Int32.Parse(textBox1.Text);//定时任务间隔
                            //TimerProc.Enabled = true;//启动定时任务

                            //Thread.Sleep(Int32.Parse(textBox1.Text));//休眠方式
                            //outputLog("使用定时任务进行休眠" + textBox1.Text + "毫秒，结束");
                        }
                    }
                }

                nameList.Remove(tempName);//移除掉已发送的
            }




        }


        //在指定范围内  随机生成时间
        private void setTimeMap()
        {
            amTimeMap = new Dictionary<string, int>();
            pmTimeMap = new Dictionary<string, int>();
            //list为人员列表
            for (int i = 0; i < list.Count; i++)
            {
                User tempUser = list[i];

                if ("1".Equals(tempUser.IsAuto))
                {
                    int amTemp = am;
                    int amMinStartTemp = amMinStart;
                    int amMinEndTemp = amMinEnd;

                    int pmTemp = pm;
                    int pmMinStartTemp = pmMinStart;
                    int pmMinEndTemp = pmMinEnd;

                    if ("1".Equals(tempUser.IsTime))//是否启用自定义时间
                    {

                        //上午
                        amTemp = tempUser.Am;
                        amMinStartTemp = tempUser.AmMinStart;//开始分钟
                        amMinEndTemp = tempUser.AmMinEnd;//结束分钟
                        //下午
                        pmTemp = tempUser.Pm;
                        pmMinStartTemp = tempUser.PmMinStart;
                        pmMinEndTemp = tempUser.PmMinEnd;
                    }



                    /* 处理上午时间 */
                    int amTimeTemp = 0;
                    do
                    {
                        Random rd = new Random();
                        amTimeTemp = rd.Next(amMinStartTemp, amMinEndTemp); //生成随机数，范围限制到 amMinStart与 amMinEnd之间
                    } while (amTimeMap.ContainsValue(amTimeTemp) || pmTimeMap.ContainsValue(amTimeTemp));//控制数据不能重复
                    //将数据放到上午的时间中
                    amTimeMap.Add(tempUser.UserName, amTimeTemp);
                    outputLog("【初始化时间】 " + tempUser.UserName + " 上午时间为：" + amTemp + "点" + amTimeTemp + "分");



                    /* 处理下午时间 */
                    int pmTimeTemp = 0;
                    do
                    {
                        Random rd = new Random();
                        pmTimeTemp = rd.Next(pmMinStartTemp, pmMinEndTemp); //生成随机数，范围限制到 pmMinStart与 amMinEndpmMinEnd
                    } while (pmTimeMap.ContainsValue(pmTimeTemp) || amTimeMap.ContainsValue(pmTimeTemp));
                    //将数据放到上午的时间中
                    pmTimeMap.Add(tempUser.UserName, pmTimeTemp);
                    outputLog("【初始化时间】 " + tempUser.UserName + " 下午时间为：" + pmTemp + "点" + pmTimeTemp + "分");
                }



            }

        }






        #region
        ////创建NotifyIcon对象 
        //NotifyIcon notifyicon = new NotifyIcon();
        ////创建托盘图标对象 
        //Icon ico = new Icon("q4.ico");
        ////创建托盘菜单对象 
        //ContextMenu notifyContextMenu = new ContextMenu();
        #endregion


        #region 托盘提示
        //private void Form1_Load(object sender, EventArgs e)
        //{

        //}
        #endregion

        #region 隐藏任务栏图标、显示托盘图标
        //private void Form1_SizeChanged(object sender, EventArgs e)
        //{
        //    //判断是否选择的是最小化按钮 
        //    if (WindowState == FormWindowState.Minimized)
        //    {
        //        //托盘显示图标等于托盘图标对象 
        //        //注意notifyIcon1是控件的名字而不是对象的名字 
        //        notifyIcon1.Icon = ico;
        //        //隐藏任务栏区图标 
        //        this.ShowInTaskbar = false;
        //        //图标显示在托盘区 
        //        notifyicon.Visible = true;
        //    }
        //}
        #endregion

        #region 还原窗体
        //private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        //{
        //    //判断是否已经最小化于托盘 
        //    if (WindowState == FormWindowState.Minimized)
        //    {
        //        //还原窗体显示 
        //        WindowState = FormWindowState.Normal;
        //        //激活窗体并给予它焦点 
        //        this.Activate();
        //        //任务栏区显示图标 
        //        this.ShowInTaskbar = true;
        //        //托盘区图标隐藏 
        //        notifyicon.Visible = false;


        //        //让文本框获取焦点，不过注释这行也能达到效果
        //        this.richTextBoxLog.Focus();
        //        //设置光标的位置到文本尾   
        //        this.richTextBoxLog.Select(this.richTextBoxLog.TextLength, 0);
        //        //滚动到控件光标处   
        //        this.richTextBoxLog.ScrollToCaret();
        //    }
        //}
        #endregion


        //关闭窗体前执行
        //private void form_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    exitpassword form2 =new exitpassword();
        //    //form.FormBorderStyle = FormBorderStyle.None; 
        //    //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
        //   // form.TopLevel =false; 
        //    //指示子窗体非顶级窗体
        //    //this.panel1.Controls.Add(form);//将子窗体载入panel

        //    //form2.MdiParent = this;
        //    //form2.StartPosition = FormStartPosition.CenterScreen;


        //    form2.ShowDialog();
        //    e.Cancel = true;

        //    //方法二：指定父容器实现
        //    //Form2 
        //    //form=new 
        //    //Form2();
        //    //form.MdiParent=this;//指定当前窗体为顶级Mdi窗体
        //    //form.Parent=this.Panel1;//指定子窗体的父容器为
        //    //Frm.FormBorderStyle = FormBorderStyle.None;//隐藏子窗体边框，当然也可以在子窗体的窗体加载事件中实现
        //    //panelform.Show();
        //}








































        //以下是新的写法



        #region
        //创建NotifyIcon对象 
        NotifyIcon notifyicon = new NotifyIcon();
        //创建托盘图标对象 
        Icon ico = new Icon("q4.ico");
        //创建托盘菜单对象 
        ContextMenu notifyContextMenu = new ContextMenu();
        #endregion


        #region 托盘提示
        //private void Form1_Load(object sender, EventArgs e)
        //{

        //}
        #endregion

        #region 隐藏任务栏图标、显示托盘图标
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //判断是否选择的是最小化按钮 
            if (WindowState == FormWindowState.Minimized)
            {
                //托盘显示图标等于托盘图标对象 
                //注意notifyIcon1是控件的名字而不是对象的名字 
                notifyIcon1.Icon = ico;
                //隐藏任务栏区图标 
                this.ShowInTaskbar = false;
                //图标显示在托盘区 
                notifyicon.Visible = true;
            }
        }
        #endregion

        #region 还原窗体
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            huanyuan();
        }


        private void huanyuan()
        {
            //判断是否已经最小化于托盘 
            if (WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示 
                WindowState = FormWindowState.Normal;
                //激活窗体并给予它焦点 
                this.Activate();
                //任务栏区显示图标 
                this.ShowInTaskbar = true;
                //托盘区图标隐藏 
                notifyicon.Visible = false;


                //让文本框获取焦点，不过注释这行也能达到效果
                this.richTextBoxLog.Focus();
                //设置光标的位置到文本尾   
                this.richTextBoxLog.Select(this.richTextBoxLog.TextLength, 0);
                //滚动到控件光标处   
                this.richTextBoxLog.ScrollToCaret();
            }
        }
        #endregion


        //关闭窗体前执行
        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            //exitpassword form2 = new exitpassword();
            //form.FormBorderStyle = FormBorderStyle.None; 
            //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
            // form.TopLevel =false; 
            //指示子窗体非顶级窗体
            //this.panel1.Controls.Add(form);//将子窗体载入panel

            //form2.MdiParent = this;
            //form2.StartPosition = FormStartPosition.CenterScreen;

            e.Cancel = true;
            //form2.ShowDialog();

            this.WindowState = FormWindowState.Minimized;

        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            huanyuan();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？退出后自动请求功能将失效", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // 关闭所有的线程
                this.Dispose();
                this.Close();




            }
        }
    }
}
