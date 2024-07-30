namespace NewtouchHIS.Base.Domain.Model
{
    /// <summary>
    /// 下拉选择Model
    /// （select 控件）
    /// </summary>
    public class TreeSelectModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public string parentId { get; set; }
    }
    /// <summary>
    /// treeview Model 无Nodes基类
    /// </summary>
    public class BsTreeSelectBaseModel
    {
        public string text { get; set; }
        public string? href { get; set; }
        public string? color { get; set; }
        public string? backColor { get; set; }
        public string? icon { get; set; }
        public string[]? tags { get; set; }
        public bool? selected { get; set; } = false;
    }
    /// <summary>
    /// treeview Model 原生
    /// </summary>
    public class BsTreeSelectModel: BsTreeSelectBaseModel
    {        
        public List<BsTreeSelectModel>? nodes { get; set; }
    }
    /// <summary>
    /// treeview Model 扩展字段
    /// 用于特殊数据处理 业务关联登
    /// </summary>
    public class BsTreeSelectExtDataModel : BsTreeSelectBaseModel
    {
        public List<BsTreeSelectExtDataModel>? nodes { get; set; }
        public string Id { get; set; }
        public string? ParentId { get; set; }
        public int? LevelId { get; set; }
        public List<BsTreeSelectExtModel>? ext { get; set; }
        public BsTreeNodeStuModel? state { get; set; }
    }
    public class BsTreeSelectExtModel
    {
        public string? Key { get; set; }
        public string? Value { get; set; }
        public string? Desc { get; set; }
    }

    public class BsTreeNodeStuModel
    {
        public bool? _checked { get; set; } = false;
        public bool? selected { get; set; } = false;
        public bool? disabled { get; set; } = false;
        public bool? expanded { get; set; } = false;
    }
    public class TreeSelectCModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public List<TreeSelectCModel>? children { get; set; }
    }
}
