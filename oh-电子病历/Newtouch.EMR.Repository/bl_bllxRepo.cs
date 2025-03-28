using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using FrameworkBase.MultiOrg.Repository;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using System;
using System.Data.SqlClient;
using Newtouch.Infrastructure;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2018-08-30 11:31
    /// 描 述：病历大类
    /// </summary>
    public class bl_bllxRepo : RepositoryBase<bl_bllxEntity>, Ibl_bllxRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public bl_bllxRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(bl_bllxEntity entity, string keyValue)
        {
            string sql = "";
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                var check = this.FindEntity(p => p.bllx == entity.bllx && p.zt == "1" && p.Id != keyValue && p.OrganizeId==entity.OrganizeId);
                if (check != null)
                {
                    throw new FailedException("该病历大类标识已存在！");
                }
                if (string.IsNullOrWhiteSpace(entity.ParentId))
                {
                    //dbEntity.bllxcode = entity.bllxcode;
                    dbEntity.relTB = entity.relTB;
                    dbEntity.MenuLev = entity.MenuLev;
                    dbEntity.MenuLevName = entity.MenuLevName;
                    dbEntity.RelDutys = entity.RelDutys;
                    sql = @" update [bl_bllx] 
set MenuLev=@MenuLev,MenuLevName=@MenuLevName,RelDutys=@RelDutys,mzbz=@mzbz,[LastModifyTime]=getdate(),[LastModifierCode]='auto'
where ParentId=@id and zt='1' and OrganizeId=@orgId ";
                    this.ExecuteSqlCommand(sql,new SqlParameter[] {
                        new SqlParameter("@MenuLev",entity.MenuLev),
                        new SqlParameter("@MenuLevName",entity.MenuLevName??string.Empty),
                        new SqlParameter("@RelDutys",entity.RelDutys??string.Empty),
                        new SqlParameter("@id",dbEntity.Id),
                        new SqlParameter("@orgId",dbEntity.OrganizeId),
                        new SqlParameter("@mzbz",entity.mzbz),
                    });
                }
                dbEntity.mzbz = entity.mzbz;
                dbEntity.bllxmc = entity.bllxmc;
                dbEntity.px = entity.px;
                dbEntity.zt = entity.zt == "true" ? "1" : "0";
                dbEntity.LastModifierCode = entity.LastModifierCode;
                dbEntity.LastModifyTime= DateTime.Now;
                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
            }
            else
            {
                var check = this.FindEntity(p => p.bllx == entity.bllx && p.zt == "1" && p.OrganizeId==entity.OrganizeId);
                if (check != null)
                {
                    throw new FailedException("该病历大类标识已存在！");
                }
                if (!string.IsNullOrWhiteSpace(entity.ParentId))
                {
                    bl_bllxEntity pety = this.FindEntity(p => p.Id == entity.ParentId && p.zt == "1");
                    if (pety != null)
                    {
                        entity.bllx = pety.bllx + EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("bl_bllx.bllx", entity.OrganizeId, "{0:D3}", false);
                        entity.relTB = pety.relTB;
                        entity.RelDutys = pety.RelDutys;
                        entity.bllxcode = pety.bllxcode;
                        entity.MenuLev = pety.MenuLev;
                    }
                    else
                    {
                        throw new FailedException("病历大类不存在，请重新选择！");
                    }
                }
                else
                {
                    sql = "select 1 from " + entity.relTB;
                    try
                    {
                        int tbcheck = this.ExecuteSqlCommand(sql);
                    }
                    catch (Exception ex)
                    {
                        throw new FailedException("请检查业务表关联！" + ex.Message);
                    }
                }
                entity.CreateTime = DateTime.Now;
                entity.zt = "1";
                entity.IsRoot = string.IsNullOrWhiteSpace(entity.ParentId) == true ? "1" : "0";
                entity.Create(true);
                this.Insert(entity);
            }
        }

        public void DeleteForm(string keyValue,string orgId,string user)
        {
            if (!string.IsNullOrWhiteSpace(keyValue) && !string.IsNullOrWhiteSpace(orgId))
            {
                bl_bllxEntity ety = this.FindEntity(p => p.Id == keyValue && p.OrganizeId == orgId && p.zt == "1");
                if (ety!=null)
                {
                    ety.zt = "0";
                    ety.LastModifierCode = user;
                    ety.LastModifyTime = DateTime.Now;
                    ety.Modify(keyValue);
                    this.Update(ety);
                }
                else
                {
                    throw new FailedException("病历类型不存在或已删除，请重新选择！");
                }
            }
            else
            {
                throw new FailedException("病历类型不存在或已删除，请重新选择！");
            }
        }

    }
}