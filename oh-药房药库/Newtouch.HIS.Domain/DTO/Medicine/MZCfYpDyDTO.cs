using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.Domain.DTO.Medicine
{
    public class MZCfYpDyDTO
    {
        public long Id { get; set; }

        /// <summary>
        /// 处方内码
        /// </summary>
        public long cfnm { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 结算内码
        /// </summary>
        public long jsnm { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string Fph { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int? nl { get; set; }

        /// <summary>
        /// 病人性质
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// 医生
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 领药药房
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// 收费时间
        /// </summary>
        public DateTime? sfsj { get; set; }

        /// <summary>
        /// 0：未排；1：已排；2：已发；3：已退
        /// </summary>
        public string fybz { get; set; }

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
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改人员
        /// </summary>
        public string LastModiFierCode { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        public string cfId { get; set; }
        public string mzh { get; set; }
    }
}