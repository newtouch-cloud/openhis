using System;
using System.Collections.Generic;
using Newtouch.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity.Settlement;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysWardRepo : RepositoryBase<SysWardEntity>, ISysWardRepo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysWardRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<SysWardEntity> GetPagintionList(Pagination pagination, string organizeId, string keyword = null)
        {
            var sql = @"	select bqId,bqCode,bqmc,bq.OrganizeId,bq.py,bq.CreatorCode,bq.CreateTime,bq.LastModifyTime,bq.LastModifierCode,bq.zt,bq.px, staff.Name hsz_gh,staff1.Name kzr_gh,staff2.Name kfzlsz_gh		from xt_bq bq
						left join Sys_Staff staff on staff.gh=bq.hsz_gh and staff.OrganizeId=bq.OrganizeId
						left join Sys_Staff staff1 on staff1.gh=bq.kzr_gh and staff1.OrganizeId=bq.OrganizeId
						left join Sys_Staff staff2 on staff2.gh=bq.kfzlsz_gh and staff2.OrganizeId=bq.OrganizeId
						where bq.OrganizeId=@organizeId 
                        and (isnull(@keyword, '') = '' or bqCode like @searchkeyword 
                        or bqmc like @searchkeyword or bq.py like @searchkeyword )";
            SqlParameter[] param = new SqlParameter[] {
                 new SqlParameter("@keyword",keyword ?? ""),
                            new SqlParameter("@organizeId",organizeId),
                            new SqlParameter("@searchkeyword", "%" + (keyword ?? "") + "%")
            };
            return this.QueryWithPage<SysWardEntity>(sql, pagination, param);
        }

        public IList<Getcz> Selectzccx(string organizeId, int xz)
       {
            var sql = "";
            var pargh = new List<SqlParameter>();
            pargh.Add(new SqlParameter("@organizeId", organizeId));
            pargh.Add(new SqlParameter("@xz", xz));
            if (xz==1)
            {
                sql = @"select  Name,gh,(case zc when 'zrys' then '科主任' end)zc
from Sys_Staff staff where  OrganizeId=@organizeId and zt='1' and zc='zrys'";
                
            }
            else if (xz == 2)
            {
                 sql = @"select Name, gh,(case zc when 'hsz' then '护士长' end)zc
from Sys_Staff staff where OrganizeId=@organizeId and zt = '1' and zc = 'hsz'";
                
            }
            else
            {
                 sql = @"select  Name,gh,(case zc when 'zlsz' then '康复治疗师长' end)zc
from Sys_Staff staff where  OrganizeId='9bb029d0-5da0-4118-9d19-06b829eede46' and zt='1' and zc='zlsz'";
                
            }
            return this.FindList<Getcz>(sql, pargh.ToArray());
        }

        /// <summary>
        /// 修改和添加病区信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void submitForm(SysWardEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId
                && p.bqId != keyValue && p.bqCode == entity.bqCode
                ))
                {
                    throw new FailedException("病区编号不能重复！");
                }

                /*停用病区的判断*/
                var zt = entity.zt;
                if (zt == "0")
                {
                    var bq = entity.bqCode;
                    var orgId = entity.OrganizeId;
                    var sql = "select count(1) from NewtouchHIS_Sett..zy_brjbxx where zybz in(0, 1) and bq = @bq and OrganizeId = @orgId and zt = '1'";
                    var kssyqk = this.FirstOrDefault<int>(sql, new[] { new SqlParameter("@orgId", orgId)
                    ,new SqlParameter("@bq", bq)});

                    if (kssyqk != 0)
                    {
                        throw new FailedException("病区中有病人在院，无法停止！");
                    }
                }
                

                SysWardEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(keyValue.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.bqId = keyValue.Value;
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysWardEntity.GetTableName(), oldEntity.bqId.ToString());
                }
            }
            else
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId &&
                p.bqCode == entity.bqCode))
                {
                    throw new FailedException("病区编号不能重复！");
                }
                entity.Create();
                this.Insert(entity);
            }
        }

        /// <summary>
        /// 获取所有病区列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<SysWardEntity> SelectWardList(string orgId)
        {
            var list = this.IQueryable().Where(a => a.OrganizeId == orgId && a.zt == "1").ToList();
            return list;
        }
    }
}
