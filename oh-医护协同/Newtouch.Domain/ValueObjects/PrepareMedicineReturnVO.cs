using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    public class PrepareMedicineReturnVO
    {
        public string Id { get; set; }
        public string thzt { get; set; }
        public string djh { get; set; }
        public string yfbm { get; set; }
        public string yfmc { get; set; }

        public string ksbm { get; set; }

        public string bqbm { get; set; }
        public string ksmc{ get; set; }
        public string bqmc { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime?  tjsj { get; set; }
        public string thyy { get; set; }
    }
}
