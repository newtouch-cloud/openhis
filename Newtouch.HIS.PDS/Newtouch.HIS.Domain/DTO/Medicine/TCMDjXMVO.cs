using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.Domain.DTO.Medicine
{
    public class TCMDjXMVO
    {
        public string sfxmCode { get; set; }
        public string sfxmmc { get; set; }

        public decimal dj { get; set; }

        public string dw { get; set; }
        public string gjybdm { get; set; }
        public string sfdlCode { get; set; }
        public string zfxz { get; set; }
        public decimal zfbl { get; set; }
    }
}