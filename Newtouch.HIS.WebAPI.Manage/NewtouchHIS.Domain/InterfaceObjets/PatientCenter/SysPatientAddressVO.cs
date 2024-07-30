using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.InterfaceObjets.PatientCenter
{
    public  class SysPatientAddressVO
    {
    }

    /// <summary>
    /// 患者地址查询索引
    /// </summary>
    public class SysPatientAddressIndexVO 
    {

        public string? OrganizeId { get; set; }
        public int patid { get; set; }
    }

    /// <summary>
    /// 患者地址信息
    /// </summary>
    public class SysPatientAddressInfoVO {
        public string Id { get; set; }
        public string? OrganizeId { get; set; }
        public int patid { get; set; }
        public string xm { get; set; }
        public string dh { get; set; }
        public string xian_sheng { get; set; }
        public string xian_shi { get; set; }
        public string xian_xian { get; set; }
        public string xian_dz { get; set; }
    }

    public class SysPatientOrderAddressIndexVO
    {
        public string? OrganizeId { get; set; }
        public string visitNo { get; set; }
        //public string orderNo { get; set; }
        public string ks { get; set; }
    }
}
