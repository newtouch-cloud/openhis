using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.NeiMengGu.Input
{
    /// <summary>
    /// 若定点医药机构没有通过终端完成医保移动支付，需要调用此接口在终端上显示结算结果。若定点医药机构通过终端完成医保移动支付，则不需要调用此接口入參
    /// </summary>
    public class SettleNotifyInput : InputBase 
    {
        /// <summary>
        /// 机构 ID 字符 40 N 医保定点机构代码
        /// </summary>
        public string orgId { get; set; }
        /// <summary>
        /// 需要传核身接口或者电子凭证接口里的流水号（因为需要将核身结果与医保结算结果相对应。
        /// </summary>
        public string outBizNo { get; set; }
        /// <summary>
        /// 医保/自费结算状态 字符    64 N    SUCCESS:结算成功 | FAIL:结算失败
        /// </summary>
        public string medicalSettleState { get; set; }

        /// <summary>
        /// 实人认证业务流水号 字符 64 Y 医保综合服务终端返回的授权码
        /// </summary>
        public string authNo { get; set; }

        /// <summary>
        /// 收款员编号 字符 20 N 收款员编号
        /// </summary>
        public string operatorId { get; set; }

        /// <summary>
        /// 收款员姓名 字符 30 N 收款员姓名
        /// </summary>
        public string operatorName { get; set; }

        /// <summary>
        /// totalFee 总费用 字符 64 N
        /// </summary>
        public string totalFee { get; set; }

        /// <summary>
        /// bizType 业务场景    字符 30   N   register:挂号窗口 |settle:诊间
        /// </summary>
        public string bizType { get; set; }

        /// <summary>
        /// idNo 身份证 字符 64 N
        /// </summary>
        public string idNo { get; set; }

        /// <summary>
        /// userName 姓名 字符 64 N
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// setlTime 结算时间 字符 64 N
        /// </summary>
        public string setlTime { get; set; }

        /// <summary>
        /// hospitalName 医院名称 字符 64 N
        /// </summary>
        public string hospitalName { get; set; }

        /// <summary>
        /// 医保科室编号 字符 20 N 医保科室编号
        /// </summary>
        public string officeId { get; set; }

        /// <summary>
        /// officeName 科室名称 字符 30 N 科室名称
        /// </summary>
        public string officeName { get; set; }

        /// <summary>
        /// doctorName 医生 字符 64 N
        /// </summary>
        public string doctorName { get; set; }


        //////////////以下当 medicalSettleState=SUCCESS 时，需要传以下值（自费不需要传///////////////////////

        /// <summary>
        /// medicalSettleNo 医保单据流水号 字符 64 N
        /// </summary>
        public string medicalSettleNo { get; set; }
        /// <summary>
        /// ownAmt 自费费用 字符 64 N
        /// </summary>
        public string ownAmt { get; set; }
        /// <summary>
        /// hifAmt 医保报销费用 字符 64 Y
        /// </summary>
        public string hifAmt { get; set; }
        /// <summary>
        /// acctAmt 个人帐户支出 字符 64 Y
        /// </summary>
        public string acctAmt { get; set; }
        /// <summary>
        /// hifpAmt 统筹基金支出 字符 64 Y
        /// </summary>
        public string hifpAmt { get; set; }
        /// <summary>
        ///hifmiAmt 大额医疗保险支出 字符 64 Y
        /// </summary>
        public string hifmiAmt { get; set; }
        /// <summary>
        /// cvlservAmt 公务员补助 字符 64 Y
        /// </summary>
        public string cvlservAmt { get; set; }
        /// <summary>
        /// maAmt 医疗救助 字符 64 Y
        /// </summary>
        public string maAmt { get; set; }
        /// <summary>
        /// hosPreAmt 单病种定点医疗机构垫支 字符 64 Y
        /// </summary>
        public string hosPreAmt { get; set; }
        /// <summary>
        /// medOverLmtAmt 药品超标扣款金额 字符 64 Y
        /// </summary>
        public string medOverLmtAmt { get; set; }
        /// <summary>
        /// mafAmt 扶贫救助 字符 64 Y
        /// </summary>
        public string mafAmt { get; set; }
        /// <summary>
        /// cvlservDedcAmt 历史起付公务员返还 字符 64 Y
        /// </summary>
        public string cvlservDedcAmt { get; set; }
        /// <summary>
        /// balance 帐户余额 字符 64 N
        /// </summary>
        public string balance { get; set; }
        /// <summary>
        /// drugList 药品明细 N JSON 对象字符串
        /// </summary>
        public string drugList { get; set; }
    }


    public class DrugList
    {
        /// <summary>
        /// ITEM_NO 项目编号 字符 40 N 对照医保项目编码
        /// </summary>
        public string ITEM_NO { get; set; }

        /// <summary>
        /// ITEMNAME 项目名称 字符 100 N
        /// </summary>
        public string ITEMNAME { get; set; }

        /// <summary>
        /// HI_ITEM 是否医保项目 字符 20 N Y 是 N 否
        /// </summary>
        public string HI_ITEM { get; set; }
        /// <summary>
        /// PRIC 项目单价 字符 20 N
        /// </summary>
        public string PRIC { get; set; }
        /// <summary>
        ///ITEM_CNT 项目数量 字符 10 N
        /// </summary>
        public string ITEM_CNT { get; set; }
        /// <summary>
        /// ITEM_AMT 项目金额 字符 20 N
        /// </summary>
        public string ITEM_AMT { get; set; }
        /// <summary>
        /// DRUG_FRQU 药品频率 字符 20 N
        /// </summary>
        public string DRUG_FRQU { get; set; }
        /// <summary>
        /// DRUG_DOS 药品用量 字符 20 N
        /// </summary>
        public string DRUG_DOS { get; set; }
    }
}
