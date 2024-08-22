using Newtouch.HIS.Domain.Entity;
using System;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.BusinessObjects

{
    public class InterfaceBase
    { }
    public class MZJS
    {
        public int patid { get; set; }
        public string BRXZ { get; set; }
        public string BRXZMC { get; set; }
        public string KS { get; set; }
        public string KSMC { get; set; }
        public string YS { get; set; }
        public string RYMC { get; set; }
        public int GHNM { get; set; }
    }
    [NotMapped]

    public class MZJSInfo
    {
        public string xm { get; set; }
        public string kh { get; set; }
        public int jsnm { get; set; }
        public int patid { get; set; }
        public decimal flzffy { get; set; }
        public decimal zffy { get; set; }
        public decimal jzfy { get; set; }
        public decimal zlfy { get; set; }
        public decimal xjzf { get; set; }
        // public string xm { get; set; }

        public string fph { get; set; }
        public string fpdm { get; set; }
        public int zh { get; set; }
        public string oldFPH { get; set; }
        public DateTime? CreateTime { get; set; }
        public decimal zje { get; set; }
        public decimal zhjz { get; set; }

        public int cxjsnm { get; set; }
        public int brxzbh { get; set; }
        public int jszt { get; set; }
        public string brxz { get; set; }
        public decimal jmje { get; set; }
        public string jslx { get; set; }
        public string jylx { get; set; }
        public decimal xjwc { get; set; }
        public string xjzffs { get; set; }
        public decimal zl { get; set; }
        public decimal ysk { get; set; }
        public string czr { get; set; }
        public string blh { get; set; }

    }
    /// <summary>
    /// 包含医保和非医保
    /// </summary>
    public class Brsfsf_ReturnValue_YBANDFYB
    {
        public Brsfsf_ReturnValue_YBANDFYB()
        {
            YB = new Brsfsf_ReturnValue();
            FYB = new Brsfsf_ReturnValue();
        }

        /// <summary>
        /// 病人性质
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// 需要医保交易
        /// </summary>
        public Brsfsf_ReturnValue YB { get; set; }

        /// <summary>
        /// 不需要医保交易
        /// </summary>
        public Brsfsf_ReturnValue FYB { get; set; }

        /// <summary>
        /// 交易费用总额
        /// </summary>
        public decimal jyze
        {
            get
            {
                return YB.jyze;
            }
        }

        /// <summary>
        /// 医保结算范围费用总额
        /// </summary>
        public decimal ybjsfwze
        {
            get
            {
                return YB.ybjsfwze;
            }
        }

        /// <summary>
        /// 非医保结算范围个人自费
        /// </summary>
        public decimal fybjsfwgrzf
        {
            get
            {
                return YB.fybjsfwgrzf;
            }
        }

        /// <summary>
        /// 分类自负
        /// </summary>
        public decimal flzfHj
        {
            get
            {
                return YB.flzfHj + FYB.flzfHj;
            }
        }

        /// <summary>
        /// 自理
        /// </summary>
        public decimal zlHj
        {
            get
            {
                return YB.zlHj + FYB.zlHj;
            }
        }

        /// <summary>
        /// 记账费用（如果是医保病人，为医保返回的结果）
        /// </summary>
        public decimal jzfyHj
        {
            get
            {
                return YB.jzfyHj + FYB.jzfyHj;
            }
        }

        /// <summary>
        /// 现金（未包含　医保返回的自负）
        /// </summary>
        public decimal xjHj
        {
            get
            {
                return YB.xjHj + FYB.xjHj;
            }
        }

        /// <summary>
        /// 减免金额
        /// </summary>
        public decimal jmjeHj
        {
            get
            {
                return YB.jmjeHj + FYB.jmjeHj;
            }
        }

        /// <summary>
        /// 算法自负（通过算法计算出的自负金额）
        /// </summary>
        public decimal sfzfHj
        {
            get
            {
                return YB.sfzfHj + FYB.sfzfHj;
            }
        }


        /// <summary>
        /// 算法自理（通过算法计算出的自理金额）
        /// </summary>
        public decimal sfzlHj
        {
            get
            {
                return YB.sfzlHj + FYB.sfzlHj;
            }
        }

        /// <summary>
        /// 费用合计（自费病人需要要支付的现金）
        /// </summary>
        public decimal totalHj
        {
            get
            {
                return YB.totalHj + FYB.totalHj;
            }
        }

        /// <summary>
        /// 交易明细
        /// </summary>
        public List<HospTransactionSettlementDetailEntity> jsmxList { get; set; }

    }
    public class Brsfsf_ReturnValue
    {
        public Brsfsf_ReturnValue()
        {
            this.dlFymxList = new List<Brsfsf_Dl_Fymx>();
        }

        /// <summary>
        /// 交易费用总额
        /// </summary>
        public decimal jyze
        {
            get
            {
                if (dlFymxList == null)
                    return 0m;

                Dictionary<string, decimal> dlfy = new Dictionary<string, decimal>();

                decimal fy = 0m;
                foreach (Brsfsf_Dl_Fymx fymx in this.dlFymxList)
                {
                    string key = fymx.dl;
                    if (!dlfy.ContainsKey(key))
                        dlfy.Add(key, 0);

                    dlfy[key] = dlfy[key] + fymx.jzfy;
                }

                foreach (KeyValuePair<string, decimal> pair in dlfy)
                {
                    //四舍五入
                    fy += Math.Round(pair.Value, 2, MidpointRounding.AwayFromZero);
                }

                return fy;
            }
        }

        /// <summary>
        /// 医保结算范围费用总额
        /// </summary>
        public decimal ybjsfwze
        {
            get
            {
                if (dlFymxList == null)
                    return 0m;

                Dictionary<string, decimal> dlfy = new Dictionary<string, decimal>();

                decimal fy = 0m;
                foreach (Brsfsf_Dl_Fymx fymx in this.dlFymxList)
                {
                    string key = fymx.dl;
                    if (!dlfy.ContainsKey(key))
                        dlfy.Add(key, 0);

                    dlfy[key] = dlfy[key] + (fymx.jzfy + fymx.flzf);
                }

                foreach (KeyValuePair<string, decimal> pair in dlfy)
                {
                    //四舍五入
                    fy += Math.Round(pair.Value, 2, MidpointRounding.AwayFromZero);
                }

                return fy;
            }
        }

        /// <summary>
        /// 非医保结算范围个人自费
        /// </summary>
        public decimal fybjsfwgrzf
        {
            get
            {
                if (dlFymxList == null)
                    return 0m;

                Dictionary<string, decimal> dlfy = new Dictionary<string, decimal>();

                decimal fy = 0m;
                foreach (Brsfsf_Dl_Fymx fymx in this.dlFymxList)
                {
                    string key = fymx.dl;
                    if (!dlfy.ContainsKey(key))
                        dlfy.Add(key, 0);

                    dlfy[key] = dlfy[key] + fymx.zl;
                }

                foreach (KeyValuePair<string, decimal> pair in dlfy)
                {
                    //四舍五入
                    fy += Math.Round(pair.Value, 2, MidpointRounding.AwayFromZero);
                }

                return fy;
            }
        }

        /// <summary>
        /// 分类自负
        /// </summary>
        public decimal flzfHj
        {
            get
            {
                if (dlFymxList == null)
                    return 0m;

                decimal fy = 0m;
                foreach (Brsfsf_Dl_Fymx fymx in this.dlFymxList)
                {
                    fy += fymx.flzf;
                }

                return fy;
            }
        }

        /// <summary>
        /// 自理
        /// </summary>
        public decimal zlHj
        {
            get
            {
                if (dlFymxList == null)
                    return 0m;

                decimal fy = 0m;
                foreach (Brsfsf_Dl_Fymx fymx in this.dlFymxList)
                {
                    fy += fymx.zl;
                }

                return fy;
            }
        }

        /// <summary>
        /// 记账费用（如果是医保病人，为医保返回的结果）
        /// </summary>
        public decimal jzfyHj
        {
            get
            {
                if (dlFymxList == null)
                    return 0m;

                decimal fy = 0m;
                foreach (Brsfsf_Dl_Fymx fymx in this.dlFymxList)
                {
                    fy += fymx.jzfy;
                }

                //可记账费用减去超额部分
                fy = fy - jzce;
                //计算后的起付金额
                if (fy > qfje)
                {
                    fy = fy - qfje;
                }
                else
                {
                    fy = 0;
                }

                return fy;
            }
        }

        /// <summary>
        /// 现金（未包含　医保返回的自负）
        /// </summary>
        public decimal xjHj
        {
            get
            {
                if (dlFymxList == null)
                    return 0m;

                decimal fy = 0m;
                foreach (Brsfsf_Dl_Fymx fymx in this.dlFymxList)
                {
                    fy += fymx.xj;
                }

                return fy;
            }
        }

        /// <summary>
        /// 减免金额
        /// </summary>
        public decimal jmjeHj
        {
            get
            {
                if (dlFymxList == null)
                    return 0m;

                decimal fy = 0m;
                foreach (Brsfsf_Dl_Fymx fymx in this.dlFymxList)
                {
                    fy += fymx.jmje;
                }

                return fy;
            }
        }

        /// <summary>
        /// 算法自负（通过算法计算出的自负金额）
        /// </summary>
        public decimal sfzfHj
        {
            get
            {
                if (dlFymxList == null)
                    return 0m;

                decimal fy = 0m;
                foreach (Brsfsf_Dl_Fymx fymx in this.dlFymxList)
                {
                    fy += fymx.sfzf;
                }

                //自负加上记账超额部分
                fy = fy + jzce;
                //计算后的起付金额
                if (jzfyHj > qfje)
                {
                    fy = fy + qfje;
                }

                return fy;
            }
        }

        /// <summary>
        /// 算法自理（通过算法计算出的自理金额）
        /// </summary>
        public decimal sfzlHj
        {
            get
            {
                if (dlFymxList == null)
                    return 0m;

                decimal fy = 0m;
                foreach (Brsfsf_Dl_Fymx fymx in this.dlFymxList)
                {
                    fy += fymx.sfzl;
                }

                return fy;
            }
        }


        /// <summary>
        /// 费用合计（自费病人需要要支付的现金）
        /// </summary>
        public decimal totalHj
        {
            get
            {
                if (dlFymxList == null)
                    return 0m;

                decimal fy = 0m;
                foreach (Brsfsf_Dl_Fymx fymx in this.dlFymxList)
                {
                    fy += fymx.total;
                }

                //现金加上记账超额部分
                fy = fy + jzce;
                //计算后的起付金额
                if (jzfyHj > qfje)
                {
                    fy = fy + qfje;
                }

                return fy;
            }
        }

        /// <summary>
        /// 大类费用明细
        /// </summary>
        public List<Brsfsf_Dl_Fymx> dlFymxList { get; set; }

        /// <summary>
        /// 项目费用明细  ----fangyi
        /// </summary>
        public List<SFXM_FYMX> fymxList { get; set; }


        /// <summary>
        /// 获取收费大类医保结算范围费用总额
        /// </summary>
        /// <returns></returns>
        public decimal getDlJyje(string dl, bool flag)
        {
            decimal amount = 0m;
            foreach (Brsfsf_Dl_Fymx fymx in dlFymxList)
            {
                if (fymx.dl == dl)
                {
                    if (flag == true)
                    {
                        amount += (fymx.total - fymx.sfzl) + (fymx.jzfy + fymx.flzf);
                    }
                    else
                    {
                        amount += (fymx.jzfy + fymx.flzf);
                    }
                    break;
                }
            }

            //四舍五入
            amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);

            return amount;
        }


        /// <summary>
        /// 获取收费大类医保结算范围费用总额
        /// </summary>
        /// <returns></returns>
        public decimal getDlJyje(string dl)
        {
            decimal amount = 0m;
            foreach (Brsfsf_Dl_Fymx fymx in dlFymxList)
            {
                if (fymx.dl == dl)
                {
                    amount += (fymx.jzfy + fymx.flzf);
                    break;
                }
            }

            //四舍五入
            amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);

            return amount;
        }

        /// <summary>
        /// 获取收费大类医保结算范围费用总额
        /// </summary>
        /// <param name="yydlList">医院大类</param>
        /// <returns></returns>
        public decimal getDlJyje(List<string> yydlList)
        {
            decimal amount = 0m;
            foreach (Brsfsf_Dl_Fymx fymx in dlFymxList)
            {
                if (yydlList.Contains(fymx.dl))
                {
                    amount += (fymx.jzfy + fymx.flzf);
                }
            }

            //四舍五入
            amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);

            return amount;
        }

        /// <summary>
        /// 记账超额部分（要转入自负）
        /// </summary>
        public decimal jzce { get; set; }

        /// <summary>
        /// 计算后的起付金额（要转入自负）
        /// </summary>
        public decimal qfje { get; set; }
    }
    /// <summary>
    /// 病人收费算法计算出的大类费用明细
    /// </summary>
    public class Brsfsf_Dl_Fymx
    {
        /// <summary>
        /// 大类
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 明细编码（暂时仅供新农合使用）
        /// </summary>
        //public string mxbm { get; set; }

        /// <summary>
        /// 收费项目（暂时仅供新农合使用）
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// 明细内码（暂时仅供新农合使用）
        /// </summary>
        public int mxnm { get; set; }

        /// <summary>
        /// 处方明细内码（暂时仅供新农合使用）
        /// </summary>
        public int cf_mxnm { get; set; }

        /// <summary>
        /// 成组号（暂时仅供新农合使用）
        /// </summary>
        public string czh { get; set; }

        /// <summary>
        /// 分类自负
        /// </summary>
        public decimal flzf { get; set; }

        /// <summary>
        /// 自理
        /// </summary>
        public decimal zl { get; set; }

        /// <summary>
        /// 可记账费用
        /// </summary>
        public decimal jzfy { get; set; }

        /// <summary>
        /// 现金
        /// </summary>
        public decimal xj { get; set; }

        /// <summary>
        /// 减免金额
        /// </summary>
        public decimal jmje { get; set; }

        /// <summary>
        /// 算法自负（通过算法计算出的自负金额）
        /// </summary>
        public decimal sfzf { get; set; }

        /// <summary>
        /// 算法自理（通过算法计算出的自负金额）
        /// </summary>
        public decimal sfzl { get; set; }

        /// <summary>
        /// 费用合计（自费病人需要要支付的现金）
        /// </summary>
        public decimal total { get; set; }

        /// <summary>
        /// 住院项目计费表编号
        /// </summary>
        public int xmjfbbh { get; set; }

        /// <summary>
        /// 住院药品计费表编号
        /// </summary>
        public int ypjfbbh { get; set; }

        /// <summary>
        /// 医保交易金额（计算之后得到的数据）
        /// </summary>
        public decimal jyje { get; set; }

        /// <summary>
        /// 医保交易范围金额（计算之后得到的数据）
        /// </summary>
        public decimal jyfwje { get; set; }

        /// <summary>
        /// 医嘱类型1 药品 2 项目
        /// </summary>
        public string yzlx { get; set; }
    }
    public partial class SFXM_FYMX
    {
        public SFXM_FYMX() { }

        #region model
        #region property
        private decimal _kbje;
        private decimal _flzf;
        private decimal _zlfy;
        private decimal _jmje;
        #endregion property

        /// <summary>
        /// 大类
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 明细编码
        /// </summary>
        //public string mxbm { get; set; }

        /// <summary>
        /// 收费项目
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// 明细内码
        /// </summary>
        public int mxnm { get; set; }

        /// <summary>
        /// 处方明细内码
        /// </summary>
        public int cf_mxnm { get; set; }

        /// <summary>
        /// 成组号
        /// </summary>
        public string czh { get; set; }

        /// <summary>
        /// 住院项目计费表编号
        /// </summary>
        public int xmjfbbh { get; set; }

        /// <summary>
        /// 住院药品计费表编号
        /// </summary>
        public int ypjfbbh { get; set; }

        /// <summary>
        /// 医嘱类型1 药品 2 项目
        /// </summary>
        public string yzlx { get; set; }

        /// <summary>
        /// 医保交易金额（计算之后得到的数据）
        /// </summary>
        public decimal jyje { get; set; }

        /// <summary>
        /// 医保交易范围金额（计算之后得到的数据）
        /// </summary>
        public decimal jyfwje { get; set; }

        /// <summary>
        /// 可报金额
        /// </summary>
        public decimal Kbje
        {
            get { return _kbje; }
            set { _kbje = value; }
        }

        /// <summary>
        /// 分类自负
        /// </summary>
        public decimal Flzf
        {
            get { return _flzf; }
            set { _flzf = value; }
        }

        /// <summary>
        /// 自理费用
        /// </summary>
        public decimal Zlfy
        {
            get { return _zlfy; }
            set { _zlfy = value; }
        }

        /// <summary>
        /// 减免金额
        /// </summary>
        public decimal Jmje
        {
            get { return _jmje; }
            set { _jmje = value; }
        }

        #endregion model
    }
    public partial class CMSEntity
    {
        public CMSEntity() { }

        #region model
        #region property
        private string _dl;
        private string _sfxm;
        private decimal _sl;
        private decimal _kbje;
        private decimal _flzf;
        private decimal _zlfy;
        private decimal _jmje;
        #endregion property

        /// <summary>
        /// 大类
        /// </summary>
        public string Dl
        {
            get { return _dl; }
            set { _dl = value; }
        }

        /// <summary>
        /// 收费项目
        /// </summary>
        public string Sfxm
        {
            get { return _sfxm; }
            set { _sfxm = value; }
        }

        /// <summary>
        /// 数量，大类数量为1，收费项目为实际数量
        /// </summary>
        public decimal Sl
        {
            get { return _sl; }
            set { _sl = value; }
        }

        /// <summary>
        /// 可报金额
        /// </summary>
        public decimal Kbje
        {
            get { return _kbje; }
            set { _kbje = value; }
        }

        /// <summary>
        /// 分类自负
        /// </summary>
        public decimal Flzf
        {
            get { return _flzf; }
            set { _flzf = value; }
        }

        /// <summary>
        /// 自理费用
        /// </summary>
        public decimal Zlfy
        {
            get { return _zlfy; }
            set { _zlfy = value; }
        }

        /// <summary>
        /// 减免金额
        /// </summary>
        public decimal Jmje
        {
            get { return _jmje; }
            set { _jmje = value; }
        }

        #endregion model
    }
    [NotMapped]
    public class tbl_mz_jszffs_Ex : OutpatientSettlementPaymentModelEntity
    {
        /// <summary>
        /// 现金支付方式名称
        /// </summary>
     //   public string xjzffsmc { get; set; }


    }
    #region 获取处方状态
    [Serializable]
    public class GET_CF_STATUS : InterfaceBase
    {
        /// <summary>
        /// 处方号
        /// </summary>
        public string prescriptionNo { get; set; }

        /// <summary>
        /// 处方状态
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 处方退药信息
        /// </summary>
        public List<returnDrugInfo> returnDrugInfo { get; set; }
    }
    [Serializable]
    public class returnDrugInfo : InterfaceBase
    {
        /// <summary>
        /// 药品编码
        /// </summary>
        public string drugCode { get; set; }

        /// <summary>
        /// 三级退药数量
        /// </summary>
        public int thirdLevelPkgQty { get; set; }

        /// <summary>
        /// 四级退药数量
        /// </summary>
        public int fourthLevelPkgQty { get; set; }

    }
    #endregion
    public class InterfaceRepParaBase<T>
    {
        /// <summary>
        /// 操作成功的数据条数
        /// </summary>
        [JsonProperty]
        public int successCount { get; set; }

        /// <summary>
        /// 操作失败的数据条数
        /// </summary>
        [JsonProperty]
        public int failureCount { get; set; }

        /// <summary>
        /// 操作成功的返回结果
        /// </summary>
        [JsonProperty]
        public List<T> result { get; set; }

        /// <summary>
        /// 操作失败的消息列表
        /// </summary>
        [JsonProperty]
        public List<failureMessage> failureMessage { get; set; }

        /// <summary>
        /// 操作成功的返回结果
        /// </summary>
        [JsonIgnore]
        public string failureMessageShow
        {
            get
            {
                string strMsg = string.Empty;
                foreach (failureMessage m in failureMessage)
                {
                    strMsg += m.id + ":" + m.errorMessage + ";";
                }

                return strMsg;
            }
        }
    }
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public class failureMessage
    {
        /// <summary>
        /// 出现异常的业务对象唯一标识
        /// </summary>
        [JsonProperty]
        public string id { get; set; }

        /// <summary>
        /// 异常码
        /// </summary>
        [JsonProperty]
        public string errorCode { get; set; }

        /// <summary>
        /// 异常描述
        /// </summary>
        [JsonProperty]
        public string errorMessage { get; set; }

    }

    #region 处方退费通知

    [Serializable]
    public class ADD_CF_TFTZ : InterfaceBase
    {
        /// <summary>
        /// 处方号
        /// </summary>
        public string pn { get; set; }

        /// <summary>
        /// 处方退药信息
        /// </summary>
        public List<pd> pd { get; set; }
    }

    [Serializable]
    public class pd : InterfaceBase
    {
        /// <summary>
        /// 药品编码
        /// </summary>
        public string dc { get; set; }

        /// <summary>
        /// 三级包装退费数量
        /// </summary>
        public int tq { get; set; }

        /// <summary>
        /// 四级包装退费数量
        /// </summary>
        public int fq { get; set; }
    }

    #endregion
}
