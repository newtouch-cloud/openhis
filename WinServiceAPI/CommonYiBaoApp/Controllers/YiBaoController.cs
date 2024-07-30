using CommonHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Data;
using Newtonsoft.Json;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;
using CQYiBaoInterface.Models.Post;
using CQYiBaoInterface.Models.Input;
using CQYiBaoInterface.Models.Output;
using YiBaoInterface;
using CQYiBaoInterface.Models.SQL;
using CQYiBaoInterface;

namespace CommonYiBaoApp.Controllers
{
    /// <summary>
    /// 国家医保
    /// </summary>
    public class YiBaoController : BaseController
    {
        //调用:http://127.0.0.1:22222/api/YiBao/ReadCard
        //[HttpGet]
        //public string ReadCard()
        //{

        //    Output_1101 retModel = new Output_1101 { sbkh = "12345" };
        //    ReturnYibaoModel<ReadCardOutPut> model =
        //        new ReturnYibaoModel<ReadCardOutPut> { Code = "1", ErrorMsg = "", Data = retModel };
        //    return JsonHelper.ObjToJson(retModel);
        //}



        [HttpGet]
        public JsonResult<ResponseModel> GetTest(string id)
        {
            return ToSuccessResponse($"API接口调用成功,入参 {id}");
        }


        /*
        [HttpGet]
        public JsonResult<ResponseModel> GetTestList(string id)
        {
            List<ReadCardOutPut> readCardOutPuts = new List<ReadCardOutPut>();
            readCardOutPuts.Add(new ReadCardOutPut() { sbkh = "0", brxm = "测试1" });
            readCardOutPuts.Add(new ReadCardOutPut() { sbkh = "1", brxm = "测试2" });
            return ToSuccessResponse(readCardOutPuts, $"入参 {id}");
        }

        [HttpPost]
        public JsonResult<ResponseModel> UpdateReadCardOutPutInfo(ReadCardOutPut readCardOutPut)
        {
            //1.HIS入参获取
            //2.进行医保交易
            //3.医保交易结果存储
            //4.接口返回HIS消息

            //1.HIS入参获取-解析
            //…………ToDo…………

            //2.进行医保交易
            //…………ToDo…………
            string jsonStr = System.IO.File.ReadAllText("TestJson\\json1.json", Encoding.Default);
            CQYiBaoInterface.Models.ResponseYibao responseYibao = JsonHelper.ObjectFromJson<CQYiBaoInterface.Models.ResponseYibao>(jsonStr);
            if (responseYibao.infcode != "1")
            {
                return ToErrorResponse("东软医保接口调用出错");
            }
            CQYiBaoInterface.Models.Yibao1101 yibao1101 = JsonHelper.ObjectFromJson<CQYiBaoInterface.Models.Yibao1101>(responseYibao.output.ToString());
            //3.医保交易结果存储
            //…………ToDo…………

            //4.接口返回HIS消息
            return ToSuccessResponse(yibao1101);
        }
        */





        /// <summary>
        /// 人员信息获取 
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public string CardecInfo_1101(Post_1101 post1101)
        {
            //签到
            //GetSigin(post1101.operatorId, post1101.operatorName);

            PostBase post = new PostBase();
            //先进行读卡 然后再获取个人基本信息
            post.hisId = "0";
            post.tradiNumber = "1101";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = "";
            post.tradiNumber = "1101";
            string code = "1";
            post.operatorId = post1101.operatorId;
            post.operatorName = post1101.operatorName;
            post.inModel = 0;

            Input_1101 input1101 = new Input_1101();
            input1101.data = new data1101();
            input1101.data.mdtrt_cert_type = post1101.mdtrt_cert_type;//“01”时填写电子凭证令牌，为“02”时填写身份证号，为“03”时填写社会保障卡卡号
            input1101.data.mdtrt_cert_no = post1101.mdtrt_cert_no;
            input1101.data.card_sn = post1101.card_sn;
            input1101.data.begntime = ClassSqlHelper.GetServerTime().ToString("yyyy-MM-dd HH:mm:ss");
            input1101.data.psn_cert_type = post1101.psn_cert_type;
            input1101.data.certno = "";
            input1101.data.psn_name = "";

            Output_1101 Output1101 = new Output_1101();

            string jsonStr = ClassHelper.SaveToInterface1(input1101, out Output1101, post, out code);
            return jsonStr;


        }


        /// <summary>
        /// 进行社保卡读卡获取人员信息
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]

        public string CardecInfo_1161(PostBase post)
        {
            //签到
            //GetSigin(post.operatorId, post.operatorName);
            //先进行读卡 然后再获取个人基本信息
            post.hisId = "0";
            post.tradiNumber = "1161";
            //post.userId = Function.GetLocalIp();
            //post.sign_no = sign_no;
            post.insuplc_admdvs = "";

            post.inModel = 0;
            Input_1161 input = new Input_1161();
            Output_1161 output = new Output_1161();
            string code = "1";
            string jsonOut = ClassHelper.SaveToInterface1(input, out output, post, out code);

            return jsonOut;


        }

        /// <summary>
        /// 通过电子凭证获取 人员基本信息
        /// </summary>
        /// <param name="input1162"></param>
        /// <returns></returns>
        [HttpPost]

        public string CardecInfo_1162(Post_1162 input1162)
        {

            //签到
            PostBase post = new PostBase();
            //先进行读卡 然后再获取个人基本信息
            post.hisId = "0";
            //post.userId = Function.GetLocalIp();
            //post.sign_no = sign_no;



            post.insuplc_admdvs = "";
            post.operatorId = input1162.operatorId;
            post.operatorName = input1162.operatorName;
            post.tradiNumber = "1162";
            post.inModel = 0;
            if (input1162.mdtrt_cert_type == "0")//社保卡读卡
            {

                return CardecInfo_1161(post);

            }
            Input_1162 input = new Input_1162();
            input.data = new data1162();
            input.data.businessType = input1162.businessType;
            input.data.deviceType = input1162.deviceType;
            input.data.mdtrt_cert_type = input1162.mdtrt_cert_type;
            input.data.officeId = input1162.officeId;
            input.data.officeName = input1162.officeName;
            input.data.operatorId = input1162.operatorId;
            input.data.operatorName = input1162.operatorName;
            //input.data.orgId = input1162.orgId;
            //GetSigin(input1162.operatorId, input1162.operatorName);
            input.data.orgId = ConfigurationManager.AppSettings["fixmedins_code"];
            Output_1162 output = new Output_1162();
            string code = "";
            ;
            string jsonOut = ClassHelper.SaveToInterface1(input, out output, post, out code);
            return jsonOut;

        }

        /// <summary>
        /// 获取病人慢病信息 
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public string SlowDisease_5301(Post_5301 post5301)
        {
            //
            PostBase post = new PostBase();
            post.hisId = post5301.hisId;
            post.tradiNumber = "5301";
            post.insuplc_admdvs = post5301.insuplc_admdvs;
            post.operatorId = post5301.operatorId;
            post.operatorName = post5301.operatorName;
            post.inModel = 0;

            Input_5301 input5301 = new Input_5301();
            input5301.data = new data5301();
            input5301.data.psn_no = post5301.psn_no;
            Output_5301 output5301 = new Output_5301();

            string code = "1";
            string jsonStr = ClassHelper.SaveToInterface1(input5301, out output5301, post, out code);
            return jsonStr;
        }

        /// <summary>
        ///  【2 2 201 】门诊挂号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public string Registered_2201(Post_2201 input)
        {
            //签到
            //GetSigin(input.operatorId, input.operatorName);

            PostBase post = new PostBase();
            post.hisId = input.hisId;
            post.tradiNumber = "2201";
            //post.userId = Function.GetLocalIp();
            //post.sign_no = sign_no;
            post.insuplc_admdvs = input.insuplc_admdvs;
            post.operatorId = input.operatorId;
            post.operatorName = input.operatorName;

            post.inModel = 0;

            Input_2201 input2201 = new Input_2201();
            input2201.data = new data2201();
            input2201.data.atddr_no = input.atddr_no;
            input2201.data.begntime = input.begntime;
            input2201.data.caty = input.caty;
            input2201.data.dept_code = input.dept_code;
            input2201.data.dept_name = input.dept_name;
            input2201.data.dr_name = input.dr_name;
            input2201.data.insutype = input.insutype;
            input2201.data.ipt_otp_no = input.ipt_otp_no;
            input2201.data.mdtrt_cert_no = input.mdtrt_cert_no;
            input2201.data.mdtrt_cert_type = input.mdtrt_cert_type;
            input2201.data.psn_no = input.psn_no;
            string code = "-1";
            Output_2201 output = new Output_2201();
            output.data = new data2201_out();

            string jsonOut = ClassHelper.SaveToInterface1(input2201, out output, post, out code);
            return jsonOut;
        }

        /// <summary>
        /// 【2 2 201 】门诊挂号撤销
        /// </summary>
        /// <param name="input">参数类</param>
        /// <returns></returns>
        [HttpPost]
        public string RegisteredUndo_2202(Post_2202 input)
        {
            PostBase post = new PostBase();
            post.hisId = input.hisId;
            post.tradiNumber = "2202";
            //post.userId = Function.GetLocalIp();
            //post.sign_no = sign_no;
            post.insuplc_admdvs = input.insuplc_admdvs;
            post.operatorId = input.operatorId;
            post.operatorName = input.operatorName;
            post.inModel = 0; ;
            Input_2202 input2202 = new Input_2202();
            input2202.data = new data2202();
            input2202.data.ipt_otp_no = input.ipt_otp_no;
            input2202.data.mdtrt_id = input.mdtrt_id;
            input2202.data.psn_no = input.psn_no;
            string code = "-1";
            Output_2202 output = new Output_2202();
            string jsonOut = ClassHelper.SaveToInterface1(input2202, out output, post, out code);
            return jsonOut;
        }

        /// <summary>
        /// 就诊登记
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public string Mdtrtinfo_2203A(Post_2203A input)
        {

            PostBase post = new PostBase();
            post.hisId = input.hisId;
            post.tradiNumber = "2203A";
            post.insuplc_admdvs = input.insuplc_admdvs;
            post.operatorId = input.operatorId;
            post.operatorName = input.operatorName;

            post.inModel = 0;

            Input_2203A input2203 = new Input_2203A();
            input2203.mdtrtinfo = new mdtrtinfo_mz();
            input2203.mdtrtinfo.begntime = input.begntime.ToString("yyyy-MM-dd HH:mm:ss");
            input2203.mdtrtinfo.birctrl_matn_date = input.birctrl_matn_date;
            input2203.mdtrtinfo.birctrl_type = input.birctrl_type;

            input2203.mdtrtinfo.dise_codg = input.dise_codg;
            input2203.mdtrtinfo.dise_name = input.dise_name;
            input2203.mdtrtinfo.geso_val = input.geso_val;
            input2203.mdtrtinfo.main_cond_dscr = input.main_cond_dscr;
            input2203.mdtrtinfo.matn_type = input.matn_type;
            input2203.mdtrtinfo.mdtrt_id = input.mdtrt_id;
            input2203.mdtrtinfo.med_type = input.med_type;
            input2203.mdtrtinfo.psn_no = input.psn_no;
            input2203.diseinfo = new List<diseinfo>();
            //获取诊断信息
            DataTable dtDiseinfo = ClassSqlHelper.QueryICD(1, input.hisId, "", "");
            input2203.diseinfo = Function.ToList<diseinfo>(dtDiseinfo);

            string code = "-1";
            Output_2203A output = new Output_2203A();
            string json = ClassHelper.SaveToInterface1(input2203, out output, post, out code);
            if (code == "0")
            {
                Drjk_mzjzxxsc_input jzxx = new Drjk_mzjzxxsc_input();
                jzxx.mdtrt_id = input.mdtrt_id;
                jzxx.psn_no = input.psn_no;
                jzxx.med_type = input.med_type;
                jzxx.begntime = input.begntime;
                jzxx.main_cond_dscr = input.main_cond_dscr;
                jzxx.dise_codg = input.dise_codg;
                jzxx.dise_name = input.dise_name;
                jzxx.birctrl_type = input.birctrl_type;
                jzxx.birctrl_matn_date = input.birctrl_matn_date;
                jzxx.matn_type = input.matn_type;
                jzxx.geso_val = input.geso_val;
                jzxx.mzh = input.hisId;
                jzxx.zt = 1;
                jzxx.zt_czy = "";
                jzxx.czrq = ClassSqlHelper.GetServerTime();
                jzxx.czydm = post.operatorId;

                if (ClassSqlHelper.ExecuteSql(jzxx.ToAddSql()) < 1)
                {
                    JObject jsonUP = JObject.Parse(json);
                    jsonUP["err_msg"] = jsonUP["err_msg"] + "HIS信息提示：写入就诊登记信息表失败drjk_mzjzxxsc_input-100";
                    jsonUP["infcode"] = "-100";
                    return Convert.ToString(jsonUP);
                }
            }
            return json;

        }
        /// <summary>
        /// 费用信息上传
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public string Feedetail_2204(Post_2204 post2204)
        {
            //@type varchar(1),--0挂号  1处方  3 处方退费再传
            //@mzh varchar(20),    
            //@ghxm varchar(20),  --挂号项目
            //@zlxm varchar(20),  --诊疗项目
            //@ckf varchar(20),  --磁卡费
            //@gbf varchar(20),  --工本费
            //@orgId varchar(50),  --组织机构
            //@jzid varchar(50),  --就诊id
            //@rybh varchar(50),  --人员编号
            //@pch varchar(50), --收费批次号
            //@jsnm varchar(50), --结算内码
            //@ksbm varchar(50),--科室编码
            //@ysbm varchar(50)--医生编码
            PostBase post = new PostBase();
            post.tradiNumber = "2204";
            //post.userId = Function.GetLocalIp();
            post.insuplc_admdvs = post2204.insuplc_admdvs;
            post.inModel = 1;
            post.operatorId = post2204.operatorId;
            post.operatorName = post2204.operatorName;
            post.hisId = post2204.hisId;

            DataTable dtFeed = ClassSqlHelper.QuertFeedetail(post2204);
            Input_2204 input2204 = new Input_2204();
            input2204.feedetail = new List<feedetail2204>();

            input2204.feedetail = Function.ToList<feedetail2204>(dtFeed);

            Output_2204 output2204 = new Output_2204();
            output2204.result = new List<result2204>();

            string code = "-1";
            string json = ClassHelper.SaveToInterface1(input2204, out output2204, post, out code);
            if (code == "0")//成功后写入本地数据库
            {

                DateTime date = ClassSqlHelper.GetServerTime();

                List<string> sqlList = new List<string>();
                foreach (feedetail2204 output in input2204.feedetail)
                {
                    Drjk_mzfymxxxsc_input feed = new Drjk_mzfymxxxsc_input();
                    feed = Function.Mapping<Drjk_mzfymxxxsc_input, feedetail2204>(output);//通过函数根据属性名称 自动给值

                    feed.zt = 1;
                    feed.czrq = date;
                    feed.czydm = post.operatorId;
                    feed.mzh = post2204.hisId;
                    sqlList.Add(feed.ToAddSql());
                }


                foreach (result2204 output in output2204.result)
                {
                    Drjk_mzfymxxxsc_output feed = new Drjk_mzfymxxxsc_output();
                    feed = Function.Mapping<Drjk_mzfymxxxsc_output, result2204>(output);
                    feed.zt = 1;
                    feed.chrg_bchno = post2204.pch;
                    feed.mzh = post2204.hisId;
                    sqlList.Add(feed.ToAddSql());
                }
                int eeor = -1;
                ClassSqlHelper.Merge(sqlList, out eeor);
                if (eeor < 0)
                {
                    JObject jsonUP = JObject.Parse(json);
                    jsonUP["err_msg"] = jsonUP["err_msg"] + "HIS信息提示：写入费用信息表表失败Drjk_mzfymxxxsc_input,Drjk_mzfymxxxsc_output-100";
                    jsonUP["infcode"] = "-100";
                    return Convert.ToString(jsonUP);
                }

            }
            return json;

        }

