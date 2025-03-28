using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    public class BythDjInfoDTO
    {
        public string yfbm { get; set; }
        public string rkbm { get; set; }
        public string ksbm { get; set; }
        public string djh { get; set; }
        public string thyy { get; set; }
        public string issavesubmit { get; set; }
        public string isdeldj { get; set; }
        public List<ThDjDetailDTO> mx { get; set; }
    }

    public class ThDjDetailDTO
    {
        public string Id { get; set; }
        public string byId { get; set; }
        public string OrganizeId { get; set; }
        public string ypdm { get; set; }
        public string ypmc { get; set; }
        public string yplb { get; set; }
        public decimal tsl { get; set; }
        public string dw { get; set; }
        public string gg { get; set; }
        public DateTime? yxq { get; set; }
        public string yfbm { get; set; }
        public string thyy { get; set; }
        public string sccj { get; set; }
        public string ph { get; set; }
        public string pc { get; set; }
    }
}
