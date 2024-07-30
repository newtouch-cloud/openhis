using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects.MedicalRecord
{
    public class BackuptimetreeVO
    {
        public string id { get; set; }
        public string name { get; set; }
        public string parentid { get; set; }
    }
}
