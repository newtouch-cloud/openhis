using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Domain.DTO.OutputDto
{
    [XmlRoot("INFO")]
    public class OutXMLDTO
    {
        public OUTMESSAGE MESSAGE { get; set; }
        public OUTDATA DATA { get; set; }
        

    }
    public class OUTMESSAGE {
        public string VERSION { get; set; }
        public string FORMAT { get; set; }
    }
    public class OUTDATA
    {
        public OUTBEAN BEAN { get; set; }
    }
    public class OUTBEAN
    {
        public string BILL_ID { get; set; }
        public string HIS_BILL_ID { get; set; }
        public string IS_ERROR { get; set; }
    }


}
