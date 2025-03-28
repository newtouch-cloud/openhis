using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ViewModels
{
    public class NoticeJobRequest
    {
        public string JobId { get; set; }
        public string NoticeId { get; set; }
        public string QueueId { get; set; }
        public string[] QueueIds { get; set; }
        public int? NoticeStu { get; set; }
        public string ErrMessage { get; set; }
    }
}
