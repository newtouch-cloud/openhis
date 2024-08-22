using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.NeiMengGu.Output
{
    public class QrCodeOutput : OutputBase
    {
        /// <summary>
        /// 参保人身份证 字符 64 Y
        /// </summary>
        public string idNo { get; set; }
        /// <summary>
        /// 证件类型 字符 64 Y 详见附录 A.2
        /// </summary>
        public string idType { get; set; }
        /// <summary>
        /// 参保人姓名 字符 64 
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        ///  ecToken 令牌 字符 40 Y 用于业务处理验证
        /// </summary>
        public string ecToken { get; set; }
        /// <summary>
        ///  参保地区编码 字符 6 Y 详见附录
        /// </summary>
        public string insuOrg { get; set; }
        /// <summary>
        ///  电子凭证索引号 字符 3
        /// </summary>
        public string ecIndexNo { get; set; }
        /// <summary>
        /// 性别 字符 2
        /// </summary>
        public string gender { get; set; }
        /// <summary>
        ///  出生日期 字符 10
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        ///   nationality 国籍 字符 32 N
        /// </summary>
        public string nationality { get; set; }
        /// <summary>
        ///   email 电子邮箱 字符 32 N
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 扩展参数 字符 N JSON 对象字符
        /// </summary>
        public string extData { get; set; }
    }
}
