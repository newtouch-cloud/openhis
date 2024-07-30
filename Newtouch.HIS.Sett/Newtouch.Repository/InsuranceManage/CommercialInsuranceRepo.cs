using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository.InsuranceManage;
using Newtouch.Infrastructure;
using System.Linq;
using Newtouch.HIS.Domain.ValueObjects.InsuranceManage;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;

namespace Newtouch.HIS.Repository.InsuranceManage
{
    public class CommercialInsuranceRepo : RepositoryBase<CommercialInsuranceEntity>, ICommercialInsuranceRepo
    {
        public CommercialInsuranceRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public CInsuranceInfoInGrid GetForm(string keyValue, string orgId)
        {
            var sqlstr = new StringBuilder();
            sqlstr.Append(@"SELECT DISTINCT
                                * ,
		                        kbxmmc=STUFF(( SELECT DISTINCT
                                                        ',' + CAST([dlCode] AS NVARCHAR(MAX))
                                                 FROM   [dbo].[xt_sbkbxm] kb
                                                        LEFT JOIN NewtouchHIS_Base..xt_sfdl dl ON dl.dlCode = kb.kbxm
                                                                                      AND dl.OrganizeId = @orgId
                                                 WHERE  ( kb.sybxId = bx.Id
                                                          AND kb.OrganizeId =@orgId
                                                        )
                                               FOR
                                                 XML PATH('')
                                               ), 1, 1, ''),
                                 sfxmList= STUFF(( SELECT DISTINCT
                                                        ',' + CAST([dlmc] AS NVARCHAR(MAX))
                                                 FROM   [dbo].[xt_sbkbxm] kb
                                                        LEFT JOIN NewtouchHIS_Base..xt_sfdl dl ON dl.dlCode =  kb.kbxm
                                                                                      AND dl.OrganizeId = @orgId
                                                 WHERE  ( kb.sybxId = bx.Id
                                                          AND kb.OrganizeId = @orgId
                                                        )
                                               FOR
                                                 XML PATH('')
                                               ), 1, 1, '')
                        FROM    [dbo].[xt_sybx] bx
                        WHERE   bx.OrganizeId = @orgId
                                AND bx.Id = @Id");
            DbParameter[] par = {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@Id",keyValue)
            };
            return FindList<CInsuranceInfoInGrid>(sqlstr.ToString(), par).FirstOrDefault();
        }

        public List<CInsuranceInfoInGrid> GetListJson(string orgId, string code, string engName)
        {
            var sqlstr = new StringBuilder();
            sqlstr.Append(@"SELECT DISTINCT
                            bx.Id ,
                            * ,
                            kbxmmc = STUFF(( SELECT DISTINCT
                                                    ',' + CAST([dlmc] AS NVARCHAR(MAX))
                                             FROM   [dbo].[xt_sbkbxm] kb
                                                    LEFT JOIN NewtouchHIS_Base..xt_sfdl dl ON dl.dlCode = kb.kbxm
                                                                                  AND dl.OrganizeId = @orgId
                                             WHERE  ( kb.sybxId = bx.Id
                                                      AND kb.OrganizeId = @orgId
                                                    )
                                           FOR
                                             XML PATH('')
                                           ), 1, 1, '')
                    FROM    [dbo].[xt_sybx] bx
                    WHERE   bx.OrganizeId = @orgId
                            AND ( bx.bxcode LIKE @code
                                  OR bx.NAME LIKE @code
                                  OR @code = ''
                                )
                            AND ( bx.englishName LIKE @name
                                  OR @name = ''
                                )");
            DbParameter[] par = {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@code","%"+(code??"")+"%"),
                new SqlParameter("@name","%"+(engName??"")+"%")
            };
            return FindList<CInsuranceInfoInGrid>(sqlstr.ToString(), par);
        }

        public void DeleteForm(string keyValue)
        {
            Delete(p => p.Id == keyValue);
        }

        public List<CommercialInsuranceEntity> GetList(string orgId)
        {
            var list = this.IQueryable().Where(p => p.zt == "1" && p.OrganizeId == orgId).ToList().Select(p => new CommercialInsuranceEntity()
            {
                bxCode = p.bxCode,
                Name = p.Name,
                EnglishName = p.EnglishName
            }).ToList();
            return list;
        }
    }
}
