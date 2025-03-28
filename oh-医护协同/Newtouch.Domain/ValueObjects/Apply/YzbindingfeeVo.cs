using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Apply
{
    public class YzbindingfeeVo
    {
        public string yfdm { get; set; }
        public string ypbzdm { get; set; }
        public DateTime cyrq { get; set; }
        public DateTime ryrq { get; set; }
        public decimal je { get; set; }
        public string dw { get; set; }
        public decimal sl { get; set; }
        public string brxz { get; set; }
        public DateTime tdrq { get; set; }
        public string zyh { get; set; }
        public int? bqbh { get; set; }
        public string bq { get; set; }
        public string cw { get; set; }
        public string yzlx { get; set; }
        public int sfxmbh { get; set; }

        public string sfxm { get; set; }
        public decimal dj { get; set; }
        public string dl { get; set; }
        public string jfdw { get; set; }
        public decimal zfbl { get; set; }
        public string zfxz { get; set; }
        public string ybbm { get; set; }
        public int? ysbh { get; set; }
        public string ys { get; set; }
        public int? ksbh { get; set; }

        public string ks { get; set; }
        public string zxksbh { get; set; }
        public string zxks { get; set; }
        public string ysmc { get; set; }
        public string ksmc { get; set; }

        /// <summary>
        /// 拆零数
        /// </summary>
        public int? cls { get; set; }
        public decimal ztsl { get; set; }
        public string dlmc { get; set; }
        public string sfxmmc { get; set; }
        public string pcCode { get; set; }
        /// <summary>
        /// 执行次数
        /// </summary>
        /// <returns></returns>
        public int? zxcs { get; set; }
        /// <summary>
        /// 执行周期
        /// </summary>
        /// <returns></returns>
        public int? zxzq { get; set; }
        /// <summary>
        /// 执行周期单位 -1:不规则周期，0：天,1：小时,2：分钟.注意：当为-1时，周代码（zdm）为数字（1234567）中的任意几个数字
        /// </summary>
        /// <returns></returns>
        public int? zxzqdw { get; set; }

        public string gg { get; set; }
        public string pcmc { get; set; }
        public string yzxz { get; set; }
    }
}
