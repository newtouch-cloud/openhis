using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.OR.ManageSystem.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Newtouch.Core.Common;
using System.Data;

namespace Newtouch.OR.ManageSystem.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-10-31 10:04
    /// 描 述：手术申请记录
    /// </summary>
    public class ORApplyInfoRepo : RepositoryBase<ORApplyInfoEntity>, IORApplyInfoRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public ORApplyInfoRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public int SubmitForm(ORApplyInfoEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties
                
                dbEntity.Modify(keyValue);
                return Update(dbEntity);
            }
            entity.Create(true);
            return Insert(entity);
        }


        


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public void DeleteForm(string keyValue,string orgId,string ssmcn)
        {
            //return Delete(p => p.Id == keyValue);
            
            var dbEntity = this.FindEntity(p=>p.Applyno==keyValue && p.OrganizeId==orgId);
            var mc = dbEntity.ssmcn;
            if (dbEntity != null)
            {
                dbEntity.sqzt = ((int)EnumSqzt.yqx).ToString();
                Update(dbEntity);

                var sqlcx = @"select 1 from [Newtouch_CIS].[dbo].[zy_lsyz] 
where yzlx=3  and zyh=" + dbEntity.zyh + " and xmmc='" + ssmcn+"' and ysgh='"+dbEntity.ysgh+"' and  OrganizeId='"+dbEntity.OrganizeId+"' and sssj='"+dbEntity.sssj+"'";
                var a = ExecuteSqlCommand(sqlcx);
                if (a !=0)
                {
                    var sql = @"update [Newtouch_CIS].[dbo].[zy_lsyz]
                                set zt=0
                                where yzlx=3 and yzzt=0 and zyh='"+dbEntity.zyh+"' and xmmc='" + ssmcn + "' and ysgh='" + dbEntity.ysgh + "' and  OrganizeId='" + dbEntity.OrganizeId + "'and sssj='" + dbEntity.sssj + "'";
                    ExecuteSqlCommand(sql);
                }
                
            }
            else
            {
                throw new FailedException("操作失败，未找到相关申请单。");
            }
        }

        /// <summary>
        /// 手术排班后,更新申请状态(2.已排班)
        /// 取消手术排班后,更新申请状态(1.已申请)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public int UpdateSqzt(string keyValue,string sqzt )
        {
            var dbEntity = this.FindEntity(keyValue);
            //properties
            dbEntity.sqzt = sqzt;
            dbEntity.Modify(keyValue);
            return Update(dbEntity);
        }

    }
}