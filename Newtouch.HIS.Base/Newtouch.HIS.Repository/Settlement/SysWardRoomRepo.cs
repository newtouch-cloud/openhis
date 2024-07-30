using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Core.Common;
using System.Data.SqlClient;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Common;

namespace Newtouch.HIS.Repository
{
    public class SysWardRoomRepo:RepositoryBase<SysWardRoomEntity>,ISysWardRoomRepo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysWardRoomRepo(IBaseDatabaseFactory databaseFactory)
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
        public IList<SysWardRoomEntity> GetPagintionList(Pagination pagination, string organizeId, string keyword = null)
        {
            var sql = @"select * from xt_bf  where OrganizeId=@organizeId 
                       and (isnull(@keyword, '') = '' or bfCode like @searchkeyword 
                        or bfNo like @searchkeyword or bqCode like @searchkeyword )";
            SqlParameter[] param = new SqlParameter[] {
                 new SqlParameter("@keyword",keyword ?? ""),
                            new SqlParameter("@organizeId",organizeId),
                            new SqlParameter("@searchkeyword", "%" + (keyword ?? "") + "%")
            };
            return this.QueryWithPage<SysWardRoomEntity>(sql, pagination, param);
        }

        /// <summary>
        /// 修改和添加病区信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void submitForm(SysWardRoomEntity entity, int? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId
                && p.bfId != keyValue && p.bfCode == entity.bfCode
                ))
                {
                    throw new FailedException("房间编码不能重复！");
                }

                SysWardRoomEntity oldEntity = null;   //变更前Entity
                oldEntity = this.FindEntity(keyValue.Value);
                this.DetacheEntity(oldEntity);

                entity.Modify();
                entity.bfId = keyValue.Value;
                this.Update(entity);

                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysWardRoomEntity.GetTableName(), oldEntity.bfId.ToString());
                }
            }
            else
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId &&
                p.bfCode == entity.bfCode))
                {
                    throw new FailedException("房间编码不能重复！");
                }
                entity.Create();
                this.Insert(entity);
            }
        }

    }
}
