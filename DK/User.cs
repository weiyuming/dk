using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DK
{
    class User
    {
        private String userName;//姓名

        public String UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private String userId;//登陆账号

        public String UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        private String password;//密码

        public String Password
        {
            get { return password; }
            set { password = value; }
        }
        private String userAgent;//请求头信息中的手机信息

        public String UserAgent
        {
            get { return userAgent; }
            set { userAgent = value; }
        }
        private String qzId;

        public String QzId
        {
            get { return qzId; }
            set { qzId = value; }
        }
        private String memberId;//菜单ID

        public String MemberId
        {
            get { return memberId; }
            set { memberId = value; }
        }
        private String encryptedAttentance;//加密后的请求信息

        public String EncryptedAttentance
        {
            get { return encryptedAttentance; }
            set { encryptedAttentance = value; }
        }

        private String isAuto;//是否自动提交

        public String IsAuto
        {
            get { return isAuto; }
            set { isAuto = value; }
        }

        private String isEmail;//是否发邮件

        public String IsEmail
        {
            get { return isEmail; }
            set { isEmail = value; }
        }

        private String email;//邮件地址

        public String Email
        {
            get { return email; }
            set { email = value; }
        }

        private String isSMS;//是否发短信

        public String IsSMS
        {
            get { return isSMS; }
            set { isSMS = value; }
        }

        private String mobile;//短信地址

        public String Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }

        private string isTime;//是否启用自定义时间

        public string IsTime
        {
            get { return isTime; }
            set { isTime = value; }
        }

        private int am;//早晨小时数

        public int Am
        {
            get { return am; }
            set { am = value; }
        }

        private int amMinStart;//早晨随机开始时间

        public int AmMinStart
        {
            get { return amMinStart; }
            set { amMinStart = value; }
        }

        private int amMinEnd;//早晨随机结束时间

        public int AmMinEnd
        {
            get { return amMinEnd; }
            set { amMinEnd = value; }
        }

        private int pm;//晚上小时数

        public int Pm
        {
            get { return pm; }
            set { pm = value; }
        }

        private int pmMinStart;//晚上随机开始时间

        public int PmMinStart
        {
            get { return pmMinStart; }
            set { pmMinStart = value; }
        }

        private int pmMinEnd;//晚上随机结束时间

        public int PmMinEnd
        {
            get { return pmMinEnd; }
            set { pmMinEnd = value; }
        }


    }
}