        /// <summary>
        /// 门诊费用明细信息撤销
        /// </summary>
        /// <param name="post2205"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpFeedetail_2205(Post_2205 post2205)
        {

            PostBase post = new PostBase();
            post.tradiNumber = "2205";
            post.hisId = post2205.hisId;
            post.insuplc_admdvs = post2205.insuplc_admdvs;
            post.inModel = 0;
            post.operatorId = post2205.operatorId;
            post.operatorName = post2205.operatorName;

            Input_2205 input = new Input_2205();
            input.data = new data2205();
            input.data.mdtrt_id = post2205.mdtrt_id;
            input.data.chrg_bchno = post2205.chrg_bchno;
            input.data.psn_no = post2205.psn_no;
            Output_2205 output = new Output_2205();
            string code = "-1";
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            if (code == "0")//如果成功则写入医保
            {
                try
                {
                    int eeor = 0;
                    ClassSqlHelper.UpFeedetail(post2205.hisId, post2205.chrg_bchno, out eeor);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("医保明细撤销成功，HIS删除上传明细数据异常：" + ex.Message);
                    return ClassHelper.BuildReturnJson("-99", "医保明细撤销成功，HIS删除上传明细数据失败：" + ex.Message);
                }
            }
            return json;
        }

        /// <summary>
        /// 门诊收费预结算
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public string AccounSettlement_2206(Post_2206 input)
        {

            PostBase post = new PostBase();
            post.hisId = input.hisId;
            post.tradiNumber = "2206";
            //post.userId = Function.GetLocalIp();
            //post.sign_no = sign_no;
            post.insuplc_admdvs = input.insuplc_admdvs;
            post.operatorId = input.operatorId;
            post.operatorName = input.operatorName;
            post.inModel = 0;
            //获取诊断信息
            string code = "1";
            Input_2206 input2206 = new Input_2206();
            input2206.data = new data2206();
            input2206.data = Function.Mapping<data2206, Post_2206>(input);

            Output_2206 output = new Output_2206();
            string json = ClassHelper.SaveToInterface1(input2206, out output, post, out code);

            return json;

        }

