namespace NewtouchHIS.Lib.Base.Exceptions
{
    public class EncryptException
    {
    }
    /// <summary>
    /// 异常类-Rj加密异常（帮助区分异常类型）
    /// </summary>
    public class RijndaelEncryptException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RijndaelEncryptException()
        {

        }
    }
    /// <summary>
    /// 异常类-Rj解密异常（帮助区分异常类型）
    /// </summary>
    public class RijndaelDecrptyException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RijndaelDecrptyException()
        {

        }
    }
}
