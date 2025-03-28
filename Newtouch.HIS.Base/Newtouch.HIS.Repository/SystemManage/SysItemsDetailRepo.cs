using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysItemsDetailRepo : RepositoryBase<SysItemsDetailEntity>, ISysItemsDetailRepository
    {
        public SysItemsDetailRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取公共字典项List 所有机构公用的
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysItemsDetailEntity> GetCommonList(string itemId = null, string keyword = null)
        {
            var expression = ExtLinq.True<SysItemsDetailEntity>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.ItemId == itemId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.Name.Contains(keyword));
                expression = expression.Or(t => t.Code.Contains(keyword));
            }
            expression = expression.And(t => t.OrganizeId == "*");
            return this.IQueryable(expression).OrderBy(t => t.px).ToList();
        }

        /// <summary>
        /// 获取 单一组织机构 字典项List
        /// </summary>
        /// <param name="orgId">医疗机构Id，不传时差共享的</param>
        /// <param name="itemId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysItemsDetailVO> GetListByOrgId(string orgId = null, string itemId = null, string keyword = null)
        {
            var sql = @"select * from Sys_ItemsDetail(nolock) 
where 1 = 1
and(OrganizeId = @orgId or OrganizeId = '*')
and zt = '1'
and (isnull(@itemId,'') = '' or ItemId = @itemId)
and (Code like @searchKeyword or Name like @searchKeyword) 
order by CreateTime desc";
            return this.FindList<SysItemsDetailVO>(sql, new SqlParameter[] {
                    new SqlParameter("@searchKeyword", "%" + (keyword ?? "") + "%")
                    ,new SqlParameter("@orgId", orgId ??"")
                    ,new SqlParameter("@itemId", itemId ??"")
            });
        }

        /// <summary>
        /// 更新、保存字典项
        /// </summary>
        /// <param name="itemsDetailEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysItemsDetailEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var isRepeated = entity.OrganizeId == "*"
                    ? this.IQueryable().Any(p => 
                        p.ItemId == entity.ItemId && 
                        p.Code == entity.Code && p.Id != keyValue)
                    : this.IQueryable().Any(p => (p.OrganizeId == entity.OrganizeId || p.OrganizeId == "*")
                         && p.ItemId == entity.ItemId 
                         && p.Code == entity.Code && p.Id != keyValue)
                         ;
                if (isRepeated)
                {
                    throw new FailedException("编码不可重复");
                }

                SysItemsDetailEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(keyValue);
                this.DetacheEntity(oldEntity);

                entity.Modify(keyValue);
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysItemsDetailEntity.GetTableName(), oldEntity.Id);
                }
            }
            else
            {
                var isRepeated = entity.OrganizeId == "*"
                    ? this.IQueryable().Any(p => 
                        p.ItemId == entity.ItemId && 
                        p.Code == entity.Code)
                    : this.IQueryable().Any(p => (p.OrganizeId == entity.OrganizeId || p.OrganizeId == "*")
                         && p.ItemId == entity.ItemId 
                         && p.Code == entity.Code)
                         ;
                if (isRepeated)
                {
                    throw new FailedException("编码不可重复");
                }
                entity.Create(true);
                this.Insert(entity);
            }
        }

        /// <summary>
        /// 获取 科目分类树状图
        /// </summary>
        /// <param name="orgId">医疗机构Id，不传时差共享的</param>
        /// <returns></returns>
        public List<ProjectZbVO> GetTreeViewdata(string orgId)
        {
            var sql = @"select * from sys_kmglzb
where OrganizeId = @orgId";
            return this.FindList<ProjectZbVO>(sql, new SqlParameter[] {
                    new SqlParameter("@orgId", orgId)
            });
        }
        public IList<ProjectMxVO> GetProjectMxVO(string orgId, string kmdm)
        {
            var sql = @"select * from sys_kmglmx
where OrganizeId = @orgId and zt=1";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
                sql += " and kmdm=@kmdm";
                pars.Add(new SqlParameter("@kmdm", kmdm));
            return this.FindList<ProjectMxVO>(sql, pars.ToArray());
        }

        public IList<XmandYp> GetXmandYp(string orgId, string kmdm, int? xmlx)
        {
            if (xmlx == null) {
                var sqlstr = @"select * from sys_kmglzb where kmdm=@kmdm";
                var pars = new List<SqlParameter>();
                pars.Add(new SqlParameter("@kmdm", kmdm));
                var xm = this.FirstOrDefault<ProjectZbVO>(sqlstr.ToString(), pars.ToArray());
                xmlx = xm.xmlx;
            }

            var sql = "";
            switch (xmlx)
            {
                case 1: sql = @"select sfxmId,sfxmCode,sfxmmc from xt_sfxm where sfxmId not in 
(select xmdm from sys_kmglmx where zt=1)";
                    break;
                case 2: sql = @"select ypId sfxmId,ypCode sfxmCode,ypmc sfxmmc from xt_yp where ypId not in 
(select xmdm from sys_kmglmx where zt=1)";
                    break;
                default: sql = @"select * from( select sfxmId,sfxmCode,sfxmmc from xt_sfxm
union
select ypId,ypCode,ypmc from xt_yp) a
where sfxmId not in 
(select xmdm from sys_kmglmx where zt=1)";
                    break;
            }
            return this.FindList<XmandYp>(sql);
        }

        public int UpdateMx(string orgid, string kmdm, string xmdm, string czydm) {
            var sql = @"update  sys_kmglmx set zt=0,LastModifierCode=@czydm,LastModifyTime=GETDATE() where xmdm=@xmdm and xmdm=@xmdm and OrganizeId=@orgid";

            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@xmdm", xmdm));
            pars.Add(new SqlParameter("@kmdm", kmdm));
            pars.Add(new SqlParameter("@orgid", orgid));
            pars.Add(new SqlParameter("@czydm", czydm));
            return this.ExecuteSqlCommand(sql, pars.ToArray());
        }
        public int InsertMx(string orgid, string kmdm, string xmdm, string czydm) {
            var sql = @"insert into sys_kmglmx values(@kmdm,@xmdm,GETDATE(),@czydm,null,null,@orgid,1)";

            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@xmdm", xmdm));
            pars.Add(new SqlParameter("@kmdm", kmdm));
            pars.Add(new SqlParameter("@orgid", orgid));
            pars.Add(new SqlParameter("@czydm", czydm));
            return this.ExecuteSqlCommand(sql, pars.ToArray());
        }
        public int DeleteMl(string orgid, string kmdm) {
            var sql = @"select * from sys_kmglzb where sjkmdm=@kmdm";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@kmdm", kmdm));

            List<ProjectZbVO> zb = this.FindList<ProjectZbVO>(sql, pars.ToArray());
            if (zb.Count > 0)
            {
                return 0;
            }
            else
            {
                sql = @"delete sys_kmglzb where OrganizeId=@orgid and kmdm=@kmdm";

                var ps = new List<SqlParameter>();
                ps.Add(new SqlParameter("@kmdm", kmdm));
                ps.Add(new SqlParameter("@orgid", orgid));
                return this.ExecuteSqlCommand(sql, ps.ToArray());
            }
        }
        public List<ProjectZbVO> GetMlbyKmdm(string kmdm, string orgid)
        {
            var sql = @"select * from sys_kmglzb
where OrganizeId = @orgId and kmdm=@kmdm";
            return this.FindList<ProjectZbVO>(sql, new SqlParameter[] {
                    new SqlParameter("@orgId", orgid),
                    new SqlParameter("@kmdm", kmdm)
            });
        }
        public int InsertMl(string kmmc, string kmdm, string sjkmdm, int xmlx, int gmlbz, decimal sl, string orgid, string czydm,int zt)
        {
            var sql = @"insert into sys_kmglzb values(@kmmc,@kmdm,@sjkmdm,@xmlx,@gmlbz,@sl,GETDATE(),@czydm,null,null,@orgid,@zt)";

            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@kmmc", kmmc));
            pars.Add(new SqlParameter("@kmdm", kmdm));
            pars.Add(new SqlParameter("@sjkmdm", sjkmdm));
            pars.Add(new SqlParameter("@xmlx", xmlx));
            pars.Add(new SqlParameter("@gmlbz", gmlbz));
            pars.Add(new SqlParameter("@sl", sl));
            pars.Add(new SqlParameter("@orgid", orgid));
            pars.Add(new SqlParameter("@czydm", czydm));
            pars.Add(new SqlParameter("@zt", zt));
            return this.ExecuteSqlCommand(sql, pars.ToArray());
        }
        public int UpdateMl(string kmmc, string kmdm, string sjkmdm, int xmlx, int gmlbz, decimal sl, string orgid, string czydm, int zt)
        {
            var sql = @"update sys_kmglzb set kmmc=@kmmc,sjkmdm=@sjkmdm,xmlx=@xmlx,gmlbz=@gmlbz,sl=@sl,LastModifyTime=GETDATE(),LastModifierCode=@czydm,zt=@zt where kmdm=@kmdm and OrganizeId=@orgid";

            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@kmmc", kmmc));
            pars.Add(new SqlParameter("@kmdm", kmdm));
            pars.Add(new SqlParameter("@sjkmdm", sjkmdm));
            pars.Add(new SqlParameter("@xmlx", xmlx));
            pars.Add(new SqlParameter("@gmlbz", gmlbz));
            pars.Add(new SqlParameter("@sl", sl));
            pars.Add(new SqlParameter("@orgid", orgid));
            pars.Add(new SqlParameter("@czydm", czydm));
            pars.Add(new SqlParameter("@zt", zt));
            return this.ExecuteSqlCommand(sql, pars.ToArray());
        }
        public List<ProjectZbVO> Getsjdm(string orgid)
        {
            var sql = @"select * from sys_kmglzb
where OrganizeId = @orgid and (sjkmdm is null or sjkmdm='')";
            return this.FindList<ProjectZbVO>(sql, new SqlParameter[] {
                    new SqlParameter("@orgid", orgid)
            });
        }
    }
}


