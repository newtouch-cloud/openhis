using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    public class SysPatientChargeWaiverRepo : RepositoryBase<SysPatientChargeWaiverEntity>, ISysPatientChargeWaiverRepo
    {
        public SysPatientChargeWaiverRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <returns></returns>
        public List<SysPatientChargeWaiverEntity> GetEffectiveList(int keyValue)
        {
            return null;
        }
        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysPatientChargeWaiverEntity GetForm(int keyValue, string orgId)
        {
            return this.IQueryable().Where(a => a.OrganizeId == orgId && a.brsfjmbh == keyValue).FirstOrDefault();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(int keyValue, string orgId)
        {
            this.Delete(a => a.brsfjmbh == keyValue && a.OrganizeId == orgId);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sysPatiChargeWaiverEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysPatientChargeWaiverEntity sysPatiChargeWaiverEntity, int? keyValue, string orgId)
        {
            if (keyValue > 0)
            {
                sysPatiChargeWaiverEntity.Modify(keyValue);
                this.Update(sysPatiChargeWaiverEntity);
            }
            else
            {
                sysPatiChargeWaiverEntity.OrganizeId = orgId;
                sysPatiChargeWaiverEntity.Create(true, EFDBBaseFuncHelper.GetInstance().GetNewPrimaryKeyInt(SysPatientChargeWaiverEntity.GetTableName()));
                this.Insert(sysPatiChargeWaiverEntity);
            }

        }

        ///// <summary>
        ///// 根据首拼查询收费项目
        ///// </summary>
        ///// <param name="parm"></param>
        ///// <returns></returns>
        //public decimal GetXT_brsfjm(string strWhere)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    sql.Append("select top 1 * from [dbo].[xt_brsfjm] where zt=1");
        //    sql.Append(strWhere);
        //    var data = this.FindList<SysPatientChargeWaiverEntity>(sql.ToString());
        //    if (data != null && data.Count > 0)
        //    {
        //        return data[0].jmbl;
        //    }
        //    return 0;
        //}

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
        public decimal Get_Calcjm(string parmBrxz, string parmDl, string parmSfxm, decimal parmJe, out decimal outJmbl, out decimal outJmje, string orgId)
        {
            //减免比例，减免金额，减免后的金额
            decimal jmbl = 0, jmje = 0, count = 0;

            var entity = IQueryable().FirstOrDefault(p => p.brxz == parmBrxz && p.sfxm == parmSfxm && p.zt == "1" && p.OrganizeId == orgId);
            if (entity != null && entity.jmbl != 0)
            {
                jmbl = entity.jmbl;
                if (jmbl < 0)//定额自负
                {
                    count = parmJe + jmbl;
                    jmje = Math.Abs(jmbl);
                }
                else
                {
                    jmje = parmJe * jmbl;
                    count = parmJe - jmje;
                }
            }
            else
            {
                entity = IQueryable().FirstOrDefault(p => p.dl == parmDl && p.brxz == parmBrxz && p.sfxm == parmSfxm && p.zt == "1" && p.OrganizeId == orgId);
                if (entity != null && entity.jmbl != 0)
                {
                    jmbl = entity.jmbl;
                    if (jmbl < 0)//定额自负
                    {
                        count = Convert.ToDecimal(parmJe + jmbl);
                        jmje = Math.Abs(jmbl);
                    }
                    else
                    {
                        jmje = Convert.ToDecimal(parmJe * jmbl);
                        count = parmJe - jmje;
                    }
                }
            }
            outJmbl = jmbl;
            outJmje = jmje;
            return count;
        }
    }
}
