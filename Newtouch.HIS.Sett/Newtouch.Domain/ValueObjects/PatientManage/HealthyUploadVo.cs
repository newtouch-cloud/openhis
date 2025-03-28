using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.PatientManage
{
    public class HealthyUploadVo
    {
        public int Id { get; set; }
        public string TabName { get; set; }
        public string TabNameDesc { get; set; }
        public string status { get; set; }
        public string err_msg { get; set; }
        public DateTime createtime { get; set; }
        public string statusName { get; set; }
        public string OrganizeId { get; set; }
    }
}
