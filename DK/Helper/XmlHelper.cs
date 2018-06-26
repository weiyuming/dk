using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DK
{
    class XmlHelper
    {
        public static List<User> getUserListByXml()
        {
            XmlDocument doc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;//忽略文档里面的注释
            XmlReader reader = XmlReader.Create(@"User.xml", settings);
            doc.Load(reader);
            reader.Close();

            //获得跟节点  user
            XmlNode xn = doc.SelectSingleNode("users");

            //获取子节点
            XmlNodeList xnl = xn.ChildNodes;

            List<User> list = new List<User>();
            //循环节点
            foreach (XmlNode xn1 in xnl)
            {
                User user = new User();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                // 得到Book节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                user.UserName = xnl0.Item(0).InnerText;
                user.UserId = xnl0.Item(1).InnerText;
                user.Password = xnl0.Item(2).InnerText;
                user.UserAgent = xnl0.Item(3).InnerText;
                user.QzId = xnl0.Item(4).InnerText;
                user.MemberId = xnl0.Item(5).InnerText;
                user.EncryptedAttentance = xnl0.Item(6).InnerText;
                user.IsAuto = xnl0.Item(7).InnerText;
                user.IsEmail = xnl0.Item(8).InnerText;
                user.Email = xnl0.Item(9).InnerText;
                user.IsSMS = xnl0.Item(10).InnerText;
                user.Mobile = xnl0.Item(11).InnerText;

                user.IsTime = xnl0.Item(12).InnerText;
                user.Am = Int32.Parse(xnl0.Item(13).InnerText);
                user.AmMinStart = Int32.Parse(xnl0.Item(14).InnerText);
                user.AmMinEnd = Int32.Parse(xnl0.Item(15).InnerText);
                user.Pm = Int32.Parse(xnl0.Item(16).InnerText);
                user.PmMinStart = Int32.Parse(xnl0.Item(17).InnerText);
                user.PmMinEnd = Int32.Parse(xnl0.Item(18).InnerText);
                    

                list.Add(user);
            }

            return list;

        }





        /// <summary>
        /// 获取特定工作日
        /// </summary>
        /// <returns></returns>
        public static List<Days> getWorkday()
        {
            XmlDocument doc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;//忽略文档里面的注释
            XmlReader reader = XmlReader.Create(@"Workday.xml", settings);
            doc.Load(reader);
            reader.Close();

            //获得跟节点  user
            XmlNode xn = doc.SelectSingleNode("days");

            //获取子节点
            XmlNodeList xnl = xn.ChildNodes;

            List<Days> list = new List<Days>();
            //循环节点
            foreach (XmlNode xn1 in xnl)
            {
                Days days = new Days();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                // 得到Book节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                days.Day = xnl0.Item(0).InnerText;
                list.Add(days);
            }

            return list;

        }


        /// <summary>
        /// 获取特定节假日
        /// </summary>
        /// <returns></returns>
        public static List<Days> getHoliday()
        {
            XmlDocument doc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;//忽略文档里面的注释
            XmlReader reader = XmlReader.Create(@"Holiday.xml", settings);
            doc.Load(reader);
            reader.Close();

            //获得跟节点  user
            XmlNode xn = doc.SelectSingleNode("days");

            //获取子节点
            XmlNodeList xnl = xn.ChildNodes;

            List<Days> list = new List<Days>();
            //循环节点
            foreach (XmlNode xn1 in xnl)
            {
                Days days = new Days();
                // 将节点转换为元素，便于得到节点的属性值
                XmlElement xe = (XmlElement)xn1;
                // 得到Book节点的所有子节点
                XmlNodeList xnl0 = xe.ChildNodes;
                days.Day = xnl0.Item(0).InnerText;
                list.Add(days);
            }

            return list;

        }



    }
}
