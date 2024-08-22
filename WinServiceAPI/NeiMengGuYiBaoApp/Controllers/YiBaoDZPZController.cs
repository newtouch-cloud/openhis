using NeiMengGuYiBaoApp.Models.Input.NationECCodeDll;
using NeiMengGuYiBaoApp.Models.Input.YiBao;
using NeiMengGuYiBaoApp.Models.Output.NationECCodeDll;
using NeiMengGuYiBaoApp.Models.Output.YiBao;
using NeiMengGuYiBaoApp.Models.Post;
using NeiMengGuYiBaoApp.Models.Post.NationECCodeDll;
using NeiMengGuYiBaoApp.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Web.Http;

namespace NeiMengGuYiBaoApp.Controllers
{
    /// <summary>
    /// 国家医保电子凭证业务|定点医药机构对接医保业务综合服务终端
    /// </summary>
    public class YiBaoDZPZController: BaseController
    {
        /// <summary>
        /// 刷脸获取医保信息 
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetInfoByFace(EcAuthPost ecAuthPost)
        {

            PostBase post = new PostBase();
            //先进行刷脸 然后再获取个人基本信息
            post.hisId = "0";
            post.tradiNumber = "cn.nhsa.ec.auth";
            int code = 0;
            post.operatorId = ecAuthPost.operatorId;
            post.operatorName = ecAuthPost.operatorName;
            post.inModel = 0;

            EcAuthInput input = new EcAuthInput();
            input.orgId = ConfigurationManager.AppSettings["fixmedins_code"];
            input.outBizNo = DllHelper.GetoutBizNo(); 
            input.businessType = ecAuthPost.businessType;
            input.operatorId = ecAuthPost.operatorId;
            input.operatorName = ecAuthPost.operatorName;
            input.officeId = ecAuthPost.officeId;
            input.officeName = ecAuthPost.officeName;

            InputBasePost inputPost = new InputBasePost();
            inputPost.orgId = input.orgId;
            inputPost.transType = "cn.nhsa.ec.auth";
            inputPost.data = JObject.Parse(JsonConvert.SerializeObject(input));

            EcAuthOutput ecAuthOutput = new EcAuthOutput();

            string jsonStr = DllHelper.CallNationECCodeAndSaveLog(inputPost, out ecAuthOutput, post, out code);
            if (code != 0) {
                return YiBaoHelper.BuildReturnJson("-99","刷脸获取授权失败");
            }

            post.tradiNumber = "cn.nhsa.auth.check";
            AuthCheckInput authCheckInput = new AuthCheckInput();
            authCheckInput.orgId = ConfigurationManager.AppSettings["fixmedins_code"];
            authCheckInput.authNo = ecAuthOutput.authNo;
            authCheckInput.outBizNo = DllHelper.GetoutBizNo();
            authCheckInput.businessType = ecAuthPost.businessType;
            authCheckInput.operatorId = ecAuthPost.operatorId;
            authCheckInput.operatorName = ecAuthPost.operatorName;
            authCheckInput.officeId = ecAuthPost.officeId;
            authCheckInput.officeName = ecAuthPost.officeName;

            InputBasePost authCheckInputPost = new InputBasePost();
            authCheckInputPost.orgId = authCheckInput.orgId;
            authCheckInputPost.transType = "cn.nhsa.auth.check";
            authCheckInputPost.data = JObject.Parse(JsonConvert.SerializeObject(authCheckInput));

            AuthCheckOutput authCheckOutput = new AuthCheckOutput();

            string authCheckJsonStr = DllHelper.CallNationECCodeAndSaveLog(authCheckInputPost, out authCheckOutput, post, out code);
            if (code != 0)
            {
                return YiBaoHelper.BuildReturnJson("-99", "刷脸获取获取个人信息失败");
            }

            //获取通过医保接口获取个人信息
            post.hisId = "0";
            post.tradiNumber = "1101";
            post.insuplc_admdvs = "";
            post.tradiNumber = "1101";
            post.operatorId = ecAuthPost.operatorId;
            post.operatorName = ecAuthPost.operatorName;
            post.inModel = 0;

            Input_1101 input1101 = new Input_1101();
            input1101.data = new data1101();
            input1101.data.mdtrt_cert_type = "02";//“01”时填写电子凭证令牌，为“02”时填写身份证号，为“03”时填写社会保障卡卡号
            input1101.data.mdtrt_cert_no = authCheckOutput.idNo;
            //input1101.data.card_sn = authCheckOutput.ecToken;
            input1101.data.begntime = ClassSqlHelper.GetServerTime().ToString("yyyy-MM-dd HH:mm:ss");
            input1101.data.psn_cert_type = authCheckOutput.idType;
            input1101.data.certno = authCheckOutput.idNo;
            input1101.data.psn_name = authCheckOutput.userName;

            Output_1101 Output1101 = new Output_1101();
            string codeOut = "1";
            jsonStr = YiBaoHelper.CallAndSaveLog(input1101, out Output1101, post, out codeOut);
            return jsonStr;
        }

