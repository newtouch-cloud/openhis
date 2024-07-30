using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects.MedicalRecord
{
    public class BljghVo
    {
    }

    public class BlwsInfoVo
    {
        public string dzbl_id { get; set; }
        public string ksmc { get; set; }
        public string ksdm { get; set; }
        public DateTime? qmrq { get; set; }
        public string czydm_zzys { get; set; }
        public DateTime? blrq { get; set; }
        public string creatorcode { get; set; }
        public string xmlConten { get; set; }
    }

    public class ElementTablStructureVo
    {
        public string Table_Column_Code { get; set; }
        public string Table_Colunn_Name { get; set; }
        public string Element_ID { get; set; }
        public string Element_Name { get; set; }
        public string Element_Value { get; set; }

    }
}
