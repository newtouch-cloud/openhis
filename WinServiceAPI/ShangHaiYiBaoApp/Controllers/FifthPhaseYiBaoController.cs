using CQYiBaoInterface.Models.Post;
using CQYiBaoInterface.Models.ShangHai;
using CQYiBaoInterface.Models.ShangHai.Input;
using CQYiBaoInterface.Models.ShangHai.Output;
using CQYiBaoInterface.Models.ShangHai.Post;
using CQYiBaoInterface.Models.SQL.ShangHai;
using CQYiBaoInterface.ShangHai;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShangHaiYiBaoApp.Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;
using YiBaoInterface;
using ClassHelper = CQYiBaoInterface.ShangHai.ClassHelper;
using ClassSqlHelper = CQYiBaoInterface.ShangHai.ClassSqlHelper;

namespace ShangHaiYiBaoApp.Controllers
{
    /// <summary>
    /// 上海五期医保
    /// </summary>
    public class FifthPhaseYiBaoController:BaseController
    {
        #region 读卡、账户查询 S000 SM01
        /// <summary>
        /// S000 读取基本信息、SM01 账户查询
        /// </summary>
        /// <param name="inputs000"></param>
        /// <returns></returns>
        [HttpPost]
        public string ybInterface_S000(Post_S000 inputs000)
        {
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "S000";
            post.operatorId = inputs000.operatorId;
            post.operatorName = inputs000.operatorName;
            post.insuplc_admdvs = "";

            post.inModel = 1;
            S000Input input = new S000Input();
            S000Output output = new S000Output();
            string code = "1";
            string jsonOut = ClassHelper.SaveToInterface(input, out output, post, out code);
            if (code== "P001" || code == "")
            {
                string sm01Code = "1";
                post.tradiNumber = "SM01";
                post.inModel = 1;
                post.insuplc_admdvs = output.xzqh;
                SM01Input sm01input = new SM01Input();
                sm01input.cardtype = inputs000.cardtype;
                //sm01input.carddata = output.kh;
                sm01input.carddata = "";
                SM01OutPut sm01output = new SM01OutPut();
                string sm01jsonOut = ClassHelper.SaveToInterface(sm01input, out sm01output, post, out sm01Code);
                if (sm01Code == "P001")
                {
                    S000_SM01OutPut data = new S000_SM01OutPut();

                    S000_SE01_SM01OutPut outentity = new S000_SE01_SM01OutPut();
                    outentity = Function.Mapping<S000_SE01_SM01OutPut, SM01OutPut>(sm01output);
                    outentity.kh = output.kh;
                    outentity.xm = output.xm;
                    outentity.xb = output.xb;
                    outentity.sfzh = output.sfzh;
                    outentity.lxdh = output.lxdh;
                    outentity.txdz = output.txdz;
                    outentity.yzbm = output.yzbm;
                    outentity.xzqh = output.xzqh;

                    data.sm01 = outentity;
                    data.xxfhm = "P001";
                    //落地读卡信息
                    try
                    {
                        XmlDocument xmlstr = (XmlDocument)Newtonsoft.Json.JsonConvert.DeserializeXmlNode(jsonOut, "root");
                        int errse = ClassSqlHelper.SaveLogSM01(inputs000.orgId, xmlstr);
                    }
                    catch (Exception e)
                    { }
                    return JsonHelper.ObjToJson(data);
                }
                return sm01jsonOut;
            }
            return jsonOut;
        }
        #endregion

        #region 电子凭证、账户信息 SE01 SM01
        /// <summary>
        ///  SE01 电子凭证、  SM01 账户信息
        /// </summary>
        /// <param name="inputse01"></param>
        /// <returns></returns>
        [HttpPost]
        public string ybInterface_SE01(Post_SE01 inputse01)
        {
            PostBase post = new PostBase();
            post.hisId = "0";
            post.tradiNumber = "SE01";
            post.operatorId = inputse01.operatorId;
            post.operatorName = inputse01.operatorName;
            post.insuplc_admdvs = "";

            post.inModel = 1;
            SE01Input input = new SE01Input();
            input.orgId = ConfigurationManager.AppSettings["fixmedins_code"]; ;
            input.ecQrCode = inputse01.ecQrCode;
            input.ecQrChannel = inputse01.ecQrChannel;
            input.businessType = inputse01.businessType;
            input.termId = Dns.GetHostName();
            string ip = "";
            foreach (IPAddress addressList in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (addressList.AddressFamily.ToString() == "InterNetwork")
                {
                    ip = addressList.ToString();
                }
            }
            input.termIp = ip;
            input.operatorId = inputse01.operatorId;
            input.operatorName = inputse01.operatorName;
            input.officeId = inputse01.officeId;
            input.officeName = inputse01.officeName;
            SE01Output output = new SE01Output();
            string code = "1";
            string jsonOut = ClassHelper.SaveToInterface(input, out output, post, out code);

            if (code == "P001"|| code == "")
            {
                string sm01Code = "1";
                post.tradiNumber = "SM01";
                post.inModel = 1;
                post.insuplc_admdvs = "";
                SM01Input sm01input = new SM01Input();
                sm01input.cardtype = inputse01.cardtype;
                sm01input.carddata = output.ecToken;
                SM01OutPut sm01output = new SM01OutPut();
                string sm01jsonOut = ClassHelper.SaveToInterface(sm01input, out sm01output, post, out sm01Code);
                if (sm01Code == "P001")
                {
                    S000_SM01OutPut data = new S000_SM01OutPut();

                    S000_SE01_SM01OutPut outentity = new S000_SE01_SM01OutPut();
                    outentity = Function.Mapping<S000_SE01_SM01OutPut, SM01OutPut>(sm01output);
                    outentity.kh = outentity.cardid;
                    outentity.xm = output.username;
                    outentity.xb = outentity.xb;
                    outentity.sfzh = output.idno;
                    outentity.idno = output.idno;
                    outentity.idtype = output.idtype;
                    outentity.ecToken = output.ecToken;

                    data.sm01 = outentity;
                    data.xxfhm = "P001";

                    try
                    {
                        XmlDocument xmlstr = (XmlDocument)Newtonsoft.Json.JsonConvert.DeserializeXmlNode(jsonOut, "root");
                        int errse = ClassSqlHelper.SaveLogSM01(inputse01.orgId, xmlstr);
                    }
                    catch (Exception e)
                    { }

                    return JsonHelper.ObjToJson(data);
                }
                return sm01jsonOut;
            }
            return jsonOut;
        }
        #endregion

        #region 医保磁卡账户查询 SM01
        /// <summary>
        /// SM01 账户查询
        /// </summary>
        /// <param name="inputsm01"></param>
        /// <returns></returns>
        [HttpPost]
        public string ybInterface_SM01(Post_S000 inputsm01)
        {
            PostBase post = new PostBase();
            post.hisId = "0";
            post.operatorId = inputsm01.operatorId;
            post.operatorName = inputsm01.operatorName;
            post.insuplc_admdvs = inputsm01.insuplc_admdvs;
            post.tradiNumber = "SM01";
            post.insuplc_admdvs = "";
            

            post.inModel = 1;
            SM01Input input = new SM01Input();
            input.cardtype = inputsm01.cardtype;
            if (input.cardtype == "1")
            {
                input.carddata = "";
            }
            else {
                input.carddata = inputsm01.carddata;
            }
            
            //input.carddata = string.Empty;
            SM01OutPut output = new SM01OutPut();
            string code = "1";
            string jsonOut = ClassHelper.SaveToInterface(input, out output, post, out code);
            if (code == "P001")
            {
                S000_SM01OutPut data = new S000_SM01OutPut();

                S000_SE01_SM01OutPut outentity = new S000_SE01_SM01OutPut();
                outentity = Function.Mapping<S000_SE01_SM01OutPut, SM01OutPut>(output);
                outentity.kh = output.cardid;
                outentity.xm = output.personname;
                outentity.xb = output.xb;
                outentity.sfzh = "";
                outentity.lxdh = "";
                outentity.txdz ="";
                outentity.yzbm = "";
                outentity.xzqh = "";

                data.sm01 = outentity;
                data.xxfhm = "P001";

                try
                {
                    XmlDocument xmlstr = (XmlDocument)Newtonsoft.Json.JsonConvert.DeserializeXmlNode(jsonOut, "root");
                    int errse = ClassSqlHelper.SaveLogSM01(inputsm01.orgId, xmlstr);
                }
                catch (Exception e)
                {}
                return JsonHelper.ObjToJson(data);
               
            }
            return jsonOut;
        }
        #endregion

