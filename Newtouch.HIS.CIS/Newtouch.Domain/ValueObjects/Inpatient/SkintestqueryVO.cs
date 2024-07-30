using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    public class SkintestqueryVO
    {
        /// <summary>
        /// 医嘱id
        /// </summary>
        public string yzid { get; set; }
        /// <summary>
        /// 医嘱性质
        /// </summary>
        public int? yzxz { get; set; }
        /// <summary>
        /// 医嘱性质说明
        /// </summary>
        public string yzxzsm { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? kssj { get; set; }
        /// <summary>
        /// 项目类型
        /// </summary>
        public string xmmc { get; set; }
        public string xmdm { get; set; }
        /// <summary>
        /// 药品剂量
        /// </summary>
        public Decimal? ypjl { get; set; }
        /// <summary>
        /// 医嘱内容
        /// </summary>
        public string yznr { get; set; }
        /// <summary>
        /// 停止/作废时间
        /// </summary>
        public DateTime? tzsj { get; set; }
        /// <summary>
        /// 停止/作废医生工号
        /// </summary>
        public string tzysgh { get; set; }
        /// <summary>
        /// 停止/作废操作员
        /// </summary>
        public string tzr { get; set; }

        public string Creator { get; set; }
        /// <summary>
        /// 医嘱组号
        /// </summary>
        public int? zh { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// 0 待审 1已审核 2已执行 3DC 4停止
        /// </summary>
        public int yzzt { get; set; }
        /// <summary>
        /// 医嘱类型
        /// </summary>
        //public int? yzlx { get; set; }
        public string lrjg { get; set; }

        public string zyh { get; set; }
        public string blh { get; set; }
        public string hzxm { get; set; }
        //public string yzlxmc { get; set; }
        public string yztag { get; set; }
        public string yztagName { get; set; }
        public int? isjf { get; set; }
        public string zh1 { get; set; }
        public string ispscs { get; set; }
        public string sex { get; set; }
        public string psbz { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastModifierName { get; set; }
        public string CreatorName { get; set; }
        public DateTime? LastModifyTime { get; set; }
    }
}
