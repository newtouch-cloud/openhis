/*********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description： 住院管理》账户管理》患者基本信息
// Author：HLF
// CreateDate： 2016/12/7 17:01:42 
//**********************************************************/
using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class HosPatAccountActionVO
    { 
        /// <summary>
        /// 住院号 zy_brjbxx
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// 拼音码 xt_brjbxx  从zy_brjbxx表读取  添加相同字段
        /// </summary>
        //public string py { get; set; }
        /// <summary>
        /// 病人内码 
        /// </summary>
        public Int32 patid { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }
    
        /// <summary>
        /// 性别
        /// </summary>
        public string xb { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        //public Int16 nl { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime? csny { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// 账户性质 xt_brzh
        /// </summary>
        public int? zhxz { get; set; }

        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal zhye { get; set; }
        /// <summary>
        /// 账户所有人
        /// </summary>
        public string zhsyr { get; set; }
      
        /// <summary>
        /// 启用日期
        /// </summary>
        public DateTime qyrq { get; set; }
        /// <summary>
        /// 终止日期
        /// </summary>
        public DateTime zzrq { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// 建档日期
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 建档人员
        /// </summary>
        public string CreatorUserCode { get; set; }
        /// <summary>
        /// 帐户
        /// </summary>
        public int zh { get; set; }

        //添加字段
        /// <summary>
        /// 科室
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 病区
        /// </summary>
        public string bq { get; set; }

        /// <summary>
        /// 入院日期
        /// </summary>
        public DateTime ryrq { get; set; }


        /// <summary>
        /// 账户编号
        /// </summary>
        public int zhbh { get; set; }

        /// <summary>
        /// 账户性质名称
        /// </summary>
        public string zhxzMC { get; set; }
    }
}
