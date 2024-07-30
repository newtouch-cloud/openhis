using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Newtouch.Domain.DTO.OutputDto
{
    [XmlRoot("INFO")]
    public class DeletePriorReviewDTO
    {
        public DELPRMESSAGE MESSAGE { get; set; }
        public DELPRDATA DATA { get; set; }
    }
    public class DELPRMESSAGE
    {
        public string VERSION { get; set; }
    }
    public class DELPRDATA
    {
        public DELPRBEAN BEAN { get; set; }
    }
    public class DELPRBEAN
    {
        public string BILL_ID { get; set; }
        public string HIS_BILL_ID { get; set; }
        public string HIS_BILL_DETAIL_ID { get; set; }
    }

}
