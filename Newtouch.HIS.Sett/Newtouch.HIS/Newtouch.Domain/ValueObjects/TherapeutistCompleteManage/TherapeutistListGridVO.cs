

using System;

namespace Newtouch.HIS.Domain.ValueObjects.TherapeutistCompleteManage
{
    /// <summary>
    /// 显示在治疗师工作列表的Grid
    /// </summary>
    public class TherapeutistListGridVO
    {
        public string type { get; set; }
        public decimal sl { get; set; }
        public decimal zlsc { get; set; }
        public DateTime? jzsj { get; set; }
        public int jfbbh { get; set; }
        public string ysgh { get; set; }
        public string ysxm { get; set; }
        public string hzxm { get; set; }
        public string dlmc { get; set; }
        public string dlcode { get; set; }
        public string sfxmmc { get; set; }
    }
}
