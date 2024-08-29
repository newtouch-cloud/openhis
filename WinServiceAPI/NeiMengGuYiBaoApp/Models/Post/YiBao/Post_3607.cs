using System.ComponentModel;

namespace NeiMengGuYiBaoApp.Models.Post.YiBao
{
    public class Post_3607 : PostBase
    {
        /// <summary>
        /// 结算年月  以“ yyyymm ”格式
        /// </summary>
        [Description("结算年月")]
        public string setl_ym { get; set; }

        /// <summary>
        /// 当前页数
        /// </summary>
        [Description("当前页数")]
        public string page_num { get; set; }

        /// <summary>
        /// 本页数据量
        /// </summary>
        [Description("本页数据量")]
        public string page_size { get; set; }

        /// <summary>
        /// 结算 ID  
        /// </summary>
        [Description("结算 ID")]
        public string setl_id { get; set; }





    }
}
