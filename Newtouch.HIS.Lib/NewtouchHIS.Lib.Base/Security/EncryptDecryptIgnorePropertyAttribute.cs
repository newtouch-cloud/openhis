namespace Newtouch.Core.Common.Security
{
    /// <summary>
    /// 属性不参与加密、解密
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EncryptDecryptIgnorePropertyAttribute : Attribute
    {

    }
}
