using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Clinic
{
    public class OutBookScheduleVO
    {
        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// ScheduId
        /// </summary>
        public Decimal ScheduId { get; set; }
        /// <summary>
        /// 排班编号
        /// </summary>
        public string ghpbId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OutDate { get; set; }
        /// <summary>
        /// 医生工号
        /// </summary>
        public string ysgh { get; set; }
        /// <summary>
        /// 医生姓名
        /// </summary>
        public string ysxm { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        public string czks { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string czksmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RegType { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 时间段
        /// </summary>
        public string Period { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PeriodDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TotalNum { get; set; }
        /// <summary>
        /// 开始时间段
        /// </summary>
        public string PeriodStart { get; set; }
        /// <summary>
        /// 结束时间断
        /// </summary>
        public string PeriodEnd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Decimal RegFee { get; set; }
        /// <summary>
        /// 是否可预约
        /// </summary>
        public string IsBook { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ghlx { get; set; }
        /// <summary>
        /// 诊疗项目
        /// </summary>
        public string zlxm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int weekdd { get; set; }
        /// <summary>
        /// 是否取消
        /// </summary>
        public string IsCancel { get; set; }
        /// <summary>
        /// 取消原因
        /// </summary>
        public string CancelReason { get; set; }
        /// <summary>
        /// 取消时间
        /// </summary>
        public string CancelTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// 预约数
        /// </summary>
        public int YYNum { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// 上次修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 上次修改人
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// 是否停诊 0正常出诊  1停诊状态
        /// </summary>
        public string istz { get; set; }
        /// <summary>
        /// 停诊原因
        /// </summary>
        public string tzyy { get; set; }
        /// <summary>
        /// 停诊时间
        /// </summary>
        public string tzsj { get; set; }
    }
}
