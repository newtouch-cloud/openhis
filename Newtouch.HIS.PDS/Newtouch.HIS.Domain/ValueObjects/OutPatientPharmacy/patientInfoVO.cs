using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 门诊发药信息
    /// </summary>
    public class OutpatienDrugDeliveryInfo
    {
        /// <summary>
        /// 药房部门编码
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 操作员账号
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// 发药患者信息
        /// </summary>
        public List<patientInfoVO> PatientInfo { get; set; }
    }


    /// <summary>
    /// 患者信息 
    /// </summary>
    [Serializable]
    public class patientInfoVO
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 收费时间
        /// </summary>
        public string ShowSfsj { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        public string cfnm { get; set; }

        /// <summary>
        /// 追溯码
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// 是否拆零
        /// 1： 是
        /// 2： 否
        /// </summary>
        public int? sfcl { get; set; }
    }
}
