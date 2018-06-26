using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace DK
{
    class  HttpHelper
    {
        public static string post(string Url, string postDataStr)
        {
            string retString = "错误";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";//请求方式
                
                //clent部分
                request.Headers.Add("Accept-Encoding", "gzip,deflate");//编码格式 
                request.UserAgent = "Dalvik/2.1.0 (Linux; U; Android 6.0.1; MI 4LTE MIUI/7.5.4";
                //entity
                request.ContentType = "application/x-www-form-urlencoded";//类型

                request.Headers.Add("X-Tingyun-Processed", "true");//编码格式 


                //request.Connection = "Keep-Alive";
                request.Host = "ezone.upesn.com";
               
                //request.CookieContainer = cookie;
                //request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);//字节长度
                //request.Accept = "Accept: application/json, text/javascript, */*; q=0.01";
                //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
                //request.Referer = "http://x.pps.tv/105178";
                
                //request.Headers.Add("Cookie:user_type=web;QC102=bid:4110886807uid:2020591377=0;uid=0;cf=1435914505;QC006=2kpbc27lcia4nsbi4fpplstg;QC008=1440327333.1440327333.1440327333.1;QC005=da78959d68a1d35a44a8f83432c572cc;QC010=167611133;P00001=f9U7b64bIYbwDm3nll3nG7RzFNGPm2QwD4YlCaXg9m1tLiOkm419;P00010=2020591377;P01010=1441900800;P00007=f9U7b64bIYbwDm3nll3nG7RzFNGPm2QwD4YlCaXg9m1tLiOkm419;P00PRU=2020591377;P00002=%7B%22uid%22%3A%222020591377%22%2C%22user_name%22%3A%2218600091814%22%2C%22email%22%3A");

                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
                myStreamWriter.Write(postDataStr);
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
    }
}
