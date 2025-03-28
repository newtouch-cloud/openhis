/**********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description： 门诊收费处方结算保存实体 
// Author：HLF
// CreateDate： 2016/12/30 10:17:05 
//**********************************************************/
namespace Newtouch.HIS.Domain.DTO
{
    /// <summary>
    /// grid列表DTO
    /// </summary>
    public class OutpatChargeDto: OutpatInfoDto
    {
    
        /// <summary>
        /// 门急诊标志
        /// </summary>
        public string mjzbz { get; set; }

        public string ybdm { get; set; }

        /// <summary>
        /// 医生
        /// </summary>
        public string ys { get; set; }

        public string ysmc { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// 科室编号
        /// </summary>
        public int ksbh { get; set; }

        public string ksmc { get; set; }

        //大类
        public string dl { get; set; }
        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 收费项目类别 1项目  2药品
        /// </summary>
        public string xmtype { get; set; }
        /// <summary>
        /// 药房代码
        /// </summary>
        public string yfdm { get; set; }
        /// <summary>
        /// 收费项目编号
        /// </summary>
        public string sfxm { get; set; }
        public string ypbzdm { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal sl { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public string zje { get; set; }
        public int pt_nm { get; set; }
        public int pt_zdmxnm { get; set; }
        public byte pt_zdmxxz { get; set; }

        /// <summary>
        /// 成组号
        /// </summary>
        public string czh { get; set; }

        //处方明细有关 sfxm dl
        /// <summary>
        /// 项目编号 int
        /// </summary>
        public int xmbh { get; set; }
        public string xmmc { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal dj { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal je { get; set; }
        /// <summary>
        /// 支付比例
        /// </summary>
        public decimal zfbl { get; set; }
        public string zfxz { get; set; }
        public string dw { get; set; }
        /// <summary>
        /// 服务费
        /// </summary>
        public decimal fwfdj { get; set; }

        //结算相关数据
        //dl dj sl je zfbl zfxz  sfxm
        //项目名称 outJmbl outJmje mxnm mxbm 
        public string nbdl { get; set; }


        ///// <summary>
        ///// 现金支付方式
        ///// </summary>
        //public string xjzffs { get; set; }

        ///// <summary>
        ///// 支付金额
        ///// </summary>
        //public string zfje { get; set; }

        ///// <summary>
        ///// 预收款 == 实收款
        ///// </summary>
        //public string ysk { get; set; }

        ///// <summary>
        ///// 找零
        ///// </summary>
        //public string zl { get; set; }

    }
}
