namespace Newtouch.HIS.Domain.ValueObjects
{
    public class ChargeItemDetailVO
    {
        public int xmbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// 通用名
        /// </summary>
        public string xmmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 详见系统项目表“自负比例”说明
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 1 自理 2 分类自负   详见系统项目表“自负性质”说明      门诊可用，急诊不可用，或反之，以不同药剂部门有无药来控制
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string spm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal bzs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? jl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jldw { get; set; }
        /// <summary>
        /// xt_yc  修改直接保存名称
        /// </summary>
        public string ycmc { get; set; }
        /// <summary>
        /// 药品在各药房消耗时的“包装级别”或“拆零数信息”
        /// </summary>
        public string ypbzdm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string nbdl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dlmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ybdm { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string ypgg { get; set; }

        public int xmtype { get; set; }

        public string yfdm { get; set; }

        public string ksdm { get; set; }
        public string ypdm { get; set; }

        public string xmkc { get; set; }
    }
}
