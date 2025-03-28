using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.Domain.ValueObjects.Apply
{
    public class MedicalApplyVo
    {
        public string Id { get; set; }
        public string ApplyDept { get; set; }
        public string ApplyDeptName { get; set; }
        public string Bed { get; set; }
        public string PatName { get; set; }
        public string ApplyDoctor { get; set; }
        public string ApplyDoctorName { get; set; }
        public int ApplyType { get; set; }
        public string MedicalName { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? ApplyDate { get; set; }
        public DateTime? ApplyCompletionDate { get; set; }
        public string ApplyReason { get; set; }
        public int ApplyStatus { get; set; }
        public string Approver { get; set; }
        public string ApproverName { get; set; }
        public string ApproverDept { get; set; }
        public string ApproverDeptName { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? Ryrq { get; set; }
        public DateTime? Cyrq { get; set; }
    }
}
