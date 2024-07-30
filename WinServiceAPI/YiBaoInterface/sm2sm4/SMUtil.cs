using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.sm2sm4
{
    public class SMUtil
    {
        /**
       * 加密
       *
       * @param data
       * @param appId
       * @param appSecret
       * @return
       */
        public static String encrypt(String data, String appId, String appSecret)
        {
            //加密流程
            //用appId加密appSecret获取新秘钥
            byte[] appSecretEncData = EasyGmUtils.sm4Encrypt(Encoding.UTF8.GetBytes(appId.Substring(0, 16)), Encoding.UTF8.GetBytes(appSecret));
            //新秘钥串
            byte[] secKey = Encoding.UTF8.GetBytes(Hex.ToHexString(appSecretEncData).ToUpper().Substring(0, 16));
            //加密0数据
            String encryptDataStr = Hex.ToHexString(EasyGmUtils.sm4Encrypt(secKey, Encoding.UTF8.GetBytes(data))).ToUpper();
            return encryptDataStr;
        }

        /**
         * 解密
         *
         * @param data
         * @param appId
         * @param appSecret
         * @return
        */
        public static String decrypt(String data, String appId, String appSecret)
        {
            byte[] appSecretEncDataDecode = EasyGmUtils.sm4Encrypt(Encoding.UTF8.GetBytes(appId.Substring(0, 16)), Encoding.UTF8.GetBytes(appSecret));
            byte[] secKeyDecode = Encoding.UTF8.GetBytes(Hex.ToHexString(appSecretEncDataDecode).ToUpper().Substring(0, 16));
            String decryptDataStr = Encoding.UTF8.GetString(EasyGmUtils.sm4Decrypt(secKeyDecode, Hex.Decode(data)));
            return decryptDataStr;
        }

        /**
        * 签名
        *
        * @param jsonObject
        * @param appSecret
        * @param privateKey
        * @return
        */
        public static String sign(JObject jsonObject, String appSecret, String privateKey)
        {
            // 获取签名串
            byte[] signText = Encoding.UTF8.GetBytes(SignUtil.getSignText(jsonObject, appSecret));
            byte[] userId = Encoding.UTF8.GetBytes(appSecret);
            byte[] prvkey = Base64.Decode(privateKey);
            String responseSign = Base64.ToBase64String(EasyGmUtils.signSm3WithSm2(signText, userId, prvkey));
            return responseSign;
        }

        /**
         * 验签
         *
         * @param jsonObject
         * @param appSecret
         * @param publicKey
         * @param responseSign
         * @return
         */
        public static Boolean verify(JObject jsonObject, String appSecret, String publicKey, String responseSign)
        {
            //验签
            byte[] msg = Encoding.UTF8.GetBytes(SignUtil.getSignText(jsonObject, appSecret));
            byte[] userIdDecode = Encoding.UTF8.GetBytes(appSecret);
            byte[] pubkey = Base64.Decode(publicKey);
            byte[] signData = Base64.Decode(responseSign);
            return EasyGmUtils.verifySm3WithSm2(msg, userIdDecode, signData, pubkey);
        }



        /**
        * 签名
        *
        * @param jsonObject
        * @param appSecret
        * @param privateKey
        * @return
        */
        public static String sign(String jsonString, String appSecret, String privateKey)
        {
            JObject jsonObject = (JObject)JObject.Parse(jsonString);
            // 获取签名串
            byte[] signText = Encoding.UTF8.GetBytes(SignUtil.getSignText(jsonObject, appSecret));
            byte[] userId = Encoding.UTF8.GetBytes(appSecret);
            byte[] prvkey = Base64.Decode(privateKey);
            String responseSign = Base64.ToBase64String(EasyGmUtils.signSm3WithSm2(signText, userId, prvkey));
            return responseSign;
        }

        /**
         * 验签
         *
         * @param jsonObject
         * @param appSecret
         * @param publicKey
         * @param responseSign
         * @return
         */
        public static Boolean verify(String jsonString, String appSecret, String publicKey, String responseSign)
        {
            JObject jsonObject = (JObject)JObject.Parse(jsonString);
            //验签
            byte[] msg = Encoding.UTF8.GetBytes(SignUtil.getSignText(jsonObject, appSecret));
            byte[] userIdDecode = Encoding.UTF8.GetBytes(appSecret);
            byte[] pubkey = Base64.Decode(publicKey);
            byte[] signData = Base64.Decode(responseSign);
            return EasyGmUtils.verifySm3WithSm2(msg, userIdDecode, signData, pubkey);
        }
    }
}
