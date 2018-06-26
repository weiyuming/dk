using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.IO.Compression;

namespace DK
{
    class DKService
    {
        public String login(String userName,String password)
        {
            string retString = "错误";
            try
            {
                String url = "https://ezone.yonyoucloud.com/signin/index/webLogin";
                String postData = "qzId=81779&memberId=2889527";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";//请求方式


                //clent部分
                request.Headers.Add("Accept-Encoding", "gzip,deflate");//编码格式 
                request.UserAgent = "Dalvik/2.1.0 (Linux; U; Android 6.0.1; MI 4LTE MIUI/7.5.4";
                //entity
                request.ContentType = "application/x-www-form-urlencoded";//类型
                request.Headers.Add("X-Tingyun-Processed", "true");//编码格式 
                request.Host = "ezone.upesn.com";

                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
                myStreamWriter.Write(postData);
                myStreamWriter.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //response.Cookies = cookie.GetCookies(response.ResponseUri);
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
            }
            catch
            {
                return retString;
            }
            return retString;
        }




        public String getToken(String qzId, String memberId,String userAgent)
        {
            string retString = "错误";
            String token = "";
            try
            {
                String url = "https://ezone.yonyoucloud.com/signin/index/webLogin";
                //String postData = "qzId=81779&memberId=2889527";
                String postData = "qzId=" + qzId + "&memberId=" + memberId;
                //1． 创建httpWebRequest对象
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);


                //2． 初始化HttpWebRequest对象
                request.Method = "POST";//请求方式
                //clent部分
                request.Headers.Add("Accept-Encoding", "gzip,deflate");//编码格式 
                //request.UserAgent = "Dalvik/2.1.0 (Linux; U; Android 6.0.1; MI 4LTE MIUI/7.5.4";
                request.UserAgent = userAgent;
                //entity
                request.ContentType = "application/x-www-form-urlencoded";//类型
                request.Headers.Add("X-Tingyun-Processed", "true");//编码格式 
                request.Host = "ezone.upesn.com";


                //3． 附加要POST给服务器的数据到HttpWebRequest对象(附加POST数据的过程比较特殊，它并没有提供一个属性给用户存取，需要写入HttpWebRequest对象提供的一个stream里面。)
                Stream myRequestStream = request.GetRequestStream();//创建一个Stream,赋值是写入HttpWebRequest对象提供的一个stream里面
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.Default);
                myStreamWriter.Write(postData);
                myStreamWriter.Close();


                //4． 读取服务器的返回信息
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream myResponseStream = response.GetResponseStream();
                //Stream responseStream = responseStream = response.GetResponseStream();
                //5、gzip 反解
                if (response.ContentEncoding.ToLower().Contains("gzip"))
                {
                    myResponseStream = new GZipStream(myResponseStream, CompressionMode.Decompress);
                }
                else if (response.ContentEncoding.ToLower().Contains("deflate"))
                {
                    myResponseStream = new DeflateStream(myResponseStream, CompressionMode.Decompress);
                }



                
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.Default);
                retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                //6; 暂时不进行json的解析，直接截取  不进行判断
                String [] retStrings =retString.Split(',');
                String[] tokens = retStrings[1].Split(':');
                token = tokens[1];

                token = token.Replace("\"", "");
                token = token.Replace("\\", "");
                token = token.Replace("}", "");
              


            }
            catch
            {
                return retString;
            }


            Console.WriteLine(token);
            return token;
        }



        public String requestDk(String toKen, String encryptedAttentance,String userAgent)
        {
            string retString = "错误";
            try
            {
                String url = "https://ezone.yonyoucloud.com/signin/attentance/encryptSignIn?token=" + toKen + "&clientV=1-5.5.0-1-1";
                String postData = encryptedAttentance;
                //1． 创建httpWebRequest对象
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);


                //2． 初始化HttpWebRequest对象
                request.Method = "POST";//请求方式
                //clent部分
                request.Headers.Add("Accept-Encoding", "gzip,deflate");//编码格式 
                request.UserAgent = userAgent;
                //entity
                request.ContentType = "application/x-www-form-urlencoded";//类型
                request.Headers.Add("X-Tingyun-Processed", "true");//编码格式 
                request.Host = "ezone.yonyoucloud.com";


                //3． 附加要POST给服务器的数据到HttpWebRequest对象(附加POST数据的过程比较特殊，它并没有提供一个属性给用户存取，需要写入HttpWebRequest对象提供的一个stream里面。)
                Stream myRequestStream = request.GetRequestStream();//创建一个Stream,赋值是写入HttpWebRequest对象提供的一个stream里面
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.Default);
                myStreamWriter.Write(postData);
                myStreamWriter.Close();


                //4． 读取服务器的返回信息
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream myResponseStream = response.GetResponseStream();
                //Stream responseStream = responseStream = response.GetResponseStream();
                //5、gzip 反解
                if (response.ContentEncoding.ToLower().Contains("gzip"))
                {
                    myResponseStream = new GZipStream(myResponseStream, CompressionMode.Decompress);
                }
                else if (response.ContentEncoding.ToLower().Contains("deflate"))
                {
                    myResponseStream = new DeflateStream(myResponseStream, CompressionMode.Decompress);
                }




                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.Default);
                retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                ////6; 暂时不进行json的解析，直接截取  不进行判断
                //String[] retStrings = retString.Split(',');
                //String[] tokens = retStrings[1].Split(':');
                //token = tokens[1];

                //token = token.Replace("\"", "");
                //token = token.Replace("\\", "");
                //token = token.Replace("}", "");



            }
            catch
            {
                return retString;
            }


            Console.WriteLine(retString);
            return retString;
        }

    }
}
