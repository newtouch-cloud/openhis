using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 住院项目计费 项目 的 详细信息
    /// </summary>
    public class MedicalRecordDeptVO
    {
        public string version { get; set; }
        public List<medicalList> medicalList { get; set; }
        public string[] medicalArr { get; set; }
    }
    public class medicalList {
        public string Index { get; set; }
        public string gender { get; set; }
        public string age { get; set; }

        public string ageDay { get; set; }
        public string weight { get; set; }
        public string dept { get; set; }
        public string inHospitalTime { get; set; }
        public string leavingType { get; set; }
        public string[] zdList { get; set; }
        public string[] ssList { get; set; }
        public string remark { get; set; }
       
    }
}
