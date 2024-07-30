using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace SqlHelper
{
    public class Hex
    {
        /// <summary>
        /// 字节数组转换为Hex字符串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="toLowerCase"></param>
        /// <returns></returns>
        public static string ByteArrayToHexString(byte[] data, bool toLowerCase = true)
        {
            var hex = BitConverter.ToString(data).Replace("-", string.Empty);
            return toLowerCase ? hex.ToLower() : hex.ToUpper();
        }

    }

    public class DigestUtils
    {
        /// <summary>
        /// SHA256 转换为 Hex字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Sha256Hex(string data)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(bytes);
                return Hex.ByteArrayToHexString(hash);
            }
        }


        
    }

    public class Utilities {

        /// <summary>
        /// sha256算法
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string sha256(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] hash = SHA256Managed.Create().ComputeHash(bytes);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("X2"));
            }

            return builder.ToString();
        }
    }
    class RSACryptoItem
    {
        public RSACryptoServiceProvider Provider;
        public List<byte> PubKeyBytes;
    }

    public class RSAManager
    {
        private RSACryptoItem item;

        public RSAManager()
        {
            item = GenRSACryptoItem();
        }

        private RSACryptoItem GenRSACryptoItem()
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            List<byte> pubKeyBytes = new List<byte>(provider.ExportCspBlob(false));
            return new RSACryptoItem
            {
                Provider = provider,
                PubKeyBytes = pubKeyBytes,
            };
        }

        /// <summary>
        /// 使用公钥加密
        /// </summary>
        /// <param name="inBytes"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static byte[] EncryptDataByPublicKey(string content, string publicKey)
        {
            byte[] inBytes = System.Text.Encoding.Default.GetBytes(content);
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString(publicKey);
            return provider.Encrypt(inBytes, false);
        }

        /// <summary>
        /// 使用私钥解密
        /// </summary>
        /// <param name="inBytes"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static byte[] DecryptDataByPrivateKey(string scontent, string privateKey)
        {
            byte[] inBytes = System.Text.Encoding.Default.GetBytes(scontent);
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString(privateKey);
            return provider.Decrypt(inBytes, false);
        }


        /// <summary>
        /// 生成数字签名
        /// </summary>
        /// <param name="originalText">原文</param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string GenSign(string originalText, string privateKey)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(originalText);
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString(privateKey);
            //使用SHA1进行摘要算法，生成签名
            byteData = provider.SignData(byteData, new SHA1CryptoServiceProvider());
            return Convert.ToBase64String(byteData);
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="originalText">原文</param>
        /// <param name="SignedData">签名</param>
        /// <param name="publicKey">公钥</param>
        /// <returns></returns>
        public static bool VerifySigned(string originalText, string signedData, string publicKey)
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString(publicKey);
            byte[] byteData = Encoding.UTF8.GetBytes(originalText);
            byte[] signData = Convert.FromBase64String(signedData);
            return provider.VerifyData(byteData, new SHA1CryptoServiceProvider(), signData);
        }

        public byte[] EncryptData(byte[] inBytes)
        {
            if (item != null)
                return item.Provider.Encrypt(inBytes, false);
            return null;
        }

        public byte[] DecryptData(byte[] inBytes)
        {
            if (item != null)
                return item.Provider.Decrypt(inBytes, false);
            return null;
        }

        //public static string RSAPublicKeyEncrypt(string publicKey, string content)
        //{
        //    RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKey));
        //    string XML = string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>",
        //        Convert.ToBase64String(publicKeyParam.Modulus.ToByteArrayUnsigned()),
        //        Convert.ToBase64String(publicKeyParam.Exponent.ToByteArrayUnsigned()));

        //    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        //    byte[] cipherbytes;
        //    rsa.FromXmlString(XML);
        //    cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);
        //    return Convert.ToBase64String(cipherbytes);
        //}

        ///// <summary>
        ///// Pem密钥转RSA密钥
        ///// </summary>
        ///// <param name="pemKey">Pem密钥</param>
        ///// <param name="isPrivateKey">是否是私钥</param>
        ///// <returns>RSA密钥</returns>
        //public static string PemToRSAKey(string pemKey, bool isPrivateKey)
        //{
        //    string rsaKey = string.Empty;
        //    object pemObject = null;
        //    RSAParameters rsaPara = new RSAParameters();
        //    using (StringReader sReader = new StringReader(pemKey))
        //    {
        //        var pemReader = new Org.BouncyCastle.OpenSsl.PemReader(sReader);
        //        pemObject = pemReader.ReadObject();
        //    }
        //    //RSA私钥
        //    if (isPrivateKey)
        //    {
        //        RsaPrivateCrtKeyParameters key = (RsaPrivateCrtKeyParameters)((AsymmetricCipherKeyPair)pemObject).Private;
        //        rsaPara = new RSAParameters
        //        {
        //            Modulus = key.Modulus.ToByteArrayUnsigned(),
        //            Exponent = key.PublicExponent.ToByteArrayUnsigned(),
        //            D = key.Exponent.ToByteArrayUnsigned(),
        //            P = key.P.ToByteArrayUnsigned(),
        //            Q = key.Q.ToByteArrayUnsigned(),
        //            DP = key.DP.ToByteArrayUnsigned(),
        //            DQ = key.DQ.ToByteArrayUnsigned(),
        //            InverseQ = key.QInv.ToByteArrayUnsigned(),
        //        };
        //    }
        //    //RSA公钥
        //    else
        //    {
        //        RsaKeyParameters key = (RsaKeyParameters)pemObject;
        //        rsaPara = new RSAParameters
        //        {
        //            Modulus = key.Modulus.ToByteArrayUnsigned(),
        //            Exponent = key.Exponent.ToByteArrayUnsigned(),
        //        };
        //    }
        //    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        //    rsa.ImportParameters(rsaPara);
        //    using (StringWriter sw = new StringWriter())
        //    {
        //        sw.Write(rsa.ToXmlString(isPrivateKey ? true : false));
        //        rsaKey = sw.ToString();
        //    }
        //    return rsaKey;
        //}

        public static string RSAEncrypt(string publickey, string content)
        {
            publickey = @"<RSAKeyValue><Modulus>" + publickey + "</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(publickey);
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);

            return Convert.ToBase64String(cipherbytes);
        }

        ///// <summary>
        ///// RSA解密
        ///// </summary>
        ///// <param name="privatekey"></param>
        ///// <param name="content"></param>
        ///// <returns></returns>
        //public static string RSADecrypt(string privatekey, string content)
        //{
        //    privatekey = @"<RSAKeyValue><Modulus>5m9m14XH3oqLJ8bNGw9e4rGpXpcktv9MSkHSVFVMjHbfv+SJ5v0ubqQxa5YjLN4vc49z7SVju8s0X4gZ6AzZTn06jzWOgyPRV54Q4I0DCYadWW4Ze3e+BOtwgVU1Og3qHKn8vygoj40J6U85Z/PTJu3hN1m75Zr195ju7g9v4Hk=</Modulus><Exponent>AQAB</Exponent><P>/hf2dnK7rNfl3lbqghWcpFdu778hUpIEBixCDL5WiBtpkZdpSw90aERmHJYaW2RGvGRi6zSftLh00KHsPcNUMw==</P><Q>6Cn/jOLrPapDTEp1Fkq+uz++1Do0eeX7HYqi9rY29CqShzCeI7LEYOoSwYuAJ3xA/DuCdQENPSoJ9KFbO4Wsow==</Q><DP>ga1rHIJro8e/yhxjrKYo/nqc5ICQGhrpMNlPkD9n3CjZVPOISkWF7FzUHEzDANeJfkZhcZa21z24aG3rKo5Qnw==</DP><DQ>MNGsCB8rYlMsRZ2ek2pyQwO7h/sZT8y5ilO9wu08Dwnot/7UMiOEQfDWstY3w5XQQHnvC9WFyCfP4h4QBissyw==</DQ><InverseQ>EG02S7SADhH1EVT9DD0Z62Y0uY7gIYvxX/uq+IzKSCwB8M2G7Qv9xgZQaQlLpCaeKbux3Y59hHM+KpamGL19Kg==</InverseQ><D>vmaYHEbPAgOJvaEXQl+t8DQKFT1fudEysTy31LTyXjGu6XiltXXHUuZaa2IPyHgBz0Nd7znwsW/S44iql0Fen1kzKioEL3svANui63O3o5xdDeExVM6zOf1wUUh/oldovPweChyoAdMtUzgvCbJk1sYDJf++Nr0FeNW1RB1XG30=</D></RSAKeyValue>";
        //    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        //    byte[] cipherbytes;
        //    rsa.FromXmlString(privatekey);
        //    cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), false);

        //    return Encoding.UTF8.GetString(cipherbytes);
        //}
    }


}
