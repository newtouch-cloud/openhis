using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
    public class OutPatientChargeMainDto {
        #region 挂号收费查询的主记录

        /// <summary>
        /// 病例号
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// 结算内码
        /// </summary>
        public int jsnm { get; set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }
        /// <summary>
        /// 建档人员
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 挂号医生名称（有可能是后更新）
        /// </summary>
        public string ghysmc { get; set; }
        /// <summary>
        /// 挂号科室名称
        /// </summary>
        public string ghksmc { get; set; }
        /// <summary>
        /// 老的发票号
        /// </summary>
        public string oldfph { get; set; }
        /// <summary>
        /// isxfph
        /// </summary>
        public string isxfph { get; set; }
        /// <summary>
        /// tCreatorCode
        /// </summary>
        public string tCreatorCode { get; set; }

        /// <summary>
        /// 收费员姓名
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 结算类型
        /// </summary>
        public string jslx { get; set; }

        #endregion

        /// <summary>
        /// 收费总金额
        /// </summary>
        public decimal jszje { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal? jsxjzf { get; set; }

        /// <summary>
        /// 收费日期
        /// </summary>
        public DateTime? sfrq { get; set; }

        /// <summary>
        /// 病人性质名称
        /// </summary>
        public string brxzmc { get; set; }
        /// <summary>
        /// 结算对应挂号的门诊号
        /// </summary>
        public string mzh { get; set; }

    }
    /// <summary>
    /// for API
    /// </summary>
    public class OutPatientChargeInfoDto
    {
        public IList<OutPatientChargeMainDto> MainRecords;
        public IList<OutPatientRegChargeDetailsVO> DetailRecords;
    }
}
