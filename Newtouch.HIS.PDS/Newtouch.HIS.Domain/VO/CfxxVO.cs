using System;

namespace Newtouch.HIS.Domain.VO
{
    /// <summary>
    /// 处方信息
    /// </summary>
    public class CfxxVO
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 医护协同工作站的chufangId
        /// </summary>
        public string cfId { get; set; }

        /// <summary>
        /// 门诊号
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// 处方标签
        /// </summary>
        public string cftag { get; set; }

        /// <summary>
        /// 处方类型  1-西药处方 2-中药处方 3-康复处方 4-检验处方 5-检查处方 6-输液处方
        /// </summary>
        public int cflx { get;set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 处方内码
        /// </summary>
        public long? cfnm { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string Fph { get; set; }
        
        /// <summary>
        /// 医生名称
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 0：未排；1：已排；2：已发；3：已退
        /// </summary>
        public string fybz { get; set; }

        /// <summary>
        /// 收费时间
        /// </summary>
        public DateTime? sfsj { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? fysj { get; set; }

        /// <summary>
        /// 结算内码
        /// </summary>
        public long jsnm { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int? nl { get; set; }

        /// <summary>
        /// 病人性质
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// 领药药房
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// 组织机构Id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 处方金额
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 0：无效 1：有效
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 创建人员
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 修改人员
        /// </summary>
        public string LastModiFierCode { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}