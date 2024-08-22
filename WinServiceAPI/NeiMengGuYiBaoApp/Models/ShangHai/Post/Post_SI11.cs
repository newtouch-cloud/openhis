using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Post
{
    public class Post_SI11:Post_Base
    {
        /// <summary>
        /// 科室编码
        /// </summary>
        public string deptid { get; set; }

        public string orgId { get; set; }
        /// <summary>
        /// 特殊人员标识
        /// </summary>
        public string personspectag { get; set; }

        /// <summary>
        /// 医疗类别 
        /// </summary>
        public string yllb { get; set; }

        /// <summary>
        /// 病人类型
        /// </summary>
        public string persontype { get; set; }
        /// <summary>
        /// 工伤认定号
        /// </summary>
        public string gsrdh { get; set; }

        /// <summary>
        /// 大病项目代码
        /// </summary>
        public string dbtype { get; set; }

        /// <summary>
        /// 家床结算开始日期
        /// </summary>
        public string jsksrq { get; set; }

        /// <summary>
        /// 家床结算结束日期
        /// </summary>
        public string jsjsrq { get; set; }

        /// <summary>
        /// 家床就诊次数
        /// </summary>
        public int jzcs { get; set; }

        /// <summary>
        /// 就诊单元号
        /// </summary>
        public string jzdyh { get; set; }

        /// <summary>
        /// 线上业务类型
        /// </summary>
        public string xsywlx { get; set; }
        public string chrg_bchno { get; set; }

    }
}
