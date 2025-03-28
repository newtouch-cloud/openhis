using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.ValueObjects
{
    public class bafeeVO
    {
        public string id1 { get; set; }
        public string id2 { get; set; }
        public string id3 { get; set; }
        /// <summary>
        /// 一级大类编号
        /// </summary>
        public string code1 { get; set; }
        /// <summary>
        /// 一级大类名称
        /// </summary>
        public string name1 { get; set; }
        /// <summary>
        /// 二级大类编号
        /// </summary>
        public string code2 { get; set; }
        /// <summary>
        /// 二级大类名称
        /// </summary>
        public string name2 { get; set; }
        /// <summary>
        /// 三级大类编号
        /// </summary>
        public string code3 { get; set; }
        /// <summary>
        /// 三级大类名称
        /// </summary>
        public string name3 { get; set; }
        /// <summary>
        /// 大类名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 大类拼音
        /// </summary>
        public string py { get; set; }

        public string ShortCode { get; set; }
        public int Lev { get; set; }
        public string ParentCode { get; set; }
        public string code { get; set; }
        public int px { get; set; }
    }
}
