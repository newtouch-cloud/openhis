using Newtouch.HIS.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects.TherapeutistCompleteManage
{
    /// <summary>
    /// 治疗师工作时间查询返回类
    /// </summary>
    [NotMapped]
    public class TherapeutistWorkPlanGridVO : DoctorWorkingDaysPlanEntity
    {
        public string Name { get; set; }
    }

    /// <summary>
    /// 新增排班时，显示所有治疗师
    /// </summary>
    [NotMapped]
    public class AddStaffPlanVO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string gh { get; set; }
        public int ts { get; set; }
    }

    /// <summary>
    /// 新增排班时，提交到数据库类
    /// </summary>
    [NotMapped]
    public class EditPlanVO
    {
        public string month { get; set; }
        public string year { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string ysgh { get; set; }
        public int ts { get; set; }
    }
    /// <summary>
    /// 时长调整时，统计治疗师时长类
    /// </summary>
    [NotMapped]
    public class AdjustStaffHourVO
    {
        public string type { get; set; }
        public string Id { get; set; }
        public string ysgh { get; set; }
        public string jzsj { get; set; }
        public decimal? sc { get; set; }
        public decimal? tzsc { get; set; }
        public string NAME { get; set; }
        public int? smonth { get; set; }
        public int syear { get; set; }
        public string tzly { get; set; }
    }
    [NotMapped]
    public class updatetimeVO
    {
        public string ysgh { get; set; }
        public string jzsj { get; set; }
        public decimal? sc { get; set; }
        public decimal? sjsc { get; set; }
    }
    [NotMapped]
    public class submitTimeVO
    {
        public string type { get; set; }//区分门诊住院
        public int Id { get; set; }
        public string jqRowId { get; set; }
        public string jzsj { get; set; }
        public string sc { get; set; }
        public string tzsc { get; set; }
        public string tzly { get; set; }
    }
    [NotMapped]
    public class StaffReportVO
    {
        public string ysgh { get; set; }
        public string NAME { get; set; }
        public int? smonth { get; set; }
        public int syear { get; set; }
        public decimal zlgs { get; set; }
        public decimal fzlgs { get; set; }
        public decimal zgs { get; set; }
        public decimal zlzb1 { get; set; }
        public decimal zlzb2 { get; set; }
    }
}