        /// <summary>
        /// 用于电子凭证二维码解码。 
        /// </summary>
        /// <param name="ecQueryPost"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetInfoByEcQuery(EcQueryPost ecQueryPost)
        {

            int code = 0;
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "ec.query";
            post.operatorId = ecQueryPost.operatorId;
            post.operatorName = ecQueryPost.operatorName;
            post.inModel = 0;

            EcQueryInput input = new EcQueryInput();
            input.orgId = ConfigurationManager.AppSettings["fixmedins_code"];
            input.businessType = ecQueryPost.businessType;
            input.operatorId = ecQueryPost.operatorId;
            input.operatorName = ecQueryPost.operatorName;
            input.officeId = ecQueryPost.officeId;
            input.officeName = ecQueryPost.officeName;
            input.deviceType = ecQueryPost.deviceType;

            InputBasePost inputPost = new InputBasePost();
            inputPost.orgId = input.orgId;
            inputPost.transType = "ec.query";
            inputPost.data = JObject.Parse(JsonConvert.SerializeObject(input));

            EcQueryOutput output = new EcQueryOutput();

            string authCheckJsonStr = DllHelper.CallNationECCodeAndSaveLog(inputPost, out output, post, out code);
            if (code != 0)
            {
                return YiBaoHelper.BuildReturnJson("-99", "电子凭证二维码解码获取个人信息失败");
            }

            //获取通过医保接口获取个人信息
            post.hisId = "0";
            post.tradiNumber = "1101";
            post.insuplc_admdvs = "";
            post.tradiNumber = "1101";
            post.operatorId = ecQueryPost.operatorId;
            post.operatorName = ecQueryPost.operatorName;
            post.inModel = 0;

            Input_1101 input1101 = new Input_1101();
            input1101.data = new data1101();
            input1101.data.mdtrt_cert_type = "02";//“01”时填写电子凭证令牌，为“02”时填写身份证号，为“03”时填写社会保障卡卡号
            input1101.data.mdtrt_cert_no = output.idNo;
            input1101.data.card_sn = output.ecToken;
            input1101.data.begntime = ClassSqlHelper.GetServerTime().ToString("yyyy-MM-dd HH:mm:ss");
            input1101.data.psn_cert_type = output.idType;
            input1101.data.certno = output.idNo;
            input1101.data.psn_name = output.userName;

            Output_1101 Output1101 = new Output_1101();
            string codeOut = "1";
            string jsonStr = YiBaoHelper.CallAndSaveLog(input1101, out Output1101, post, out codeOut);
            return jsonStr;
        }

        /// <summary>
        ///用于定点医疗药机构通过使用医保业务综合服务终端集成的扫码设备模块获取医保电子凭证二维码码值后，通过电子凭证中台完成解码
        /// </summary>
        /// <param name="ecQueryPost"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetInfoByQrcode(QrCodePost qrCodePost)
        {

            int code = 0;
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "cn.nhsa.qrcode.get";
            post.operatorId = qrCodePost.operatorId;
            post.operatorName = qrCodePost.operatorName;
            post.inModel = 0;

            QrCodeInput input = new QrCodeInput();
            input.orgId = ConfigurationManager.AppSettings["fixmedins_code"];
            input.businessType = qrCodePost.businessType;
            input.operatorId = qrCodePost.operatorId;
            input.operatorName = qrCodePost.operatorName;
            input.officeId = qrCodePost.officeId;
            input.officeName = qrCodePost.officeName;

            InputBasePost inputPost = new InputBasePost();
            inputPost.orgId = input.orgId;
            inputPost.transType = "cn.nhsa.qrcode.get";
            inputPost.data = JObject.Parse(JsonConvert.SerializeObject(input));

            QrCodeOutput output = new QrCodeOutput();

            string authCheckJsonStr = DllHelper.CallNationECCodeAndSaveLog(inputPost, out output, post, out code);
            if (code != 0)
            {
                return YiBaoHelper.BuildReturnJson("-99", "终端医保电子凭证码解码失败");
            }
            //获取通过医保接口获取个人信息
            post.hisId = "0";
            post.tradiNumber = "1101";
            post.insuplc_admdvs = "";
            post.tradiNumber = "1101";
            post.operatorId = qrCodePost.operatorId;
            post.operatorName = qrCodePost.operatorName;
            post.inModel = 0;

            Input_1101 input1101 = new Input_1101();
            input1101.data = new data1101();
            input1101.data.mdtrt_cert_type = "02";//“01”时填写电子凭证令牌，为“02”时填写身份证号，为“03”时填写社会保障卡卡号
            input1101.data.mdtrt_cert_no = output.idNo;
            input1101.data.card_sn = output.ecToken;
            input1101.data.begntime = ClassSqlHelper.GetServerTime().ToString("yyyy-MM-dd HH:mm:ss");
            input1101.data.psn_cert_type = output.idType;
            input1101.data.certno = output.idNo;
            input1101.data.psn_name = output.userName;

            Output_1101 Output1101 = new Output_1101();
            string codeOut = "1";
            string jsonStr = YiBaoHelper.CallAndSaveLog(input1101, out Output1101, post, out codeOut);
            return jsonStr;
        }

