using System.Data;
using Newtouch.HIS.Application.Interface.BusinessManage;
using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Tools;
using System;
using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.Common.Operator;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.IRepository;
using Newtonsoft.Json;
using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Core.Common.Exceptions;
using newtouchyibao;

namespace Newtouch.HIS.Application.Implementation.BusinessManage
{
    /// <summary>
    /// 
    /// </summary>
    public class RefundApp : AppBase, IRefundApp
    {
        public int jsnmEx = 0;
        private OperatorModel _userModel;
        public OperatorModel userModel
        {
            get
            {
                if (_userModel == null)
                {
                    _userModel = OperatorProvider.GetCurrent();//获取当前登录用户对象
                }
                return _userModel;
            }
        }

        public DataTable zffsDt = new DataTable();
        public List<FinanceReceiptEntity> newFphOrSj = new List<FinanceReceiptEntity>();
        public readonly IFinanceReceiptRepo _finRecRepos; //收据凭证号记录
        public List<OutpatientRegistNonAttendanceEntity> ghjsDt = new List<OutpatientRegistNonAttendanceEntity>();

        /// <summary>
        /// 收款支付方式
        /// </summary>
        public DataTable ZffsDt
        {
            get { return zffsDt; }
            set { zffsDt = value; }
        }

        public bool jxyb = false;

        public string kh = string.Empty;

        /// <summary>
        /// 卡号
        /// </summary>
        public string Kh
        {
            get { return kh; }
            set { kh = value; }
        }

        public bool isReturnAll = false;

        /// <summary>
        /// 是否全退
        /// </summary>
        public bool IsReturnAll
        {
            get { return isReturnAll; }
            set { isReturnAll = value; }
        }

        public string lblYtk = "";
        public string lblSrce = "";
        private int jsnm = 0;

        /// <summary>
        /// 门诊结算
        /// </summary>
        public int Jsnm
        {
            get { return jsnm; }
            set { jsnm = value; }
        }

        public OutpatientSettlementEntity mzJs = new OutpatientSettlementEntity();
        public string lblFPHNew = "";
        public List<string> fpsqlList = new List<string>();
        private List<string> listSql = new List<string>();

        /// <summary>
        /// 需退费插入对应表信息
        /// </summary>
        public List<string> ListSql
        {
            get { return listSql; }
            set { listSql = value; }
        }

        public JS_ENTITY accountEntity = new JS_ENTITY();

        /// <summary>
        /// 需退费插入结算信息
        /// </summary>
        public JS_ENTITY AccountEntity
        {
            get { return accountEntity; }
            set { accountEntity = value; }
        }

        public bool posTxj = false;
        public bool isAlreadyPosCX = false;
        public bool isChangeBrxz = false;

        /// <summary>
        /// 是否自费转医保
        /// </summary>
        public bool IsChangeBrxz
        {
            get { return isChangeBrxz; }
            set { isChangeBrxz = value; }

        }

        private readonly IRefundDmnService _RefundService;
        private readonly IOutPatientDmnService _IOutPatientDmnService;
        private NB_INTERFACE_MZJS_REQ mzjsReq = null;
        private NB_INTERFACE_MZSS_REP mzssRep = null;
        private bool isAlreadyComfirmTF = false;
        private List<OutpatientSettlementCategoryEntity> jsdlList = new List<OutpatientSettlementCategoryEntity>();
        private string sqxh = string.Empty;
        private string lblSsk = "";
        private OutpatientSettlementEntity newMzJs = new OutpatientSettlementEntity();

        private List<OutpatientSettlementPaymentModelEntity> jszffsArray =
            new List<OutpatientSettlementPaymentModelEntity>();

        private readonly ISysConfigRepo _sysConfigRepo;

        private Dictionary<string, List<GridViewMx>> resultList = new Dictionary<string, List<GridViewMx>>()
        {
            {"all", new List<GridViewMx>()},
            {"any", new List<GridViewMx>()},
            {"nothing", new List<GridViewMx>()}
        };

        public List<OutpatientSettlementPaymentModelEntity> JszffsArray
        {
            get { return jszffsArray; }
        }

        //收款结算支付方式
        private List<tbl_mz_jszffs_Ex> jszffsExListForSK = new List<tbl_mz_jszffs_Ex>();

        //结算支付方式
        private List<tbl_mz_jszffs_Ex> jszffsExList = new List<tbl_mz_jszffs_Ex>();

        private string kahao;

        public List<MZJSInfo> GetFPHByKh(string kh, string startTime, string endTime, string fph)
        {

            return _RefundService.GetFPHByKh(kh, startTime, endTime, fph);
        }

        public List<MZJS> GetMZJSByJsnm(int jsnm)
        {

            return _RefundService.GetMZJSByJsnm(jsnm);
        }

        public List<GridViewMx> GetGridViewMx(int jsnm)
        {
            /// 门诊项目明细
            var data = _RefundService.GetMz_xmRecordsByJsnm(jsnm);
            /// 门诊挂号项目明细
            var data2 = _RefundService.GetMz_ghxmRecordsByJsnm(jsnm);
            /// 门诊处方明细
            var data3 = _RefundService.GetMz_cfmxRecordsByJsnm(jsnm);
            int id = 0;
            data.ForEach(p =>
            {
                id = id + 1;
                p.id = id;
            });
            if (data2.Count > 0)
            {
                data2.ForEach(p =>
                {
                    id = id + 1;
                    p.id = id;
                    p.czh = "gh0209";
                    data.Add(p);
                });

            }
            if (data3.Count > 0)
            {
                data3.ForEach(p =>
                {
                    id = id + 1;
                    p.id = id;
                    data.Add(p);
                });
            }

            return data;
        }

        public decimal getSysl(int patid, int ghnm)
        {
            return _RefundService.getSysl(patid, ghnm);
        }

        public List<OutpatientRegistNonAttendanceEntity> getGhjsByGhnm(int tmpGhnm)
        {
            return _RefundService.getGhjsByGhnm(tmpGhnm);
        }

        /// <summary>
        /// 退费
        /// </summary>
        public bool btnReturn(string kh, List<GridViewMx> GridViewMxList, int jsnm, out bool isReturnAll)
        {
            bool result = false;
            kahao = kh;
            Jsnm = jsnm;
            var data = GetFPHByKh(kh, "", "", "");
            var fapiao = data.Find(p => p.jsnm == jsnm);

            var GridViewMxData = GetGridViewMx(jsnm);
            GridViewMxList.ForEach(p =>
            {
                var tem = GridViewMxData.Find(c => c.id == p.id);
                p.tag = tem.tag;
            });


            OutpatientSettlementEntity tblMzJs = new OutpatientSettlementEntity();
            tblMzJs.fph = fapiao.oldFPH;
            tblMzJs.flzffy = fapiao.flzffy;
            tblMzJs.zffy = fapiao.zffy;
            tblMzJs.jzfy = fapiao.jzfy;
            tblMzJs.zlfy = fapiao.zlfy;

            tblMzJs.jsnm = fapiao.jsnm;
            tblMzJs.patid = fapiao.patid;
            tblMzJs.xjzf = fapiao.xjzf;
            tblMzJs.jslx = fapiao.jslx;
            tblMzJs.brxz = fapiao.brxz;

            tblMzJs.zje = fapiao.zje;
            tblMzJs.xjwc = fapiao.xjwc;
            tblMzJs.zhjz = fapiao.zhjz;
            tblMzJs.xjzffs = fapiao.xjzffs;
            tblMzJs.jszt = fapiao.jszt;
            tblMzJs.cxjsnm = fapiao.cxjsnm;
            tblMzJs.zh = fapiao.zh;
            tblMzJs.fpdm = fapiao.fpdm;
            //  tblMzJs.jmbl = fapiao.jmbl;
            tblMzJs.jmje = fapiao.jmje;
            tblMzJs.jylx = fapiao.jylx;
            tblMzJs.ysk = fapiao.ysk;
            tblMzJs.zl = fapiao.zl;
            tblMzJs.jzsj = fapiao.CreateTime;

            if (tblMzJs != null)
            {
                //除挂号之外全部退费
                var MZJS = GetMZJSByJsnm(jsnm);
                var tmpDr = MZJS[0];
                int tmpGhnm = tmpDr.GHNM;

                if (tblMzJs.jslx == "0")
                {
                    decimal sysl = getSysl(tblMzJs.patid, tmpGhnm);

                    if (sysl > 0)
                    {
                        throw new FailedCodeException("OUTPAT_REFUNG_FAIL_GUHAO");

                    }
                }


                DataTable zffsDt = _RefundService.GetMzJszffsForJsnm(tblMzJs.jsnm);


                //设置全退，半退，不退明细list

                getRecordWithCategory(GridViewMxList);
                JS_ENTITY accountEntityAll = getAccountList(2, tblMzJs, tmpDr);
                JS_ENTITY accountEntity = getAccountList(1, tblMzJs, tmpDr);
                if (accountEntityAll.jsxmList.Count == 0)
                {
                    accountEntityAll = accountEntity;
                }
                isReturnAll = (resultList["any"].Count == 0 && resultList["nothing"].Count == 0);


                //记录退号表
                List<OutpatientRegistNonAttendanceEntity> ghjsDt = new List<OutpatientRegistNonAttendanceEntity>();
                if (fapiao.jslx == "0" && fapiao.zje > 2.0M) //退磁卡费，工本费不插退号记录
                {
                    //获取挂号结算信息
                    ghjsDt = getGhjsByGhnm(tmpGhnm);

                }
                result = false;

                if (true == showTuiFeiJsFrm(kh, zffsDt, tblMzJs.jsnm, isReturnAll, accountEntity, ghjsDt,
                        accountEntityAll))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }


            }
            else
            {
                throw new FailedException("OUTPAT_REFUNG_FP_NULL");
            }

            return result;

        }

        public bool showTuiFeiJsFrm(string kh, DataTable ZffsDt, int jsnm, bool isReturnAll, JS_ENTITY accountEntity,
            List<OutpatientRegistNonAttendanceEntity> ghjsDt, JS_ENTITY accountEntityAll)
        {
            var returnsOk = true;
            IsReturnAll = isReturnAll;
            Kh = kh;
            try
            {
                FrmJS_Load(kh, jsnm, ZffsDt, isReturnAll, accountEntity, ghjsDt, accountEntityAll);
            }
            catch
            {
                returnsOk = false;
            }
            return returnsOk;
        }

        private void FrmJS_Load(string kh, int jsnm, DataTable ZffsDt, bool isReturnAll, JS_ENTITY accountEntity,
            List<OutpatientRegistNonAttendanceEntity> GhjsDt, JS_ENTITY accountEntityAll)
        {
            this.isAlreadyPosCX = false;
            this.posTxj = false;
            mzJs = _RefundService.GetMZJSFromMz_jsByJsnm(jsnm);
            zffsDt = ZffsDt;
            ghjsDt = GhjsDt;

            if (isReturnAll == false && (accountEntity.isGh == false ||
                                         (accountEntity.isGh == true && accountEntity.isZh == true)))
            {


                //加床记账半退打凭证(清单)
                //if (mzJs.jslx == "1" && mzJs.jylx == "14" && mzJs.jch != 0)
                //{
                //    flag = false;
                //}

                //半退则开始获取发票号
                bool flag = true;

                // List<string> fpsql = new List<string>();
                if (flag == true)
                {

                    lblFPHNew =
                        _IOutPatientDmnService.GetInvoiceListByEmpNo(this.UserIdentity.UserCode, this.OrganizeId);
                    //插入/更新cw_fp

                    if (!_RefundService.CheckFPH(lblFPHNew))
                    {

                        //fpsqlList.AddRange(fpsql); ;
                    }
                    else
                    {
                        lblFPHNew = string.Empty;
                        throw new FailedException("GET_FPH_FAIL");
                    }
                }
                else
                {
                    lblFPHNew = _finRecRepos.getDQSJH(userModel.UserCode, this.OrganizeId);
                    string type = string.Empty;
                    FinanceReceiptEntity sjEntity = new FinanceReceiptEntity();

                    if (_finRecRepos.checkSJH(lblFPHNew, userModel.UserCode, out sjEntity, out type, this.OrganizeId))
                    {
                        //fpsqlList.AddRange(fpsql); 
                        if (type == "insert")
                        {
                            _finRecRepos.AddReceiptInfo(sjEntity, this.OrganizeId); // 添加收据凭证号
                        }
                        if (type == "update")
                        {
                            _finRecRepos.UpdateReceiptInfo(sjEntity, sjEntity.cwsjId); //修改收据凭证号
                        }
                    }
                    else
                    {
                        lblFPHNew = string.Empty;
                        throw new FailedCodeException("GET_SJH_FAIL");
                    }
                }
            }


            lblSsk = (mzJs.xjzf).ToString("0.00");


            //第一次请求交易，判断账户是否封存

            if (false == isReturnAll) //部分退需要重新申请医保
            {
                //先请求交易,在计算交易
                newMzJs = applyBackPart(accountEntity, out sqxh, out jsdlList);
                newMzJs.zh = mzJs.zh;
            }


            //初始化数据

            //初始化控件
            // initControl();
            //加载数据
            loadData(mzJs, isReturnAll, mzJs.jsnm);


            //全部退款
            // try
            // {
            bool result = isAlreadyComfirmTF;
            mzJs.CreateTime = DateTime.Now; //获取系统服务器时间
            mzJs.CreatorCode = userModel.UserCode; //记录退费操作员

            //    pos机全额退费

            #region   pos机全额退费

            //结算支付方式记录（退款方式）
            jszffsArray = getJszffsEntityForSK();
            if (JszffsArray.Count == 1)
            {
                if (JszffsArray.Find(p => p.xjzffs == "1") != null)
                {
                    //pos方法
                    if (PosJY(kahao, mzJs, mzJs.xjzf, isReturnAll) == false)
                    {
                        //    this.Close();
                        return;
                    }
                }
            }
            else
            {
                //多退款方式,半退pos
                if (JszffsArray.Count > 1)
                {
                    OutpatientSettlementPaymentModelEntity mz_jszf = JszffsArray.Find(p => p.xjzffs == "1");
                    if (mz_jszf != null)
                    {
                        if (PosJY(kahao, mzJs, mz_jszf.zfje.ToDecimal(), IsReturnAll) == false)
                        {
                            // this.Close();
                            return;
                        }
                    }

                }
            }

            #endregion


            result = fullBack(Kh, accountEntity.isGh, mzJs, ghjsDt);


            //第二次请求交易
            if (false == isReturnAll) //部分退需要重新申请医保
            {
                //先请求交易,在计算交易
                newMzJs = applyBackPart(accountEntity, out sqxh, out jsdlList);
                newMzJs.zh = mzJs.zh;
                //退款后再次请求医保计算现金支付
                YBTKloadData();
            }




            //报价器退款
            //string setFileName = (new Common.ConfigFile.AppConfig()).AppConfigGet("INIFileName");   // ConfigurationManager.AppSettings["INIFileName"].ToString();
            //string fileName = System.IO.Path.GetFullPath("../../") + "\\" + setFileName + ".ini";
            //IniFile Ini = new IniFile(fileName);
            //string BJQpzStr = Ini.IniReadValue("本机参数", "BJQPZparm");
            //decimal tk = Common.Tools.AnyToDecimal(lblYtk.Text);
            //BF<BjqInterface>.Instance.mz_tk(tk, ClsCurrentUserInfo.UserLogin, BJQpzStr);

            if (jszffsExListForSK.Count < 2 && this.posTxj == false && false == isReturnAll)
            {
                //多支付方式弹出退款界面
                //this.btnOk.PerformClick();
                btnOk_Click(newMzJs, accountEntity, mzJs, jsdlList);
            }

        }

