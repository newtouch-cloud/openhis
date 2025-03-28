using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class SysMedicineElectronicPrescriptionVO
    {
        public string medListCodg { get; set; }
        public string natDrugNo { get; set; }
        public string genname { get; set; }
        public string prodname { get; set; }
        public string regName { get; set; }
        public string listType { get; set; }
        public string listTypeName { get; set; }
        public string specName { get; set; }
        public string prdrName { get; set; }
        public string aprvno { get; set; }
        public string dosformName { get; set; }
        public string minPacunt { get; set; }
        public string minPacCnt { get; set; }
        public string minPrepunt { get; set; }
        public string poolareaNo { get; set; }
        public string poolareaName { get; set; }
        public string dualchnlFlag { get; set; }
        public string oppoolFlag { get; set; }
        public string begntime { get; set; }
        public string endtime { get; set; }
        public string zt { get; set; }
    }
}
