namespace NewtouchHIS.Lib.Framework.Attributes
{
    /// <summary>
    /// 自定义Attribute 不验证登录身份（给action加）
    /// </summary>
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Class, AllowMultiple = false)]
    public class HandlerLoginAttribute : Attribute
    {

    }
}
