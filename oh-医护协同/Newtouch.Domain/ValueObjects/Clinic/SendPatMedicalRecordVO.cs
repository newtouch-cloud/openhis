using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Clinic
{
    public class SendPatMedicalRecordVO : OutpMedicalRecordVO
    {
        public string ApplyId { get; set; }

        public string Sqlsh { get; set; }
    }
}
