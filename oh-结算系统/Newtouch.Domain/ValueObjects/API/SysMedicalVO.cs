using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.API
{
    public class SysMedicalVO
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int sfxmId { get; set; }
        /// <summary>
        /// 医疗项目编码
        /// </summary>
        public string sfxmCode { get; set; }
        /// <summary>
        /// 医疗项目名称
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal dj { get; set; }
        /// <summary>
        /// 项目分类编号
        /// </summary>
        public string sfdlCode { get; set; }
        /// <summary>
        /// 项目分类名称
        /// </summary>
        public string dlmc { get; set; }
        /// <summary>
        /// 医保医疗编码
        /// </summary>
        public string ybdm { get; set; }
        /// <summary>
        /// 自负性质(医保等级)  enum EnumZiFuXingZhi
        /// </summary>
        public string zfxz { get; set; }
    }
}
