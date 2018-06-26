using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace DK.Helper
{
    class DateHelper
    {
        /// <summary>
        /// 判断是不是节假日,节假日返回true 
        /// </summary>
        /// <param name="date">日期格式：yyyyMMdd</param>
        /// <returns></returns>
        public static bool IsHolidayByDate(string date)
        {
            bool isHoliday = false;

            //string temp = date.Replace('-','');

            System.Net.WebClient WebClientObj = new System.Net.WebClient();
            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
            PostVars.Add("d", date);//参数
            try
            {
                byte[] byRemoteInfo = WebClientObj.UploadValues(@"http://www.easybots.cn/api/holiday.php", "POST", PostVars);//请求地址,传参方式,参数集合
                string sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo);//获取返回值

                string result = JObject.Parse(sRemoteInfo)[date].ToString();
                if (result == "0")
                {
                    isHoliday = false;
                }else if (result == "1" || result == "2")
                {
                    isHoliday = true;
                }
            }
            catch(Exception ex)
            {
                isHoliday = false;
            }
            return isHoliday;
        }

        /// <summary>
        /// 根据输入的日期判断星期几，日期格式为  20170703
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static String getWeek(string date)
        {
            //DateTime dt;

            //DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();

            //dtFormat.ShortDatePattern = "yyyyMMdd";

            //dt = Convert.ToDateTime(date, dtFormat);


            DateTime dt = DateTime.ParseExact(date, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);

            return dt.DayOfWeek.ToString();
            //return dt.ToString();
        }


        /// <summary>
        /// 根据输入的日期判断是否为周末，日期格式为  20170703
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool isWeekends(string date)
        {
            String week = getWeek(date);
            //是周六或者周日，Sunday标示周日，Saturday标示周六
            if (week != null && (week.Equals("Saturday") || week.Equals("Sunday")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }




        /// <summary>
        /// 判断是不是节假日,节假日返回true ，根据本地配置文件判断
        /// </summary>
        /// <param name="date">日期格式：yyyyMMdd</param>
        /// <returns></returns>
        public static bool IsHolidayByDateLocal(string date)
        {

            /*
             * 第一部分：先判断是否为指定节假日 
             */

            List<Days> holidayList = XmlHelper.getHoliday();
            if (holidayList != null && holidayList.Count > 0)
            {
                Days days = null;
                for (int i = 0; i<holidayList.Count; i++ )
                {
                    days = holidayList[i];
                    if (days != null)
                    {
                        if(StringHelper.isEqual(days.Day,date))
                        {
                            return true;
                        }
                    }
                }
            }
            

            /*
             * 第二部分：先判断是否为指定工作日
             */

            List<Days> workdayList = XmlHelper.getWorkday();
            if (workdayList != null && workdayList.Count > 0)
            {
                Days days = null;
                for (int i = 0; i < workdayList.Count; i++)
                {
                    days = workdayList[i];
                    if (days != null)
                    {
                        if (StringHelper.isEqual(days.Day, date))
                        {
                            return false;
                        }
                    }
                }
            }


            /*
             * 第三部分：判断是否为周末或工作日
             */

            return isWeekends(date);
        }

    }

}