        #region 门诊挂号结算 SH01 SH02
        /// <summary>
        /// SH01 门诊挂号(预结算)
        /// </summary>
        /// <param name="inputsm01"></param>
        /// <returns></returns>
        [HttpPost]
        public string ybInterface_SH01(Post_SH01 inputsm01)
        {
            PostBase post = new PostBase();
            post.hisId = inputsm01.hisId;
            post.tradiNumber = "SH01";
            post.operatorId = inputsm01.operatorId;
            post.operatorName = inputsm01.operatorName;
            post.insuplc_admdvs = inputsm01.insuplc_admdvs;
            post.inModel = 0;

            string ghfzf = ConfigurationManager.AppSettings["ghzfzlf"];
            decimal ghfzfval= string.IsNullOrWhiteSpace(ghfzf) ? Convert.ToDecimal(ghfzf) : Convert.ToDecimal(3.00);
            SH01Input input = new SH01Input();

            input = Function.Mapping<SH01Input, Post_SH01>(inputsm01);
            DataTable dtybks = ClassSqlHelper.QueryDeptToybdm(inputsm01.deptid,inputsm01.orgId);
            input.deptid= dtybks.Rows[0]["sybdm"].ToString();
            DataTable dtXmdm = ClassSqlHelper.QueryXmYbdm(inputsm01.zlxmdm, inputsm01.orgId);
            if (dtXmdm.Rows.Count > 0)
            {
                input.zlxmdm = dtXmdm.Rows[0]["ybdm"].ToString();
            }
            if (input.ghf<=3)
            {
                input.ghf = input.ghf + ghfzfval;
                input.fybjsfwfyze = input.fybjsfwfyze + ghfzfval;
                input.totalexpense = input.totalexpense - input.ghf;
                input.zhenlf = input.zhenlf - input.ghf;
                input.ybjsfwfyze = input.ybjsfwfyze- input.ghf;
            }

            SH01Output output = new SH01Output();
            string code = "1";
            string jsonOut = ClassHelper.SaveToInterface(input, out output, post, out code);

            return jsonOut;

        }
        /// <summary>
        /// 门诊挂号结算确认
        /// </summary>
        /// <param name="inputsh02"></param>
        /// <returns></returns>
        [HttpPost]
        public string ybInterface_SH02(Post_SH02 inputsh02)
        {
            PostBase post = new PostBase();
            post.hisId = inputsh02.hisId;
            post.tradiNumber = "SH02";
            post.operatorId = inputsh02.operatorId;
            post.operatorName = inputsh02.operatorName;
            post.insuplc_admdvs = inputsh02.insuplc_admdvs;
            post.inModel = 1;

            string ghfzf = ConfigurationManager.AppSettings["ghzfzlf"];
            decimal ghfzfval = string.IsNullOrWhiteSpace(ghfzf) ? Convert.ToDecimal(ghfzf) : Convert.ToDecimal(3.00);

            SH02Input input = new SH02Input();
            input = Function.Mapping<SH02Input, Post_SH02>(inputsh02);

            DataTable dtybks = ClassSqlHelper.QueryDeptToybdm(inputsh02.deptid, inputsh02.orgId);
            input.deptid = dtybks.Rows[0]["sybdm"].ToString();
            DataTable dtXmdm = ClassSqlHelper.QueryXmYbdm(inputsh02.zlxmdm, inputsh02.orgId);
            if (dtXmdm.Rows.Count > 0)
            {
                input.zlxmdm = dtXmdm.Rows[0]["ybdm"].ToString();
            }
            if (input.ghf <= 3)
            {
                input.ghf = input.ghf+ghfzfval;
                input.fybjsfwfyze = input.fybjsfwfyze+ghfzfval;
                input.totalexpense = input.totalexpense - input.ghf;
                input.zhenlf = input.zhenlf - input.ghf;
                input.ybjsfwfyze = input.ybjsfwfyze - input.ghf;
            }
            SH02Output output = new SH02Output();
            string code = "1";
            string jsonOut = ClassHelper.SaveToInterface(input, out output, post, out code);
            if (code == "P001")//如果成功则写入医保
            {
                
                try {
                    DateTime date = ClassSqlHelper.GetServerTime();
                    List<string> sqlList = new List<string>();
                    Ybjk_SH02_Input inentity = new Ybjk_SH02_Input();
                    inentity = Function.Mapping<Ybjk_SH02_Input, SH02Input>(input);
                    inentity.zt = 1;
                    inentity.czrq = date;
                    inentity.czydm = post.operatorId;
                    inentity.mzh = inputsh02.hisId;
                    inentity.lsh = output.lsh;
                    inentity.Id = Guid.NewGuid().ToString();
                    inentity.zfje = input.fybjsfwfyze;
                    sqlList.Add(inentity.ToAddSql());

                    Ybjk_SH02_Output outentity = new Ybjk_SH02_Output();

                    outentity = Function.Mapping<Ybjk_SH02_Output, SH02Output>(output);
                    outentity.Id = Guid.NewGuid().ToString();
                    outentity.czrq = date;
                    outentity.czydm = post.operatorId;
                    outentity.zt = 1;
                    outentity.zfje = input.fybjsfwfyze;
                    outentity.mzh = inputsh02.hisId;
                    sqlList.Add(outentity.ToAddSql());

                    int eeor = 0;
                    ClassSqlHelper.Merge(sqlList, out eeor);
                }
                catch (Exception ex) {
                    AppLogger.Info("医保挂号结算成功，HIS写结算数据表异常：" + ex.Message);
                    AppLogger.Info("自动执行门诊挂号结算撤销...");
                    string mzjscs = CancalSettlement_SK01(inputsh02, output.lsh,"0",output.totalexpense);
                    SHOutputReturn mzjscsRt = JsonConvert.DeserializeObject<SHOutputReturn>(mzjscs);
                    if (mzjscsRt.xxfhm != "P001")
                    {
                        AppLogger.Info("自动执行门诊挂号结算撤销失败：" + mzjscsRt.fhxx);
                        return ClassHelper.BuildReturnJson("-99", "医保挂号结算成功，HIS写挂号结算数据表失败：【" + ex.Message + "】，撤销医保结算失败：【" + mzjscsRt.fhxx + "】，请速联系HIS厂商技术人员。");
                    }
                    else
                    {
                        AppLogger.Info("自动执行门诊挂号结算撤销成功");
                        return ClassHelper.BuildReturnJson("-99", "医保挂号结算成功，HIS写挂号结算数据表失败，已自动撤销医保挂号结算，请联系HIS厂商技术人员。");
                    }
                }
            }
            return jsonOut;

        }
        #endregion

