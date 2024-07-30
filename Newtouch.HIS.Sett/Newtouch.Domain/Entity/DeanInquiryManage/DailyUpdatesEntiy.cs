using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.DeanInquiryManage
{
    /// <summary>
    /// 当日动态_全院收入概况
    /// </summary>
    public class DailyUpdatesEntiy
    {
        /// <summary>
        /// 门诊科室
        /// </summary>
        public string mzks { get; set; } 

        /// <summary>
        /// 已就诊
        /// </summary>
        public int yjz { get; set; }

        /// <summary>
        /// 就诊中
        /// </summary>
        public int jzz { get; set; }
    }
    /// <summary>
    /// 院长查询-今日动态-banner
    /// </summary>
    public class JRDT_Banner
    {
        /// <summary>
        /// 医院总收入
        /// </summary>
        public decimal? yyzsr { get; set; }
        /// <summary>
        /// 门诊收入
        /// </summary>
        public decimal? mzsr { get; set; }
        /// <summary>
        /// 门诊人次
        /// </summary>
        public int? mzrc { get; set; }
        /// <summary>
        /// 住院收入
        /// </summary>
        public decimal? zysr { get; set; }
        /// <summary>
        /// 出院人数
        /// </summary>
        public int? cyrs { get; set; }
    }
    /// <summary>
    /// 当日动态_今日动态门诊
    /// </summary>
    public class DailyUpdates_GetJrdtmz
    {
        /// <summary>
        /// 门诊科室
        /// </summary>
        public string mzks { get; set; }

        /// <summary>
        /// 已就诊
        /// </summary>
        public int yjz { get; set; }

        /// <summary>
        /// 就诊中
        /// </summary>
        public int jzz { get; set; }
        /// <summary>
        /// 待就诊
        /// </summary>
        public int djz { get; set; }
    }

    /// <summary>
    /// 当日动态_门诊处方
    /// </summary>
    public class DailyUpdates_GetMzcf
    {
        /// <summary>
        /// 项目
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal? sl { get; set; }

    }

    /// <summary>
    /// 当日动态_门诊费用
    /// </summary>
    public class DailyUpdates_GetMzfy
    {
        /// <summary>
        /// 费用分类
        /// </summary>
        public string fyfl { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal? je { get; set; }
        /// <summary>
        /// 占比
        /// </summary>
        public decimal? zb { get; set; }
    }

    /// <summary>
    /// 当日动态_住院占床率
    /// </summary>
    public class DailyUpdates_GetZyzcl
    {
        /// <summary>
        /// 项目
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int sl { get; set; }
    }

    /// <summary>
    /// 当日动态_门诊挂号统计
    /// </summary>
    public class DailyUpdates_GetMzghtj
    {
        /// <summary>
        /// 挂号类型
        /// </summary>
        public string ghlx { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int sl { get; set; }
    }

    /// <summary>
    /// 当日动态_住院患者统计
    /// </summary>
    public class DailyUpdates_GetZyhztj
    {
        /// <summary>
        /// 项目
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int sl { get; set; }
    }

}
