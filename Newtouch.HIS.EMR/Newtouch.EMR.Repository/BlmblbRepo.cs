using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using Newtouch.Core.Common;
using System.Data.SqlClient;
using System.Text;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common.Exceptions;
using System.Linq;
using System;
using Newtouch.Common.Operator;
using FrameworkBase.MultiOrg.Domain.IRepository;
using System.Data;
using Newtouch.EMR.Domain.ValueObjects.MedicalRecord;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2018-09-05 11:34
    /// 描 述：病历模板列表
    /// </summary>
    public class BlmblbRepo : RepositoryBase<BlmblbEntity>, IBlmblbRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public BlmblbRepo(IDefaultDatabaseFactory databaseFactory, ISysUserRoleRepo SysRoleRepo)
            : base(databaseFactory)
        {
            
        }

        public BlmblbEntity GetTemplateById(string ID)
        {

            return this.FindEntity(p => p.Id == ID);
        }
        public BlmblbEntity GetTemplateByBllxId(string bllxid, string OrganizeId)
        {

            return this.FindEntity(p => p.bllxId == bllxid && p.OrganizeId == OrganizeId && p.zt == "1");
        }

        public List<BlmblbEntity> GetParentTemplate(string OrganizeId)
        {
            return this.IQueryable().Where(p => p.OrganizeId == OrganizeId && p.zt == "1").OrderByDescending(p => p.CreateTime).ToList();

        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(BlmblbEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(entity.mbbm))
            {
                var checkety = this.FindEntity(p => p.Id != keyValue && p.mbbm == entity.mbbm && p.OrganizeId == entity.OrganizeId && p.zt == "1");
                if (checkety != null)
                {
                    throw new FailedException("该模板编码已存在，不可重复");
                }
                //文件迁移需要 名称也不可重复
                checkety = this.FindEntity(p => p.Id != keyValue && p.mbmc == entity.mbmc && p.OrganizeId == entity.OrganizeId && p.zt == "1");
                if (checkety != null)
                {
                    throw new FailedException("该模板名称已存在，不可重复");
                }
            }
            if (entity.zt == "true")
            {
                entity.zt = "1";
            }
            else
            {
                entity.zt = "0";
            }

            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                dbEntity.zt = entity.zt;
                dbEntity.mbqx = entity.mbqx;
                dbEntity.mblj = entity.mblj;
                dbEntity.ksbm = entity.ksbm;
                dbEntity.ysgh = entity.ysgh;
                dbEntity.mbmc = entity.mbmc;
                dbEntity.Isempty = entity.Isempty;
                dbEntity.IsYB = entity.IsYB == "true" ? "1" : "0";
                dbEntity.Ybbm = entity.Ybbm;
                dbEntity.mzbz = entity.mzbz;
                dbEntity.EnableDataLoad = entity.EnableDataLoad == "true" ? "1" : "0";
                dbEntity.DataSource = entity.DataSource;
                dbEntity.LoadWay = entity.LoadWay;

                //properties
                dbEntity.LastModifyTime = DateTime.Now;
                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
            }
            else
            {
                entity.IsYB = entity.IsYB == "true" ? "1" : "0";
                entity.EnableDataLoad = entity.EnableDataLoad == "true" ? "1" : "0";
                entity.Create(true);
                this.Insert(entity);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public void DeleteForm(string keyValue)
        {
            this.Delete(p => p.Id == keyValue);
        }


        public List<BlmblbEntity> GetPagintionList(Pagination pagination, string OrganizeId, string keyword, OperatorModel user)
        {
            string sql = @"SELECT [Id],[OrganizeId],[mbqx],[mbbm],[mbmc],[bllxId],[bllxmc],[ksbm],[ysgh],[mblj],[py]
,[Isempty],[Memo],[CreateTime],[CreatorCode],[LastModifyTime],[LastModifierCode],[zt],bllx,IsYB，Ybbm
FROM[dbo].[bl_mblb] with(nolock)
where zt=1 and OrganizeId=@OrgId
                           ";

            var para = new List<SqlParameter>();
            para.Add(new SqlParameter("@OrgId", OrganizeId));
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and (charindex(@keyword,mbmc)>0  or charindex(@keyword,mbbm)>0) ";
                para.Add(new SqlParameter("@keyword", keyword));
            }

            return this.QueryWithPage<BlmblbEntity>(sql, pagination, para.ToArray()).ToList();

        }





        //护理记录数据提交
        public void BtnSubmit(OperatorModel user, IList<HljldataVO> data)
        {
            
            foreach (var item in data)
            {
                var sql = @"insert into [Newtouch_EMR].[dbo].[bl_hljldata] values(
newId(),'"+item.blId+ "','" + item.jlrq + "','" + item.tw+ "','" + item.mb + "','" + item.hx + "','" + item.xy + "','" + item.ybhd + "','" + item.cxxdjc + "'" +
",'" + item.xroyx + "','" + item.hljb + "','" + item.xzjs + "','" + item.pbjyxkt + "','" + item.ycyf + "','" + item.ddyf + "','" + item.qtjh + "'" +
",'" + item.zkhl + "','" + item.dglb + "','" + item.hlzd + "','" + item.nl + "','" + item.wy + "','" + item.bqhlcontent + "','" + item.hsqm + "'" +
",'1','" + DateTime.Now.ToString() + "','" + user.UserCode + "',NULL,'" + item.LastModifierCode + "','" + user.OrganizeId + "')";

                //var count = this.FindList<HljldataVO>(sql);
                ExecuteSqlCommand(sql);
            }
           
        }

        //护理记录页面加载
        public IList<HljldataVO> HljlLoadData(Pagination pagination, string blId, string zyh,string orgId)
        {
            string sql = @"select * from [Newtouch_EMR].dbo.bl_hljldata a
left join [Newtouch_EMR].[dbo].[bl_hljl] b
on a.blId=b.Id and a.OrganizeId=b.OrganizeId
where a.OrganizeId=@orgId";
            return this.QueryWithPage<HljldataVO>(sql, pagination, new[] {
                new SqlParameter("@orgId", orgId)
            }, false);
        }
    }
}