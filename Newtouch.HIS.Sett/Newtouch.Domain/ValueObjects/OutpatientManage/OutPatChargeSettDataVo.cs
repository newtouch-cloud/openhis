/**********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description： 门诊收费结算数据
// Author：HLF
// CreateDate： 2016/12/29 13:38:52 
//**********************************************************/

using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class OutPatChargeSettDataVo
    {

        //新增  门诊处方、门诊处方明细、门诊项目、
        //更新  当前发票号
        //新增  门诊结算、门诊结算明细、 门诊结算支付方式、门诊结算大类

        //门诊处方表 mz_cf
        //如果换方  处方表需要List
        public List<OutpatientPrescriptionEntity> mz_cf { get; set; }

        /// <summary>
        /// 门诊处方明细  mz_cfmx
        /// </summary>
        public List<OutpatientPrescriptionDetailEntity> mz_cfmxList { get; set; }

        //mz_xm
        /// <summary>
        /// 门诊项目
        /// </summary>
        public List<OutpatientItemEntity> mz_xmList { get; set; }


        //update 财务发票 更新当前发票号 //cw_fp
        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 门诊结算 mz_js
        /// </summary>
        public OutpatientSettlementEntity mz_js { get; set; }

        /// <summary>
        /// 门诊结算支付方式  mz_jszffs
        /// </summary>
        public OutpatientSettlementPaymentModelEntity mz_jszffs { get; set; }

        /// <summary>
        /// 门诊结算明细 mz_jsmx
        /// </summary>
        public List<OutpatientSettlementDetailEntity> mz_jsmxList { get; set; }

        /// <summary>
        /// 门诊结算大类 mz_jsdl
        /// </summary> 
        public List<OutpatientSettlementCategoryEntity> mz_jsdlList { get; set; }

    }

    public class OutPatChargeSettInAccDataVo
    {

        //新增  门诊结算、门诊结算明细、 门诊结算支付方式、门诊结算大类

        //门诊处方表 mz_cf
        //如果换方  处方表需要List
        public List<OutpatientPrescriptionEntity> mz_cf { get; set; }

        /// <summary>
        /// 门诊处方明细  mz_cfmx
        /// </summary>
        public List<OutpatientPrescriptionDetailEntity> mz_cfmxList { get; set; }

        //mz_xm
        /// <summary>
        /// 门诊项目
        /// </summary>
        public List<OutpatientItemEntity> mz_xmList { get; set; }


        /// <summary>
        /// 门诊结算 mz_js
        /// </summary>
        public List<OutpatientSettlementEntity> mz_js { get; set; }

        /// <summary>
        /// 门诊结算支付方式  mz_jszffs
        /// </summary>
        public List<OutpatientSettlementPaymentModelEntity> mz_jszffs { get; set; }

        /// <summary>
        /// 门诊结算明细 mz_jsmx
        /// </summary>
        public List<OutpatientSettlementDetailEntity> mz_jsmxList { get; set; }

        /// <summary>
        /// 门诊结算大类 mz_jsdl
        /// </summary> 
        public List<OutpatientSettlementCategoryEntity> mz_jsdlList { get; set; }

    }

    #region 门诊收费 Vision-1.3 门诊记账的第三个版本
    public class OutPatSettInAccDataVo
    {

        //新增  门诊处方 门诊处方明细 门诊结算 门诊结算明细 门诊项目 门诊记账计划 门诊记账计划明细

        //门诊处方表 mz_cf
        //如果换方  处方表需要List
        public List<OutpatientPrescriptionEntity> mz_cf { get; set; }

        /// <summary>
        /// 门诊处方明细  mz_cfmx
        /// </summary>
        public List<OutpatientPrescriptionDetailEntity> mz_cfmxList { get; set; }

        //mz_xm
        /// <summary>
        /// 门诊项目
        /// </summary>
        public List<OutpatientItemEntity> mz_xmList { get; set; }


        /// <summary>
        /// 门诊结算 mz_js
        /// </summary>
        public List<OutpatientSettlementEntity> mz_js { get; set; }

        /// <summary>
        /// 门诊结算明细 mz_jsmx
        /// </summary>
        public List<OutpatientSettlementDetailEntity> mz_jsmxList { get; set; }
        public OutpatientAccountEntity mz_jzjh { get; set; }
        public List<OutpatientAccountDetailEntity> mz_jzjhmx { get; set; }

    }

    public class updatePatSettInAccDataVo
    {

        //更改  门诊处方 门诊处方明细 门诊结算 门诊结算明细 门诊项目 门诊记账计划 门诊记账计划明细

        //门诊处方表 mz_cf
        //如果换方  处方表需要List
        public Dictionary<OutpatientPrescriptionEntity, OutpatientPrescriptionEntity> mz_cf { get; set; }

        /// <summary>
        /// 门诊处方明细  mz_cfmx
        /// </summary>
        public Dictionary<OutpatientPrescriptionDetailEntity, OutpatientPrescriptionDetailEntity> mz_cfmxList { get; set; }

        //mz_xm
        /// <summary>
        /// 门诊项目
        /// </summary>
        public Dictionary<OutpatientItemEntity, OutpatientItemEntity> mz_xmList { get; set; }


        /// <summary>
        /// 门诊结算 mz_js
        /// </summary>
        public Dictionary<OutpatientSettlementEntity, OutpatientSettlementEntity> mz_js { get; set; }

        /// <summary>
        /// 门诊结算明细 mz_jsmx
        /// </summary>
        public Dictionary<OutpatientSettlementDetailEntity, OutpatientSettlementDetailEntity> mz_jsmxList { get; set; }
        public Dictionary<OutpatientAccountEntity, OutpatientAccountEntity> mz_jzjh { get; set; }
        public Dictionary<OutpatientAccountDetailEntity, OutpatientAccountDetailEntity> mz_jzjhmx { get; set; }
    }

    public class DelPatSettInAccDataVo
    {

        //更改  门诊处方 门诊处方明细 门诊结算 门诊结算明细 门诊项目 门诊记账计划 门诊记账计划明细

        //门诊处方表 mz_cf
        //如果换方  处方表需要List
        public List<int> mz_cf { get; set; }

        /// <summary>
        /// 门诊处方明细  mz_cfmx
        /// </summary>
        public List<int> mz_cfmxList { get; set; }

        //mz_xm
        /// <summary>
        /// 门诊项目
        /// </summary>
        public List<int> mz_xmList { get; set; }


        /// <summary>
        /// 门诊结算 mz_js
        /// </summary>
        public List<int> mz_js { get; set; }

        /// <summary>
        /// 门诊结算明细 mz_jsmx
        /// </summary>
        public List<int> mz_jsmxList { get; set; }
        public List<string> mz_jzjh { get; set; }
        public List<string> mz_jzjhmx { get; set; }
    }
    #endregion

    #region  optima记账 Vision-1.2 门诊记账的第二个版本（包括门诊记账）
    /// <summary>
    /// 门诊
    /// </summary>
    public class SettInAccOutpatDataVo
    {
        //mz_xm
        /// <summary>
        /// 门诊项目
        /// </summary>
        public List<OutpatientItemEntity> mz_xmList { get; set; }

        public List<OutpatientPrescriptionEntity> mz_cf { get; set; }

        /// <summary>
        /// 门诊处方明细  mz_cfmx
        /// </summary>
        public List<OutpatientPrescriptionDetailEntity> mz_cfmxList { get; set; }
    }

    /// <summary>
    /// 修改门诊记账
    /// </summary>
    public class updateInAccOutpatVo
    {
        //mz_xm
        /// <summary>
        /// 门诊项目
        /// </summary>
        public Dictionary<OutpatientItemEntity, OutpatientItemEntity> mz_xmList { get; set; }

        public Dictionary<OutpatientPrescriptionEntity, OutpatientPrescriptionEntity> mz_cf { get; set; }

        /// <summary>
        /// 门诊处方明细  mz_cfmx
        /// </summary>
        public Dictionary<OutpatientPrescriptionDetailEntity, OutpatientPrescriptionDetailEntity> mz_cfmxList { get; set; }
    }

    /// <summary>
    /// 删除门诊
    /// </summary>
    public class delInAccOutpatVo
    {
        //mz_xm
        /// <summary>
        /// 门诊项目
        /// </summary>
        public List<int> mz_xmList { get; set; }

        public List<int> mz_cf { get; set; }

        /// <summary>
        /// 门诊处方明细  mz_cfmx
        /// </summary>
        public List<int> mz_cfmxList { get; set; }
    }
    /// <summary>
    /// 住院
    /// </summary>
    public class SettInAccHospatDataVo
    {
        /// <summary>
        /// 项目计费表
        /// </summary>
        public List<HospItemBillingEntity> zy_xmjfbList { get; set; }

        /// <summary>
        /// 药品计费表
        /// </summary>
        public List<HospDrugBillingEntity> zy_ypjfbList { get; set; }
    }


    /// <summary>
    /// 修改住院记账
    /// </summary>
    public class updateInAccHospatVo
    {
        /// <summary>
        /// 项目计费表
        /// </summary>
        public Dictionary<HospItemBillingEntity, HospItemBillingEntity> zy_xmjfbList { get; set; }

        /// <summary>
        /// 药品计费表
        /// </summary>
        public Dictionary<HospDrugBillingEntity, HospDrugBillingEntity> zy_ypjfbList { get; set; }
    }

    /// <summary>
    /// 删除住院记账
    /// </summary>
    public class delInAccHospatVo
    {
        /// <summary>
        /// 项目计费表
        /// </summary>
        public List<string> zy_xmjfbList { get; set; }

        /// <summary>
        /// 药品计费表
        /// </summary>
        public List<string> zy_ypjfbList { get; set; }
    }
    #endregion

    #region 门诊收费 Vision-1.4 门诊记账的第四个版本 
    public class OutPatSettInAccDataVo4
    {

        //新增  门诊结算 门诊结算明细 门诊项目 门诊记账计划 门诊记账计划明细

        //mz_xm
        /// <summary>
        /// 门诊项目
        /// </summary>
        public List<OutpatientItemEntity> mz_xmList { get; set; }


        /// <summary>
        /// 门诊结算 mz_js
        /// </summary>
        public List<OutpatientSettlementEntity> mz_js { get; set; }

        /// <summary>
        /// 门诊结算明细 mz_jsmx
        /// </summary>
        public List<OutpatientSettlementDetailEntity> mz_jsmxList { get; set; }
        public OutpatientAccountEntity mz_jzjh { get; set; }
        public List<OutpatientAccountDetailEntity> mz_jzjhmx { get; set; }

    }

    public class updatePatSettInAccDataVo4
    {

        //更改 门诊结算 门诊结算明细 门诊项目 门诊记账计划 门诊记账计划明细

        //mz_xm
        /// <summary>
        /// 门诊项目
        /// </summary>
        public Dictionary<OutpatientItemEntity, OutpatientItemEntity> mz_xmList { get; set; }


        /// <summary>
        /// 门诊结算 mz_js
        /// </summary>
        public Dictionary<OutpatientSettlementEntity, OutpatientSettlementEntity> mz_js { get; set; }

        /// <summary>
        /// 门诊结算明细 mz_jsmx
        /// </summary>
        public Dictionary<OutpatientSettlementDetailEntity, OutpatientSettlementDetailEntity> mz_jsmxList { get; set; }
        public Dictionary<OutpatientAccountEntity, OutpatientAccountEntity> mz_jzjh { get; set; }
        public Dictionary<OutpatientAccountDetailEntity, OutpatientAccountDetailEntity> mz_jzjhmx { get; set; }
    }
    #endregion
}
