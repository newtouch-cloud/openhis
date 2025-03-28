using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Repository
{
    public class MrFeeRepo : RepositoryBase<bafeeEntity>, IMrFeeRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public MrFeeRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
		
        public int SubmitForm(bafeeVO entity, string keyValue,string OrganizeId,bafeeVO oldEntity)
        {
			var flag = (oldEntity.Lev == 1 && entity.name1 == oldEntity.name1) || (oldEntity.Lev == 2 && entity.name1 == oldEntity.name1 && entity.name2 == oldEntity.name2);//true执行新增

			if ( (!string.IsNullOrEmpty(keyValue)) && (!flag) )
            {
                //if (this.IQueryable().Any(p => p.ssdm == entity.ssdm && p.Id != keyValue && p.zt == "1"))
                //{
                //    throw new FailedException("编号不可重复");
                //}
                //var dbEntity = this.FindEntity(keyValue);
                var dbEntity = new bafeeEntity();
				//properties
				dbEntity.Id = keyValue;
                dbEntity.OrganizeId = OrganizeId;
                dbEntity.Name = entity.name;
                dbEntity.py = entity.py;
				var code = getCode(dbEntity.Lev, dbEntity.ParentCode,OrganizeId).ToString();
				dbEntity.ShortCode = entity.ShortCode == null ? "-" : entity.ShortCode; ;
                if (entity.code1 == null)
                {
                    dbEntity.Lev = 1;
                    dbEntity.ParentCode = null;
                }
                else if (entity.code2 == null)
                {
                    dbEntity.Lev = 2;
                    dbEntity.ParentCode = entity.code1;
                }
                else
                {
                    dbEntity.Lev = 3;
                    dbEntity.ParentCode = entity.code2;
                }
                if (entity.Lev == dbEntity.Lev && entity.ParentCode == dbEntity.ParentCode)
                {
                    //大类未修改
                    dbEntity.px = entity.px;
                    dbEntity.Code = entity.code;
                }
                else
                {
                    dbEntity.px = getPx(dbEntity.Lev, dbEntity.ParentCode,OrganizeId);//获取最大px+1
					if (dbEntity.Lev == 1)
					{
						//dbEntity.Code = dbEntity.px.ToString();
						dbEntity.Code = code;
					}
					else
					{
						if (code == "1")
						{
							dbEntity.Code = dbEntity.ParentCode + code.ToString().PadLeft(2, '0');
						}
						else
						{
							dbEntity.Code = code;
						}
						//dbEntity.Code = dbEntity.ParentCode+dbEntity.px.ToString().PadLeft(2, '0');
					}
                }
                dbEntity.zt = "1";

				dbEntity.Modify(keyValue);
				return Update(dbEntity);
				//return 0;
			}
            else
            {
                var dbEntity = new bafeeEntity();
                dbEntity.OrganizeId = OrganizeId;
                dbEntity.Name = entity.name;
                dbEntity.py = entity.py;
                dbEntity.ShortCode = entity.ShortCode==null?"-": entity.ShortCode;
                if (entity.code1 == null)
                {
                    dbEntity.Lev = 1;
                    dbEntity.ParentCode = null;
                }
                else if (entity.code2 == null)
                {
                    dbEntity.Lev = 2;
                    dbEntity.ParentCode = entity.code1;
                }
                else {
                    dbEntity.Lev = 3;
                    dbEntity.ParentCode = entity.code2;
                }
                dbEntity.px =getPx(dbEntity.Lev,dbEntity.ParentCode,OrganizeId);
				var code= getCode(dbEntity.Lev, dbEntity.ParentCode, OrganizeId).ToString();
				if (dbEntity.Lev == 1)
                {
					//dbEntity.Code = dbEntity.px.ToString();
					dbEntity.Code = code;
				}
                else {
					if (code == "1")
					{
						dbEntity.Code = dbEntity.ParentCode + code.ToString().PadLeft(2, '0');
					}
					else {
						dbEntity.Code = code;
					}
                    //dbEntity.Code = dbEntity.ParentCode+dbEntity.px.ToString().PadLeft(2, '0');
                }
				dbEntity.Create(true);
				return Insert(dbEntity);
				//return 1;
			}
        }

        //public IList<int> SelectPx(int Lev, string parentCode)
        //{
        //    string sql = "select px from mr_dic_bafeetype  where 1=1 and zt='1'";
        //    var para = new List<SqlParameter>();
        //    if (Lev != 0)
        //    {
        //        sql += " and lev=@lev";
        //        para.Add(new SqlParameter("lev", Lev));
        //    }
        //    if (!string.IsNullOrWhiteSpace(parentCode))
        //    {
        //        sql += " and parentCode=@parentCode";
        //        para.Add(new SqlParameter("@parentCode", parentCode));
        //    }
        //    var result = this.FindList<IList<int>>(sql, para.ToArray()).FirstOrDefault();
        //    return result;
        //}

        public int getPx(int Lev, string parentCode,string OrganizeId) {
            string sql = "select isnull(Max(px),0)+1 px  from mr_dic_bafeetype  where zt='1'";
            var para = new List<SqlParameter>();
            if (Lev != 0) {
                sql += " and lev=@lev";
                para.Add(new SqlParameter("lev", Lev));
            }
            if (!string.IsNullOrWhiteSpace(parentCode))
            {
                sql += " and parentCode=@parentCode";
                para.Add(new SqlParameter("@parentCode", parentCode));
			}
			if (!string.IsNullOrWhiteSpace(OrganizeId))
			{
				sql += " and OrganizeId=@OrganizeId";
				para.Add(new SqlParameter("@OrganizeId", OrganizeId));
			}
			var result= this.FindList<int>(sql, para.ToArray()).FirstOrDefault();
            //result = result == null ? 0 : result;
            return result;
        }

		public int getCode(int Lev, string parentCode,string OrganizeId)
		{
			string sql = "select isnull(Max(CONVERT(int,Code)),0)+1 code  from mr_dic_bafeetype  where zt='1' ";
			var para = new List<SqlParameter>();
			if (Lev != 0)
			{
				sql += " and lev=@lev";
				para.Add(new SqlParameter("lev", Lev));
			}
			if (!string.IsNullOrWhiteSpace(parentCode))
			{
				if (Lev == 1)
				{
					sql += " and parentCode is null";
				}
				else
				{
					sql += " and parentCode=@parentCode";
					para.Add(new SqlParameter("@parentCode", parentCode));
				}
			}
			if (!string.IsNullOrWhiteSpace(OrganizeId))
			{
				sql += " and OrganizeId=@OrganizeId";
				para.Add(new SqlParameter("@OrganizeId", OrganizeId));
			}
			var result = this.FindList<int>(sql, para.ToArray()).FirstOrDefault();
			//result = result == null ? 0 : result;
			return result;
		}

		public int DeleteForm(string keyValue)
        {
            var dbEntity = this.FindEntity(keyValue);
            //properties
            dbEntity.zt = "0";
            dbEntity.Modify(keyValue);
            return Update(dbEntity);
        }

		public IList<bafeeEntity> GetAllFeeList(string orgId)
		{
			string sql = @"select * from mr_dic_bafeetype with(nolock) where zt='1'  and organizeid=@orgId";

			return this.FindList<bafeeEntity>(sql, new SqlParameter[] {
				new SqlParameter("@orgId",orgId)
			});
		}
	}
}
