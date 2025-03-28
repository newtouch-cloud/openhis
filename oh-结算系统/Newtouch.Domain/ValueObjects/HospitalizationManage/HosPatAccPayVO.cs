/*********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description： 住院管理》账户管理》患者收支记录
// Author：HLF
// CreateDate： 2016/12/7 17:01:42 
//**********************************************************/

using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 账户收支记录
    /// </summary>
    public class HosPatAccPayVO
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
    public class PatAccPayVO
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
        public string tId { get; set; }

    }
}
