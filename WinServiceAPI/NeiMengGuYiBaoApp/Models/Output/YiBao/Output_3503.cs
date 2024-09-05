namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    /// <summary>
    /// 3503 和3503A 都用同一个
    /// </summary>
    public class Output_3503 : OutputBase
    {
        public Output3503 result { get; set; }
    }

    public class Output3503
    {
        /// <summary>
        /// 返回结果 字符型 6 成功返回值为1 , 失败返回值为 0。
        /// </summary>
        public string retRslt { get; set; }
        /// <summary>
        /// 返回信息 字符型 6 返回失败原因
        /// </summary>
        public string msgRslt { get; set; }
    }


}