        /// <summary>
        ///若定点医药机构没有通过终端完成医保移动支付，需要调用此接口在终端上显示结算结果。若定点医药机构通过终端完成医保移动支付，则不需要调用此接口
        /// </summary>
        /// <param name="ecQueryPost"></param>
        /// <returns></returns>
        [HttpPost]
        public string SettleNotify(SettleNotifyPost settleNotifyPost)
        {

            int code = 0;
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "cn.nhsa.settle.notify";
            post.operatorId = settleNotifyPost.operatorId;
            post.operatorName = settleNotifyPost.operatorName;
            post.inModel = 0;

            SettleNotifyInput input = new SettleNotifyInput();
            input.orgId = ConfigurationManager.AppSettings["fixmedins_code"];
            input.outBizNo = settleNotifyPost.outBizNo;
            input.medicalSettleState = settleNotifyPost.medicalSettleState;
            input.authNo = settleNotifyPost.authNo;
            input.operatorId = settleNotifyPost.operatorId;
            input.operatorName = settleNotifyPost.operatorName;
            input.totalFee = settleNotifyPost.totalFee;
            input.bizType = settleNotifyPost.bizType;
            input.idNo = settleNotifyPost.idNo;
            input.userName = settleNotifyPost.userName;
            input.setlTime = settleNotifyPost.setlTime;
            input.hospitalName = settleNotifyPost.hospitalName;
            input.officeId = settleNotifyPost.officeId;
            input.officeName = settleNotifyPost.officeName;
            input.doctorName = settleNotifyPost.doctorName;
            if (input.medicalSettleState == "SUCCESS")
            {
                input.medicalSettleNo = settleNotifyPost.medicalSettleNo;
                input.ownAmt = settleNotifyPost.ownAmt;
                input.hifAmt = settleNotifyPost.hifAmt;
                input.acctAmt = settleNotifyPost.acctAmt;
                input.hifpAmt = settleNotifyPost.hifpAmt;
                input.hifmiAmt = settleNotifyPost.hifmiAmt;
                input.cvlservAmt = settleNotifyPost.cvlservAmt;
                input.maAmt = settleNotifyPost.maAmt;
                input.hosPreAmt = settleNotifyPost.hosPreAmt;
                input.medOverLmtAmt = settleNotifyPost.medOverLmtAmt;
                input.mafAmt = settleNotifyPost.mafAmt;
                input.cvlservDedcAmt = settleNotifyPost.cvlservDedcAmt;
                input.balance = settleNotifyPost.balance;

                DrugList drugList = new DrugList();
                drugList.ITEM_NO = settleNotifyPost.ITEM_NO;
                drugList.ITEMNAME = settleNotifyPost.ITEMNAME;
                drugList.HI_ITEM = settleNotifyPost.HI_ITEM;
                drugList.PRIC = settleNotifyPost.PRIC;
                drugList.ITEM_CNT = settleNotifyPost.ITEM_CNT;
                drugList.ITEM_AMT = settleNotifyPost.ITEM_AMT;
                drugList.DRUG_FRQU = settleNotifyPost.DRUG_FRQU;
                drugList.DRUG_DOS = settleNotifyPost.DRUG_DOS;

                input.drugList = JsonConvert.SerializeObject(drugList);
            }

            InputBasePost inputPost = new InputBasePost();
            inputPost.orgId = input.orgId;
            inputPost.transType = "cn.nhsa.settle.notify";
            inputPost.data = JObject.Parse(JsonConvert.SerializeObject(input));

            QrCodeOutput output = new QrCodeOutput();

            string authCheckJsonStr = DllHelper.CallNationECCodeAndSaveLog(inputPost, out output, post, out code);
            if (code != 0)
            {
                return YiBaoHelper.BuildReturnJson("-99", "结算结果通知失败");
            }
            return JsonConvert.SerializeObject(output);
        }

    }
}
