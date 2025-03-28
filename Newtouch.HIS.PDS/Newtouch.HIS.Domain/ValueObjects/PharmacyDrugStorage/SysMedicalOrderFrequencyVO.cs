using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    public class SysMedicalOrderFrequencyVO : IEntity<SysMedicalOrderFrequencyVO>
    {
        /// <summary>
        /// 
        /// </summary>
        public int yzpcId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yzpcCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yzpcmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int zxcs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int zxzq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zxzqdw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zxsj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

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
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 医嘱频次名称说明（中文名称）
        /// </summary>
        public string yzpcmcsm { get; set; }
    }
}
