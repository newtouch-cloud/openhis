using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.Domain.ValueObjects
{
    public class MedicalBlCenterVo
    {
        ///// <summary>
        ///// 诊断列表
        ///// </summary>
        //public List<MedicalHomeDiagVo> DiagList { get; set; }
        //public List<MedRecordTree> MedRecordTree { get; set; }
    }
    /// <summary>
    /// 病历文书tree
    /// </summary>
    public class MedRecordTree
    {
        public string Id { get; set; }
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
        public int? Blzt { get; set; }
        public string IsRoot { get; set; }
    }
    /// <summary>
    /// 诊断list
    /// </summary>
    public class MedicalHomeDiagVo
    {
        public string Zyh { get; set; }
        public string ZdOrder { get; set; }
        public string ZdType { get; set; }
        public string Zddm { get; set; }
        public string Zdmc { get; set; }
        public DateTime? Zdrq { get; set; }
        public string Zdys { get; set; }
    }
}
