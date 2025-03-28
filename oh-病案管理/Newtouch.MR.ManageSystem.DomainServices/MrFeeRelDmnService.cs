using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.DomainServices
{
    public class MrFeeRelDmnService : DmnServiceBase, IMrFeeRelDmnService
    {
        public MrFeeRelDmnService(IDefaultDatabaseFactory databaseFactory)
               : base(databaseFactory)
        {

        }


        /// <summary>
        /// 获取分页实体列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="keyword">筛选关键字</param>
        public IList<bafeeRelVO> GetPagintionList(Pagination pagination, string keyword, string organizeId,string code)
        {
            var sql = new StringBuilder();
            sql.Append("select  a.*,b.name from [dbo].[mr_dic_sfxmrel] a  left join mr_dic_bafeetype b on a.feetypecode = b.code where a.zt != 0 ");
            List<SqlParameter> pars = null;
            if (!string.IsNullOrWhiteSpace(organizeId))
            {
                pars = pars ?? new List<SqlParameter>();
                sql.Append(" and a.organizeid = @organizeId");
                pars.Add(new SqlParameter("@organizeId", organizeId));
            }
            if (!string.IsNullOrWhiteSpace(code))
            {
                pars = pars ?? new List<SqlParameter>();
                sql.Append(" and b.code = @code");
                pars.Add(new SqlParameter("@code", code));
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                pars = pars ?? new List<SqlParameter>();
                sql.Append(" and(1 = 2");
                sql.Append(" or sfxm like @keyword");
                sql.Append(" or sfxmmc like @keyword");
                sql.Append(" or feetypecode like @keyword");
                sql.Append(" or b.name like @keyword");
                sql.Append(")");
                pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            return this.QueryWithPage<bafeeRelVO>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
        }

        /// <summary>
        /// 获取二级 三级收费大类
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<feeSelVO> GetFeeSel(string orgId)
        {
            string sql = @"select code,name from mr_dic_bafeetype with(nolock)
where zt='1'  and organizeid=@orgId and  Lev!=1";

            return this.FindList<feeSelVO>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId)
            });
        }

        public bafeeRelVO GetFormJson(string keyValue,string organizeId)
        {
            string sql = "select  a.*,b.name from [dbo].[mr_dic_sfxmrel] a  left join mr_dic_bafeetype b on a.feetypecode = b.code where 1 = 1 ";
            sql += "and a.zt=1 ";
            if (!string.IsNullOrWhiteSpace(organizeId))
            {
                sql += " and a.organizeId = @organizeId";
            }
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                sql += " and a.id=@id";
            }
            var para = new List<SqlParameter>();
           
            para.Add(new SqlParameter("@id", keyValue));
            para.Add(new SqlParameter("@organizeid", organizeId));
            return this.FindList<bafeeRelVO>(sql, para.ToArray()).FirstOrDefault();
        }


        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public int SubmitForm(bafeeRelVO entity)
        {
                //his科室代码存在 更新
                string sql = "update [mr_dic_sfxmrel] set feetypecode=@feetypecode ,zt='1' ,LastModifyTime=@LastModifyTime ,LastModifierCode=@LastModifierCode where id = @id and organizeId=@organizeId";
                SqlParameter[] para ={
                new SqlParameter("@feetypecode",entity.feetypecode?? ""),
                new SqlParameter("@LastModifyTime",entity.CreateTime),
                new SqlParameter("@LastModifierCode",entity.CreatorCode ?? ""),
                new SqlParameter("@id",entity.Id ?? ""),
                new SqlParameter("@organizeId",entity.OrganizeId ?? ""),
                };
                return this.ExecuteSqlCommand(sql, para);
           
        }

		public int SaveForm(bafeeRelVO entity)
		{
			
			string sql = "  insert into mr_dic_sfxmrel values(@id,@organizeId,@sfxm,@sfxmmc,@feetypecode,@zt,@CreateTime,@CreatorCode,@LastModifyTime,@LastModifierCode)";
			SqlParameter[] para ={
				new SqlParameter("@feetypecode",entity.feetypecode?? ""),
				new SqlParameter("@LastModifyTime",entity.CreateTime),
				new SqlParameter("@LastModifierCode",entity.CreatorCode ?? ""),
				new SqlParameter("@id",entity.Id ?? ""),
				new SqlParameter("@organizeId",entity.OrganizeId ?? ""),
				new SqlParameter("@sfxm",entity.sfxm?? ""),
				new SqlParameter("@sfxmmc",entity.sfxmmc?? ""),
				new SqlParameter("@zt",entity.zt?? ""),
				new SqlParameter("@CreateTime",entity.CreateTime),
				new SqlParameter("@CreatorCode",entity.CreatorCode?? ""),
				};
			return this.ExecuteSqlCommand(sql, para);

		}

		//根据树单选显示项目列表
		public IList<bafeeRelVO> GetPagintionListById(Pagination pagination, string organizeId, string id)
		{
			var sql = new StringBuilder();
			sql.Append("select a.id,b.name,a.OrganizeId,sfxm,a.sfxmmc,a.feetypecode,a.zt,a.CreateTime,a.CreatorCode,a.LastModifyTime,a.LastModifierCode from [dbo].[mr_dic_sfxmrel] a  left join mr_dic_bafeetype b on a.feetypecode = b.code where a.zt != 0 ");
			List<SqlParameter> pars = null;
			if (!string.IsNullOrWhiteSpace(organizeId))
			{
				pars = pars ?? new List<SqlParameter>();
				sql.Append(" and a.organizeid = @organizeId");
				pars.Add(new SqlParameter("@organizeId", organizeId));
			}
			if (!string.IsNullOrWhiteSpace(id))
			{
				pars = pars ?? new List<SqlParameter>();
				sql.Append(" and b.id =@id");
				pars.Add(new SqlParameter("@id", id));
			}
			return this.QueryWithPage<bafeeRelVO>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
		}

		//根据树多选显示项目列表
		public IList<bafeeRelVO> GetPagintionListByCodes(Pagination pagination, string organizeId, string ids)
		{
			ids = confirmEnding(ids, ",");
			//ids = "";
			//ids += "523b9c37-4453-41bf-93f2-6252f18384df" + ",";
			//ids += "D868C70A-3E4B-4618-B5FB-4E7B801543F9";
			//ids = "'523b9c37-4453-41bf-93f2-6252f18384df','D868C70A-3E4B-4618-B5FB-4E7B801543F9'";
			//ids = "'3F536F13 - 683C - 4D87 - 8E8E - F07097F81E12,00DA04CF - AB99 - 4219 - 9F8E-C1D4B86C4401','748DA7CB - 22A7 - 4506 - 8F2A - EBC394C791C7','862C772C - 86F1 - 443D - A90F - 6C407BE44A7D,523b9c37 - 4453 - 41bf - 93f2 - 6252f18384df','E17CC469 - 5419 - 4F92 - 972A - 2F3089D86C3C','70DBE0C5 - 1DBF - 4E6C - BF42 - DB50630195C2','C76DCEF8 - A93B - 4A62 - 8B9B - D524BA6A1662','D10C6D55 - 7D91 - 4A72 - 9067 - C1EC3C239828'";
			var sql = new StringBuilder();
			sql.Append("select b.id,b.name,a.OrganizeId,sfxm,a.sfxmmc,a.feetypecode,a.zt,a.CreateTime,a.CreatorCode,a.LastModifyTime,a.LastModifierCode from [dbo].[mr_dic_sfxmrel] a  left join mr_dic_bafeetype b on a.feetypecode = b.code where  a.zt != 0");
			List<SqlParameter> pars = null;
			if (!string.IsNullOrWhiteSpace(organizeId))
			{
				pars = pars ?? new List<SqlParameter>();
				sql.Append(" and a.organizeid = @organizeId");
				pars.Add(new SqlParameter("@organizeId", organizeId));
			}
			if (!string.IsNullOrWhiteSpace(ids))
			{
				pars = pars ?? new List<SqlParameter>();
				sql.Append(" and b.id in ("+ids+")");
				pars.Add(new SqlParameter("@ids", ids));
			}
			return this.QueryWithPage<bafeeRelVO>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
		}

		public string confirmEnding(string str, string target)
		{
			if (str.EndsWith(target))
			{
				return str.Substring(0, str.Length-target.Length);
			}
			else
			{
				return str;
			}
			//if (str.Length >= target.Length)
			//{
			//	var start = str.Length - target.Length;
			//	var arr = str.Substring(start, target.Length);
			//	if (arr == target)
			//	{
			//		return str.Replace(target, "");
			//	}
			//	return str;
			//}
			//else {
			//	return str;
			//}

		}

		//显示未分类项目列表
		public IList<itemEntity> GetPagintionItem(Pagination pagination, string organizeId, string keyword,string hissfdl,string code)
		{
			var sql = new StringBuilder();
            var sqlyp = new StringBuilder();
            string strwhere = "";
            sql.Append(@"select sfxmCode, sfxmmc,sfdlCode,py
from  [NewtouchHIS_Base].[dbo].[V_S_xt_sfxm]  
where organizeId=@orgId  and sfxmcode not in (
	select  a.sfxm 
	from Newtouch_MRMS.dbo.[mr_dic_sfxmrel] a 
	where a.zt !=0  ");
            sqlyp.Append(@"select ypCode,ypmc,dlCode,py
from [NewtouchHIS_Base].[dbo].V_C_xt_yp 
where organizeId=@orgId  and ypCode not in (
	select  a.sfxm 
	from Newtouch_MRMS.dbo.[mr_dic_sfxmrel] a 
	where a.zt !=0 ");

            List<SqlParameter> pars = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(code))
            {
                sql.Append(" and feetypecode=@code ) ");
                sqlyp.Append(" and feetypecode=@code ) ");
                pars.Add(new SqlParameter("@code",  code));

            }
            else
            {
                sql.Append(" ) ");
                sqlyp.Append(" ) ");
            }
            if (!string.IsNullOrWhiteSpace(keyword))
			{				
				sql.Append(" and(sfxmCode like @keyword or sfxmmc like @keyword )");
                sqlyp.Append(" and(ypCode like @keyword or ypmc like @keyword )");
                pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            if (!string.IsNullOrWhiteSpace(hissfdl))
            {
                sql.Append(" and sfdlCode=@hissfdl ");
                sqlyp.Append("  and dlCode=@hissfdl ");
                pars.Add(new SqlParameter("@hissfdl", hissfdl ?? ""));
            }

            pars.Add(new SqlParameter("@orgId", organizeId));
            
            sql.Append(strwhere);
            sql.Append(" union all " + sqlyp);

			return this.QueryWithPage<itemEntity>(sql.ToString(), pagination, pars.ToArray());
		}

        public void SaveRelbyHissfdl(string code,string hissfdl,string user,string orgId)
        {
            if (!string.IsNullOrWhiteSpace(code) && !string.IsNullOrWhiteSpace(hissfdl))
            {
                try
                {
                    string sql = @"insert into mr_dic_sfxmrel(Id,OrganizeId,sfxm,sfxmmc,feetypecode,zt,CreateTime,CreatorCode)
select newid(),OrganizeId,sfxmCode,sfxmmc,@code,'1',getdate(),@user 
from  [NewtouchHIS_Base].[dbo].[V_S_xt_sfxm] a with(nolock)
where sfdlcode=@sfdl and OrganizeId=@orgId 
--and not exists(select 1 from mr_dic_sfxmrel b with(nolock) where a.sfxmCode=b.sfxm and b.zt='1')
";
                    this.ExecuteSqlCommand(sql, new SqlParameter[] {
                        new SqlParameter("@code",code),
                        new SqlParameter("@sfdl",hissfdl),
                        new SqlParameter("@user",user),
                        new SqlParameter("@orgId",orgId)
                    });
                }
                catch (Exception ex)
                {
                    throw new FailedException("异常：" + ex.Message);
                }
            }
            else
            {
                throw new FailedException("病案收费类别及HIS收费大类不可为空！");
            }
        }
        public void SaveRelbyhissfxm(string code, string ids, string user, string orgId)
        {
            if (!string.IsNullOrWhiteSpace(code) &&!string.IsNullOrWhiteSpace(ids))
            {
                try
                {
                    string sql = @"insert into mr_dic_sfxmrel(Id,OrganizeId,sfxm,sfxmmc,feetypecode,zt,CreateTime,CreatorCode)
select newid(),OrganizeId,sfxmCode,sfxmmc,@code,'1',getdate(),@user 
from  [NewtouchHIS_Base].[dbo].[V_S_xt_sfxm] a with(nolock)
where sfxmCode in({0}) and OrganizeId=@orgId 
--and not exists(select 1 from mr_dic_sfxmrel b with(nolock) where a.sfxmCode=b.sfxm and b.zt='1')
";
                    string pids = "";
                    string[] idss = ids.Split(',');
                    foreach (string id in idss)
                    {
                        if (!string.IsNullOrWhiteSpace(id))
                        {
                            if (pids == "")
                            {
                                pids = "'" + id + "'";
                            }
                            else {
                                pids += ",'" + id + "'";
                            }
                        }
                    }
                    sql = string.Format(sql, pids);
                    int cnt= this.ExecuteSqlCommand(sql, new SqlParameter[] {
                        new SqlParameter("@code",code),
                        new SqlParameter("@user",user),
                        new SqlParameter("@orgId",orgId)
                    });
                }
                catch (Exception ex)
                {
                    throw new FailedException("异常：" + ex.Message);
                }
            }
            else
            {
                throw new FailedException("病案收费类别及HIS收费大类不可为空！");
            }
        }
    }
}
