namespace FrameworkBase.MultiOrg.Domain.DTO
{
    /// <summary>
    /// 收费项目药品筛选 条件 DTO
    /// </summary>
    public class SelectSfxmYpFilterDTO
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SelectSfxmYpFilterDTO()
        {
            topCount = 50;
            isContansChildDl = true;
            containyp0ck = true;
        }

        /// <summary>
        /// 前多少条 默认50
        /// </summary>
        public int topCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string orgId { get; set; }
        /// <summary>
        /// 1门诊 或 2住院	必须指定是查门诊 还是住院，不指定什么都查不到
        /// </summary>
        public string mzzybz { get; set; }
        /// <summary>
        /// 1药品 2项目 3非治疗项目	多个 逗号分割
        /// </summary>
        public string dllb { get; set; }
        /// <summary>
        /// 关联字典ChargeCateType，如WM 西药
        /// </summary>
        public string sfdllx { get; set; }
        /// <summary>
        /// 关键字 模糊匹配
        /// </summary>
        public string keyword { get; set; }
        /// <summary>
        /// 具体的收费大类Code	多个 逗号分割
        /// </summary>
        public string dlCode { get; set; }
        /// <summary>
        /// 配合@dlCode使用
        /// </summary>
        public bool isContansChildDl { get; set; }
        /// <summary>
        /// 是否使用药品库存逻辑
        /// ‘动态参数配置’来的
        /// </summary>
        public bool useypckflag { get; set; }
        /// <summary>
        /// 药品的药房筛选	多个 逗号分割
        /// </summary>
        public string ypyfbmCode { get; set; }
        /// <summary>
        /// 是否包含药品0库存，0不包含 1包含
        /// </summary>
        public bool containyp0ck { get; set; }

        /// <summary>
        /// 是否仅医保项目/药品
        /// </summary>
        public bool onlyybflag { get; set; }

        /// <summary>
        /// 是否启用抗生素控制
        /// </summary>
        public bool isQyKssKZ { get; set; }

        /// <summary>
        /// 权限级别
        /// </summary>
        public string qxjb { get; set; }
    }
}
