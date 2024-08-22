using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    public class SysChargeItemTemplateVO : SysChargeItemVEntity
    {
        public int? sl { get; set; }

        public string kflb { get; set; }
        public string kflbmc { get; set; }

        /// <summary>
        /// 治疗量
        /// </summary>
        public int? zll { get; set; }

        /// <summary>
        /// 执行次数
        /// </summary>
        public int? zxcs { get; set; }

        /// <summary>
        /// 医嘱频次
        /// </summary>
        public string yzpc { get; set; }

        /// <summary>
        /// 部位（康复治疗建议）
        /// </summary>
        public string bw { get; set; }
        public string yfdm { get; set; }
        public string yfmc { get; set; }
    }
}
