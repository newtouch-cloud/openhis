using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.GM;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.sm2sm4
{
    public class EasyGmUtils
    {
        private static X9ECParameters x9ECParameters = GMNamedCurves.GetByName("sm2p256v1");
        private static ECDomainParameters ecDomainParameters = new ECDomainParameters(x9ECParameters.Curve, x9ECParameters.G, x9ECParameters.N);
        /**
        *
        * @param msg
        * @param userId
        * @param privateKey
        * @return r||s，直接拼接byte数组的rs
        */
        public static byte[] signSm3WithSm2(byte[] msg, byte[] userId, byte[] privateKeyBytes)
        {
            ECPrivateKeyParameters privateKeyParameters = getPrivatekeyFromD(new BigInteger(1, privateKeyBytes));
            return rsAsn1ToPlainByteArray(signSm3WithSm2Asn1Rs(msg, userId, privateKeyParameters));
        }
        /**
         * @param msg
         * @param userId
         * @param privateKey
         * @return rs in <b>asn1 format</b>
         */
        public static byte[] signSm3WithSm2Asn1Rs(byte[] msg, byte[] userId, AsymmetricKeyParameter privateKey)
        {
            try
            {
                ISigner signer = SignerUtilities.InitSigner("SM3withSM2", true, privateKey, new SecureRandom());
                signer.BlockUpdate(msg, 0, msg.Length);
                byte[] sig = signer.GenerateSignature();
                return sig;
            }
            catch (Exception e)
            {
                //log.Error("SignSm3WithSm2Asn1Rs error: " + e.Message, e);
                return null;
            }
        }


        /**
      *
      * @param msg
      * @param userId
      * @param rs r||s，直接拼接byte数组的rs
      * @param publicKey
      * @return
      */
        public static bool verifySm3WithSm2(byte[] msg, byte[] userId, byte[] rs, byte[] publicKeyBytes)
        {
            if (rs == null || msg == null || userId == null) return false;
            if (rs.Length != RS_LEN * 2) return false;

            if (publicKeyBytes.Length != 64 && publicKeyBytes.Length != 65) throw new ArgumentException("err key length");
            BigInteger x, y;
            if (publicKeyBytes.Length > 64)
            {
                x = fromUnsignedByteArray(publicKeyBytes, 1, 32);
                y = fromUnsignedByteArray(publicKeyBytes, 33, 32);
            }
            else
            {
                x = fromUnsignedByteArray(publicKeyBytes, 0, 32);
                y = fromUnsignedByteArray(publicKeyBytes, 32, 32);
            }
            ECPublicKeyParameters publicKey = getPublickeyFromXY(x, y);
            return verifySm3WithSm2Asn1Rs(msg, userId, rsPlainByteArrayToAsn1(rs), publicKey);
        }

        public static BigInteger fromUnsignedByteArray(byte[] var0, int var1, int var2)
        {
            byte[] var3 = var0;
            if (var1 != 0 || var2 != var0.Length)
            {
                var3 = new byte[var2];
                Array.Copy(var0, var1, var3, 0, var2);
            }

            return new BigInteger(1, var3);
        }

        /**
         *
         * @param msg
         * @param userId
         * @param rs in <b>asn1 format</b>
         * @param publicKey
         * @return
         */

        public static bool verifySm3WithSm2Asn1Rs(byte[] msg, byte[] userId, byte[] sign, AsymmetricKeyParameter publicKey)
        {
            try
            {
                ISigner signer = SignerUtilities.GetSigner("SM3withSM2");
                signer.Init(false, publicKey);
                signer.BlockUpdate(msg, 0, msg.Length);
                return signer.VerifySignature(sign);
            }
            catch (Exception e)
            {
                //log.Error("VerifySm3WithSm2Asn1Rs error: " + e.Message, e);
                return false;
            }
        }


        /**
        * bc加解密使用旧标c1||c2||c3，此方法在加密后调用，将结果转化为c1||c3||c2
        * @param c1c2c3
        * @return
        */
        private static byte[] changeC1C2C3ToC1C3C2(byte[] c1c2c3)
        {
            int c1Len = (x9ECParameters.Curve.FieldSize + 7) / 8 * 2 + 1; //sm2p256v1的这个固定65。可看GMNamedCurves、ECCurve代码。
            const int c3Len = 32; //new SM3Digest().getDigestSize();
            byte[] result = new byte[c1c2c3.Length];
            Buffer.BlockCopy(c1c2c3, 0, result, 0, c1Len); //c1
            Buffer.BlockCopy(c1c2c3, c1c2c3.Length - c3Len, result, c1Len, c3Len); //c3
            Buffer.BlockCopy(c1c2c3, c1Len, result, c1Len + c3Len, c1c2c3.Length - c1Len - c3Len); //c2
            return result;
        }


        /**
         * bc加解密使用旧标c1||c3||c2，此方法在解密前调用，将密文转化为c1||c2||c3再去解密
         * @param c1c3c2
         * @return
         */
        private static byte[] changeC1C3C2ToC1C2C3(byte[] c1c3c2)
        {
            int c1Len = (x9ECParameters.Curve.FieldSize + 7) / 8 * 2 + 1; //sm2p256v1的这个固定65。可看GMNamedCurves、ECCurve代码。
            const int c3Len = 32; //new SM3Digest().GetDigestSize();
            byte[] result = new byte[c1c3c2.Length];
            Buffer.BlockCopy(c1c3c2, 0, result, 0, c1Len); //c1: 0->65
            Buffer.BlockCopy(c1c3c2, c1Len + c3Len, result, c1Len, c1c3c2.Length - c1Len - c3Len); //c2
            Buffer.BlockCopy(c1c3c2, c1Len, result, c1c3c2.Length - c3Len, c3Len); //c3
            return result;
        }

        /**
         * c1||c3||c2
         * @param data
         * @param key
         * @return
         */
        public static byte[] sm2Decrypt(byte[] data, AsymmetricKeyParameter key)
        {
            return sm2DecryptOld(changeC1C3C2ToC1C2C3(data), key);
        }

        /**
         * c1||c3||c2
         * @param data
         * @param key
         * @return
         */

        public static byte[] sm2Encrypt(byte[] data, AsymmetricKeyParameter key)
        {
            return changeC1C2C3ToC1C3C2(sm2EncryptOld(data, key));
        }

        /**
         * c1||c2||c3
         * @param data
         * @param key
         * @return
         */
        public static byte[] sm2EncryptOld(byte[] data, AsymmetricKeyParameter pubkey)
        {
            try
            {
                SM2Engine sm2Engine = new SM2Engine();
                sm2Engine.Init(true, new ParametersWithRandom(pubkey, new SecureRandom()));
                return sm2Engine.ProcessBlock(data, 0, data.Length);
            }
            catch (Exception e)
            {
                //log.Error("Sm2EncryptOld error: " + e.Message, e);
                return null;
            }
        }

        /**
         * c1||c2||c3
         * @param data
         * @param key
         * @return
         */
        public static byte[] sm2DecryptOld(byte[] data, AsymmetricKeyParameter key)
        {
            try
            {
                SM2Engine sm2Engine = new SM2Engine();
                sm2Engine.Init(false, key);
                return sm2Engine.ProcessBlock(data, 0, data.Length);
            }
            catch (Exception e)
            {
                //log.Error("Sm2DecryptOld error: " + e.Message, e);
                return null;
            }
        }

        /**
        * @param bytes
        * @return
        */
        public static byte[] sm3(byte[] bytes)
        {
            try
            {
                SM3Digest digest = new SM3Digest();
                digest.BlockUpdate(bytes, 0, bytes.Length);
                byte[] result = DigestUtilities.DoFinal(digest);
                return result;
            }
            catch (Exception e)
            {
                //log.Error("Sm3 error: " + e.Message, e);
                return null;
            }
        }

        private const int RS_LEN = 32;

        private static byte[] bigIntToFixexLengthBytes(BigInteger rOrS)
        {
            // for sm2p256v1, n is 00fffffffeffffffffffffffffffffffff7203df6b21c6052b53bbf40939d54123,
            // r and s are the result of mod n, so they should be less than n and have length<=32
            byte[] rs = rOrS.ToByteArray();
            if (rs.Length == RS_LEN) return rs;
            else if (rs.Length == RS_LEN + 1 && rs[0] == 0) return Arrays.CopyOfRange(rs, 1, RS_LEN + 1);
            else if (rs.Length < RS_LEN)
            {
                byte[] result = new byte[RS_LEN];
                Arrays.Fill(result, (byte)0);
                Buffer.BlockCopy(rs, 0, result, RS_LEN - rs.Length, rs.Length);
                return result;
            }
            else
            {
                throw new ArgumentException("err rs: " + Hex.ToHexString(rs));
            }
        }

        /**
         * BC的SM3withSM2签名得到的结果的rs是asn1格式的，这个方法转化成直接拼接r||s
         * @param rsDer rs in asn1 format
         * @return sign result in plain byte array
         */
        private static byte[] rsAsn1ToPlainByteArray(byte[] rsDer)
        {
            Asn1Sequence seq = Asn1Sequence.GetInstance(rsDer);
            byte[] r = bigIntToFixexLengthBytes(DerInteger.GetInstance(seq[0]).Value);
            byte[] s = bigIntToFixexLengthBytes(DerInteger.GetInstance(seq[1]).Value);
            byte[] result = new byte[RS_LEN * 2];
            Buffer.BlockCopy(r, 0, result, 0, r.Length);
            Buffer.BlockCopy(s, 0, result, RS_LEN, s.Length);
            return result;
        }

        /**
         * BC的SM3withSM2验签需要的rs是asn1格式的，这个方法将直接拼接r||s的字节数组转化成asn1格式
         * @param sign in plain byte array
         * @return rs result in asn1 format
         */
        private static byte[] rsPlainByteArrayToAsn1(byte[] sign)
        {
            if (sign.Length != RS_LEN * 2) throw new ArgumentException("err rs. ");
            BigInteger r = new BigInteger(1, Arrays.CopyOfRange(sign, 0, RS_LEN));
            BigInteger s = new BigInteger(1, Arrays.CopyOfRange(sign, RS_LEN, RS_LEN * 2));
            Asn1EncodableVector v = new Asn1EncodableVector();
            v.Add(new DerInteger(r));
            v.Add(new DerInteger(s));
            try
            {
                return new DerSequence(v).GetEncoded("DER");
            }
            catch (IOException e)
            {
                //log.Error("RsPlainByteArrayToAsn1 error: " + e.Message, e);
                return null;
            }
        }

        public static byte[] sm4DecryptCBC(byte[] keyBytes, byte[] cipher, byte[] iv, String algo)
        {
            if (keyBytes.Length != 16) throw new ArgumentException("err key length");
            if (cipher.Length % 16 != 0) throw new ArgumentException("err data length");

            try
            {
                KeyParameter key = ParameterUtilities.CreateKeyParameter("SM4", keyBytes);
                IBufferedCipher c = CipherUtilities.GetCipher(algo);
                if (iv == null) iv = zeroIv(algo);
                c.Init(false, new ParametersWithIV(key, iv));
                return c.DoFinal(cipher);
            }
            catch (Exception e)
            {
                //log.Error("Sm4DecryptCBC error: " + e.Message, e);
                return null;
            }
        }


        public static byte[] sm4EncryptCBC(byte[] keyBytes, byte[] plain, byte[] iv, String algo)
        {
            if (keyBytes.Length != 16) throw new ArgumentException("err key length");
            if (plain.Length % 16 != 0) throw new ArgumentException("err data length");

            try
            {
                KeyParameter key = ParameterUtilities.CreateKeyParameter("SM4", keyBytes);
                IBufferedCipher c = CipherUtilities.GetCipher(algo);
                if (iv == null) iv = zeroIv(algo);
                c.Init(true, new ParametersWithIV(key, iv));
                return c.DoFinal(plain);
            }
            catch (Exception e)
            {
                //log.Error("Sm4EncryptCBC error: " + e.Message, e);
                return null;
            }
        }


        public static byte[] sm4EncryptECB(byte[] keyBytes, byte[] plain, string algo)
        {
            if (keyBytes.Length != 16) throw new ArgumentException("err key length");
            if (plain.Length % 16 != 0) throw new ArgumentException("err data length");

            try
            {
                KeyParameter key = ParameterUtilities.CreateKeyParameter("SM4", keyBytes);
                IBufferedCipher c = CipherUtilities.GetCipher(algo);
                c.Init(true, key);
                return c.DoFinal(plain);
            }
            catch (Exception e)
            {
                //log.Error("Sm4EncryptECB error: " + e.Message, e);
                return null;
            }
        }

        public static byte[] sm4DecryptECB(byte[] keyBytes, byte[] cipher, string algo)
        {
            if (keyBytes.Length != 16) throw new ArgumentException("err key length");
            if (cipher.Length % 16 != 0) throw new ArgumentException("err data length");

            try
            {
                KeyParameter key = ParameterUtilities.CreateKeyParameter("SM4", keyBytes);
                IBufferedCipher c = CipherUtilities.GetCipher(algo);
                c.Init(false, key);
                return c.DoFinal(cipher);
            }
            catch (Exception e)
            {
                //log.Error("Sm4DecryptECB error: " + e.Message, e);
                return null;
            }
        }


        public static ECPrivateKeyParameters getPrivatekeyFromD(BigInteger d)
        {
            return new ECPrivateKeyParameters(d, ecDomainParameters);
        }

        public static ECPublicKeyParameters getPublickeyFromXY(BigInteger x, BigInteger y)
        {
            return new ECPublicKeyParameters(x9ECParameters.Curve.CreatePoint(x, y), ecDomainParameters);
        }

        public static byte[] sm4Encrypt(byte[] keyBytes, byte[] plain)
        {
            if (keyBytes.Length != 16) throw new ArgumentException("err key length");
            //        if (plain.length % 16 != 0) throw new RuntimeException("err data length");

            try
            {
                KeyParameter key = ParameterUtilities.CreateKeyParameter("SM4", keyBytes);
                IBufferedCipher c = CipherUtilities.GetCipher("SM4/ECB/PKCS7Padding");
                c.Init(true, key);

                return c.DoFinal(plain);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static byte[] sm4Decrypt(byte[] keyBytes, byte[] cipher)
        {
            //        if (keyBytes.length != 16) throw new RuntimeException("err key length");
            if (cipher.Length % 16 != 0) throw new ArgumentException("err data length");

            try
            {
                KeyParameter key = ParameterUtilities.CreateKeyParameter("SM4", keyBytes);
                IBufferedCipher c = CipherUtilities.GetCipher("SM4/ECB/PKCS7Padding");
                c.Init(false, key);
                return c.DoFinal(cipher);

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public const String SM4_ECB_NOPADDING = "SM4/ECB/NoPadding";
        public const String SM4_CBC_NOPADDING = "SM4/CBC/NoPadding";
        public const String SM4_CBC_PKCS7PADDING = "SM4/CBC/PKCS7Padding";

        public static byte[] zeroIv(String algo)
        {

            try
            {
                IBufferedCipher cipher = CipherUtilities.GetCipher(algo);
                int blockSize = cipher.GetBlockSize();
                byte[] iv = new byte[blockSize];
                Arrays.Fill(iv, (byte)0);
                return iv;
            }
            catch (Exception e)
            {
                //log.Error("ZeroIv error: " + e.Message, e);
                return null;
            }
        }

    }
}
