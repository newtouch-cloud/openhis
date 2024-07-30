using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 住院项目计费 项目 的 详细信息
    /// </summary>
    public class MedicalRecordPatVO
    {
        public string bah { get; set; }
        public string xb { get; set; }
        public string nl { get; set; }
        public string nlt { get; set; }
        public string cstz { get; set; }
        public string cykb { get; set; }
        public string zyts { get; set; }
        public string lyfs { get; set; }
        public decimal zfy { get; set; }
        public string zdlist { get; set; }
        public string sslist { get; set; }
        public string brxzmc { get; set; }
    }

}
