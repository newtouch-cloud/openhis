using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ViewModels
{
    public class ReportConfigVO
    {
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// 应用代码Sett\CIS 等
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 报表名称
        /// </summary>
        public string RptName { get; set; }
        /// <summary>
        /// 报表服务root
        /// </summary>
        public string RptHost { get; set; }
        /// <summary>
        /// 报表地址
        /// </summary>
        public string RptUrl { get; set; }
        /// <summary>
        /// 数据源
        /// </summary>
        public string RptSource { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string Paras { get; set; }
        /// <summary>
        /// 工具栏 date（单日期） ,daterange（时间范围）
        /// </summary>
        public string Tools { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public int? px { get; set; }
        public string zt { get; set; }
        public IList<ReportPara> Paralist { get; set; }
        /// <summary>
        /// 报表 代码
        /// </summary>
        public string RptCode { get; set; }
    }

    public class ReportPara
    {
        public string p { get; set; }
    }

    public class ReportParaDetail
    {
        public string orgId { get; set; }
        public string zyh { get; set; }
        public string mzh { get; set; }
        public DateTime? kssj { get; set; }
        public DateTime? jssj { get; set; }
        public string rq { get; set; }
        public string doctor { get; set; }
        public string dept { get; set; }
        public string keyValue { get; set; }
        public string cfh { get; set; }
        public string sqdh { get; set; }
        public string lsh { get; set; }
    }
}
