using System.Security.Cryptography;
using System.Text;

namespace NewtouchHIS.Lib.Base.Security
{
    /// <summary>
    /// DES 加密 解密
    /// </summary>
    public class DESEncrypt
    {
        private static string DESKey = "Newtouch_desencrypt_2016";

        /// <summary> 
        /// 加密数据
        /// </summary> 
        /// <param name="text">明文</param> 
        /// <param name="sKey">秘钥，默认使用‘默认秘钥’</param> 
        /// <returns></returns> 
        public static string Encrypt(string text, string sKey)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }
            if (string.IsNullOrWhiteSpace(sKey))
            {
                sKey = DESKey;
            }
            var md5Key = MD5Encrypt.Encrypt(sKey, true);
            var des = DES.Create();

            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(text);
            des.Key = Encoding.ASCII.GetBytes(md5Key.Substring(0, 8));
            des.IV = Encoding.ASCII.GetBytes(md5Key.Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        /// <summary> 
        /// 解密数据
        /// </summary> 
        /// <param name="text">密文</param> 
        /// <param name="sKey">秘钥，默认使用‘默认秘钥’</param> 
        /// <returns></returns> 
        public static string Decrypt(string text, string? sKey = null)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }
            if (string.IsNullOrWhiteSpace(sKey))
            {
                sKey = DESKey;
            }
            int len;
            len = text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            var des = DES.Create();
            des.Key = Encoding.ASCII.GetBytes(MD5Encrypt.Encrypt(sKey).Substring(0, 8));
            des.IV = Encoding.ASCII.GetBytes(MD5Encrypt.Encrypt(sKey).Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        public void Des()
        {
            using var des = DES.Create();
            des.GenerateKey();
            des.GenerateIV();

            // 加密数据
            byte[] plainText = System.Text.Encoding.UTF8.GetBytes("Hello, DES!");
            byte[] encryptedData = EncryptData(plainText, des.Key, des.IV);

            // 解密数据
            byte[] decryptedData = DecryptData(encryptedData, des.Key, des.IV);
            string decryptedText = System.Text.Encoding.UTF8.GetString(decryptedData);
        }

        static byte[] EncryptData(byte[] data, byte[] key, byte[] iv)
        {
            var des = DES.Create();
            using var encryptor = des.CreateEncryptor(key, iv);
            return PerformCryptography(data, encryptor);
        }

        static byte[] DecryptData(byte[] data, byte[] key, byte[] iv)
        {
            var des = DES.Create();
            using var decryptor = des.CreateDecryptor(key, iv);
            return PerformCryptography(data, decryptor);
        }

        static byte[] PerformCryptography(byte[] data, ICryptoTransform transform)
        {
            using var stream = new System.IO.MemoryStream();
            using var cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Write);
            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.FlushFinalBlock();
            return stream.ToArray();
        }

    }
}
