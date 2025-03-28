using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.Repository
{
    public class SysDoctorRemarkRepo : RepositoryBase<SysDoctorRemarkEntity>, ISysDoctorRemarkRepo
    {
        public SysDoctorRemarkRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<SysDoctorRemarkVO> GetListByOrg(string orgId)
        { 
            var sql = @"
select zt.*,Department.Name as ksmc from xt_zt zt
left join NewtouchHIS_Base..Sys_Department Department 
on Department.Code = zt.ksCode and Department.OrganizeId=zt.OrganizeId
where zt.OrganizeId=@orgId
                    ";

            var list = this.FindList<SysDoctorRemarkVO>(sql, new[] { new SqlParameter("@orgId", orgId) });
            return list;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysDoctorRemarkEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId && p.ztCode == entity.ztCode && p.Id != keyValue))
                {
                    throw new FailedException("编号不可重复");
                }

                entity.Modify(keyValue);
                this.Update(entity);
            }
            else
            {
                if (this.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId && p.ztCode == entity.ztCode))
                {
                    throw new FailedException("编号不可重复");
                }
                entity.Create(true);
                this.Insert(entity);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="orgId"></param>
        public void DeleteForm(string keyValue)
        {
            var entity = this.FindEntity(keyValue);
            this.Delete(entity);
        }



        #region 医生站--》文字录入--》指示文本框按钮

//        public List<SyssurgeryTextVO> surgery_record(string OrganizeId) {

//            var sql =string.Format(@"select a.ssmc,CONVERT(varchar(10), b.sssj, 23) as sssj, c.Name
//from [Newtouch_OR].[dbo].[OR_ApplyInfo_Expand] a 
//left join [Newtouch_OR].[dbo].[OR_ApplyInfo] b on a.zyh=b.zyh and a.zt=1
//left join [NewtouchHIS_Base].[dbo].[V_S_Sys_Staff] c on b.ysgh=c.gh and b.zt=1
//where a.zt=1 and a.OrganizeId='" + OrganizeId + "' group by a.ssmc,b.sssj,c.Name");

//            return this.FindList<SyssurgeryTextVO>(sql);

//        }
        

        #endregion




    }
}
