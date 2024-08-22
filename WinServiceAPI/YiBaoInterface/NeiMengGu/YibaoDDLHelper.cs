﻿using CQYiBaoInterface.Models.Post;
using CQYiBaoInterface.Models.NeiMengGu;
using CQYiBaoInterface.Models.NeiMengGu.Input;
using CQYiBaoInterface.Models.NeiMengGu.Output;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YiBaoInterface;
using CQYiBaoInterface.Models.ShangHai;

namespace CQYiBaoInterface.NeiMengGu

{
    public static class YibaoDDLHelper
    {
        static YibaoDDLHelper()
        {
            string is_test = System.Configuration.ConfigurationManager.AppSettings["is_test"];
            if (is_test == "1")
            {
                IsTest = 1;
            }
            middlegroundUrl = System.Configuration.ConfigurationManager.AppSettings["middleground_url"];
        }

        [DllImport("NationECCode.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr NationEcTrans(string strUrl, string pindata, StringBuilder poutdata);


        /// <summary>
        /// 是否医保本地模拟测试 1本地测试 0正式医保
        /// </summary>
        public static int IsTest = 0;
        /// <summary>
        /// 是否医保本地模拟测试 1本地测试 0正式医保
        /// </summary>
        public static string middlegroundUrl = "";
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

        public static string SaveToInterface<O>(NMGInputPost input, out O OutModlem, PostBase post, out int code)
         where O : OutputBase
        {

            AppLogger.Info("进入NationECCode-SaveToInterface");
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
                 
                    IntPtr returnValue = NationEcTrans(middlegroundUrl, sendMsg, rcvMsg);
                    returnValueText = Marshal.PtrToStringAnsi(returnValue);
                    if (returnValueText == "0000")
                    {
                        outputText = rcvMsg.ToString();
                    }
                    else {
                        AppLogger.Info("调用医保异常: " + returnValueText);
                        var nn = new { xxfhm = "-1", fhxx = "调用医保异常: " + returnValueText };
                        outputText = JsonConvert.SerializeObject(nn);
                    }
                    Marshal.FreeHGlobal(returnValue);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("调用医保异常: " + ex.Message);
                    var nn = new { xxfhm = "-1", fhxx = "调用医保异常: " + ex.Message };
                    outputText = JsonConvert.SerializeObject(nn);
                    returnValueText = ex.Message;
                }
            }
            else
            {
                switch (input.transType)
                {
                    case "SN01":
                        testdata = "{jysj:\"2020-06-03 12:00:00\",xxlxm:\"SN01\",xxfhm:\"P001\",fhxx:\"交易成功\",bbh:\"\",msgid:\"9999999990020200101000000001\",xzqhdm:\"310000\",jgdm:\"99999999900\",czybm:\"GH0001\",czyxm:\"挂号收费终端\",xxnr:{mxzdh:\"ABC001001\",mxxms:[{xh:1,clbz:\"0\",fhxx:\"\",bxbz:\"1\",mxxmje:2.00,mxxmjyfy:2.00,mxxmybjsfwfy:2.00}]},jyqd:\"10\",jyyzm:\"11\",zdjbhs:\"10\"}";
                        break;
                    case "S000":
                        testdata = "{\"jysj\":\"2020-06-03 12:00:00\",\"xxlxm\":\"S000\",\"xxfhm\":\"P001\",\"fhxx\":\"交易成功\",\"bbh\":\"001\",\"msgid\":\"9999999992020200000001\",\"xzqhdm\":\"310000\",\"jgdm\":\"9999999\",\"czybm\":\"GH0001\",\"czyxm\":\"挂号收费终端\",\"xxnr\":{\"kh\":\"KH002\",\"xm\":\"XXXX\",\"xb\":\"0\",\"sfzh\":\"510202194201201225\",\"lxdh\":\"15630567890\",\"txdz\":\"1111111111\",\"yzbm\":\"222222\",\"xzqh\":\"111111\"},\"jyqd\":\"10\",\"jyyzm\":\"00000|ABC123dddd\",\"zdjbhs\":\"E0909123213\",\"ectoken\":\"\"}";
                        break;
                    case "SJG1":
                        testdata = "{\"jysj\":\"2020-06-23 12:00:00\",\"xxlxm\":\"SJG1\",\"xxfhm\":\"P001\",\"fhxx\":\"交易成功\",\"bbh\":\"001\",\"msgid\":\"9999999992020200000001\",\"xzqhdm\":\"310000\",\"jgdm\":\"9999999\",\"czybm\":\"GH0001\",\"czyxm\":\"挂号收费终端\",\"xxnr\":{\"cardtype\":\"1\",\"cardid\":\"123123123123\",\"personname\":\"xxxxx\",\"gsxxs\":[{\"gsrdh\":\"123123\",\"ssbw\":\"eretret\",\"gsjsbj\":\"1\",\"gsqsrq\":\"123112\"}]},\"jyqd\":\"10\",\"jyyzm\":\"00000|ABC123dddd\",\"zdjbhs\":\"E0909123213\",\"ectoken\":\"\"}";
                        break;
                    case "SN02":
                        testdata = "{\"jysj\":\"2020-06-23 12:00:00\",\"xxlxm\":\"SN02\",\"xxfhm\":\"P001\",\"fhxx\":\"交易成功\",\"bbh\":\"001\",\"msgid\":\"9999999992020200000001\",\"xzqhdm\":\"310000\",\"jgdm\":\"9999999\",\"czybm\":\"GH0001\",\"czyxm\":\"挂号收费终端\",\"xxnr\":{},\"jyqd\":\"10\",\"jyyzm\":\"00000|ABC123dddd\",\"zdjbhs\":\"E0909123213\",\"ectoken\":\"\"}";
                        break;
                    case "SH01":
                        testdata = "{\"jysj\":\"2020-06-24 12:00:00\",\"xxlxm\":\"SH01\",\"xxfhm\":\"P001\",\"fhxx\":\"交易成功\",\"bbh\":\"001\",\"msgid\":\"9999999992020200000001\",\"xzqhdm\":\"310000\",\"jgdm\":\"9999999\",\"czybm\":\"GH0001\",\"czyxm\":\"挂号收费终端\",\"xxnr\":{\"cardtype\":\"0\",\"cardid\":\"12312312312312\",\"personname\":\"XXXX\",\"personspectag\":\"0\",\"accountattr\":\"1\",\"jmjsbz\":\"0\",\"totalexpense\":12.20,\"curaccountpay\":12.20,\"hisaccountpay\":12.20,\"zfdxjzfs\":12.20,\"zfdlnzhzfs\":12.20,\"tcdzhzfs\":12.20,\"tcdxjzfs\":12.20,\"tczfs\":12.20,\"fjdzhzfs\":12.20,\"fjdxjzfs\":12.20,\"fjzfs\":12.20,\"curaccountamt\":12.20,\"hisaccountamt\":12.20,\"ybjsfwfyze\":12.20,\"fybjsfwfyze\":12.20,\"jssqxh\":\"00000001\",\"jlc\":\"00000001\",\"jfje\":12.20},\"jyqd\":\"10\",\"jyyzm\":\"00000|ABC123dddd\",\"zdjbhs\":\"E0909123213\",\"ectoken\":\"\"}";
                        break;
                    case "SH02":
                        testdata = "{\"jysj\":\"2020-06-24 12:00:00\",\"xxlxm\":\"SH02\",\"xxfhm\":\"P001\",\"fhxx\":\"交易成功\",\"bbh\":\"001\",\"msgid\":\"9999999992020200000001\",\"xzqhdm\":\"310000\",\"jgdm\":\"9999999\",\"czybm\":\"GH0001\",\"czyxm\":\"挂号收费终端\",\"xxnr\":{\"cardtype\":\"0\",\"cardid\":\"12333\",\"jzdyh\":\"12132132\",\"lsh\":\"1232132131\",\"jssqxh\":\"00000001\",\"jmjsbz\":\"0\",\"totalexpense\":12.20,\"curaccountpay\":12.20,\"hisaccountpay\":12.20,\"zfdxjzfs\":12.20,\"zfdlnzhzfs\":12.20,\"tcdzhzfs\":12.20,\"tcdxjzfs\":12.20,\"tczfs\":12.20,\"fjdzhzfs\":12.20,\"fjdxjzfs\":12.20,\"fjzfs\":12.20,\"curaccountamt\":12.20,\"hisaccountamt\":12.20,\"ybjsfwfyze\":12.20,\"fybjsfwfyze\":0.00,\"jlc\":\"00000001\",\"jfje\":12.20},\"jyqd\":\"10\",\"jyyzm\":\"00000|ABC123dddd\",\"zdjbhs\":\"E0909123213\",\"ectoken\":\"\"}";
                        break;
                    case "SI11":
                        testdata = "{\"jysj\":\"2020-06-24 12:00:00\",\"xxlxm\":\"SI11\",\"xxfhm\":\"P001\",\"fhxx\":\"交易成功\",\"bbh\":\"001\",\"msgid\":\"9999999992020200000001\",\"xzqhdm\":\"310000\",\"jgdm\":\"9999999\",\"czybm\":\"GH0001\",\"czyxm\":\"挂号收费终端\",\"xxnr\":{\"cardtype\":\"0\",\"cardid\":\"12333\",\"personname\":\"xxxxx\",\"personspectag\":\"0\",\"accountattr\":\"11111\",\"totalexpense\":12.20,\"curaccountpay\":12.20,\"hisaccountpay\":12.20,\"zfdxjzfs\":12.20,\"zfdlnzhzfs\":12.20,\"tcdzhzfs\":12.20,\"tcdxjzfs\":12.20,\"tczfs\":12.20,\"fjdzhzfs\":12.20,\"fjdxjzfs\":12.20,\"fjzfs\":12.20,\"curaccountamt\":12.20,\"hisaccountamt\":12.20,\"ybjsfwfyze\":12.20,\"fybjsfwfyze\":12.20,\"jssqxh\":\"00000001\",\"jlc\":\"00000001\",\"jfje\":12.20},\"jyqd\":\"10\",\"jyyzm\":\"00000|ABC123dddd\",\"zdjbhs\":\"E0909123213\",\"ectoken\":\"\"}";
                        break;
                    case "SI12":
                        testdata = "{\"jysj\":\"2020-06-24 12:00:00\",\"xxlxm\":\"SI12\",\"xxfhm\":\"P001\",\"fhxx\":\"交易成功\",\"bbh\":\"001\",\"msgid\":\"9999999992020200000001\",\"xzqhdm\":\"310000\",\"jgdm\":\"9999999\",\"czybm\":\"GH0001\",\"czyxm\":\"挂号收费终端\",\"xxnr\":{\"cardtype\":\"0\",\"cardid\":\"12333\",\"lsh\":\"123xxxxx\",\"jssqxh\":\"65670\",\"totalexpense\":12.20,\"curaccountpay\":12.20,\"hisaccountpay\":12.20,\"zfdxjzfs\":12.20,\"zfdlnzhzfs\":12.20,\"tcdzhzfs\":12.20,\"tcdxjzfs\":12.20,\"tczfs\":12.20,\"fjdzhzfs\":12.20,\"fjdxjzfs\":12.20,\"fjzfs\":12.20,\"curaccountamt\":12.20,\"hisaccountamt\":12.20,\"ybjsfwfyze\":12.20,\"fybjsfwfyze\":12.20,\"jlc\":\"00000001\",\"jfje\":12.20},\"jyqd\":\"10\",\"jyyzm\":\"00000|ABC123dddd\",\"zdjbhs\":\"E0909123213\",\"ectoken\":\"\"}";
                        break;
                    case "SI51":
                        testdata = "{\"jysj\":\"2023-07-21 17:25:00\",\"xxlxm\":\"SI51\",\"xxfhm\":\"P001\",\"fhxx\":\"\",\"bbh\":\"0001\",\"msgid\":\"9999999940420230721000008467\",\"xzqhdm\":\"\",\"jgdm\":\"99999999404\",\"czybm\":\"000000\",\"czyxm\":\"管理员\",\"xxnr\":{\"fjdzhzfs\":\"0.00\",\"tcdxjzfs\":\"0.00\",\"totalexpense\":\"145.00\",\"jfje\":\"0.00\",\"qfdzhzfs\":\"0.00\",\"tczfs\":\"0.00\",\"curaccountamt\":\"0.00\",\"fjdxjzfs\":\"0.00\",\"fjdgjzhzfs\":\"0.00\",\"fjzfs\":\"0.00\",\"qfdxjzfs\":\"0.00\",\"cardid\":\"9600000039\",\"qfdgjzhzfs\":\"145.00\",\"accountattr\":\"1000000000000000\",\"tcdzhzfs\":\"0.00\",\"cardtype\":\"0\",\"fybjsfwfyze\":\"0.00\",\"jssqxh\":\"I529999999940460000390721991486483\",\"hisaccountamt\":\"0.00\",\"ybjsfwfyze\":\"145.00\",\"tcdgjzhzfs\":\"0.00\",\"personname\":\"城保测试039（组）\"},\"jyqd\":\"10\",\"jyyzm\":\"\",\"zdjbhs\":null}";
                        break;
                    case "SI52":
                        testdata = "{\"jysj\":\"2023-07-21 17:28:06\",\"xxlxm\":\"SK01\",\"xxfhm\":\"P001\",\"fhxx\":\"\",\"bbh\":\"0001\",\"msgid\":\"9999999940420230721000005320\",\"xzqhdm\":\"\",\"jgdm\":\"99999999404\",\"czybm\":\"000000\",\"czyxm\":\"管理员\",\"xxnr\":{\"translsh\":\"2301310000009103\",\"zfcash\":\"0.00\",\"dffj\":\"0.00\",\"curaccountamt\":\"0.00\",\"curaccount\":\"0.00\",\"tc\":\"0.00\",\"tccash\":\"0.00\",\"dffjhisaccount\":\"0.00\",\"gjaccount\":\"145.00\",\"cardid\":\"9600000039\",\"accountattr\":\"1000000000000000\",\"tchisaccount\":\"0.00\",\"hisaccount\":\"0.00\",\"dffjcash\":\"0.00\",\"cardtype\":\"0\",\"hisaccountamt\":\"0.00\",\"personname\":\"城保测试039（组）\"},\"jyqd\":\"10\",\"jyyzm\":\"\",\"zdjbhs\":null}";
                        break;
                    case "SK01":
                        testdata = "{\"jysj\":\"2023-07-21 16:52:53\",\"xxlxm\":\"SK01\",\"xxfhm\":\"P001\",\"fhxx\":\"\",\"bbh\":\"0001\",\"msgid\":\"9999999940420230721000008993\",\"xzqhdm\":\"\",\"jgdm\":\"99999999404\",\"czybm\":\"000000\",\"czyxm\":\"管理员\",\"xxnr\":{\"translsh\":\"2301310000009091\",\"zfcash\":\"0.00\",\"dffj\":\"0.00\",\"curaccountamt\":\"0.00\",\"curaccount\":\"0.00\",\"tc\":\"0.00\",\"tccash\":\"0.00\",\"dffjhisaccount\":\"0.00\",\"gjaccount\":\"145.00\",\"cardid\":\"9600000039\",\"accountattr\":\"1000000000000000\",\"tchisaccount\":\"0.00\",\"hisaccount\":\"0.00\",\"dffjcash\":\"0.00\",\"cardtype\":\"0\",\"hisaccountamt\":\"0.00\",\"personname\":\"城保测试039（组）\"},\"jyqd\":\"10\",\"jyyzm\":\"\",\"zdjbhs\":null}";
                        break;
                    case "SJ11":
                        testdata = "{\"jysj\":\"2020-06-24 12:00:00\",\"xxlxm\":\"SJ11\",\"xxfhm\":\"P001\",\"fhxx\":\"交易成功\",\"bbh\":\"001\",\"msgid\":\"9999999992020200000001\",\"xzqhdm\":\"310000\",\"jgdm\":\"9999999\",\"czybm\":\"GH0001\",\"czyxm\":\"挂号收费终端\",\"xxnr\":{\"cardtype\":\"0\",\"cardid\":\"12333\",\"jzdyh\":\"1232131\",\"personname\":\"XXXX\",\"sfzh\":\"123231231\",\"rysx\":\"0\",\"gzqk\":\"1\",\"zcyymc\":\"xxxxx\",\"startdate\":\"20200610\",\"enddate\":\"20200612\",\"lsh\":\"123123213\",\"curaccountamt\":12.20,\"hisaccountamt\":12.20,},\"jyqd\":\"10\",\"jyyzm\":\"00000|ABC123dddd\",\"zdjbhs\":\"E0909123213\",\"ectoken\":\"\"}";
                        break;
                    case "SJ21":
                        testdata = "{\"jysj\":\"2020-06-24 12:00:00\",\"xxlxm\":\"SJ21\",\"xxfhm\":\"P001\",\"fhxx\":\"交易成功\",\"bbh\":\"001\",\"msgid\":\"9999999992020200000001\",\"xzqhdm\":\"310000\",\"jgdm\":\"9999999\",\"czybm\":\"GH0001\",\"czyxm\":\"挂号收费终端\",\"xxnr\":{\"cardtype\":\"0\",\"cardid\":\"12333\",\"personname\":\"XXXX\",\"accountattr\":\"123231231\",\"curaccountamt\":12.20,\"hisaccountamt\":12.20,},\"jyqd\":\"10\",\"jyyzm\":\"00000|ABC123dddd\",\"zdjbhs\":\"E0909123213\",\"ectoken\":\"\"}";
                        break;
                    case "SJ31":
                        testdata = "{\"jysj\":\"2020-06-24 12:00:00\",\"xxlxm\":\"SJ31\",\"xxfhm\":\"P001\",\"fhxx\":\"交易成功\",\"bbh\":\"001\",\"msgid\":\"9999999992020200000001\",\"xzqhdm\":\"310000\",\"jgdm\":\"9999999\",\"czybm\":\"GH0001\",\"czyxm\":\"挂号收费终端\",\"xxnr\":{\"cardtype\":\"0\",\"cardid\":\"12333\",\"personname\":\"xxx\",\"accountattr\":\"1\",\"djxxs\":[{\"djtype\":\"1\",\"djno\":\"1231322\",\"startdate\":\"20200610\",\"enddate\":\"20200629\",\"zdno\":\"123123213\",\"dbtype\":\"1\",\"dbzl\":\"1\",\"djhossame\":\"0\",\"djhosname\":\"name\"}],\"curaccountamt\":12.20,\"hisaccountamt\":12.20,\"zkdbnye\":12.20},\"jyqd\":\"10\",\"jyyzm\":\"00000|ABC123dddd\",\"zdjbhs\":\"E0909123213\",\"ectoken\":\"\"}";
                        break;
                    case "SJ41":
                        testdata = "{\"jysj\":\"2020-06-24 12:00:00\",\"xxlxm\":\"SJ41\",\"xxfhm\":\"P001\",\"fhxx\":\"交易成功\",\"bbh\":\"001\",\"msgid\":\"9999999992020200000001\",\"xzqhdm\":\"310000\",\"jgdm\":\"9999999\",\"czybm\":\"GH0001\",\"czyxm\":\"挂号收费终端\",\"xxnr\":{\"cardtype\":\"0\",\"cardid\":\"12333\",\"personname\":\"xxx\",\"accountattr\":\"1\",\"djhossame\":\"0\",\"zzqsrq\":\"20200601\",\"zzjsrq\":\"20200605\"},\"jyqd\":\"10\",\"jyyzm\":\"00000|ABC123dddd\",\"zdjbhs\":\"E0909123213\",\"ectoken\":\"\"}";
                        break;
                    case "SJ51":
                        testdata = "{\"jysj\":\"2020-06-24 12:00:00\",\"xxlxm\":\"SJ51\",\"xxfhm\":\"P001\",\"fhxx\":\"交易成功\",\"bbh\":\"001\",\"msgid\":\"9999999992020200000001\",\"xzqhdm\":\"310000\",\"jgdm\":\"9999999\",\"czybm\":\"GH0001\",\"czyxm\":\"挂号收费终端\",\"xxnr\":{\"cardtype\":\"0\",\"cardid\":\"12333\",\"personname\":\"xxx\",\"accountattr\":\"1\",\"djhossame\":\"0\"},\"jyqd\":\"10\",\"jyyzm\":\"00000|ABC123dddd\",\"zdjbhs\":\"E0909123213\",\"ectoken\":\"\"}";
                        break;
                    case "SJC1":
                        testdata = "{\"jysj\":\"2020-06-24 12:00:00\",\"xxlxm\":\"SJC1\",\"xxfhm\":\"P001\",\"fhxx\":\"交易成功\",\"bbh\":\"001\",\"msgid\":\"9999999992020200000001\",\"xzqhdm\":\"310000\",\"jgdm\":\"9999999\",\"czybm\":\"GH0001\",\"czyxm\":\"挂号收费终端\",\"xxnr\":{\"cardtype\":\"0\",\"cardid\":\"12333\",\"personname\":\"xxx\",\"accountattr\":\"1\"},\"jyqd\":\"10\",\"jyyzm\":\"00000|ABC123dddd\",\"zdjbhs\":\"E0909123213\",\"ectoken\":\"\"}";
                        break;
                    case "SJF1":
                        testdata = "{\"jysj\":\"2020-06-24 12:00:00\",\"xxlxm\":\"SJF1\",\"xxfhm\":\"P001\",\"fhxx\":\"交易成功\",\"bbh\":\"001\",\"msgid\":\"9999999992020200000001\",\"xzqhdm\":\"310000\",\"jgdm\":\"9999999\",\"czybm\":\"GH0001\",\"czyxm\":\"挂号收费终端\",\"xxnr\":{\"cardtype\":\"0\",\"cardid\":\"12333\",\"personname\":\"xxx\",\"accountattr\":\"1\"},\"jyqd\":\"10\",\"jyyzm\":\"00000|ABC123dddd\",\"zdjbhs\":\"E0909123213\",\"ectoken\":\"\"}";
                        break;
                    case "SJH1":
                        testdata = "{\"jysj\":\"2020-06-24 12:00:00\",\"xxlxm\":\"SJH1\",\"xxfhm\":\"P001\",\"fhxx\":\"交易成功\",\"bbh\":\"001\",\"msgid\":\"9999999992020200000001\",\"xzqhdm\":\"310000\",\"jgdm\":\"9999999\",\"czybm\":\"GH0001\",\"czyxm\":\"挂号收费终端\",\"xxnr\":{\"zrjgs\":[{\"zrjgbh\":\"123\",\"zrjgmc\":\"12321312\"}]},\"jyqd\":\"10\",\"jyyzm\":\"00000|ABC123dddd\",\"zdjbhs\":\"E0909123213\",\"ectoken\":\"\"}";
                        break;
                    case "SL01":
                        testdata = "{\"jysj\":\"2020-06-24 12:00:00\",\"xxlxm\":\"SL01\",\"xxfhm\":\"P001\",\"fhxx\":\"交易成功\",\"bbh\":\"001\",\"msgid\":\"9999999992020200000001\",\"xzqhdm\":\"310000\",\"jgdm\":\"9999999\",\"czybm\":\"GH0001\",\"czyxm\":\"挂号收费终端\",\"xxnr\":{\"daycollate\":\"20200620\",\"resultcollate\":\"0\"},\"jyqd\":\"10\",\"jyyzm\":\"00000|ABC123dddd\",\"zdjbhs\":\"E0909123213\",\"ectoken\":\"\"}";
                        break;
                    case "SM01":
                        //testdata = "{\"jysj\":\"2020-06-24 12:00:00\",\"xxlxm\":\"SM01\",\"xxfhm\":\"P001\",\"fhxx\":\"交易成功\",\"bbh\":\"001\",\"msgid\":\"9999999992020200000001\",\"xzqhdm\":\"310000\",\"jgdm\":\"9999999\",\"czybm\":\"GH0001\",\"czyxm\":\"挂号收费终端\",\"xxnr\":{\"cardtype\":\"0\",\"cardid\":\"12333\",\"personname\":\"XXX\",\"accountattr\":\"11111111111G\",\"curaccountamt\":12.20,\"hisaccountamt\":12.20,\"totalmzzfdpay\":12.20,\"qfxxpay\":12.20,\"rationpay\":12.20,\"beinqf\":12.20,\"tcfdx\":12.20,\"qfxsfdxxfylj\":12.20,\"jlch\":\"123213\",\"ybsqjmbz\":\"1\",\"xb\":\"1\"},\"jyqd\":\"10\",\"jyyzm\":\"00000|ABC123dddd\",\"zdjbhs\":\"E0909123213\",\"ectoken\":\"\"}";
                        testdata = "{\"jysj\":\"2023-09-20 15:41:18\",\"xxlxm\":\"SM01\",\"xxfhm\":\"P001\",\"fhxx\":\"\",\"bbh\":\"0001\",\"msgid\":\"9999999940420230920000005082\",\"xzqhdm\":\"\",\"jgdm\":\"99999999404\",\"czybm\":\"0616\",\"czyxm\":\"丁艳虹\",\"xxnr\":{\"beinqf\":\"1500.00\",\"curaccountamt\":\"1887.60\",\"xb\":\"1\",\"totalmzzfdpay\":\"0.00\",\"qfxxpay\":\"0.00\",\"qfxsfdxxfylj\":\"0.00\",\"jlch\":\"2XX372202801\",\"cardid\":\"P1290667X\",\"accountattr\":\"1000000000000000\",\"tcfdx\":\"610000.00\",\"ybsqjmbz\":\"1\",\"cardtype\":\"1\",\"hisaccountamt\":\"39109.44\",\"rationpay\":\"500.00\",\"personname\":\"卜兆宏\"},\"jyqd\":\"10\",\"jyyzm\":\"\",\"zdjbhs\":null}";
                        break;
                    case "SI91":
                        testdata = "{\"jysj\":\"2020-06-24 12:00:00\",\"xxlxm\":\"SI91\",\"xxfhm\":\"P001\",\"fhxx\":\"交易成功\",\"bbh\":\"001\",\"msgid\":\"9999999992020200000001\",\"xzqhdm\":\"310000\",\"jgdm\":\"9999999\",\"czybm\":\"GH0001\",\"czyxm\":\"挂号收费终端\",\"xxnr\":{\"cardtype\":\"0\",\"cardid\":\"12333\",\"personname\":\"XXX\",\"accountattr\":\"1\",\"jzdyh\":\"12323\",\"lsh\":\"213123\",\"jssqxh\":\"12323\",\"totalexpense\":12.20,\"curaccountamt\":12.20,\"hisaccountamt\":12.20,\"ybjsfwfyze\":12.20,\"fybjsfwfyze\":12.20,\"curaccountpay\":12.20,\"hisaccountpay\":12.20,\"zfdxjzfs\": 12.20,\"zfdlnzhzfs\":12.20,\"qfdzhzfs\":12.20,\"tcdzhzfs\":12.20,\"fjdzhzfs\":12.20,\"qfdxjzfs\":12.20,\"tcdxjzfs\":12.20,\"fjdxjzfs\":12.20,\"tczfs\":12.20,\"fjzfs\":12.20,\"paystatus\":\"1\"},\"jyqd\":\"10\",\"jyyzm\":\"00000|ABC123dddd\",\"zdjbhs\":\"E0909123213\",\"ectoken\":\"\"}";
                        break;
                    case "SE01":
                        testdata = "{\"jysj\":\"2023-09-20 17:22:17\",\"xxlxm\":\"SE01\",\"xxfhm\":\"P001\",\"fhxx\":\"成功\",\"bbh\":\"0001\",\"msgid\":\"9999999940420230920000007093\",\"xzqhdm\":\"\",\"jgdm\":\"99999999404\",\"czybm\":\"000000\",\"czyxm\":\"管理员\",\"xxnr\":{\"idType\":\"01\",\"insuOrg\":\"310000\",\"ecToken\":\"310000ecaouce4t4h70100007f000055707ba9\",\"userName\":\"卜兆宏\",\"idNo\":\"310103197307252018\"},\"jyqd\":\"10\",\"jyyzm\":\"\",\"zdjbhs\":\"\"}";
                        break;
                    case "SJD1":
                        testdata = "{\"jysj\":\"2020-07-20 12:00:00\",\"xxlxm\":\"SJD1\",\"xxfhm\":\"P001\",\"fhxx\":\"交易成功\",\"bbh\":\"001\",\"msgid\":\"1111111111111111\",\"xzqhdm\":\"111111\",\"jgdm\":\"111111\",\"czybm\":\"11111\",\"czyxm\":\"11111\",\"xxnr\":{\"cardtype\":\"0\",\"cardid\":\"12312312\",\"personname\":\"xxxx\",\"djtype\":\"1\",\"zzxxs\":[{\"zcjgdm\":\"123\",\"zcjgmc\":\"123\",\"zrjgdm\":\"123\",\"zrjgmc\":\"123\",\"startdate\":\"20200702\"}]},\"jyqd\":\"10\",\"jyyzm\":\"11111\",\"zdjbhs\":\"11111\"}";
                        break;
                    default:
                        break;
                }
                outputText = Convert.ToString(testdata);
            }
            AppLogger.Info("请求交易出参：" + outputText);
            //反序列化输出
            NMGOutputPost outputJson = null;
            try
            {
                outputJson = JsonConvert.DeserializeObject<NMGOutputPost>(outputText);
                if (outputJson == null)
                {
                    throw new Exception("(空值)");
                }
            }
            catch (Exception ex)
            {
                return BuildReturnJson("-1", "JSON反序列化SendRcv4函数输出值失败：" + ex.Message + "，输出值：" + outputText);
            }
            //保存交易日志到数据库
            AppLogger.SaveNMGJyDbLog(post, startdate, input, inputJsonStr, outputJson, returnValueText);
            //转换交易输出实体类
            if (outputJson.data != null)
            {
                OutModlem = JsonConvert.DeserializeObject<O>(Convert.ToString(outputJson.data));
            }

            //返回结果
            code = outputJson.code;
            return outputText;
        }
    }
}
