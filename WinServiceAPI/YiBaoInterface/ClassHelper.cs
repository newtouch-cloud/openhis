using CQYiBaoInterface.Models.Input;
using CQYiBaoInterface.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Configuration;
using CQYiBaoInterface.Models;
using CQYiBaoInterface.Models.Post;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Management;
using System.Reflection;
using System.Security.Cryptography;
using System.IO;
using SqlHelper;
using System.Web;
using System.Threading;
using YiBaoInterface.Models;
using Microsoft.VisualBasic;
using System.Windows.Forms;

namespace YiBaoInterface
{
    public static class ClassHelper
    {
        /// <summary>
        /// 是否医保本地模拟测试 1本地测试 0正式医保
        /// </summary>
        public static int IsTest = 0;
        static ClassHelper()
        {
            string is_test = System.Configuration.ConfigurationManager.AppSettings["is_test"];
            if (is_test == "1")
            {
                IsTest = 1;
            }
        }
        /// <summary>
        /// 交易输入各个实体的json
        /// </summary>
        //public static string InputJson = "";

        /// <summary>
        /// 交易输入
        /// </summary>
        //public static string OutputJson = "";
        /// <summary>
        /// 整体输入的json串
        /// </summary>
        //public static string BaseJson = "";
        /// <summary>
        /// 凭证号码
        /// </summary>
        //public static decimal InNumber = 0;
        #region 测试环境
        //public const string paasid = "mbs_fsi_auth";
        //public const string token = "d6Ad0P1B3z6Dsdst0gYAFPlz8YlIvFDx";
        //public const string paasid = "mbs_fsi_auth";
        //public const string token = "d6Ad0P1B3z6Dsdst0gYAFPlz8YlIvFDx";
        #endregion
        public const string paasid = "99999999404";
        public const string token = "dec15f850fc54c77a91d245663122cb3";
        public const string RSAKeyPrv = "-----BEGIN PRIVATE KEY-----MIICeAIBADANBgkqhkiG9w0BAQEFAASCAmIwggJeAgEAAoGBAKx1bpyh6A12DukVrQxhZbmaSmeaS335leLTY2xXRTGfshh0X6f9roPu+oSQFNq0ITMAyeNQ6XBhrNOmvwV+WZAVreaRTxwD91/05VObQVbNBXhd5ebSws5CUDd/oUWMejrfjexOYo2XjIpO4goBZaSjdEH+DtlBbdqkM/OtwypjAgMBAAECgYA7PrRbFrvTNAV3ST0DjcoWcsywvq7EfiNhouD67RgWLhhOklvyKBH1vPlO5PhlEZB+Jv00HC21r7hhlVz2FCvL5US6itKUE0U6rjV1XoUTF8a/4/sawYRBSYWeln0GvCbvsEN4qNSVdjjhGHt+N7nf1BcIaF9PJRPOhjnqWuMU0QJBANuhmRhpjv6YxQjfJJI/dBfFv+qvZ6PMY7OSRv+UeSbzaQpRdSLOno8Dti72R959VEf2idZ10gP67njKS/Yl/80CQQDJBCP/PXtOoKWSQyKv1VEbnBmR/lExebF3w0hNtHT3GR3vTrGRFNQk0ieXyMBZQiXDXTGIRlLChVMqP3aWMMLvAkEAt4IQDsu0BJnUl5MKVX/bGjnKHuar67pM268uz0FY8OiULWDeRFTrOodZY2e3qPiCwRYHGT+cWGMDeb1dNq+NSQJBAJcnzu3CzLNOZ9K5Ox8feDMbybqXk3RxvvqA1SDhXsbkkzb9ZNbk47WgdfdUFRfJft4OeQ2xW9A8M1JkkIbVue8CQQCeMWH5CZ3fbfTJCRAlZ/08K9mirqUShAs0x3tkC31FQOOQY6qdrnmzQKsihhb++9Vtrjkjnr5YgWnwMcKBlknb-----END PRIVATE KEY-----";
        public const string RSAKeyPub = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCz21YSY9QWzyB6ulfTo8r2x2OENVGheM+fSGIG4Ew4e7sjgMGfoHCP7klWtkndQy8mPCsqNpXtjWDufwq0W8ZuztZTCRUoqHMZLtJgpsTtRfYFwwftxnRzDzxKGzrtCRj1ldKbKTLrKzyX1k+fbgZMUgpVuuaNnDLtB8V3hxkNSQIDAQAB";
        //public const string RSAKeyPub = "-----BEGIN PUBLIC KEY-----MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCsdW6coegNdg7pFa0MYWW5mkpnmkt9+ZXi02NsV0Uxn7IYdF+n/a6D7vqEkBTatCEzAMnjUOlwYazTpr8FflmQFa3mkU8cA/df9OVTm0FWzQV4XeXm0sLOQlA3f6FFjHo6343sTmKNl4yKTuIKAWWko3RB/g7ZQW3apDPzrcMqYwIDAQAB-----BEGIN PUBLIC KEY-----";
        private static string downloadfilepath = ConfigurationManager.AppSettings["DownloadFilePath"];
        private static string ybRequestPath = ConfigurationManager.AppSettings["qqurl"];//国家医保前置机器请求地址
        /// <summary>
        /// 获取本地的IP地址
        /// </summary>
        /// <returns></returns>
        private static string GetLocalIp()
        {
            if (!string.IsNullOrEmpty(YiBaoInitHelper.localIp))
            {
                return YiBaoInitHelper.localIp;
            }

            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();

                    YiBaoInitHelper.localIp = AddressIP;
                }
            }

