namespace Newtouch.HIS.Proxy.guian.DTO.S30
{
    /// <summary>
    /// 根据时间戳下载疾病字典信息 返回报文
    /// </summary>
    public class item
    {
        /// <summary>
        /// 疾病中文名
        /// </summary>
        public string name { get; set; }
        
        /// <summary>
        /// 疾病编码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 项目类别
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string specification { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
        public string dosageForm { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string unit { get; set; }
        /// <summary>
        /// 使用级别
        /// </summary>
        public string rank { get; set; }
        /// <summary>
        /// 补偿比例
        /// </summary>
        public string scale { get; set; }
        /// <summary>
        /// 医保范围1保内 0保外
        /// </summary>
        public string scope { get; set; }
        /// <summary>
        /// 是否基药
        /// </summary>
        public string isBase { get; set; }
        /// <summary>
        /// 拼音码
        /// </summary>
        public string pcode { get; set; }
        /// <summary>
        /// 价格（省）
        /// </summary>
        public string price5 { get; set; }
        /// <summary>
        /// 价格（市）
        /// </summary>
        public string price4 { get; set; }
        /// <summary>
        /// 价格（县）
        /// </summary>
        public string price3 { get; set; }
        /// <summary>
        /// 价格（乡）
        /// </summary>
        public string price2 { get; set; }
        /// <summary>
        /// 价格（村）
        /// </summary>
        public string price1 { get; set; }
        /// <summary>
        /// 限价（省）
        /// </summary>
        public string limitPrice5 { get; set; }
        /// <summary>
        /// 限价（市）
        /// </summary>
        public string limitPrice4 { get; set; }
        /// <summary>
        /// 限价（县）
        /// </summary>
        public string limitPrice3 { get; set; }
        /// <summary>
        /// 限价（乡）
        /// </summary>
        public string limitPrice2 { get; set; }
        /// <summary>
        /// 限价（村）
        /// </summary>
        public string limitPrice1 { get; set; }
        /// <summary>
        /// 目录级别
        /// </summary>
        public string grade { get; set; }
        /// <summary>
        /// 状态0未审核1已审核2未通过
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string datetime { get; set; }
    }
}