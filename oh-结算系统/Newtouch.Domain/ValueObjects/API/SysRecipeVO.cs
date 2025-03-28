using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.API
{
    public class SysRecipeVO
    {
        /// <summary>
        /// 处方ID
        /// </summary>
        public int cfnm { get; set; }
        /// <summary>
        /// 门诊流水号
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }
        /// <summary>
        /// 就诊卡号
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// 处方状态
        /// </summary>
        public string cfzt { get; set; }
        /// <summary>
        /// 处方类型
        /// </summary>
        public int cflx { get; set; }
        /// <summary>
        /// 处方类型名称
        /// </summary>
        public string cflxmc { get; set; }
        /// <summary>
        /// 开方科室编号
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// 开方科室名称
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// 开方医生职称编号
        /// </summary>
        public string dutyId { get; set; }
        /// <summary>
        /// 开方医生职称
        /// </summary>
        public string dutyName { get; set; }
        /// <summary>
        /// 开方医生工号
        /// </summary>
        public string gh { get; set; }
        /// <summary>
        /// 开方医生姓名
        /// </summary>
        public string ysxm { get; set; }
        /// <summary>
        /// 开方时间
        /// </summary>
        public DateTime? createTime { get; set; }
        /// <summary>
        /// 处方金额
        /// </summary>
        public Decimal zje { get; set; }
        /// <summary>
        /// 急诊标志
        /// </summary>
        public string mjzbz { get; set; }
    }
}
