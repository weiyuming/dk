using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DK.Helper
{
    class StringHelper
    {
        
        /// <summary>
        /// isEqual(判断两个值是否相等，进行了NULL的判断)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool isEqual(String a, String b)
        {
            if (a == null)
            {
                if (b == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return a.Equals(b);
            }
        }
    }
}
