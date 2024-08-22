using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Input
{
    public class SN01Input:InputBase
    {

        public string cardtype { get; set; }
        public string carddata { get; set; }
        /// <summary>
        /// 就诊单元号
        /// </summary>
        public string jzdyh { get; set; }

        /// <summary>
        /// 登记号
        /// </summary>
        public string djh { get; set; }

        /// <summary>
        /// 明细账单号
        /// </summary>
        public string mxzdh { get; set; }
        /// <summary>
        /// 本次费用明细包的医疗费用总额
        /// </summary>

        public decimal bcmxylfyze { get; set; }

        /// <summary>
        /// 结算类型标志
        /// </summary>
        public string jslxbz { get; set; }

        public List<MxXmsIn> mxxms { get; set; }

    }
    /// <summary>
    /// 上传明细
    /// </summary>
    public class MxXmsIn
    {
    
        public int xh { get; set; }

  
        public string cfh { get; set; }
        
        public string deptid { get; set; }
        
        public string ksmc { get; set; }
        
        public string cfysh { get; set; }
        
        public string cfysxm { get; set; }
        
        public string fylb { get; set; }
        
        public string mxxmbm { get; set; }
        
        public string mxxmmc { get; set; }
        
        public string mxxmdw { get; set; }
        
        public decimal mxxmdj { get; set; }
        
        public decimal mxxmsl { get; set; }
        
        public decimal mxxmje { get; set; }
        
        public decimal mxxmjyfy { get; set; }
        
        public decimal mxxmybjsfwfy { get; set; }
        
        public string yyclpp { get; set; }
        
        public string zczh { get; set; }


        public string mxxmgg { get; set; }
        
        public string mxxmsyrq { get; set; }
        
        public string bxbz { get; set; }

        public string sftfbz { get; set; }


        public string jfbz { get; set; }

        public string sfxfmx { get; set; }
        public decimal? zfbl { get; set; }
        public decimal? zfje { get; set; }
        public decimal? fyxj { get; set; }

        public List<XfMx> xfmx { get; set; }
    }
    /// <summary>
    /// 明细细分(项目组合)
    /// </summary>
    public class XfMx
    {
        public int xh { get; set; }

        public string mxxflsh { get; set; }

    
        public string mxxmbmzl { get; set; }

        public string mxxmmczl { get; set; }

        public string mxxmdwzl { get; set; }

        public decimal mxxmdjzl { get; set; }

        public decimal mxxmslzl { get; set; }

        public decimal mxxmjezl { get; set; }

        public string bxbzzl { get; set; }

        public string yyclppzl { get; set; }

        public string zczhzl { get; set; }

        public string mxxmggzl { get; set; }

        public string sftfbzzl { get; set; }

        public string mxxmsyrqzl { get; set; }
    }
}