        /// <summary>
        /// 门诊收费结算
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public string Settlement_2207(Post_2206 input)
        {
            //签到
            //GetSigin(input.operatorId, input.operatorName);

            PostBase post = new PostBase();
            post.hisId = input.hisId;
            post.tradiNumber = "2207";
            //post.userId = Function.GetLocalIp();
            //post.sign_no = sign_no;
            post.insuplc_admdvs = input.insuplc_admdvs;
            post.operatorId = input.operatorId;
            post.operatorName = input.operatorName;
            post.inModel = 1;

            //获取费用金额
            DataTable dtCost = ClassSqlHelper.QueryCost(1, input.hisId, input.chrg_bchno);

            string code = "1";
            Input_2207 input2207 = new Input_2207();
            input2207.data = new data2207();
            input2207.data = Function.Mapping<data2207, Post_2206>(input);
            input2207.data.fulamt_ownpay_amt = Convert.ToDecimal(dtCost.Rows[0]["fulamt_ownpay_amt"]); //12  fulamt_ownpay_amt 全自费金额  数值型 16,2  Y
            input2207.data.overlmt_selfpay = Convert.ToDecimal(dtCost.Rows[0]["overlmt_selfpay"]); //13  overlmt_selfpay 超限价金额  数值型 16,2  Y
            input2207.data.preselfpay_amt = Convert.ToDecimal(dtCost.Rows[0]["preselfpay_amt"]); //14  preselfpay_amt 先行自付金额  数值型 16,2  Y
            input2207.data.inscp_scp_amt = Convert.ToDecimal(dtCost.Rows[0]["inscp_scp_amt"]);//15  inscp_scp_amt 符合政策范围金额
                                                                                              // input2207.data.dise_codg = input.dise_codg;
            Output_2207 output = new Output_2207();
            output.setlinfo = new setlinfo2207();
            output.setldetail = new List<setldetail>();
            string json = ClassHelper.SaveToInterface1(input2207, out output, post, out code);
            if (code == "0")//如果成功则写入医保
            {
                try
                {
                    DateTime date = ClassSqlHelper.GetServerTime();

                    List<string> sqlList = new List<string>();
                    Drjk_mzjs_input feedInput = new Drjk_mzjs_input();
                    feedInput = Function.Mapping<Drjk_mzjs_input, data2207>(input2207.data);
                    feedInput.zt = 1;
                    feedInput.czrq = date;
                    feedInput.czydm = post.operatorId;
                    feedInput.mzh = input.hisId;
                    feedInput.setl_id = output.setlinfo.setl_id;
                    sqlList.Add(feedInput.ToAddSql());


                    Drjk_mzjs_output feedOutput = new Drjk_mzjs_output();

                    feedOutput = Function.Mapping<Drjk_mzjs_output, setlinfo2207>(output.setlinfo);
                    feedOutput.czrq = date;
                    feedOutput.czydm = post.operatorId;
                    feedOutput.zt = 1;
                    feedOutput.mzh = input.hisId;
                    sqlList.Add(feedOutput.ToAddSql());

                    if (output.setldetail != null && output.setldetail.Count > 0)
                    {
                        foreach (setldetail detail in output.setldetail)
                        {
                            Drjk_mzjs_output_mx outputDetail = new Drjk_mzjs_output_mx();
                            outputDetail = Function.Mapping<Drjk_mzjs_output_mx, setldetail>(detail);
                            outputDetail.mzh = feedInput.mzh;
                            outputDetail.setl_id = output.setlinfo.setl_id;
                            sqlList.Add(outputDetail.ToAddSql());
                        }
                    }

                    int eeor = 0;
                    ClassSqlHelper.Merge(sqlList, out eeor);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("医保结算成功，HIS写结算数据表异常：" + ex.Message);
                    AppLogger.Info("自动执行门诊结算撤销...");
                    string mzjscs = UpSettlement_2208_For2207(input, output.setlinfo.setl_id);
                    OutputReturn mzjscsRt = JsonConvert.DeserializeObject<OutputReturn>(mzjscs);
                    if (mzjscsRt.infcode != "0")
                    {
                        AppLogger.Info("自动执行门诊结算撤销失败：" + mzjscsRt.err_msg);
                        return ClassHelper.BuildReturnJson("-99", "医保结算成功，HIS写结算数据表失败：【" + ex.Message + "】，撤销医保结算失败：【" + mzjscsRt.err_msg + "】，请速联系HIS厂商技术人员。");
                    }
                    else
                    {
                        AppLogger.Info("自动执行门诊结算撤销成功");
                        return ClassHelper.BuildReturnJson("-99", "医保结算成功，HIS写结算数据表失败，已自动撤销医保结算，请联系HIS厂商技术人员。");
                    }
                }
            }

            return json;
        }
        /// <summary>
        /// 门诊收费结算撤销
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpSettlement_2208(Post_2208 input)
        {

            PostBase post = new PostBase();
            post.hisId = input.hisId;
            post.tradiNumber = "2208";
            post.insuplc_admdvs = input.insuplc_admdvs;
            post.operatorId = input.operatorId;
            post.operatorName = input.operatorName;
            post.inModel = 0;

            Input_2208 input2208 = new Input_2208();
            input2208.data = new data2208();
            input2208.data.setl_id = input.setl_id;
            input2208.data.psn_no = input.psn_no;
            input2208.data.mdtrt_id = input.mdtrt_id;
            Output_2208 output = new Output_2208();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input2208, out output, post, out code);
            if (code == "0")//如果成功则更新本地医保表的状态
            {
                try
                {
                    int eeor = 0;
                    ClassSqlHelper.UpSettlement(input.hisId, input.setl_id, input.operatorId, out eeor);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("撤销医保结算成功，HIS更新结算数据表状态异常：" + ex.Message);
                    return ClassHelper.BuildReturnJson("-99", "撤销医保结算成功，HIS更新结算数据表状态失败：" + ex.Message);
                }
            }
            return json;

        }
        /// <summary>
        /// 门诊收费结算撤销(门诊结算数据落地异常时专用)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string UpSettlement_2208_For2207(Post_2206 input, string setl_id)
        {
            Post_2208 p2208 = new Post_2208();
            p2208.operatorId = input.operatorId;
            p2208.operatorName = input.operatorName;
            p2208.insuplc_admdvs = input.insuplc_admdvs;
            p2208.hisId = input.hisId;
            p2208.setl_id = setl_id;
            p2208.mdtrt_id = input.mdtrt_id;
            p2208.psn_no = input.psn_no;

            return UpSettlement_2208(p2208);
        }

        /// <summary>
        /// 住院病人办理登记
        /// </summary>
        /// <param name="Post2401"></param>
        /// <returns></returns>
        [HttpPost]
        public string HospitaMdtrtinfo_2401(Post_2401 Post2401)
        {
            //签到
            //GetSigin(Post2401.operatorId, Post2401.operatorName);

            PostBase post = new PostBase();
            post.hisId = Post2401.hisId;
            post.tradiNumber = "2401";
            post.insuplc_admdvs = Post2401.insuplc_admdvs;
            post.operatorId = Post2401.operatorId;
            post.operatorName = Post2401.operatorName;
            post.inModel = 1;

            DataTable dtHospital = ClassSqlHelper.QueryHospitalInfo(Post2401, 1);

            Input_2401 input2401 = new Input_2401();
            input2401.mdtrtinfo = new mdtrtinfo();
            List<mdtrtinfo> list = Function.ToList<mdtrtinfo>(dtHospital);
            input2401.mdtrtinfo = list[0];
            input2401.diseinfo = new List<diseinfo>();
            input2401.diseinfo = Function.ToList<diseinfo>(ClassSqlHelper.QueryICD(2, Post2401.hisId, Post2401.mdtrt_id, Post2401.psn_no));
            input2401.mdtrtinfo.dise_codg = Post2401.dise_codg;
            input2401.mdtrtinfo.dise_name = Post2401.dise_name;
            Output_2401 output = new Output_2401();
            output.result = new result2401();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input2401, out output, post, out code);
            if (code == "0")//如果成功则写入本地信息表 
            {
                Drjk_rybl_input rybl = new Drjk_rybl_input();
                rybl = Function.Mapping<Drjk_rybl_input, mdtrtinfo>(input2401.mdtrtinfo);
                rybl.zt = 1;
                rybl.czrq = ClassSqlHelper.GetServerTime();
                rybl.czydm = Post2401.operatorId;
                rybl.mdtrt_id = output.result.mdtrt_id;
                rybl.zyh = Post2401.hisId;
                rybl.cybz = 0;
                if (ClassSqlHelper.ExecuteSql(rybl.ToAddSql()) < 0)
                {
                    JObject jsonUP = JObject.Parse(json);
                    jsonUP["err_msg"] = jsonUP["err_msg"] + "HIS信息提示：写入入院办理病人信息失败Drjk_rybl_input-100";
                    jsonUP["infcode"] = "-100";
                    return Convert.ToString(jsonUP);
                }
            }
            return json;
        }
        /// <summary>
        /// 撤销入院登记信息
        /// </summary>
        /// <param name="post2404"></param>
        /// <returns></returns>
        public string HospitaUpMdtrtinfo_2404(Post_2404 post2404)
        {

            PostBase post = new PostBase();
            post.hisId = post2404.hisId;
            post.tradiNumber = "2404";
            post.insuplc_admdvs = post2404.insuplc_admdvs;
            post.operatorId = post2404.operatorId;
            post.operatorName = post2404.operatorName;
            post.inModel = 0;
            Input_2404 input = new Input_2404();
            input.data = new data2404();
            input.data.psn_no = post2404.psn_no;
            input.data.mdtrt_id = post2404.mdtrt_id;
            Output_2404 output = new Output_2404();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            if (code == "0")//如果成功则更新本地信息表 
            {

                if (ClassSqlHelper.UpMdtrtinfo(post2404.operatorId, post2404.hisId, post2404.mdtrt_id) < 1)
                {
                    JObject jsonUP = JObject.Parse(json);
                    jsonUP["err_msg"] = jsonUP["err_msg"] + "HIS信息提示：更新入院办理病人信息失败Drjk_rybl_input-100";
                    jsonUP["infcode"] = "-100";
                    return Convert.ToString(jsonUP);
                }
            }
            return json;

        }

        /// <summary>
        /// 住院病人办理登记修改
        /// </summary>
        /// <param name="Post2401"></param>
        /// <returns></returns>
        [HttpPost]
        public string HospitaMdtrtinfo_2403(Post_2401 Post2401)
        {

            PostBase post = new PostBase();
            post.hisId = Post2401.hisId;
            post.tradiNumber = "2403";
            //post.userId = Function.GetLocalIp();
            //post.sign_no = sign_no;
            post.insuplc_admdvs = Post2401.insuplc_admdvs;
            post.operatorId = Post2401.operatorId;
            post.operatorName = Post2401.operatorName;
            post.inModel = 0;

            DataTable dtHospital = ClassSqlHelper.QueryHospitalInfo(Post2401, 2);

            Input_2403 input2403 = new Input_2403();
            input2403.adminfo = new adminfo();
            List<adminfo> list = Function.ToList<adminfo>(dtHospital);
            input2403.adminfo = list[0];
            input2403.adminfo.mdtrt_id = Post2401.mdtrt_id;
            input2403.adminfo.dise_codg = Post2401.dise_codg;
            input2403.adminfo.dise_name = Post2401.dise_name;
            input2403.diseinfo = new List<diseinfo>();
            input2403.diseinfo = Function.ToList<diseinfo>(ClassSqlHelper.QueryICD(2, Post2401.hisId, Post2401.mdtrt_id, Post2401.psn_no));

            Output_2403 output = new Output_2403();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input2403, out output, post, out code);
            return json;
        }

        /// <summary>
        /// 住院费用信息上传
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public string HospitaFeedetail_2301(Post_2301 post2301)
        {
            //  @orgId varchar(50),  --组织结构
            //@zyh varchar(20),--住院号
            //@yllb varchar(20)--医疗类别


            //签到
            //GetSigin(post2301.operatorId, post2301.operatorName);
            PostBase post = new PostBase();
            post.tradiNumber = "2301";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = post2301.insuplc_admdvs;
            post.hisId = post2301.hisId;
            post.operatorId = post2301.operatorId;
            post.operatorName = post2301.operatorName;

            post.inModel = 1;
            DataTable dtFeed = ClassSqlHelper.QuertHospitalFeedetail(post2301);
            Input_2301 inputListAll = new Input_2301();
            inputListAll.feedetail = new List<feedetail2301>();
            inputListAll.feedetail = Function.ToList<feedetail2301>(dtFeed);


            string json = "";
            HospitaFeedetail(inputListAll.feedetail, post, 0, post2301.uploadCount, out json);
            if (json == "")
            {
                return "{\"infcode\":\"0\",}";
            }
            return json;

        }
        /// <summary>
        /// 住院费用信息上传V2
        /// </summary>
        /// <param name="post2301"></param>
        /// <returns></returns>
        [HttpPost]
        public string HospitaFeedetail_2301V2(Post_2301V2 post2301)
        {
            PostBase post = new PostBase();
            post.tradiNumber = "2301";
            post.insuplc_admdvs = post2301.insuplc_admdvs;
            post.hisId = post2301.hisId;
            post.operatorId = post2301.operatorId;
            post.operatorName = post2301.operatorName;
            post.inModel = 1;
            DataTable dtFeed = ClassSqlHelper.QuertHospitalFeedetailV2(post2301);
            Input_2301 inputListAll = new Input_2301();
            inputListAll.feedetail = new List<feedetail2301>();
            inputListAll.feedetail = Function.ToList<feedetail2301>(dtFeed);

            Input_2301 inputListData = new Input_2301();
            inputListData.feedetail = inputListAll.feedetail.FindAll(p => !p.cnt.Contains("-"));

            Input_2301 inputListRefund = new Input_2301();
            inputListRefund.feedetail = inputListAll.feedetail.FindAll(p => p.cnt.Contains("-"));
            string json = "";
            HospitaFeedetail(inputListData.feedetail, post, 0, post2301.uploadCount, out json);
            if (json == "")
            {
                if (inputListRefund.feedetail.Count > 0)
                {
                    HospitaFeedetail(inputListRefund.feedetail, post, 0, post2301.uploadCount, out json);
                    if (json == "")
                    {
                        return "{\"infcode\":\"0\",}";
                    }
                    else
                    {
                        return json;
                    }
                }
                return "{\"infcode\":\"0\",}";
            }
            return json;
        }

        /// <summary>
        /// 分割进行上传信息
        /// </summary>
        /// <param name="inputListAll">需要上传的总共条数</param>
        /// <param name="post">基础类</param>
        /// <param name="flag">当前第几次上传</param>
        /// <param name="uploadCount">每次上传的条数</param>
        /// <param name="json">返回信息</param>
        private void HospitaFeedetail(List<feedetail2301> inputListAll, PostBase post, int flag, int uploadCount, out string json)
        {

            json = "";
            int max = inputListAll.Count;//总共数据条数

            if (max - flag * uploadCount <= 0) return;
            int curMax = (flag + 1) * uploadCount;
            if (curMax - max >= 0) curMax = max;

            // string Nums = Convert.ToString(curMax - uploadCount * flag);//当前上传条数
            //List<Input_2301> inputList = new List<Input_2301>();

            Input_2301 inputList = new Input_2301();
            inputList.feedetail = new List<feedetail2301>();

            Output_2301 outputList = new Output_2301();
            outputList.result = new List<result2301>();

            for (int i = uploadCount * flag; i < curMax; i++)
            {
                inputList.feedetail.Add(inputListAll[i]);
            }

            string code = "-1";
            string jsonOutput = ClassHelper.SaveToInterface1(inputList, out outputList, post, out code);
            if (code == "0")//成功后写入本地数据库
            {
                DateTime date = ClassSqlHelper.GetServerTime();
                List<string> sqlList = new List<string>();
                foreach (feedetail2301 output in inputList.feedetail)
                {
                    Drjk_zyfymxsc_input feed = new Drjk_zyfymxsc_input();
                    feed = Function.Mapping<Drjk_zyfymxsc_input, feedetail2301>(output);//通过函数根据属性名称 自动给值
                    feed.zyh = post.hisId;
                    feed.zt = 1;
                    feed.czrq = date;
                    feed.czydm = post.operatorId;
                    sqlList.Add(feed.ToAddSql());
                }
                foreach (result2301 output in outputList.result)
                {
                    Drjk_zyfymxsc_output feed = new Drjk_zyfymxsc_output();
                    feed = Function.Mapping<Drjk_zyfymxsc_output, result2301>(output);
                    feed.zyh = post.hisId;
                    feed.zt = 1;
                    sqlList.Add(feed.ToAddSql());
                }
                int eeor = 0;
                ClassSqlHelper.Merge(sqlList, out eeor);
                if (eeor < 0)
                {
                    JObject jsonUP = JObject.Parse(jsonOutput);
                    jsonUP["err_msg"] = jsonUP["err_msg"] + "HIS信息提示：写入费用信息表表失败Drjk_zyfymxsc_input,Drjk_zyfymxsc_output-100";
                    jsonUP["infcode"] = "-100";
                    json = Convert.ToString(json);
                    return;//如报错则不再继续

                }

            }
            else
            {
                json = jsonOutput;
                return;
            }

            HospitaFeedetail(inputListAll, post, flag + 1, uploadCount, out json);//递归

        }

        /// <summary>
        /// 住院费用明细撤销
        /// </summary>
        /// <param name="post2302"></param>
        /// <returns></returns>
        [HttpPost]
        public string HospitaUpDscginfo_2302(Post_2302 post2302)
        {
            //签到
            //GetSigin(post2302.operatorId, post2302.operatorName);

            PostBase post = new PostBase();
            post.hisId = post2302.hisId;
            post.tradiNumber = "2302";
            //post.userId = Function.GetLocalIp();
            //post.sign_no = sign_no;
            post.insuplc_admdvs = post2302.insuplc_admdvs;
            ;
            post.operatorId = post2302.operatorId;
            post.operatorName = post2302.operatorName;
            post.inModel = 0;
            Input_2302 input = new Input_2302();
            input.data = new List<data2302>();
            data2302 dtinput = new data2302();
            dtinput.mdtrt_id = post2302.mdtrt_id;
            dtinput.psn_no = post2302.psn_no;
            dtinput.feedetl_sn = post2302.feedetl_sn;
            input.data.Add(dtinput);
            //input.data.mdtrt_id = post2302.mdtrt_id;
            //input.data.psn_no = post2302.psn_no;
            //input.data.feedetl_sn = post2302.feedetl_sn;
            Output_2302 output = new Output_2302();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            if (code == "0")//如果成功则更新本地信息表 
            {
                try
                {
                    int eeor = 0;
                    ClassSqlHelper.UpHospitaFeedetail(post2302.hisId, post2302.feedetl_sn, out eeor);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("医保明细撤销成功，HIS删除上传明细数据异常：" + ex.Message);
                    return ClassHelper.BuildReturnJson("-99", "医保明细撤销成功，HIS删除上传明细数据失败：" + ex.Message);
                }
            }
            return json;

        }

        /// <summary>
        /// 住院费用明细撤销V2
        /// </summary>
        /// <param name="post2302"></param>
        /// <returns></returns>
        [HttpPost]
        public string HospitaUpDscginfo_2302V2(Post_2302 post2302)
        {
            //签到
            //GetSigin(post2302.operatorId, post2302.operatorName);
            PostBase post = new PostBase();
            post.hisId = post2302.hisId;
            post.tradiNumber = "2302";
            //post.userId = Function.GetLocalIp();
            //post.sign_no = sign_no;
            post.insuplc_admdvs = post2302.insuplc_admdvs;
            post.operatorId = post2302.operatorId;
            post.operatorName = post2302.operatorName;
            post.inModel = 0;
            Input_2302 input = new Input_2302();
            input.data = new List<data2302>();
            if (post2302.feedetl_sn != "0000")
            {
                string[] sArray = post2302.feedetl_sn.Split(',');
                foreach (string jfbh in sArray)
                {
                    data2302 dtinput = new data2302();
                    dtinput.mdtrt_id = post2302.mdtrt_id;
                    dtinput.psn_no = post2302.psn_no;
                    dtinput.feedetl_sn = jfbh;
                    input.data.Add(dtinput);
                }
            }
            else
            {
                data2302 dtinput = new data2302();
                dtinput.mdtrt_id = post2302.mdtrt_id;
                dtinput.psn_no = post2302.psn_no;
                dtinput.feedetl_sn = post2302.feedetl_sn;
                input.data.Add(dtinput);
            }

            Output_2302 output = new Output_2302();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            if (code == "0")//如果成功则更新本地信息表 
            {
                try
                {
                    int eeor = 0;
                    ClassSqlHelper.UpHospitaFeedetail(post2302.hisId, post2302.feedetl_sn, out eeor);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("医保明细撤销成功，HIS删除上传明细数据异常：" + ex.Message);
                    return ClassHelper.BuildReturnJson("-99", "医保明细撤销成功，HIS删除上传明细数据失败：" + ex.Message);
                }
            }
            return json;

        }


        /// <summary>
        /// 住院办理出院登记
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public string HospitaUpSettlement_2402(Post_2402 input)
        {

            PostBase post = new PostBase();
            post.hisId = input.hisId;
            post.tradiNumber = "2402";
            post.insuplc_admdvs = input.insuplc_admdvs;
            post.operatorId = input.operatorId;
            post.operatorName = input.operatorName;
            post.inModel = 0;

            //查询出院登记信息
            Post_2401 post2401 = new Post_2401();
            post2401.hisId = input.hisId;
            post2401.orgId = input.orgId;
            post2401.mdtrt_cert_type = input.mdtrt_cert_type;//过程中暂时不用
            post2401.mdtrt_cert_no = input.mdtrt_cert_no;
            post2401.med_type = input.med_type;
            post2401.mdtrt_id = input.mdtrt_id;

            Input_2402 input2204 = new Input_2402();
            input2204.dscginfo = new dscginfo();

            DataTable dtHospital = ClassSqlHelper.QueryHospitalInfo(post2401, 3);
            List<dscginfo> list = Function.ToList<dscginfo>(dtHospital);
            input2204.dscginfo = list[0];
            input2204.dscginfo.mdtrt_id = input.mdtrt_id;
            input2204.dscginfo.dise_codg = input.dise_codg;
            input2204.dscginfo.dise_name = input.dise_name;
            //查询出院诊断信息
            DataTable dtDiseinfo = ClassSqlHelper.QueryICD(3, input.hisId, input.mdtrt_id, input.psn_no);

            input2204.diseinfo = new List<diseinfo>();
            input2204.diseinfo = Function.ToList<diseinfo>(dtDiseinfo);


            Output_2402 output = new Output_2402();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input2204, out output, post, out code);
            if (code == "0")//如果成功则更新本地医保入院办理表的状态
            {
                int eeor = 0;
                ClassSqlHelper.UpHospitaMdtrtinfo(input.operatorId, input.hisId, input.mdtrt_id, 1);
                if (eeor < 0)
                {
                    JObject jsonUP = JObject.Parse(json);
                    jsonUP["err_msg"] = jsonUP["err_msg"] + "HIS信息提示：更新出院登记信息失败Drjk_zybl_intput-100";
                    jsonUP["infcode"] = "-100";
                    return Convert.ToString(jsonUP);
                }

            }
            return json;
        }

        /// <summary>
        /// 出院撤销
        /// </summary>
        /// <param name="post2405"></param>
        /// <returns></returns>
        [HttpPost]
        public string HospitaUpOutMdtrtinfo_2405(Post_2405 post2405)
        {
            PostBase post = new PostBase();
            post.hisId = post2405.hisId;
            post.tradiNumber = "2405";
            post.insuplc_admdvs = post2405.insuplc_admdvs;
            post.operatorId = post2405.operatorId;
            post.operatorName = post2405.operatorName;
            post.inModel = 0;

            Input_2405 input = new Input_2405();
            input.data = new data2405();
            input.data.mdtrt_id = post2405.mdtrt_id;
            input.data.psn_no = post2405.psn_no;

            Output_2405 output = new Output_2405();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            if (code == "0")//如果成功则更新本地信息表 
            {
                try
                {
                    ClassSqlHelper.UpHospitaMdtrtinfo(post2405.operatorId, post2405.hisId, post2405.mdtrt_id, 0);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("撤销医保出院成功，HIS更新入院数据表状态异常：" + ex.Message);
                    return ClassHelper.BuildReturnJson("-99", "撤销医保出院成功，HIS更新入院数据表状态失败：" + ex.Message);
                }
            }
            return json;

        }
        /// <summary>
        /// 出院撤销(住院结算数据落地异常时专用)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string HospitaUpOutMdtrtinfo_2405_For2304(Post_2303 input)
        {
            Post_2405 p2405 = new Post_2405();
            p2405.hisId = input.hisId;
            p2405.operatorId = input.operatorId;
            p2405.operatorName = input.operatorName;
            p2405.insuplc_admdvs = input.insuplc_admdvs;
            p2405.mdtrt_id = input.mdtrt_id;
            p2405.psn_no = input.psn_no;
            return HospitaUpOutMdtrtinfo_2405(p2405);
        }

        /// <summary>
        /// 住院收费结算
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public string HospitaSettlement_2304(Post_2303 input)
        {
            //签到
            //GetSigin(input.operatorId, input.operatorName);

            PostBase post = new PostBase();
            post.hisId = input.hisId;
            post.tradiNumber = "2304";
            //post.userId = Function.GetLocalIp();
            //post.sign_no = sign_no;
            post.insuplc_admdvs = input.insuplc_admdvs;
            post.operatorId = input.operatorId;
            post.operatorName = input.operatorName;
            post.inModel = 1;

            //获取费用金额
            DataTable dtCost = ClassSqlHelper.QueryCost(2, input.hisId, input.psn_no);
            string code = "1";
            Input_2304 input2304 = new Input_2304();
            input2304.data = new data2304();
            input2304.data = Function.Mapping<data2304, Post_2303>(input);
            input2304.data.fulamt_ownpay_amt = Convert.ToDecimal(dtCost.Rows[0]["fulamt_ownpay_amt"]); //12  fulamt_ownpay_amt 全自费金额  数值型 16,2  Y
            input2304.data.overlmt_selfpay = Convert.ToDecimal(dtCost.Rows[0]["overlmt_selfpay"]); //13  overlmt_selfpay 超限价金额  数值型 16,2  Y
            input2304.data.preselfpay_amt = Convert.ToDecimal(dtCost.Rows[0]["preselfpay_amt"]); //14  preselfpay_amt 先行自付金额  数值型 16,2  Y
            input2304.data.inscp_scp_amt = Convert.ToDecimal(dtCost.Rows[0]["inscp_scp_amt"]);//15  inscp_scp_amt 符合政策范围金额 det_item_fee_sumamt 总金额
            Output_2304 output = new Output_2304();
            output.setldetail = new List<setldetail>();
            output.setlinfo = new setlinfo2304();
            string json = ClassHelper.SaveToInterface1(input2304, out output, post, out code);
            if (code == "0")//如果成功则写入医保
            {
                try
                {
                    DateTime date = ClassSqlHelper.GetServerTime();

                    List<string> sqlList = new List<string>();
                    Drjk_zyjs_input feedInput = new Drjk_zyjs_input();
                    feedInput = Function.Mapping<Drjk_zyjs_input, data2304>(input2304.data);
                    feedInput.zt = 1;
                    feedInput.czrq = date;
                    feedInput.czydm = post.operatorId;
                    feedInput.zyh = input.hisId;
                    feedInput.setl_id = output.setlinfo.setl_id;
                    sqlList.Add(feedInput.ToAddSql());


                    Drjk_zyjs_output feedOutput = new Drjk_zyjs_output();
                    feedOutput = Function.Mapping<Drjk_zyjs_output, setlinfo2304>(output.setlinfo);
                    feedOutput.zt = 1;
                    feedOutput.zyh = input.hisId;
                    feedOutput.czrq = date;
                    feedOutput.czydm = post.operatorId;
                    sqlList.Add(feedOutput.ToAddSql());

                    if (output.setldetail.Count > 0)
                    {
                        foreach (setldetail detail in output.setldetail)
                        {
                            Drjk_zyjs_output_mx outputDetail = new Drjk_zyjs_output_mx();

                            outputDetail = Function.Mapping<Drjk_zyjs_output_mx, setldetail>(detail);
                            outputDetail.zyh = feedInput.zyh;
                            outputDetail.setl_id = output.setlinfo.setl_id;
                            sqlList.Add(outputDetail.ToAddSql());
                        }
                    }

                    int eeor = -1;
                    ClassSqlHelper.Merge(sqlList, out eeor);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("医保结算成功，HIS写结算数据表异常：" + ex.Message);
                    AppLogger.Info("自动执行住院结算撤销...");
                    string zyjscx = HospitaUpSettlement_2305_For2304(input, output.setlinfo.setl_id);
                    OutputReturn zyjscxRt = JsonConvert.DeserializeObject<OutputReturn>(zyjscx);
                    if (zyjscxRt.infcode != "0")
                    {
                        AppLogger.Info("自动执行住院结算撤销失败：" + zyjscxRt.err_msg);
                        return ClassHelper.BuildReturnJson("-99", "医保结算成功，HIS写结算数据表失败，撤销医保结算失败，请速联系HIS厂商技术人员。");
                    }
                    else
                    {
                        AppLogger.Info("自动执行住院结算撤销成功");
                        return ClassHelper.BuildReturnJson("-99", "医保结算成功，HIS写结算数据表失败，已自动撤销医保结算，请联系HIS厂商技术人员。");
                    }
                    /* (HIS端已有出院撤销处理)
                    AppLogger.Info("自动执行出院撤销...");
                    string cycx = HospitaUpOutMdtrtinfo_2405_For2304(input);
                    OutputReturn cycxRt = JsonConvert.DeserializeObject<OutputReturn>(cycx);
                    if (cycxRt.infcode != "0")
                    {
                        AppLogger.Info("自动执行住院撤销失败：" + cycxRt.err_msg);
                        return ClassHelper.BuildReturnJson("-99", "医保结算成功，HIS写结算数据表失败，已自动撤销医保结算，但撤销出院失败，请速联系HIS厂商技术人员。");
                    }
                    else
                    {
                        AppLogger.Info("自动执行住院撤销成功");
                        return ClassHelper.BuildReturnJson("-99", "医保结算成功，HIS写结算数据表失败，已自动撤销医保结算和出院，请联系HIS厂商技术人员。");
                    }
                    */
                }
            }

            return json;
        }


        /// <summary>
        /// 住院收费预结算
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public string HospitaSettlement_2303(Post_2303 input)
        {
            //签到
            //GetSigin(input.operatorId, input.operatorName);

            PostBase post = new PostBase();
            post.hisId = input.hisId;
            post.tradiNumber = "2303";
            //post.userId = Function.GetLocalIp();
            //post.sign_no = sign_no;
            post.insuplc_admdvs = input.insuplc_admdvs;
            post.operatorId = input.operatorId;
            post.operatorName = input.operatorName;
            post.inModel = 1;

            //获取费用金额
            DataTable dtCost = ClassSqlHelper.QueryCost(2, input.hisId, input.psn_no);
            string code = "1";
            Input_2304 input2304 = new Input_2304();
            input2304.data = new data2304();
            input2304.data = Function.Mapping<data2304, Post_2303>(input);

            input2304.data.fulamt_ownpay_amt = Convert.ToDecimal(dtCost.Rows[0]["fulamt_ownpay_amt"]); //12  fulamt_ownpay_amt 全自费金额  数值型 16,2  Y
            input2304.data.overlmt_selfpay = Convert.ToDecimal(dtCost.Rows[0]["overlmt_selfpay"]); //13  overlmt_selfpay 超限价金额  数值型 16,2  Y
            input2304.data.preselfpay_amt = Convert.ToDecimal(dtCost.Rows[0]["preselfpay_amt"]); //14  preselfpay_amt 先行自付金额  数值型 16,2  Y
            input2304.data.inscp_scp_amt = Convert.ToDecimal(dtCost.Rows[0]["inscp_scp_amt"]);//15  inscp_scp_amt 符合政策范围金额 det_item_fee_sumamt 总金额
            input2304.data.dscgTime = input.dscgTime;
            Output_2304 output = new Output_2304();
            output.setldetail = new List<setldetail>();
            output.setlinfo = new setlinfo2304();
            string json = ClassHelper.SaveToInterface1(input2304, out output, post, out code);


            return json;
        }


        /// <summary>
        /// 住院结算撤销
        /// </summary>
        /// <param name="post2305"></param>
        /// <returns></returns>
        public string HospitaUpSettlement_2305(Post_2305 post2305)
        {
            //签到
            //GetSigin(post2305.operatorId, post2305.operatorName);
            PostBase post = new PostBase();
            post.hisId = post2305.hisId;
            post.tradiNumber = "2305";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = post2305.insuplc_admdvs;

            post.operatorId = post2305.operatorId;
            post.operatorName = post2305.operatorName;
            post.inModel = 0;
            Input_2305 input = new Input_2305();
            input.data = new data2305();
            input.data.mdtrt_id = post2305.mdtrt_id;
            input.data.psn_no = post2305.psn_no;
            input.data.setl_id = post2305.setl_id;
            Output_2305 output = new Output_2305();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            if (code == "0")//如果成功则更新本地信息表 zt=0 
            {
                try
                {
                    int eeor = 0;
                    ClassSqlHelper.UpHospitaSettlement(post2305.hisId, post2305.setl_id, post2305.operatorId, out eeor);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("撤销医保结算成功，HIS更新结算数据表状态异常：" + ex.Message);
                    return ClassHelper.BuildReturnJson("-99", "撤销医保结算成功，HIS更新结算数据表状态失败：" + ex.Message);
                }
            }
            return json;
        }
        /// <summary>
        /// 住院结算撤销(住院结算数据落地异常时专用)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string HospitaUpSettlement_2305_For2304(Post_2303 input, string setl_id)
        {
            Post_2305 p2305 = new Post_2305();
            p2305.hisId = input.hisId;
            p2305.operatorId = input.operatorId;
            p2305.operatorName = input.operatorName;
            p2305.insuplc_admdvs = input.insuplc_admdvs;
            p2305.mdtrt_id = input.mdtrt_id;
            p2305.psn_no = input.psn_no;
            p2305.setl_id = setl_id;
            return HospitaUpSettlement_2305(p2305);
        }

        /// <summary>
        /// 冲正交易
        /// </summary>
        /// <param name="input2601"></param>
        /// <returns></returns>
        [HttpPost]
        public string ImpactIs_2601(Post_2601 input2601)
        {
            //签到
            //GetSigin(input2601.operatorId, input2601.operatorName);
            PostBase post = new PostBase();
            post.hisId = input2601.hisId;
            post.tradiNumber = "2601";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = input2601.insuplc_admdvs;

            post.operatorId = input2601.operatorId;
            post.operatorName = input2601.operatorName;
            post.inModel = 0;

            Input_2601 input = new Input_2601();
            input.data = new data2601();
            input.data.oinfno = input2601.oinfno;
            input.data.psn_no = input2601.psn_no;
            input.data.omsgid = input2601.omsgid;
            Output_2601 output = new Output_2601();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            return json;
        }


        /// <summary>
        /// 医药机构费用结算对总账
        /// </summary>
        /// <param name="input2601"></param>
        /// <returns></returns>
        [HttpPost]
        public string ReconciliationMain_3201(Post_3201 input2301)
        {
            //签到
            //GetSigin(input2301.operatorId, input2301.operatorName);

            PostBase post = new PostBase();
            post.hisId = input2301.hisId;
            post.tradiNumber = "3201";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = input2301.insuplc_admdvs;

            post.operatorId = input2301.operatorId;
            post.operatorName = input2301.operatorName;

            DataTable dtMain = ClassSqlHelper.QueryReconciliation(input2301, 1);

            Input_3201 input = new Input_3201();
            input.data = new data3201();

            List<data3201> list = Function.ToList<data3201>(dtMain);
            input.data = list[0];

            Output_3201 output = new Output_3201();
            output.stmtinfo = new stmtinfo();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            return json;
        }

        /// <summary>
        /// 医药机构费用结算对明细
        /// </summary>
        /// <param name="input2601"></param>
        /// <returns></returns>
        [HttpPost]
        public string ReconciliationDetail_3202(Post_3201 input3202)
        {
            //签到
            //GetSigin(input3202.operatorId, input3202.operatorName);

            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "3202";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = input3202.insuplc_admdvs;
            post.operatorId = input3202.operatorId;
            post.operatorName = input3202.operatorName;
            post.inModel = 0;

            //上传对账明细
            Post_9101 post9101 = new Post_9101();
            post9101.operatorId = input3202.operatorId;
            post9101.operatorName = input3202.operatorName;
            post9101.insuplc_admdvs = input3202.insuplc_admdvs;
            post9101.fixmedins_code = input3202.clr_type;
            post9101.fileName = input3202.fileName;
            post9101.dtIn = ClassSqlHelper.QueryReconciliation(input3202, 0);
            string file = FsUploadIn_9101(post9101);
            Output_9101 OutModlem = (Output_9101)JsonConvert.DeserializeObject(file);


            /*
             1  setl_optins  结算经办机构  字符型  6  Y
            2  file_qury_no  文件查询号  字符型  30  Y上传明细文件后返回的号码
            3  stmt_begndate  对账开始日期  日期型  Yyyyy-MM-dd
            4  stmt_enddate  对账结束日期  日期型  Yyyyy-MM-dd
            5  medfee_sumamt  医疗费总额  数值型  16,2  Y 
            6  fund_pay_sumamt  基金支付总额  数值型  16,2  Y 
            7  cash_payamt  现金支付金额  数值型  16,2  Y 
            8  fixmedins_setl_cnt定点医药机构结算笔数数值型  10  Y
             */
            Input_3202 input = new Input_3202();
            DataTable dtMain = ClassSqlHelper.QueryReconciliation(input3202, 0);
            input.data = new data3202();
            input.data.setl_optins = Convert.ToString(dtMain.Rows[0]["setl_optins"]);

            input.data.file_qury_no = input3202.fileName;
            input.data.stmt_begndate = Convert.ToDateTime(dtMain.Rows[0]["stmt_begndate"]).ToString("yyyy-MM-dd");
            input.data.stmt_enddate = Convert.ToDateTime(dtMain.Rows[0]["stmt_enddate"]).ToString("yyyy-MM-dd");
            input.data.medfee_sumamt = Convert.ToDecimal(dtMain.Rows[0]["medfee_sumamt"]);
            input.data.medfee_sumamt = Convert.ToDecimal(dtMain.Rows[0]["fund_pay_sumamt"]);
            input.data.medfee_sumamt = Convert.ToDecimal(dtMain.Rows[0]["cash_payamt"]);
            input.data.medfee_sumamt = Convert.ToInt32(dtMain.Rows[0]["fixmedins_setl_cnt"]);
            Output_3202 output = new Output_3202();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            return json;
        }
        /// <summary>
        /// 【3501】商品盘存上传
        /// </summary>
        /// <param name="post3501"></param>
        /// <returns></returns>
        [HttpPost]
        public string InventoryUpload_3501(Post_3501 post3501)
        {
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "3501";
            post.insuplc_admdvs = ConfigurationManager.AppSettings["mdtrtarea_admvs"];
            post.inModel = 0;
            post.operatorId = post3501.operatorId;
            post.operatorName = post3501.operatorName;
            string orgId = ConfigurationManager.AppSettings["orgId"];
            string ddyyid = ConfigurationManager.AppSettings["fixmedins_code"];
            string ddyymc = ConfigurationManager.AppSettings["fixmedins_name"];
            Input_3501 input3501 = new Input_3501();
            DataTable dtDiseinfo = ClassSqlHelper.QueryInventory3501(orgId);
            string json = "";
            for (int i = 0; i < dtDiseinfo.Rows.Count; i++)
            {
                input3501.invinfo = new invinfo3501();
                input3501.invinfo = Function.ToList<invinfo3501>(dtDiseinfo)[i];
                Output_null output = new Output_null();
                string code = "1";
                json = ClassHelper.SaveToInterface1(input3501, out output, post, out code);
                try
                {
                    ClassSqlHelper.DeleteInventory(dtDiseinfo.Rows[i]["mlbm_id"].ToString(), post.tradiNumber);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("商品盘存上传成功，HIS删除数据异常：" + ex.Message);
                    return ClassHelper.BuildReturnJson("-99", "商品盘存上传成功，HIS删除数据异常：" + ex.Message);
                }
                if (code == "0")//如果成功则更新本地信息表 
                {
                    try
                    {
                        DateTime date = ClassSqlHelper.GetServerTime();
                        int eeor = 0;
                        List<string> sqlList = new List<string>();
                        Drjk_jxcsc_output jxcsc = new Drjk_jxcsc_output();
                        jxcsc.mlbm_id = dtDiseinfo.Rows[i]["mlbm_id"].ToString();//盘点明细id
                        jxcsc.xm_id = input3501.invinfo.fixmedins_hilist_id;//项目编码
                        jxcsc.OrganizeId = orgId;
                        jxcsc.OrganizeName = ddyymc;
                        jxcsc.type = "3501";//接口交易编号
                        jxcsc.issuccess = "True";//成功
                        jxcsc.log = json;//接口出参内容
                        jxcsc.czydm = post.operatorId;
                        jxcsc.czrq = date;
                        jxcsc.zt = 1;
                        sqlList.Add(jxcsc.ToAddSql());
                        ClassSqlHelper.Merge(sqlList, out eeor);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Info("进销存接口3501商品盘存上传成功，本地保存失败，数据异常：" + ex.Message);
                        return ClassHelper.BuildReturnJson("-99", "进销存接口3501商品盘存上传成功，HIS上传数据失败：" + ex.Message);
                    }
                }
                else
                {//上传失败内容日志记录，可以重新根据id查询数据进行补传
                    try
                    {
                        DateTime date = ClassSqlHelper.GetServerTime();
                        int eeor = 0;
                        List<string> sqlList = new List<string>();
                        Drjk_jxcsc_output jxcsc = new Drjk_jxcsc_output();
                        jxcsc.mlbm_id = dtDiseinfo.Rows[i]["mlbm_id"].ToString();//盘点id
                        jxcsc.xm_id = input3501.invinfo.fixmedins_hilist_id;//项目编码
                        jxcsc.OrganizeId = orgId;
                        jxcsc.OrganizeName = ddyymc;
                        jxcsc.type = "3501";//接口交易编号
                        jxcsc.issuccess = "False";//失败
                        jxcsc.log = json;//接口出参内容
                        jxcsc.czydm = post.operatorId;
                        jxcsc.czrq = date;
                        jxcsc.zt = 1;
                        sqlList.Add(jxcsc.ToAddSql());
                        ClassSqlHelper.Merge(sqlList, out eeor);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Info("进销存接口3501商品盘存上传失败，本地保存失败，数据异常：" + ex.Message);
                        return ClassHelper.BuildReturnJson("-99", "进销存接口3501商品盘存上传失败，HIS上传数据失败：" + ex.Message);
                    }
                }
            }
            return json;
        }
        /// <summary>
        /// 【3502】商品库存变更
        /// </summary>
        /// <param name="post3502"></param>
        /// <returns></returns>
        [HttpPost]
        public string InventoryUpload_3502(Post_3502 post3502)
        {
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "3502";
            post.insuplc_admdvs = ConfigurationManager.AppSettings["mdtrtarea_admvs"];
            post.inModel = 0;
            post.operatorId = post3502.operatorId;
            post.operatorName = post3502.operatorName;
            string orgId = ConfigurationManager.AppSettings["orgId"];
            string ddyyid = ConfigurationManager.AppSettings["fixmedins_code"];
            string ddyymc = ConfigurationManager.AppSettings["fixmedins_name"];
            Input_3502 input3502 = new Input_3502();
            DataTable dtDiseinfo = ClassSqlHelper.QueryInventory3502(orgId);
            string json = "";
            for (int i = 0; i < dtDiseinfo.Rows.Count; i++)
            {
                input3502.invinfo = new invinfo3502();
                input3502.invinfo = Function.ToList<invinfo3502>(dtDiseinfo)[i];
                Output_null output = new Output_null();
                string code = "1";
                json = ClassHelper.SaveToInterface1(input3502, out output, post, out code);
                try
                {
                    ClassSqlHelper.DeleteInventory(dtDiseinfo.Rows[i]["mlbm_id"].ToString(), post.tradiNumber);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("商品库存变更上传成功，HIS删除数据异常：" + ex.Message);
                    return ClassHelper.BuildReturnJson("-99", "商品库存变更上传成功，HIS删除数据异常：" + ex.Message);
                }
                if (code == "0")//如果成功则更新本地信息表 
                {
                    try
                    {
                        DateTime date = ClassSqlHelper.GetServerTime();
                        int eeor = 0;
                        List<string> sqlList = new List<string>();
                        Drjk_jxcsc_output jxcsc = new Drjk_jxcsc_output();
                        jxcsc.mlbm_id = dtDiseinfo.Rows[i]["mlbm_id"].ToString();//出入库明细id
                        jxcsc.xm_id = input3502.invinfo.fixmedins_hilist_id;//项目编码
                        jxcsc.OrganizeId = orgId;
                        jxcsc.OrganizeName = ddyymc;
                        jxcsc.type = "3502";//接口交易编号
                        jxcsc.issuccess = "True";//成功
                        jxcsc.log = json;//接口出参内容
                        jxcsc.czydm = post.operatorId;
                        jxcsc.czrq = date;
                        jxcsc.zt = 1;
                        sqlList.Add(jxcsc.ToAddSql());
                        ClassSqlHelper.Merge(sqlList, out eeor);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Info("进销存接口3502商品库存变更上传成功，本地保存失败，数据异常：" + ex.Message);
                        return ClassHelper.BuildReturnJson("-99", "进销存接口3502商品库存变更上传成功，HIS上传数据失败：" + ex.Message);
                    }
                }
                else
                {//上传失败内容日志记录，可以重新根据id查询数据进行补传
                    try
                    {
                        DateTime date = ClassSqlHelper.GetServerTime();
                        int eeor = 0;
                        List<string> sqlList = new List<string>();
                        Drjk_jxcsc_output jxcsc = new Drjk_jxcsc_output();
                        jxcsc.mlbm_id = dtDiseinfo.Rows[i]["mlbm_id"].ToString();//出入库id
                        jxcsc.xm_id = input3502.invinfo.fixmedins_hilist_id;//项目编码
                        jxcsc.OrganizeId = orgId;
                        jxcsc.OrganizeName = ddyymc;
                        jxcsc.type = "3502";//接口交易编号
                        jxcsc.issuccess = "False";//失败
                        jxcsc.log = json;//接口出参内容
                        jxcsc.czydm = post.operatorId;
                        jxcsc.czrq = date;
                        jxcsc.zt = 1;
                        sqlList.Add(jxcsc.ToAddSql());
                        ClassSqlHelper.Merge(sqlList, out eeor);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Info("进销存接口3502商品库存变更上传失败，本地保存失败，数据异常：" + ex.Message);
                        return ClassHelper.BuildReturnJson("-99", "进销存接口3502商品库存变更上传失败，HIS上传数据失败：" + ex.Message);
                    }
                }
            }
            return json;
        }
        /// <summary>
        /// 【3503】商品采购
        /// </summary>
        /// <param name="post3503"></param>
        /// <returns></returns>
        [HttpPost]
        public string InventoryUpload_3503(Post_3503 post3503)
        {
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "3503";
            post.insuplc_admdvs = ConfigurationManager.AppSettings["mdtrtarea_admvs"];
            post.inModel = 0;
            post.operatorId = post3503.operatorId;
            post.operatorName = post3503.operatorName;
            string orgId = ConfigurationManager.AppSettings["orgId"];
            string ddyyid = ConfigurationManager.AppSettings["fixmedins_code"];
            string ddyymc = ConfigurationManager.AppSettings["fixmedins_name"];
            Input_3503 input3503 = new Input_3503();
            DataTable dtDiseinfo = ClassSqlHelper.QueryInventory3503(orgId);
            string json = "";
            for (int i = 0; i < dtDiseinfo.Rows.Count; i++)
            {
                input3503.purcinfo = new purcinfo3503();
                input3503.purcinfo = Function.ToList<purcinfo3503>(dtDiseinfo)[i];
                Output_null output = new Output_null();
                string code = "1";
                json = ClassHelper.SaveToInterface1(input3503, out output, post, out code);
                try
                {
                    ClassSqlHelper.DeleteInventory(dtDiseinfo.Rows[i]["mlbm_id"].ToString(), post.tradiNumber);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("商商品采购上传成功，HIS删除数据异常：" + ex.Message);
                    return ClassHelper.BuildReturnJson("-99", "商品采购上传成功，HIS删除数据异常：" + ex.Message);
                }
                if (code == "0")//如果成功则更新本地信息表 
                {
                    try
                    {
                        DateTime date = ClassSqlHelper.GetServerTime();
                        int eeor = 0;
                        List<string> sqlList = new List<string>();
                        Drjk_jxcsc_output jxcsc = new Drjk_jxcsc_output();
                        jxcsc.mlbm_id = dtDiseinfo.Rows[i]["mlbm_id"].ToString();//出入库id
                        jxcsc.xm_id = input3503.purcinfo.fixmedins_hilist_id;//项目编码
                        jxcsc.OrganizeId = orgId;
                        jxcsc.OrganizeName = ddyymc;
                        jxcsc.type = "3503";//接口交易编号
                        jxcsc.issuccess = "True";//成功
                        jxcsc.log = json;//接口出参内容
                        jxcsc.czydm = post.operatorId;
                        jxcsc.czrq = date;
                        jxcsc.zt = 1;
                        sqlList.Add(jxcsc.ToAddSql());
                        ClassSqlHelper.Merge(sqlList, out eeor);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Info("进销存接口【3503】商品采购上传成功，本地保存失败，数据异常：" + ex.Message);
                        return ClassHelper.BuildReturnJson("-99", "进销存接口【3503】商品采购上传成功，HIS上传数据失败：" + ex.Message);
                    }
                }
                else
                {//上传失败内容日志记录，可以重新根据id查询数据进行补传
                    try
                    {
                        DateTime date = ClassSqlHelper.GetServerTime();
                        int eeor = 0;
                        List<string> sqlList = new List<string>();
                        Drjk_jxcsc_output jxcsc = new Drjk_jxcsc_output();
                        jxcsc.mlbm_id = dtDiseinfo.Rows[i]["mlbm_id"].ToString();//出入库id
                        jxcsc.xm_id = input3503.purcinfo.fixmedins_hilist_id;//项目编码
                        jxcsc.OrganizeId = orgId;
                        jxcsc.OrganizeName = ddyymc;
                        jxcsc.type = "3503";//接口交易编号
                        jxcsc.issuccess = "False";//失败
                        jxcsc.log = json;//接口出参内容
                        jxcsc.czydm = post.operatorId;
                        jxcsc.czrq = date;
                        jxcsc.zt = 1;
                        sqlList.Add(jxcsc.ToAddSql());
                        ClassSqlHelper.Merge(sqlList, out eeor);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Info("进销存接口【3503】商品采购上传失败，本地保存失败，数据异常：" + ex.Message);
                        return ClassHelper.BuildReturnJson("-99", "进销存接口【3503】商品采购上传失败，HIS上传数据失败：" + ex.Message);
                    }
                }
            }
            return json;
        }
        /// <summary>
        /// 【3504】商品采购退货
        /// </summary> 
        /// <param name="post3504"></param>
        /// <returns></returns>
        [HttpPost]
        public string InventoryUpload_3504(Post_3504 post3504)
        {
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "3504";
            post.insuplc_admdvs = ConfigurationManager.AppSettings["mdtrtarea_admvs"];
            post.inModel = 0;
            post.operatorId = post3504.operatorId;
            post.operatorName = post3504.operatorName;
            string orgId = ConfigurationManager.AppSettings["orgId"];
            string ddyyid = ConfigurationManager.AppSettings["fixmedins_code"];
            string ddyymc = ConfigurationManager.AppSettings["fixmedins_name"];
            Input_3504 input3504 = new Input_3504();
            DataTable dtDiseinfo = ClassSqlHelper.QueryInventory3504(orgId);
            string json = "";

            for (int i = 0; i < dtDiseinfo.Rows.Count; i++)
            {
                input3504.purcinfo = new purcinfo3504();
                input3504.purcinfo = Function.ToList<purcinfo3504>(dtDiseinfo)[i];
                Output_null output = new Output_null();
                string code = "1";
                json = ClassHelper.SaveToInterface1(input3504, out output, post, out code);
                try
                {
                    AppLogger.Info("HIS删除商品采购退货数据：" + dtDiseinfo.Rows[i]["mlbm_id"].ToString() + " 类型：" + post.tradiNumber);
                    ClassSqlHelper.DeleteInventory(dtDiseinfo.Rows[i]["mlbm_id"].ToString(), post.tradiNumber);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("商品采购退货上传成功，HIS删除数据异常：" + ex.Message);
                    return ClassHelper.BuildReturnJson("-99", "商品采购退货上传成功，HIS删除数据异常：" + ex.Message);
                }
                if (code == "0")//如果成功则更新本地信息表 
                {
                    try
                    {
                        DateTime date = ClassSqlHelper.GetServerTime();
                        int eeor = 0;
                        List<string> sqlList = new List<string>();
                        Drjk_jxcsc_output jxcsc = new Drjk_jxcsc_output();
                        jxcsc.mlbm_id = dtDiseinfo.Rows[i]["mlbm_id"].ToString();//出入库id
                        jxcsc.xm_id = input3504.purcinfo.fixmedins_hilist_id;//项目编码
                        jxcsc.OrganizeId = orgId;
                        jxcsc.OrganizeName = ddyymc;
                        jxcsc.type = "3504";//接口交易编号
                        jxcsc.issuccess = "True";//成功
                        jxcsc.log = json;//接口出参内容
                        jxcsc.czydm = post.operatorId;
                        jxcsc.czrq = date;
                        jxcsc.zt = 1;
                        sqlList.Add(jxcsc.ToAddSql());
                        ClassSqlHelper.Merge(sqlList, out eeor);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Info("进销存接口【3504】商品采购退货上传成功，本地保存失败，数据异常：" + ex.Message);
                        return ClassHelper.BuildReturnJson("-99", "进销存接口【3504】商品采购退货上传成功，HIS上传数据失败：" + ex.Message);
                    }
                }
                else
                {//上传失败内容日志记录，可以重新根据id查询数据进行补传
                    try
                    {
                        DateTime date = ClassSqlHelper.GetServerTime();
                        int eeor = 0;
                        List<string> sqlList = new List<string>();
                        Drjk_jxcsc_output jxcsc = new Drjk_jxcsc_output();
                        jxcsc.mlbm_id = dtDiseinfo.Rows[i]["mlbm_id"].ToString();//出入库id
                        jxcsc.xm_id = input3504.purcinfo.fixmedins_hilist_id;//项目编码
                        jxcsc.OrganizeId = orgId;
                        jxcsc.OrganizeName = ddyymc;
                        jxcsc.type = "3504";//接口交易编号
                        jxcsc.issuccess = "False";//失败
                        jxcsc.log = json;//接口出参内容
                        jxcsc.czydm = post.operatorId;
                        jxcsc.czrq = date;
                        jxcsc.zt = 1;
                        sqlList.Add(jxcsc.ToAddSql());
                        ClassSqlHelper.Merge(sqlList, out eeor);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Info("进销存接口【3504】商品采购退货上传失败，本地保存失败，数据异常：" + ex.Message);
                        return ClassHelper.BuildReturnJson("-99", "进销存接口【3504】商品采购退货上传失败，HIS上传数据失败：" + ex.Message);
                    }
                }
            }
            return json;
        }
        /// <summary>
        /// 【3505】商品销售
        /// </summary> 
        /// <param name="post3505"></param>
        /// <returns></returns>
        [HttpPost]
        public string InventoryUpload_3505(Post_3505 post3505)
        {
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "3505";
            post.insuplc_admdvs = ConfigurationManager.AppSettings["mdtrtarea_admvs"];
            post.inModel = 0;
            post.operatorId = post3505.operatorId;
            post.operatorName = post3505.operatorName;
            string orgId = ConfigurationManager.AppSettings["orgId"];
            string ddyyid = ConfigurationManager.AppSettings["fixmedins_code"];
            string ddyymc = ConfigurationManager.AppSettings["fixmedins_name"];
            Input_3505 input3505 = new Input_3505();
            DataTable dtDiseinfo = ClassSqlHelper.QueryInventory3505(orgId);
            string json = "";
            for (int i = 0; i < dtDiseinfo.Rows.Count; i++)
            {
                input3505.selinfo = new purcinfo3505();
                input3505.selinfo = Function.ToList<purcinfo3505>(dtDiseinfo)[i];
                Output_null output = new Output_null();
                string code = "1";
                json = ClassHelper.SaveToInterface1(input3505, out output, post, out code);
                try
                {
                    ClassSqlHelper.DeleteInventory(dtDiseinfo.Rows[i]["mlbm_id"].ToString(), post.tradiNumber);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("商品采购退货上传成功，HIS删除数据异常：" + ex.Message);
                    return ClassHelper.BuildReturnJson("-99", "商品采购退货上传成功，HIS删除数据异常：" + ex.Message);
                }
                if (code == "0")//如果成功则更新本地信息表 
                {
                    try
                    {
                        DateTime date = ClassSqlHelper.GetServerTime();
                        int eeor = 0;
                        List<string> sqlList = new List<string>();
                        Drjk_jxcsc_output jxcsc = new Drjk_jxcsc_output();
                        jxcsc.mlbm_id = dtDiseinfo.Rows[i]["mlbm_id"].ToString();//唯一号
                        jxcsc.xm_id = input3505.selinfo.fixmedins_hilist_id;//项目编码
                        jxcsc.OrganizeId = orgId;
                        jxcsc.OrganizeName = ddyymc;
                        jxcsc.type = post.tradiNumber;//接口交易编号
                        jxcsc.issuccess = "True";//成功
                        jxcsc.log = json;//接口出参内容
                        jxcsc.czydm = post.operatorId;
                        jxcsc.czrq = date;
                        jxcsc.zt = 1;
                        sqlList.Add(jxcsc.ToAddSql());
                        ClassSqlHelper.Merge(sqlList, out eeor);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Info("进销存接口【3505】商品销售上传成功，本地保存失败，数据异常：" + ex.Message);
                        return ClassHelper.BuildReturnJson("-99", "进销存接口【3505】商品销售上传成功，HIS上传数据失败：" + ex.Message);
                    }
                }
                else
                {//上传失败内容日志记录，可以重新根据id查询数据进行补传
                    try
                    {
                        DateTime date = ClassSqlHelper.GetServerTime();
                        int eeor = 0;
                        List<string> sqlList = new List<string>();
                        Drjk_jxcsc_output jxcsc = new Drjk_jxcsc_output();
                        jxcsc.mlbm_id = dtDiseinfo.Rows[i]["mlbm_id"].ToString();//批次流水号
                        jxcsc.xm_id = input3505.selinfo.fixmedins_hilist_id;//项目编码
                        jxcsc.OrganizeId = orgId;
                        jxcsc.OrganizeName = ddyymc;
                        jxcsc.type = post.tradiNumber;//接口交易编号
                        jxcsc.issuccess = "False";//失败
                        jxcsc.log = json;//接口出参内容
                        jxcsc.czydm = post.operatorId;
                        jxcsc.czrq = date;
                        jxcsc.zt = 1;
                        sqlList.Add(jxcsc.ToAddSql());
                        ClassSqlHelper.Merge(sqlList, out eeor);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Info("进销存接口【3505】商品销售上传失败，本地保存失败，数据异常：" + ex.Message);
                        return ClassHelper.BuildReturnJson("-99", "进销存接口【3505】商品销售上传失败，HIS上传数据失败：" + ex.Message);
                    }
                }
            }
            return json;
        }
        /// <summary>
        /// 【3506】商品销售退货
        /// </summary> 
        /// <param name="post3506"></param>
        /// <returns></returns>
        [HttpPost]
        public string InventoryUpload_3506(Post_3505 post3505)
        {
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "3506";
            post.insuplc_admdvs = ConfigurationManager.AppSettings["mdtrtarea_admvs"];
            post.inModel = 0;
            post.operatorId = post3505.operatorId;
            post.operatorName = post3505.operatorName;
            string orgId = ConfigurationManager.AppSettings["orgId"];
            string ddyyid = ConfigurationManager.AppSettings["fixmedins_code"];
            string ddyymc = ConfigurationManager.AppSettings["fixmedins_name"];
            Input_3506 input3506 = new Input_3506();
            DataTable dtDiseinfo = ClassSqlHelper.QueryInventory3506(orgId);
            string json = "";
            for (int i = 0; i < dtDiseinfo.Rows.Count; i++)
            {
                input3506.selinfo = new purcinfo3506();
                input3506.selinfo = Function.ToList<purcinfo3506>(dtDiseinfo)[i];
                Output_null output = new Output_null();
                string code = "1";
                json = ClassHelper.SaveToInterface1(input3506, out output, post, out code);
                try
                {
                    ClassSqlHelper.DeleteInventory(dtDiseinfo.Rows[i]["mlbm_id"].ToString(), post.tradiNumber);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("商品采购退货上传成功，HIS删除数据异常：" + ex.Message);
                    return ClassHelper.BuildReturnJson("-99", "商品采购退货上传成功，HIS删除数据异常：" + ex.Message);
                }
                if (code == "0")//如果成功则更新本地信息表 
                {
                    try
                    {
                        DateTime date = ClassSqlHelper.GetServerTime();
                        int eeor = 0;
                        List<string> sqlList = new List<string>();
                        Drjk_jxcsc_output jxcsc = new Drjk_jxcsc_output();
                        jxcsc.mlbm_id = dtDiseinfo.Rows[i]["mlbm_id"].ToString();//批次流水号
                        jxcsc.xm_id = input3506.selinfo.fixmedins_hilist_id;//项目编码
                        jxcsc.OrganizeId = orgId;
                        jxcsc.OrganizeName = ddyymc;
                        jxcsc.type = post.tradiNumber;//接口交易编号
                        jxcsc.issuccess = "True";//成功
                        jxcsc.log = json;//接口出参内容
                        jxcsc.czydm = post.operatorId;
                        jxcsc.czrq = date;
                        jxcsc.zt = 1;
                        sqlList.Add(jxcsc.ToAddSql());
                        ClassSqlHelper.Merge(sqlList, out eeor);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Info("进销存接口【3506】商品销售退货上传成功，本地保存失败，数据异常：" + ex.Message);
                        return ClassHelper.BuildReturnJson("-99", "进销存接口【3506】商品销售退货上传成功，HIS上传数据失败：" + ex.Message);
                    }
                }
                else
                {//上传失败内容日志记录，可以重新根据id查询数据进行补传
                    try
                    {
                        DateTime date = ClassSqlHelper.GetServerTime();
                        int eeor = 0;
                        List<string> sqlList = new List<string>();
                        Drjk_jxcsc_output jxcsc = new Drjk_jxcsc_output();
                        jxcsc.mlbm_id = dtDiseinfo.Rows[i]["mlbm_id"].ToString();//批次流水号
                        jxcsc.xm_id = input3506.selinfo.fixmedins_hilist_id;//项目编码
                        jxcsc.OrganizeId = orgId;
                        jxcsc.OrganizeName = ddyymc;
                        jxcsc.type = post.tradiNumber;//接口交易编号
                        jxcsc.issuccess = "False";//失败
                        jxcsc.log = json;//接口出参内容
                        jxcsc.czydm = post.operatorId;
                        jxcsc.czrq = date;
                        jxcsc.zt = 1;
                        sqlList.Add(jxcsc.ToAddSql());
                        ClassSqlHelper.Merge(sqlList, out eeor);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Info("进销存接口【3506】商品销售退货上传失败，本地保存失败，数据异常：" + ex.Message);
                        return ClassHelper.BuildReturnJson("-99", "进销存接口【3506】商品销售退货上传失败，HIS上传数据失败：" + ex.Message);
                    }
                }
            }
            return json;
        }
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="input9101"></param>
        /// <returns></returns>
        public string FsUploadIn_9101(Post_9101 input9101)
        {
            //签到
            //GetSigin(input9101.operatorId, input9101.operatorName);

            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "9101";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = input9101.insuplc_admdvs;
            post.operatorId = input9101.operatorId;
            post.operatorName = input9101.operatorName;
            post.inModel = 0;
            string txt = System.Environment.CurrentDirectory + input9101.fileName;
            Function.WriteTXT(txt + ".txt", input9101.dtIn);
            System.IO.Compression.ZipFile.CreateFromDirectory(txt, txt + ".zip"); //压缩
            byte[] buffer = File.ReadAllBytes(txt + ".zip");
            //System.IO.Compression.ZipFile.ExtractToDirectory(@"e:\test\test.zip", @"e:\test"); //解压
            Input_9101 input = new Input_9101();

            input.fsUploadIn = new fsUploadIn();
            input.fsUploadIn.filename = input9101.fileName;
            input.fsUploadIn.fixmedins_code = input9101.fixmedins_code;
            input.fsUploadIn.@in = Convert.ToBase64String(buffer);

            Output_9101 output = new Output_9101();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            return json;
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="input9102"></param>
        /// <returns></returns>
        public string FsDownloadIn(Post_9102 input9102)
        {
            //签到
            //GetSigin(input9102.operatorId, input9102.operatorName);

            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "9102";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = "";
            post.inModel = 0;
            post.operatorId = input9102.operatorId;
            post.operatorName = input9102.operatorName;

            Input_9102 input = new Input_9102();
            input.fsDownloadIn = new fsUploadIn9102();
            input.fsDownloadIn.filename = input9102.filename;
            input.fsDownloadIn.fixmedins_code = input9102.fixmedins_code;
            input.fsDownloadIn.file_qury_no = input9102.file_qury_no;

            Output_9102 output = new Output_9102();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            return json;
        }

        [HttpPost]
        public string UpPassword_1163(PostBase post)
        {

            post.hisId = "0";
            post.tradiNumber = "1163";
            //post.sign_no = sign_no;
            post.insuplc_admdvs = "";
            post.inModel = 0;

            Input_null input = new Input_null();
            Output_null output = new Output_null();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            return json;
        }
        /// <summary>
        /// 20 01 人员待遇享受检查
        /// </summary>
        /// <param name="post2001"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetPersonnelTreatment_2001(Post_2001 post2001)
        {
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "2001";
            post.insuplc_admdvs = "";
            post.inModel = 0;
            post.operatorId = post2001.operatorId;
            post.operatorName = post2001.operatorName;

            Input_2001 input2001 = new Input_2001();
            input2001.data = new data2001();
            input2001.data.psn_no = post2001.psn_no;
            input2001.data.insutype = post2001.insutype;
            input2001.data.fixmedins_code = ConfigurationManager.AppSettings["setl_optins"];
            input2001.data.med_type = post2001.med_type;
            input2001.data.begntime = DateTime.Now.ToString("yyyy-MM-dd");
            input2001.data.endtime = "";
            input2001.data.dise_codg = "";
            input2001.data.dise_name = "";
            input2001.data.oprn_oprt_code = "";
            input2001.data.oprn_oprt_name = "";
            input2001.data.matn_type = "";
            input2001.data.birctrl_type = "";

            Output_null output = new Output_null();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input2001, out output, post, out code);
            return json;
        }
        /// <summary>
        /// 【5201】就诊信息查询
        /// </summary>
        /// <param name="post5201"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetRegistration_5201(Post_5201 post5201)
        {
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "5201";
            post.insuplc_admdvs = post5201.insuplc_admdvs; ;
            post.inModel = 0;
            post.operatorId = post5201.operatorId;
            post.operatorName = post5201.operatorName;

            Input_5201 input5201 = new Input_5201();
            input5201.data = new data5201();
            input5201.data.psn_no = post5201.psn_no;
            input5201.data.med_type = post5201.med_type;
            input5201.data.mdtrt_id = post5201.mdtrt_id;
            input5201.data.begntime = post5201.begntime;
            input5201.data.endtime = post5201.endtime; ;

            Output_null output = new Output_null();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input5201, out output, post, out code);
            return json;
        }
        /// <summary>
        /// 5202诊断信息查询
        /// </summary>
        /// <param name="post5202"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetDiagnosis_5202(Post_5202 post5202)
        {
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "5202";
            post.insuplc_admdvs = post5202.insuplc_admdvs; ;
            post.inModel = 0;
            post.operatorId = post5202.operatorId;
            post.operatorName = post5202.operatorName;

            Input_5202 input5202 = new Input_5202();
            input5202.data = new data5202(); ;
            input5202.data.psn_no = post5202.psn_no;
            input5202.data.mdtrt_id = post5202.mdtrt_id;

            Output_null output = new Output_null();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input5202, out output, post, out code);
            return json;
        }

        /// <summary>
        /// 5203结算信息查询
        /// </summary>
        /// <param name="post5203"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetHospitaSettlement_5203(Post_5203 post5203)
        {
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "5203";
            post.insuplc_admdvs = post5203.insuplc_admdvs; ;
            post.inModel = 0;
            post.operatorId = post5203.operatorId;
            post.operatorName = post5203.operatorName;

            Input_5203 input5203 = new Input_5203();
            input5203.data = new data5203(); ;
            input5203.data.psn_no = post5203.psn_no;
            input5203.data.mdtrt_id = post5203.mdtrt_id;
            input5203.data.setl_id = post5203.setl_id;

            Output_null output = new Output_null();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input5203, out output, post, out code);
            return json;
        }
        /// <summary>
        /// 5205 人员慢特病用药记录查询
        /// </summary>
        /// <param name="post5205"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetSpecialDisease_5205(Post_5205 post5205)
        {
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "5205";
            post.insuplc_admdvs = post5205.insuplc_admdvs;
            post.inModel = 0;
            post.operatorId = post5205.operatorId;
            post.operatorName = post5205.operatorName;


            Input_5205 input5205 = new Input_5205();
            input5205.data = new data5205(); ;
            input5205.data.psn_no = post5205.psn_no;
            input5205.data.begntime = post5205.begntime;
            input5205.data.endtime = post5205.endtime;

            Output_null output = new Output_null();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input5205, out output, post, out code);
            return json;
        }
        /// <summary>
        /// 5206 人员累计信息查询
        /// </summary>
        /// <param name="post5206"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetPeopleTotalInfo_5206(Post_5206 post5206)
        {
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "5206";
            post.insuplc_admdvs = post5206.insuplc_admdvs;
            post.inModel = 0;
            post.operatorId = post5206.operatorId;
            post.operatorName = post5206.operatorName;


            Input_5206 input5206 = new Input_5206();
            input5206.data = new data5206(); ;
            input5206.data.psn_no = post5206.psn_no;
            input5206.data.cum_ym = post5206.cum_ym == null ? "" : post5206.cum_ym;


            Output_null output = new Output_null();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input5206, out output, post, out code);
            return json;
        }
        /// <summary>
        /// 5302 人员定点信息查询
        /// </summary>
        /// <param name="post5302"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetPersonnel_5302(Post_5302 post5302)
        {
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "5302";
            post.insuplc_admdvs = post5302.insuplc_admdvs;
            post.inModel = 0;
            post.operatorId = post5302.operatorId;
            post.operatorName = post5302.operatorName;


            Input_5302 input5302 = new Input_5302();
            input5302.data = new data5302(); ;
            input5302.data.psn_no = post5302.psn_no;
            input5302.data.biz_appy_type = post5302.biz_appy_type;


            Output_null output = new Output_null();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input5302, out output, post, out code);
            return json;
        }
        /// <summary>
        /// 5303 在院信息查询
        /// </summary>
        /// <param name="post5303"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetInHospital_5303(Post_5304 post5303)
        {
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "5303";
            post.insuplc_admdvs = post5303.insuplc_admdvs;
            post.inModel = 0;
            post.operatorId = post5303.operatorId;
            post.operatorName = post5303.operatorName;


            Input_5304 input5303 = new Input_5304();
            input5303.data = new data5304();
            input5303.data.psn_no = post5303.psn_no == null ? "" : post5303.psn_no;
            input5303.data.begntime = post5303.begntime;
            input5303.data.endtime = post5303.endtime;


            Output_null output = new Output_null();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input5303, out output, post, out code);
            return json;
        }
        /// <summary>
        /// 5304 转院信息查询
        /// </summary>
        /// <param name="post5303"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetTurnHospital_5304(Post_5304 post5303)
        {
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "5304";
            post.insuplc_admdvs = post5303.insuplc_admdvs;
            post.inModel = 0;
            post.operatorId = post5303.operatorId;
            post.operatorName = post5303.operatorName;


            Input_5304 input5303 = new Input_5304();
            input5303.data = new data5304();
            input5303.data.psn_no = post5303.psn_no == null ? "" : post5303.psn_no;
            input5303.data.begntime = post5303.begntime;
            input5303.data.endtime = post5303.endtime;


            Output_null output = new Output_null();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input5303, out output, post, out code);
            return json;
        }
        /// <summary>
        /// 医保结算清单信息上传
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public string PostMedicalSettlement_4101(PostBase post)
        {

            post.inModel = 1;
            post.tradiNumber = "4101";
            Input_4101 input4101 = new Input_4101();
            input4101.setlinfo = new setlinfo();
            input4101.icuinfo = new List<icuinfo>();
            input4101.iteminfo = new List<iteminfo>();
            input4101.oprninfo = new List<oprninfo>();
            input4101.opspdiseinfo = new List<opspdiseinfo>();
            input4101.payinfo = new List<payinfo>();
            input4101.diseinfo = new List<diseinfo101>();

            //post.hisId = post.hisId;

            string orgId = ConfigurationManager.AppSettings["orgId"];
            List<setlinfo> list = Function.ToList<setlinfo>(ClassSqlHelper.QuerySetlinfo4101(orgId, post.hisId));
            input4101.setlinfo = list[0];
            input4101.iteminfo = Function.ToList<iteminfo>(ClassSqlHelper.QueryIteminfo4101(post.hisId, orgId));
            input4101.oprninfo = Function.ToList<oprninfo>(ClassSqlHelper.QueryOprninfo4101(orgId, post.hisId));
            input4101.payinfo = Function.ToList<payinfo>(ClassSqlHelper.QueryPayinfo4101(orgId, post.hisId));
            input4101.diseinfo = Function.ToList<diseinfo101>(ClassSqlHelper.QueryDiseinfo4101(orgId, post.hisId));

            Output_4101 output = new Output_4101();

            string code = "1";
            string json = ClassHelper.SaveToInterface1(input4101, out output, post, out code);
            if (code != "0")
                return json;


            int eeor = ClassSqlHelper.UpHospitaSettlement4101(post.hisId, output.setl_list_id, post.operatorId);
            if (eeor < 0)
            {
                JObject jsonUP = JObject.Parse(json);
                jsonUP["err_msg"] = jsonUP["err_msg"] + "HIS信息提示：更新本地上传清单流水号失败 drjk_zyjs_output-100";
                jsonUP["infcode"] = "-100";
                return Convert.ToString(jsonUP);
            }
            return json;
        }

        /// <summary>
        /// 4401A 医疗保障基金结算清单
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public string PostMedicalSettlement_4101_V2(PostBase post)
        {

            post.inModel = 1;
            post.tradiNumber = "4101A";
            Input_4101A input4101A = new Input_4101A();
            input4101A.setlinfo = new setlinfoA();
            input4101A.opspdiseinfo = new List<opspdiseinfoA>();
            input4101A.diseinfo = new List<diseinfo101A>();
            input4101A.oprninfo = new List<oprninfoA>();
            input4101A.icuinfo = new List<icuinfoA>();
            input4101A.bldinfo = new List<bldinfoA>();

            string orgId = ConfigurationManager.AppSettings["orgId"];
            List<setlinfoA> list = Function.ToList<setlinfoA>(ClassSqlHelper.QuerySetlinfo4101A(orgId, post.hisId));
            input4101A.setlinfo = list[0];
            input4101A.oprninfo = Function.ToList<oprninfoA>(ClassSqlHelper.QueryOprninfo4101A(orgId, post.hisId));
            input4101A.diseinfo = Function.ToList<diseinfo101A>(ClassSqlHelper.QueryDiseinfo4101A(orgId, post.hisId));

            Output_4101 output = new Output_4101();

            string code = "1";
            string json = ClassHelper.SaveToInterface1(input4101A, out output, post, out code);
            if (code != "0")
                return json;


            int eeor = ClassSqlHelper.UpHospitaSettlement4101(post.hisId, output.setl_list_id, post.operatorId);
            if (eeor < 0)
            {
                JObject jsonUP = JObject.Parse(json);
                jsonUP["err_msg"] = jsonUP["err_msg"] + "HIS信息提示：更新本地上传清单流水号失败 drjk_zyjs_output-100";
                jsonUP["infcode"] = "-100";
                return Convert.ToString(jsonUP);
            }
            return json;
        }


        /// <summary>
        /// 医疗保障基金结算清单信息状态修改
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public string MedicalSettlementUpdate_4102(PostBase post)
        {

            post.inModel = 1;
            post.tradiNumber = "4102";
            Input_4102 input4102 = new Input_4102();
            input4102.data = new data();



            string orgId = ConfigurationManager.AppSettings["orgId"];
            List<stastinfo> list = Function.ToList<stastinfo>(ClassSqlHelper.QueryStastinfo4102(orgId, post.hisId));
            input4102.data.stastinfo = list;

            //List<stastinfo> list = new List<stastinfo>();
            //stastinfo stastinfo = new stastinfo();
            //stastinfo.psn_no = "JM2075450041";
            //stastinfo.setl_id = "300422458037";
            //stastinfo.stas_type = "0";
            //list.Add(stastinfo);
            //input4102.data.stastinfo = list;

            string code = "1";

            Output_4102 output = new Output_4102();
            string json = ClassHelper.SaveToInterface1(input4102, out output, post, out code);
            if (code != "0")
                return json;


            //int eeor = ClassSqlHelper.UpHospitaSettlement4101(post.hisId, output.setl_list_id, post.operatorId);
            //if (eeor < 0)
            //{
            //    JObject jsonUP = JObject.Parse(json);
            //    jsonUP["err_msg"] = jsonUP["err_msg"] + "HIS信息提示：更新本地上传清单流水号失败 drjk_zyjs_output-100";
            //    jsonUP["infcode"] = "-100";
            //    return Convert.ToString(jsonUP);
            //}
            return json;
        }

        public string QueryMedicalSettlement_4103(PostBase post)
        {

            post.inModel = 1;
            post.tradiNumber = "4103";
            Input_4103 input4103 = new Input_4103();
            input4103.data = new data4103();

            string orgId = ConfigurationManager.AppSettings["orgId"];
            List<data4103> list = Function.ToList<data4103>(ClassSqlHelper.QuerySetlinfoData4103(orgId, post.hisId));
            input4103.data = list[0];


            Output_4103 output = new Output_4103();

            string code = "1";
            string json = ClassHelper.SaveToInterface1(input4103, out output, post, out code);
            if (code != "0")
                return json;


            //int eeor = ClassSqlHelper.UpHospitaSettlement4101(post.hisId, output.setl_list_id, post.operatorId);
            //if (eeor < 0)
            //{
            //    JObject jsonUP = JObject.Parse(json);
            //    jsonUP["err_msg"] = jsonUP["err_msg"] + "HIS信息提示：更新本地上传清单流水号失败 drjk_zyjs_output-100";
            //    jsonUP["infcode"] = "-100";
            //    return Convert.ToString(jsonUP);
            //}
            return json;
        }

        /// <summary>
        /// 病案首页上传
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public string PostMedicalRcord_4401(PostBase post)
        {
            //post.hisId = post.hisId;
            post.inModel = 0;
            post.tradiNumber = "4401";
            Input_4401 input4401 = new Input_4401();
            input4401.baseinfo = new baseinfo_4401();
            input4401.diseinfo = new List<diseinfo_4401>();

            input4401.icuinfo = new List<icuinfo_4401>();
            input4401.oprninfo = new List<oprninfo_4401>();

            string orgId = ConfigurationManager.AppSettings["orgId"];
            string orgcode = ConfigurationManager.AppSettings["fixmedins_code"];
            List<baseinfo_4401> list = Function.ToList<baseinfo_4401>(ClassSqlHelper.QueryBaseinfo4401(orgId, post.hisId, orgcode));
            input4401.baseinfo = list[0];
            input4401.oprninfo = Function.ToList<oprninfo_4401>(ClassSqlHelper.QueryOprninfo4401(orgId, post.hisId));
            input4401.diseinfo = Function.ToList<diseinfo_4401>(ClassSqlHelper.QueryDiseinfo4401(orgId, post.hisId));
            input4401.icuinfo = Function.ToList<icuinfo_4401>(ClassSqlHelper.QueryIcuinfo4401(orgId, post.hisId));

            Output_null output = new Output_null();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input4401, out output, post, out code);
            return json;
        }
        /// <summary>
        ///   【4505】临床检查记录
        /// </summary>
        /// <param name="post4505"></param>
        /// <returns></returns>
        public string PostPathologyCheck_4505(Post_4505 post4505)
        {
            Random ran = new Random();
            int RandKey = ran.Next(100, 9999);

            PostBase post = new PostBase();
            post.hisId = post4505.hisId;
            post.tradiNumber = "4505";
            post.insuplc_admdvs = post4505.insuplc_admdvs;
            post.inModel = 0;
            post.operatorId = post4505.operatorId;
            post.operatorName = post4505.operatorName;

            post.inModel = 0;
            post.tradiNumber = "4505";
            Input_4505 input = new Input_4505();
            input.data = new data_4505();
            input.data.mdtrt_sn = post4505.hisId + RandKey.ToString();
            input.data.mdtrt_id = post4505.mdtrt_id;
            input.data.psn_no = post4505.psn_no;
            input.data.palg_no = post4505.hisId;
            input.data.frozen_no = post4505.hisId;
            input.data.cma_date = DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd");
            input.data.rpot_date = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            input.data.cma_matl = "右小腿溃疡组织";
            input.data.clnc_dise = "皮肤癌";
            input.data.exam_fnd = "皮肤溃疡组织2块，0.5*0.5cm，0.4*0.6cm，表面有结节，质地硬。";
            input.data.sabc = "GFAP(+)、Ki67(约10%+)";
            input.data.palg_diag = "右小腿皮肤癌";
            input.data.rpot_doc = "何霞";
            input.data.vali_flag = "1";
            Output_null output = new Output_null();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input, out output, post, out code);
            return json;
        }

        /// <summary>
        /// 【4402】住院医嘱记录
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public string PostDoctorAdvice_4402(PostBase post)
        {
            string orgId = ConfigurationManager.AppSettings["orgId"];
            post.inModel = 0;
            post.tradiNumber = "4402";

            //post.hisId = "00058";
            Input_4402 input4402 = new Input_4402();
            input4402.data = new List<data_4402>();
            input4402.data = Function.ToList<data_4402>(ClassSqlHelper.QueryMedicalRecords4402(orgId, post.hisId));


            Output_null output = new Output_null();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input4402, out output, post, out code);
            return json;
        }

        /// <summary>
        ///  】 临床检查报告记录
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public string PostClinicalExamination_4501(PostBase post)
        {
            //post.hisId = "00059";
            post.inModel = 0;
            post.tradiNumber = "4501";
            Input_4501 input4501 = new Input_4501();
            input4501.examinfo = new examinfo_4501();
            input4501.sampleinfo = new List<sampleinfo_4501>();
            input4501.imageinfo = new List<imageinfo_4501>();
            input4501.iteminfo = new List<iteminfo_4501>(); ;

            string orgId = ConfigurationManager.AppSettings["orgId"];
            List<examinfo_4501> list = Function.ToList<examinfo_4501>(ClassSqlHelper.QueryExaminfo4501(orgId, post.hisId));
            if (list.Count < 1)
            {
                return ClassHelper.BuildReturnJson("-99", "该病人无临床检查报告");
            }
            input4501.examinfo = list[0];
            input4501.sampleinfo = Function.ToList<sampleinfo_4501>(ClassSqlHelper.QuerySampleinfo4501(orgId, post.hisId));
            input4501.imageinfo = Function.ToList<imageinfo_4501>(ClassSqlHelper.QueryImageinfo4501(orgId, post.hisId));
            input4501.iteminfo = Function.ToList<iteminfo_4501>(ClassSqlHelper.QueryIteminfo4501(orgId, post.hisId));
            Output_null output = new Output_null();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input4501, out output, post, out code);
            return json;
        }

        /// <summary>
        ///  】 临床检验报告记录
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public string PostClinicalExamination_4502(PostBase post)
        {
            //post.hisId = post.hisId;
            post.inModel = 0;
            post.tradiNumber = "4502";
            //Input_4502 input4502 = new Input_4502();
            //input4502.labinfo = new labinfo_4502();
            //input4502.iteminfo = new List<iteminfo_4502>();
            //input4502.sampleinfo = new List<sampleinfo_4502>();

            string orgId = ConfigurationManager.AppSettings["orgId"];
            List<labinfo_4502> list = Function.ToList<labinfo_4502>(ClassSqlHelper.QueryLabinfo4502(orgId, post.hisId));
            Input_4502 input4502 = new Input_4502();
            input4502.labinfo = new labinfo_4502();
            input4502.iteminfo = new List<iteminfo_4502>();
            input4502.sampleinfo = new List<sampleinfo_4502>();
            if (list.Count < 1)
            {
                return ClassHelper.BuildReturnJson("-99", "该病人无临床检验报告");
            }
            input4502.labinfo = list[0];
            input4502.iteminfo = Function.ToList<iteminfo_4502>(ClassSqlHelper.QueryIteminfo4502(orgId, post.hisId));
            input4502.sampleinfo = Function.ToList<sampleinfo_4502>(ClassSqlHelper.QuerySampleinfo4502(orgId, post.hisId));
            Output_null output = new Output_null();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input4502, out output, post, out code);
            return json;
        }
        /// <summary>
        /// 【4701】电子病历上传
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public string PostMedicalRecords_4701(PostBase post)
        {
            post.inModel = 0;
            post.tradiNumber = "4701";
            //post.hisId = "00056";
            Input_4701 input4701 = new Input_4701();
            input4701.adminfo = new adminfo_4701();
            input4701.coursrinfo = new List<coursrinfo_4701>();
            input4701.dieinfo = new List<dieinfo_4701>();
            input4701.diseinfo = new List<diseinfo_4701>();
            input4701.dscginfo = new List<dscginfo_4701>();
            input4701.oprninfo = new List<oprninfo_4701>();
            input4701.rescinfo = new List<rescinfo_4701>();

            string orgId = ConfigurationManager.AppSettings["orgId"];
            List<adminfo_4701> list = Function.ToList<adminfo_4701>(ClassSqlHelper.QueryAdminfo4701(orgId, post.hisId));
            input4701.adminfo = list[0];
            input4701.coursrinfo = Function.ToList<coursrinfo_4701>(ClassSqlHelper.QueryCoursrinfo4701(orgId, post.hisId));
            input4701.dieinfo = Function.ToList<dieinfo_4701>(ClassSqlHelper.QueryDieinfo4701(orgId, post.hisId));
            input4701.diseinfo = Function.ToList<diseinfo_4701>(ClassSqlHelper.QueryDiseinfo4701(orgId, post.hisId));
            input4701.dscginfo = Function.ToList<dscginfo_4701>(ClassSqlHelper.QueryDscginfo4701(orgId, post.hisId));
            input4701.oprninfo = Function.ToList<oprninfo_4701>(ClassSqlHelper.QueryOprninfo4701(orgId, post.hisId));
            input4701.rescinfo = Function.ToList<rescinfo_4701>(ClassSqlHelper.QueryRescinfo4701(orgId, post.hisId));
            Output_null output = new Output_null();
            string code = "1";
            string json = ClassHelper.SaveToInterface1(input4701, out output, post, out code);
            return json;
        }
        #region 明细审核
        #region 明细审核事前分析服务
        //通过此交易进行事前的明细审核分析。
        [HttpPost]
        public string DetailAuditFront_3101(Receive3101 rv)
        {
            try
            {
                string json = "";
                string code = "1";
                PostBase post = new PostBase();
                post.hisId = rv.zyh;
                post.tradiNumber = "3101";
                post.insuplc_admdvs = "";
                post.inModel = 0;
                string orgId = ConfigurationManager.AppSettings["orgId"];
                post.operatorId = rv.operatorId;
                post.operatorName = rv.operatorName;
                Input_3101 input3101 = new Input_3101();
                //input3101.data = ClassSqlHelper.DetailAuditData3101(rv.zyh, rv.orgId);
                //InputData3101 data = ClassSqlHelper.DetailAuditData3101(rv.zyh, rv.kh, rv.orgId);
                string ddyyid = ConfigurationManager.AppSettings["fixmedins_code"];
                string ddyymc = ConfigurationManager.AppSettings["fixmedins_name"];
                string insuplc_admdvs = ConfigurationManager.AppSettings["mdtrtarea_admvs"];
                List<InputFDD3101> inputFDD3101 = new List<InputFDD3101>();
                inputFDD3101 = Function.ToList<InputFDD3101>(ClassSqlHelper.DetailAuditData3102_fsi_diagnose_dtos(rv.zyh, orgId));
                List<InputFOD3101> inputFOD3101 = new List<InputFOD3101>();
                inputFOD3101 = Function.ToList<InputFOD3101>(ClassSqlHelper.DetailAuditData3101_fsi_order_dtos(rv.zyh, orgId, rv.yzh));

                List<InputFED3101> inputFED3101 = new List<InputFED3101>();
                inputFED3101 = Function.ToList<InputFED3101>(ClassSqlHelper.DetailAuditData3102_fsi_encounter_dtos(rv.zyh, orgId, ddyyid, ddyymc, insuplc_admdvs));
                foreach (var item in inputFED3101)
                {
                    item.fsi_diagnose_dtos = inputFDD3101;
                    item.fsi_order_dtos = inputFOD3101;
                }
                List<InputPD3101> inputPD3101 = new List<InputPD3101>();
                inputPD3101 = Function.ToList<InputPD3101>(ClassSqlHelper.DetailAuditData3102_patient_dtos(rv.zyh, orgId));
                foreach (var item in inputPD3101)
                {
                    item.fsi_encounter_dtos = inputFED3101;
                }
                InputData3101 inputData3101 = new InputData3101();
                inputData3101.patient_dtos = inputPD3101;
                input3101.data = inputData3101;
                input3101.data.trig_scen = "2";
                input3101.data.syscode = "his";
                Output_3101 output3101 = new Output_3101();
                output3101.result = new List<Outputrs3101>(); ;
                json = ClassHelper.SaveToInterface1(input3101, out output3101, post, out code);
                if (code == "0")//如果成功则写入本地信息表 
                {
                    if (inputPD3101 != null)
                    {
                        foreach (var item in inputPD3101)
                        {
                            int reslut = ClassSqlHelper.Inser3101(item, rv.operatorId, rv.zyh);
                            if (reslut < 0)
                            {
                                JObject jsonUP = JObject.Parse(json);
                                jsonUP["err_msg"] = jsonUP["err_msg"] + "HIS信息提示：写入事前审核信息失败Drjk_sqsh_input-100";
                                jsonUP["infcode"] = "-100";
                                return Convert.ToString(jsonUP);
                            }
                        }
                    }
                    if (output3101 != null)
                    {
                        if (output3101.result != null)
                        {
                            if (output3101.result.Count > 0)
                            {
                                foreach (var item in output3101.result)
                                {
                                    string reslut = ClassSqlHelper.InserOutput3101(item, rv.operatorId, rv.zyh, "0");
                                    if (reslut != null && reslut != "")
                                    {
                                        JObject jsonUP = JObject.Parse(json);
                                        jsonUP["err_msg"] = jsonUP["err_msg"] + "HIS信息提示：写入事后审核返回信息失败," + reslut + "-100";
                                        jsonUP["infcode"] = "-100";
                                        return Convert.ToString(jsonUP);
                                    }
                                }
                            }
                        }
                    }
                }
                return json;
            }
            catch (Exception ex)
            {
                AppLogger.Info("明细审核：" + ex.Message);
                return ClassHelper.BuildReturnJson("-99", "明细审核失败：" + ex.Message);
            }

        }
        #endregion

        #region 明细审核事中分析服务
        //通过此交易进行事中的明细审核分析。
        [HttpPost]
        public string Undereview_3102(Receive3102 rv)
        {
            try
            {
                string json = "";
                string code = "1";
                PostBase post = new PostBase();
                post.hisId = rv.zyh;
                post.tradiNumber = "3102";
                post.insuplc_admdvs = "";
                post.inModel = 0;
                post.operatorId = rv.operatorId;
                post.operatorName = rv.operatorName;
                Input_3102 input3102 = new Input_3102();
                string orgId = ConfigurationManager.AppSettings["orgId"];
                string ddyyid = ConfigurationManager.AppSettings["fixmedins_code"];
                string ddyymc = ConfigurationManager.AppSettings["fixmedins_name"];
                string insuplc_admdvs = ConfigurationManager.AppSettings["mdtrtarea_admvs"];
                List<InputFDD3102> inputFDD3102 = new List<InputFDD3102>();
                inputFDD3102 = Function.ToList<InputFDD3102>(ClassSqlHelper.DetailAuditData3102_fsi_diagnose_dtos(rv.zyh, orgId));
                List<InputFOD3102> inputFOD3102 = new List<InputFOD3102>();
                inputFOD3102 = Function.ToList<InputFOD3102>(ClassSqlHelper.DetailAuditData3102_fsi_order_dtos(rv.zyh, orgId));

                List<InputFED3102> inputFED3102 = new List<InputFED3102>();
                inputFED3102 = Function.ToList<InputFED3102>(ClassSqlHelper.DetailAuditData3102_fsi_encounter_dtos(rv.zyh, orgId, ddyyid, ddyymc, insuplc_admdvs));
                foreach (var item in inputFED3102)
                {
                    item.fsi_diagnose_dtos = inputFDD3102;
                    item.fsi_order_dtos = inputFOD3102;
                }
                List<InputPD3102> inputPD3102 = new List<InputPD3102>();
                inputPD3102 = Function.ToList<InputPD3102>(ClassSqlHelper.DetailAuditData3102_patient_dtos(rv.zyh, orgId));
                foreach (var item in inputPD3102)
                {
                    item.fsi_encounter_dtos = inputFED3102;


                }
                InputData3102 inputData3102 = new InputData3102();
                inputData3102.patient_dtos = inputPD3102;
                input3102.data = inputData3102;
                input3102.data.trig_scen = "2";
                input3102.data.syscode = "his";
                Output_3101 output3102 = new Output_3101();
                output3102.result = new List<Outputrs3101>();
                json = ClassHelper.SaveToInterface1(input3102, out output3102, post, out code);
                if (code == "0")//如果成功则写入本地信息表 
                {
                    if (inputPD3102 != null)
                    {
                        foreach (var item in inputPD3102)
                        {
                            int reslut = ClassSqlHelper.Inser3102(item, rv.operatorId, rv.zyh);
                            if (reslut < 0)
                            {
                                JObject jsonUP = JObject.Parse(json);
                                jsonUP["err_msg"] = jsonUP["err_msg"] + "HIS信息提示：写入事中审核信息失败Drjk_szsh_input-100";
                                jsonUP["infcode"] = "-100";
                                return Convert.ToString(jsonUP);
                            }
                        }
                    }
                    if (output3102 != null)
                    {
                        if (output3102.result != null)
                        {
                            if (output3102.result.Count > 0)
                            {
                                foreach (var item in output3102.result)
                                {
                                    string reslut = ClassSqlHelper.InserOutput3101(item, rv.operatorId, rv.zyh, "1");
                                    if (reslut != null && reslut != "")
                                    {
                                        JObject jsonUP = JObject.Parse(json);
                                        jsonUP["err_msg"] = jsonUP["err_msg"] + "HIS信息提示：写入事后审核返回信息失败," + reslut + "-100";
                                        jsonUP["infcode"] = "-100";
                                        return Convert.ToString(jsonUP);
                                    }
                                }
                            }
                        }
                    }
                }
                return json;
            }
            catch (Exception ex)
            {
                AppLogger.Info("事中审核：" + ex.Message);
                return ClassHelper.BuildReturnJson("-99", "事中审核失败：" + ex.Message);
            }

        }
        #endregion

        #endregion
        #region 医保电子处方流转
        #region   电子处方上传预核验
        /// <summary>
		/// 【D001】电子处方上传预核验
		/// </summary>
		/// <param name="post"></param>
		/// <returns></returns>
		public string ElectronicPrescription_D001(Post_D001 post)
        {
            post.inModel = 0;
            post.tradiNumber = "D001";
            string orgId = ConfigurationManager.AppSettings["orgId"];
            Input_D001 inputd001 = new Input_D001();
            List<Input_D001> list = Function.ToList<Input_D001>(ClassSqlHelper.ElectronicPrescription_D001_PrescriptionData(post.hisId, orgId, post.cfh));
            inputd001 = list[0];
            inputd001.rxdrugdetail = Function.ToList<rxdrugdetail>(ClassSqlHelper.ElectronicPrescription_D001_PrescriptionData(post.hisId, orgId, post.cfh));
            List<mdtrtinfoV0> listmdtrtinfo = Function.ToList<mdtrtinfoV0>(ClassSqlHelper.ElectronicPrescription_D001_Prescription_mdtrtData(post.hisId, orgId, post.cfh));
            inputd001.mdtrtinfo = listmdtrtinfo[0];
            inputd001.diseinfo = Function.ToList<diseinfoVO>(ClassSqlHelper.ElectronicPrescription_D001_Prescription_diseData(post.hisId, orgId, post.cfh));
            Output_D001 output = new Output_D001();
            string code = "1";
            string json = ElectronicCSHelper.SaveToInterface1(inputd001, out output, post, out code);
            if (code == "0")//如果成功则更新本地信息表 
            {
                try
                {
                    DateTime date = ClassSqlHelper.GetServerTime();
                    int eeor = 0;
                    List<string> sqlList = new List<string>();
                    Dzcf_D001_output D001 = new Dzcf_D001_output();
                    D001.mzh = post.hisId;//
                    D001.cfh = post.cfh;//
                    D001.OrganizeId = orgId;
                    D001.InputContent = JsonConvert.SerializeObject(inputd001);
                    D001.rxTraceCode = output.rxTraceCode;
                    D001.hiRxno = output.hiRxno;
                    D001.czydm = post.operatorId;
                    D001.czrq = date;
                    D001.zt = 1;
                    sqlList.Add(D001.ToAddSql());
                    ClassSqlHelper.Merge(sqlList, out eeor);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("电子处方上传预核验上传成功，本地保存失败，数据异常：" + ex.Message);
                    return ClassHelper.BuildReturnJson("-99", "电子处方上传预核验上传成功，HIS上传数据失败：" + ex.Message);
                }
            }
            return json;
        }
        #endregion
        #region 电子处方签名

        /// <summary>
        /// 【D002】电子处方医保电子签名
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public string ElectronicPrescription_D002(Post_D002 post)
        {
            string templatePath_Pdf = System.IO.Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName, "Assets", $"dzcf.pdf");
            string outputPath_Pdf = System.IO.Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName, $"Assets/PrescriptionToPDF/" + post.cfh + ".pdf");
            post.inModel = 0;
            post.tradiNumber = "D002";
            string orgId = ConfigurationManager.AppSettings["orgId"];
            var docinfo = GetRecipeDocInfo(post);
            var result = PDFSharpController.ExportDocxByObject(templatePath_Pdf, docinfo);
            result.Save(outputPath_Pdf);
            string originalRxFile = DocumentToBase64Str(outputPath_Pdf);//生成的处方文件转换为base64编码
            Input_D002 input = new Input_D002();
            string fixmedins_code = ConfigurationManager.AppSettings["fixmedins_code"];
            input.fixmedinsCode = fixmedins_code;//机构编码
            Input_D003_Data top20originalValue = new Input_D003_Data();
            top20originalValue = Function.ToList<Input_D003_Data>(ClassSqlHelper.ElectronicPrescription_D003_Prescription_top20(post.hisId, orgId, post.cfh, post.shks, post.shys))[0];
            string originalValue = JsonConvert.SerializeObject(top20originalValue).GetBytes().ToBase64String();//电子处方上传前二十个字段的base64编码值
            input.originalValue = originalValue;//电子处方上传前二十个字段的base64编码值
            input.originalRxFile = originalRxFile;//文件转base64编码值
            Output_D002 output = new Output_D002();
            string code = "1";
            string json = ElectronicCSHelper.SaveToInterface1(input, out output, post, out code);
            if (code == "0")//如果成功则更新本地信息表 
            {
                try
                {
                    DateTime date = ClassSqlHelper.GetServerTime();
                    int eeor = 0;
                    List<string> sqlList = new List<string>();
                    Dzcf_D002_output D002 = new Dzcf_D002_output();
                    D002.mzh = post.hisId;//
                    D002.cfh = post.cfh;//
                    D002.OrganizeId = orgId;
                    D002.InputContent = JsonConvert.SerializeObject(input);
                    D002.originalValue = originalValue;
                    D002.originalRxFile = originalRxFile;
                    D002.rxFile = output.rxFile;
                    D002.signDigest = output.signDigest;
                    D002.signCertSn = output.signCertSn;
                    D002.signCertDn = output.signCertDn;
                    D002.czydm = post.operatorId;
                    D002.czrq = date;
                    D002.zt = 1;
                    sqlList.Add(D002.ToAddSql());
                    ClassSqlHelper.Merge(sqlList, out eeor);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("电子处方医保电子签名成功，本地保存失败，数据异常：" + ex.Message);
                    return ClassHelper.BuildReturnJson("-99", "电子处方医保电子签名成功，HIS上传数据失败：" + ex.Message);
                }
                //post.rxFile = originalRxFile;
                //post.signDigest = output.signDigest;
                //ElectronicPrescription_D003(post);
            }
            return json;
        }
        public static PDFinput GetRecipeDocInfo(Post_D002 post)
        {
            var docinfo = new PDFinput();
            string orgId = ConfigurationManager.AppSettings["orgId"];
            mdtrtinfoV0 mdtrtinfoV0 = Function.ToList<mdtrtinfoV0>(ClassSqlHelper.ElectronicPrescription_D001_Prescription_mdtrtData(post.hisId, orgId, post.cfh))[0];
            List<rxdrugdetail> cfxxdetail = Function.ToList<rxdrugdetail>(ClassSqlHelper.ElectronicPrescription_D001_Prescription_DetailData(post.hisId, orgId, post.cfh));
            docinfo.mzh = mdtrtinfoV0.iptOtpNo;
            docinfo.cfh = post.cfh;
            docinfo.kjrq = mdtrtinfoV0.mdtrtTime;
            docinfo.kb = mdtrtinfoV0.prscDeptName;
            docinfo.xm = mdtrtinfoV0.patnName;
            docinfo.xb = mdtrtinfoV0.gend == "1" ? "男" : "女";
            docinfo.nl = mdtrtinfoV0.patnAge;
            docinfo.fb = "医保";
            docinfo.lczd = mdtrtinfoV0.maindiagName;
            docinfo.kfys = System.IO.Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName, "Assets", $"" + post.kfys + ".jpg");
            docinfo.shys = System.IO.Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName, "Assets", $"" + post.shys + ".jpg");
            List<string> cfxxlist = new List<string>();
            int rows = 1;
            string cfxxstr = "";
            foreach (var item in cfxxdetail)
            {
                cfxxstr = rows + "." + item.drugGenname + "            " + item.drugSpec + "           " + item.drugCnt + item.drugDosunt + "\n" + "用法用量：" + item.medcWayDscr + "    " + item.usedFrquName + "      一次" + item.sinDoscnt + item.sinDosunt;
                cfxxlist.Add(cfxxstr);
                rows += 1;
            }
            docinfo.cfxxList = cfxxlist;
            //docinfo.cfxxList = new List<string>()
            //{
            //    "1.苯磺酸氨氯地平片(京新)(省采)\n 规格：2mg*28片\t×10片\n用法用量：2片/次，每日三次，口服。",
            //    "2.苯磺酸氨氯地平片(络活喜)(非省采)\n 规格：5mg*28片\t×20片\n用法用量：1片/次，每日两次，口服。",
            //};
            docinfo.cfxx = string.Join("\n", docinfo.cfxxList);
            return docinfo;
        }
        //文件全路径：fileName
        public string DocumentToBase64Str(string fileName)
        {
            FileStream filestream = new FileStream(fileName, FileMode.Open);
            byte[] bt = new byte[filestream.Length];
            //调用read读取方法
            filestream.Read(bt, 0, bt.Length);
            string base64Str = Convert.ToBase64String(bt);
            filestream.Close();
            return base64Str;
        }
        #endregion
        #region 电子处方上传
        /// <summary>
		/// 【D003】电子处方上传
		/// </summary>
		/// <param name="post"></param>
		/// <returns></returns>
		public string ElectronicPrescription_D003(Post_D002 post)
        {
            post.inModel = 0;
            post.tradiNumber = "D003";
            string orgId = ConfigurationManager.AppSettings["orgId"];
            Input_D003 input = new Input_D003();
            List<Input_D003> list = Function.ToList<Input_D003>(ClassSqlHelper.ElectronicPrescription_D003_Prescription(post.hisId, orgId, post.cfh, post.shks, post.shys, post.rxFile, post.signDigest));
            input = list[0];
            Output_D003 output = new Output_D003();
            string code = "1";
            string json = ElectronicCSHelper.SaveToInterface1(input, out output, post, out code);
            if (code == "0")//如果成功则更新本地信息表 
            {
                try
                {
                    DateTime date = ClassSqlHelper.GetServerTime();
                    int eeor = 0;
                    List<string> sqlList = new List<string>();
                    Dzcf_D003_output D003 = new Dzcf_D003_output();
                    D003.mzh = post.hisId;
                    D003.cfh = post.cfh;
                    D003.OrganizeId = orgId;
                    D003.InputContent = JsonConvert.SerializeObject(input);
                    D003.rxTraceCode = input.rxTraceCode;
                    D003.hiRxno = output.hiRxno;
                    D003.rxStasCodg = output.rxStasCodg;
                    D003.rxStasName = output.rxStasName;
                    D003.czydm = post.operatorId;
                    D003.czrq = date;
                    D003.zt = 1;
                    sqlList.Add(D003.ToAddSql());
                    ClassSqlHelper.Merge(sqlList, out eeor);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("电子处方上传成功，本地保存失败，数据异常：" + ex.Message);
                    return ClassHelper.BuildReturnJson("-99", "电子处方上传成功，HIS上传数据失败：" + ex.Message);
                }
            }
            return json;
        }
        #endregion
        #region 电子处方撤销
        /// <summary>
		/// 【D004】电子处方撤销
		/// </summary>
		/// <param name="post"></param>
		/// <returns></returns>
		public string ElectronicPrescription_D004(Post_D004 post)
        {
            post.inModel = 0;
            post.tradiNumber = "D004";
            string orgId = ConfigurationManager.AppSettings["orgId"];
            Input_D004 input = new Input_D004();
            List<Input_D004> list = Function.ToList<Input_D004>(ClassSqlHelper.ElectronicPrescription_D004_Prescription(post.hisId, orgId, post.cfh, post.cxys));
            input = list[0];
            input.undoRea = post.cxyy;
            Output_D003 output = new Output_D003();
            string code = "1";
            string json = ElectronicCSHelper.SaveToInterface1(input, out output, post, out code);
            if (code == "0")//如果成功更新本地D003 电子处方上传落地表
            {
                try
                {
                    int errorNo = 0;
                    ClassSqlHelper.RevokePrescriptionToD003(input.hiRxno, post.cxyy, output.rxStasCodg, output.rxStasName, out errorNo);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("撤销电子处方成功，HIS更新电子处方数据表状态异常：" + ex.Message);
                    return ClassHelper.BuildReturnJson("-99", "撤销电子处方成功，HIS更新电子处方数据表状态失败：" + ex.Message);
                }
            }
            return json;
        }
        #endregion
        #region 电子处方信息查询
        /// <summary>
        /// 【D005】电子处方信息查询
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public string ElectronicPrescription_D005(Post_D006 post)
        {
            post.inModel = 0;
            post.tradiNumber = "D005";
            string orgId = ConfigurationManager.AppSettings["orgId"];
            Input_D006 input = new Input_D006();
            List<Input_D006> list = Function.ToList<Input_D006>(ClassSqlHelper.ElectronicPrescription_D006_Prescription(post.hisId, orgId, post.cfh));
            input = list[0];
            Output_D005 output = new Output_D005();
            string code = "1";
            string json = ElectronicCSHelper.SaveToInterface1(input, out output, post, out code);
            return json;
        }
        #endregion
        #region 电子处方审核结果查询
        /// <summary>
        /// 【D006】电子处方审核结果查询
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public string ElectronicPrescription_D006(Post_D006 post)
        {
            post.inModel = 0;
            post.tradiNumber = "D006";
            string orgId = ConfigurationManager.AppSettings["orgId"];
            Input_D006 input = new Input_D006();
            List<Input_D006> list = Function.ToList<Input_D006>(ClassSqlHelper.ElectronicPrescription_D006_Prescription(post.hisId, orgId, post.cfh));
            input = list[0];
            Output_D006 output = new Output_D006();
            string code = "1";
            string json = ElectronicCSHelper.SaveToInterface1(input, out output, post, out code);
            if (code == "0")//如果成功更新本地D003 电子处方上传落地表
            {
                try
                {
                    int errorNo = 0;
                    ClassSqlHelper.D006UpdatePrescriptionToD003(input.hiRxno, output.rxStasCodg, output.rxStasName, output.rxChkStasCodg, output.rxChkOpnn, output.rxChkTime, output.rxChkStasName, out errorNo);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("电子处方审核结果查询成功，HIS更新电子处方数据表状态异常：" + ex.Message);
                    return ClassHelper.BuildReturnJson("-99", "电子处方审核结果查询成功，HIS更新电子处方数据表状态失败：" + ex.Message);
                }
            }
            return json;
        }
        #endregion
        #region 电子处方取药结果查询
        /// <summary>
        /// 【D007】电子处方取药结果查询
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public string ElectronicPrescription_D007(Post_D006 post)
        {
            post.inModel = 0;
            post.tradiNumber = "D007";
            string orgId = ConfigurationManager.AppSettings["orgId"];
            Input_D006 input = new Input_D006();
            List<Input_D006> list = Function.ToList<Input_D006>(ClassSqlHelper.ElectronicPrescription_D006_Prescription(post.hisId, orgId, post.cfh));
            input = list[0];
            Output_D007 output = new Output_D007();
            string code = "1";
            string json = ElectronicCSHelper.SaveToInterface1(input, out output, post, out code);
            if (code == "0")//如果成功更新本地D003 电子处方上传落地表
            {
                try
                {
                    int errorNo = 0;
                    ClassSqlHelper.D007UpdatePrescriptionToD003(input.hiRxno, output.rxStasCodg, output.rxStasName, output.rxUsedStasCodg, output.rxUsedStasName, out errorNo);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("电子处方审核结果查询成功，HIS更新电子处方数据表状态异常：" + ex.Message);
                    return ClassHelper.BuildReturnJson("-99", "电子处方审核结果查询成功，HIS更新电子处方数据表状态失败：" + ex.Message);
                }
            }
            return json;
        }
        #endregion
        #region 电子处方审核结果通知

        #endregion
        #region 电子处方取药结果通知

        #endregion
        #endregion
    }
}