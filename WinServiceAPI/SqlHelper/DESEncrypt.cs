using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SqlHelper
{
    public class DESEncrypt
    {
        #region ========加密======== 

        /// <summary>
        /// 利用缺省密钥加密字符串
        /// </summary>
        /// <value>密钥："wqsj"</value>
        /// <param name="Text">需要加密的明文</param>
        /// <returns>缺省密钥加密的密文</returns>
        public static string Encrypt(string Text)
        {
            return Encrypt(Text, "wqsj");
        }
        /// <summary> 
        /// 利用指定密钥加密字符串 
        /// </summary> 
        /// <param name="Text">需要加密的明文</param> 
        /// <param name="sKey">指定密钥</param> 
        /// <returns>指定密钥加密的密文</returns> 
        public static string Encrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
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

        #endregion

        #region ========解密======== 


        /// <summary>
        /// 利用缺省密钥解密数据
        /// </summary>
        /// <value>密钥："wqsj"</value>
        /// <param name="Text">缺省密钥加密的密文</param>
        /// <returns>缺省密钥解密后的明文</returns>
        public static string Decrypt(string Text)
        {
            return Decrypt(Text, "wqsj");
        }

        /// <summary> 
        /// 利用指定密钥解密数据 
        /// </summary> 
        /// <param name="Text">指定密钥加密的密文</param> 
        /// <param name="sKey">指定密钥</param> 
        /// <returns>指定密钥解密后的明文</returns> 
        public static string Decrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        #endregion
    }
}
