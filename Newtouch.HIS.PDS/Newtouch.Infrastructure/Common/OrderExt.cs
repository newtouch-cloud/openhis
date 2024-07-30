using System;

namespace Newtouch.Infrastructure.Common
{
    /// <summary>
    /// 单据扩展
    /// </summary>
    public static class OrderExt
    {
        /// <summary>
        /// 生成简单的单号  时间点+三位随机数
        /// </summary>
        /// <returns></returns>
        public static string GennerateSimpleOrderNo()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(100, 1000);
        }
    }
}