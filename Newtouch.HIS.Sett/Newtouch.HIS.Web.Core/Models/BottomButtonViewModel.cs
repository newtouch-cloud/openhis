using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Newtouch.HIS.Web.Core.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class BottomButtonViewModel
    {
        public BottomButtonViewModel()
        {
            F4Text = "清除";
            F5Text = "刷新";
            F8Text = "保存";
            F10Text = "退出";
        }

        /// <summary>
        /// 显示的快捷键列表
        /// </summary>
        public int[] ShowKeyList { get; set; }

        /// <summary>
        /// 显示的文本
        /// </summary>
        public string F2Text { get; set; }
        /// <summary>
        /// 显示的文本
        /// </summary>
        public string F3Text { get; set; }
        /// <summary>
        /// 显示的文本
        /// </summary>
        public string F4Text { get; set; }
        /// <summary>
        /// 显示的文本
        /// </summary>
        public string F5Text { get; set; }
        /// <summary>
        /// 显示的文本
        /// </summary>
        public string F6Text { get; set; }
        /// <summary>
        /// 显示的文本
        /// </summary>
        public string F7Text { get; set; }
        /// <summary>
        /// 显示的文本
        /// </summary>
        public string F8Text { get; set; }
        /// <summary>
        /// 显示的文本
        /// </summary>
        public string F9Text { get; set; }
        /// <summary>
        /// 显示的文本
        /// </summary>
        public string F10Text { get; set; }
    }


}
