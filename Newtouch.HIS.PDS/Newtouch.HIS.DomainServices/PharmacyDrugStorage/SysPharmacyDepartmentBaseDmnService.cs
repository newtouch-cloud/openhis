using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.DomainServices.PharmacyDrugStorage
{
    public class SysPharmacyDepartmentBaseDmnService : DmnServiceBase, ISysPharmacyDepartmentBaseDmnService
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysPharmacyDepartmentBaseDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }

        public SysPharmacyDepartmentVO GetFormJson(string orgId, int? yfbmId)
        {
            var strSql = @"select * from [NewtouchHIS_Base].dbo.xt_yfbm where OrganizeId=@orgId";
            var parList = new List<SqlParameter>() { };
            parList.Add(new SqlParameter("@orgId", orgId ?? ""));
            if (yfbmId != 0)
            {
                strSql += " and yfbmId = @yfbmId";
                parList.Add(new SqlParameter("@yfbmId", yfbmId));
            }
            return this.FirstOrDefault<SysPharmacyDepartmentVO>(strSql, parList.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="yjbmjb"></param>
        /// <returns></returns>
        public IList<SysPharmacyDepartmentVO> GetList(string orgId, byte? yjbmjb)
        {
            var strSql = @"select * from [NewtouchHIS_Base].dbo.xt_yfbm where OrganizeId=@orgId";
            var parList = new List<SqlParameter>() { };
            parList.Add(new SqlParameter("@orgId", orgId ?? ""));
            if (yjbmjb.HasValue)
            {
                strSql += " and yjbmjb = @yjbmjb";
                parList.Add(new SqlParameter("@yjbmjb", yjbmjb.Value));
            }
            return this.FindList<SysPharmacyDepartmentVO>(strSql, parList.ToArray());
        }

        /// <summary>
        /// 获取有效的药房和药库
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="yjbmjb"></param>
        /// <returns></returns>
        public IList<SysPharmacyDepartmentVO> GetEffectiveList(string orgId, byte? yjbmjb)
        {
            var strSql = @"select * from [NewtouchHIS_Base].dbo.xt_yfbm where OrganizeId=@orgId and zt='1' ";
            var parList = new List<SqlParameter>
            {
                new SqlParameter("@orgId", orgId ?? "")
            };
            if (!yjbmjb.HasValue) return FindList<SysPharmacyDepartmentVO>(strSql, parList.ToArray());
            strSql += " and yjbmjb = @yjbmjb";
            parList.Add(new SqlParameter("@yjbmjb", yjbmjb.Value));
            return FindList<SysPharmacyDepartmentVO>(strSql, parList.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<SysPharmacyDepartmentVO> GetPagintionList(Pagination pagination, string organizeId, string keyword = null)
        {
            var strSql = @"select * from [NewtouchHIS_Base].dbo.xt_yfbm
            where OrganizeId=@organizeId  and (isnull(@keyword, '') = '' or yfbmCode like @searchkeyword or yfbmmc like @searchkeyword or py like @searchkeyword)";
            SqlParameter[] param = new SqlParameter[] {
                            new SqlParameter("@keyword",keyword ?? ""),
                            new SqlParameter("@organizeId",organizeId),
                            new SqlParameter("@searchkeyword", "%" + (keyword ?? "") + "%")
                        };
            return this.QueryWithPage<SysPharmacyDepartmentVO>(strSql, pagination, param);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void submitForm(SysPharmacyDepartmentVO entity, int? keyValue)
        {

            if (keyValue.HasValue && keyValue.Value > 0)
            {
                var checkSql = "select * from [NewtouchHIS_Base].dbo.xt_yfbm where zt=1 and OrganizeId=@organizeId  and yfbmId = @yfbmId and yfbmCode=@yfbmCode";
                var parList = new List<SqlParameter>
                {
                    new SqlParameter("@organizeId", entity.OrganizeId ?? ""),
                    new SqlParameter("@yfbmId", keyValue ),
                    new SqlParameter("@yfbmCode", entity.yfbmCode ?? ""),
                };
                var checkList = FindList<SysPharmacyDepartmentVO>(checkSql, parList.ToArray());

                if (checkList.Count > 0)
                {
                    throw new FailedException("部门编号不能重复！");
                }
                try
                {
                    using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
                    {
                        var strSql = new StringBuilder();
                        strSql.Append(@"  UPDATE [NewtouchHIS_Base].dbo.xt_yfbm
SET yfbmCode=@yfbmCode,yfbmmc=@yfbmmc,OrganizeId=@OrganizeId,ksCode=@ksCode,ynwbz=@ynwbz,yjbmjb=@yjbmjb,fybz=@fybz,mzzybz=@mzzybz,py=@py,LastModifyTime=@LastModifyTime,LastModifierCode=@LastModifierCode,zt=@zt
where yfbmId=@yfbmId");
                        var paraList = new DbParameter[]
                        {
                        new SqlParameter("@yfbmId", keyValue),
                        new SqlParameter("@yfbmCode", entity.yfbmCode),
                        new SqlParameter("@yfbmmc", entity.yfbmmc),
                        new SqlParameter("@OrganizeId", entity.OrganizeId),
                        new SqlParameter("@ksCode", entity.ksCode),
                        new SqlParameter("@ynwbz", entity.ynwbz),
                        new SqlParameter("@yjbmjb", entity.yjbmjb),
                        new SqlParameter("@fybz", entity.fybz),
                        new SqlParameter("@mzzybz", entity.mzzybz),
                        new SqlParameter("@py", entity.py),
                        new SqlParameter("@LastModifyTime", DateTime.Now),
                        new SqlParameter("@LastModifierCode", OperatorProvider.GetCurrent().UserCode),
                        new SqlParameter("@zt", entity.zt)
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
                var checkSql = "select * from [NewtouchHIS_Base].dbo.xt_yfbm where zt=1 and OrganizeId=@organizeId  and yfbmCode=@yfbmCode ";
                var parList = new List<SqlParameter>
                {
                    new SqlParameter("@organizeId", entity.OrganizeId ?? ""),
                    new SqlParameter("@yfbmCode", entity.yfbmCode ?? ""),
                };
                var checkList = FindList<SysPharmacyDepartmentVO>(checkSql, parList.ToArray());
                if (checkList.Count > 0)
                {
                    throw new FailedException("部门编号不能重复！");
                }
                try
                {
                    using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
                    {
                        var strSql = new StringBuilder();
                        strSql.Append(@"insert into [NewtouchHIS_Base].dbo.xt_yfbm
                                  (yfbmCode,yfbmmc,OrganizeId,ksCode,ynwbz,yjbmjb,fybz,mzzybz,jsfs,py,CreatorCode,CreateTime,LastModifyTime,LastModifierCode,zt,px)
                                  values (@yfbmCode,@yfbmmc,@OrganizeId,@ksCode,@ynwbz,@yjbmjb,@fybz,@mzzybz,null,@py,@CreatorCode,@CreateTime,null,null,@zt,null);");
                        var paraList = new DbParameter[]
                        {
                        //new SqlParameter("@yfbmId", entity.yfbmId),
                        new SqlParameter("@yfbmCode", entity.yfbmCode),
                        new SqlParameter("@yfbmmc", entity.yfbmmc),
                        new SqlParameter("@OrganizeId", entity.OrganizeId),
                        new SqlParameter("@ksCode", entity.ksCode),
                        new SqlParameter("@ynwbz", entity.ynwbz),
                        new SqlParameter("@yjbmjb", entity.yjbmjb),
                        new SqlParameter("@fybz", entity.fybz),
                        new SqlParameter("@mzzybz", entity.mzzybz),
                        //new SqlParameter("@jsfs", entity.jsfs),
                        new SqlParameter("@py", entity.py),
                        new SqlParameter("@CreatorCode",  OperatorProvider.GetCurrent().UserCode),
                        new SqlParameter("@CreateTime", DateTime.Now),
                        //new SqlParameter("@LastModifyTime", null),
                        //new SqlParameter("@LastModifierCode", null),
                        new SqlParameter("@zt", entity.zt),
                        //new SqlParameter("@px", entity.px)
                        };
                        db.ExecuteSqlCommand(strSql.ToString(), paraList);
                        db.Commit();
                    }
                }
                catch (Exception ex) {
                    throw new FailedException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 获取UserId当前已关联的要yfbmCode （不包括子机构）
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IList<string> GetCurPharmacyList(string userId, string orgId)
        {
            var strsql = @"select yfbmCode from [NewtouchHIS_Base].dbo.Sys_UserYfbm where UserId = @UserId and OrganizeId = @orgId";
            return this.FindList<string>(strsql, new SqlParameter[] {
                new SqlParameter("@UserId",userId),
                new SqlParameter("@orgId",orgId),
            });
        }

        /// <summary>
        /// 获取组织机构下的药房/药库 （不包括子机构）
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IList<SysPharmacyDepartmentVO> GetPharmacyListByOrg(string OrganizeId)
        {
            var sql = @"select * from [NewtouchHIS_Base].dbo.xt_yfbm where OrganizeId=@OrganizeId and zt='1'";
            return this.FindList<SysPharmacyDepartmentVO>(sql, new SqlParameter[] {
                new SqlParameter("@OrganizeId",OrganizeId)
            });
        }

    }
}