using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto.Encodings;

namespace YiBaoInterface
{
    public class RSAHelper
    {
        private static Encoding Encoding_UTF8 = Encoding.UTF8;

        /// <summary>
        /// KEY 结构体
        /// </summary>
        public struct RSAKEY
        {
            /// <summary>
            /// 公钥
            /// </summary>
            public string PublicKey
            {
                get;
                set;
            }
            /// <summary>
            /// 私钥
            /// </summary>
            public string PrivateKey
            {
                get;
                set;
            }
        }
        public RSAKEY GetKey()
        {
            //RSA密钥对的构造器  
            RsaKeyPairGenerator keyGenerator = new RsaKeyPairGenerator();

            //RSA密钥构造器的参数  
            RsaKeyGenerationParameters param = new RsaKeyGenerationParameters(
                Org.BouncyCastle.Math.BigInteger.ValueOf(3),
                new Org.BouncyCastle.Security.SecureRandom(),
                1024,   //密钥长度  
                25);
            //用参数初始化密钥构造器  
            keyGenerator.Init(param);
            //产生密钥对  
            AsymmetricCipherKeyPair keyPair = keyGenerator.GenerateKeyPair();
            //获取公钥和密钥  
            AsymmetricKeyParameter publicKey = keyPair.Public;
            AsymmetricKeyParameter privateKey = keyPair.Private;

            SubjectPublicKeyInfo subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);
            PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKey);


            Asn1Object asn1ObjectPublic = subjectPublicKeyInfo.ToAsn1Object();

            byte[] publicInfoByte = asn1ObjectPublic.GetEncoded("UTF-8");
            Asn1Object asn1ObjectPrivate = privateKeyInfo.ToAsn1Object();
            byte[] privateInfoByte = asn1ObjectPrivate.GetEncoded("UTF-8");

            RSAKEY item = new RSAKEY()
            {
                PublicKey = Convert.ToBase64String(publicInfoByte),
                PrivateKey = Convert.ToBase64String(privateInfoByte)
            };
            return item;
        }
        private static AsymmetricKeyParameter GetPublicKeyParameter(string keyBase64)
        {
            keyBase64 = keyBase64.Replace("\r", "").Replace("\n", "").Replace(" ", "");
            byte[] publicInfoByte = Convert.FromBase64String(keyBase64);
            Asn1Object pubKeyObj = Asn1Object.FromByteArray(publicInfoByte);//这里也可以从流中读取，从本地导入   
            AsymmetricKeyParameter pubKey = PublicKeyFactory.CreateKey(publicInfoByte);
            return pubKey;
        }

        private AsymmetricKeyParameter GetPrivateKeyParameter(string keyBase64)
        {
            keyBase64 = keyBase64.Replace("\r", "").Replace("\n", "").Replace(" ", "");
            byte[] privateInfoByte = Convert.FromBase64String(keyBase64);
            // Asn1Object priKeyObj = Asn1Object.FromByteArray(privateInfoByte);//这里也可以从流中读取，从本地导入   
            // PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKey);
            AsymmetricKeyParameter priKey = PrivateKeyFactory.CreateKey(privateInfoByte);
            return priKey;
        }

        /// <summary>
        /// 私钥加密
        /// </summary>
        /// <param name="data">加密内容</param>
        /// <param name="privateKey">私钥（Base64后的）</param>
        /// <returns>返回Base64内容</returns>
        public string EncryptByPrivateKey(string data, string privateKey)
        {
            //非对称加密算法，加解密用  
            IAsymmetricBlockCipher engine = new Pkcs1Encoding(new RsaEngine());


            //加密  

            try
            {
                engine.Init(true, GetPrivateKeyParameter(privateKey));
                byte[] byteData = Encoding_UTF8.GetBytes(data);
                var ResultData = engine.ProcessBlock(byteData, 0, byteData.Length);
                return Convert.ToBase64String(ResultData);
                //Console.WriteLine("密文（base64编码）:" + Convert.ToBase64String(testData) + Environment.NewLine);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 私钥解密
        /// </summary>
        /// <param name="data">待解密的内容</param>
        /// <param name="privateKey">私钥（Base64编码后的）</param>
        /// <returns>返回明文</returns>
        public string DecryptByPrivateKey(string data, string privateKey)
        {
            data = data.Replace("\r", "").Replace("\n", "").Replace(" ", "");
            //非对称加密算法，加解密用  
            IAsymmetricBlockCipher engine = new Pkcs1Encoding(new RsaEngine());

            //解密  
            try
            {
                engine.Init(false, GetPrivateKeyParameter(privateKey));
                byte[] byteData = Convert.FromBase64String(data);
                var ResultData = engine.ProcessBlock(byteData, 0, byteData.Length);
                return Encoding_UTF8.GetString(ResultData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 公钥加密
        /// </summary>
        /// <param name="data">加密内容</param>
        /// <param name="publicKey">公钥（Base64编码后的）</param>
        /// <returns>返回Base64内容</returns>
        public static string EncryptByPublicKey(string data, string publicKey)
        {
            //非对称加密算法，加解密用  
            IAsymmetricBlockCipher engine = new Pkcs1Encoding(new RsaEngine());

            //加密  
            try
            {
                engine.Init(true, GetPublicKeyParameter(publicKey));
                byte[] byteData = Encoding_UTF8.GetBytes(data);
                var ResultData = engine.ProcessBlock(byteData, 0, byteData.Length);
                return Convert.ToBase64String(ResultData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 公钥解密
        /// </summary>
        /// <param name="data">待解密的内容</param>
        /// <param name="publicKey">公钥（Base64编码后的）</param>
        /// <returns>返回明文</returns>
        public string DecryptByPublicKey(string data, string publicKey)
        {
            data = data.Replace("\r", "").Replace("\n", "").Replace(" ", "");
            //非对称加密算法，加解密用  
            IAsymmetricBlockCipher engine = new Pkcs1Encoding(new RsaEngine());

            //解密  
            try
            {
                engine.Init(false, GetPublicKeyParameter(publicKey));
                byte[] byteData = Convert.FromBase64String(data);
                var ResultData = engine.ProcessBlock(byteData, 0, byteData.Length);
                return Encoding_UTF8.GetString(ResultData);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
