using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Domain.DTO.OutputDto
{
    [XmlRoot("INFO")]
    public class DiagnoseDTO
    {
        public DiagnoseSSAGE MESSAGE { get; set; }
        public DiagnoseDATA DATA { get; set; }
    }
    public class DiagnoseSSAGE
    {
        public string VERSION { get; set; }
    }
    public class DiagnoseDATA
    {
        public DiagnoseBEAN BEAN { get; set; }
    }
    public class DiagnoseBEAN
    {
        public string ICD_CODE { get; set; }
        public string ICD_NAME { get; set; }
    }

}
