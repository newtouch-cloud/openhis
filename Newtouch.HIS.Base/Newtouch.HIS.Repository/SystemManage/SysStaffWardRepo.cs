using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.SystemManage
{
    public class SysStaffWardRepo:RepositoryBase<SysStaffWardEntity>,ISysStaffWardRepo
    {
        public SysStaffWardRepo(IBaseDatabaseFactory databaseFactory)
             : base(databaseFactory)
        {
        }
        /// <summary>
        /// 查找员工病区绑定信息
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public IList<SysStaffWardEntity> GetStaffWardList(string staffId)
        {
            if (string.IsNullOrWhiteSpace(staffId))
            {
                return null;
            }
            string sql = @"select [Id],[OrganizeId],[StaffId],[bqCode],[CreatorCode],[CreateTime],
                            [LastModifyTime],[LastModifierCode],[zt],[px] from sys_staffward with(nolock) 
                            where zt=1 and StaffId= @staffId";

            return this.FindList<SysStaffWardEntity>(sql, new[] { new SqlParameter("@staffId", staffId) });
        }
        ///// <summary>
        ///// 修改和添加病区信息
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <param name="keyValue"></param>
        //public void submitForm(SysUserWardEntity entity, string keyValue)
        //{
        //    if (keyValue!=null &&keyValue!="")
        //    {
        //        if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId
        //        && p.Id != keyValue && p.bfCode == entity.bfCode
        //        ))
        //        {
        //            throw new FailedException("房间编码不能重复！");
        //        }

        //        SysWardRoomEntity oldEntity = null;   //变更前Entity
        //        oldEntity = this.FindEntity(keyValue.Value);
        //        this.DetacheEntity(oldEntity);

        //        entity.Modify();
        //        entity.bfId = keyValue.Value;
        //        this.Update(entity);

        //        if (oldEntity != null)
        //        {
        //            AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysWardRoomEntity.GetTableName(), oldEntity.bfId.ToString());
        //        }
        //    }
        //    else
        //    {

        //        if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId &&
        //        p.bfCode == entity.bfCode))
        //        {
        //            throw new FailedException("房间编码不能重复！");
        //        }
        //        entity.Create();
        //        this.Insert(entity);
        //    }
        //}
    }
}
