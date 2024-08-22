using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class SysPatientManagerVO
    {
        public int patid { get; set; }
        public string brxzmc { get; set; }
        public string blh { get; set; }
        public string xm { get; set; }
        public string xl { get; set; }
        public string xb { get; set; }
        public string py { get; set; }
        public string csny { get; set; }
        public string zjh { get; set; }

        public string zjlx { get; set; }

        public short nl { get; set; }

        public string dh { get; set; }

        public string jjllr { get; set; }

        public string jjlrdh { get; set; }

        public DateTime CreateTime { get; set; }

        public string CardNo { get; set; }
    }
}
