using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Base.API.Notice
{
    public class NoticeModel
    {
        public string appid { get; set; }
        public string orgid { get; set; }
        public string user { get; set; }
        public string message { get; set; }
        public string msgid { get; set; }

    }

    public class MsgGetRequest: RequestBase
    {
        public string nick { get; set; }
        public string id { get; set; }
    }
}