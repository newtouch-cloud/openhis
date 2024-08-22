using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Domain.DTO.InputDto
{
    public class InpatientOutpatAccInfoDto
    {
        public int patid { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public string blh { get; set; }
        public DateTime? csny { get; set; }
        public string zjlx { get; set; }
        public string zjh { get; set; }
        public string jsr { get; set; }
        public short? nl { get; set; }
        public string phone { get; set; }
        public string brxz { get; set; }
        public string brxzmc { get; set; }
        public int? sycs { get; set; }
        public int? dybh { get; set; }
        public string kh { get; set; }
        /// <summary>
        /// 卡类型
        /// </summary>
        public string CardType { get; set; }
        /// <summary>
        /// 卡类型名称
        /// </summary>
        public string CardTypeName { get; set; }
        public string mzh { get; set; }
        public int? ghnm { get; set; }
        public DateTime? ghsj { get; set; }
        public string brly { get; set; }
        public string ghrq { get; set; }

        public string lxr { get; set; }
        public string lxrgx { get; set; }
        public string lxrdh { get; set; }
        public string jjllrgx { get; set; }

        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 初复诊
        /// </summary>
        public byte fzbz { get; set; }

        /// <summary>
        /// 大病
        /// </summary>
        public string db { get; set; }

        /// <summary>
        /// 大病诊断
        /// </summary>
        public string dbzd { get; set; }

        /// <summary>
        /// 婚否 枚举 EnumHF
        /// </summary>
        public byte? hf { get; set; }

        /// <summary>
        /// 国籍
        /// </summary>
        public string gjCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gjmc { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string mzCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzmc { get; set; }

        /// <summary>
        /// 挂号状态
        /// </summary>
        public string ghzt { get; set; }

        /// <summary>
        /// 医保结算号
        /// </summary>
        public string ybjsh { get; set; }

        /// <summary>
        /// 就诊原因
        /// </summary>
        public string jzyy { get; set; }

        /// <summary>
        /// 社保编号
        /// </summary>
        public string sbbh { get; set; }

        /// <summary>
        /// 就诊标志
        /// </summary>
        public string jiuzhenbiaozhi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }

        /// <summary>一个
        /// 
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// 参保类别
        /// </summary>
        public string cblb { get; set; }
        public string grbh { get; set; }
        public string dwbh { get; set; }
        public string mjzbz { get; set; }


        public string zyh { get; set; }
        public string zybz { get; set; }
        /// <summary>
        /// 预交金报警额
        /// </summary>
        public decimal bje { get; set; }

    }
    

}