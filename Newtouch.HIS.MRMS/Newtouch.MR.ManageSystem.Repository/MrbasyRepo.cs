using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using System;

namespace Newtouch.MR.ManageSystem.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-11-20 11:05
    /// 描 述：病案首页
    /// </summary>
    public class MrbasyRepo : RepositoryBase<MrbasyEntity>, IMrbasyRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public MrbasyRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public int SubmitForm(MrbasyEntity entity, string keyValue)
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
        public int DeleteForm(string keyValue)
        {
            return Delete(p => p.Id == keyValue);
        }

        public IList<MrbasyEntity> GetPagintionHospitalList(Pagination pagination, string organizeId, string jkkh, string keyword)
        {
	        var sql = new StringBuilder();
	        sql.Append("select  * from  [dbo].[mr_basy]  where 1 = 1 and zt=1");

	        List<SqlParameter> pars = null;
	        if (!string.IsNullOrWhiteSpace(organizeId))
	        {
		        pars = pars ?? new List<SqlParameter>();
		        sql.Append(" and organizeid = @organizeId");
		        pars.Add(new SqlParameter("@organizeId", organizeId));
	        }
	        if (!string.IsNullOrWhiteSpace(jkkh))
	        {
		        pars = pars ?? new List<SqlParameter>();
		        sql.Append(" and jkkh like @jkkh");
		        pars.Add(new SqlParameter("@jkkh", "%" + jkkh.Trim() + "%"));
	        }
	        if (!string.IsNullOrWhiteSpace(keyword))
	        {
		        pars = pars ?? new List<SqlParameter>();
		        sql.Append(" and (1=2 ");
				sql.Append("or zyh like @keyword ");
				sql.Append(" or xm like @keyword )");
		        pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
	        }
	        return this.QueryWithPage<MrbasyEntity>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
        }




        /// <summary>
        /// 修改归档状态
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="ZYH"></param>
        /// <returns></returns>
        public int Updatebazt(string dataId, string ZYH,string XM)
        {
            var GDRQ= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var sql = "update  [Newtouch_MRMS].[dbo].[mr_basy] set bazt='3',GDRQ='"+ GDRQ + "' where Id='"+dataId+"' and ZYH='"+ZYH+"' and XM='"+XM+"'";
           var bazt1=  ExecuteSqlCommand(sql);
            var sql2 = "update  [Newtouch_EMR].[dbo].[mr_basy] set bazt='3',GDRQ='" + GDRQ + "' where  ZYH='" + ZYH + "' and XM='" + XM + "'";
            var bazt2= ExecuteSqlCommand(sql2);
            if (bazt1!=0 && bazt2 != 0)
            {
                return 1;
            }
            return 0;
        }
        //撤销病案状态
        public int CXUpdatebazt(string dataId, string ZYH, string XM, string organizeId, string LastModifierCode, DateTime LastModifyTime)
        {
            var GDRQ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var sql = "update  [Newtouch_MRMS].[dbo].[mr_basy] set bazt='2',GDRQ=null where Id=@dataId and ZYH=@ZYH and XM=@XM";
            
            SqlParameter[] para ={
                new SqlParameter("@dataId",dataId),
                new SqlParameter("@ZYH",ZYH),
                new SqlParameter("@XM",XM)
                };
            var data1= this.ExecuteSqlCommand(sql, para);

            string sql2 = "update [Newtouch_EMR].[dbo].[zy_brjbxx] set RecordStu=2,LastModifyTime=@LastModifyTime,LastModifierCode=@LastModifierCode where 1=1 and zt='1'";
            SqlParameter[] para2 ={
                new SqlParameter("@LastModifyTime",Convert.ToDateTime(LastModifyTime)),
                new SqlParameter("@LastModifierCode",LastModifierCode ?? ""),
                new SqlParameter("@organizeId",organizeId ?? "")
                };
            var data2 = this.ExecuteSqlCommand(sql2, para2);
            if (data1 != 0 && data2 != 0)
            {
                return 1;
            }
            else {
                return 0;
            }
        }

        public int CXGDZT(string dataId, string ZYH, string XM, string organizeId, string LastModifierCode, DateTime LastModifyTime)
        {
            var GDRQ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var sql = "update  [Newtouch_MRMS].[dbo].[mr_basy] set bazt='2',GDRQ=@GDRQ where Id=@dataId and ZYH=@ZYH and XM=@XM";

            SqlParameter[] para ={
                new SqlParameter("@GDRQ",GDRQ),
                new SqlParameter("@dataId",dataId),
                new SqlParameter("@ZYH",ZYH),
                new SqlParameter("@XM",XM)
                };
            var data1 = this.ExecuteSqlCommand(sql, para);
            if (data1 != 0 )
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}