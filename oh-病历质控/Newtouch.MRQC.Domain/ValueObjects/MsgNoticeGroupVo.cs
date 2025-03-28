using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.Domain.ValueObjects
{
    public class MsgNoticeGroupVo
    {
        public string Id { get; set; }
    }

    public class MsgNoticeQueueVo
    {
        public string GroupTag { get; set; }
        public string GroupName { get; set; }
        public string GroupDesc { get; set; }
        public string Id { get; set; }

        public string OrganizeId { get; set; }

        public string NoticeId { get; set; }

        public string SendFrom { get; set; }

        public string NoticeGroupId { get; set; }

        public int GroupYwlx { get; set; }

        public int NoticeRange { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ContentData { get; set; }

        public string OpenPath { get; set; }

        public string OpenPathPara { get; set; }

        public string Recipient { get; set; }

        public int RecipientType { get; set; }

        public int QueueExecType { get; set; }
 
        public string ExecCron { get; set; }

        public int NoticeStu { get; set; }
        public int? px { get; set; }

        public string AppId { get; set; }

        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }


        public DateTime? LastModifyTime { get; set; }

        public string LastModifierCode { get; set; }

        public string zt { get; set; }
    }
}
