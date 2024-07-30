using System.Security.Cryptography;
using System.Text;

namespace NewtouchHIS.Lib.Base.Security
{
    /// <summary>
    /// MD5加密
    /// </summary>
    public class MD5Encrypt
    {
        public static string Encrypt(string text, bool uppercase = false)
        {
            using MD5 mD = MD5.Create();
            byte[] array = mD.ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                stringBuilder.Append(array[i].ToString("x2"));
            }

            string text2 = stringBuilder.ToString();
            return (!uppercase) ? text2 : text2.ToUpper();
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <param name="code">加密位数16/32</param>
        /// <returns></returns>
        public static string Md5(string str, int code, bool uppercase = true)
        {
            string strEncrypt = string.Empty;
            if (code == 16)
            {
                strEncrypt = Encrypt(str, uppercase).Substring(8, 16);
            }

            if (code == 32)
            {
                strEncrypt = Encrypt(str, uppercase);
            }

            return strEncrypt;
        }
    }
}