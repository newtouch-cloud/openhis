namespace NewtouchHIS.Lib.Framework.Attributes
{
    /// <summary>
    /// 自定义Attribute 不验证是Ajax请求（给action加）
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class HandlerAjaxOnlyIgnoreAttribute : Attribute
    {

    }
}
