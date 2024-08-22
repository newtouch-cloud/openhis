using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysChargeTemplateItemMappRepo : RepositoryBase<SysChargeTemplateItemMappEntity>, ISysChargeTemplateItemMappRepo
    {
        public SysChargeTemplateItemMappRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }


        public IList<MbmxsfxmVO> getmbmx(string sfmb,string orgId)
        {
            string sql = @"select a.sfmbbh,a.sfmb,a.sfmbmc,c.sfxmcode,b.sl,b.dj,c.dw,c.sfdlcode from xt_sfmb a
left join xt_sfmbxm b on a.sfmbbh=b.sfmbbh and a.OrganizeId=b.OrganizeId and b.zt='1'
inner join [NewtouchHIS_Base]..V_S_xt_sfxm c on b.sfxm=c.sfxmcode and b.OrganizeId=c.OrganizeId 
where a.zt='1' and  a.sfmb=@sfmb
and a.OrganizeId=@orgId";
            
            return this.FindList<MbmxsfxmVO>(sql, new[] { new SqlParameter("@sfmb", sfmb)
                ,new SqlParameter("@orgId", orgId)});
        }

    }
}


