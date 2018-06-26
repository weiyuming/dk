using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;


namespace DK.Helper
{
    class SMSHelper
    {

        static string url = "https://api.netease.im/sms/sendtemplate.action";
                             
        static string appKey = "************************************";
        static string appSecret = "******";


        //templateid 	模板编号(由客户顾问配置之后告知开发者)
        //mobiles 接收者号码列表，JSONArray格式,如["186xxxxxxxx","186xxxxxxxx"]，限制接收者号码个数最多为100个
        //paramss 短信参数列表，用于依次填充模板，JSONArray格式，每个变量长度不能超过30字，如["xxx","yyy"];对于不包含变量的模板，不填此参数表示模板即短信全文内容
        public static string sendSMS(int templateid, string mobiles, string paramss)
        {

            appKey = getAppKey();
            string nonce = getNonce(1);
            TimeSpan ts=DateTime.Now - DateTime.Parse("1970-1-1 00:00:00");
            string curTime = ts.TotalMilliseconds.ToString();
            appSecret = "fafe38df2145";
            string checkSum = getCheckSum(appSecret , nonce , curTime);
            


            //string msg = "templateid";
            string post = "templateid=" + templateid + "&mobiles=" + mobiles;
            //byte[] btBodys = Encoding.UTF8.GetBytes(post);

            System.Net.WebRequest wReq = System.Net.WebRequest.Create(url);
            wReq.Method = "POST";
            wReq.Headers.Add("AppKey", appKey);
            wReq.Headers.Add("Nonce", nonce);
            wReq.Headers.Add("CurTime", curTime);
            wReq.Headers.Add("CheckSum", checkSum);
            //wReq.ContentLength = btBodys.Length;
            wReq.ContentType = "application/x-www-form-urlencoded;charset=utf-8";


            //3． 附加要POST给服务器的数据到HttpWebRequest对象(附加POST数据的过程比较特殊，它并没有提供一个属性给用户存取，需要写入HttpWebRequest对象提供的一个stream里面。)
            Stream myRequestStream = wReq.GetRequestStream();//创建一个Stream,赋值是写入HttpWebRequest对象提供的一个stream里面
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.Default);
            myStreamWriter.Write(post);
            myStreamWriter.Close();

            System.Net.WebResponse wResp = wReq.GetResponse();
            System.IO.Stream respStream = wResp.GetResponseStream();

            string result;
            using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream,System.Text.Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }



        public static string sendSMSTrue(string mobile)
        {
            String mobiles = "[\"" + mobile + "\"]";
            int templateid = 3058459;
            return sendSMS(templateid, mobiles, null);
        }


        public static string sendSMSFalse(string mobile)
        {
            String mobiles = "[\"" + mobile + "\"]";
            int templateid = 3049492;
            return sendSMS(templateid, mobiles, null);
        }

        //获取Key
        private static string getAppKey()
        {
            return "c5210d18387ddb617f2193eb835e287f";
        }

        //生成随机数
        private static string getNonce(int type)
        {
            String nonce = "";
            if (type == 1)
            {
                nonce=Guid.NewGuid().ToString();
            }else if(type==2)
            {
                RNGCryptoServiceProvider csp = new RNGCryptoServiceProvider();
                byte[] byteCsp = new byte[10];
                csp.GetBytes(byteCsp);
                nonce=BitConverter.ToString(byteCsp);
            }
            return nonce;
        }



         private static string SHA1_Hash(string str_sha1_in)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = UTF8Encoding.Default.GetBytes(str_sha1_in);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
            str_sha1_out = str_sha1_out.Replace("-", "").ToLower();
            return str_sha1_out;
        }

        private static string getFormattedText(byte[] bytes)
        {
            int len = bytes.Length;
            StringBuilder buf = new StringBuilder(len * 2);
            for (int j = 0; j < len; j++)
            {
                buf.Append(HEX_DIGITS[(bytes[j] >> 4) & 0x0f]);
                buf.Append(HEX_DIGITS[bytes[j] & 0x0f]);
            }
            return buf.ToString();
        }

        private static char[] HEX_DIGITS = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };


        // 计算并获取CheckSum
        public static String getCheckSum(String appSecret, String nonce, String curTime)
        {
            byte[] data = Encoding.Default.GetBytes(appSecret + nonce + curTime);
            byte[] result;

            SHA1 sha = new SHA1CryptoServiceProvider();
            // This is one implementation of the abstract class SHA1.
            result = sha.ComputeHash(data);

            return getFormattedText(result);
        }



    }
        
}
