using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiBaoScheduling.Common
{
    public class Constants
    {
        #region tags

        /// <summary>
        /// tag 标签 客户流水号
        /// </summary>
        public const string Tags_ClientNo = "ClientNo";

        #endregion

        #region other

        /// <summary>
        /// 时间格式化 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public const string TimeFormat = "yyyy-MM-dd HH:mm:ss";

        #endregion

        #region Log
        /// <summary>
        /// 请求类型
        /// </summary>
        public const string RequsetType = "{0}_RequestXML";

        /// <summary>
        /// 返回类型
        /// </summary>
        public const string ResponseType = "{0}_ResponseXML";

        #endregion
    }
}
