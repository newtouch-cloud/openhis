/**********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description： 门诊挂号收费病人有关信息 
// Author：HLF
// CreateDate： 2016/12/23 11:22:53 
//**********************************************************/

using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class OutPatChargeInfoVO
    {
        //[xt_brjbxx]
        /// <summary>
        /// 病人内码
        /// </summary>
        public int patid { get; set; }
        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string xb { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime csny { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string dz { get; set; }
        /// <summary>
        /// 凭证号
        /// </summary>
        public string pzh { get; set; }
        /// <summary>
        /// 医疗项目
        /// </summary>
        public string ylxm { get; set; }
        /// <summary>
        /// 凭证开始日期
        /// </summary>
        public DateTime? pzksrq { get; set; }
        /// <summary>
        /// 凭证终止日期
        /// </summary>
        public DateTime? pzzzrq { get; set; }
        /// <summary>
        /// 凭证诊断
        /// </summary>
        public string pzzd { get; set; }
        /// <summary>
        /// 账户
        /// </summary>
        public int? zh { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// 区县代码
        /// </summary>
        public string qxdm { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string dwmc { get; set; }
        /// <summary>
        /// 身份证--证件号
        /// </summary>
        public string zjh { get; set; }
        /// <summary>
        /// 卡类型
        /// </summary>
        public string zjlx { get; set; }


        // [xt_dy] 不用了
        /// <summary>
        /// 地域编号
        /// </summary>
        public int? dybh { get; set; }
        /// <summary>
        /// 地域
        /// </summary>
        public string dy { get; set; }
        ///// <summary>
        ///// 地域名称
        ///// </summary>
        //public string dymc { get; set; }

        //[mz_gh]
        /// <summary>
        /// 挂号内码
        /// </summary>
        public int ghnm { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// 就诊序号
        /// </summary>
        public Int16 jzxh { get; set; }

        /// <summary>
        /// 门诊号
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// 复诊标志
        /// </summary>
        public Byte fzbz { get; set; }
        /// <summary>
        /// 病人性质
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// 病人性质编号
        /// </summary>
        public int brxzbh { get; set; }

        /// <summary>
        /// 病人性质名称
        /// </summary>
        public string brxzmc { get; set; }
        /// <summary>
        /// 医保交易类型
        /// </summary>
        public string ybjylx { get; set; }



        /// <summary>
        /// 病人性质集合
        /// </summary>
        public List<OutPatChargeProperty> brxzList { get; set; }

        public short nl { get; set; }

    }
    public class GhJzInfo
    {
        public int ghnm { get; set; }
        public string mzh { get; set; }
        public string blh { get; set; }
        public string xm { get; set; }
        public string zjh { get; set; }
        public string xb { get; set; }
        public string brxz { get; set; }
        public string ghksmc { get; set; }
        public string ghks { get; set; }
        public string ghysmc { get; set; }
        public string ghys { get; set; }
        public DateTime? ghrq { get; set; }
        public string ghzt { get; set; }
        public string jzzt { get; set; }
        public DateTime? jzrq { get; set; }
        public string jzksmc { get; set; }
        public string jzks { get; set; }
        public string jzysmc { get; set; }
        public string jzys { get; set; }
        public string fph { get; set; }
        public string syth { get; set; }
        public string thys { get; set; }
        public string thrq { get; set; }
    }


}