        #region 门诊收费结算 SI11 SI12
        /// <summary>
        /// 门诊收费预结算 : SI11
        /// </summary>
        /// <param name="inputsi11"></param>
        /// <returns></returns>
        [HttpPost]
        public string ybInterface_SI11(Post_SI11 inputsi11)
        {
            PostBase post = new PostBase();
            post.hisId = inputsi11.hisId;
            post.tradiNumber = "SI11";
            post.operatorId = inputsi11.operatorId;
            post.operatorName = inputsi11.operatorName;
            post.insuplc_admdvs = inputsi11.insuplc_admdvs;
            post.inModel = 1;
            //取对照的医保科室
            DataTable dtdept = ClassSqlHelper.QueryYbDept(inputsi11.hisId, inputsi11.orgId);

            SI11Input input = new SI11Input();
            input.cardtype = inputsi11.cardtype;
            input.carddata = inputsi11.carddata;
            input.deptid = dtdept.Rows[0]["deptid"].ToString();
            input.personspectag = inputsi11.personspectag;
            input.yllb = inputsi11.yllb;
            input.persontype = inputsi11.persontype;
            input.gsrdh = inputsi11.gsrdh;
            input.dbtype = inputsi11.dbtype;
            input.jsksrq = inputsi11.jsjsrq;
            input.jsjsrq = inputsi11.jsksrq;
            input.jzcs = 0;
            input.jzdyh = dtdept.Rows[0]["jzid"].ToString();
            input.xsywlx = inputsi11.xsywlx;
            //获取诊断信息
            DataTable dtDiseinfo = ClassSqlHelper.QueryICD(1, inputsi11.hisId, "", "");
            input.zdnos = Function.ToList<Zdnos>(dtDiseinfo);

            //获取明细账单
            DataTable dtCost = ClassSqlHelper.QueryCost(1, inputsi11.hisId, inputsi11.chrg_bchno);
            input.mxzdhs = Function.ToList<Mxzdhs>(dtCost);

            SI11Ouptput output = new SI11Ouptput();
            string code = "1";
            string jsonOut = ClassHelper.SaveToInterface(input, out output, post, out code);

            return jsonOut;

        }
        /// <summary>
        /// 门诊收费结算确认 SI12
        /// </summary>
        /// <param name="inputsm02"></param>
        /// <returns></returns>
        [HttpPost]
        public string ybInterface_SI12(Post_SI12 inputsi12)
        {
            PostBase post = new PostBase();
            post.hisId = inputsi12.hisId;
            post.tradiNumber = "SI12";
            post.operatorId = inputsi12.operatorId;
            post.operatorName = inputsi12.operatorName;
            post.insuplc_admdvs = inputsi12.insuplc_admdvs;
            post.inModel = 1;
            //取对照的医保科室
            DataTable dtdept = ClassSqlHelper.QueryYbDept(inputsi12.hisId, inputsi12.orgId);

            SI12Input input = new SI12Input();
            input.cardtype = inputsi12.cardtype;
            input.carddata = inputsi12.carddata;
            input.deptid = dtdept.Rows[0]["deptid"].ToString();
            input.personspectag = inputsi12.personspectag;
            input.yllb = inputsi12.yllb;
            input.persontype = inputsi12.persontype;
            input.gsrdh = inputsi12.gsrdh;
            input.dbtype = inputsi12.dbtype;
            input.jsksrq = inputsi12.jsjsrq;
            input.jsjsrq = inputsi12.jsksrq;
            input.jzcs = 0;
            input.jzdyh = dtdept.Rows[0]["jzid"].ToString();
            input.xsywlx = inputsi12.xsywlx;
            input.jssqxh = inputsi12.jssqxh;

            //获取诊断信息
            DataTable dtDiseinfo = ClassSqlHelper.QueryICD(1, inputsi12.hisId, "", "");
            input.zdnos = Function.ToList<Zdnos>(dtDiseinfo);

            //获取明细账单
            DataTable dtCost = ClassSqlHelper.QueryCost(1, inputsi12.hisId, inputsi12.chrg_bchno);
            input.mxzdhs = Function.ToList<Mxzdhs>(dtCost);

            decimal zfje = (decimal)input.mxzdhs.Sum(t => t.zfje);//分类自负金额
            SI12Output output = new SI12Output();
            string code = "1";
            string jsonOut = ClassHelper.SaveToInterface(input, out output, post, out code);
            if (code == "P001")//如果成功则写入医保
            {
                try
                {
                    string id = Guid.NewGuid().ToString();
                    DateTime date = ClassSqlHelper.GetServerTime();
                    List<string> sqlList = new List<string>();
                    Ybjk_SI12_Input inentity = new Ybjk_SI12_Input();
                    inentity = Function.Mapping<Ybjk_SI12_Input, SI12Input>(input);
                    inentity.zt = 1;
                    inentity.czrq = date;
                    inentity.czydm = post.operatorId;
                    inentity.mzh = inputsi12.hisId;
                    inentity.lsh = output.lsh;
                    inentity.Id = id;
                    inentity.zfje = zfje;
                    sqlList.Add(inentity.ToAddSql());

                    if (input.zdnos != null && input.zdnos.Count > 0) {
                        foreach (Zdnos detail in input.zdnos)
                        {
                            Ybjk_SI12_ZdnosInput zdentity = new Ybjk_SI12_ZdnosInput();
                            zdentity = Function.Mapping<Ybjk_SI12_ZdnosInput, Zdnos>(detail);
                            zdentity.mzh = inputsi12.hisId;
                            zdentity.lsh = output.lsh;
                            zdentity.Id= Guid.NewGuid().ToString();
                            zdentity.mainid = id;
                            zdentity.zt = "1";
                            sqlList.Add(zdentity.ToAddSql());
                        }
                    }
                    if (input.mxzdhs != null && input.mxzdhs.Count > 0)
                    {
                        foreach (Mxzdhs detail in input.mxzdhs)
                        {
                            Ybjk_SI12_MxzdhsInput mxzdentity = new Ybjk_SI12_MxzdhsInput();
                            mxzdentity = Function.Mapping<Ybjk_SI12_MxzdhsInput, Mxzdhs>(detail);
                            mxzdentity.mzh = inputsi12.hisId;
                            mxzdentity.lsh = output.lsh;
                            mxzdentity.Id = Guid.NewGuid().ToString();
                            mxzdentity.mainid = id;
                            mxzdentity.zt = "1";
                            sqlList.Add(mxzdentity.ToAddSql());
                        }
                    }
                    Ybjk_SI12_Output outentity = new Ybjk_SI12_Output();

                    outentity = Function.Mapping<Ybjk_SI12_Output, SI12Output>(output);
                    outentity.Id = Guid.NewGuid().ToString();
                    outentity.czrq = date;
                    outentity.czydm = post.operatorId;
                    outentity.zt = 1;
                    outentity.mzh = inputsi12.hisId;
                    outentity.zfje = zfje;
                    sqlList.Add(outentity.ToAddSql());

                    int eeor = 0;
                    ClassSqlHelper.Merge(sqlList, out eeor);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("医保结算成功，HIS门诊收费写结算数据表异常：" + ex.Message);
                    AppLogger.Info("自动执行门诊收费结算撤销...");
                    Post_SH02 sh02 = new Post_SH02() ;
                    sh02.operatorId = inputsi12.operatorId;
                    sh02.operatorName = inputsi12.operatorName;
                    sh02.carddata = inputsi12.carddata;
                    sh02.cardtype = inputsi12.cardtype;
                    sh02.hisId = inputsi12.hisId;
                    sh02.insuplc_admdvs= inputsi12.insuplc_admdvs;
                    string mzjscs = CancalSettlement_SK01(sh02, output.lsh,"1", output.totalexpense);
                    SHOutputReturn mzjscsRt = JsonConvert.DeserializeObject<SHOutputReturn>(mzjscs);
                    if (mzjscsRt.xxfhm != "P001")
                    {
                        AppLogger.Info("自动执行门诊结算撤销失败：" + mzjscsRt.fhxx);
                        return ClassHelper.BuildReturnJson("-99", "医保结算成功，HIS写结算数据表失败：【" + ex.Message + "】，撤销医保结算失败：【" + mzjscsRt.fhxx + "】，请速联系HIS厂商技术人员。");
                    }
                    else
                    {
                        AppLogger.Info("自动执行门诊结算撤销成功");
                        return ClassHelper.BuildReturnJson("-99", "医保结算成功，HIS写结算数据表失败，已自动撤销医保结算，请联系HIS厂商技术人员。");
                    }
                }
            }
            return jsonOut;

        }
        #endregion

