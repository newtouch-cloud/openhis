using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Repository
{
    public class SysNationalityRepo : RepositoryBase<SysNationalityVEntity>, ISysNationalityRepo
    {
        public SysNationalityRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取有效国籍
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysNationalityVEntity> GetgjList()
        {
            var sql = "select gjId, gjCode,py,gjmc from [NewtouchHIS_Base]..V_S_xt_gj width(nolock) where zt = '1'";
            return this.FindList<SysNationalityVEntity>(sql);
        }

        /// <summary>
        /// 获取病人性质
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysBrxzVEntity> GetBRXZList(string orgId)
        {
            var sql = @"select 
[brxzbh], [OrganizeId], [brxz], [brxzmc], [py], [brxzlb], [ybjylx], [ybtsdy], [jyfyfw], [pzbz], [mzzybz], [bzs], [sqjmbz], [zhxz], [zt], [syybbf], [brxxpz], [fpdymb], [fpdyghmb], [CreatorCode], [CreateTime], [LastModifyTime], [LastModifierCode], [px], [bz], [ParentId], [insutype]
 from [NewtouchHIS_Sett]..xt_brxz width(nolock) where zt = '1' and  OrganizeId = @OrganizeId  ";

            var param = new DbParameter[]
           {
                new SqlParameter("@OrganizeId", orgId),
           };
            return this.FindList<SysBrxzVEntity>(sql,param);
        }
        /// <summary>
        /// 现金支付方式
        /// </summary>
        /// <returns></returns>
        public IList<SysCashPaymenVEntity> GetCashPay()
        {
            var sql = @" select [xjzffsbh], [xjzffs], [xjzffsmc], [zt], [zhxz], [CreatorCode], [CreateTime], [LastModifyTime], [LastModifierCode], [px]
 from [NewtouchHIS_Sett].[dbo].[xt_xjzffs] where zt = '1'";
            return this.FindList<SysCashPaymenVEntity>(sql);
        }
    }
}
