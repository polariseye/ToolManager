using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolManager.Utility.Extensions
{
    public static class DateTimeExt
    {
        /// <summary>
        /// 转换为时间字符串
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns></returns>
        public static String ToDateTimeString(this DateTime dt)
        {
            return DateTimeUtil.ToDateTimeString(dt);
        }

        /// <summary>
        /// 转换为日期字符串
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns></returns>
        public static String ToDateString(this DateTime dt)
        {
            return DateTimeUtil.ToDateString(dt);
        }
    }
}