        /// <summary>
        /// 按全退，半退，不退取明细
        /// </summary>
        /// <returns>全退，半退，不退明细</returns>
        private void getRecordWithCategory(List<GridViewMx> GridViewMxList)
        {
            resultList["all"].Clear();
            resultList["any"].Clear();
            resultList["nothing"].Clear();
            foreach (var GridViewMx in GridViewMxList)
            {

                decimal tmpSl = 0.00M;
                decimal tmpReturnSl = 0.00M;

                decimal.TryParse(string.Format("{0}", GridViewMx.SL), out tmpSl);
                decimal.TryParse(string.Format("{0}", GridViewMx.RETURNS_SL), out tmpReturnSl);
                if (tmpSl == tmpReturnSl && GridViewMx.IS_RETURN == "true")
                {
                    resultList["all"].Add(GridViewMx); //全退
                }
                if ((tmpSl > tmpReturnSl) && GridViewMx.IS_RETURN == "true") //半退
                {
                    resultList["any"].Add(GridViewMx);
                }
                else if ("false" == GridViewMx.IS_RETURN) //不退
                {
                    resultList["nothing"].Add(GridViewMx);
                }
            }
        }

        /// <summary>
        /// 取半退，不退的结算信息 （1），取全退的结算信息（2）
        /// </summary>
        /// <returns></returns>
        private JS_ENTITY getAccountList(int flag, OutpatientSettlementEntity fapiao, MZJS tmpDr)
        {
            int tmpGhnm;

            JS_ENTITY returnAny = new JS_ENTITY(); //半退的不退部分
            returnAny.patid = fapiao.patid;
            if (tmpDr != null)
            {
                returnAny.ghnm = tmpDr.GHNM;
            }
            returnAny.isGh = fapiao.jslx == "0";
            returnAny.jslx = fapiao.jslx;
            returnAny.brxz = fapiao.brxz;
            returnAny.jsxmList = new List<jsxm_entity>();
            returnAny.ghnmList = new List<int>();

            List<GridViewMx> listDataRow = new List<GridViewMx>();
            if (flag == 1)
            {
                listDataRow = resultList["any"];
                listDataRow.AddRange(resultList["nothing"]);
            }
            else
            {
                listDataRow = resultList["all"];
            }
            foreach (var GridViewMx in listDataRow)
            {
                decimal dj = 0.00M;
                decimal fwfdj = 0.00M;
                decimal je = 0.00M;
                decimal sl = 0.00M;
                decimal zfbl = 0.00M;
                decimal returnSl = 0.00M;
                string infoSet = string.Empty;

                DataRow tmpDataRow = GridViewMx.tag as DataRow;
                infoSet = string.Format("{0}", GridViewMx.InfoSet);

                dj = GridViewMx.DJ.ToDecimal(); //加服务费单价
                fwfdj = GridViewMx.FWFDJ.ToDecimal();
                je = GridViewMx.JE.ToDecimal();
                sl = GridViewMx.SL.ToDecimal();
                zfbl = GridViewMx.ZFBL.ToDecimal();
                returnSl = GridViewMx.RETURNS_SL.ToDecimal();

                jsxm_entity tmpJsxm = new jsxm_entity();
                tmpJsxm.dj = (dj + fwfdj);
                tmpJsxm.dl = GridViewMx.DL;
                if (GridViewMx.IS_RETURN == "true")
                {
                    tmpJsxm.je = (dj + fwfdj) * (sl - returnSl);
                    tmpJsxm.sl = (sl - returnSl);
                }
                else
                {
                    tmpJsxm.je = (dj + fwfdj) * (sl);
                    tmpJsxm.sl = sl;
                }
                //tmpJsxm.mxbm = string.Empty;

                //成组号和处方号
                tmpJsxm.czh = GridViewMx.czh;
                tmpJsxm.cfh = GridViewMx.CHUFANGHAO;


                if (infoSet == "mz_xm" || infoSet == "mz_ghxm")
                {
                    tmpJsxm.sfxm = string.Format("{0}", tmpDataRow["SFXM"]);
                    tmpJsxm.mxnm = tmpDataRow["XMNM"].ToInt();
                }
                else if (infoSet == "mz_cfmx")
                {
                    tmpJsxm.sfxm = string.Format("{0}", tmpDataRow["SFXM"]);
                    //tmpJsxm.mxbm = string.Format("{0}", tmpDataRow["SFXM"]);
                    tmpJsxm.cf_mxnm = GridViewMx.CF_MXNM.ToInt();
                }
                tmpJsxm.zfbl = zfbl;
                tmpJsxm.zfxz = string.Format("{0}", tmpDataRow["ZFXZ"]);
                tmpJsxm.Xmbm = string.Format("{0}", tmpDataRow["NBDL"]);
                returnAny.jsxmList.Add(tmpJsxm);

                //添加挂号内码list，过滤重复记录
                int.TryParse(string.Format("{0}", tmpDataRow["GHNM"]), out tmpGhnm);
                if (returnAny.ghnmList.Contains(tmpGhnm) == false)
                {
                    returnAny.ghnmList.Add(tmpGhnm);
                }
            }
            return returnAny;
        }

        /// <summary>
        /// 部分退请求医保交易
        /// </summary>
        /// <param name="accountEntity"></param>
        /// <param name="sqxh"></param>
        /// <returns></returns>
        private OutpatientSettlementEntity applyBackPart(JS_ENTITY accountEntity, out string sqxh,
            out List<OutpatientSettlementCategoryEntity> jsdl)
        {
            bool ybError = true;
            OutpatientSettlementEntity newMzJs = getMzJs(accountEntity, true, out ybError, out sqxh, out jsdl,
                out mzjsReq, out mzssRep);
            //如果请求医保交易失败，则转自费
            //if (ybError)
            //{
            //  
            //    //if (DialogHelper.Ask_OkCancel("医保交易失败，是否要转自费交易？") == DialogResult.OK)
            //    //{
            //    FrmBrxzEdit zfForm = new FrmBrxzEdit();
            //    zfForm.currBrxzEntity = BF<xt_brxz>.Instance.GetEntityByID(accountEntity.brxzbh.ToString());
            //    zfForm.brnm = accountEntity.brnm;
            //    if (zfForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    {
            //        jxyb = zfForm.jxyb;//继续医保交易
            //        if (!jxyb)
            //        {
            //            accountEntity.brxzbh = zfForm.currBrxzEntity.brxzbh;
            //            accountEntity.isZh = true;
            //            newMzJs = BF<mz_js>.Instance.getMzJs(accountEntity, out ybError, out sqxh, out jsdl, out mzjsReq, out mzssRep);
            //        }
            //    }
            //    else
            //    {
            //        jxyb = false;
            //        throw new FailedCodeException("医保交易失败!");
            //    }
            //    //}
            //}
            newMzJs.ysk = newMzJs.xjzf;
            newMzJs.zl = 0.00m;
            return newMzJs;
        }

        /// <summary>
        /// 需要结算的项目信息
        /// </summary>
        /// <param name="jsEntity"></param>
        /// <param name="needCalJm">是否需要计算减免</param>
        /// <param name="ybError"></param>
        /// <returns></returns>
        public OutpatientSettlementEntity getMzJs(JS_ENTITY jsEntity, bool needCalJm, out bool ybError, out string sqxh,
            out List<OutpatientSettlementCategoryEntity> jsdl, out NB_INTERFACE_MZJS_REQ msjsReq,
            out NB_INTERFACE_MZSS_REP mzssRep)
        {
            //不做减免功能
            //if (needCalJm)
            //{
            //    if (jsEntity.jsxmList == null)
            //        throw new FailedCodeException("JSXM_REQUIRED");

            //    var brxz = _RefundService.GetBrxzByBrbh(Tool.AnyToInt32(jsEntity.brxzbh));
            //    if (brxz == null)
            //        throw new FailedCodeException("OUTPAT_PATIENT_NATURE_IS_NOT_EXIST");

            //    计算收费算法：收费项目、减免费用、收费算法

            //    decimal outJmbl = 0m;
            //    decimal outJmje = 0m;
            //    foreach (jsxm_entity jsxm in jsEntity.jsxmList)
            //    {
            //        Calcjm(brxz.brxz, jsxm.dl, jsxm.sfxm, ((jsxm.dj + jsxm.fwfdj) * jsxm.sl).ToString(), out outJmbl, out outJmje);
            //        jsxm.jmbl = outJmbl;
            //        jsxm.jmje = outJmje;
            //    }
            //}

            return getMzJs(jsEntity, out ybError, out sqxh, out jsdl, out msjsReq, out mzssRep);
        }

        /// <summary>
        /// 需要结算的项目信息
        /// </summary>
        /// <param name="jsEntity"></param>
        /// <returns></returns>
        public OutpatientSettlementEntity getMzJs(JS_ENTITY jsEntity, out bool ybError, out string sqxh,
            out List<OutpatientSettlementCategoryEntity> jsdl,
            out NB_INTERFACE_MZJS_REQ msjsReq, out NB_INTERFACE_MZSS_REP mzssRep)
        {
            msjsReq = null;
            mzssRep = null;

            //根据病人性质，得到访问医保接口的方式(该病人性质仅用于判断访问哪个医保接口) 
            var brxz = _RefundService.GetBrxzByBrbh(jsEntity.brxz);
            if (brxz == null)
                throw new FailedCodeException("HOSP_PATIENT_NATURE_IS_NOT_EXIST");

            //if (brxz.ybjylx == Constants.ybjylx.ybjylx6)
            //{
            //    //新农合交易
            //    return getMzJsNb(jsEntity, brxz, out ybError, out sqxh, out jsdl, out msjsReq, out mzssRep);
            //}
            //else
            //{
            //    //自费及医保交易
            //    return getMzJs(jsEntity, brxz, out ybError, out sqxh, out jsdl);
            //}
            return getMzJs(jsEntity, brxz, out ybError, out sqxh, out jsdl);
        }

