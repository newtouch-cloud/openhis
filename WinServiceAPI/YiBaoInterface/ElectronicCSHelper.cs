using CQYiBaoInterface.Models.Input;
using CQYiBaoInterface.Models.Output;
using CQYiBaoInterface.Models.Post;
using CQYiBaoInterface.sm2sm4;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YiBaoInterface;

namespace CQYiBaoInterface
{
    public static class ElectronicCSHelper
    {
        public static string SaveToInterface1<I, O>(I InModle, out O OutModlem, PostBase post, out string code)
             where O : OutputBase
             where I : InputBase
        {
            //out参数赋初值
            OutModlem = null;
            code = "-1";
            //应用ID
            string appId = ConfigurationManager.AppSettings["appId"];
            //应用秘钥
            string appSecret = ConfigurationManager.AppSettings["appSecret"];
            //应用私钥
            string privateKey = ConfigurationManager.AppSettings["privateKey"];
            //应用公钥
            //(注意：此处应用公钥与平台公钥是不同的，请勿混淆，平台公钥用于对接收平台返回的数据进行验签；应用公钥与应用私钥成对，应用公钥只用于本地的自签名后的验签调用示例；
            //  平台公钥与平台私钥成对，实际与平台进行接口交互时，是使用平台公钥对平台私钥签名后的远端返回数据进行验签)
            string publicKey = ConfigurationManager.AppSettings["publicKey"];

            DateTime getdate = Convert.ToDateTime(ClassSqlHelper.GetServerTime().ToString("yyyy-MM-dd HH:mm:ss"));

            InputPost2<InputBase> input = new InputPost2<InputBase>();
            input.appId = appId;
            input.encType = "SM4";
            input.signType = "SM2";
            input.timestamp = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds.ToString();
            //加密
            string encData = SMUtil.encrypt(JsonConvert.SerializeObject(InModle, Formatting.None), appId, appSecret);

            JObject signDto = (JObject)JObject.Parse(JsonConvert.SerializeObject(input, Formatting.None));
            signDto.Add("data", encData);
            //加签
            String signData = SMUtil.sign(signDto, appSecret, privateKey);
            Console.WriteLine("加签：" + signData);
            //报文
            input.encData = encData;
            input.signData = signData;
            //验签
            Boolean isVerify = SMUtil.verify(signDto, appSecret, publicKey, signData);
            if (isVerify)
            {
                return BuildReturnJson("-1", "验证签名失败！");
            }
            JObject baseJson = JObject.Parse(JsonConvert.SerializeObject(input));

            string strJson = Convert.ToString(baseJson);
            /*0 – 成功，表示此次交易请求成功，业务处理也正常< 0 － 错误，包括系统级别错误(网络、主机、数据库)和业务级别错误，系统级别错误由动态库将错误信息写入输出参数，
                * 业务级别错误由后台通过输出参数提示错误信息。其中：-1－系统级别错误，HIS 系统提示错误信息后，需要进行冲正等后续业务操作处理；-2－业务处理错误，HIS 系统直接将输出参数的错误信息提示给操作员即可。*/
            StringBuilder inputStr = new StringBuilder(50000);


            //保存日志记事本文件 在D盘
            AppLogger.Info("请求交易业务代码：" + post.tradiNumber);
            AppLogger.Info("请求交易入参：" + strJson);
            inputStr.Append(strJson);

            int isSucess = 0;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            string outputText = string.Empty;
            string url = GetPubWay(post.tradiNumber);
            //正式医保调用
            try
            {
                request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "Post";
                request.ContentType = "application/json";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
                request.AllowAutoRedirect = true;
                request.CookieContainer = null;//获取验证码时候获取到的cookie会附加在这个容器里面
                request.KeepAlive = true;//建立持久性连接
                //整数据
                //string paasid = "mbs_fsi_auth";                    
                byte[] bytepostData = System.Text.Encoding.UTF8.GetBytes(strJson);

                //发送数据  using结束代码段释放
                using (Stream requestStm = request.GetRequestStream())
                {
                    requestStm.Write(bytepostData, 0, bytepostData.Length);
                }
                //响应
                response = (HttpWebResponse)request.GetResponse();
                AppLogger.Info("response.ContentLength" + response.ContentLength);
                using (Stream responseStm = response.GetResponseStream())
                {
                    StreamReader redStm = new StreamReader(responseStm, Encoding.UTF8);
                    outputText = redStm.ReadToEnd();
                }
                isSucess = 0;
            }
            catch (Exception ex)
            {
                AppLogger.Info("post请求异常：" + ex.Message + ex.InnerException);
                throw ex;
            }
            AppLogger.Info("请求交易出参：" + outputText);

            //反序列化输出
            OutputReturn outputJson = null;
            try
            {
                outputJson = JsonConvert.DeserializeObject<OutputReturn>(outputText);
                if (outputJson == null)
                {
                    throw new Exception("(空值)");
                }
            }
            catch (Exception ex)
            {
                if (isSucess == 0)
                {
                    return BuildReturnJson("-1", "JSON反序列化BUSINESS_HANDLE函数输出值失败：" + ex.Message + "，输出值：" + outputText);
                }
                else
                {
                    return BuildReturnJson(isSucess.ToString(), outputText);
                }
            }
            //转换交易输出实体类
            if (post.inModel == 1 && outputJson.output != null)
            {
                OutModlem = JsonConvert.DeserializeObject<O>(Convert.ToString(outputJson.output));
            }

            //返回结果
            code = outputJson.infcode;
            return outputText;
        }
        /// <summary>
        /// 构造精简错误输出JSON字符串
        /// </summary>
        /// <param name="infcode">交易状态码</param>
        /// <param name="err_msg">错误信息</param>
        /// <returns></returns>
        public static string BuildReturnJson(string infcode, string err_msg)
        {
            JObject jsonUP = new JObject();
            jsonUP["infcode"] = infcode;
            jsonUP["err_msg"] = err_msg;
            return JsonConvert.SerializeObject(jsonUP);
        }
        private static string GetPubWay(string tradiNumber)
        {
            if (!string.IsNullOrWhiteSpace(tradiNumber))
            {
                string url = "http://10.123.185.12:8080/epc/api";//电子处方直连地址
                switch (tradiNumber)
                {
                    //电子处方上传预核验
                    case "D001":
                        url += "/fixmedins/uploadChk";
                        break;
                    //电子处方医保电子签名
                    case "D002":
                        url += "/fixmedins/rxFixmedinsSign";
                        break;
                    //电子处方上传
                    case "D003":
                        url += "/fixmedins/rxFileUpld";
                        break;
                    //电子处方撤销
                    case "D004":
                        url += "/fixmedins/rxUndo";
                        break;
                    //电子处方信息查询 
                    case "D005":
                        url += "/fixmedins/hospRxDetlQuery";
                        break;
                    //电子处方审核结果查询 
                    case "D006":
                        url += "/fixmedins/rxChkInfoQuery";
                        break;
                    //电子处方取药结果查询 
                    case "D007":
                        url += "/fixmedins/rxSetlInfoQuery";
                        break;
                    //电子处方药品目录查询 
                    case "D008":
                        url += "/fixmedins/circDrugQuery";
                        break;
                }
                return url;
            }
            else
            {
                return "";
            }
        }
    }
}
