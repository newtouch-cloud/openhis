namespace NewtouchHIS.Lib.Framework.Attributes
{
    /// <summary>
    /// 自定义Attribute 不记录访问跟踪日志（给action加）
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TrackerIgnoreAttribute : Attribute
    {

    }
}