        /// <summary>
        /// 需要结算的项目信息
        /// </summary>
        /// <param name="jsEntity"></param>
        /// <returns></returns>
        public OutpatientSettlementEntity getMzJs(JS_ENTITY jsEntity, SysPatientNatureEntity brxz, out bool ybError,
            out string sqxh, out List<OutpatientSettlementCategoryEntity> jsdl)
        {
            bool isScgh = false;
            Constants.ybDealLB jylx = Constants.ybDealLB.yb_deal_wjy;
            sqxh = string.Empty;
            ybError = false;

            if (jsEntity.jsxmList == null)
                throw new FailedCodeException("HOSP_SETTLEMENTITEM_IS_NULL");
            if (jsEntity.ghnmList == null || jsEntity.ghnmList.Count == 0)
                throw new FailedCodeException("GHNM_REQUIRED");

            OutpatientSettlementEntity js = new OutpatientSettlementEntity();
            //不同病人性质和门急诊不能一起结算

            /// 根据挂号内码获取挂号信息
            List<OutpatientRegistEntity> ghList = _RefundService.getGhs(jsEntity.ghnmList);

            if (jsEntity.ghnmList.Count != ghList.Count)
                throw new FailedCodeException("MSG_ERROR");

            //不同病人性质和门急诊不能一起结算
            checkGhjs(ghList);

            //根据挂号内码获取挂号信息
            OutpatientRegistEntity gh = ghList[0];

            //根据病人内码获取病人信息
            SysPatientBasicInfoEntity brjbxx = _RefundService.GetBrjbxxByPatid(jsEntity.patid);
            if (brjbxx == null)
                throw new FailedCodeException("HOSP_SYSPATIENT_BASICINFO_IS_NOT_EXIST");

            ////根据病人性质，得到访问医保接口的方式(该病人性质仅用于判断访问哪个医保接口) 
            //tbl_xt_brxz brxz = BF<xt_brxz>.Instance.GetEntityByID(jsEntity.brxzbh.ToString());
            //if (brxz == null)
            //    throw new FailedCodeException("该病人的病人性质在系统中不存在，请确认！");

            //因为存在组合病人性质，所以不做该判断
            //如果不存在医保与自费转换，当前病人性质必须与挂号相同
            //if (!jsEntity.isZh)
            //{
            //    if (gh.brxz != brxz.brxz)
            //        throw new FailedCodeException("当前病人性质与挂号病人性质不一致，请确认！");
            //}
            //if (!jsEntity.isGh)
            //{
            //    //计算服务费
            //    CMS_Business cb = new CMS_Business();
            //    cb.CalFwf(brxz.brxz, jsEntity.jsxmList);
            //}

            //根据收费项目和病人收费算法，得到计算结果集
            Brsfsf_ReturnValue_YBANDFYB returnValue;
            getBrsfResultNB(jsEntity, brxz.brxz, out returnValue);


            js.jsnm = _RefundService.GetPrimaryKeyByTableName("mz_js");
            js.patid = brjbxx.patid;
            js.brxz = brxz.brxz;
            js.jslx = jsEntity.jslx;
            js.zlfy = returnValue.zlHj + returnValue.sfzlHj; //自理费用
            js.zffy = returnValue.sfzfHj; //自负费用
            js.flzffy = returnValue.flzfHj; //分类自负费用
            js.jzfy = returnValue.jzfyHj; //记账费用，如果是医保病人，为医保返回的结果
            js.jmje = returnValue.jmjeHj; //减免费用
            js.jszt = (int)Constants.jsztEnum.YJ; //结算状态

            js.xjzffs = string.Empty;
            // js.jch = jsEntity.jch;

            //现金支付
            decimal ssk = 0m;
            //现金误差
            decimal difference = 0m;
            //医保返回自负结果
            decimal ybzf = 0m;
            //医保返回可记账费用
            decimal jzfy = 0m;
            //应收款
            decimal ysk = 0m;

            string ybjylx = brxz.ybjylx;
            //金额为0，不访问医保接口

            ybjylx = Constants.ybjylx.ybjylx0;
            //if (returnValue.ybjsfwze == 0 && returnValue.jyze == 0)
            //{
            //    ybjylx = Constants.ybjylx.ybjylx0;
            //}
            ////如果是家床且为挂号，则不访问医保接口,通过金额判断
            //if (ybjylx == Constant.ybjylx.ybjylx3 && jsEntity.isGh)
            //{
            //    ybjylx = Constant.ybjylx.ybjylx0;
            //}
            //医保病人，并且是造口袋项目，进行造口袋交易
            //if (ybjylx != Constants.ybjylx.ybjylx0)
            //{
            //    //造口袋和非造口袋项目必须分开结算
            //    bool zkd = isZkd(jsEntity);
            //    if (zkd)
            //    {
            //        ybjylx = "ZKD";
            //    }
            //}

            switch (ybjylx)
            {
                //不交易
                case Constants.ybjylx.ybjylx0:
                    ysk = returnValue.totalHj;
                    jylx = Constants.ybDealLB.yb_deal_wjy;
                    break;

                    #region

                    //普通交易
                    //case Constants.ybjylx.ybjylx1:
                    //    ybptjy(jsEntity.isGh, brjbxx, gh, brxz, returnValue.YB, out ybzf, out jzfy, out ybError, out sqxh, out isScgh);
                    //    //记账费用
                    //    js.jzfy = returnValue.FYB.jzfyHj + jzfy;
                    //    //应收款
                    //    ysk = returnValue.xjHj + ybzf;
                    //    //如果是伤残普通挂号，挂号费为记账
                    //    if (isScgh)
                    //    {
                    //        js.zlfy = js.zlfy - returnValue.YB.fybjsfwgrzf;
                    //        ysk = ysk - returnValue.YB.fybjsfwgrzf;
                    //    }
                    //    if (jsEntity.isGh)
                    //    {
                    //        jylx = Constants.ybDealLB.yb_deal_mzgh;
                    //    }
                    //    else
                    //    {
                    //        jylx = Constants.ybDealLB.yb_deal_mzsf;
                    //    }
                    //    break;

                    ////大病交易
                    //case Constants.ybjylx.ybjylx2:
                    //    ybdbjy(jsEntity.isGh, brjbxx, gh, brxz, returnValue.YB, out ybzf, out jzfy, out ybError, out sqxh, out isScgh);
                    //    //记账费用
                    //    js.jzfy = returnValue.FYB.jzfyHj + jzfy;
                    //    //应收款
                    //    ysk = returnValue.xjHj + ybzf;
                    //    //如果是伤残普通挂号，挂号费为记账
                    //    if (isScgh)
                    //    {
                    //        js.zlfy = js.zlfy - returnValue.YB.fybjsfwgrzf;
                    //        ysk = ysk - returnValue.YB.fybjsfwgrzf;
                    //    }

                    //    if (jsEntity.isGh)
                    //    {
                    //        jylx = Constants.ybDealLB.yb_deal_dbgh;
                    //    }
                    //    else
                    //    {
                    //        jylx = Constants.ybDealLB.yb_deal_dbsf;
                    //    }
                    //    break;

                    ////家床交易
                    //case Constants.ybjylx.ybjylx3:
                    //    //家床交易总额(传给医保的)，账户余额，门诊自负段现金支付累计数，门诊自负段定额
                    //    decimal jc_Jyze = 0;
                    //    //decimal jc_zhye = 0, jc_mzzfdxjzfljs = 0, jc_mzzfdde = 0;
                    //    //是否离休、在职配置的比例
                    //    decimal jc_bl = 0;
                    //    if (jsEntity.jslx == "1")
                    //    {
                    //        jc_Jyze = returnValue.jyze;//(包含了记账费用)                 
                    //        //家床记账模拟交易
                    //        //获取门诊自负段现金支付累计数、门诊自负段定额
                    //        //DataTable dtYbzh = BLLFactory<yb_brzhbz>.Instance.getYBbrzhxx(brjbxx.kh);
                    //        //if (dtYbzh != null)
                    //        //{
                    //        //    if (dtYbzh.Rows.Count > 0)
                    //        //    {
                    //        //        jc_mzzfdxjzfljs = Tools.AnyToDecimal(dtYbzh.Rows[0]["mzzfdxjzfljs"].ToString());
                    //        //        jc_mzzfdde = Tools.AnyToDecimal(dtYbzh.Rows[0]["mzzfdde"].ToString());
                    //        //    }
                    //        //}

                    //        //模拟医保算法update 51200 20150921
                    //        //jzfy = (jc_Jyze - jc_mzzfdxjzfljs - jc_mzzfdde) * jc_bl;//医保可报记账费用
                    //        jzfy = jc_Jyze * (1 - jc_bl);//医保可报记账费用
                    //        ybzf = jc_Jyze - jzfy;
                    //        //记账费用
                    //        js.jzfy = returnValue.FYB.jzfyHj + jzfy;
                    //        //应收款
                    //        ysk = returnValue.xjHj + ybzf;
                    //        jylx = Constants.ybDealLB.yb_deal_jcjz;
                    //    }
                    //    else
                    //    {
                    //        ybjcjy(jsEntity.isGh, brjbxx, gh, brxz, returnValue.YB, jsEntity.jch, out ybzf, out jzfy, out ybError, out sqxh);
                    //        //记账费用
                    //        js.jzfy = returnValue.FYB.jzfyHj + jzfy;
                    //        //应收款
                    //        ysk = returnValue.xjHj + ybzf;

                    //        jylx = Constants.ybDealLB.yb_deal_jcsf;
                    //    }

                    //    break;

                    ////工伤交易
                    //case Constants.ybjylx.ybjylx4:
                    //    ybgsjy(jsEntity.isGh, brjbxx, gh, brxz, returnValue.YB, out ybzf, out jzfy, out ybError, out sqxh);
                    //    //记账费用
                    //    js.jzfy = returnValue.FYB.jzfyHj + jzfy;
                    //    //应收款
                    //    ysk = returnValue.xjHj + ybzf;
                    //    if (jsEntity.isGh)
                    //    {
                    //        jylx = Constants.ybDealLB.yb_deal_gsgh;
                    //    }
                    //    else
                    //    {
                    //        jylx = Constants.ybDealLB.yb_deal_gsmz;
                    //    }
                    //    break;

                    ////造口袋交易
                    //case "ZKD":
                    //    ybmxxmjy(jsEntity.isGh, brjbxx, gh, brxz, jsEntity, out ybzf, out jzfy, out ybError, out sqxh);
                    //    //记账费用
                    //    js.jzfy = returnValue.FYB.jzfyHj + jzfy;
                    //    //应收款
                    //    ysk = returnValue.xjHj + ybzf;

                    //    jylx = Constants.ybDealLB.yb_deal_sssf;
                    //    break;

                    #endregion
            }
            //交易类型
            js.jylx = ((int)jylx).ToString();
            //自负费用
            js.zffy += ybzf;
            Ext.get5s6r(ysk, out ssk, out difference);
            //现金支付
            js.xjzf = ssk;
            //现金误差
            js.xjwc = difference;
            //总金额:自理费用+自负费用+分类自负费用+记帐费用
            js.zje = js.zlfy + js.zffy + js.flzffy + js.jzfy;
            //获取结算大类对象
            jsdl = getJsdl(js, returnValue);

            return js;
        }


        /// <summary>
        /// 计算收费项目减免金额
        /// 病人状态：1；--有效
        /// 变更标志：0；--未变更
        /// </summary>
        /// <param name="parmBrxz">病人性质</param>
        /// <param name="parmDl">大类</param>
        /// <param name="parmSfxm">收费项目</param>
        /// <param name="parmJe">金额</param>
        /// <param name="outJmbl">减免比例</param>
        /// <param name="outJmje">减免金额</param>
        /// <returns>count:减免后金额</returns>
        public decimal Calcjm(string parmBrxz, string parmDl, string parmSfxm, string parmJe, out decimal outJmbl,
            out decimal outJmje)
        {
            //减免比例，减免金额，减免后的金额
            decimal jmbl = 0, jmje = 0, count = 0;
            //  DataTable dt = new DataTable();
            // dt = BLLFactory<xt_brsfjm>.Instance.GetXT_brsfjm("brxz='" + parmBrxz + "' and sfxm='" + parmSfxm + "'");

            var dt = _RefundService.GetXT_brsfjm(parmBrxz);
            if (dt.Count > 0)
            {
                jmbl = dt[0].jmbl.ToDecimal();
                if (jmbl < 0) //定额自负
                {
                    count = parmJe.ToDecimal() + jmbl;
                    jmje = Math.Abs(jmbl);
                }
                else
                {
                    jmje = parmJe.ToDecimal() * jmbl;
                    count = parmJe.ToDecimal() - jmje;
                }
            }
            else
            {
                dt = _RefundService.GetXT_brsfjm("brxz='" + parmBrxz + "' and dl='" + parmDl + "' and sfxm=''");
                if (dt.Count > 0)
                {
                    jmbl = dt[0].jmbl.ToDecimal();
                    if (jmbl < 0) //定额自负
                    {
                        count = parmJe.ToDecimal() + jmbl;
                        jmje = Math.Abs(jmbl);
                    }
                    else
                    {
                        jmje = parmJe.ToDecimal() * jmbl;
                        count = parmJe.ToDecimal() - jmje;
                    }
                }
            }
            outJmbl = jmbl;
            outJmje = jmje;
            return count;
        }

        /// <summary>
        /// 多个挂号信息一起结算，不同病人性质和门急诊标志不能一起结算
        /// </summary>
        /// <param name="ghList"></param>
        public void checkGhjs(List<OutpatientRegistEntity> ghList)
        {
            int brxzTotal = 0;
            int mjzTotal = 0;

            string brxzTemp = ghList[0].brxz;
            string mjzTemp = ghList[0].mjzbz;
            foreach (var gh in ghList)
            {
                if (gh.brxz == brxzTemp)
                    brxzTotal++;

                if (gh.mjzbz == mjzTemp)
                    mjzTotal++;
            }

            if (brxzTotal != ghList.Count)
                throw new FailedCodeException("JS_ERROR！");

            if (mjzTotal != ghList.Count)
                throw new FailedCodeException("JS_ERROR_2");
        }

        /// <summary>
        /// 根据收费项目和病人收费算法，得到计算结果集
        /// </summary>
        /// <param name="jsEntity"></param>
        /// <param name="brxz"></param>
        /// <param name="returnValue"></param>
        public void getBrsfResultNB(JS_ENTITY jsEntity, string brxz, out Brsfsf_ReturnValue_YBANDFYB returnValue)
        {
            //需要结算的项目列表
            List<jsxm_entity> xmList = jsEntity.jsxmList;
            returnValue = new Brsfsf_ReturnValue_YBANDFYB();

            //上传医保和不上传医保项目
            List<jsxm_entity> xmListYB;
            List<jsxm_entity> xmListFYB;

            //计算收费项目费用
            //List<SFXM_FYMX> sfxmFYBDict;
            //List<SFXM_FYMX> sfxmYBDict;

            //病人算法返回计算的结果集
            Brsfsf_ReturnValue returnValueYB;
            Brsfsf_ReturnValue returnValueFYB;

            //获取要上传医保和不上传医保的大类
            getYBANDFYB(xmList, out xmListYB, out xmListFYB);

            //上传和不上传医保收费项目合计
            getSfxmHj(xmListFYB);
            getSfxmHj(xmListYB);

            //根据收费项目合计和病人收费算法计算
            //getBrsfResultNB(sfxmFYBDict, out returnValueFYB);
            //getBrsfResultNB(sfxmYBDict, out returnValueYB);
            getBrsfResult(xmListFYB, brxz, out returnValueFYB);
            getBrsfResult(xmListYB, brxz, out returnValueYB);

            returnValue.YB = returnValueYB;
            returnValue.FYB = returnValueFYB;
            //暂不区分医保和非医保
            //jsEntity.jsxmList.Clear();
            //jsEntity.jsxmList.AddRange(xmListYB);
            //jsEntity.jsxmList.AddRange(xmListFYB);
        }

        /// <summary>
        /// 是否造口袋
        /// </summary>
        /// <returns></returns>
        public bool isZkd(JS_ENTITY jsEntity)
        {
            int zkdTotal = 0;
            if (jsEntity == null)
                return false;

            var pz = _sysConfigRepo.GetByCode(Constants.xtmzpz.SFXM_ZKD, OperatorProvider.GetCurrent().OrganizeId);
            if (pz == null || string.IsNullOrEmpty(pz.Value))
                return false;

            List<string> values = new List<string>(pz.Value.Split(','));
            foreach (jsxm_entity xm in jsEntity.jsxmList)
            {
                bool temp = values.Exists(t => t.ToString() == xm.sfxm && xm.mxnm != 0);
                if (temp)
                {
                    zkdTotal++;
                }
            }

            if (zkdTotal == 0)
                return false;
            else if (zkdTotal == jsEntity.jsxmList.Count)
                return true;
            else
                return true;

            // throw new FailedCodeException("造口袋项目与非造口袋项目不能一起结算！");
        }

        /// <summary>
        /// 获取要上传医保和不上传医保的大类
        /// </summary>
        /// <param name="xmList"></param>
        /// <param name="xmListYB"></param>
        /// <param name="xmListFYB"></param>
        private void getYBANDFYB(List<jsxm_entity> xmList, out List<jsxm_entity> xmListYB,
            out List<jsxm_entity> xmListFYB)
        {
            xmListYB = new List<jsxm_entity>();
            xmListFYB = new List<jsxm_entity>();

            //获取门诊配置：不上传医保大类

            var entity = _sysConfigRepo.GetByCode(Constants.xtmzpz.DL_BSCYB, OperatorProvider.GetCurrent().OrganizeId);
            if (entity == null)
            {
                xmListYB = xmList;
                return;
            }

            string[] struDl = entity.Value.TrimEnd(',').Split(',');
            Dictionary<string, string> dlDict = new Dictionary<string, string>();
            foreach (string dl in struDl)
            {
                if (dlDict.ContainsKey(dl))
                    continue;

                dlDict.Add(dl, dl);
            }

            //没有配置不上传医保
            if (struDl.Length == 0)
            {
                xmListYB = xmList;
                return;
            }

            //根据大类区分是否上传医保
            foreach (jsxm_entity xm in xmList)
            {
                if (dlDict.ContainsKey(xm.dl))
                {
                    xmListFYB.Add(xm);
                }
                else
                {
                    xmListYB.Add(xm);
                }
            }
        }

