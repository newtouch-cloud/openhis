namespace NeiMengGuYiBaoApp.Models.Post.YiBao
{
    public class Post_4201 : PostBase
    {
        /// <summary>
        /// 就诊号
        /// </summary>
        public string mdtrt_id { get; set; }

        /// <summary>
        /// 结算内码(结算ID)
        /// </summary>
        public int jsnm { get; set; }
        /// <summary>
        /// 0：门诊 1：住院
        /// </summary>
        public string type { get; set; }

    }
}
