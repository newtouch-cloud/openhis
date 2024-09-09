namespace NeiMengGuYiBaoApp.Models.Post.YiBao
{
    public class Post_5401 : PostBase
    {
        /// <summary>
        /// 人员编号 (必填, 长度: 30)
        /// </summary>
        public string psn_no { get; set; }

        /// <summary>
        /// 检查机构代码 (长度: 50)
        /// </summary>
        public string exam_org_code { get; set; }

        /// <summary>
        /// 检查机构名称 (长度: 50)
        /// </summary>
        public string exam_org_name { get; set; }

        /// <summary>
        /// 检查项目代码 (长度: 30)
        /// </summary>
        public string exam_item_code { get; set; }

        /// <summary>
        /// 检查项目名称 (长度: 300)
        /// </summary>
        public string exam_item_name { get; set; }
    }
}