        #region  反结算交易(门诊住院) SK01
        /// <summary>
        /// 收费结算反交易(结算数据落地异常时专用)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="setl_id"></param>
        /// <param name="sflx">收费类型 0：门诊挂号 1：门诊收费 2：住院收费</param>
        /// <param name="totalexpense"></param>
        /// <returns></returns>
        private string CancalSettlement_SK01(Post_SH02 input, string setl_id,string sflx,decimal totalexpense)
        {
            Post_SK01 sk01 = new Post_SK01();
            sk01.operatorId = input.operatorId;
            sk01.operatorName = input.operatorName;
            sk01.insuplc_admdvs = input.insuplc_admdvs;
            sk01.hisId = input.hisId;
            sk01.translsh = setl_id;
            sk01.sflx = sflx;
            sk01.totalexpense = totalexpense;
            sk01.cxly = "0";

            return ybInterface_SK01(sk01);
        }
        /// <summary>
        /// 反交易 SK01-退款
        /// </summary>
        /// <param name="inputsk01"></param>
        /// <returns></returns>
        [HttpPost]
        public string ybInterface_SK01(Post_SK01 inputsk01)
        {
            PostBase post = new PostBase();
            post.hisId = inputsk01.hisId;
            post.tradiNumber = "SK01";
            post.operatorId = inputsk01.operatorId;
            post.operatorName = inputsk01.operatorName;
            post.insuplc_admdvs = inputsk01.insuplc_admdvs;
            post.inModel = 1;
           
            SK01Input input = new SK01Input();
            input.cardtype = inputsk01.cardtype;
            input.carddata = inputsk01.carddata;
            input.translsh = inputsk01.translsh;
            input.totalexpense = inputsk01.totalexpense;
            input.xsywlx = inputsk01.xsywlx;
            if (inputsk01.cxly != "0")
            {
                ///获取住院结算流水号
                if (inputsk01.sflx == "2")
                {
                    DataTable lshdata = ClassSqlHelper.QueryYbReturnLsh(inputsk01.translsh);
                    //input.translsh = lshdata.Rows[0]["lsh"].ToString();
                    input.totalexpense = decimal.Parse(lshdata.Rows[0]["totalexpense"].ToString());
                }
                else if(inputsk01.sflx == "1") {
                    DataTable dtCost = ClassSqlHelper.QueryYbReturnData(inputsk01.translsh);
                    input.totalexpense = Convert.ToDecimal(dtCost.Rows[0]["totalexpense"]);
                }
            }
            else
            {
                input.totalexpense = inputsk01.totalexpense;
            }
           
            SK01Output output = new SK01Output();
            string code = "1";
            string jsonOut = ClassHelper.SaveToInterface(input, out output, post, out code);
            if (code== "P001")
            {
                try {
                    int eeor = 0;
                    ClassSqlHelper.UpSettlement(inputsk01.hisId, inputsk01.translsh, output.translsh, inputsk01.operatorId,inputsk01.sflx, out eeor);
                } catch (Exception ex)
                {
                    AppLogger.Info("撤销医保结算成功，HIS更新结算数据表状态异常：" + ex.Message);
                    return ClassHelper.BuildReturnJson("-99", "撤销医保结算成功，HIS更新结算数据表状态失败：" + ex.Message);
                }
            }
            return jsonOut;

        }
        #endregion

        #region 门诊明细上传 SN01
        /// <summary>
        /// 门诊 明细上传 SN01
        /// </summary>
        /// <param name="inputsn01"></param>
        /// <returns></returns>
        [HttpPost]
        public string ybInterface_MzSN01(Post_SN01MZ inputsn01)
        {
            PostBase post = new PostBase();
            post.hisId = inputsn01.hisId;
            post.operatorId = inputsn01.operatorId;
            post.operatorName = inputsn01.operatorName;
            post.tradiNumber = "SN01";
            post.insuplc_admdvs = inputsn01.insuplc_admdvs;

            post.inModel = 1;
            DataTable dtFeed = ClassSqlHelper.QuertFeedetail(inputsn01);
            if (inputsn01.type == "3")
            {
                for (int i =0;i< dtFeed.Rows.Count;i++)
                {
                    foreach (var itemtf in inputsn01.tjsxmDict)
                    {
                        if (itemtf.tjsmxnm == dtFeed.Rows[i]["jsmxnm"].ToString())
                        {
                            dtFeed.Rows[i]["mxxmsl"] =Convert.ToDecimal(dtFeed.Rows[i]["mxxmsl"]) - itemtf.tsl;
                            dtFeed.Rows[i]["mxxmje"] =Math.Round(Convert.ToDecimal(dtFeed.Rows[i]["mxxmdj"])* 
                                Convert.ToDecimal(dtFeed.Rows[i]["mxxmsl"]),2,MidpointRounding.AwayFromZero);
                            dtFeed.Rows[i]["mxxmjyfy"] = dtFeed.Rows[i]["mxxmje"];
                            dtFeed.Rows[i]["mxxmybjsfwfy"] = dtFeed.Rows[i]["mxxmje"];
                        }
                    }
                }
            }
            SN01Input sn01input = new SN01Input();
            sn01input.cardtype = inputsn01.cardtype;
            sn01input.carddata = inputsn01.carddata;
            sn01input.jzdyh = dtFeed.Rows[0]["jzid"].ToString();
            sn01input.djh = inputsn01.hisId;
            sn01input.mxzdh = string.Empty;//初始为空
            sn01input.bcmxylfyze = inputsn01.bcmxylfyze;
            sn01input.jslxbz = inputsn01.jslxbz;
            sn01input.mxxms = new List<MxXmsIn>();
            sn01input.mxxms = Function.ToList<MxXmsIn>(dtFeed);
            decimal zje =Convert.ToDecimal(0.00);
            sn01input.mxxms = sn01input.mxxms.Where(p=>p.mxxmje> zje).ToList();

            SN01Output output = new SN01Output();
            output.mxxms = new List<MxXmsOut>();

            string code = "1";
            string jsonOut = ClassHelper.SaveToInterface(sn01input, out output, post, out code);
            if (code == "P001")//如果成功则写入医保
            {
                DateTime date = ClassSqlHelper.GetServerTime();
                string mainId = Guid.NewGuid().ToString();
                List<string> sqlList = new List<string>();
                Ybjk_SN01_Input sn01entity = new Ybjk_SN01_Input();
                sn01entity = Function.Mapping<Ybjk_SN01_Input, SN01Input>(sn01input);
                sn01entity.zt = 1;
                sn01entity.czrq = date;
                sn01entity.czydm = post.operatorId;
                sn01entity.mzzyh = inputsn01.hisId;
                sn01entity.fhmxzdh = output.mxzdh;
                sn01entity.Id = mainId;
                sqlList.Add(sn01entity.ToAddSql());

                if (sn01input.mxxms != null && sn01input.mxxms.Count > 0)
                {
                    foreach (MxXmsIn detail in sn01input.mxxms)
                    {
                        Ybjk_SN01_Mxxmz_Input outputDetail = new Ybjk_SN01_Mxxmz_Input();
                        outputDetail = Function.Mapping<Ybjk_SN01_Mxxmz_Input, MxXmsIn>(detail);
                        outputDetail.Id = Guid.NewGuid().ToString();
                        outputDetail.mzzyh = inputsn01.hisId;
                        outputDetail.mxzdh = output.mxzdh;
                        outputDetail.mainId = mainId;
                        outputDetail.zt = 1;
                        sqlList.Add(outputDetail.ToAddSql());
                    }
                }

                Ybjk_SN01_Output sn01outentity = new Ybjk_SN01_Output();
                string outmainId = Guid.NewGuid().ToString();
                sn01outentity = Function.Mapping<Ybjk_SN01_Output, SN01Output>(output);
                sn01outentity.Id = outmainId;
                sn01outentity.czrq = date;
                sn01outentity.czydm = post.operatorId;
                sn01outentity.zt = 1;
                sn01outentity.mzzyh = inputsn01.hisId;
                sn01outentity.ybzje = inputsn01.ybzje;
                sn01outentity.zfzje = inputsn01.zfzje;
                sn01outentity.jylsh = inputsn01.chrg_bchno;
                sqlList.Add(sn01outentity.ToAddSql());

                if (output.mxxms != null && sn01input.mxxms.Count > 0)
                {
                    output.mxxms.ForEach(m =>
                    {
                        var mxentity = sn01input.mxxms.FirstOrDefault(n=>n.xh==m.xh);
                        if (mxentity != null)
                        {
                            m.zfbl = mxentity.zfbl;
                            m.zfje = mxentity.zfje;
                            m.fyxj = mxentity.fyxj;
                        }
                    });

                    foreach (MxXmsOut detail in output.mxxms)
                    {
                        Ybjk_SN01_MxXms_Output outputDetail = new Ybjk_SN01_MxXms_Output();
                        outputDetail = Function.Mapping<Ybjk_SN01_MxXms_Output, MxXmsOut>(detail);
                        outputDetail.mainId = outmainId;
                        outputDetail.zt = 1;
                        outputDetail.mxzdh = output.mxzdh;
                        outputDetail.id = Guid.NewGuid().ToString();
                        outputDetail.mzzyh = inputsn01.hisId;
                        sqlList.Add(outputDetail.ToAddSql());
                    }
                }
                int eeor = 0;
                ClassSqlHelper.Merge(sqlList, out eeor);
                if (eeor < 0)
                {
                    JObject jsonUP = JObject.Parse(jsonOut);
                    jsonUP["fhxx"] = jsonUP["err_msg"] + "HIS信息提示：写入费用信息表表失败Ybjk_SN01_Input,Ybjk_SN01_Output-100";
                    jsonUP["xxfhm"] = "-100";
                    return Convert.ToString(jsonUP);
                }
            }
            return jsonOut;
        }

