using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.ValueObjects
{
    public class ArrangeRegVO : IEntity<ORArrangementEntity>
    {
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 手术申请ID(ApplyInfoId)
        /// </summary>
        /// <returns></returns>
        public string ApplyId { get; set; }

        /// <summary>
        /// 申请编码
        /// </summary>
        /// <returns></returns>
        public string Applyno { get; set; }

        /// <summary>
        /// 手术登记码
        /// </summary>
        /// <returns></returns>
        public string ssxh { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        /// <returns></returns>
        public string xm { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        /// <returns></returns>
        public string ks { get; set; }

        /// <summary>
        /// 病区
        /// </summary>
        /// <returns></returns>
        public string bq { get; set; }

        /// <summary>
        /// 床号
        /// </summary>
        /// <returns></returns>
        public string ch { get; set; }

        /// <summary>
        /// 诊断
        /// </summary>
        /// <returns></returns>
        public string zd { get; set; }

        /// <summary>
        /// 申请状态
        /// </summary>
        /// <returns></returns>
        public string sqzt { get; set; }

        /// <summary>
        /// 手术名称
        /// </summary>
        /// <returns></returns>
        public string ssmc { get; set; }

        /// <summary>
        /// 手术代码
        /// </summary>
        /// <returns></returns>
        public string ssdm { get; set; }

        /// <summary>
        /// 手术时间
        /// </summary>
        /// <returns></returns>
        public DateTime? sssj { get; set; }

        /// <summary>
        /// 主刀医生编号
        /// </summary>
        /// <returns></returns>
        public string ysgh { get; set; }

        /// <summary>
        /// 主刀医生姓名
        /// </summary>
        /// <returns></returns>
        public string ysxm { get; set; }

        /// <summary>
        /// 麻醉方式
        /// </summary>
        /// <returns></returns>
        public string AnesCode { get; set; }

        /// <summary>
        /// 手术室
        /// </summary>
        /// <returns></returns>
        public string oproom { get; set; }

        /// <summary>
        /// 台次
        /// </summary>
        /// <returns></returns>
        public string oporder { get; set; }

        /// <summary>
        /// 助理医生1
        /// </summary>
        /// <returns></returns>
        public string zlys1 { get; set; }

        /// <summary>
        /// 助理医生2
        /// </summary>
        /// <returns></returns>
        public string zlys2 { get; set; }

        /// <summary>
        /// 助理医生3
        /// </summary>
        /// <returns></returns>
        public string zlys3 { get; set; }

        /// <summary>
        /// 助理医生4
        /// </summary>
        /// <returns></returns>
        public string zlys4 { get; set; }

        /// <summary>
        /// 巡回护士
        /// </summary>
        /// <returns></returns>
        public string xhhs { get; set; }

        /// <summary>
        /// 洗手护士
        /// </summary>
        /// <returns></returns>
        public string xshs { get; set; }

        /// <summary>
        /// 手术部位
        /// </summary>
        /// <returns></returns>
        public string ssbw { get; set; }

        /// <summary>
        /// 状态     1:有效  0：无效
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 登记状态
        /// </summary>
        /// <returns></returns>
        public string djzt { get; set; }
    }
}