        /// <summary>
        /// 计算收费项目合计 
        /// </summary>
        /// <param name="ghxmList"></param>
        /// <returns>可记账金额,分类自负金额,自理金额,减免金额</returns>
        public List<CMSEntity> getSfxmHj(List<OutpatientRegistItemEntity> ghxmList)
        {
            //计算收费项目合计费用
            ////key - 大类；value - 可记账金额,分类自负金额,自理金额,减免
            List<CMSEntity> amountList = new List<CMSEntity>();
            Dictionary<string, CMSEntity> amountDict = new Dictionary<string, CMSEntity>();

            //根据收费项目计算
            foreach (OutpatientRegistItemEntity ghxm in ghxmList)
            {
                //大类
                string dl = ghxm.dl;
                //减免后金额
                decimal jmhje = (decimal)ghxm.je - ghxm.jmje;
                if (!amountDict.ContainsKey(dl))
                    amountDict.Add(dl, new CMSEntity());

                //大类
                amountDict[dl].Dl = ghxm.dl;
                //减免金额
                amountDict[dl].Jmje += ghxm.jmje;

                //自理金额
                if (ghxm.zfxz == ((int)Constants.zfxzEnum.ZF).ToString())
                {
                    amountDict[dl].Zlfy += jmhje;
                }
                else if (ghxm.zfxz == ((int)Constants.zfxzEnum.KB).ToString())
                {
                    //可记账金额
                    amountDict[dl].Kbje += jmhje;
                }
                else
                {
                    //可报：费用×（1－自负比例）
                    //分类自负：费用×自负比例
                    //当自负比例为负数时，表示定额自负
                    if (ghxm.zfbl >= 0)
                    {
                        //可记账金额
                        amountDict[dl].Kbje += (jmhje * (1 - (decimal)ghxm.zfbl));
                        //分类自负金额
                        amountDict[dl].Flzf += (jmhje * (decimal)ghxm.zfbl);
                    }
                    else
                    {
                        //if (ghxm.jmbl < 0)
                        //{
                        //    throw new FailedCodeException("减免比例和自负比例只能有一个是定额，请重新配置！");
                        //}

                        //分类自负金额: 定额 * （1-减免比例）* 数量
                        amountDict[dl].Flzf += -((decimal)ghxm.zfbl) * (1 - ghxm.jmbl) * ghxm.sl;
                        //可记账金额
                        amountDict[dl].Kbje += jmhje - amountDict[dl].Flzf;
                    }
                }
            }

            foreach (CMSEntity entity in amountDict.Values)
            {
                amountList.Add(entity);
            }

            return amountList;
        }

        /// <summary>
        /// 计算收费项目不做合计
        /// </summary>
        /// <param name="xmList"></param>
        /// <returns>可记账金额,分类自负金额,自理金额,减免金额</returns>
        public void getSfxmHj(List<jsxm_entity> xmList)
        {
            //xt_brsfsf.mzSfActiveList.Clear();
            ////获取所有算法
            //List<tbl_xt_brsfsf> sfAllList = xt_brsfsf.mzSfActiveList;

            //根据收费项目计算
            foreach (jsxm_entity xm in xmList)
            {
                //减免后金额
                decimal jmhje = (xm.dj + xm.fwfdj) * xm.sl - xm.jmje;

                //自理金额
                if (xm.zfxz == ((int)Constants.zfxzEnum.ZF).ToString())
                {
                    xm.zlfy = jmhje;
                }
                else if (xm.zfxz == ((int)Constants.zfxzEnum.KB).ToString())
                {
                    //可记账金额
                    xm.kbje = jmhje;
                }
                else
                {
                    //可报：费用×（1－自负比例）
                    //分类自负：费用×自负比例
                    //当自负比例为负数时，表示定额自负
                    if (xm.zfbl >= 0)
                    {
                        //可记账金额
                        xm.kbje = (jmhje * (1 - xm.zfbl));
                        //分类自负金额
                        xm.flzf = (jmhje * xm.zfbl);
                    }
                    else
                    {
                        //if (xm.jmbl < 0)
                        //{
                        //    throw new FailedCodeException("减免比例和自负比例只能有一个是定额，请重新配置！");
                        //}

                        //分类自负金额: 定额 * （1-减免比例）* 数量
                        xm.flzf = -(xm.zfbl) * (1 - xm.jmbl) * xm.sl;
                        //可记账金额
                        xm.kbje = jmhje - xm.flzf;
                    }
                }
            }
        }

        /// <summary>
        /// 根据收费项目合计和病人收费算法计算
        /// </summary>
        /// <param name="amountList"></param>
        /// <param name="brxz"></param>
        public void getBrsfResult(List<jsxm_entity> amountList, string brxz, out Brsfsf_ReturnValue returnValue)
        {
            returnValue = new Brsfsf_ReturnValue();
            returnValue.dlFymxList = new List<Brsfsf_Dl_Fymx>();

            //获取所有算法
            List<SysPatientChargeAlgorithmEntity> sfAllList = _RefundService.getMzActive();
            //根据收费算法计算
            foreach (jsxm_entity entity in amountList)
            {
                Brsfsf_Dl_Fymx fymx = new Brsfsf_Dl_Fymx();
                //大类
                string dl = entity.dl;
                fymx.dl = dl;
                fymx.sfxm = entity.sfxm;
                fymx.mxnm = entity.mxnm;
                //fymx.mxbm = entity.mxbm;
                fymx.cf_mxnm = entity.cf_mxnm;
                fymx.czh = entity.czh;

                //减免费用
                fymx.jmje = entity.jmje;

                ////根据病人性质和大类获取病人收费算法
                //List<tbl_xt_brsfsf> sfList = BF<xt_brsfsf>.Instance.getBrxzAndDl(brxz, dl);
                List<SysPatientChargeAlgorithmEntity> sfList = null;
                //项目算法优先级更高
                if (entity.mxnm != 0)
                {
                    sfList = sfAllList.FindAll(t => t.brxz == brxz && t.sfxm == entity.sfxm);
                }
                //没有项目的算法，则考虑大类
                if (sfList == null || sfList.Count == 0)
                {
                    sfList = sfAllList.FindAll(
                        t => t.brxz == brxz && t.sfxm == "" && (t.dl == dl || t.dl.Trim() == "*"));
                }

                #region 没有病人收费算法

                //如果没有病人收费算法
                if (sfList == null || sfList.Count == 0)
                {
                    //没有算法 费用合计 = 可记账＋分类自负＋自理
                    fymx.total = entity.kbje + entity.flzf + entity.zlfy;
                    //分类自负
                    fymx.flzf = entity.flzf;
                    //记账费用
                    fymx.jzfy = entity.kbje;
                    //自理费用
                    fymx.zl = entity.zlfy;
                    //现金
                    fymx.xj = entity.flzf + entity.zlfy;

                    entity.jyje = fymx.jzfy;
                    entity.jyfwje = fymx.jzfy + fymx.flzf;

                    returnValue.dlFymxList.Add(fymx);
                    continue;
                }

                #endregion

                #region 费用合计

                if (sfList[0].fyfw == Constants.fyfw.fyfw0)
                {
                    //可记帐 : 交易费用总额 = 医保结算范围费用总额 - 分类自负；现金：分类自负+自理+自负
                    //现金
                    fymx.xj = entity.flzf + entity.zlfy;
                    //记账费用
                    fymx.jzfy = entity.kbje;
                    //分类自负
                    fymx.flzf = entity.flzf;
                    //自理费用
                    fymx.zl = entity.zlfy;
                }
                else if (sfList[0].fyfw == Constants.fyfw.fyfw1)
                {
                    //可记帐 + 分类自负：交易费用总额 = 医保结算范围费用总额；现金：自理+自负
                    //现金
                    fymx.xj = entity.zlfy;
                    //记账费用
                    fymx.jzfy = entity.kbje + entity.flzf;
                    //分类自负
                    fymx.flzf = 0m;
                    //自理费用
                    fymx.zl = entity.zlfy;
                }
                else if (sfList[0].fyfw == Constants.fyfw.fyfw2)
                {
                    //可记帐 + 分类自负 + 自理：交易费用总额 = 医保结算范围费用总额；现金：自负
                    //现金
                    fymx.xj = 0m;
                    //记账费用
                    fymx.jzfy = entity.kbje + entity.flzf + entity.zlfy;
                    //分类自负
                    fymx.flzf = 0m;
                    //自理费用
                    fymx.zl = 0m;
                }
                else if (sfList[0].fyfw == Constants.fyfw.fyfw3)
                {
                    //可记帐 + 分类自负 + 自理 + 绝对自费：交易费用总额 = 医保结算范围费用总额；现金：自负
                    //现金
                    fymx.xj = 0m;
                    //记账费用
                    fymx.jzfy = entity.kbje + entity.flzf + entity.zlfy;
                    //分类自负
                    fymx.flzf = 0m;
                    //自理费用
                    fymx.zl = 0m;
                }

                #endregion

                #region 费用计算

                //计算金额
                decimal jsAmount = fymx.jzfy;
                //根据费用范围需要计算的金额
                decimal jsedAmount = 0m;
                decimal sfzf = 0m;
                //如果有费用上线，则根据算法级别计算(实际只需计算自理费用) 
                foreach (SysPatientChargeAlgorithmEntity sfEntity in sfList)
                {
                    //如果同一批算法的自负性质和费用范围不一致则直接报错
                    //if (sfEntity.zfxz != sfList[0].zfxz)
                    //    throw new FailedCodeException("当前病人性质和大类的自负性质必须相同，请重新配置！");
                    //if (sfEntity.fyfw != sfList[0].fyfw)
                    //    throw new FailedCodeException("当前病人性质和大类的费用范围必须相同，请重新配置！");

                    decimal fysx = sfEntity.fysx * entity.sl;

                    if (fysx > fymx.jzfy)
                    {
                        sfzf += calZfbl(jsAmount, sfEntity.zfbl, entity.sl);
                        jsedAmount += jsAmount;
                        jsAmount = fymx.jzfy - jsedAmount;
                        break;
                    }
                    else
                    {
                        if (fysx > jsAmount)
                        {
                            jsedAmount += fysx - jsedAmount;
                            sfzf += calZfbl(jsAmount, sfEntity.zfbl, entity.sl);
                        }
                        else
                        {
                            if (fysx == 0)
                            {
                                sfzf += calZfbl(jsAmount, sfEntity.zfbl, entity.sl);
                                jsedAmount = jsAmount;
                                break;
                            }
                            else
                            {
                                jsedAmount += fysx;
                                sfzf += calZfbl(fysx, sfEntity.zfbl, entity.sl);
                            }
                        }
                        jsAmount = fymx.jzfy - jsedAmount;
                    }
                }
                if (jsedAmount != fymx.jzfy)
                {
                    sfzf += fymx.jzfy - jsedAmount;
                }

                #endregion

                if (sfList[0].zfxz == ((int)Constants.sfZfxzEnum.ZL).ToString())
                {
                    //自负性质为自理
                    fymx.sfzl += sfzf;
                }
                else
                {
                    fymx.sfzf += sfzf;
                }
                if (sfList[0].fyfw == Constants.fyfw.fyfw3)
                {
                    //如果是绝对自费,记账金额为自理
                    fymx.zl += fymx.jzfy - sfzf;
                    fymx.jzfy = 0m;
                    fymx.xj += fymx.zl;
                }
                else
                {
                    //可记账费用
                    fymx.jzfy = fymx.jzfy - sfzf;
                }
                //现金
                fymx.xj += sfzf;
                //有算法 费用合计 = 现金
                fymx.total = fymx.xj;

                entity.jyje = fymx.jzfy;
                entity.jyfwje = fymx.jzfy + fymx.flzf;

                returnValue.dlFymxList.Add(fymx);
            }
        }

        /// <summary>
        /// 根据自负比例计算自负金额
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="zfbl"></param>
        /// <returns></returns>
        private decimal calZfbl(decimal amount, decimal zfbl, decimal sl)
        {
            if (zfbl >= 0)
            {
                return amount * zfbl;
            }
            else
            {
                if (amount > 0)
                    return -zfbl * sl;
                else
                    return 0;
            }
        }

        /// <summary>
        /// 获取门结算大类
        /// </summary>
        /// <param name="js"></param>
        /// <param name="returnValue"></param>
        /// <returns></returns>
        public List<OutpatientSettlementCategoryEntity> getJsdl(OutpatientSettlementEntity js,
            Brsfsf_ReturnValue_YBANDFYB returnValue)
        {

            //医保费用
            //foreach (Brsfsf_Dl_Fymx yb in returnValue.YB.dlFymxList)
            //{
            //    OutpatientSettlementCategoryEntity jsdl = jsdlList.Find(t => t.dl == yb.dl);
            //    if (jsdl == null)
            //    {
            //        jsdl = new OutpatientSettlementCategoryEntity();
            //        jsdlList.Add(jsdl);
            //    }

            //    jsdl.jsnm = js.jsnm;
            //    jsdl.dl = yb.dl;//大类
            //    jsdl.flzffy += yb.flzf;//分类自负费用
            //    jsdl.zlfy += yb.zl + yb.sfzl;//自理费用
            //    jsdl.jmfy += yb.jmje;//减免金额
            //    if (returnValue.ybjsfwze == 0)
            //        if (0 == 0)
            //        {
            //            jsdl.zffy += 0m;
            //            jsdl.kbfy += 0m;
            //        }
            //        else
            //        {
            //            jsdl.zffy += ((yb.jzfy + yb.flzf) / returnValue.ybjsfwze) * js.zffy;//分类金额/分类金额合计*自负金额
            //            jsdl.kbfy += ((yb.jzfy + yb.flzf) / returnValue.ybjsfwze) * js.jzfy;//分类金额/分类金额合计*自负金额
            //        }
            //    jsdl.zje += jsdl.flzffy + jsdl.zlfy + jsdl.jmfy + jsdl.zffy + jsdl.kbfy;
            //}
            //非医保费用
            //foreach (Brsfsf_Dl_Fymx yb in returnValue.FYB.dlFymxList)
            //{
            //    OutpatientSettlementCategoryEntity jsdl = jsdlList.Find(t => t.dl == yb.dl);
            //    if (jsdl == null)
            //    {
            //        jsdl = new OutpatientSettlementCategoryEntity();
            //        jsdlList.Add(jsdl);
            //    }

            //    jsdl.jsnm = js.jsnm;
            //    jsdl.dl = yb.dl;//大类
            //    jsdl.flzffy += yb.flzf;//分类自负费用
            //    jsdl.zlfy += yb.zl + yb.sfzl;//自理费用
            //    jsdl.jmfy += yb.jmje;//减免金额

            //    jsdl.zffy += yb.sfzf;
            //    jsdl.kbfy += yb.jzfy;
            //    jsdl.zje += jsdl.flzffy + jsdl.zlfy + jsdl.jmfy + jsdl.zffy + jsdl.kbfy;
            //}

            foreach (Brsfsf_Dl_Fymx yb in returnValue.YB.dlFymxList)
            {
                OutpatientSettlementCategoryEntity jsdl = jsdlList.Find(t => t.dl == yb.dl);
                if (jsdl == null)
                {
                    jsdl = new OutpatientSettlementCategoryEntity();

                }
                jsdl.jsnm = js.jsnm;
                jsdl.dl = yb.dl; //大类
                jsdl.zje = yb.total;
                jsdl.kbfy = 0;
                jsdl.flzffy += yb.flzf; //分类自负费用
                jsdl.zlfy = yb.total; //自理费用
                jsdl.jmfy += yb.jmje; //减免金额
                jsdlList.Add(jsdl);
            }
            foreach (OutpatientSettlementCategoryEntity jsdl in jsdlList)
            {
                jsdl.flzffy = Ext.getsr(jsdl.flzffy);
                jsdl.zlfy = Ext.getsr(jsdl.zlfy);
                jsdl.jmfy = Ext.getsr(jsdl.jmfy);
                jsdl.zffy = Ext.getsr(jsdl.zffy);
                jsdl.kbfy = Ext.getsr(jsdl.kbfy);
                jsdl.zje = Ext.getsr(jsdl.zje);
            }

            return jsdlList;
        }

