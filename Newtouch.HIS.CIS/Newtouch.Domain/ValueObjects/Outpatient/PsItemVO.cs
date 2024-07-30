using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// 皮试项目信息
    /// </summary>
    public class PsItemVO
    {
        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 处方明细id
        /// </summary>
        public string cfmxId { get; set; }

        /// <summary>
        /// 项目Code
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string xmmc { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 执行人
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 过敏信息Id
        /// </summary>
        public string gmxxId { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 执行结果
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 剂量
        /// </summary>
        public decimal? ypjl { get; set; }

        /// <summary>
        /// 药品规格
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// 医嘱内容
        /// </summary>
        public string yznr { get; set; }

        /// <summary>
        /// 医嘱Id
        /// </summary>
        public string yzId { get; set; }
    }
}