        #endregion

        #region 住院明细上传  SN01
        /// <summary>
        /// 住院明细上传 SN01
        /// </summary>
        /// <param name="inputsn01"></param>
        /// <returns></returns>
        [HttpPost]
        public string ybInterface_ZySN01(Post_SN01ZY inputsn01)
        {
            PostBase post = new PostBase();
            post.hisId = inputsn01.hisId;
            post.operatorId = inputsn01.operatorId;
            post.operatorName = inputsn01.operatorName;
            post.tradiNumber = "SN01";

            post.inModel = 1;
            DataTable dtFeed = ClassSqlHelper.QuertHospitalFeedetailV2(inputsn01);
            DataTable dtjzxh = ClassSqlHelper.Queryjzxh(inputsn01.hisId, inputsn01.orgId);
            inputsn01.jzdyh = dtjzxh.Rows[0]["jzdyh"].ToString();
            inputsn01.carddata= dtjzxh.Rows[0]["carddata"].ToString();
            inputsn01.cardtype = dtjzxh.Rows[0]["cardtype"].ToString();
            SN01Input sn01input = new SN01Input();
            //sn01input.mxxms = new List<MxXmsIn>();
            sn01input.mxxms = Function.ToList<MxXmsIn>(dtFeed);

            //SN01Output output = new SN01Output();

            SN01Input inputListData = new SN01Input();
            inputListData.mxxms = sn01input.mxxms.FindAll(p => !p.mxxmsl.ToString().Contains("-"));

            SN01Input inputListRefund = new SN01Input();
            inputListRefund.mxxms = sn01input.mxxms.FindAll(p => p.mxxmsl.ToString().Contains("-"));

            string json = "";
            ybInterface_MultibatchSN01(inputListData.mxxms, post, 0, inputsn01.uploadCount, inputsn01, out json);
            if (json == "")
            {
                if (inputListRefund.mxxms.Count > 0)
                {
                    ybInterface_MultibatchSN01(inputListRefund.mxxms, post, 0, inputsn01.uploadCount, inputsn01, out json);
                    if (json == "")
                    {
                        return "{\"xxfhm\":\"P001\",}";
                    }
                    else
                    {
                        return json;
                    }
                }
                return "{\"xxfhm\":\"P001\",}";
            }
            return json;
        }

        /// <summary>
        /// 分批次上传费用 (50条)
        /// </summary>
        /// <param name="inputListAll"></param>
        /// <param name="post"></param>
        /// <param name="flag"></param>
        /// <param name="uploadCount"></param>
        /// <param name="json"></param>
        private void ybInterface_MultibatchSN01(List<MxXmsIn> inputListAll, PostBase post, int flag, int uploadCount, Post_SN01ZY inputsn01, out string json)
        {
            json = "";
            int max = inputListAll.Count;//总共数据条数
            string mxzdh = "";
            if (max - flag * uploadCount <= 0) return;
            int curMax = (flag + 1) * uploadCount;
            if (curMax - max >= 0) curMax = max;

            SN01Input sn01input = new SN01Input();
            sn01input.cardtype = inputsn01.cardtype;//凭证类别
            sn01input.carddata = inputsn01.carddata;//凭证码
            sn01input.jzdyh = inputsn01.jzdyh ;//就诊单元号
            sn01input.djh = inputsn01.hisId;//登记号
            sn01input.mxzdh = mxzdh;//明细账单号
            sn01input.jslxbz = "610";//结算类型标 志 默认为住院结算

            //明细内容上传
            sn01input.mxxms = new List<MxXmsIn>();

            SN01Output sn01output = new SN01Output();
            sn01output.mxxms = new List<MxXmsOut>();
            decimal bcscje = 0;

            for (int i = uploadCount * flag; i < curMax; i++)
            {
                sn01input.mxxms.Add(inputListAll[i]);
                bcscje += inputListAll[i].mxxmje;
            }
            sn01input.bcmxylfyze = bcscje;//本次费用明 细包的医疗 费用总额
            string code = "1";
            string jsonOut = ClassHelper.SaveToInterface(sn01input, out sn01output, post, out code);
            if (code == "P001")//如果成功则写入医保
            {
                DateTime date = ClassSqlHelper.GetServerTime();
                string mainId = Guid.NewGuid().ToString();
                List<string> sqlList = new List<string>();
                Ybjk_SN01_zy_Input sn01entity = new Ybjk_SN01_zy_Input();
                sn01entity = Function.Mapping<Ybjk_SN01_zy_Input, SN01Input>(sn01input);
                sn01entity.zt = 1;
                sn01entity.czrq = date;
                sn01entity.czydm = post.operatorId;
                sn01entity.mzzyh = post.hisId;
                sn01entity.fhmxzdh = sn01output.mxzdh;
                sn01entity.Id = mainId;
                sqlList.Add(sn01entity.ToAddSql());

                if (sn01input.mxxms != null && sn01input.mxxms.Count > 0)
                {
                    foreach (MxXmsIn detail in sn01input.mxxms)
                    {
                        Ybjk_SN01_Mxxzy_Input inputDetail = new Ybjk_SN01_Mxxzy_Input();
                        inputDetail = Function.Mapping<Ybjk_SN01_Mxxzy_Input, MxXmsIn>(detail);
                        inputDetail.Id = Guid.NewGuid().ToString();
                        inputDetail.mainId = mainId;
                        inputDetail.zt = 1;
                        inputDetail.mzzyh = post.hisId;
                        inputDetail.mxzdh = sn01output.mxzdh;
                        sqlList.Add(inputDetail.ToAddSql());
                    }
                }

                Ybjk_SN01_zy_Output sn01outentity = new Ybjk_SN01_zy_Output();
                string outmainId = Guid.NewGuid().ToString();
                sn01outentity = Function.Mapping<Ybjk_SN01_zy_Output, SN01Output>(sn01output);
                sn01outentity.Id = outmainId;
                sn01outentity.czrq = date;
                sn01outentity.czydm = post.operatorId;
                sn01outentity.zt = 1;
                sn01outentity.mzzyh = post.hisId;
                sn01outentity.mxzdh = sn01output.mxzdh;
                sn01outentity.ybzje = bcscje;
                sqlList.Add(sn01outentity.ToAddSql());

                if (sn01output.mxxms != null && sn01input.mxxms.Count > 0)
                {
                    foreach (MxXmsOut detail in sn01output.mxxms)
                    {
                        Ybjk_SN01_MxXms_zy_Output outputDetail = new Ybjk_SN01_MxXms_zy_Output();
                        outputDetail = Function.Mapping<Ybjk_SN01_MxXms_zy_Output, MxXmsOut>(detail);
                        outputDetail.id = Guid.NewGuid().ToString();
                        outputDetail.mainId = outmainId;
                        outputDetail.zt = 1;
                        outputDetail.mzzyh = post.hisId;
                        outputDetail.mxzdh = sn01output.mxzdh;
                        mxzdh = sn01output.mxzdh;
                        sqlList.Add(outputDetail.ToAddSql());
                    }
                }
                int eeor = 0;
                ClassSqlHelper.Merge(sqlList, out eeor);
                if (eeor < 0)
                {
                    JObject jsonUP = JObject.Parse(jsonOut);
                    jsonUP["fhxx"] = jsonUP["fhxx"] + "HIS信息提示：写入费用信息表表失败Ybjk_SN01_Input,Ybjk_SN01_Output-100";
                    jsonUP["xxfhm"] = "-100";
                    json = Convert.ToString(jsonUP);
                    return;//如报错则不再继续

                }
            }
            else
            {
                json = jsonOut;
                return;
            }
            ybInterface_MultibatchSN01(inputListAll, post, flag + 1, uploadCount,inputsn01, out json);//递归
        }