        /// <summary>
        /// 造口袋交易
        /// </summary>
        //public void ybmxxmjy(bool isGh, SysPatientBasicInfoEntity brjbxx, OutpatientRegistEntity gh, SysPatientNatureEntity brxz, JS_ENTITY jsEntity,
        //    out decimal ybzf, out decimal jzfy, out bool ybError, out string sqxh)
        //{
        //    sqxh = string.Empty;
        //    ybError = false;
        //    ybzf = 0m;
        //    jzfy = 0m;

        //    if (isGh)
        //    {
        //        throw new FailedCodeException("医保挂号没有明细项目实时交易！");
        //    }
        //    else
        //    {
        //        tbl_yb_deal_sssf sssf = ybmxxmsfjy(brjbxx, gh, brxz, jsEntity, out ybError);
        //        //根据医保返回的结果，得到结算数据
        //        //自负费用 = 自负段现金支付数+统筹段现金支付数+附加段现金支付数。
        //        ybzf = Convert.ToDecimal(sssf.zf_xjzf) +
        //            Convert.ToDecimal(sssf.tc_xjzf) +
        //            Convert.ToDecimal(sssf.fj_xjzf);
        //        //记帐费用 = 当年帐户支付数+历年帐户支付数+统筹支付数+附加支付数
        //        jzfy = Convert.ToDecimal(sssf.dn_zhzf) +
        //            Convert.ToDecimal(sssf.ln_zhzf) +
        //            Convert.ToDecimal(sssf.tc_zf) +
        //            Convert.ToDecimal(sssf.fj_zf);
        //        sqxh = sssf.sqxh;
        //    }
        //}

        private void loadData(OutpatientSettlementEntity mzJs, bool isReturnAll, int Jsnm)
        {

            if (IsReturnAll == true)
            {
                lblSsk = (mzJs.xjzf).ToString("0.00");
            }
            else
            {
                lblSsk = ((mzJs.xjzf) - (newMzJs.xjzf)).ToString("0.00");
            }
            lblYtk = lblSsk;
            jszffsExListForSK = new List<tbl_mz_jszffs_Ex>();
            jszffsExList = new List<tbl_mz_jszffs_Ex>();
            List<tbl_mz_jszffs_Ex> jszffsExListTemp = new List<tbl_mz_jszffs_Ex>();
            if (isReturnAll)
            {
                DataTable jszffsData = _RefundService.GetMzJszffsForJsnm(Jsnm);

                foreach (DataRow entiy in jszffsData.Rows)
                {
                    tbl_mz_jszffs_Ex tbl_jszffs = new tbl_mz_jszffs_Ex();
                    tbl_jszffs.jsnm = entiy["jsnm"].ToInt();
                    tbl_jszffs.xjzffs = entiy["xjzffs"].ToString();
                    tbl_jszffs.zfje = entiy["zfje"].ToDecimal();
                    tbl_jszffs.ssry = entiy["ssry"].ToString();
                    tbl_jszffs.ssrq = DateTime.Parse(entiy["ssrq"].ToString());
                    tbl_jszffs.zh = entiy["zh"].ToInt();
                    // tbl_jszffs.xjzffsmc = Tool.AnyToString(entiy["xjzffsmc"]);
                    jszffsExList.Add(tbl_jszffs);
                }
                //收款结算支付方式
                jszffsExListForSK = jszffsExList;
            }
            else
            {
                DataTable jszffsData = _RefundService.GetMzJszffsForJsnm(Jsnm);

                foreach (DataRow entiy in jszffsData.Rows)
                {
                    tbl_mz_jszffs_Ex tbl_jszffs = new tbl_mz_jszffs_Ex();
                    tbl_jszffs.jsnm = entiy["jsnm"].ToInt();
                    tbl_jszffs.xjzffs = entiy["xjzffs"].ToString();
                    tbl_jszffs.zfje = entiy["zfje"].ToDecimal();
                    tbl_jszffs.ssry = entiy["ssry"].ToString();
                    tbl_jszffs.ssrq = DateTime.Parse(entiy["ssrq"].ToString());
                    tbl_jszffs.zh = entiy["zh"].ToInt();
                    // tbl_jszffs.xjzffsmc = Tool.AnyToString(entiy["xjzffsmc"]);
                    jszffsExListForSK.Add(tbl_jszffs);
                }
                if (jszffsData.Rows.Count == 1)
                {
                    foreach (DataRow entiy in jszffsData.Rows)
                    {
                        tbl_mz_jszffs_Ex tbl_jszffs = new tbl_mz_jszffs_Ex();
                        tbl_jszffs.jsnm = entiy["jsnm"].ToInt();
                        tbl_jszffs.xjzffs = entiy["xjzffs"].ToString();
                        tbl_jszffs.zfje = lblYtk.ToDecimal();
                        tbl_jszffs.ssry = entiy["ssry"].ToString();
                        tbl_jszffs.ssrq = DateTime.Parse(entiy["ssrq"].ToString());
                        tbl_jszffs.zh = entiy["zh"].ToInt();
                        // tbl_jszffs.xjzffsmc = Tool.AnyToString(entiy["xjzffsmc"]);
                        jszffsExList.Add(tbl_jszffs);
                        break;
                    }
                }
            }


            ////计算实收总额  ----fangyi
            //if (jszffsExListForSK.Count > 1)
            //{
            //    jsSSK();
            //}

        }

