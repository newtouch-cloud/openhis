using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.HospitalizationManage
{
    public class InpatientHosPatAccPayVO
    {
        /// <summary>
        /// 收支金额 xt_brzhszjl
        /// </summary>
        public decimal szje { get; set; }
        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal zhye { get; set; }
        /// <summary>
        /// 凭证号
        /// </summary>
        public string pzh { get; set; }
        /// <summary>
        /// 收支人员
        /// </summary>
        public string CreatorUserName { get; set; }

        /// <summary>
        /// 收支日期-即创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 收支性质
        /// </summary>
        public string szxz { get; set; }

        /// <summary>
        /// 支付方式  xt_xjzffs
        /// </summary>
        public string xjzffsmc { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string xjzffs { get; set; }
    }

        /// <summary>
        /// 账户收支记录
        /// </summary>
        public class InpatientPatAccPayVO
    {
            public string Id { get; set; }
            /// <summary>
            /// 收支金额 xt_brzhszjl
            /// </summary>
            public decimal szje { get; set; }
            /// <summary>
            /// 账户余额
            /// </summary>
            public decimal zhye { get; set; }
            /// <summary>
            /// 凭证号
            /// </summary>
            public string pzh { get; set; }
            /// <summary>
            /// 收支人员
            /// </summary>
            public string Creator { get; set; }

            /// <summary>
            /// 收支日期-即创建日期
            /// </summary>
            public DateTime CreateTime { get; set; }
            /// <summary>
            /// 收支性质
            /// </summary>
            public int szxz { get; set; }

            public string szxzmc { get; set; }

            /// <summary>
            /// 支付方式  xt_xjzffs
            /// </summary>
            public string xjzffsmc { get; set; }

            /// <summary>
            /// 支付方式
            /// </summary>
            public string xjzffs { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            public string memo { get; set; }
        }
}
