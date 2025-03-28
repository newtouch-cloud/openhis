using NewtouchHIS.Domain.Entity.EMR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.InterfaceObjets.EMR
{
    public class MedicalBlCenterVo
    {

    }
    /// <summary>
    /// 出入院诊断
    /// </summary>
    public class MedDiagListVo
    {
        public string Zyh { get; set; }
        public string ZdOrder { get; set; }
        public string ZdType { get; set; }
        public string Zddm { get; set; }
        public string Zdmc { get; set; }
        public DateTime? Zdrq { get; set; }
        public string Zdys { get; set; }

    }
    /// <summary>
    /// 病历文书tree
    /// </summary>
    public class MedRecordTree 
    {
        public string Id { get; set; }
        public int? Blzt { get; set; } 
        public string Blmc { get; set; }
        public string ParentId { get; set; }
        public string Bllx { get; set; }
        public string Zyh { get; set; }
        public string BlId { get; set; }
        public string MbId { get; set; }
        public string Doccode { get; set; }
        public string Docname { get; set; }
        public DateTime? Blrq { get; set; }
        public string Mblj { get; set; }
        public string IsRoot { get; set; }
    }
}
