using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IDomainServices;

namespace Newtouch.Herp.DomainServices.Licence
{
    /// <summary>
    /// 证照维护
    /// </summary>
    public class LicLicenceDmnService : DmnServiceBase, ILicLicenceDmnService
    {
        public LicLicenceDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取证照类型
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IList<VLicLicenceTypeEntity> GetLicenceTypeList(Pagination pagination, string keyWord = "")
        {
            const string sql = @"
SELECT lit.Id, lit.typeName, lib.belonged, lit.zt, lit.CreateTime, lit.CreatorCode, lit.LastModifierCode, lit.LastModifyTime 
FROM dbo.lic_licenceType(NOLOCK) lit
INNER JOIN dbo.lic_licenceBelonged(NOLOCK) lib ON lib.Id=lit.belongedId AND lib.zt='1'
WHERE lit.Id=@keyWord OR ''=@keyWord
";
            var param = new DbParameter[]
            {
                new SqlParameter("@keyWord", keyWord??"")
            };
            return QueryWithPage<VLicLicenceTypeEntity>(sql, pagination, param);
        }

        /// <summary>
        /// 获取证照列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <param name="belongedId"></param>
        /// <param name="licenceTypeId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<VLicLicencesEntity> GetLicenceList(Pagination pagination, string keyWord, string belongedId, string licenceTypeId, string organizeId)
        {
            const string sql = @"
SELECT lic.Id, lit.typeName, lic.objectName, lic.licenceNo, lib.belonged, lic.qxrq, lic.sxrq, lic.fileUrl, lic.zt, lic.CreateTime, lic.CreatorCode, lic.LastModifierCode, lic.LastModifyTime 
FROM dbo.lic_licence(NOLOCK) lic
LEFT JOIN dbo.lic_licenceType(NOLOCK) lit ON lit.Id=lic.licenceTypeId AND lit.zt='1'
LEFT JOIN dbo.lic_licenceBelonged(NOLOCK) lib ON lib.Id=lic.belongedId AND lib.zt='1'
WHERE (lic.licenceNo LIKE '%'+@keyWord+'%' OR lic.objectName LIKE '%'+@keyWord+'%')
AND (lic.belongedId =@belongedId or ''=@belongedId)
AND (lic.licenceTypeId =@licenceTypeId or ''=@licenceTypeId)
AND lic.OrganizeId=@OrganizeId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@keyWord", keyWord??""),
                new SqlParameter("@belongedId", belongedId??""),
                new SqlParameter("@licenceTypeId", licenceTypeId??""),
                new SqlParameter("@OrganizeId", organizeId??"")
            };
            return QueryWithPage<VLicLicencesEntity>(sql, pagination, param);
        }
    }
}
