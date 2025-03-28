
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    public class ShybBrxzblVo
    {
        public string Code { get; set; }
        public string xmcode { get; set; }
        public string xmmc { get; set; }
        public string xzId { get; set; }
        public string xzmc { get; set; }
        public decimal zfbl { get; set; }
        public decimal fyxe { get; set; }
        public decimal cxbl { get; set; }
    }
}