            return AddressIP;
        }

        /// <summary>
        /// 获取mac地址
        /// </summary>
        /// <returns></returns>
        private static string GetLocalMac()
        {
            if (!string.IsNullOrEmpty(YiBaoInitHelper.localMac))
            {
                return YiBaoInitHelper.localMac;
            }

            string ip = GetLocalIp();
            string mac = "";
            try
            {
                ManagementClass mcMAC = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection mocMAC = mcMAC.GetInstances();
                foreach (ManagementObject m in mocMAC)
                {
                    if (!(bool)m["IPEnabled"]) continue;
                    System.Array ar = (System.Array)(m.Properties["IPAddress"].Value);
                    string zip = ar.GetValue(0).ToString();
                    if (zip != ip) continue;
                    mac = m["MacAddress"].ToString();
                    break;
                }
                if (!string.IsNullOrEmpty(mac)) return mac;

                //如果以IP没有找到相应的MAC则获取默认第一个MAX
                foreach (ManagementObject m in mocMAC)
                {
                    if ((bool)m["IPEnabled"])
                    {
                        mac = m["MacAddress"].ToString();
                        break;
                    }
                }
            }
            catch
            {
                mac = "00:00:00:00:00:00";
            }
            finally /*因上述代码中间有return*/
            {
                YiBaoInitHelper.localMac = mac;
            }

            return mac;
        }



        /*
        private static string SetLogMain(PostBase post, DateTime getdate)
        {
            string msg = "";
            //todo  调用接口前，保存输入数据到操作日志中
            LogMain main = new LogMain();
            main.inNumber = InNumber;
            main.inDate = getdate;
            main.operatorId = post.operatorId;
            main.userIp = GetLocalIp();// post.userId;
            main.tradiNumber = post.tradiNumber;
            if (ClassSqlHelper.InputLogMain(main) < 1)
            {
                msg = "HIS返回信息：写入交易信息主表失败MainLog";
            }
            return msg;
        }
        */

        private static void SettingsBase(PostBase post, DateTime getdate, InputPost1<InputBase> input)
        {
            input.infno = post.tradiNumber;
            input.msgid = ConfigurationManager.AppSettings["fixmedins_code"] + getdate.ToString("yyyyMMddHHmmss") + new Random().Next(1,9999).ToString().PadLeft(4,'0');
            input.mdtrtarea_admvs = ConfigurationManager.AppSettings["mdtrtarea_admvs"]; // 3 mdtrtarea_admvs 就医地医保区划 字符型  6  Y 
            input.insuplc_admdvs = post.insuplc_admdvs;//4  insuplc_admdvs 参保地医保区划 字符型  6 如果交易输入中含有人员编号，此项必填，可通过【1101】人员信息获取交易取得
            input.recer_sys_code = GetRecerSysCode(post, input); //5 recer_sys_code 接收方系统代码 字符型 10  Y
            input.dev_no = ConfigurationManager.AppSettings["dev_no"];//6  dev_no  设备编号  字符型 100 
            input.dev_safe_info = ConfigurationManager.AppSettings["dev_safe_info"]; //7  dev_safe_info  设备安全信息 字符型 2000 
            input.signtype = ConfigurationManager.AppSettings["signtype"]; ;//9  signtype  签名类型  字符型 10  建议使用 SM2、SM3
            input.infver = ConfigurationManager.AppSettings["infver"]; //10  infver  接口版本号  字符型  6  Y例如：“V1.0”，版本号由医保下发通知。
            input.opter_type = ConfigurationManager.AppSettings["opter_type"];//经办人类别  3  Y  Y1-经办人；2-自助终端；3-移动终端
            input.opter = post.operatorId;//经办人  字符型 30  Y  按地方要求传入经办人/终端编号
            input.opter_name = post.operatorName;//
            input.inf_time = getdate.ToString("yyyy-MM-dd HH:mm:ss");
            input.fixmedins_code = ConfigurationManager.AppSettings["fixmedins_code"];// fixmedins_code定点医药机构编号字符型 30  Y 
            input.fixmedins_name = ConfigurationManager.AppSettings["fixmedins_name"];//fixmedins_name定点医药机构名称字符型 200  Y 
            input.sign_no = YiBaoInitHelper.sign_no;//sign_no  交易签到流水号 字符型 30  通过签到【9001】交易获取
            //input.serv_code = ConfigurationManager.AppSettings["serv_code"];//服务商代码
            //input.serv_sign = ConfigurationManager.AppSettings["serv_sign"];//服务商识别码
            input.cainfo = input.cainfo ?? "";

            //input.input.data = InModle;
            //调用【2102】药店结算、【2207】门诊结算、【2304】住院结算三个接口需要验证患者电子凭证密码或医保卡密码（跨省异地不验密）。测试环境会真实的验证电子凭证密码，对接时可以单独调试验密部分，验密没问题后可以使用MOCK模式在测试环境做结算测试。

            if (post.tradiNumber == "2207" || post.tradiNumber == "2304" || post.tradiNumber == "2102")
            {
                 if (input.signtype != "MOCK")
                {
                    //string rsapubkey = RSAManager.PemToRSAKey(RSAKeyPub, false);
                    int cnt = 0;
                    string pwd = "123456";
                    //while (cnt <= 1)
                    //{
                    //    pwd = Interaction.InputBox("输入卡密码", "密码框", "", -1, -1);   //int型为弹出框坐标,str为返回的输入参数
                    //    AppLogger.Info("进入SettingsBase密码:" + pwd);
                    //    if (pwd.Length != 0)
                    //    {
                    //        cnt = 2;
                    //        break;
                    //    }
                    //    MessageBox.Show("请输入社保卡密码!");
                    //    cnt++;
                    //}
                    input.cainfo = RSAHelper.EncryptByPublicKey(pwd !=""? pwd:"123456"+ token, RSAKeyPub);
                }
            }
        }

        private static string GetRecerSysCode(PostBase post, InputPost1<InputBase> input)
        {
            /*
                普通交易传 YBXT,与人相关的交易时离休人员业务传 LXXT,工伤人员业务传 GSXT;
                【2201】门诊挂号、【2206】门诊预结算、【2207】门诊结算
                【2303】住院预结算、【2304】住院结算
                【2401】入院办理、【2402】出院办理、【2501】转院备案
                【2503】人员慢特病备案、【2001】人员待遇享受检查
            */
            if (post.tradiNumber == "2201" || post.tradiNumber == "2206" || post.tradiNumber == "2207"
                || post.tradiNumber == "2303" || post.tradiNumber == "2304" 
                || post.tradiNumber == "2401" || post.tradiNumber == "2402" || post.tradiNumber == "2501" 
                || post.tradiNumber == "2503" || post.tradiNumber == "2001")
            {
                if (input.input != null)
                {
                    //获取节点名
                    string nodeName = "data";
                    if (post.tradiNumber == "2401")
                    {
                        nodeName = "mdtrtinfo";
                    }
                    else if (post.tradiNumber == "2402")
                    {
                        nodeName = "dscginfo";
                    }
                    else if (post.tradiNumber == "2501")
                    {
                        nodeName = "refmedin";
                    }
                    //获取节点属性
                    PropertyInfo nodeProp = input.input.GetType().GetProperty(nodeName);
                    if (nodeProp != null)
                    {
                        object nodeVal = nodeProp.GetValue(input.input);
                        if (nodeVal != null)
                        {
                            //获取险种类型属性
                            PropertyInfo pro = nodeVal.GetType().GetProperty("insutype");
                            if (pro != null)
                            {
                                object val = pro.GetValue(nodeVal);
                                if (val != null)
                                {
                                    //离休人员医疗保障
                                    if (val.ToString() == "340")
                                    {
                                        return "LXXT";
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return ConfigurationManager.AppSettings["recer_sys_code"];
        }


        /// <summary>
        /// 删除json指定的节点
        /// </summary>
        /// <param name="jsonStr">json字符串</param>
        /// <param name="node">节点</param>
        private static void JsonRemoveNode(JObject json, string node)
        {
            JObject jsonNode = (JObject)json["input"];
            jsonNode.Property(node).Remove();

        }


        /// <summary>
        /// 签到 每个交易之前调用 如果未签到则自动签到
        /// </summary>
        /// <returns>已签到或签到成功返回空串，否则返回JSON串</returns>
        private static string DoSigin(string operatorId, string operatorName)
        {
            if (YiBaoInitHelper.hasSignIn)
            {
                return string.Empty;
            }

            Input_9001 input = new Input_9001();

            input.signIn = new signIn();
            input.signIn.opter_no = operatorId;
            input.signIn.mac = GetLocalMac();
            input.signIn.ip = GetLocalIp();


            Output_9001 output = new Output_9001();

            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "9001";
            post.operatorId = operatorId;
            post.operatorName = operatorName;
            post.insuplc_admdvs = "";
            post.inModel = 1;

            string code = "1";
            string oprs = SaveToInterface1(input, out output, post, out code);
            if (code == "0" && output.signinoutb.sign_no != "")
            {
                YiBaoInitHelper.hasSignIn = true;
                YiBaoInitHelper.sign_no = output.signinoutb.sign_no;
                return string.Empty;
            }
            else
            {
                return oprs;
            }
        }

        /*
        public static string SaveToInterfaceList<I, O>(I InModle, out List<O> OutModlem, PostBase post, out string code)
            where O : OutputBase
            where I : InputBase
        {
            string msg = "";
            OutputJson = "";
           
            //初始化
            msg = YiBaoInitHelper.IsInit();
            //签到
            if (YiBaoInitHelper.signIn == 0)
            {
                GetSigin(post.operatorId, post.operatorName);
            }

            //获取凭证号码，每个医保交易必须的字段
            if (!ClassSqlHelper.GetCertificateNumber("jypzhm", out InNumber) || InNumber <= 0)
            {
                InNumber = -100;
                msg = "HIS返回信息：获取医保凭证号码失败";
            }
            DateTime getdate = Convert.ToDateTime(ClassSqlHelper.GetServerTime().ToString("yyyy-MM-dd HH:mm:ss"));

            try
            {
                //todo  调用接口前，保存输入数据到操作日志中
                msg = SetLogMain(post, getdate);
                InputPost1<InputBase> input = new InputPost1<InputBase>();
                input.input = new InputBase();
                input.input = InModle;
                SettingsBase(post, getdate, input);

                JObject baseJson = JObject.Parse(JsonConvert.SerializeObject(input));

                StringBuilder inputStr = new StringBuilder(50000);
                StringBuilder outputStr = new StringBuilder(50000);
                //保存日志记事本文件 在D盘
                AppLogger.Info("请求交易业务代码：" + post.tradiNumber);
                AppLogger.Info("请求交易入参：" + Convert.ToString(baseJson));
                inputStr.Append(Convert.ToString(baseJson));
                int isSucess = YiBaoInitHelper.BUSINESS_HANDLE(inputStr, outputStr);//调用接口
                OutputReturn outputJson = JsonConvert.DeserializeObject<OutputReturn>(Convert.ToString(outputStr));
                AppLogger.Info("请求交易出参：" + Convert.ToString(outputStr));


                //msg = SetLogContent(post, getdate, input, outputJson, isSucess); 数据量大 不保存数据库
                OutModlem = JsonConvert.DeserializeObject<List<O>>(Convert.ToString(outputJson.output));
                code = outputJson.infcode;
                if (msg != "")
                {
                    JObject jsonUP = JObject.Parse(OutputJson);
                    jsonUP["err_msg"] = jsonUP["err_msg"] + "HIS信息提示：写入日志表失败";
                    jsonUP["infcode"] = "-99";
                    return Convert.ToString(jsonUP);
                }
                else
                {
                    return OutputJson;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        */

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

        public static string SaveToInterface1<I, O>(I InModle, out O OutModlem, PostBase post, out string code)
          where O : OutputBase
          where I : InputBase
        {
            AppLogger.Info("进入SaveToInterface1");
            //out参数赋初值
            OutModlem = null;
            code = "-1";

            #region test初始化 
            //签到
            if (post.tradiNumber != "9001")
            {
                string sns = DoSigin(post.operatorId, post.operatorName);
                if (!string.IsNullOrEmpty(sns))
                {
                    return sns;
                }
            }
            #endregion
            DateTime getdate = Convert.ToDateTime(ClassSqlHelper.GetServerTime().ToString("yyyy-MM-dd HH:mm:ss"));

            InputPost1<InputBase> input = new InputPost1<InputBase>();
            input.input = new InputBase();
            input.input = InModle;
            AppLogger.Info("进入SettingsBase");
            SettingsBase(post, getdate, input);
            AppLogger.Info("进入SettingsBase:"+ input.cainfo);
            JObject baseJson = post.tradiNumber == "1162" ? JObject.Parse(JsonConvert.SerializeObject(InModle)) : JObject.Parse(JsonConvert.SerializeObject(input));
            string strJson = Convert.ToString(baseJson);
            /*0 – 成功，表示此次交易请求成功，业务处理也正常< 0 － 错误，包括系统级别错误(网络、主机、数据库)和业务级别错误，系统级别错误由动态库将错误信息写入输出参数，
                * 业务级别错误由后台通过输出参数提示错误信息。其中：-1－系统级别错误，HIS 系统提示错误信息后，需要进行冲正等后续业务操作处理；-2－业务处理错误，HIS 系统直接将输出参数的错误信息提示给操作员即可。*/
            StringBuilder inputStr = new StringBuilder(50000);
            //StringBuilder outputStr = new StringBuilder(50000);
            if (post.tradiNumber == "4401")
            {
                strJson = strJson.Replace("hcv_ab", "hcv-ab");
                strJson = strJson.Replace("hiv_ab", "hiv-ab");
                strJson = strJson.Replace("null", @"""0""");
            }
            //保存日志记事本文件 在D盘
            AppLogger.Info("请求交易业务代码：" + post.tradiNumber);
            AppLogger.Info("请求交易入参：" + strJson);
            string url = GetPubWay(post.tradiNumber);
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            string outputText = string.Empty;
            int isSucess = -1;
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
                string nonce = Guid.NewGuid().ToString();
                long times = (int)(DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))).TotalSeconds;
                //string token = "d6Ad0P1B3z6Dsdst0gYAFPlz8YlIvFDx";

                string signature = DigestUtils.Sha256Hex(times + token + nonce + times);
                //request.Headers.Add("x-tif-paasid", paasid);
                //request.Headers.Add("x-tif-signature", signature);
                //request.Headers.Add("x-tif-timestamp", times.ToString());
                //request.Headers.Add("x-tif-nonce", nonce);
                request.Headers.Add("apikey", "123456");
                AppLogger.Info("请求交易header入参：" + url + @"
            " + request.Headers.ToString());
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
					
					if (post.tradiNumber == "9102")
					{
						JObject inputJson = JObject.Parse(strJson);
						string filename = inputJson["input"]["fsDownloadIn"]["filename"].ToString();
						string dicname = System.Text.RegularExpressions.Regex.Replace(filename, @".zip", "");
						MemoryStream memoryStream = new MemoryStream();
						byte[] buffer = new byte[400960];
						int i;
						//将字节逐个放入到Byte中
						while ((i = responseStm.Read(buffer, 0, buffer.Length)) > 0)
						{
							memoryStream.Write(buffer, 0, i);
						}
						byte[]  result = memoryStream.ToArray();//文件流Byte，需要文件流可直接return，不需要下面的保存代
						BytesToFile(result, filename);//下载文件到指定目录并解压
						string readjson = ReadToFile(dicname);

						var nn = new { infcode = "0", inf_refmsgid = "", refmsg_time = "", respond_time = "", enctype = "", signtype = "", err_msg = "", output = JObject.Parse(readjson) };
						outputText = JsonConvert.SerializeObject(nn);
					}
					else
					{
						StreamReader redStm = new StreamReader(responseStm, Encoding.UTF8);
						outputText = redStm.ReadToEnd();
					}
                }
                isSucess = 0;
            }
            catch (Exception ex)
            {
                AppLogger.Info("post请求异常：" + ex.Message + ex.InnerException);
                var nn = new { infcode = "-1", inf_refmsgid = "", refmsg_time = "", respond_time = "", enctype = "", signtype = "", err_msg = "本地模拟医保返回异常: " + ex.Message };
                outputText=JsonConvert.SerializeObject(nn);
                //throw ex;
            }

            #region 测试
            string test = "";
            switch (post.tradiNumber)
            {
                case "1162":
                    test = "{\"message\":\"请使用医保电子凭证二维码\",\"success\":false,\"code\":500159}";
                    //test = "{\"code\":0,\"type\":\"success\",\"message\":\"成功\",\"data\":{\"userName\":\"王正\",\"idNo\":\"13030419841122***\",\"idType\":\"01\",\"insuOrg\":\"130300\",\"ecIndexNo\":\"86F43F8EDCA9C98866A32CC65202B057\",\"ecToken\":\"130000ecmecib288580f0f1a0a0000e7328251\",\"gender\":null,\"birthday\":null,\"nationality\":null,\"email\":null,\"chnlId\":\"1DBHJRS5I01E0F34A8C00000B959FA87\",\"ecQrCode\":\"4243754826492874240001130000\",\"authNo\":null}}";
                    break;
                case "5301":
                    test = "{\"infcode\":0,\"inf_refmsgid\":\"500000202107141108170000514555\",\"refmsg_time\":\"20210713230809297\",\"respond_time\":\"20210713230810282\",\"enctype\":\"\",\"signtype\":\"\",\"err_msg\":null,\"output\":{\"feedetail\":[{\"begndate\":\"2020-10-01\",\"enddate\":\"2120-11-01\",\"opsp_dise_name\":\"高血压\",\"exp_content\":null,\"opsp_dise_code\":\"M03900\"}]}}";
                    break;
                case "2207":
                    test = "{\"infcode\":0,\"inf_refmsgid\":\"500000202107141108170000514555\",\"refmsg_time\":\"20210713230809297\",\"respond_time\":\"20210713230810282\",\"enctype\":\"\",\"signtype\":\"\",\"err_msg\":null,\"output\":{\"setlinfo\":{\"setl_time\":\"2021-11-29 18:09:52\",\"cvlserv_pay\":0,\"cvlserv_flag\":\"0\",\"med_type\":\"11\",\"brdy\":\"1986-10-04\",\"naty\":\"01\",\"psn_cash_pay\":0,\"certno\":\"130302198610041614\",\"hifmi_pay\":0,\"psn_no\":\"13030011303013321217\",\"act_pay_dedc\":4.29,\"mdtrt_cert_type\":\"02\",\"balc\":1373.63,\"medins_setl_id\":\"H13110200026202111291809520478\",\"psn_cert_type\":\"01\",\"acct_mulaid_pay\":0,\"clr_way\":\"1\",\"hifob_pay\":0,\"oth_pay\":0,\"medfee_sumamt\":4.52,\"hifes_pay\":0,\"gend\":\"1\",\"mdtrt_id\":\"300179916\",\"acct_pay\":4.52,\"fund_pay_sumamt\":0,\"fulamt_ownpay_amt\":0,\"hosp_part_amt\":0,\"setl_id\":\"400125241\",\"inscp_scp_amt\":4.29,\"insutype\":\"310\",\"maf_pay\":0,\"psn_name\":\"雷贺姜\",\"psn_part_amt\":4.52,\"clr_optins\":\"131199\",\"pool_prop_selfpay\":0.0,\"psn_type\":\"1101\",\"hifp_pay\":0,\"overlmt_selfpay\":0,\"preselfpay_amt\":0.23,\"age\":35.0,\"clr_type\":\"11\"},\"setldetail\":[{\"fund_pay_type\":\"310200\",\"fund_payamt\":4.52,\"setl_proc_info\":null,\"crt_payb_lmt_amt\":null,\"inscp_scp_amt\":4.29,\"fund_pay_type_name\":\"城镇职工基本医疗保险个人账户基金\"}],\"expContent\":null}}";
                    break;
                case "2206":
                    test = "{\"infcode\":0,\"inf_refmsgid\":\"500000202107141108170000514555\",\"refmsg_time\":\"20210713230809297\",\"respond_time\":\"20210713230810282\",\"enctype\":\"\",\"signtype\":\"\",\"err_msg\":null,\"output\":{\"setlinfo\":{\"setl_time\":null,\"cvlserv_pay\":0,\"cvlserv_flag\":\"0\",\"med_type\":\"11\",\"brdy\":\"1986-10-04\",\"naty\":\"01\",\"psn_cash_pay\":0,\"certno\":\"130302198610041614\",\"hifmi_pay\":0,\"psn_no\":\"13030011303013321217\",\"act_pay_dedc\":4.29,\"mdtrt_cert_type\":\"02\",\"balc\":1373.63,\"medins_setl_id\":\"H13110200026202111291809516465\",\"psn_cert_type\":\"01\",\"acct_mulaid_pay\":0,\"clr_way\":null,\"hifob_pay\":0,\"oth_pay\":0,\"medfee_sumamt\":4.52,\"hifes_pay\":0,\"gend\":\"1\",\"mdtrt_id\":\"300179916\",\"acct_pay\":4.52,\"fund_pay_sumamt\":0,\"fulamt_ownpay_amt\":0,\"hosp_part_amt\":0,\"setl_id\":null,\"inscp_scp_amt\":4.29,\"insutype\":\"310\",\"maf_pay\":0,\"psn_name\":\"雷贺姜\",\"psn_part_amt\":4.52,\"clr_optins\":null,\"pool_prop_selfpay\":0.0,\"psn_type\":\"1101\",\"hifp_pay\":0,\"overlmt_selfpay\":0,\"preselfpay_amt\":0.23,\"age\":35.0,\"clr_type\":\"11\"},\"setldetail\":[{\"fund_pay_type\":\"310200\",\"fund_payamt\":4.52,\"setl_proc_info\":null,\"crt_payb_lmt_amt\":null,\"inscp_scp_amt\":4.29,\"fund_pay_type_name\":\"城镇职工基本医疗保险个人账户基金\"}],\"expContent\":null}}";
                    break;
                case "2204":
                    test = "{\"infcode\":0,\"inf_refmsgid\":\"500000202107141108170000514555\",\"refmsg_time\":\"20210713230809297\",\"respond_time\":\"20210713230810282\",\"enctype\":\"\",\"signtype\":\"\",\"err_msg\":null,\"output\":{\"result\":[{\"bas_medn_flag\":\"0\",\"med_chrgitm_type\":\"09\",\"det_item_fee_sumamt\":4.52,\"hi_nego_drug_flag\":\"0\",\"fulamt_ownpay_amt\":0,\"cnt\":1.0,\"pric\":4.52,\"exp_content\":null,\"memo\":null,\"feedetl_sn\":\"yp570\",\"inscp_scp_amt\":4.29,\"drt_reim_flag\":\"0\",\"overlmt_amt\":0.0,\"list_sp_item_flag\":null,\"pric_uplmt_amt\":999999,\"selfpay_prop\":0.05,\"chld_medc_flag\":null,\"preselfpay_amt\":0.23,\"lmt_used_flag\":\"0\",\"chrgitm_lv\":\"02\"}]}}";
                    break;
                case "2208":
                    test = "{\"infcode\":0,\"inf_refmsgid\":\"500000202107141108170000514555\",\"refmsg_time\":\"20210713230809297\",\"respond_time\":\"20210713230810282\",\"enctype\":\"\",\"signtype\":\"\",\"err_msg\":null,\"output\":{\"setlinfo\":{\"setl_time\":\"2021-11-26 11:41:26\",\"cvlserv_pay\":0.0,\"cvlserv_flag\":\"0\",\"med_type\":\"11\",\"brdy\":\"1986-10-04\",\"naty\":\"01\",\"psn_cash_pay\":0.0,\"certno\":\"130302198610041614\",\"hifmi_pay\":0.0,\"psn_no\":\"13030011303013321217\",\"act_pay_dedc\":-24.87,\"mdtrt_cert_type\":\"02\",\"balc\":1378.15,\"medins_setl_id\":\"H13110200026202111261147436094\",\"psn_cert_type\":\"01\",\"acct_mulaid_pay\":null,\"clr_way\":\"1\",\"hifob_pay\":0.0,\"oth_pay\":0.0,\"medfee_sumamt\":-25.1,\"hifes_pay\":0.0,\"gend\":\"1\",\"mdtrt_id\":\"300179916\",\"acct_pay\":-25.1,\"fund_pay_sumamt\":0.0,\"fulamt_ownpay_amt\":0.0,\"hosp_part_amt\":null,\"setl_id\":\"400120618\",\"inscp_scp_amt\":-24.87,\"insutype\":\"310\",\"maf_pay\":0.0,\"psn_name\":\"雷贺姜\",\"psn_part_amt\":null,\"clr_optins\":\"131199\",\"pool_prop_selfpay\":0.0,\"psn_type\":\"1101\",\"hifp_pay\":0.0,\"overlmt_selfpay\":0.0,\"preselfpay_amt\":-0.23,\"age\":35.0,\"clr_type\":\"11\"},\"setldetail\":[{\"fund_pay_type\":\"310200\",\"fund_payamt\":-25.1,\"setl_proc_info\":null,\"crt_payb_lmt_amt\":null,\"inscp_scp_amt\":-24.87,\"fund_pay_type_name\":\"城镇职工基本医疗保险个人账户基金\"}],\"expContent\":null}}";
                    break;
                case "2203A":
                    test = "{\"infcode\":0,\"inf_refmsgid\":\"500000202107141108170000514555\",\"refmsg_time\":\"20210713230809297\",\"respond_time\":\"20210713230810282\",\"enctype\":\"\",\"signtype\":\"\",\"err_msg\":null,}";
                    break;
                case "2201":
                    test = "{\"infcode\":0,\"inf_refmsgid\":\"500000202107141108170000514555\",\"refmsg_time\":\"20210713230809297\",\"respond_time\":\"20210713230810282\",\"enctype\":\"\",\"signtype\":\"\",\"err_msg\":null,\"output\":{\"data\":{\"psn_no\":\"13040011304012963751\",\"mdtrt_id\":\"300178725\",\"exp_content\":null,\"ipt_otp_no\":\"2111241810003\"}}}";
                    break;
                case "9001":
                    test = "{\"infcode\":0,\"inf_refmsgid\":\"500000202107141108170000514555\",\"refmsg_time\":\"20210713230809297\",\"respond_time\":\"20210713230810282\",\"enctype\":\"\",\"signtype\":\"\",\"err_msg\":null,\"output\":{\"signinoutb\":{\"sign_no\":\"359754\",\"sign_time\":\"2021-08-29 19:35:27\"}}}";
                    break;
                case "2401":
                    test = "{\"infcode\":-1,\"inf_refmsgid\":\"\",\"refmsg_time\":\"\",\"respond_time\":\"\",\"enctype\":\"\",\"signtype\":\"\",\"err_msg\":\"\"}";
                    break;
                case "1101":
                    //test = "{\"infcode\":0,\"inf_refmsgid\":\"500000202107141108170000514555\",\"refmsg_time\":\"20210713230809297\",\"respond_time\":\"20210713230810282\",\"enctype\":\"\",\"signtype\":\"\",\"err_msg\":null,\"output\":{\"idetinfo\":[],\"baseinfo\":{\"certno\":\"130304198406281517\",\"psn_no\":\"13030011303013067303\",\"gend\":\"1\",\"exp_content\":null,\"brdy\":\"1984-06-28\",\"naty\":\"11\",\"psn_cert_type\":\"01\",\"psn_name\":\"徐志鹏\",\"age\":37.5},\"insuinfo\":[{\"insuplc_admdvs\":\"130399\",\"psn_insu_date\":\"2008-01-01\",\"cvlserv_flag\":\"0\",\"balc\":26933.88,\"emp_name\":\"秦皇岛市医疗保险基金管理中心\",\"psn_type\":\"1101\",\"psn_insu_stas\":\"1\",\"insutype\":\"310\",\"paus_insu_date\":null},{\"insuplc_admdvs\":\"130399\",\"psn_insu_date\":\"2008-01-01\",\"cvlserv_flag\":\"0\",\"balc\":0,\"emp_name\":\"秦皇岛市医疗保险基金管理中心\",\"psn_type\":\"1102\",\"psn_insu_stas\":\"1\",\"insutype\":\"320\",\"paus_insu_date\":null},{\"insuplc_admdvs\":\"130399\",\"psn_insu_date\":\"2008-01-01\",\"cvlserv_flag\":\"0\",\"balc\":0,\"emp_name\":\"秦皇岛市医疗保险基金管理中心\",\"psn_type\":\"1101\",\"psn_insu_stas\":\"1\",\"insutype\":\"330\",\"paus_insu_date\":null},{\"insuplc_admdvs\":\"130399\",\"psn_insu_date\":\"2008-01-01\",\"cvlserv_flag\":\"0\",\"balc\":0,\"emp_name\":\"秦皇岛市医疗保险基金管理中心\",\"psn_type\":\"1101\",\"psn_insu_stas\":\"4\",\"insutype\":\"39999\",\"paus_insu_date\":\"2008-01-01\"},{\"insuplc_admdvs\":\"130399\",\"psn_insu_date\":\"2008-01-01\",\"cvlserv_flag\":\"0\",\"balc\":0,\"emp_name\":\"秦皇岛市医疗保险基金管理中心\",\"psn_type\":\"1101\",\"psn_insu_stas\":\"1\",\"insutype\":\"510\",\"paus_insu_date\":null},{\"insuplc_admdvs\":\"130302\",\"psn_insu_date\":\"2009-06-02\",\"cvlserv_flag\":\"0\",\"balc\":0.0,\"emp_name\":\"海港区社区\",\"psn_type\":\"1560\",\"psn_insu_stas\":\"1\",\"insutype\":\"390\",\"paus_insu_date\":null}]}}";
                    //test = "{\"infcode\":0,\"inf_refmsgid\":\"500000202107141108170000514555\",\"refmsg_time\":\"20210713230809297\",\"respond_time\":\"20210713230810282\",\"enctype\":\"\",\"signtype\":\"\",\"err_msg\":null,\"output\":{\"idetinfo\":[],\"baseinfo\":{\"certno\":\"220204195308122144\",\"psn_no\":\"13030081303300164452\",\"gend\":\"2\",\"exp_content\":null,\"brdy\":\"1953-08-12\",\"naty\":\"01\",\"psn_cert_type\":\"01\",\"psn_name\":\"姜淑芹\",\"age\":69.2},\"insuinfo\":[{\"insuplc_admdvs\":\"130302\",\"psn_insu_date\":\"2008-04-25\",\"cvlserv_flag\":\"0\",\"balc\":200.0,\"emp_name\":\"燕海里社区\",\"psn_type\":\"1560\",\"psn_insu_stas\":\"1\",\"insutype\":\"390\",\"paus_insu_date\":null}]}}";
                    //test = "{\"infcode\":0,\"inf_refmsgid\":\"500000202107141108170000514555\",\"refmsg_time\":\"20210713230809297\",\"respond_time\":\"20210713230810282\",\"enctype\":\"\",\"signtype\":\"\",\"err_msg\":null,\"output\":{\"idetinfo\":[],\"baseinfo\":{\"certno\":\"130302198610041614\",\"psn_no\":\"13030011303013321217\",\"gend\":\"1\",\"exp_content\":null,\"brdy\":\"1986-10-04\",\"naty\":\"01\",\"psn_cert_type\":\"01\",\"psn_name\":\"雷贺姜\",\"age\":35.1},\"insuinfo\":[{\"insuplc_admdvs\":\"130399\",\"psn_insu_date\":\"2015-06-01\",\"cvlserv_flag\":\"0\",\"balc\":1378.15,\"emp_name\":\"秦皇岛市第一医院\",\"psn_type\":\"1101\",\"psn_insu_stas\":\"1\",\"insutype\":\"310\",\"paus_insu_date\":null},{\"insuplc_admdvs\":\"130399\",\"psn_insu_date\":\"2015-06-01\",\"cvlserv_flag\":\"0\",\"balc\":0,\"emp_name\":\"秦皇岛市第一医院\",\"psn_type\":\"1101\",\"psn_insu_stas\":\"1\",\"insutype\":\"330\",\"paus_insu_date\":null},{\"insuplc_admdvs\":\"130399\",\"psn_insu_date\":\"2015-06-01\",\"cvlserv_flag\":\"0\",\"balc\":0,\"emp_name\":\"秦皇岛市第一医院\",\"psn_type\":\"1101\",\"psn_insu_stas\":\"4\",\"insutype\":\"39999\",\"paus_insu_date\":\"2015-06-01\"},{\"insuplc_admdvs\":\"130399\",\"psn_insu_date\":\"2015-06-01\",\"cvlserv_flag\":\"0\",\"balc\":0,\"emp_name\":\"秦皇岛市第一医院\",\"psn_type\":\"1101\",\"psn_insu_stas\":\"1\",\"insutype\":\"510\",\"paus_insu_date\":null}]}}";
                    test = "{\"output\":{\"idetinfo\":[],\"baseinfo\":{\"certno\":\"310103197307252018\",\"psn_no\":\"801874939\",\"gend\":\"1\",\"exp_content\":null,\"brdy\":\"1973-07-25\",\"naty\":\"99\",\"psn_cert_type\":\"01\",\"psn_name\":\"卜兆宏\",\"age\":50.2},\"insuinfo\":[{\"insuplc_admdvs\":\"310115\",\"psn_insu_date\":\"1900-01-01\",\"cvlserv_flag\":\"0\",\"balc\":41509.00,\"emp_name\":\"上海正德医院有限公司\",\"psn_type\":\"116032\",\"psn_insu_stas\":\"1\",\"insutype\":\"310\",\"paus_insu_date\":null}]},\"infcode\":0,\"warn_msg\":null,\"cainfo\":null,\"err_msg\":null,\"refmsg_time\":\"20230920182350513\",\"signtype\":null,\"respond_time\":\"20230920182350662\",\"inf_refmsgid\":\"310000202309201823500002516630\"}";

                    break;
                case "1161":

                    test = "{\"infcode\":0,\"inf_refmsgid\":\"500000202107141108170000514555\",\"refmsg_time\":\"20210713230809297\",\"respond_time\":\"20210713230810282\",\"enctype\":\"\",\"signtype\":\"\",\"err_msg\":null,\"output\":{\"idetinfo\":[],\"baseinfo\":{\"certno\":\"220204195308122145\",\"psn_no\":\"13030081303300164452\",\"gend\":\"2\",\"exp_content\":null,\"brdy\":\"1953-08-12\",\"naty\":\"01\",\"psn_cert_type\":\"01\",\"psn_name\":\"姜淑芹1\",\"age\":69.2},\"insuinfo\":[{\"insuplc_admdvs\":\"130302\",\"psn_insu_date\":\"2008-04-25\",\"cvlserv_flag\":\"0\",\"balc\":200.0,\"emp_name\":\"燕海里社区\",\"psn_type\":\"1560\",\"psn_insu_stas\":\"1\",\"insutype\":\"390\",\"paus_insu_date\":null}]}}";
                    // test = "{\"infcode\":\"0\",\"inf_refmsgid\":\"500000202107141108170000514555\",\"refmsg_time\":\"20210713230809297\",\"respond_time\":\"20210713230810282\",\"enctype\":\"\",\"signtype\":\"\",\"err_msg\":null,\"output\":{\"idetinfo\":[],\"baseinfo\":{\"certno\":\"512301194710010012\",\"psn_no\":\"ZG1004897052\",\"gend\":\"1\",\"brdy\":\"1950-06-16\",\"naty\":\"01\",\"psn_cert_type\":\"01\",\"psn_name\":\"杨昌合\",\"age\":71.2},\"cardecinfo\":{\"certno\":\"510921195006165775\",\"ecToken\":\"\",\"psn_cert_type\":\"01\",\"psn_name\":\"杨昌合\",\"cardno\":\"A58570797\",\"card_sn\":\"\"},\"insuinfo\":[{\"insuplc_admdvs\":\"500102\",\"psn_insu_date\":\"2016-11-10\",\"cvlserv_flag\":\"0\",\"balc\":0.0,\"emp_name\":\"抗建堂社区\",\"psn_type\":\"15\",\"psn_insu_stas\":\"1\",\"insutype\":\"310\"}]}}";
                    break;
                case "2402":
                    test = "{\"infcode\":0,\"inf_refmsgid\":\"500000202107141108170000514555\",\"refmsg_time\":\"20210713230809297\",\"respond_time\":\"20210713230810282\",\"enctype\":\"\",\"signtype\":\"\",\"err_msg\":null,\"output\":{}}";
                    break;
                case "2405":
                    test = "{\"infcode\":0,\"inf_refmsgid\":\"500000202107141108170000514555\",\"refmsg_time\":\"20210713230809297\",\"respond_time\":\"20210713230810282\",\"enctype\":\"\",\"signtype\":\"\",\"err_msg\":null,\"output\":{}}";
                    break;
                case "2304":
                    test = "{\"infcode\":0,\"inf_refmsgid\":\"500000202107141108170000514555\",\"refmsg_time\":\"20210713230809297\",\"respond_time\":\"20210713230810282\",\"enctype\":\"\",\"signtype\":\"\",\"err_msg\":null,\"output\":{\"setlinfo\":{\"setl_time\":\"2021-08-27 09:58:41\",\"cvlserv_pay\":0,\"cvlserv_flag\":\"0\",\"med_type\":\"21\",\"brdy\":\"1947-10-01\",\"naty\":\"01\",\"psn_cash_pay\":3982.02,\"certno\":\"512301194710010012\",\"hifmi_pay\":0,\"psn_no\":\"ZG1004897052\",\"act_pay_dedc\":0,\"mdtrt_cert_type\":\"03\",\"balc\":0.0,\"medins_setl_id\":\"H50010302473202108270958341146\",\"psn_cert_type\":\"01\",\"acct_mulaid_pay\":0,\"clr_way\":\"3\",\"hifob_pay\":18785.39,\"oth_pay\":0.0,\"medfee_sumamt\":23442.46,\"hifes_pay\":0,\"gend\":\"1\",\"mdtrt_id\":\"310003327436\",\"acct_pay\":170.04,\"fund_pay_sumamt\":18785.39,\"fulamt_ownpay_amt\":2533.97,\"hosp_part_amt\":505.01,\"setl_id\":\"300014497225\",\"inscp_scp_amt\":18808.16,\"insutype\":\"310\",\"maf_pay\":0,\"psn_name\":\"杨昌合\",\"psn_part_amt\":4152.06,\"clr_optins\":\"500103\",\"pool_prop_selfpay\":0.0,\"psn_type\":\"12\",\"hifp_pay\":0,\"overlmt_selfpay\":1396.89,\"preselfpay_amt\":703.44,\"age\":73.0,\"clr_type\":\"21\"},\"setldetail\":[{\"fund_pay_type\":\"330101\",\"fund_payamt\":18785.39,\"inscp_scp_amt\":18808.16},{\"fund_pay_type\":\"999996\",\"fund_payamt\":505.01,\"inscp_scp_amt\":18808.16},{\"fund_pay_type\":\"310201\",\"fund_payamt\":170.04,\"inscp_scp_amt\":18808.16}]}}";
                    break;
                case "2303":
                    test = "{\"infcode\":0,\"inf_refmsgid\":\"500000202107141108170000514555\",\"refmsg_time\":\"20210713230809297\",\"respond_time\":\"20210713230810282\",\"enctype\":\"\",\"signtype\":\"\",\"err_msg\":null,\"output\":{\"setlinfo\":{\"othfund_pay\":0.0,\"cvlserv_pay\":0,\"hifdm_pay\":0,\"insu_admdvs\":\"500102\",\"med_type\":\"21\",\"psn_pay\":4152.06,\"cvlserv_flag\":\"0\",\"mdtrt_cert_no\":\"A58570797\",\"brdy\":\"1947-10-01\",\"naty\":\"01\",\"psn_cash_pay\":3982.02,\"certno\":\"512301194710010012\",\"hifmi_pay\":0,\"psn_no\":\"ZG1004897052\",\"act_pay_dedc\":0,\"mdtrt_cert_type\":\"03\",\"balc\":0.0,\"medins_setl_id\":\"H50010302473202108270958272985\",\"psn_cert_type\":\"01\",\"acct_mulaid_pay\":0,\"hifob_pay\":18785.39,\"oth_pay\":0.0,\"ownpay_hosp_part\":0,\"inscp_amt\":18808.16,\"medfee_sumamt\":23442.46,\"hifes_pay\":0,\"cashPayamt\":3982.02,\"gend\":\"1\",\"mdtrt_id\":\"310003327436\",\"acct_pay\":170.04,\"fund_pay_sumamt\":18785.39,\"fulamt_ownpay_amt\":2533.97,\"dedc_std\":0,\"hosp_part_amt\":505.01,\"crt_dedc\":0,\"inscp_scp_amt\":18808.16,\"insutype\":\"310\",\"maf_pay\":0,\"psn_name\":\"杨昌合\",\"psn_part_amt\":4152.06,\"psn_insu_rlts_id\":\"ZG1004897052310\",\"pool_prop_selfpay\":0.0,\"psn_type\":\"12\",\"hifp_pay\":0,\"overlmt_selfpay\":1396.89,\"preselfpay_amt\":703.44,\"age\":73.0,\"clr_type\":\"21\"},\"setldetail\":[{\"fund_pay_type\":\"330101\",\"fund_payamt\":18785.39,\"inscp_scp_amt\":18808.16},{\"fund_pay_type\":\"999996\",\"fund_payamt\":505.01,\"inscp_scp_amt\":18808.16},{\"fund_pay_type\":\"310201\",\"fund_payamt\":170.04,\"inscp_scp_amt\":18808.16}]}}";
                    break;
                case "2305":
                    test = "{\"infcode\":0,\"inf_refmsgid\":\"500000202107141108170000514555\",\"refmsg_time\":\"20210713230809297\",\"respond_time\":\"20210713230810282\",\"enctype\":\"\",\"signtype\":\"\",\"err_msg\":null,\"output\":{\"setlinfo\":{\"othfund_pay\":0.0,\"setl_time\":\"2021-08-05 09:31:35\",\"cvlserv_pay\":0.0,\"hifdm_pay\":0.0,\"insu_admdvs\":\"500113\",\"psn_pay\":-1307.01,\"mdtrt_cert_no\":\"A86841005\",\"exp_content\":null,\"psn_cash_pay\":-1307.01,\"begndate\":\"2021-07-29\",\"hifmi_pay\":0.0,\"act_pay_dedc\":0.0,\"balc\":0.0,\"medins_setl_id\":\"H50010302473202108050931355556\",\"acct_mulaid_pay\":null,\"hifob_pay\":-2039.39,\"oth_pay\":0.0,\"ownpay_hosp_part\":-32.86,\"inscp_amt\":-2039.39,\"medfee_sumamt\":-3379.26,\"hifes_pay\":0.0,\"cashPayamt\":-1307.01,\"mdtrt_id\":\"310001913300\",\"acct_pay\":0.0,\"fund_pay_sumamt\":-2039.39,\"fulamt_ownpay_amt\":-956.34,\"dedc_std\":0.0,\"hosp_part_amt\":null,\"setl_id\":\"300001010540\",\"setl_type\":\"2\",\"crt_dedc\":0.0,\"inscp_scp_amt\":-2039.39,\"maf_pay\":0.0,\"invono\":null,\"psn_insu_rlts_id\":\"ZG1002443923310\",\"psn_part_amt\":null,\"enddate\":\"2021-08-05\",\"clr_optins\":\"500103\",\"pool_prop_selfpay\":0.0,\"hifp_pay\":0.0,\"overlmt_selfpay\":-186.31,\"preselfpay_amt\":-197.22},\"setldetail\":[{\"fund_pay_type\":\"330100\",\"fund_payamt\":-2039.39,\"setl_proc_info\":null,\"crt_payb_lmt_amt\":null,\"inscp_scp_amt\":-2039.39,\"fund_pay_type_name\":null},{\"fund_pay_type\":\"999997\",\"fund_payamt\":-32.86,\"setl_proc_info\":null,\"crt_payb_lmt_amt\":null,\"inscp_scp_amt\":-2039.39,\"fund_pay_type_name\":null}]}}";
                    break;
                case "2301":
                    test = "{\"infcode\":0,\"inf_refmsgid\":\"500000202107141108170000514555\",\"refmsg_time\":\"20210713230809297\",\"respond_time\":\"20210713230810282\",\"enctype\":\"\",\"signtype\":\"\",\"err_msg\":null,\"output\":{\"result\":[{\"bas_medn_flag\":\"0\",\"med_chrgitm_type\":\"14\",\"det_item_fee_sumamt\":6.0,\"hi_nego_drug_flag\":\"0\",\"fulamt_ownpay_amt\":0,\"cnt\":3.0,\"pric\":2.0,\"feedetl_sn\":\"XM5129673\",\"inscp_scp_amt\":6.0,\"drt_reim_flag\":\"0\",\"overlmt_amt\":0.0,\"pric_uplmt_amt\":2.0,\"selfpay_prop\":0.0,\"preselfpay_amt\":0.0,\"lmt_used_flag\":\"0\",\"chrgitm_lv\":\"01\"},{\"bas_medn_flag\":\"0\",\"med_chrgitm_type\":\"14\",\"det_item_fee_sumamt\":6.0,\"hi_nego_drug_flag\":\"0\",\"fulamt_ownpay_amt\":0,\"cnt\":3.0,\"pric\":2.0,\"feedetl_sn\":\"XM5129674\",\"inscp_scp_amt\":6.0,\"drt_reim_flag\":\"0\",\"overlmt_amt\":0.0,\"pric_uplmt_amt\":2.0,\"selfpay_prop\":0.0,\"preselfpay_amt\":0.0,\"lmt_used_flag\":\"0\",\"chrgitm_lv\":\"01\"},{\"bas_medn_flag\":\"0\",\"med_chrgitm_type\":\"14\",\"det_item_fee_sumamt\":1.0,\"hi_nego_drug_flag\":\"0\",\"fulamt_ownpay_amt\":0,\"cnt\":1.0,\"pric\":1.0,\"feedetl_sn\":\"XM5129675\",\"inscp_scp_amt\":1.0,\"drt_reim_flag\":\"0\",\"overlmt_amt\":0.0,\"pric_uplmt_amt\":1.0,\"selfpay_prop\":0.0,\"preselfpay_amt\":0.0,\"lmt_used_flag\":\"0\",\"chrgitm_lv\":\"01\"},{\"bas_medn_flag\":\"0\",\"med_chrgitm_type\":\"14\",\"det_item_fee_sumamt\":16.0,\"hi_nego_drug_flag\":\"0\",\"fulamt_ownpay_amt\":0,\"cnt\":2.0,\"pric\":8.0,\"feedetl_sn\":\"XM5129676\",\"inscp_scp_amt\":16.0,\"drt_reim_flag\":\"0\",\"overlmt_amt\":0.0,\"pric_uplmt_amt\":8.0,\"selfpay_prop\":0.0,\"preselfpay_amt\":0.0,\"lmt_used_flag\":\"0\",\"chrgitm_lv\":\"01\"},{\"bas_medn_flag\":\"0\",\"med_chrgitm_type\":\"14\",\"det_item_fee_sumamt\":1.5,\"hi_nego_drug_flag\":\"0\",\"fulamt_ownpay_amt\":1.5,\"cnt\":1.0,\"pric\":1.5,\"feedetl_sn\":\"XM5129935\",\"inscp_scp_amt\":0.0,\"drt_reim_flag\":\"0\",\"overlmt_amt\":0.0,\"pric_uplmt_amt\":999999,\"selfpay_prop\":1,\"preselfpay_amt\":0.0,\"lmt_used_flag\":\"0\",\"chrgitm_lv\":\"03\"},{\"bas_medn_flag\":\"0\",\"med_chrgitm_type\":\"14\",\"det_item_fee_sumamt\":1.5,\"hi_nego_drug_flag\":\"0\",\"fulamt_ownpay_amt\":1.5,\"cnt\":1.0,\"pric\":1.5,\"feedetl_sn\":\"XM5129936\",\"inscp_scp_amt\":0.0,\"drt_reim_flag\":\"0\",\"overlmt_amt\":0.0,\"pric_uplmt_amt\":999999,\"selfpay_prop\":1,\"preselfpay_amt\":0.0,\"lmt_used_flag\":\"0\",\"chrgitm_lv\":\"03\"},{\"bas_medn_flag\":\"0\",\"med_chrgitm_type\":\"14\",\"det_item_fee_sumamt\":3.0,\"hi_nego_drug_flag\":\"0\",\"fulamt_ownpay_amt\":0,\"cnt\":1.0,\"pric\":3.0,\"feedetl_sn\":\"XM5129937\",\"inscp_scp_amt\":3.0,\"drt_reim_flag\":\"0\",\"overlmt_amt\":0.0,\"pric_uplmt_amt\":3.0,\"selfpay_prop\":0.0,\"preselfpay_amt\":0.0,\"lmt_used_flag\":\"0\",\"chrgitm_lv\":\"01\"},{\"bas_medn_flag\":\"0\",\"med_chrgitm_type\":\"14\",\"det_item_fee_sumamt\":3.0,\"hi_nego_drug_flag\":\"0\",\"fulamt_ownpay_amt\":0,\"cnt\":1.0,\"pric\":3.0,\"feedetl_sn\":\"XM5129974\",\"inscp_scp_amt\":3.0,\"drt_reim_flag\":\"0\",\"overlmt_amt\":0.0,\"pric_uplmt_amt\":3.0,\"selfpay_prop\":0.0,\"preselfpay_amt\":0.0,\"lmt_used_flag\":\"0\",\"chrgitm_lv\":\"01\"},{\"bas_medn_flag\":\"0\",\"med_chrgitm_type\":\"14\",\"det_item_fee_sumamt\":1.5,\"hi_nego_drug_flag\":\"0\",\"fulamt_ownpay_amt\":1.5,\"cnt\":1.0,\"pric\":1.5,\"feedetl_sn\":\"XM5129975\",\"inscp_scp_amt\":0.0,\"drt_reim_flag\":\"0\",\"overlmt_amt\":0.0,\"pric_uplmt_amt\":999999,\"selfpay_prop\":1,\"preselfpay_amt\":0.0,\"lmt_used_flag\":\"0\",\"chrgitm_lv\":\"03\"},{\"bas_medn_flag\":\"0\",\"med_chrgitm_type\":\"14\",\"det_item_fee_sumamt\":1.5,\"hi_nego_drug_flag\":\"0\",\"fulamt_ownpay_amt\":1.5,\"cnt\":1.0,\"pric\":1.5,\"feedetl_sn\":\"XM5129976\",\"inscp_scp_amt\":0.0,\"drt_reim_flag\":\"0\",\"overlmt_amt\":0.0,\"pric_uplmt_amt\":999999,\"selfpay_prop\":1,\"preselfpay_amt\":0.0,\"lmt_used_flag\":\"0\",\"chrgitm_lv\":\"03\"},{\"bas_medn_flag\":\"0\",\"med_chrgitm_type\":\"14\",\"det_item_fee_sumamt\":1.5,\"hi_nego_drug_flag\":\"0\",\"fulamt_ownpay_amt\":1.5,\"cnt\":1.0,\"pric\":1.5,\"feedetl_sn\":\"XM5129977\",\"inscp_scp_amt\":0.0,\"drt_reim_flag\":\"0\",\"overlmt_amt\":0.0,\"pric_uplmt_amt\":999999,\"selfpay_prop\":1,\"preselfpay_amt\":0.0,\"lmt_used_flag\":\"0\",\"chrgitm_lv\":\"03\"},{\"bas_medn_flag\":\"0\",\"med_chrgitm_type\":\"14\",\"det_item_fee_sumamt\":2.0,\"hi_nego_drug_flag\":\"0\",\"fulamt_ownpay_amt\":0,\"cnt\":1.0,\"pric\":2.0,\"feedetl_sn\":\"XM5130524\",\"inscp_scp_amt\":2.0,\"drt_reim_flag\":\"0\",\"overlmt_amt\":0.0,\"pric_uplmt_amt\":2.0,\"selfpay_prop\":0.0,\"preselfpay_amt\":0.0,\"lmt_used_flag\":\"0\",\"chrgitm_lv\":\"01\"},{\"bas_medn_flag\":\"0\",\"med_chrgitm_type\":\"14\",\"det_item_fee_sumamt\":8.0,\"hi_nego_drug_flag\":\"0\",\"fulamt_ownpay_amt\":0,\"cnt\":1.0,\"pric\":8.0,\"feedetl_sn\":\"XM5150592\",\"inscp_scp_amt\":8.0,\"drt_reim_flag\":\"0\",\"overlmt_amt\":0.0,\"pric_uplmt_amt\":8.0,\"selfpay_prop\":0.0,\"preselfpay_amt\":0.0,\"lmt_used_flag\":\"0\",\"chrgitm_lv\":\"01\"},{\"bas_medn_flag\":\"0\",\"med_chrgitm_type\":\"14\",\"det_item_fee_sumamt\":36.0,\"hi_nego_drug_flag\":\"0\",\"fulamt_ownpay_amt\":0,\"cnt\":9.0,\"pric\":4.0,\"feedetl_sn\":\"XM5150593\",\"inscp_scp_amt\":36.0,\"drt_reim_flag\":\"0\",\"overlmt_amt\":0.0,\"pric_uplmt_amt\":4.0,\"selfpay_prop\":0.0,\"preselfpay_amt\":0.0,\"lmt_used_flag\":\"0\",\"chrgitm_lv\":\"01\"},{\"bas_medn_flag\":\"0\",\"med_chrgitm_type\":\"14\",\"det_item_fee_sumamt\":45.0,\"hi_nego_drug_flag\":\"0\",\"fulamt_ownpay_amt\":0,\"cnt\":9.0,\"pric\":5.0,\"feedetl_sn\":\"XM5150594\",\"inscp_scp_amt\":45.0,\"drt_reim_flag\":\"0\",\"overlmt_amt\":0.0,\"pric_uplmt_amt\":5.0,\"selfpay_prop\":0.0,\"preselfpay_amt\":0.0,\"lmt_used_flag\":\"0\",\"chrgitm_lv\":\"01\"},{\"bas_medn_flag\":\"0\",\"med_chrgitm_type\":\"14\",\"det_item_fee_sumamt\":31.5,\"hi_nego_drug_flag\":\"0\",\"fulamt_ownpay_amt\":0,\"cnt\":9.0,\"pric\":3.5,\"feedetl_sn\":\"XM5150595\",\"inscp_scp_amt\":31.5,\"drt_reim_flag\":\"0\",\"overlmt_amt\":0.0,\"pric_uplmt_amt\":3.5,\"selfpay_prop\":0.0,\"preselfpay_amt\":0.0,\"lmt_used_flag\":\"0\",\"chrgitm_lv\":\"01\"}]}}";
                    break;
                case "2302":
                    test = "{\"infcode\":0,\"inf_refmsgid\":\"500000202107141108170000514555\",\"refmsg_time\":\"20210713230809297\",\"respond_time\":\"20210713230810282\",\"enctype\":\"\",\"signtype\":\"\",\"err_msg\":null,\"output\":{}}";
                    break;
            }
            if (IsTest != 0)
            {
                outputText = test;
            }
            //int isSucess = 0;
            #endregion

            if (post.tradiNumber == "9102")
            {
                AppLogger.Info("请求交易出参：9102下载完成" );
            }
            else
            {
                AppLogger.Info("请求交易出参：" + outputText);
            }

            /*（待补充实现当BUSINESS_HANDLE函数返回-1时对某些交易的冲正处理）
             * -1－系统级别错误，HIS系统提示错误信息后，需要进行冲正等后续业务操作处理；
             * 因网络中断或超时等原因导致无法获取接收方状态，导致多方数据不一致或已确认接收方数据多时，
             * 可通过冲正取消接收方相应数据，保持双方数据一致。
             * 医疗机构可被冲正的交易包括：【2207】门诊结算、【2208】门诊结算撤销、
             * 【2304】住院结算、【2207】住院结算撤销、【2401】入院办理
             */

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
            catch(Exception ex)
            {
                if (isSucess == -1)
                {
                    return BuildReturnJson("-1", "JSON反序列化BUSINESS_HANDLE函数输出值失败：" + ex.Message + "，输出值：" + outputText);
                }
                else
                {
                    return BuildReturnJson(isSucess.ToString(), outputText);
                }
            }

            //保存交易日志到数据库
            AppLogger.SaveJyDbLog(post, getdate, input, strJson, outputJson, isSucess);

            //转换交易输出实体类
            if (post.inModel == 1 && outputJson.output != null)
            {
                OutModlem = JsonConvert.DeserializeObject<O>(Convert.ToString(outputJson.output));
            }

            //返回结果
            code = outputJson.infcode;
            if (post.tradiNumber == "9102" || post.tradiNumber == "9101")
            {
                return Convert.ToString(outputJson.output);
            }
            else
            {
                return outputText;
            }
        }
        /// <summary>
        /// 前置机请求地址
        /// </summary>
        /// <param name="tradiNumber"></param>
        /// <returns></returns>
        private static string GetPubWay(string tradiNumber)
        {
            string url = ybRequestPath;//"http://inner.getway.ylbzj.hebei.gov.cn/ebus/rio_fsi";
            if (!string.IsNullOrWhiteSpace(tradiNumber))
            {
                #region
                //switch (tradiNumber) {
                //    case "9001":
                //        url += "/fsi/api/signInSignOutService/signIn";
                //        break;
                //    //通过此交易上传文件。
                //    case "9101": 
                //        url += "/fsi/api/fileupload/upload";
                //        break;
                //    //通过此交易下载【1301-1319】目录信息下载、【5204】费用明细查询、【3202】医药机构费用结算对明细账交易生成的文件。
                //    case "9102": 
                //        url += "/fsi/api/fileupload/download";
                //        break;
                //    #region 目录下载
                //    //医保目录信息下载
                //    case "1312":
                //        url += "/fsi/api/catalogqueryservice/queryHilist";
                //        break;
                //    //西药中成药目录下载
                //    case "1301":
                //        url += "/fsi/api/catalogdownservice/downCatalog1301";
                //        break;
                //    //中药饮片目录下载
                //    case "1302":
                //        url += "/fsi/api/catalogdownservice/downCatalog1302";
                //        break;
                //    //医疗机构制剂目录下载
                //    case "1303":
                //        url += "/fsi/api/catalogdownservice/downCatalog1303";
                //        break;
                //    //民族药品目录下载
                //    case "1304":
                //        url += "/fsi/api/catalogqueryservice/queryNatyPreparedByPage";
                //        break;
                //    //医疗服务项目目录下载
                //    case "1305":
                //        url += "/fsi/api/catalogdownservice/downCatalog1305";
                //        break;
                //    //医用耗材目录下载
                //    case "1306":
                //        url += "/fsi/api/catalogdownservice/downCatalog1306";
                //        break;
                //    //疾病与诊断目录下载
                //    case "1307":
                //        url += "/fsi/api/catalogdownservice/downCatalog1307";
                //        break;
                //    //手术操作目录下载
                //    case "1308":
                //        url += "/fsi/api/catalogdownservice/downCatalog1308";
                //        break;
                //    //门诊慢特病种目录下载
                //    case "1309":
                //        url += "/fsi/api/catalogdownservice/downCatalog1309";
                //        break;
                //    //按病种付费病种目录下载
                //    case "1310":
                //        url += "/fsi/api/catalogdownservice/downCatalog1310";
                //        break;
                //    //日间手术治疗病种目录下载
                //    case "1311":
                //        url += "/fsi/api/catalogdownservice/downCatalog1311";
                //        break;
                //    //肿瘤形态学目录下载
                //    case "1313":
                //        url += "/fsi/api/catalogdownservice/downCatalog1313";
                //        break;
                //    //中医疾病目录下载
                //    case "1314":
                //        url += "/fsi/api/catalogdownservice/downCatalog1314";
                //        break;
                //    //中医证候目录下载
                //    case "1315":
                //        url += "/fsi/api/catalogdownservice/downCatalog1315";
                //        break;
                //    //医疗目录与医保目录匹配信息下载
                //    case "1316":
                //        url += "/fsi/api/catalogqueryservice/queryMedinsHilistMapByPage";
                //        break;
                //    //医药机构目录匹配信息下载
                //    case "1317":
                //        url += "/fsi/api/catalogqueryservice/queryMedListMapByPage";
                //        break;
                //    //医保目录限价信息下载
                //    case "1318":
                //        url += "/fsi/api/catalogqueryservice/queryLmtprcByPage";
                //        break;
                //    //医保目录先自付比例信息下载
                //    case "1319":
                //        url += "/fsi/api/catalogqueryservice/querySelfpayByPage";
                //        break;
                //    //其他信息-字典表查询
                //    case "1901":
                //        url += "/fsi/api/catalogqueryservice/queryDataDic";
                //        break;
                //    #endregion
                //    //人员基本信息获取
                //    case "1101":
                //        url += "/fsi/api/fsiPsnInfoService/queryPsnInfo";
                //        break;
                //    //电子凭证
                //    case "1162":
                //        url += "/fsi/api/hsecfc/localQrCodeQuery";
                //        break;
                //    //医药机构信息获取
                //    case "1201":
                //        url += "/fsi/api/fsiFixMedInsService/queryFixMedIns";
                //        break;
                //    //人员待遇享受检查
                //    case "2001":
                //        url += "/fsi/api/fsiPsnPriorityInfoService/queryPsnPriorityInfo";
                //        break;
                //    //门诊挂号
                //    case "2201":
                //        url += "/fsi/api/outpatientDocInfoService/outpatientRregistration";
                //        break;
                //    //门诊挂号撤销
                //    case "2202":
                //        url += "/fsi/api/outpatientDocInfoService/outpatientRegistrationCancel";
                //        break;
                //    //门诊就诊信息上传
                //    case "2203":
                //        url += "/fsi/api/outpatientDocInfoService/outpatientMdtrtinfoUp";
                //        break;
                //    //门诊就诊信息上传A
                //    case "2203A":
                //        url += "/fsi/api/outpatientDocInfoService/outpatientMdtrtinfoUpA";
                //        break;
                //    //门诊费用明细信息上传
                //    case "2204":
                //        url += "/fsi/api/outpatientDocInfoService/outpatientFeeListUp";
                //        break;
                //    //门诊费用明细信息撤销
                //    case "2205":
                //        url += "/fsi/api/outpatientDocInfoService/outpatientFeeListUpCancel";
                //        break;
                //    //门诊预结算
                //    case "2206":
                //        url += "/fsi/api/outpatientSettleService/preSettletment";
                //        break;
                //    case "2206A":
                //        url += "/fsi/api/outpatientSettleService/preSettletmentA";
                //        break;
                //    //门诊结算
                //    case "2207":
                //        url += "/fsi/api/outpatientSettleService/saveSettletmentV2";
                //        break;
                //    case "2207A":
                //        url += "/fsi/api/outpatientSettleService/saveSettletment";
                //        break;
                //    //门诊结算撤销
                //    case "2208":
                //        url += "/fsi/api/outpatientSettleService/cancleSettletment";
                //        break;
                //    //住院费用明细上传
                //    case "2301":
                //        url += "/fsi/api/hospFeeDtlService/feeDtlUp";
                //        break;
                //    //住院费用明细撤销
                //    case "2302":
                //        url += "/fsi/api/hospFeeDtlService/feeDtlCl";
                //        break;
                //    //住院预结算
                //    case "2303":
                //        url += "/fsi/api/hospSettService/preSett";
                //        break;
                //    //住院结算（验密）
                //    case "2304":
                //        url += "/fsi/api/hospSettService/settV2";
                //        break;
                //    //住院结算撤销
                //    case "2305":
                //        url += "/fsi/api/hospSettService/settCl";
                //        break;
                //    //入院办理
                //    case "2401":
                //        url += "/fsi/api/hospitalRegisterService/hospitalRegisterSave";
                //        break;
                //    //出院办理
                //    case "2402":
                //        url += "/fsi/api/dscgService/dischargeProcess";
                //        break;
                //    //住院信息变更
                //    case "2403":
                //        url += "/fsi/api/hospitalRegisterService/hospitalRegisterEdit";
                //        break;
                //    //入院撤销
                //    case "2404":
                //        url += "/fsi/api/hospitalRegisterService/hospitalRegisterCancel";
                //        break;
                //    //出院撤销
                //    case "2405":
                //        url += "/fsi/api/dscgService/dischargeUndo";
                //        break;
                //    //冲正交易
                //    case "2601":
                //        url += "/fsi/api/reverseService/revsMethod";
                //        break;
                //    //电子凭证
                //    case "9999":
                //        url += "/fsi/api/hsecfc/localQrCodeQuery";
                //        break;
                //    //明细审核-事前明细审核分析服务
                //    case "3101":
                //        url += "/fsi/api/riskConService/beforeAnalyze";
                //        break;
                //    //明细审核-事中明细审核分析服务
                //    case "3102":
                //        url += "/fsi/api/riskConService/courseAnalyze";
                //        break;
                //    //商品盘存上传
                //    case "3501":
                //        url += "/fsi/api/goodsService/goodsUpload";
                //        break;
                //    //商品库存变更
                //    case "3502":
                //        url += "/fsi/api/goodsService/goodsUpdate";
                //        break;
                //    //商品采购
                //    case "3503":
                //        url += "/fsi/api/goodsService/goodsBuy";
                //        break;
                //    //商品采购退货
                //    case "3504":
                //        url += "/fsi/api/goodsService/goodsBuyReturn";
                //        break;
                //    //商品销售
                //    case "3505":
                //        url += "/fsi/api/goodsService/goodsSell";
                //        break;
                //    //商品销售退货
                //    case "3506":
                //        url += "/fsi/api/goodsService/goodsSellReturn";
                //        break;
                //    //商品信息删除
                //    case "3507":
                //        url += "/fsi/api/goodsService/goodsInfoDelete";
                //        break;
                //    #region 查询类
                //    //医药机构费用结算对总账
                //    case "3201":
                //        url += "/fsi/api/ybSettlementStmtService/stmtTotal";
                //        break;
                //    //医药机构费用结算对明细账
                //    case "3202":
                //        url += "/fsi/api/ybSettlementStmtService/stmtDetail";
                //        break;
                //    //医药机构费用结算日对账
                //    case "3211":
                //        url += "/api/fsiextend/medinsmonsettlement/medinsDaySetlReconciliation";
                //        break;
                //    //医药机构费用结算日对账
                //    case "3212":
                //        url += "/api/fsiextend/medinsmonsettlement/qMedinsDaySetlResult";
                //        break;
                //    //目录对照上传
                //    case "3301":
                //        url += "/fsi/api/catalogCompService/catalogCompUp";
                //        break;
                //    //医药机构费用结算对明细账
                //    case "3302":
                //        url += "/fsi/api/ybSettlementStmtService/stmtDetail";
                //        break;
                //    //科室信息上传
                //    case "3401":
                //        url += "/fsi/api/department/saveDepartmentManageInfo";
                //        break;
                //    //批量科室信息上传
                //    case "3401A":
                //        url += "/fsi/api/department/saveDepartmentManageBatch";
                //        break;
                //    //科室信息变更
                //    case "3402":
                //        url += "/fsi/api/department/modifyDepartmentInfo";
                //        break;
                //    //科室信息撤销
                //    case "3403":
                //        url += "/fsi/api/department/pauseDepartmentInfo";
                //        break;
                //    //医疗保障基金结算清单信息上传
                //    case "4101":
                //        url += "/fsi/api/medinfoupload/setllistinfoupld";
                //        break;
                //    //医疗保障基金结算清单信息上传V2
                //    case "4101A":
                //        url += "/fsi/api/medinfoupload/setllistinfoupldA";
                //        break;
                //    //医疗保障基金结算清单质控结果查询
                //    case "4104":
                //        url += "/fsi/api/medinfoupload/queryQltctrlProblemPage";
                //        break;
                //    //结算信息查询
                //    case "5203":
                //        url += "/fsi/api/fsiIntegratedQueryService/querySetlInfo";
                //        break;
                //    //费用明细查询
                //    case "5204":
                //        url += "/fsi/api/fsiIntegratedQueryService/queryFeeList";
                //        break;
                //    //人员累计信息查询
                //    case "5206":
                //        url += "/fsi/api/fsiIntegratedQueryService/queryFixmedinsPracPsnSum";
                //        break;
                //    //在院信息查询
                //    case "5303":
                //        url += "/fsi/api/fsiIntegratedQueryService/queryInhospInfo";
                //        break;
                //    //科室信息查询
                //    case "5101":
                //        url += "/fsi/api/fsiIntegratedQueryService/queryFixmedinsDept";
                //        break;
                //    //医执人员信息查询
                //    case "5102":
                //        url += "/fsi/api/fsiIntegratedQueryService/queryFixmedinsPracPsn";
                //        break;
                //    //门诊慢特病
                //    case "5301":
                //        url += "/fsi/api/fsiIntegratedQueryService/queryPsnOpspReg";

                //        break;
                //        #endregion
                //        ////
                //        //case "2301":
                //        //    url += "";
                //        //    break;
                //        ////
                //        //case "2301":
                //        //    url += "";
                //        //    break;
                //        ////
                //        //case "2301":
                //        //    url += "";
                //        //    break;
                //        ////
                //        //case "2301":
                //        //    url += "";
                //        //    break;

                //}
                #endregion
                return url;
            }
            else
            {
                return "";
            }
        }
        public static HttpWebResponse YBHttpRequest(string tradiNumber, string requeststr)
        {
            string url = GetPubWay(tradiNumber);
            HttpWebRequest request = null;
            HttpWebResponse response = null;
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
                string nonce = Guid.NewGuid().ToString();
                long times = (int)(DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))).TotalSeconds;
                //string token = "d6Ad0P1B3z6Dsdst0gYAFPlz8YlIvFDx";

                string signature = DigestUtils.Sha256Hex(times + token + nonce + times);
                request.Headers.Add("x-tif-paasid", paasid);
                request.Headers.Add("x-tif-signature", signature);
                request.Headers.Add("x-tif-timestamp", times.ToString());
                request.Headers.Add("x-tif-nonce", nonce);
//                AppLogger.Info("请求交易header入参：" + url + @"
//" + request.Headers.ToString());
                byte[] bytepostData = System.Text.Encoding.Default.GetBytes(requeststr);

                //发送数据  using结束代码段释放
                using (Stream requestStm = request.GetRequestStream())
                {
                    requestStm.Write(bytepostData, 0, bytepostData.Length);
                }

                //响应
                response = (HttpWebResponse)request.GetResponse();
                AppLogger.Info("response.ContentLength" + response.ContentLength);
                return response;
            }
            catch (Exception ex)
            {
                AppLogger.Info("Http请求异常：" + ex.Message + ex.InnerException);
                return response;
            }

        }

        public static string YBResponse(string tradiNumber, string requeststr)
        {
            var resp = YBHttpRequest(tradiNumber, requeststr);
            string outputText = "";
            using (Stream responseStm = resp.GetResponseStream())
            {
                StreamReader redStm = new StreamReader(responseStm, Encoding.UTF8);
                outputText = redStm.ReadToEnd();
            }
            return outputText;
        }

        #region 文件下载
        public static string Asyncrequeststr = "";
        /// <summary>
        /// 文件异步下载
        /// </summary>
        /// <param name="InModle"></param>
        /// <param name="post"></param>
        /// <param name="tradiNumber"></param>
        /// <param name="flag">true 返回入参 false 异步下载</param>
        /// <returns></returns>
        public static string Download_9102(Input_9102 InModle, PostBase post,string tradiNumber,bool flag=false)
        {
            #region test初始化 
            //签到
            if (post.tradiNumber != "9001")
            {
                string sns = DoSigin(post.operatorId, post.operatorName);
                if (!string.IsNullOrEmpty(sns))
                {
                    //return sns;
                }                
            }
            #endregion
            DateTime getdate = Convert.ToDateTime(ClassSqlHelper.GetServerTime().ToString("yyyy-MM-dd HH:mm:ss"));

            InputPost1<InputBase> input = new InputPost1<InputBase>();
            input.input = new InputBase();
            input.input = InModle;
            SettingsBase(post, getdate, input);

            JObject baseJson = JObject.Parse(JsonConvert.SerializeObject(input));
            Asyncrequeststr = Convert.ToString(baseJson);

            if (flag)
            {
                return Asyncrequeststr;
            }
            string resp = "";
            AppLogger.Info("9201下载目录：["+tradiNumber.ToString()+"]"+ Asyncrequeststr);
            YBHttpRequestAsync(tradiNumber,InModle,out resp);
            return resp;
        }

        public static bool GetFilePath(string menucode,out string dir )
        {
            bool flag = false;
            dir = "";
            //判断路径是否正确
            if (string.IsNullOrEmpty(downloadfilepath))
            {
                AppLogger.Info("9102下载完成,路径异常");
            }
            else
            {
                try //确保目录存在
                {
                    dir = downloadfilepath + menucode + @"\";
                    if (!Directory.Exists(dir))
                    {
                        try
                        {
                            Directory.CreateDirectory(dir);
                            flag = true;
                        }
                        catch (Exception ex)
                        {
                            AppLogger.Info("9102下载完成,创建文件夹失败" + ex.Message);
                        }
                    }
                    else
                        flag = true;
                }
                catch (Exception ex)
                {
                    AppLogger.Info("9102下载完成,生成文件信息对象失败" + ex.Message);
                }
            }            
            return flag;
        }


        #endregion

        #region Http异步

        /// <summary>
        /// http 请求
        /// </summary>
        /// <param name="tradiNumber"></param>
        /// <param name="requeststr"></param>
        /// <returns></returns>
        public static void YBHttpRequestAsync(string muluNumber, Input_9102 InModle,out string resp)
        {
            resp = "";
            string url = GetPubWay("9102");
            HttpWebRequest request = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "Post";
                request.ContentType = "text/plain";
                //request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
                request.AllowAutoRedirect = true;
                request.CookieContainer = null;//获取验证码时候获取到的cookie会附加在这个容器里面
                request.KeepAlive = true;//建立持久性连接

                string nonce = Guid.NewGuid().ToString();
                long times = (int)(DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))).TotalSeconds;

                string signature = DigestUtils.Sha256Hex(times + token + nonce + times);
                request.Headers.Add("x-tif-paasid", paasid);
                request.Headers.Add("x-tif-signature", signature);
                request.Headers.Add("x-tif-timestamp", times.ToString());
                request.Headers.Add("x-tif-nonce", nonce);
