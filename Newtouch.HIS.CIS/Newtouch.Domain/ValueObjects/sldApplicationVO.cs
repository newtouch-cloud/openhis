using System;
using System.Collections.Generic;

namespace Newtouch.Domain.ValueObjects
{
    public class sldApplication 
    {


       public sldApplicationVO sld;
       public List<sldmxApplicationVO> sldmx;
    }
    public class sldApplicationVO
    {
        public string OrganizeId { get; set; }
        /// <summary>
        /// 申请科室代码
        /// </summary>
        public string sqkscode { get; set; }
        /// <summary>
        /// 申请科室名称
        /// </summary>
        public string sqksname { get; set; }
        /// <summary>
        /// 申请病区代码
        /// </summary>
        public string sqbqcode { get; set; }

        /// <summary>
        /// 申请病区名称
        /// </summary>
        public string sqbqname { get; set; }

        /// <summary>
        /// 申请医生代码
        /// </summary>
        public string sqyscode { get; set; }

        /// <summary>
        /// 申请医生名称
        /// </summary>
        public string sqysname { get; set; }

        /// <summary>
        /// 申请单号
        /// </summary>
        public string sldh { get; set; }


        /// <summary>
        /// 申请单总金额
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        /// 申请单备注
        /// </summary>
        public string sqbz { get; set; }

        /// <summary>
        /// 申请单状态  
        /// </summary>
        public char sldzt { get; set; }

        public DateTime CreateTime { get; set; }

        public string CreatorCode { get; set; }
    }
    public class sldmxApplicationVO
    {

        public string xmCode { get; set; }

        public string xmmc { get; set; }

        public decimal dj { get; set; }

        public int sl { get; set; }

        public string dw { get; set; }

        public string gg { get; set; }

        public DateTime CreateTime { get; set; }

        public string CreatorCode { get; set; }

    }
}
