namespace NewtouchHIS.WebAPI.Manage.Areas.HisJob
{
    public class NoticeJobResult
    {
        public string? JobId { get; set; }
        public string? NoticeId { get; set; }
        public string? QueueId { get; set; }
        public string[]? QueueIds { get; set; }
        public int? NoticeStu { get; set; }
        public string? ErrMessage { get; set; }
    }
}
