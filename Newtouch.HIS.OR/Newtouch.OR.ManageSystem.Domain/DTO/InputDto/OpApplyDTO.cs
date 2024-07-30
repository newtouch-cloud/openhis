using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.DTO
{
    public class OpApplyDTO
    {
        public string organizeid { get; set; }
        public string staffgh { get; set; }
        public string Id { get; set; }
        public string sqzt { get; set; }

        public string sq_zyh { get; set; }
        public string sq_bqmc { get; set; }
        public string sq_bedcode { get; set; }
        public string sq_ryzdmc { get; set; }


        public string ssdm { get; set; }
        public string ssmcn { get; set; }
        public DateTime? sssj { get; set; }
        public string ssbw { get; set; }
        public string AnesCode { get; set; }
        public string ysgh { get; set; }
        public string ysxm { get; set; }
        public string mzys { get; set; }
		public string isgl { get; set; }
       
        public string Applyno { get; set; }

		public List<ORApplyInfoExpandEntity> ss { get; set; }

        
    }

    public class OpApplySubmitDTO
    {
        public string OrganizeId { get; set; }
        public string staffgh { get; set; }
        public string Id { get; set; }
        public string sqzt { get; set; }

        public string zyh { get; set; }
        public string bqmc { get; set; }
        public string bedcode { get; set; }
        public string zdmc { get; set; }


        public string[] ssdm { get; set; }
        public string[] ssmcn { get; set; }
        public DateTime? sssj { get; set; }
        public string ssbw { get; set; }
        public string AnesCode { get; set; }
        public string ysgh { get; set; }
        public string ysxm { get; set; }
        public string mzys { get; set; }
        public string mzysxm { get; set; }
        public string isgl { get; set; }

        public string Applyno { get; set; }

    }
}
