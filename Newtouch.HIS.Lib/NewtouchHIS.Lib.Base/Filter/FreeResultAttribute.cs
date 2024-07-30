namespace NewtouchHIS.Lib.Base.Filter
{
    /// <summary>
    /// 禁用封装结果
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class FreeResultAttribute : Attribute
    {
    }
}