        /// <summary>
        /// 确定全退
        /// </summary>
        /// <param name="kh">卡号</param>
        /// <param name="isGh">是否挂号</param>
        /// <param name="mzJs">门诊结算</param>
        /// <param name="resultList">明细</param>
        private bool fullBack(string kh, bool isGh, OutpatientSettlementEntity mzJs,
            List<OutpatientRegistNonAttendanceEntity> GhjsDt)
        {
            bool result = false;
            bool ybError = true;
            mzJs.jszt = 2;

            try
            {
                if (ybError)
                {

                    //自费重复退费导致插入多条重复全退记录
                    if (_RefundService.getTFForFPH(mzJs.jsnm, mzJs.fph) > 0)
                    {
                        return true;
                    }
                    List<SysPatientAccountRevenueAndExpenseEntity> szjlList =
                        new List<SysPatientAccountRevenueAndExpenseEntity>();

                    //退款成功，插门诊结算，门诊结算明细。改门诊结算的jsnm. 改结算明细内码。
                    List<OutpatientSettlementDetailEntity> mzJsmxList = getMzJsMx(mzJs.jslx, mzJs.jsnm);

                    //var mzJszffsList = _RefundService.GetJszffsByJsnm(mzJs.jsnm);
                    //if (this.posTxj == true)
                    //{
                    //    mzJszffsList = getJszffsEntity();
                    //}

                    ////新增系统病人帐户收支记录
                    //szjlList = getZhszjlList(mzJs, mzJszffsList, "T");

                    //List<OutpatientSettlementCategoryEntity> jsdl = _RefundService.GetJsdlByJsnm(mzJs.jsnm);
                    mzJs.cxjsnm = mzJs.jsnm; //旧结算是退新的撤销结算内码 
                    mzJs.ghnm = mzJs.ghnm;
                    jsnmEx = mzJs.jsnm;
                    result = _RefundService.mzJsFullBack(mzJs, mzJsmxList, GhjsDt);
                }

            }
            catch (Exception ex)
            {
                throw new FailedException(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 确定部分退
        /// </summary>
        /// <param name="newMzJs"></param>
        /// <param name="accountEntity"></param>
        /// <param name="jszffsArray"></param>
        private void backPart(OutpatientSettlementEntity newMzJs, JS_ENTITY accountEntity,
            List<OutpatientSettlementPaymentModelEntity> jszffsArray, string sqxh,
            List<OutpatientSettlementCategoryEntity> jsdlList)
        {
            bool ybError = false;
            string sqlUpdate = string.Empty;
            List<string> sqls = new List<string>();
            //如果总金额为0，不打印发票
            if (newMzJs.zje != 0m)
            {
                if (fpsqlList != null && fpsqlList.Count > 0)
                {
                    sqls.AddRange(fpsqlList);
                }
                //发票号
                newMzJs.fph = lblFPHNew.Trim();
            }
            else
            {
                newMzJs.fph = string.Empty;
            }

            try
            {
                List<OutpatientSettlementDetailEntity> listMzJsMx =
                    getMzJsMx(newMzJs.jsnm, newMzJs.jslx, accountEntity.jsxmList);

                try
                {
                    //if (newMzJs.jylx == "10")
                    //{

                    //    mzjsReq.fph = accountEntity.fph;
                    //    //NB_INTERFACE_MZJS_REP rep = BF<mz_js>.Instance.nbDealComfirm(mzjsReq, newMzJs.jsnm, newMzJs.jylx, out ybError, out sqlUpdate);

                    //    //if (mzssRep == null || mzjsReq == null)
                    //    //{
                    //    //    ybError = true;
                    //    //    throw new FailedCodeException("农保交易失败！");
                    //    //}

                    //    //如果试算和结算金额不等，则做撤销试算和撤销结算，重新加载页面
                    //    if (Convert.ToDecimal(mzssRep.bcze) != Convert.ToDecimal(rep.bcze) ||
                    //        Convert.ToDecimal(mzssRep.kbze) != Convert.ToDecimal(rep.kbze) ||
                    //        Convert.ToDecimal(mzssRep.zfze) != Convert.ToDecimal(rep.zfze))
                    //    {
                    //        if (DialogHelper.Ask_OkCancel("门诊试算结果与门诊结算结果不一致，是否重新结算？") == System.Windows.Forms.DialogResult.OK)
                    //        {
                    //            try
                    //            {
                    //                //撤销结算
                    //                BF<mz_js>.Instance.nbmzqxjs(mzjsReq.kbh, rep.bclsh, mzjsReq.yydm, mzjsReq.czydm, mzjsReq.czymc, newMzJs.jsnm, out ybError);
                    //            }
                    //            catch
                    //            {
                    //                DialogHelper.DlgError("撤销结算失败！将通过对账方式平帐！");
                    //            }
                    //            //isAlreadyComfirmYb = false;
                    //            //return;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    //中心流水号为空，不去请求医保确认交易
                    //    if (string.IsNullOrEmpty(sqxh) == false)
                    //    {
                    //        //医保交易确认请求。
                    //        BF<mz_js>.Instance.ybDealComfirm(accountEntity.isGh, sqxh, accountEntity.ghnm, newMzJs.jsnm, newMzJs.jylx, out ybError, out sqlUpdate);
                    //    }
                    //}

                }
                catch (Exception ex)
                {
                    ////如果医保交易失败，则转自费
                    //if (!ybError)
                    //    throw;

                    ////家床不转自费，继续重试交易
                    //if (accountEntity.jch > 0)
                    //{
                    //    jxyb = true;
                    //    throw;
                    //}

                    //FrmBrxzEdit zfForm = new FrmBrxzEdit();
                    //zfForm.currBrxzEntity = BF<xt_brxz>.Instance.GetEntityByID(accountEntity.brxzbh.ToString()); ;
                    //zfForm.brnm = accountEntity.brnm;
                    //zfForm.msg = ex.Message;
                    //if (zfForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    //{
                    //    jxyb = zfForm.jxyb;//继续医保交易
                    //    if (!jxyb)
                    //    {
                    //        //转自费
                    //        accountEntity.isZh = true;
                    //        accountEntity.brxzbh = zfForm.currBrxzEntity.brxzbh;
                    //        //重新初始化
                    //        FrmJS_Load(null, null);
                    //    }

                    //}
                    //else
                    //{
                    //    jxyb = false;
                    //    throw new FailedCodeException("医保交易失败!");
                    //}
                }
                //if (string.IsNullOrEmpty(sqlUpdate) && jxyb == true)
                //{
                //    try
                //    {
                //        //中心流水号为空，不去请求医保确认交易
                //        if (string.IsNullOrEmpty(sqxh) == false)
                //        {
                //            //医保交易确认请求。
                //            BF<mz_js>.Instance.ybDealComfirm(accountEntity.isGh, sqxh, accountEntity.ghnm, newMzJs.jsnm, newMzJs.jylx, out ybError, out sqlUpdate);
                //        }
                //    }
                //    catch
                //    {
                //        if (string.IsNullOrEmpty(sqlUpdate))
                //        {
                //            throw new FailedCodeException("医保网络问题，请重新拉卡退费！");
                //        }
                //    }
                //}
                //医保SQL
                if (!string.IsNullOrEmpty(sqlUpdate))
                {
                    sqls.Add(sqlUpdate);
                }

                try
                {
                    newMzJs.cxjsnm = Jsnm; //旧结算是退新的撤销结算内码 

                    //获取账号信息
                    //tbl_xt_brzh brzh = BF<xt_brzh>.Instance.getBrzhByZh(mzJs.zh);
                    //系统病人帐户收支记录
                    List<SysPatientAccountRevenueAndExpenseEntity> szjlList = getZhszjlList(newMzJs, jszffsArray, "S");
                    ;
                    //加床记账半退再收不插入收支记录
                    //if (newMzJs.jslx == "1" && newMzJs.jylx == "14" && newMzJs.jch > 0)
                    //{
                    //    szjlList = new List<tbl_xt_brzhszjl>();
                    //}

                    var result = _RefundService.InsertRemainFromReturns(newMzJs, listMzJsMx, jszffsArray, jsdlList);
                    if (false == result)
                        throw new FailedCodeException("JS_ERROR_FAIL");
                }
                catch
                {
                    throw;
                }
            }
            catch
            {
                throw;
            }
        }



        ////退款后再次请求医保计算现金支付
        private void YBTKloadData()
        {
            lblSrce = newMzJs.xjwc.ToString("0.00");
            if (IsReturnAll == true)
            {
                //lblSsk.Text = mzJs.zje.ToString("0.00");
                lblSsk = (mzJs.xjzf).ToString("0.00");
            }
            else
            {
                //lblSsk.Text = (mzJs.zje - newMzJs.zje).ToString("0.00");
                lblSsk = ((mzJs.xjzf) - (newMzJs.xjzf)).ToString("0.00");
            }
            lblYtk = lblSsk;
            jszffsExList = new List<tbl_mz_jszffs_Ex>();

            if (isReturnAll == false)
            {
                List<OutpatientSettlementPaymentModelEntity> jszffsData = _RefundService.GetJszffsByJsnm(Jsnm);
                if (jszffsData.Count == 1)
                {
                    foreach (var entiy in jszffsData)
                    {
                        tbl_mz_jszffs_Ex tbl_jszffs = new tbl_mz_jszffs_Ex();
                        tbl_jszffs.jsnm = entiy.jsnm.ToInt();
                        tbl_jszffs.xjzffs = entiy.xjzffs;
                        tbl_jszffs.zfje = lblYtk.ToDecimal();
                        tbl_jszffs.ssry = entiy.ssry.ToString();
                        tbl_jszffs.ssrq = DateTime.Parse(entiy.ssrq.ToString());
                        tbl_jszffs.zh = entiy.zh.ToInt();
                        // tbl_jszffs.xjzffsmc = Tool.AnyToString(entiy.xjzffsmc);
                        jszffsExList.Add(tbl_jszffs);
                        break;
                    }
                }

            }
            //计算实收总额  ----fangyi
            if (jszffsExListForSK.Count > 1)
            {
                // jsSSK();
            }
        }

        /// <summary>
        /// 获取处方状态
        /// </summary>
        private bool getPrescriptionStatus()
        {
            string statusDescription = string.Empty;
            string reqData = string.Empty;
            InterfaceRepParaBase<GET_CF_STATUS> interRepPara = null;
            bool jsgb = false;
            try
            {
                return true;
                //获取实际要退费的信息
                List<GET_CF_STATUS> tyCfList = getTyInfo();
                if (tyCfList == null || tyCfList.Count == 0)
                    return true;

                //Y（多个处方以“，”分隔）
                tyCfList.ForEach(t => reqData += t.prescriptionNo.ToString() + ",");
                //tyCfList.ForEach(delegate(GET_CF_STATUS item)
                //{
                //    reqData += item.prescriptionNo.ToString() + ",";
                //});
                reqData = reqData.TrimEnd(',');
                //reqData = JsonConvert.SerializeObject(reqData);       //去掉加\"的参数

                //请求接口
                //   WebPostAPI.API.post<GET_CF_STATUS>(Constant.InterfaceConstant.GET_CF_STATUS, reqData, out interRepPara, out statusDescription, out jsgb);
                jsgb = true;
                //接口关闭，直接返回
                if (jsgb)
                {
                    return true;
                }
                //系统异常
                if (interRepPara == null)
                {
                    //       DialogHelper.DlgError("接口访问异常：" + statusDescription);
                    //return false;
                    return true;
                }
                else
                {
                    //如果有业务异常，则提示
                    if (interRepPara.failureMessage != null && interRepPara.failureMessage.Count > 0)
                    {
                        //       DialogHelper.DlgError(interRepPara.failureMessageShow);
                        //return false;
                        return true;
                    }
                    else
                    {
                        //判断处方状态
                        string tempResult = JsonConvert.SerializeObject(interRepPara.result);
                        // return isAllowTy(tyCfList, tempResult);
                    }
                }
            }
            catch (Exception ex)
            {
                //DialogHelper.DlgError("获取处方状态接口处理失败：" + ex.Message);
                //return false;
                return true;
            }
        }

        /// <summary>
        /// 获取实际退药信息
        /// </summary>
        /// <returns></returns>
        private List<GET_CF_STATUS> getTyInfo()
        {
            //全退的处方
            DataTable mzCfMxDt = _RefundService.GetMz_cfmxRecords(jsnm);
            List<GET_CF_STATUS> tyCfList = new List<GET_CF_STATUS>();
            foreach (DataRow dr in mzCfMxDt.Rows)
            {
                //是否已包含处方
                GET_CF_STATUS cf = tyCfList.Find(t => t.prescriptionNo == dr["cfh"].ToString());
                if (cf == null)
                {
                    cf = new GET_CF_STATUS();
                    cf.returnDrugInfo = new List<returnDrugInfo>();
                    //cf.prescriptionNo = dr["pt_cfnm"].ToString() == "0" ? Tools.AnyToString(dr["cfh"]) : dr["pt_cfnm"].ToString();
                    cf.prescriptionNo = dr["cfh"].ToString();
                    tyCfList.Add(cf);
                }
                //处方中是否已包含药品
                returnDrugInfo yp = cf.returnDrugInfo.Find(t => t.drugCode == dr["yp"].ToString());
                if (yp == null)
                {
                    yp = new returnDrugInfo();
                    yp.drugCode = dr["yp"].ToString();
                    cf.returnDrugInfo.Add(yp);
                }

                if (AccountEntity.jsxmList == null)
                    AccountEntity.jsxmList = new List<jsxm_entity>();

                jsxm_entity jsxm = AccountEntity.jsxmList.Find(t => t.cfh == dr["cfh"].ToString()
                                                                    //&& t.mxbm == dr["yp"].ToString()
                                                                    && t.czh == dr["czh"].ToString());

                //如果不收回则算全退
                decimal returnCount = 0;
                if (jsxm == null)
                    returnCount = 0;
                else
                    returnCount = jsxm.sl;

                //药品包装代码为 3，则数量为三级数量，药品包装代码为 4，则数量为四级单位
                if (dr["ypbzdm"].ToString() == "3")
                    yp.thirdLevelPkgQty = decimal.ToInt32(Convert.ToDecimal(dr["sl"].ToString()) - returnCount);
                else
                    yp.fourthLevelPkgQty = decimal.ToInt32(Convert.ToDecimal(dr["sl"].ToString()) - returnCount);
            }

            return GetInfoForSelect(tyCfList);
        }

        /// <summary>
        /// 结算数据验证
        /// </summary>
        /// <returns></returns>
        private bool checkJs()
        {
            //应收款 >= 实收款 - 舍入差额？
            decimal Ytk = Convert.ToDecimal(lblYtk);
            decimal ssk = Convert.ToDecimal(lblSsk);
            decimal srce = Convert.ToDecimal(lblSrce);

            //if ((Ytk-ssk) > Ytk)
            if (ssk > Ytk)
            {
                throw new FailedException("退款必须小于等于收款。");
                // txtXj.Focus();
                //  return false;
            }
            if (ssk != Ytk)
            {
                throw new FailedException("实际退款金额不对，请确认。");
                // txtXj.Focus();
                //  return false;
            }

            return true;
        }

        /// <summary>
        /// 门诊结算支付方式记录————退款
        /// </summary>
        private List<OutpatientSettlementPaymentModelEntity> getJszffsEntity()
        {
            List<OutpatientSettlementPaymentModelEntity> jszffsList =
                new List<OutpatientSettlementPaymentModelEntity>();
            foreach (tbl_mz_jszffs_Ex entity in jszffsExList)
            {
                OutpatientSettlementPaymentModelEntity jszffs = (OutpatientSettlementPaymentModelEntity)entity;
                jszffsList.Add(jszffs);
            }

            return jszffsList;
        }

        private void btnOk_Click(OutpatientSettlementEntity newMzJs, JS_ENTITY accountEntity,
            OutpatientSettlementEntity mzJs, List<OutpatientSettlementCategoryEntity> jsdlList)
        {
            //自费转医保不判断旺云接口
            if (IsChangeBrxz == false)
            {
                if (!getPrescriptionStatus())
                    return;
            }

            if (!checkJs())
                return;
            //结算支付方式记录（退款方式）
            jszffsArray = getJszffsEntity();

            //收款方式-去掉退款方式就是半退的结算方式
            var jszffsArrayForSK = getJszffsEntity(jszffsArray);
            if (IsReturnAll == false && accountEntity.jsxmList.Count > 0 && accountEntity.isGh == false)
            {
                bool flag = true;

                //加床记账半退打凭证(清单)
                if (newMzJs.jslx == "1" && newMzJs.jylx == "14" && newMzJs.jch != 0)
                {
                    flag = false;
                }
                if (flag == true)
                {
                    if (string.IsNullOrEmpty(lblFPHNew))
                    {
                        throw new FailedCodeException("GET_FPH_FAIL");
                        // return;
                    }
                    //重新验证票号
                    _RefundService.CheckFPH(lblFPHNew);
                }
                else
                {
                    //凭证号
                    if (string.IsNullOrEmpty(lblFPHNew))
                    {
                        throw new FailedCodeException("GET_SJH_FAIL");
                        //  return;
                    }
                    //重新验收据号
                    checkSJH(lblFPHNew);
                }
            }

            try
            {
                //全部退费医保交易结算
                mzJs.CreatorCode = userModel.UserCode;
                mzJs.CreateTime = DateTime.Now;

                bool result = isAlreadyComfirmTF;
                //if (isAlreadyComfirmTF == false && this.posTxj == true&& false == IsReturnAll)
                //{
                //    result = fullBack(Kh, accountEntity.isGh, mzJs);
                //}
                //if (false == IsReturnAll && result)
                //if (false == IsReturnAll)
                //{
                //    string errCode = "";

                #region 调pos机接口操作

                //半退pos退款
                //if (jszffsArrayForSK.Find(p => p.xjzffs == "1") != null && jszffsArrayForSK.Count == 1)
                //{
                //    if (this.isAlreadyPosCX == true)
                //    {
                //        errCode = PosSfInterface(Kh, lblFPHNew, (mzJs.xjzf - Tool.AnyToDecimal(lblSsk)), newMzJs.jsnm);
                //        //撤销交易后收费
                //        if (errCode != "00")
                //        {
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        errCode = PosTfInterface(Kh, mzJs.fph, Tool.AnyToDecimal(lblSsk));
                //        if (errCode != "00")
                //        {
                //            //this.Close();
                //            return;
                //        }
                //    }
                //}
                //多退款方式,半退pos
                //if (jszffsArrayForSK.Count > 1)
                //{
                //    tbl_mz_jszffs mz_jszf = jszffsArrayForSK.Find(p => p.xjzffs == "1");
                //    if (mz_jszf != null)
                //    {
                //        if (PosJY(Kh, mzJs, Common.Tools.AnyToDecimal(mz_jszf.zfje), IsReturnAll) == false)
                //        {
                //            //this.Close();
                //            return;
                //        }

                //        if (this.isAlreadyPosCX == true)
                //        {
                //            if (PosSfInterface(Kh, lblFPHNew.Text, Common.Tools.AnyToDecimal(mz_jszf.zfje), newMzJs.jsnm) != "00")
                //            {
                //                //this.Close();
                //                return;
                //            }
                //        }
                //        else
                //        {
                //            if (PosTfInterface(Kh, mzJs.fph, Common.Tools.AnyToDecimal(mz_jszf.zfje)) != "00")
                //            {
                //                //this.Close();
                //                return;
                //            }
                //        }
                //    }

                //}

                #endregion

                //确定部分退，重新收部分
                //backPart(newMzJs, accountEntity, jszffsArray, sqxh);
                newMzJs.CreatorCode = userModel.UserCode;
                newMzJs.CreateTime = DateTime.Now;
                //医保半退收费
                backPart(newMzJs, accountEntity, jszffsArrayForSK, sqxh, jsdlList);

                //if (IsChangeBrxz == false)
                //{
                //    throw new FailedCodeException("退费成功！ 退款金额：" + lblSsk);
                //}
                //else
                //{
                //    throw new FailedCodeException("自费转医保成功！ 退款金额：" + lblSsk);
                //}

                //    if ((newMzJs.zje != 0 && newMzJs.jslx != "0") || (newMzJs.zje != 0 && newMzJs.jslx == "0" && IsChangeBrxz == true))     //结算类型不是挂号
                //    {
                //        bool flag = true;

                //        //1 家床记帐（包括家床记帐 ） jylx=5(家床收费 ),jylx=14(家床记帐)
                //        //if (newMzJs.jslx == "1" && newMzJs.jylx == "14" && newMzJs.jch > 0)
                //        //{
                //        //    flag = false;
                //        //}
                //        if (flag == true)
                //        {
                //            //打印发票
                //            //   printBill(newMzJs, accountEntity);
                //        }
                //        else
                //        {
                //            //获取账户余额
                //            //decimal jc_zhye = 0.00M;
                //            //tbl_xt_brzhszjl entitySz = new tbl_xt_brzhszjl();
                //            //entitySz = BLLFactory<xt_brzhszjl>.Instance.getLastestJlBybrnm(newMzJs.patid);
                //            //if (entitySz != null)
                //            //{
                //            //    jc_zhye = entitySz.zhye;
                //            //}
                //            ////历次记账+当前记账与账户余额比较
                //            ////历次记账的总数
                //            //decimal compareJe = 0m;
                //            //getJcbrjzxx(Tools.AnyToInt32(newMzJs.brnm), newMzJs.brxz, newMzJs.jch, out compareJe);
                //            ////打印清单
                //            //DataTable jc = BLLFactory<mz_jsmx>.Instance.getSFQDForJsnm(newMzJs.jsnm);
                //            ////PrintBill print = new PrintBill(jc, newMzJs.xjzf);
                //            //tbl_xt_brjbxx brjbxx = BF<xt_brjbxx>.Instance.GetEntityByID(newMzJs.brnm.ToString());
                //            //PrintBill print = new PrintBill(jc, newMzJs.xjzf, brjbxx.xm, jc_zhye - Convert.ToDecimal(newMzJs.flzffy) - newMzJs.xjzf - compareJe);
                //            //print.ReportPrint();

                //        }
                //    }
                //    // btnOk.Enabled = true;
                //    //  DialogResult = DialogResult.OK;
                //    //this.Close();
                //}
                //else
                //{
                //    //if (result)
                //    //{
                //    //    if (IsChangeBrxz == false)
                //    //    {
                //    //        throw new FailedCodeException("退费成功！ 退款金额：" + lblSsk);
                //    //    }
                //    //    else
                //    //    {
                //    //        throw new FailedCodeException("自费转医保成功！ 退款金额：" + lblSsk);
                //    //    }
                //    //}
                //}

                ////处方退费通知
                //returnMoneyOfPrescriptionNo();
            }
            catch
            {
                throw;
            }
            //DialogResult = DialogResult.OK;
            try
            {
                //支付方式为现金则启动钱箱
                //if (JszffsArray.Find(p => p.xjzffs == "0") != null)
                //{
                //    string setFileName = (new Common.ConfigFile.AppConfig()).AppConfigGet("INIFileName");   // ConfigurationManager.AppSettings["INIFileName"].ToString();
                //    string fileName = System.IO.Path.GetFullPath("../../") + "\\" + setFileName + ".ini";
                //    IniFile Ini = new IniFile(fileName);
                //    string ckpzStr = Ini.IniReadValue("本机参数", "QX_CKPZparm");
                //    if (!string.IsNullOrEmpty(ckpzStr))
                //    {
                //        SerialPort port = new SerialPort();
                //        port.PortName = ckpzStr;//设定串口
                //        port.Open();//打开串口
                //        port.Write("#$1b#$70#0#$3c#$ff");//往串口写入启动钱箱的指令
                //        port.Close();//释放串口
                //    }
                //    else { DialogHelper.DlgError("钱箱串口未配置!"); }
                //}
            }
            catch
            {
            }
            //   this.Close();
        }

        /// <summary>
        /// 门诊结算支付方式记录————退款剩下的收款
        /// </summary>
        private List<OutpatientSettlementPaymentModelEntity> getJszffsEntity(
            List<OutpatientSettlementPaymentModelEntity> jszffsListForTK)
        {
            List<OutpatientSettlementPaymentModelEntity> jszffsList =
                new List<OutpatientSettlementPaymentModelEntity>();
            List<OutpatientSettlementPaymentModelEntity> jszffsListFortemp =
                new List<OutpatientSettlementPaymentModelEntity>();
            jszffsListFortemp = jszffsListForTK;
            foreach (tbl_mz_jszffs_Ex entity in jszffsExListForSK)
            {
                OutpatientSettlementPaymentModelEntity jszffs = ChangeTbl_mz_jszffs_Ex(entity);
                //var jszffsForTk = jszffsListForTK.Find(p => p.zfbh == jszffs.zfbh && p.xjzffs == jszffs.xjzffs);
                var jszffsForTk = jszffsListForTK.Find(p => p.xjzffs == jszffs.xjzffs);
                if (jszffsForTk != null)
                {
                    jszffs.zfje = (jszffs.zfje - jszffsForTk.zfje);

                    jszffsListFortemp.Remove(jszffsForTk);
                }
                else
                {
                    if (jszffsExListForSK.Count == 1)
                    {
                        jszffs.zfje = (jszffs.zfje - this.lblSsk.ToDecimal());
                        jszffsListFortemp.Remove(jszffsForTk);
                    }
                    //else
                    //{
                    //    jszffsForTk = jszffs;
                    //    if (jszffsExListForSK.Count > 1 && jszffsForTk != null)
                    //    {
                    //        jszffsForTk.zfje = -(jszffsForTk.zfje);
                    //        jszffsListFortemp.Add(jszffsForTk);
                    //    }
                    //}
                }

                //修改操作员
                jszffs.ssry = userModel.UserCode;
                jszffs.ssrq = DateTime.Now;
                jszffsList.Add(jszffs);
            }
            //退款方式不是收款方式中任何一种
            foreach (var entity in jszffsListFortemp)
            {
                entity.zfje = -(entity.zfje);
                //修改操作员
                entity.ssry = userModel.UserCode;
                entity.ssrq = DateTime.Now;

                jszffsList.Add(entity);
            }
            return jszffsList;
        }

        public OutpatientSettlementPaymentModelEntity ChangeTbl_mz_jszffs_Ex(tbl_mz_jszffs_Ex ex)
        {
            OutpatientSettlementPaymentModelEntity zffs = new OutpatientSettlementPaymentModelEntity();
            zffs.jsnm = ex.jsnm;
            zffs.mzjszffsbh = ex.mzjszffsbh;
            zffs.ssrq = ex.ssrq;
            zffs.ssry = ex.ssry;
            zffs.xjzffs = ex.xjzffs;
            zffs.zfje = ex.zfje;
            zffs.zh = ex.zh;
            return zffs;
        }

        /// <summary>
        /// 门诊结算明细
        /// </summary>
        /// <param name="jsnm">结算内码</param>
        /// <param name="jslx">结算类型</param>
        /// <param name="xmList">结算项目明细</param>
        /// <returns></returns>
        private List<OutpatientSettlementDetailEntity> getMzJsMx(int jsnm, string jslx, List<jsxm_entity> xmList)
        {
            List<OutpatientSettlementDetailEntity> resultList = new List<OutpatientSettlementDetailEntity>();
            foreach (jsxm_entity item in xmList)
            {
                OutpatientSettlementDetailEntity tmpItem = new OutpatientSettlementDetailEntity();
                tmpItem.sl = item.sl;
                tmpItem.jslx = jslx;
                tmpItem.jsnm = jsnm;
                tmpItem.mxnm = item.mxnm;
                tmpItem.cf_mxnm = item.cf_mxnm;
                tmpItem.jyje = item.jyje;
                tmpItem.jyfwje = item.jyfwje;
                resultList.Add(tmpItem);
            }
            return resultList;
        }

        /// <summary>
        /// 处方退费通知
        /// </summary>
        private void returnMoneyOfPrescriptionNo()
        {
            string statusDescription = string.Empty;
            string reqData = string.Empty;
            InterfaceRepParaBase<ADD_CF_TFTZ> interRepPara = null;
            bool jsgb = false;
            try
            {
                //获取实际要退费的信息，借用（获取处方状态）的对象
                List<GET_CF_STATUS> tyCfList = getTyInfo();
                if (tyCfList == null || tyCfList.Count == 0)
                    return;

                List<ADD_CF_TFTZ> tfTzList = new List<ADD_CF_TFTZ>();
                foreach (GET_CF_STATUS cfStatus in tyCfList)
                {
                    //如果没有药品信息，则表示不退
                    if (cfStatus.returnDrugInfo == null)
                        continue;

                    ADD_CF_TFTZ tftz = new ADD_CF_TFTZ();
                    tftz.pn = cfStatus.prescriptionNo;
                    tftz.pd = new List<pd>();

                    foreach (returnDrugInfo yp in cfStatus.returnDrugInfo)
                    {
                        if (yp.fourthLevelPkgQty == 0 && yp.thirdLevelPkgQty == 0)
                        {
                            continue;
                        }
                        pd pd = new pd();
                        pd.dc = yp.drugCode;
                        pd.fq = yp.fourthLevelPkgQty;
                        pd.tq = yp.thirdLevelPkgQty;

                        tftz.pd.Add(pd);
                    }
                    if (tftz.pd.Count > 0)
                    {
                        tfTzList.Add(tftz);
                    }
                }

                if (tfTzList.Count < 1)
                {
                    //没有要退的处方
                    return;
                }

                reqData = JsonConvert.SerializeObject(tfTzList);

                //   请求接口
                // WebPostAPI.API.post<ADD_CF_TFTZ>(Constants.InterfaceConstant.ADD_CF_TFTZ, reqData, out interRepPara, out statusDescription, out jsgb);
                jsgb = true;
                //接口关闭，直接返回
                if (jsgb)
                {
                    return;
                }
                //系统异常
                if (interRepPara == null)
                {
                    // throw new FailedCodeException("接口访问异常：" + statusDescription);
                    //   return;
                }
                else
                {
                    //如果有业务异常，则提示
                    if (interRepPara.failureMessage != null && interRepPara.failureMessage.Count > 0)
                    {
                        //   throw new FailedCodeException(interRepPara.failureMessageShow);
                        //  return;
                    }
                }
            }
            catch (Exception ex)
            {
                //  throw new FailedCodeException("处方退费通知接口处理失败：" + ex.Message);
                //  return;
            }
        }

        //筛选没有退药的处方和退药数量为0的药品
        private List<GET_CF_STATUS> GetInfoForSelect(List<GET_CF_STATUS> tyCfList)
        {
            List<GET_CF_STATUS> retCfList = new List<GET_CF_STATUS>();
            foreach (GET_CF_STATUS cfStatus in tyCfList)
            {
                //如果没有药品信息，则表示不退
                if (cfStatus.returnDrugInfo == null)
                    continue;

                GET_CF_STATUS retcfStatus = new GET_CF_STATUS();
                retcfStatus.prescriptionNo = cfStatus.prescriptionNo;
                retcfStatus.status = cfStatus.status;
                retcfStatus.returnDrugInfo = new List<returnDrugInfo>();

                foreach (returnDrugInfo yp in cfStatus.returnDrugInfo)
                {
                    if (yp.fourthLevelPkgQty == 0 && yp.thirdLevelPkgQty == 0)
                    {
                        continue;
                    }
                    retcfStatus.returnDrugInfo.Add(yp);
                }

                if (retcfStatus.returnDrugInfo.Count > 0)
                {
                    retCfList.Add(retcfStatus);
                }
            }
            return retCfList;
        }

        /// <summary>
        /// 门诊结算支付方式记录————原收款
        /// </summary>
        private List<OutpatientSettlementPaymentModelEntity> getJszffsEntityForSK()
        {
            List<OutpatientSettlementPaymentModelEntity> jszffsSKList =
                new List<OutpatientSettlementPaymentModelEntity>();
            foreach (tbl_mz_jszffs_Ex entity in jszffsExListForSK)
            {
                OutpatientSettlementPaymentModelEntity jszffs = (OutpatientSettlementPaymentModelEntity)entity;
                jszffsSKList.Add(jszffs);
            }

            return jszffsSKList;
        }

        private bool PosJY(string kh, OutpatientSettlementEntity paramMzjs, decimal xjzf, bool flag)
        {
            return true;
        }



        /// <summary>
        /// 门诊结算明细_退
        /// </summary>
        /// <param name="jslx">结算类型</param>
        /// <param name="detailList">明细</param>
        /// <returns></returns>
        private List<OutpatientSettlementDetailEntity> getMzJsMx(string jslx, int jsnm)
        {
            List<OutpatientSettlementDetailEntity> resultList = new List<OutpatientSettlementDetailEntity>();


            try
            {
                resultList = _RefundService.getMzJsMx(jslx, jsnm);
            }
            catch
            {
                throw;
            }
            return resultList;
        }

        /// <summary>
        ///新增系统病人帐户收支记录
        /// </summary>
        private List<SysPatientAccountRevenueAndExpenseEntity> getZhszjlList(OutpatientSettlementEntity tempMzJs,
            List<OutpatientSettlementPaymentModelEntity> parmjszffsArray, string tsFlag)
        {
            return null;
            //只有账户支付，才会有账户收支记录
            //  if (brzh == null)
            //   return null;
            //List<tbl_mz_jszffs> tempjszffsArray = parmjszffsArray;
            //if (tempjszffsArray == null || tempjszffsArray.Count == 0)
            //    return null;

            //List<tbl_xt_brzhszjl> szjlList = new List<tbl_xt_brzhszjl>();
            //tbl_xt_brzhszjl szjl = new tbl_xt_brzhszjl();
            //if (accountEntity.jch <= 0)
            //{
            //    tbl_mz_jszffs fs = tempjszffsArray.Find(t => t.xjzffs == NewtouchHIS.MAT.Constant.xtzhfs.ZHZF);
            //    if (fs == null)
            //        return null;
            //    if (tsFlag == "T")
            //    {
            //        //退款
            //        szjl = getSzjl(tempMzJs.jsnm, fs.zfje, Constant.szxzEnum.MZ_JS_TH, fs.zfbh);
            //    }
            //    else
            //    {
            //        //收款
            //        szjl = getSzjl(tempMzJs.jsnm, -fs.zfje, Constant.szxzEnum.MZ_JS_TH, fs.zfbh);
            //    }
            //    szjlList.Add(szjl);
            //}
            //else
            //{
            //    //家床病人
            //    tbl_mz_jszffs fs = tempjszffsArray.Find(t => t.xjzffs == NewtouchHIS.MAT.Constant.xtzhfs.ZHZF);
            //    if (fs == null)
            //        return null;
            //    if (tsFlag == "T")
            //    {
            //        //退款
            //        szjl = getSzjl(tempMzJs.jsnm, fs.zfje, Constant.szxzEnum.MZ_JS_TH, fs.zfbh);
            //    }
            //    else
            //    {
            //        //收款
            //        szjl = getSzjl(tempMzJs.jsnm, -fs.zfje, Constant.szxzEnum.MZ_JS_TH, fs.zfbh);
            //    }
            //    szjlList.Add(szjl);


            //    //找零记录
            //    tbl_xt_xjzffs xjzffs = BF<xt_xjzffs>.Instance.getModel(NewtouchHIS.MAT.Constant.xtzhfs.XJZF);
            //    if (xjzffs == null)
            //        throw new FailedCodeException("系统中不存在该现金支付！");
            //    szjl = getSzjl(tempMzJs.jsnm, tempMzJs.zl, NewtouchHIS.MAT.Constant.szxzEnum.MZ_CZ_QK, xjzffs.xjzffsbh);
            //    szjlList.Add(szjl);

            //    ////帐户支出
            //    //foreach (tbl_mz_jszffs_Ex entity in tempjszffsArray)
            //    //{
            //    //    szjl = getSzjl(brzh, tempMzJs);
            //    //    szjl.szje = -Convert.ToDecimal(lblYsk.Text);
            //    //    szjl.szxz = ((int)NewtouchHIS.MAT.Constant.szxzEnum.MZ_JS_TH).ToString();
            //    //    szjl.xjzffsbh = entity.zfbh;
            //    //    tempMzJs.zhjz = szjl.szje;
            //    //    szjlList.Add(szjl);
            //    //}
            //}

            //return szjlList;
        }

        /// <summary>
        /// 重新检查收据号
        /// </summary>
        private void checkSJH(string sjh)
        {
            try
            {
                FinanceReceiptEntity sjEntity = new FinanceReceiptEntity();
                string type = string.Empty;
                //获取收据号
                //   List<string> fpsql = new List<string>();
                if (_finRecRepos.checkSJH(sjh, userModel.UserCode, out sjEntity, out type, this.OrganizeId))
                {
                    accountEntity.fph = sjh;
                    // fpsqlList = fpsql;

                    if (type == "insert")
                    {
                        _finRecRepos.AddReceiptInfo(sjEntity, this.OrganizeId); // 添加收据凭证号
                    }
                    if (type == "update")
                    {
                        _finRecRepos.UpdateReceiptInfo(sjEntity, sjEntity.cwsjId); //修改收据凭证号
                    }
                }
                else
                {
                    accountEntity.fph = string.Empty;
                    fpsqlList = new List<string>();
                }
            }
            catch
            {
                accountEntity.fph = string.Empty;
                fpsqlList = new List<string>();
                throw;
            }
        }

        #region 不做功能

        //private bool PosJY(string kh, tbl_mz_js paramMzjs, decimal xjzf, bool flag)
        //   {
        //       //撤销交易
        //       string code = PosCXInterface(kh, paramMzjs.fph, paramMzjs.jsnm);
        //       if (code != "00")
        //       {
        //           if (code == "12" || code == "E6")
        //           {
        //               //pos撤销
        //               this.isAlreadyPosCX = true;
        //               if (DialogHelper.Ask_YesNo("收费处确认pos撤销交易已完成，请选择YES,退款将继续，否则终止退款。") == DialogResult.No)
        //               {
        //                   return false;
        //               }
        //           }
        //           else if (code == "XX")
        //           {
        //               //无撤销
        //               this.isAlreadyPosCX = false;
        //           }
        //           else
        //           {
        //               return false;
        //           }

        //           if (this.isAlreadyPosCX == false)
        //           {
        //               if (Tools.getIniFileName("SFYXTFPZparm") != "0")
        //               {
        //                   DialogHelper.DlgInfo("当前windows上POS机退费功能已关闭,无法做POS退费操作！");
        //                   return false;
        //               }
        //           }

        //           //全退款交易
        //           if (flag == true)  //true == isReturnAll 
        //           {
        //               code = PosTfInterface(Kh, paramMzjs.fph, xjzf);
        //               if (code != "00")
        //               {
        //                   if (code == "12")
        //                   {
        //                       if (DialogHelper.Ask_YesNo("收费处确认pos退款已完成时，请选择YES,退款将继续，否则终止退款。") == DialogResult.No)
        //                       {
        //                           return false;
        //                       }
        //                   }
        //                   else if (code == "FAIL")
        //                   {
        //                       this.posTxj = true;
        //                   }
        //                   else
        //                   {
        //                       return false;
        //                   }
        //               }
        //           }
        //       }
        //       else
        //       {
        //           //pos撤销交易成功
        //           this.isAlreadyPosCX = true;
        //       }
        //       return true;
        //   }        //private bool PosJY(string kh, tbl_mz_js paramMzjs, decimal xjzf, bool flag)
        //   {
        //       //撤销交易
        //       string code = PosCXInterface(kh, paramMzjs.fph, paramMzjs.jsnm);
        //       if (code != "00")
        //       {
        //           if (code == "12" || code == "E6")
        //           {
        //               //pos撤销
        //               this.isAlreadyPosCX = true;
        //               if (DialogHelper.Ask_YesNo("收费处确认pos撤销交易已完成，请选择YES,退款将继续，否则终止退款。") == DialogResult.No)
        //               {
        //                   return false;
        //               }
        //           }
        //           else if (code == "XX")
        //           {
        //               //无撤销
        //               this.isAlreadyPosCX = false;
        //           }
        //           else
        //           {
        //               return false;
        //           }

        //           if (this.isAlreadyPosCX == false)
        //           {
        //               if (Tools.getIniFileName("SFYXTFPZparm") != "0")
        //               {
        //                   DialogHelper.DlgInfo("当前windows上POS机退费功能已关闭,无法做POS退费操作！");
        //                   return false;
        //               }
        //           }

        //           //全退款交易
        //           if (flag == true)  //true == isReturnAll 
        //           {
        //               code = PosTfInterface(Kh, paramMzjs.fph, xjzf);
        //               if (code != "00")
        //               {
        //                   if (code == "12")
        //                   {
        //                       if (DialogHelper.Ask_YesNo("收费处确认pos退款已完成时，请选择YES,退款将继续，否则终止退款。") == DialogResult.No)
        //                       {
        //                           return false;
        //                       }
        //                   }
        //                   else if (code == "FAIL")
        //                   {
        //                       this.posTxj = true;
        //                   }
        //                   else
        //                   {
        //                       return false;
        //                   }
        //               }
        //           }
        //       }
        //       else
        //       {
        //           //pos撤销交易成功
        //           this.isAlreadyPosCX = true;
        //       }
        //       return true;
        //   }


        #endregion

        #region GRS门诊退费

        /// <summary>
        /// GRS门诊退费
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="GridViewMxList"></param>
        /// <param name="jsnm"></param>
        /// <param name="isReturnAll"></param>
        /// <returns></returns>
        public bool btnReturnInAcc(string blh, List<GridViewMx> GridViewMxList, int jsnm, out bool isReturnAll)
        {
            var result = false;
            kahao = kh;
            Jsnm = jsnm;
            var data = _RefundService.GetjsInfoByblh(blh, "", "", OrganizeId);
            var fapiao = data.Find(p => p.jsnm == jsnm);
            var GridViewMxData = _RefundService.GetGridViewMx(jsnm, OrganizeId);
            GridViewMxList.ForEach(p =>
            {
                var tem = GridViewMxData.Find(c => c.id == p.id);
                p.tag = tem.tag;
                p.IS_RETURN = "true";
            });


            var tblMzJs = new OutpatientSettlementEntity
            {
                fph = fapiao.oldFPH,
                flzffy = fapiao.flzffy,
                zffy = fapiao.zffy,
                jzfy = fapiao.jzfy,
                zlfy = fapiao.zlfy,
                jsnm = fapiao.jsnm,
                patid = fapiao.patid,
                xjzf = fapiao.xjzf,
                jslx = fapiao.jslx,
                brxz = fapiao.brxz,
                zje = fapiao.zje,
                xjwc = fapiao.xjwc,
                zhjz = fapiao.zhjz,
                xjzffs = fapiao.xjzffs,
                jszt = fapiao.jszt,
                cxjsnm = fapiao.cxjsnm,
                zh = fapiao.zh,
                fpdm = fapiao.fpdm,
                jmje = fapiao.jmje,
                jylx = fapiao.jylx,
                ysk = fapiao.ysk,
                zl = fapiao.zl,
                jzsj = fapiao.CreateTime
            };
            if (tblMzJs != null)
            {
                var MZJS = _RefundService.GetMZJSByJsnmInAcc(jsnm, OrganizeId);
                var tmpDr = MZJS[0];

                #region 全退半退数据明细
                //设置全退，半退，不退明细list

                getRecordWithCategory(GridViewMxList);
                var accountEntityAll = getAccountList(2, tblMzJs, tmpDr);
                var accountEntity = getAccountList(1, tblMzJs, tmpDr);
                if (accountEntityAll.jsxmList.Count == 0)
                {
                    accountEntityAll = accountEntity;
                }
                #endregion

                isReturnAll = (resultList["any"].Count == 0 && resultList["nothing"].Count == 0);
                var returnsOk = true;
                IsReturnAll = isReturnAll;
                Kh = kh;
                try
                {
                    TuiFeiJSInAcc(jsnm, ZffsDt, isReturnAll, accountEntity, ghjsDt);
                }
                catch
                {
                    returnsOk = false;
                }
                return returnsOk;

            }
            isReturnAll = false;
            return result;

        }

        /// <summary>
        /// 退费结算
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="ZffsDt"></param>
        /// <param name="isReturnAll"></param>
        /// <param name="accountEntity"></param>
        /// <param name="GhjsDt"></param>
        public void TuiFeiJSInAcc(int jsnm, DataTable ZffsDt, bool isReturnAll, JS_ENTITY accountEntity,
            List<OutpatientRegistNonAttendanceEntity> GhjsDt)
        {
            isAlreadyPosCX = false;
            posTxj = false;
            mzJs = _RefundService.GetMZJSFromMz_jsByJsnm(jsnm);
            zffsDt = ZffsDt;
            ghjsDt = GhjsDt;

            lblSsk = (mzJs.xjzf).ToString("0.00");

            if (false == isReturnAll) //如果是部分退
            {
                var ybError = false;
                var tuije = accountEntity.jsxmList.Sum(p => p.je);
                var brxz = _RefundService.GetBrxzByBrbh(accountEntity.brxz);
                //if (brxz == null)
                //    throw new FailedCodeException("HOSP_PATIENT_NATURE_IS_NOT_EXIST");
                //基本信息
                newMzJs = getMzJsInAcc(accountEntity, brxz, out ybError, out sqxh);
                newMzJs.jzsj = mzJs.jzsj;
                newMzJs.zh = mzJs.zh;
                newMzJs.zlfy = tuije;
                newMzJs.xjzf = newMzJs.zlfy;
                newMzJs.zje = newMzJs.xjzf;
            }
            mzJs.CreateTime = DateTime.Now; //获取系统服务器时间
            mzJs.CreatorCode = userModel.UserCode; //记录退费操作员
            var jzrqList = Convert.ToDateTime(mzJs.jzsj);
            //全退
            fullBack(Kh, accountEntity.isGh, mzJs, ghjsDt);
            if (false == isReturnAll) //如果是部分退
                PartBackInAcc(newMzJs, accountEntity, mzJs);
            _RefundService.Minusybcs(jzrqList, isReturnAll, OrganizeId, accountEntity.patid);
        }

        /// <summary>
        /// 需要结算的项目信息
        /// </summary>
        /// <param name="jsEntity"></param>
        /// <param name="brxz"></param>
        /// <param name="ybError"></param>
        /// <param name="sqxh"></param>
        /// <param name="jsdl"></param>
        /// <returns></returns>
        private OutpatientSettlementEntity getMzJsInAcc(JS_ENTITY jsEntity, SysPatientNatureEntity brxz,
            out bool ybError,
            out string sqxh)
        {
            var jylx = Constants.ybDealLB.yb_deal_wjy;
            sqxh = string.Empty;
            ybError = false;

            if (jsEntity.jsxmList == null)
                throw new FailedCodeException("HOSP_SETTLEMENTITEM_IS_NULL");
            if (jsEntity.ghnmList == null || jsEntity.ghnmList.Count == 0)
                throw new FailedCodeException("GHNM_REQUIRED");

            var js = new OutpatientSettlementEntity();

            //根据病人内码获取病人信息
            var brjbxx = _RefundService.GetBrjbxxByPatid(jsEntity.patid);
            if (brjbxx == null)
                throw new FailedCodeException("HOSP_SYSPATIENT_BASICINFO_IS_NOT_EXIST");

            js.jsnm = _RefundService.GetPrimaryKeyByTableName("mz_js");
            js.patid = brjbxx.patid;
            js.brxz = brxz == null ? null : brxz.brxz;
            js.jslx = jsEntity.jslx;
            js.zffy = 0; //自负费用
            js.flzffy = 0; //分类自负费用
            js.jzfy = 0; //记账费用，如果是医保病人，为医保返回的结果
            js.jmje = 0; //减免费用
            js.jszt = (int)Constants.jsztEnum.YJ; //结算状态
            js.xjzffs = string.Empty;
            js.ghnm = jsEntity.ghnm;
            //现金支付
            decimal ssk;
            //现金误差
            decimal difference;

            var ysk = jsEntity.jsxmList.Sum(p => p.je);
            jylx = Constants.ybDealLB.yb_deal_wjy;
            js.jylx = ((int)jylx).ToString();
            js.zffy = 0;
            Ext.get5s6r(ysk, out ssk, out difference);
            js.xjwc = difference;
            return js;
        }

        /// <summary>
        /// 半退
        /// </summary>
        /// <param name="newMzJs"></param>
        /// <param name="accountEntity"></param>
        /// <param name="mzJs"></param>
        private void PartBackInAcc(OutpatientSettlementEntity newMzJs, JS_ENTITY accountEntity,
            OutpatientSettlementEntity mzJs)
        {

            //if (!checkJs())
            //    return;
            //结算支付方式记录（退款方式）
            //jszffsArray = getJszffsEntity();

            //收款方式-去掉退款方式就是半退的结算方式
            //var jszffsArrayForSK = getJszffsEntity(jszffsArray);

            mzJs.CreatorCode = userModel.UserCode;
            mzJs.CreateTime = DateTime.Now;
            var result = isAlreadyComfirmTF;
            newMzJs.CreatorCode = userModel.UserCode;
            newMzJs.CreateTime = DateTime.Now;

            #region 半退收费 数据库操作
            newMzJs.fph = string.Empty;
            var listMzJsMx =
                getMzJsMx(newMzJs.jsnm, newMzJs.jslx, accountEntity.jsxmList);
            newMzJs.cxjsnm = Jsnm; //旧结算是退新的撤销结算内码 
            var szjlList = getZhszjlList(newMzJs, jszffsArray, "S");
            var result2 = _RefundService.InsertRemainFromReturns(newMzJs, listMzJsMx, jszffsArray, jsdlList);
            if (false == result2)
                throw new FailedCodeException("JS_ERROR_FAIL");
            #endregion
        }
        #endregion

        #region 医保退费

        /// <summary>
        /// 退费流程.新的未结明细上传
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="jsnm"></param>
        /// <param name="tjsxmDict"></param>
        /// <param name="newwjybjsh"></param>
        /// <param name="tmxzje">退明细总金额</param>
        /// <param name="yxzje">余下总金额</param>
        public void DetailsUploadYb(string orgId, int jsnm
            , Dictionary<int, decimal> tjsxmDict
            , string newwjybjsh
            , out decimal tmxzje
            , out decimal yxzje)
        {
            tmxzje = 0;
            yxzje = 0;

            var clearResult = YibaoUniteInterface.ClearDetail(newwjybjsh);
            if (clearResult.Code != 0)
            {
                throw new FailedException("明细重新上传医保失败." + clearResult.ErrorMsg);
            }

            var sourcelist = _RefundService.RefundableQuery(this.OrganizeId, jsnm);

            var ybDetailList = new List<newtouchyibao.Models.DETAIL>();
            foreach (var item in sourcelist)
            {
                decimal tsl = 0;
                decimal newsl = item.sl.Value;
                if (tjsxmDict.TryGetValue(item.jsmxnm, out tsl))
                {
                    newsl = newsl - tsl;
                }

                if (newsl < 0)
                {
                    //throw
                    tmxzje += item.jsmxje.Value;    //被全退了
                    continue;
                }
                else if (newsl == 0)
                {
                    tmxzje += item.jsmxje.Value;    //被全退了
                    continue;
                }

                var detail = new newtouchyibao.Models.DETAIL();
                detail.ITEMID = item.feeType == 1 
                    ? ("YP" + item.cfmxId.Value.ToString()) 
                    : ("XM" + item.xmnm.Value.ToString());
                //医保0药品 1项目
                detail.DKE085 = item.feeType == 1 ? "0" : "1";
                detail.DKE012 = item.sfxmCode;
                detail.AKE227 = item.klsj;
                detail.AKE226 = item.ks;
                detail.AKE228 = item.ys;
                detail.AKE229 = item.ysmc;
                detail.AKE002 = item.mc;
                //药品规格、生产地
                detail.AKE207 = item.dw;
                detail.AKE212 = item.sl.Value;
                detail.AKE208 = item.dj;
                detail.AKE198 = item.jsmxje.Value;
                if (newsl != item.sl.Value)
                {
                    //退了部分
                    detail.AKE212 = newsl;
                    detail.AKE198 = item.dj * newsl;

                    tmxzje += item.jsmxje.Value - detail.AKE198;
                }
                //其他药品相关
                ybDetailList.Add(detail);

                yxzje += detail.AKE198;
            }
            if (ybDetailList.Count > 0)
            {
                var preaccResult = YibaoUniteInterface.PreAccount(newwjybjsh, ybDetailList);
                if (preaccResult.Code != 0)
                {
                    throw new FailedException("明细上传医保失败." + preaccResult.ErrorMsg);
                }
            }
            return;
        }

        #endregion

    }
}

