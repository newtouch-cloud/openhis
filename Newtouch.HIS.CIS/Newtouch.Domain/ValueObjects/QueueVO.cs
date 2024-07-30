using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    public class QueueVO
    {
        public int sp_calling_no { get; set; }
        public string sp_calling_xm { get; set; }
        public string sp_calling_dept { get; set; }
        public string sp_calling_doc { get; set; }
        public string sp_calling_ywlx { get; set; }
        public int sp_nextcall_no { get; set; }
        public string sp_nextcall_xm { get; set; }
        public string sp_nextcall_dept { get; set; }
        public string sp_nextcall_doc { get; set; }
        public string sp_nextcall_ywlx { get; set; }
        public int sp_waiting_count { get; set; }
    }
}