//                AppLogger.Info("请求交易header入参：" + url + @"
//" + request.Headers.ToString());

                if (string.IsNullOrWhiteSpace(Asyncrequeststr))
                {
                    AppLogger.Info("Http请求异常：入参不可为空");
                    return;
                }

                byte[] bytepostData = System.Text.Encoding.UTF8.GetBytes(Asyncrequeststr);
                //发送数据  using结束代码段释放
                using (Stream requestStm = request.GetRequestStream())
                {
                    requestStm.Write(bytepostData, 0, bytepostData.Length);
                }

                string loadpath = "";
                GetFilePath(muluNumber, out loadpath);
                //设置下载相关参数
                HttpRequestDto requestState = new HttpRequestDto();
                requestState.BUFFER_SIZE = 1024*1024;
                requestState.BufferRead = new byte[requestState.BUFFER_SIZE];
                requestState.Request = request;
                requestState.SavePath = Path.Combine(loadpath, InModle.fsDownloadIn.filename);
                requestState.FileStream = new FileStream(requestState.SavePath, FileMode.OpenOrCreate);

                AppLogger.Info("文件下载路径：" + requestState.SavePath);

                //开始异步请求资源
                AppLogger.Info("开始异步请求资源Begin：");
                request.BeginGetResponse(new AsyncCallback(ResponseCallback), requestState);
                resp = requestState.SavePath;
            }
            catch (Exception ex)
            {
                AppLogger.Info("Http请求异常：" + ex.Message + ex.InnerException);
            }

        }

        /// <summary>
        /// 请求资源方法的回调函数
        /// </summary>
        /// <param name="asyncResult">用于在回调函数当中传递操作状态</param>
        private static void ResponseCallback(IAsyncResult asyncResult)
        {
            HttpRequestDto requestState = (HttpRequestDto)asyncResult.AsyncState;
            requestState.Response = (HttpWebResponse)requestState.Request.EndGetResponse(asyncResult);

            Stream responseStream = requestState.Response.GetResponseStream();
            requestState.ResponseStream = responseStream;

            //开始异步读取流
            AppLogger.Info("开始异步读取流:" + requestState.BufferRead.Length);
            responseStream.BeginRead(requestState.BufferRead, 0, requestState.BufferRead.Length, ReadCallback, requestState);
        }

        /// <summary>
        /// 异步读取流的回调函数
        /// </summary>
        /// <param name="asyncResult">用于在回调函数当中传递操作状态</param>
        private static void ReadCallback(IAsyncResult asyncResult)
        {
            HttpRequestDto requestState = (HttpRequestDto)asyncResult.AsyncState;
            int read = requestState.ResponseStream.EndRead(asyncResult);
            AppLogger.Info("开始异步读取流:read:" + read.ToString());
            if (read > 0)
            {
                //将缓冲区的数据写入该文件流
                requestState.FileStream.Write(requestState.BufferRead, 0, read);
                //AppLogger.Info("将缓冲区的数据写入该文件流::" + System.Text.Encoding.UTF8.GetString(requestState.BufferRead));

                //开始异步读取流
                requestState.ResponseStream.BeginRead(requestState.BufferRead, 0, requestState.BufferRead.Length, ReadCallback, requestState);
            }
            else
            {
                requestState.Response.Close();
                requestState.FileStream.Close();
            }
        }
		#endregion


		public static MemoryStream StreamToBytes(Stream stream)
		{
			MemoryStream outstream = new MemoryStream();
			const int bufferLen = 400960;
			byte[] buffer = new byte[400960];
			int count = 0;
			while ((count = stream.Read(buffer, 0, 400960)) > 0)//Read和Write会偏移所以offset不需要变
			{
				outstream.Write(buffer, 0, count);
			}
			return outstream;

		}

		public static void BytesToFile(byte[] buff, string filename)
		{

			string dicName = $"{ System.AppDomain.CurrentDomain.BaseDirectory}YiBao_Dow";

			if (!System.IO.Directory.Exists(dicName))
			{
				System.IO.Directory.CreateDirectory(dicName);
			}
			//写入文件，方式二
			System.IO.File.WriteAllBytes($"{dicName}//{filename}", buff);

			string dicname = System.Text.RegularExpressions.Regex.Replace(filename, @".zip", "");
			//解压文件
			System.IO.Compression.ZipFile.ExtractToDirectory($"{dicName}//{filename}", $"{dicName}//{dicname}"); //解压
			//System.IO.FileStream fs = new System.IO.FileStream($"{dicName}//{filename}.zip", System.IO.FileMode.CreateNew);
			//System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
			//bw.Write(buff, 0, buff.Length);
			//bw.Close();
			//fs.Close();
		}

		public static string ReadToFile(string filename)
		{
			string dicName = $"{ System.AppDomain.CurrentDomain.BaseDirectory}YiBao_Dow";
			if (!System.IO.Directory.Exists(dicName))
			{
				return "";
			}
			string[] readtxtdata = System.IO.File.ReadAllLines($"{dicName}\\{filename}\\result.txt", Encoding.UTF8);
			List<result> results = new List<result>();

			try
			{
				for (int i = 0; i < readtxtdata.Length; i++)
				{
					string[] row = readtxtdata[i].Split('	');
					result rowlist = new result();
					rowlist.psn_no = row[0];
					rowlist.mdtrt_id = row[1];
					rowlist.setl_id = row[2];
					rowlist.msgid = row[3];
					rowlist.stmt_rslt = row[4];
					rowlist.refd_setl_flag = row[5];
					rowlist.memo = row[6];
					rowlist.medfee_sumamt = Convert.ToDecimal(row[7]);
					rowlist.fund_pay_sumamt = Convert.ToDecimal(row[8]);
					rowlist.acct_pay = Convert.ToDecimal(row[9]);
					results.Add(rowlist);
				}
			}
			catch (Exception ex)
			{

				return ex.Message;
			}


			string jsonData = "{ result:" + JsonConvert.SerializeObject(results) + "}";
			//string refstr = results.ToString();
			return jsonData;
		}


	}

}