        #endregion

        #region 明细撤销 SN02
        /// <summary>
        /// 撤销明细上传 SN02
        /// </summary>
        /// <param name="inputsn02"></param>
        /// <returns></returns>
        [HttpPost]
        public string ybInterface_SN02(Post_SN02 inputsn02)
        {
            PostBase post = new PostBase();
            post.hisId = inputsn02.hisId;
            post.tradiNumber = "SN02";
            post.operatorId = inputsn02.operatorId;
            post.operatorName = inputsn02.operatorName;
            post.insuplc_admdvs = inputsn02.insuplc_admdvs;
            post.inModel = 0;
           
            SN02Input input = new SN02Input();
            input.cardtype = inputsn02.cardtype;
            input.carddata = inputsn02.carddata;
            if (inputsn02.mzzybz == "zy")
            {
                string jsonOut = "";
                DataTable zdhdata = ClassSqlHelper.Querymxzdhsdataxx(inputsn02.hisId);//获取住院费用上传的明细账单号 全部撤销
                foreach (DataRow item in zdhdata.Rows)
                {
                    input.mxzdh = item["mxzdh"].ToString();
                    input.cardtype = item["cardtype"].ToString();
                    input.carddata = item["carddata"].ToString();
                    SN02Output output = new SN02Output();
                    string code = "1";
                    jsonOut = ClassHelper.SaveToInterface(input, out output, post, out code);
                    if (code == "P001")//如果成功则更新本地信息表 
                    {
                        try
                        {
                            int eeor = 0;
                            ClassSqlHelper.UpHospitaFeedetailZy(inputsn02.hisId, input.mxzdh, out eeor);
                        }
                        catch (Exception ex)
                        {
                            AppLogger.Info("医保明细撤销成功，HIS删除上传明细数据异常：" + ex.Message);
                            return ClassHelper.BuildReturnJson("-99", "医保明细撤销成功，HIS删除上传明细数据失败：" + ex.Message);
                        }
                    }
                }
                return jsonOut;
            }
            else {
                if (inputsn02.ishtqz == "Y")
                {
                    DataTable mxzdh = ClassSqlHelper.QueryYbCxmxzdhData(inputsn02.jylsh);
                    input.mxzdh = mxzdh.Rows[0]["mxzdh"].ToString();
                }
                else
                {
                    input.mxzdh = inputsn02.chrg_bchno;
                }
                SN02Output output = new SN02Output();
                string code = "1";
                string jsonOut = ClassHelper.SaveToInterface(input, out output, post, out code);
                if (code == "P001")//如果成功则更新本地信息表 
                {
                    try
                    {
                        int eeor = 0;
                        ClassSqlHelper.UpHospitaFeedetail(inputsn02.hisId, input.mxzdh, out eeor);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Info("医保明细撤销成功，HIS删除上传明细数据异常：" + ex.Message);
                        return ClassHelper.BuildReturnJson("-99", "医保明细撤销成功，HIS删除上传明细数据失败：" + ex.Message);
                    }
                }
                return jsonOut;
            }
           

        }
        #endregion

        #region 登记业务SJ11
        /// <summary>
        /// 住院登记 SJ11
        /// </summary>
        /// <param name="inputsj11"></param>
        /// <returns></returns>
        [HttpPost]
        public string ybInterface_SJ11(Post_SJ11 inputsj11)
        {
            PostBase post = new PostBase();
            post.hisId = inputsj11.hisId;
            post.tradiNumber = "SJ11";
            post.operatorId = inputsj11.operatorId;
            post.operatorName = inputsj11.operatorName;
            post.inModel = 1;

            SJ11Input input = new SJ11Input();
            input.cardtype = inputsj11.cardtype;
            input.carddata = inputsj11.carddata;
            if (inputsj11.djlx == "mz")
            {
                DataTable dtdept = ClassSqlHelper.QueryYbDept(inputsj11.hisId, inputsj11.orgId);
                input.deptid = dtdept.Rows[0]["deptid2"].ToString();//科室
                //获取诊断信息
                DataTable dtDiseinfo = ClassSqlHelper.QueryICD(1, inputsj11.hisId, "", "");
                input.zdnos = Function.ToList<Zdnos>(dtDiseinfo);
            }
            else {
                DataTable dtdept = ClassSqlHelper.QueryYbDeptZy(inputsj11.hisId, inputsj11.orgId);
                input.deptid = dtdept.Rows[0]["deptid"].ToString();//科室

                DataTable zddata = ClassSqlHelper.Queryryzdxx(inputsj11.hisId, inputsj11.orgId);//获取诊断内容
                input.zdnos = Function.ToList<Zdnos>(zddata);
            }
            
            input.djtype = inputsj11.djlx == "mz"?"0":"3";//默认入院登记 //1：家床建床 2：急观入观3：入院登记 4：大病登记6：保健对象急观 7：保健对象入院 0：门诊登记
            input.djno = inputsj11.hisId; //急观： 填急观号，住院： 填住院号， 家床： 填空格，大病： 填空格
            input.startdate =DateTime.Now.ToString("yyyyMMdd"); //开始日期 格式：  “YYYYMMDD”
            input.enddate = "";//格式：  “YYYYMMDD” 可空

            //DataTable zddata = ClassSqlHelper.Queryryzdxx(inputsj11.hisId, inputsj11.orgId);//获取诊断内容
            //input.zdnos = new List<Zdnos>();
            //foreach (DataRow item in zddata.Rows)
            //{
            //    Zdnos zdnos = new Zdnos();
            //    zdnos.zdno = item["zdno"].ToString();
            //    zdnos.zdmc = item["zdmc"].ToString();
            //    input.zdnos.Add(zdnos);
            //}
            input.dbxm = "";
            input.zd = "";
            input.wtrxm = "";
            input.wtrsfzh = "";
            input.yy = "";
            input.des = "";
            input.dbzl = "";
            input.ysxm = "";
            input.ysgh = "";
            SJ11Output output = new SJ11Output();
            string code = "1";
            string jsonOut = ClassHelper.SaveToInterface(input, out output, post, out code);

            if (code == "P001")//如果成功则写入本地信息表 
            {
                try
                {
                    DateTime date = ClassSqlHelper.GetServerTime();
                    List<string> sqlList = new List<string>();
                    Ybjk_SJ11_InPut djentity = new Ybjk_SJ11_InPut();
                    djentity = Function.Mapping<Ybjk_SJ11_InPut, SJ11Input>(input);
                    djentity.Id = Guid.NewGuid().ToString();
                    djentity.zt = 1;
                    djentity.czrq = ClassSqlHelper.GetServerTime();
                    djentity.czydm = inputsj11.operatorId;
                    djentity.mzzyh = inputsj11.hisId;
                    sqlList.Add(djentity.ToAddSql());

                    Ybjk_SJ11_Output djoutentity = new Ybjk_SJ11_Output();
                    djoutentity = Function.Mapping<Ybjk_SJ11_Output, SJ11Output>(output);
                    djoutentity.Id=Guid.NewGuid().ToString();
                    djoutentity.zt = 1;
                    djoutentity.czrq = ClassSqlHelper.GetServerTime();
                    djoutentity.czydm = inputsj11.operatorId;
                    djoutentity.mzzyh = inputsj11.hisId;
                    sqlList.Add(djoutentity.ToAddSql());

                    int eeor = 0;
                    ClassSqlHelper.Merge(sqlList, out eeor);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("医保登记成功，HIS写入登记数据表失败：" + ex.Message);
                    AppLogger.Info("自动执行登记撤销...");
                    string mzjscs = CancalRegister_SJ21(inputsj11);
                    SHOutputReturn mzjscsRt = JsonConvert.DeserializeObject<SHOutputReturn>(mzjscs);
                    if (mzjscsRt.xxfhm != "P001")
                    {
                        AppLogger.Info("自动执行登记业务撤销失败：" + mzjscsRt.fhxx);
                        return ClassHelper.BuildReturnJson("-99", "医保登记成功，HIS写登记落地数据表失败：【" + ex.Message + "】，撤销医保登记失败：【" + mzjscsRt.fhxx + "】，请速联系HIS厂商技术人员。");
                    }
                    else
                    {
                        AppLogger.Info("自动执行登记撤销成功");
                        return ClassHelper.BuildReturnJson("-99", "医保结算成功，HIS写登记数据表失败，已自动撤销医保登记，请联系HIS厂商技术人员。");
                    }
                }
                
            }
            return jsonOut;

        }
        /// <summary>
        /// 登记撤销
        /// </summary>
        /// <param name="inputsj21"></param>
        /// <returns></returns>
        [HttpPost]
        public string ybInterface_SJ21(Post_SJ21 inputsj21)
        {
            PostBase post = new PostBase();
            post.hisId = inputsj21.hisId;
            post.tradiNumber = "SJ21";
            post.operatorId = inputsj21.operatorId;
            post.operatorName = inputsj21.operatorName;
            post.inModel = 0;

            SJ21Input input = new SJ21Input();
            input.cardtype = inputsj21.cardtype;
            input.carddata = inputsj21.carddata;
            input.cxtype = "3";//先默认3
            input.dbxm = "";
            SJ11Output output = new SJ11Output();
            string code = "1";
            string jsonOut = ClassHelper.SaveToInterface(input, out output, post, out code);
            if (code == "P001")//如果成功则更新本地信息表 
            {
                int eeor = 0;
                ClassSqlHelper.ExSJ21info(inputsj21.operatorId, inputsj21.hisId, out eeor);
            }
            return jsonOut;

        }
        /// <summary>
        /// 登记异常专用 (自动撤销登记业务)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string CancalRegister_SJ21(Post_SJ11 input)
        {
            Post_SJ21 sj21 = new Post_SJ21();
            sj21.operatorId = input.operatorId;
            sj21.operatorName = input.operatorName;
            sj21.hisId = input.hisId;

            return ybInterface_SJ21(sj21);
        }
        #endregion

