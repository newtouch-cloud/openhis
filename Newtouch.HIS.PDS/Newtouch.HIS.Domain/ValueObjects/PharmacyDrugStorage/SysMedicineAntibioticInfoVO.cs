using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    public class SysMedicineAntibioticInfoVO : IEntity<SysMedicineAntibioticInfoVO>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 一级分类Id
        /// </summary>
        public string kssLevel1TypeId { get; set; }

        /// <summary>
        /// 二级分类Id
        /// </summary>
        public string kssLevel2TypeId { get; set; }

        /// <summary>
        /// 权限级别(0 非限制使用药物,1 限制使用药物,2 特殊使用药物)
        /// </summary>
        public string qxjb { get; set; }

        /// <summary>
        /// 剂量范围(大于)
        /// </summary>
        public decimal? jlfwBegin { get; set; }

        /// <summary>
        /// 剂量范围(小于)
        /// </summary>
        public decimal? jlfwEnd { get; set; }

        /// <summary>
        /// 频次范围(大于)
        /// </summary>
        public decimal? pcfwBegin { get; set; }

        /// <summary>
        /// 频次范围(小于)
        /// </summary>
        public decimal? pcfwEnd { get; set; }

        /// <summary>
        /// DDD值
        /// </summary>
        public decimal? DDDnum { get; set; }

        /// <summary>
        /// DDD单位
        /// </summary>
        public string DDDdw { get; set; }

        /// <summary>
        /// 效价
        /// </summary>
        public decimal? xj { get; set; }

        /// <summary>
        /// 空瓶(0 否,1 是)
        /// </summary>
        public string kp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 1：有效 0：无效
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// 抗生素剂量单位
        /// </summary>
        public string kssjldw { get; set; }
    }
}
