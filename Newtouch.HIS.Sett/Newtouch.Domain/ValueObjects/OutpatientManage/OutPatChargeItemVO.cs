/**********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description：门诊挂号项目实体 
// Author：HLF
// CreateDate： 2016/12/26 16:07:31 
//**********************************************************/
using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 门诊挂号项目实体
    /// </summary>
    public class OutPatChargeItemVO
    {
        /// <summary>
        /// 挂号内码
        /// </summary>
        public int ghnm { get; set; }

        /// <summary>
        /// 科室编号
        /// </summary>
        public int ksbh { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 医生编号
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// 门急诊标志
        /// </summary>
        public string mjzbz { get; set; }

        /// <summary>
        /// 大病项目
        /// </summary>
        public string dbxm { get; set; }

        /// <summary>
        /// 复诊标志
        /// </summary>
        public Byte fzbz { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 挂号科室  组合字段
        /// </summary>
        public string gh_ks { get; set; }

        /// <summary>
        /// 挂号显示名称
        /// </summary>
        public string ghName { get; set; }

        /// <summary>
        /// 科室显示名称
        /// </summary>
        public string ksName { get; set; }


        /// <summary>
        /// 医生名称
        /// </summary>
        public string ysName { get; set; }
         

    }
}
