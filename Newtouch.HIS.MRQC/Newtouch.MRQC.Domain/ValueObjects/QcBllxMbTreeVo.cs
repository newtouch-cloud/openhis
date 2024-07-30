using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.Domain.ValueObjects
{
    public class BllxmbRecordsVo
    {
        public List<QcBllxMbTreeVo> bllxmbRecord { get; set; }
    }
    public class QcBllxMbTreeVo
    {
        public string Id { get; set; }

        public string OrganizeId { get; set; }

        public string parentId { get; set; }
        public string bllx { get; set; }
        public string bllxmc { get; set; }
        public string isroot { get; set; }
        public string ly { get; set; }
    }
}