        #region 住院结算

        /// <summary>
        /// 住院预结算
        /// </summary>
        /// <param name="inputsj11"></param>
        /// <returns></returns>
        [HttpPost]
        public string ybInterface_SI51(Post_SI51 inputsi51)
        {
            PostBase post = new PostBase();
            post.hisId = inputsi51.hisId;
            post.tradiNumber = "SI51";
            post.operatorId = inputsi51.operatorId;
            post.operatorName = inputsi51.operatorName;
            post.inModel = 0;

            SI51Input input = new SI51Input();
            input.cardtype = inputsi51.cardtype;
            input.carddata = inputsi51.carddata;
            input.personspectag = inputsi51.personspectag;//默认为普通 特殊人员标识 0：普通 1：离休 2：伤残 3：干部保健定点
            input.yllb = inputsi51.yllb;//医保类别 默认为住院
            input.persontype = inputsi51.persontype;//病人类型
            input.gsrdh = inputsi51.gsrdh;//工伤认定号 默认为空
            input.cyjsbz = inputsi51.cyjsbz;//出院结算标志
            DataTable dtdept = ClassSqlHelper.QueryYbDeptZy(inputsi51.hisId, inputsi51.orgId);
            input.jsksrq = dtdept.Rows[0]["ryrq"].ToString();
            input.jsjsrq = dtdept.Rows[0]["cyrq"].ToString();//DateTime.Now.ToString("yyyyMMdd");
            input.zyts = dtdept.Rows[0]["zyts"].ToString();
            input.zyh = inputsi51.hisId;
            input.deptid = dtdept.Rows[0]["deptid"].ToString();//科室
            input.jzdyh= dtdept.Rows[0]["jzdyh"].ToString();//就诊单元号
            input.carddata= dtdept.Rows[0]["carddata"].ToString();
            input.cardtype= dtdept.Rows[0]["cardtype"].ToString();
            input.xsywlx = "1";
            input.zdnos = new List<Zdnos>();
            DataTable zddata = ClassSqlHelper.Queryzyzdxx(inputsi51.hisId, inputsi51.orgId);//获取诊断内容
            foreach (DataRow item in zddata.Rows)
            {
                Zdnos zdnos = new Zdnos();
                zdnos.zdno = item["zdno"].ToString();
                zdnos.zdmc = item["zdmc"].ToString();
                input.zdnos.Add(zdnos);
            }
            input.mxzdhs = new List<Mxzdhs>();
            DataTable mxzdhsdata = ClassSqlHelper.Querymxzdhsdataxx(inputsi51.hisId);//获取明细账单号
            foreach (DataRow item in mxzdhsdata.Rows)
            {
                Mxzdhs mxzdhs = new Mxzdhs();
                mxzdhs.mxzdh = item["mxzdh"].ToString();
                mxzdhs.totalexpense =decimal.Parse(item["ybzje"].ToString())+ decimal.Parse(item["zfzje"].ToString());
                mxzdhs.ybjsfwfyze = decimal.Parse(item["ybzje"].ToString());
                mxzdhs.fybjsfwfyze = decimal.Parse(item["zfzje"].ToString());
                input.mxzdhs.Add(mxzdhs);
            }
            SI51Output output = new SI51Output();
            string code = "1";
            string jsonOut = ClassHelper.SaveToInterface(input, out output, post, out code);


            return jsonOut;

        }
        /// <summary>
        /// 住院结算
        /// </summary>
        /// <param name="inputsi52"></param>
        /// <returns></returns>
        [HttpPost]
        public string ybInterface_SI52(Post_SI52 inputsi52)
        {
            PostBase post = new PostBase();
            post.hisId = inputsi52.hisId;
            post.tradiNumber = "SI52";
            post.operatorId = inputsi52.operatorId;
            post.operatorName = inputsi52.operatorName;
            post.inModel = 1;

            SI52Input input = new SI52Input();
            input.cardtype = inputsi52.cardtype;
            input.carddata = inputsi52.carddata;
            input.cyjsbz = inputsi52.cyjsbz;
            input.personspectag = inputsi52.personspectag;//默认为普通 特殊人员标识 0：普通 1：离休 2：伤残 3：干部保健定点
            input.yllb = inputsi52.yllb;//医保类别 默认为住院
            input.persontype = inputsi52.persontype;//病人类型
            input.gsrdh = inputsi52.gsrdh;//工伤认定号 默认为空
            input.cyjsbz = inputsi52.cyjsbz;//出院结算标志
            DataTable dtdept = ClassSqlHelper.QueryYbDeptZy(inputsi52.hisId, inputsi52.orgId);
            input.jsksrq = dtdept.Rows[0]["ryrq"].ToString();
            input.jsjsrq = dtdept.Rows[0]["cyrq"].ToString();
            input.zyts = dtdept.Rows[0]["zyts"].ToString();
            input.zyh = inputsi52.hisId;
            input.deptid = dtdept.Rows[0]["deptid"].ToString();//科室
            input.jzdyh = dtdept.Rows[0]["jzdyh"].ToString();//就诊单元号
            input.carddata = dtdept.Rows[0]["carddata"].ToString();
            input.cardtype = dtdept.Rows[0]["cardtype"].ToString();
            input.jssqxh = inputsi52.jssqxh;
            input.xsywlx = inputsi52.xsywlx;

            DataTable zddata = ClassSqlHelper.Queryzyzdxx(inputsi52.hisId, inputsi52.orgId);//获取诊断内容
            input.zdnos = new List<Zdnos>();
            foreach (DataRow item in zddata.Rows)
            {
                Zdnos zdnos = new Zdnos();
                zdnos.zdno = item["zdno"].ToString();
                zdnos.zdmc = item["zdmc"].ToString();
                input.zdnos.Add(zdnos);
            }
            input.mxzdhs = new List<Mxzdhs>();
            DataTable mxzdhsdata = ClassSqlHelper.Querymxzdhsdataxx(inputsi52.hisId);//获取明细账单号
            foreach (DataRow item in mxzdhsdata.Rows)
            {
                Mxzdhs mxzdhs = new Mxzdhs();
                mxzdhs.mxzdh = item["mxzdh"].ToString();
                mxzdhs.totalexpense = decimal.Parse(item["ybzje"].ToString()) + decimal.Parse(item["zfzje"].ToString());
                mxzdhs.ybjsfwfyze = decimal.Parse(item["ybzje"].ToString());
                mxzdhs.fybjsfwfyze = decimal.Parse(item["zfzje"].ToString());
                input.mxzdhs.Add(mxzdhs);
            }
            SI52Output output = new SI52Output();
            string code = "1";
            string jsonOut = ClassHelper.SaveToInterface(input, out output, post, out code);
            if (code == "P001")
            {
                try
                {
                    DateTime date = ClassSqlHelper.GetServerTime();

                    List<string> sqlList = new List<string>();
                    Ybjk_SI52_Input si52entity = new Ybjk_SI52_Input();
                    si52entity = Function.Mapping<Ybjk_SI52_Input, SI52Input>(input);
                    si52entity.Id=Guid.NewGuid().ToString();
                    si52entity.zt = 1;
                    si52entity.czrq = date;
                    si52entity.czydm = post.operatorId;
                    si52entity.zyh = inputsi52.hisId;
                    si52entity.lsh = output.lsh;
                    sqlList.Add(si52entity.ToAddSql());
                    if (input.zdnos != null && input.zdnos.Count > 0)
                    {
                        foreach (Zdnos detail in input.zdnos)
                        {
                            Ybjk_SI52_ZdnosInput zdentity = new Ybjk_SI52_ZdnosInput();
                            zdentity = Function.Mapping<Ybjk_SI52_ZdnosInput, Zdnos>(detail);
                            zdentity.id= Guid.NewGuid().ToString();
                            zdentity.zyh = inputsi52.hisId;
                            zdentity.lsh = output.lsh;
                            sqlList.Add(zdentity.ToAddSql());
                        }
                    }
                    if (input.mxzdhs != null && input.mxzdhs.Count > 0)
                    {
                        foreach (Mxzdhs detail in input.mxzdhs)
                        {
                            Ybjk_SI52_MxzdhsInput zdentity = new Ybjk_SI52_MxzdhsInput();
                            zdentity = Function.Mapping<Ybjk_SI52_MxzdhsInput, Mxzdhs>(detail);
                            zdentity.id= Guid.NewGuid().ToString();
                            zdentity.zyh = inputsi52.hisId;
                            zdentity.lsh = output.lsh;
                            sqlList.Add(zdentity.ToAddSql());
                        }
                    }

                    Ybjk_SI52_Output outentity = new Ybjk_SI52_Output();
                    outentity = Function.Mapping<Ybjk_SI52_Output, SI52Output>(output);
                    outentity.Id = Guid.NewGuid().ToString();
                    outentity.zt = 1;
                    outentity.czrq = date;
                    outentity.czydm = post.operatorId;
                    outentity.zyh = inputsi52.hisId;
                    outentity.lsh = output.lsh;
                    sqlList.Add(outentity.ToAddSql());

                    int eeor = 0;
                    ClassSqlHelper.Merge(sqlList, out eeor);
                }
                catch (Exception ex)
                {
                    AppLogger.Info("医保结算成功，HIS住院收费写结算数据表异常：" + ex.Message);
                    AppLogger.Info("自动执行住院收费结算撤销...");
                    Post_SH02 sh02 = new Post_SH02();
                    sh02.operatorId = inputsi52.operatorId;
                    sh02.operatorName = inputsi52.operatorName;
                    sh02.hisId = inputsi52.hisId;
                    sh02.carddata = inputsi52.carddata;
                    sh02.cardtype = inputsi52.cardtype;
                    sh02.xsywlx = inputsi52.xsywlx;
                    string mzjscs = CancalSettlement_SK01(sh02, output.lsh, "2", output.totalexpense);
                    SHOutputReturn mzjscsRt = JsonConvert.DeserializeObject<SHOutputReturn>(mzjscs);
                    if (mzjscsRt.xxfhm != "P001")
                    {
                        AppLogger.Info("自动执行住院结算撤销失败：" + mzjscsRt.fhxx);
                        return ClassHelper.BuildReturnJson("-99", "医保结算成功，HIS写结算数据表失败：【" + ex.Message + "】，撤销医保结算失败：【" + mzjscsRt.fhxx + "】，请速联系HIS厂商技术人员。");
                    }
                    else
                    {
                        AppLogger.Info("自动执行住院结算撤销成功");
                        return ClassHelper.BuildReturnJson("-99", "医保结算成功，HIS写结算数据表失败，已自动撤销医保结算，请联系HIS厂商技术人员。");
                    }
                }
            }

            return jsonOut;

        }
        #endregion

