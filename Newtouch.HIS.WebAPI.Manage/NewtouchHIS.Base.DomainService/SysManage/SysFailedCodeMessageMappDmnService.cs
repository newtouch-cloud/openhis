using Microsoft.Data.SqlClient;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Lib.Base.EnumExtend;
using System.Data.Common;
using System.Text;

namespace NewtouchHIS.Base.DomainService
{
    public class SysFailedCodeMessageMappDmnService : BaseDmnService<SysFailedCodeMessageMappEntity>, ISysFailedCodeMessageMappDmnService
    {
        public async Task<List<SysFailedCodeMessageMappEntity>> GetList(string orgId = null)
        {
            //顶级组织机构 匹配到的 Mapp
            var topOrgMappList = await FindAllWithAttr<SysFailedCodeMessageMappEntity>();
            if (string.IsNullOrWhiteSpace(orgId))
            {
                return (List<SysFailedCodeMessageMappEntity>)topOrgMappList.Select(p => p.OrganizeId = orgId);
            }
            return topOrgMappList;
        }
        
    }
}
