/**********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description： 
// Author：
// CreateDate： 2016/12/15 21:21:53 
//**********************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.InputDto
{
    /// <summary>
    /// 充值传参
    /// </summary>
    public class DeposDto
    {
        /// <summary>
        /// 支付方式编号
        /// </summary>
        public int zffsbh { get; set; }

        /// <summary>
        /// 支付方式名称
        /// </summary>
        public string zffsmc { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal zfje { get; set; }

        /// <summary>
        /// 凭证号
        /// </summary>
        public string pzh { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        public int zh { get; set; }
        /// <summary>
        /// 收支性质
        /// </summary>
        public int szxz { get; set; }

        /// <summary>
        /// 账户性质
        /// </summary>
        public int? zhxz { get; set; }

        /// <summary>
        /// 病人内码
        /// </summary>
        public int patid { get; set; }
        public string zyh { get; set; }
        public string memo { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string outTradeNo { get; set; }
    }
}