        #region 对账
        [HttpPost]
		public string ybInterface_SL01(Post_SL01 inputsn01)
		{
			PostBase post = new PostBase();
			post.hisId = inputsn01.hisId;
			post.operatorId = inputsn01.operatorId;
			post.operatorName = inputsn01.operatorName;
			post.tradiNumber = "SL01";
			post.insuplc_admdvs = inputsn01.insuplc_admdvs;

			post.inModel = 1;

			SL01Input inputsl = new SL01Input();
			inputsl = Function.Mapping<SL01Input, Post_SL01>(inputsn01);
			SL01Output output = new SL01Output();
			string code = "1";
			string jsonOut = ClassHelper.SaveToInterface(inputsl, out output, post, out code);
            if (code == "P001")//如果成功则写入医保
            {
                DateTime date = ClassSqlHelper.GetServerTime();
                List<string> sqlList = new List<string>();
                Ybjk_DZ_output dzjgcs = new Ybjk_DZ_output();
                dzjgcs = Function.Mapping<Ybjk_DZ_output, SL01Input>(inputsl);
                dzjgcs.resultcollate = output.resultcollate;
                dzjgcs.outdaycollate = output.daycollate;
                dzjgcs.zt = 1;
                dzjgcs.czrq = date;
                dzjgcs.State = "1";
                sqlList.Add(dzjgcs.ToAddSql());
                int eeor = 0;
                ClassSqlHelper.Merge(sqlList, out eeor);
                if (eeor < 0)
                {
                    JObject jsonUP = JObject.Parse(jsonOut);
                    jsonUP["fhxx"] = jsonUP["err_msg"] + "HIS信息提示：写入对账表失败Ybjk_DZ_output-100";
                    jsonUP["xxfhm"] = "-100";
                    return Convert.ToString(jsonUP);
                }
            }
            else {
                DateTime date = ClassSqlHelper.GetServerTime();
                List<string> sqlList = new List<string>();
                Ybjk_DZ_output dzjgcs = new Ybjk_DZ_output();
                dzjgcs = Function.Mapping<Ybjk_DZ_output, SL01Input>(inputsl);
                dzjgcs.resultcollate = "";
                dzjgcs.outdaycollate = "";
                dzjgcs.zt = 1;
                dzjgcs.czrq = date;
                JObject jsonUP = JObject.Parse(jsonOut);
                dzjgcs.State = "0";
                dzjgcs.Result = jsonUP["fhxx"].ToString();
                sqlList.Add(dzjgcs.ToAddSql());
                int eeor = 0;
                ClassSqlHelper.Merge(sqlList, out eeor);
                if (eeor < 0)
                {
                    jsonUP["fhxx"] = jsonUP["err_msg"] + "HIS信息提示：写入对账表失败Ybjk_DZ_output-100";
                    jsonUP["xxfhm"] = "-100";
                    return Convert.ToString(jsonUP);
                }
            }
			return jsonOut;
		}
        #endregion

        #region  语音报价器
        /// <summary>
        /// 调用报价器语音
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
		public string BJQ_01(BJQInput_01 input)
		{
            try
            {
                input.comport = 1;
                if (input != null)
                {
                    int comport = input.comport.Value;
                    string outstr = input.context;
                    if (input.typemc != "")
                    {
                        outstr = input.context + input.typemc;
                    }
                    int refint = ClassHelper.Dsbdll(comport, outstr);

                }
            }
            catch (Exception e) { }

            return "";
		}
        #endregion
    }
}
