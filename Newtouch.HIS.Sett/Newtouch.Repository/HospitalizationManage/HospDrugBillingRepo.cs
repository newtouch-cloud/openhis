using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class HospDrugBillingRepo : RepositoryBase<HospDrugBillingEntity>, IHospDrugBillingRepo
    {
        public HospDrugBillingRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="hospItemFeeEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(HospDrugBillingEntity hospItemFeeEntity, int? keyValue)
        {
            if (keyValue > 0)
            {
                var entity = this.FindEntity(hospItemFeeEntity.jfbbh);
                entity.Modify(keyValue);
                this.Update(entity);
                hospItemFeeEntity.Create(true, EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("zy_ypjfb"));
                this.Insert(hospItemFeeEntity);
            }
            else
            {
                hospItemFeeEntity.Create(true, EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("zy_ypjfb"));
                this.Insert(hospItemFeeEntity);
            }
        }
        public void ExecPartialSettleFeeDetail(string zyh,string jsnm,string czlx)
        {
            if (czlx == "delete")
            {
                string sql = @"update  Drjk_zyfymxsc_input set zyh=@zyh,jsnm =NULL  where left(zyh,5)=@zyh and jsnm=@jsnm and jsnm is not null" +
                    "  update  Drjk_zyfymxsc_output set zyh=@zyh,jsnm =NULL  where left(zyh,5)=@zyh and jsnm=@jsnm and jsnm is not null";
                SqlParameter[] para ={
                new SqlParameter("@zyh",zyh ?? ""),
                 new SqlParameter("@jsnm",jsnm)
                };
                int i = this.ExecuteSqlCommand(sql, para);
            }
            else { 
            var zyh_Rd= zyh+"_t";
            string sql = @"update Drjk_zyfymxsc_input set zyh=@zyh_Rd,jsnm=@jsnm where zyh=@zyh and jsnm is  null"+
                    "  update Drjk_zyfymxsc_output set zyh=@zyh_Rd,jsnm=@jsnm  where zyh=@zyh and jsnm is  null";
            SqlParameter[] para ={
                new SqlParameter("@zyh",zyh ?? ""),
                 new SqlParameter("@zyh_Rd",zyh_Rd),
                 new SqlParameter("@jsnm",jsnm)
                };
            int i= this.ExecuteSqlCommand(sql, para);
            }
        }
        public void Updatezy_brxxexpand(string OrganizeId, string zyh)
        {
            try
            {
                string sql = @" exec Newtouch_CIS..usp_zy_brxxexpand_update @orgId,@zyh";
                SqlParameter[] para ={
                new SqlParameter("@orgId",OrganizeId),
                 new SqlParameter("@zyh",zyh)
                };
                int i = this.ExecuteSqlCommand(sql, para);
            }
            catch (Exception)
            {
            }
               
        }
        //住院补计费扣掉相应库存
        public void Updatezyaddfee(string OrganizeId, decimal sl,string yfbm,string ypdm)
        {
            try
            {
                string sql = @" update [NewtouchHIS_PDS].[dbo].[xt_yp_kcxx] set kcsl-=@sl   where ypdm=@ypdm and yfbmcode=@yfbmcode'
and organizeid=@orgId and zt='1'";
                SqlParameter[] para ={
                new SqlParameter("@orgId",OrganizeId),
                 new SqlParameter("@sl",sl),
                 new SqlParameter("@ypdm",ypdm),
                 new SqlParameter("@yfbmcode",yfbm)
                };
                int i = this.ExecuteSqlCommand(sql, para);
            }
            catch (Exception ex)
            {
            }

        }
    }
}


