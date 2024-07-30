using System.Security.Cryptography;
using System.Text;

namespace NewtouchHIS.Lib.Base.Security
{
    public class AESEncrypt
    {

        public static string AESEncrypt256(string toDecrypt, string hexKey, string IV)
        {
            byte[] cipherText = Convert.FromBase64String(toDecrypt);
            SymmetricAlgorithm des = Rijndael.Create();

            des.Key = Encoding.UTF8.GetBytes(hexKey);
            des.IV = UTF8Encoding.UTF8.GetBytes(IV);
            byte[] decryptBytes = new byte[cipherText.Length];
            int keySize = 256;
            if (des.Key.Length < keySize)
            {
                //padding 0
                byte[] padded = new byte[keySize];
                Buffer.BlockCopy(des.Key, 0, padded, 0, des.Key.Length);
                des.Key = padded;
            }
            else if (des.Key.Length > keySize)
            {
                byte[] truncated = new byte[keySize];
                Buffer.BlockCopy(des.Key, 0, truncated, 0, keySize);
                des.Key = truncated;
            }

            return Encoding.UTF8.GetString(decryptBytes).Replace("\0", ""); // 将字串后尾的'\0'去掉 }
        }


        public static string AESDecrypt(string toDecrypt, string hexKey, string IV)
        {
            byte[] cipherText = Convert.FromBase64String(toDecrypt);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(hexKey);
                aes.IV = Encoding.UTF8.GetBytes(IV);
                ICryptoTransform decrypt = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Read))
                    {
                        using (StreamReader srdecrypt = new StreamReader(cs))
                        {
                            return srdecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }


    }
}