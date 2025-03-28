namespace NewtouchHIS.Lib.Framework.Attributes
{
    /// <summary>
    /// 自定义Attribute 访问跟踪日志 但不记录Form的内容（给action加）
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TrackerRequestFormIgnoreAttribute : Attribute
    {

    }
}
