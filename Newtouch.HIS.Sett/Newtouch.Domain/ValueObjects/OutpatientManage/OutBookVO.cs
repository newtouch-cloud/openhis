using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
    public class OutBookVO
    {

        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Key]
        public int ghpbId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zyi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zsan { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zsi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zwu { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zlv { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zri { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? zhl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ghzb { get; set; }

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
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ghlx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zlxm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mjzbz { get; set; }

        /// <summary>
        /// 科室名字
        /// </summary>
        public string ksmz { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        public string ysxm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// 诊疗项目名称
        /// </summary>
        public string zlxmmc { get; set; }
        /// <summary>
        /// 排班描述
        /// </summary>
        public string pbdesc { get; set; }

    }
}
