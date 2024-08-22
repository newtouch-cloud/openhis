using NeiMengGuYiBaoApp.Models.Input;
using NeiMengGuYiBaoApp.Models.Input.NationECCodeDll;
using NeiMengGuYiBaoApp.Models.Output;
using NeiMengGuYiBaoApp.Models.Output.HeaSecReadInfo;
using NeiMengGuYiBaoApp.Models.Output.NationECCodeDll;
using NeiMengGuYiBaoApp.Models.Post;
using NeiMengGuYiBaoApp.Models.Post.NationECCodeDll;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Text;

namespace NeiMengGuYiBaoApp.Service

{
    public static class DllHelper
    {
        static DllHelper()
        {
            string is_test = ConfigurationManager.AppSettings["is_test"];
            if (is_test == "1")
            {
                IsTest = 1;
            }
            nationEcTransUrl = ConfigurationManager.AppSettings["NationEcTransUrl"];
        }

        [DllImport("NationECCode.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr NationEcTrans(string strUrl, string pindata, StringBuilder poutdata);

        [DllImport("HeaSecReadInfo.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private static extern long Init(string pInitInfo, StringBuilder pErrMsg);

        // 导入 ReadCardBas 函数
        [DllImport("HeaSecReadInfo.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern long ReadCardBas(StringBuilder pCardInfo, StringBuilder pBusiCardInfo);

        [DllImport("HeaSecReadInfo.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern long ReadSFZ(string pPath, StringBuilder pOutBuff, StringBuilder pOutBusiBuff);




        /// <summary>
        /// 是否医保本地模拟测试 1本地测试 0正式医保
        /// </summary>
        public static int IsTest = 0;
        /// <summary>
        /// 刷脸电子凭证扫码接口前置地址
        /// </summary>
        public static string nationEcTransUrl = "";
        /// <summary>
        /// 构造精简错误输出JSON字符串
        /// </summary>
        /// <param name="infcode">交易状态码</param>
        /// <param name="err_msg">错误信息</param>
        /// <returns></returns>
        public static string BuildReturnJson(string infcode, string err_msg)
        {
            JObject jsonUP = new JObject();
            jsonUP["xxfhm"] = infcode;
            jsonUP["fhxx"] = err_msg;
            return JsonConvert.SerializeObject(jsonUP);
        }
       
        public static string GetoutBizNo() {
            DateTime getdate = Convert.ToDateTime(ClassSqlHelper.GetServerTime().ToString("yyyy-MM-dd HH:mm:ss"));
            return ConfigurationManager.AppSettings["fixmedins_code"] + getdate.ToString("yyyyMMdd") + new Random().Next(1, 9999).ToString().PadLeft(9, '0');
        }

        public static string CallNationECCodeAndSaveLog<O>(InputBasePost input, out O OutModlem, PostBase post, out int code)
         where O : OutputBase
        {

            AppLogger.Info("进入NationECCode-Call");
            //out参数赋初值
            OutModlem = null;
            DateTime startdate = Convert.ToDateTime(ClassSqlHelper.GetServerTime().ToString("yyyy-MM-dd HH:mm:ss"));
            code = 0;
            JObject inputJson = JObject.Parse(JsonConvert.SerializeObject(input));
            string inputJsonStr = Convert.ToString(inputJson);

            string sendMsg = "";
            StringBuilder rcvMsg = new StringBuilder(20480);
            
            //保存日志记事本文件 在C盘
            AppLogger.Info("请求交易类型编码：" + input.transType);
            AppLogger.Info("请求交易入参：" + inputJsonStr);
            sendMsg = inputJsonStr;

            string outputText = string.Empty;
            string testdata = string.Empty;
            string returnValueText = "0000";
            if (IsTest == 0)
            {
                try
                {
                 
                    IntPtr returnValue = NationEcTrans(nationEcTransUrl, sendMsg, rcvMsg);
                    returnValueText = Marshal.PtrToStringAnsi(returnValue);
                    if (returnValueText == "0000")
                    {
                        outputText = rcvMsg.ToString();
                    }
                    else {
                        AppLogger.Info("调用医保异常: " + returnValueText);
                        var errOut = new { code = -1, message = "调用医保异常: " + rcvMsg };
                        outputText = JsonConvert.SerializeObject(errOut);
                    }
                }
                catch (Exception ex)
                {
                    AppLogger.Info("调用医保异常: " + ex.Message);
                    returnValueText = ex.Message;
                    var errOut = new { code = -1, message = "调用医保异常: " + returnValueText };
                    outputText = JsonConvert.SerializeObject(errOut);
                }
            }
            else
            {
                switch (input.transType)
                {
                    case "cn.nhsa.ec.auth":
                        testdata = "{\"code\":0,\"data\":\"{\\\"authNo\\\":\\\"ano3728680187894374401630000\\\"}\",\"message\":\"\",\"orgId\":\"H15090201645\"}";
                        break;
                    case "cn.nhsa.auth.check":
                        testdata = "{\"code\":0,\"data\":\"{\\\"authNo\\\":\\\"ano3728678940491980801630000\\\",\\\"birthday\\\":\\\"1998.01.01\\\",\\\"chnlId\\\":\\\"null\\\",\\\"ecIndexNo\\\":\\\"E7244957FA2EBCE6827B53F9073F8874\\\",\\\"ecQrCode\\\":\\\"\\\",\\\"ecToken\\\":\\\"630000ec7ioqup8p1o0761820a0000f1f8b13d\\\",\\\"email\\\":\\\"123@163.com\\\",\\\"gender\\\":\\\"男\\\",\\\"idNo\\\":\\\"身份证号\\\",\\\"idType\\\":\\\"01\\\",\\\"insuOrg\\\":\\\"330399\\\",\\\"nationality\\\":\\\"null\\\",\\\"userName\\\":\\\"阿炳\\\"}\",\"message\":\"成功\",\"orgId\":\"H15090201645\"}";
                        break;
                    case "ec.query":
                        testdata = "{\"code\":0,\"data\":\"{\\\"extra\\\":\\\"\\\",\\\"idNo\\\":\\\"533222199909090123\\\",\\\"idType\\\":\\\"01\\\",\\\"ecToken\\\":\\\"ADBHJRS5I01E0F3438C00000B959FA88\\\",\\\"insuOrg\\\":\\\"430000\\\",\\\"userName\\\":\\\"阿炳\\\",\\\"ecIndexNo\\\":\\\"ecIndexNo\\\",\\\"gender\\\":\\\"男\\\",\\\"birthday\\\":\\\"1998.01.01\\\",\\\"nationality\\\":\\\"中国\\\",\\\"email\\\":\\\"123@163.com\\\"}\",\"message\":\"处理成功\"}";
                        break;
                    case "cn.nhsa.qrcode.get":
                        testdata = "{\"code\":0,\"data\":\"{\\\"birthday\\\":\\\"1998.01.01\\\",\\\"ecToken\\\":\\\"630000ec7ip4n16p3p0761820a0000aba21c7f\\\",\\\"email\\\":\\\"123@163.com\\\",\\\"gender\\\":\\\"男\\\",\\\"idNo\\\":\\\"身份证\\\",\\\"idType\\\":\\\"01\\\",\\\"insuOrg\\\":\\\"330399\\\",\\\"ecIndexNo\\\":\\\"ecIndexNo\\\",\\\"nationality\\\":\\\"中国\\\",\\\"userName\\\":\\\"阿炳\\\"}\",\"message\":\"成功\",\"orgId\":\"H15090201645\"}";
                        break;
                    default:
                        break;
                }
                outputText = Convert.ToString(testdata);
            }
            AppLogger.Info("请求交易出参：" + outputText);
            //反序列化输出
            OutputBasePost outputJson = null;
            try
            {
                outputJson = JsonConvert.DeserializeObject<OutputBasePost>(outputText);
                if (outputJson == null)
                {
                    throw new Exception("(空值)");
                }
            }
            catch (Exception ex)
            {
                code = -1;
                return BuildReturnJson("-1", "JSON反序列化SendRcv4函数输出值失败：" + ex.Message + "，输出值：" + outputText);
            }
            //保存交易日志到数据库
            AppLogger.SaveNMGJyDbLog(post, startdate, inputJsonStr, outputJson, returnValueText);
            //转换交易输出实体类
            code = outputJson.code;
            if (outputJson.data != null)
            {
                OutModlem = JsonConvert.DeserializeObject<O>(Convert.ToString(outputJson.data));
                return outputText;
            }
            else {
                
                JObject obj = JObject.Parse(rcvMsg.ToString());
                return (string)obj["message"];
            }

            //返回结果
            
            
        }

        public static string HeaSecReadInfoInit(out int code) {
            InitInput initInput = new InitInput();
            initInput.IP = ConfigurationManager.AppSettings["YLBZPT_IP"];
            int port;
            if (int.TryParse(ConfigurationManager.AppSettings["YLBZPT_PORT"], out port))
            {
                initInput.PORT = port;
            }
            else {
                code = -1;
                return BuildReturnJson("-1", "获取YLBZPT_PORT失败");
            }
            initInput.TIMEOUT = 60;
            initInput.LOG_PATH = "C:\\log_yibao";
            initInput.SFZ_DRIVER_TYPE = 0;

            if (IsTest == 0)
            {
                StringBuilder errMsg = new StringBuilder(2048); // 预分配内存大小为2048字节

                // 调用 DLL 中的 Init 函数
                long result = Init(JsonConvert.SerializeObject(initInput), errMsg);
                if (result == 0)
                {
                    code = 0;
                    return "";
                }
                else
                {
                    code = -1;
                    return BuildReturnJson("-1", "调用HeaSecReadInfoInit失败，信息是：" + errMsg);
                }
            }
            else {
                code = 0;
                return "";
            }


                
        }

        public static string CallReadCardBasAndSaveLog<O>(out O OutModlem, PostBase post, out int code)
            where O : OutputBase
        {
            AppLogger.Info("CallReadCardBasAndSaveLog-Call");
            //out参数赋初值
            OutModlem = null;
            DateTime startdate = Convert.ToDateTime(ClassSqlHelper.GetServerTime().ToString("yyyy-MM-dd HH:mm:ss"));
            code = 0;
            string inputJsonStr = "";

            //保存日志记事本文件 在C盘
            AppLogger.Info("请求交易类型编码：" + post.tradiNumber);
            AppLogger.Info("请求交易入参：" + inputJsonStr);

            string outputText = string.Empty;
            long returnValue = 0;
            OutputBasePost outputJson = new OutputBasePost();
            if (IsTest == 0)
            {
                try
                {
                    // 分配足够的内存给输出参数 pCardInfo 和 pBusiCardInfo
                    StringBuilder cardInfo = new StringBuilder(2048);
                    StringBuilder busiCardInfo = new StringBuilder(8192);
                    returnValue = ReadCardBas(cardInfo, busiCardInfo);
                    if (returnValue == 0)
                    {
                        outputJson.code = 0;
                        outputText = cardInfo.ToString();
                        if (!string.IsNullOrWhiteSpace(outputText))
                        {
                            if (outputText.EndsWith("|"))
                            {
                                outputText = outputText.TrimEnd('|');
                            }

                            var parts = outputText.Split('|');
                            if (parts.Length < 11)
                            {
                                AppLogger.Info("数据项不足，无法解析社保卡基本信息: " + cardInfo.ToString());
                                var errOut = new { code = -1, message = "数据项不足，无法解析社保卡基本信息:  " + cardInfo.ToString() };
                                outputJson.message = JsonConvert.SerializeObject(errOut);
                            }
                            else {
                                ReadCardBas readCardBas =new ReadCardBas {
                                    IssuingAreaCode = parts[0],
                                    SocialSecurityNumber = parts[1],
                                    CardNumber = parts[2],
                                    CardIdentificationCode = parts[3],
                                    Name = parts[4],
                                    CardResetInfo = parts[5],
                                    SpecificationVersion = parts[6],
                                    IssuingDate = parts[7],
                                    ExpirationDate = parts[8],
                                    TerminalNumber = parts[9],
                                    TerminalDeviceNumber = parts[10]
                                };
                                outputJson.data = JObject.Parse(JsonConvert.SerializeObject(readCardBas));
                            }
                        }
                    }
                    else
                    {
                        outputJson.code = ((int)returnValue);
                        AppLogger.Info("调用ReadCardBas异常: " + cardInfo.ToString());
                        outputJson.message = cardInfo.ToString();
                    }
                }
                catch (Exception ex)
                {
                    AppLogger.Info("调用医保异常: " + ex.Message);
                    outputJson.code = -1;
                    outputJson.message = "调用医保异常: " + ex.Message;
                }
            }
            else
            {
                outputJson.code = 0;
                outputJson.data = JObject.Parse("{\"IssuingAreaCode\":\"639900\",\"SocialSecurityNumber\":\"111111198101011110\",\"CardNumber\":\"X00000019\",\"CardIdentificationCode\":\"639900D15600000500BF7C7A48FB4966\",\"Name\":\"阿炳\",\"CardResetInfo\":\"00814E43238697159900BF7C7A\",\"SpecificationVersion\":\"1.00\",\"IssuingDate\":\"20101001\",\"ExpirationDate\":\"20201001\",\"TerminalNumber\":\"410100813475\",\"TerminalDeviceNumber\":\"终端设备号\"}");
                outputText = Convert.ToString(outputJson.data);
            }
            AppLogger.Info("请求交易出参：" + outputText);

            //保存交易日志到数据库
            AppLogger.SaveNMGJyDbLog(post, startdate, inputJsonStr, outputJson, outputJson.code.ToString());
            //转换交易输出实体类
            if (outputJson.data != null)
            {
                OutModlem = JsonConvert.DeserializeObject<O>(Convert.ToString(outputJson.data));
            }
            //返回结果
            code = outputJson.code;
            return outputJson.data.ToString();
        }

        public static string CallReadSFZAndSaveLog<O>(out O OutModlem, PostBase post, out int code)
            where O : OutputBase
        {
            AppLogger.Info("CallReadSFZAndSaveLog-Call");
            //out参数赋初值
            OutModlem = null;
            DateTime startdate = Convert.ToDateTime(ClassSqlHelper.GetServerTime().ToString("yyyy-MM-dd HH:mm:ss"));
            code = 0;
            string inputJsonStr = "";

            //保存日志记事本文件 在C盘
            AppLogger.Info("请求交易类型编码：" + post.tradiNumber);
            AppLogger.Info("请求交易入参：" + inputJsonStr);

            string outputText = string.Empty;
            long returnValue = 0;
            OutputBasePost outputJson = new OutputBasePost();
            if (IsTest == 0)
            {
                try
                {
                    string path = null; // 传入空字符串表示不生成照片
                    StringBuilder outBuff = new StringBuilder(1024);
                    StringBuilder outBusiBuff = new StringBuilder(8192);

                    returnValue = ReadSFZ(path, outBuff, outBusiBuff);
                    if (returnValue == 0)
                    {
                        outputJson.code = 0;
                        outputText = outBuff.ToString();
                        if (!string.IsNullOrWhiteSpace(outputText))
                        {
                            if (outputText.EndsWith("|"))
                            {
                                outputText = outputText.TrimEnd('|');
                            }

                            var parts = outputText.Split('|');
                            if (parts.Length < 9)
                            {
                                AppLogger.Info("数据项不足，无法获取身份证信息: " + outBuff.ToString());
                                var errOut = new { code = -1, message = "数据项不足，无法获取身份证信息:  " + outBuff.ToString() };
                                outputJson.message = JsonConvert.SerializeObject(errOut);
                            }
                            else
                            {
                                ReadSFZ readSFZ = new ReadSFZ
                                {
                                    Name = parts[0],
                                    Gender = parts[1],
                                    Ethnicity = parts[2],
                                    BirthDate = parts[3],
                                    Address = parts[4],
                                    IdNumber = parts[5],
                                    IssueDate = parts[6],
                                    ExpiryDate = parts[7],
                                    IssuingAuthority = parts[8]
                                };
                                outputJson.data = JObject.Parse(JsonConvert.SerializeObject(readSFZ));
                            }
                        }
                    }
                    else
                    {
                        outputJson.code = ((int)returnValue);
                        AppLogger.Info("调用ReadCardBas异常: " + outBuff.ToString());
                        outputJson.message = outBuff.ToString();
                    }
                }
                catch (Exception ex)
                {
                    AppLogger.Info("调用医保异常: " + ex.Message);
                    outputJson.code = -1;
                    outputJson.message = "调用医保异常: " + ex.Message;
                }
            }
            else
            {
                outputJson.code = 0;
                outputJson.data = JObject.Parse("{\"Name\":\"阿炳\",\"Gender\":\"男\",\"Ethnicity\":\"汉\",\"BirthDate\":\"19980101\",\"Address\":\"阿炳\",\"IdNumber\":\"310103197307252018\",\"IssueDate\":\"19980101\",\"ExpiryDate\":\"20101001\",\"IssuingAuthority\":\"签发机关\"}");
                outputText = Convert.ToString(outputJson.data);
            }
            AppLogger.Info("请求交易出参：" + outputText);

            //保存交易日志到数据库
            AppLogger.SaveNMGJyDbLog(post, startdate, inputJsonStr, outputJson, outputJson.code.ToString());
            //转换交易输出实体类
            if (outputJson.data != null)
            {
                OutModlem = JsonConvert.DeserializeObject<O>(Convert.ToString(outputJson.data));
            }
            //返回结果
            code = outputJson.code;
            return outputJson.data.ToString();
        }

    }
}
