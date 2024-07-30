using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.DomainServices.PharmacyDrugStorage
{
    public class SysPharmacyWindowDmnService : DmnServiceBase, ISysPharmacyWindowDmnService
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysPharmacyWindowDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }

        public SysPharmacyWindowVO GetFormJson(string orgId, int? yfckId)
        {
            var strSql = @"select * from [NewtouchHIS_Base].dbo.xt_yfck where OrganizeId=@orgId";
            var parList = new List<SqlParameter>() { };
            parList.Add(new SqlParameter("@orgId", orgId ?? ""));
            if (yfckId != 0)
            {
                strSql += " and yfckId = @yfckId";
                parList.Add(new SqlParameter("@yfckId", yfckId));
            }
            return this.FirstOrDefault<SysPharmacyWindowVO>(strSql, parList.ToArray());
        }


        public void submitForm(SysPharmacyWindowVO entity, int? keyValue)
        {

            if (keyValue.HasValue && keyValue.Value > 0)
            {
                var checkSql = "select * from [NewtouchHIS_Base].dbo.xt_yfck where zt=1 and OrganizeId=@organizeId  and yfckId = @yfckId and yfckCode=@yfckCode";
                var parList = new List<SqlParameter>
                {
                    new SqlParameter("@organizeId", entity.OrganizeId ?? ""),
                    new SqlParameter("@yfckId", keyValue ),
                    new SqlParameter("@yfckCode", entity.yfckCode ?? ""),
                };
                var checkList = FindList<SysPharmacyWindowVO>(checkSql, parList.ToArray());

                if (checkList.Count > 0)
                {
                    throw new FailedException("窗口编号不能重复！");
                }
                try
                {
                    using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
                    {
                        var strSql = new StringBuilder();
                        strSql.Append(@"  UPDATE [NewtouchHIS_Base].dbo.xt_yfck
SET yfckCode=@yfckCode,yfckmc=@yfckmc,OrganizeId=@OrganizeId,pfyms=@pfyms,kqbz=@kqbz,LastModifyTime=@LastModifyTime,LastModifierCode=@LastModifierCode,zt=@zt,px=@px
where yfckId=@yfckId");
                        var paraList = new DbParameter[]
                        {
                        new SqlParameter("@yfckId", keyValue),
                        new SqlParameter("@yfckCode", entity.yfckCode),
                        new SqlParameter("@yfckmc", entity.yfckmc),
                        new SqlParameter("@OrganizeId", entity.OrganizeId),
                        new SqlParameter("@pfyms", entity.pfyms==null?"":entity.pfyms),
                        new SqlParameter("@kqbz", entity.kqbz==null?"":entity.kqbz),
                        new SqlParameter("@LastModifyTime", DateTime.Now),
                        new SqlParameter("@LastModifierCode", OperatorProvider.GetCurrent().UserCode),
                        new SqlParameter("@zt", entity.zt),
                        new SqlParameter("@px", entity.px==null?0:entity.px),
                        };
                        db.ExecuteSqlCommand(strSql.ToString(), paraList);
                        db.Commit();
                    }
                }
                catch (Exception ex)
                {
                    throw new FailedException(ex.Message);
                }
            }
            else
            {
                var checkSql = "select * from [NewtouchHIS_Base].dbo.xt_yfck where zt=1 and OrganizeId=@organizeId  and yfckCode=@yfckCode ";
                var parList = new List<SqlParameter>
                {
                    new SqlParameter("@organizeId", entity.OrganizeId ?? ""),
                    new SqlParameter("@yfckCode", entity.yfckCode ?? ""),
                };
                var checkList = FindList<SysPharmacyWindowVO>(checkSql, parList.ToArray());
                if (checkList.Count > 0)
                {
                    throw new FailedException("窗口编号不能重复！");
                }
                try
                {
                    using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
                    {
                        var strSql = new StringBuilder();
                        strSql.Append(@"insert into [NewtouchHIS_Base].dbo.xt_yfck
                                  (yfckCode,yfckmc,OrganizeId,yfbmCode,pfyms,kqbz,CreatorCode,CreateTime,LastModifyTime,LastModifierCode,zt,px)
                                  values (@yfckCode,@yfckmc,@OrganizeId,null,@pfyms,@kqbz,@CreatorCode,@CreateTime,null,null,@zt,@px);");
                        var paraList = new DbParameter[]
                        {
                            
                        new SqlParameter("@yfckCode", entity.yfckCode),
                        new SqlParameter("@yfckmc", entity.yfckmc),
                        new SqlParameter("@OrganizeId", entity.OrganizeId),
                        new SqlParameter("@pfyms", entity.pfyms==null?"":entity.pfyms),
                        new SqlParameter("@kqbz", entity.kqbz==null?"":entity.kqbz),
                        new SqlParameter("@CreateTime", DateTime.Now),
                        new SqlParameter("@CreatorCode", OperatorProvider.GetCurrent().UserCode),
                        new SqlParameter("@zt", entity.zt),
                        new SqlParameter("@px", entity.px==null?0:entity.px),
                        };
                        db.ExecuteSqlCommand(strSql.ToString(), paraList);
                        db.Commit();
                    }
                }
                catch (Exception ex)
                {
                    throw new FailedException(ex.Message);
                }
            }
            
        }

        public IList<PharmacyWindowVO> GetPagintionList(Pagination pagination, string organizeId, string keyword = null)
        {
            var strSql = @"select yfck.yfckId,yfck.yfckCode,yfck.yfckmc,yfck.zt,yfck.px,yfck.CreateTime,yfck.CreatorCode
        from [NewtouchHIS_Base].dbo.xt_yfck yfck
    where yfck.OrganizeId=@OrganizeId
and (isnull(@keyword, '') = '' or yfck.yfckCode like @searchkeyword or yfck.yfckmc like @searchkeyword )";
            SqlParameter[] param = new SqlParameter[] {
                            new SqlParameter("@keyword",keyword ?? ""),
                            new SqlParameter("@OrganizeId",organizeId),
                            new SqlParameter("@searchkeyword", "%" + (keyword ?? "") + "%")
                        };
            return this.QueryWithPage<PharmacyWindowVO>(strSql, pagination, param);
        }

    }
}
