using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Proxy.guian.DTO.S25;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 门诊的一些通用方法
    /// 一定是不区分版本
    /// </summary>
    public interface IOutPatientUniversalDmnService
    {
        /// <summary>
        /// 提交明细（重要：如果是向处方追加或作废明细，线下意义这是在变更这张处方，医生一般不这么做）（处方的话更新主表信息）（请提前构造好其请求参数）（根据明细的主键xmnm、cfmxId值来判断这是新增，还是修改记录）（当是新增，根据cfnm字段，又分新增明细及处方，还是向已有处方追加明细）（修改的话是直接更新数据库明细记录/此版本暂不支持，否则请去退费）
        /// </summary>
        /// <param name="orgId">医疗机构Id</param>
        /// <param name="changedCfnmList">返回用的</param>
        /// <param name="changedXmnmList">返回用的（如果项目不关联处方）</param>
        /// <param name="updateSkipSettledCfhList">（这是一个返回）更新已结处方被跳过的cfh List</param>
        /// <param name="mzh">如果需要新增的话，给你准备好</param>
        /// <param name="ys">如果需要新增的话，给你准备好（未指定时去挂号的ys）</param>
        /// <param name="ItemsGroupBOList">计费信息分组（新增处方/新增处方明细/新增项目明细）处方信息/处方明细信息/项目信息</param>
        /// <param name="settledUpdateForbidden">已结时 是否禁止更新</param>
        /// <param name="settledUpdateSkip">（已结时禁止更新）检查是否Skip，true不抛异常跳过，否则抛异常</param>
        /// <param name="cfly">处方来源（0 收费处 1 医生站）</param>
        void SubmitItems(
            string orgId
            , out IList<int> changedCfnmList, out IList<int> changedXmnmList
            , out IList<string> updateSkipSettledCfhList
            , string mzh = null, string ys = null
            , IList<FeeItemsGroupedBO> ItemsGroupBOList = null
            , bool settledUpdateForbidden = true
            , bool settledUpdateSkip = false
            , string cfly = null);

        /// <summary>
        /// 更新门诊处方/门诊项目的结算信息（更新结算信息）（这个cfnm可能已作废，目的可能是也作废结算信息）（调用此方法前，请先更新好mz_cfmx、mz_xm，且已持久化到数据库中）根据cfnm查出所有的相关表mz_js mz_jsmx mz_cf mz_cfmx mz_xm，根据mz_cfmx、mz_xm的正确数据更新其他表（可能会新增mz_jsmx）（可能会新增mz_js）（check mz_xm、mz_cf zt 0，对应mz_jsmx 也要更新）（多个mz_cf可以对应一个结算）（但一个mz_cf不能对应多个结算）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="settlementAddBO"></param>
        /// <param name="feeRelated">金额及支付信息</param>
        /// <param name="ybfeeRelated">医保相关费用</param>
        /// <returns>返回新生成的结算内码List</returns>
        IList<int> AddSettlement(string orgId, string mzh
            , OutpatientSettlementAddBO settlementAddBO
            , OutpatientSettFeeRelatedDTO feeRelated = null
            , CQMzjs05Dto ybfeeRelated = null
            , S25ResponseDTO xnhybfeeRelated = null
            , string outTradeNo = null);

        /// <summary>
        /// 门诊退费（不用关联门诊处方、项目）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="jsnm"></param>
        /// <param name="tjsxmDict">退结算明细List jsmxnm:退数量 退数量 正数</param>
        /// <param name="expectedTmxZje">预期退费项目金额合计（单价*退数量），防止过程中数据发生变更</param>
        /// <param name="newfph">新结算发票号</param>
        /// <param name="newJszbInfo">生成的新结算结算信息</param>
        /// <param name="outTradeNo">原支付交易号</param>
        /// <param name="refundAmount">退现金金额（现金、支付宝、微信等）</param>
        /// <param name="hcybjsh">退费.医保交易.红冲结算号</param>
        /// <param name="newwjybjsh">退费.医保交易.新的结算号</param>
        /// <param name="hcybfeeRelated">红冲结算 的 结算医保金额相关</param>
        /// <param name="newybfeeRelated">退费.医保交易.新的结算 结算医保返回费用信息</param>
        /// <param name="guianMztfProDto"></param>
        /// <param name="s25ResponseDto">贵安新农合医保使用，结算返回结果</param>
        /// <param name="outpId">贵安新农合医保使用，门诊补偿序号</param>
        void RefundSettlement(string orgId, int jsnm
            , Dictionary<int, decimal> tjsxmDict, decimal expectedTmxZje
            , string newfph
            , out object newJszbInfo, out string outTradeNo, out decimal refundAmount, string hcybjsh = null, string newwjybjsh = null
            , CQMzjs05Dto hcybfeeRelated = null
            , OutpatientSettYbFeeRelatedDTO newybfeeRelated = null
            , CQMzjs05Dto guiannewybfeeRelated = null
            , GuiAnMztfProDto guianMztfProDto = null
            , S25ResponseDTO s25ResponseDto = null
            , string outpId = "");

        /// <summary>
        /// 计划退费全停
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="jsnm"></param>
        void AccountPlanRefundAll(string orgId, int jsnm);

        /// <summary>
        /// 作废处方（已结一定是不能作废）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cfnmList">处方内码List</param>
        /// <param name="cfhList">处方号List</param>
        /// <param name="settledUpdateForbidden">已结时 是否禁止更新</param>
        void CancelPrescription(string orgId
            , IList<int> cfnmList = null
            , IList<string> cfhList = null
            , bool settledUpdateForbidden = true);

        /// <summary>
        /// 根据门诊号 等 查询处方号
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="cfzt"></param>
        /// <param name="cfly"></param>
        /// <param name="cfhs">多个处方号，逗号分割</param>
        /// <returns></returns>
        IList<OutpatientPrescriptionEntity> GetValidCfListByMzh(string orgId, string mzh
            , string cfzt = null, string cfly = null, string cfhs = null);

        /// <summary>
        /// 更新欠费预结的记录至已收费
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="jsnm"></param>
        /// <param name="feeRelated">金额及支付信息</param>
        /// <param name="sfrq"></param>
        /// <param name="fph"></param>
        void UpdateArrearageSettlement(string orgId, int jsnm
            , OutpatientSettFeeRelatedDTO feeRelated
            , DateTime sfrq
            , string fph = null);

        /// <summary>
        /// 获取门诊结算/退费 患者出院信息（医保结算号、社保编号、就诊原因、科室、医生、诊断）
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        OutPatChargePatientInfoVO GetSettPatientInfo(string mzh, string orgId);
        /// <summary>
        /// 获取收费结算 组套明细
        /// </summary>
        /// <param name="cfnms"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<RefundZtDot> RefundZtDetail(string cfnms, string orgId);

        CheckApplicationfromDTO pushApplicationform(BasicInfoDto2018 bacDto, int cfnm, string orgid, string typezt);

        CheckApplicationfromDTO retApplicationform(string mzh, string cfnmList, string orgid, string typezt);

        void UpdateYPcfxm(List<OutGridInfoDto2018> deletelist, string orgId, string usercode);

        int pushApplicationformRef(string cfh, string orgid,int sqdzt);
    }
}
