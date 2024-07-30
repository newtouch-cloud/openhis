using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.Repository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2018-06-28 13:27
    /// 描 述：住院病人生命体征
    /// </summary>
    public class InpatientVitalSignsRepo : RepositoryBase<InpatientVitalSignsEntity>, IInpatientVitalSignsRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public InpatientVitalSignsRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 护理分页查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<InpatientVitalSignsDto> GetPaginationList(Pagination pagination, string orgId, DateTime? kssj, DateTime? jssj, string zyh,string wardCode,string isShowDelete)
        {
			//            var sb = new StringBuilder();
			//            sb.Append(@"declare @strhljb varchar(1000),@hljb int=0
			//	select @strhljb=value 
			//	from [dbo].[Sys_Config] with(nolock)
			//	where code='NursinLevelChargeItemMapp' and organizeid=@orgId and zt=1

			//	create table #tmp( num int identity(1,1) primary key,col varchar(200))

			//	insert into #tmp
			//	select col from dbo.f_split(@strhljb,',')

			//select a.*,b.xm,j.sfxmmc hljb,itemhlys.Name hlysname,itembrfood.Name brfoodname,itempfqk.Name pfqkname,itemgdhl.Name gdhlname from zy_brsmtz(nolock) a 
			//left join zy_brxxk b on a.zyh=b.zyh and a.OrganizeId=b.OrganizeId and b.zt='1'
			//LEFT JOIN NewtouchHIS_Base..V_C_Sys_ItemsDetail itemhlys ON ( itemhlys.OrganizeId = a.OrganizeId
			//                                                              OR itemhlys.OrganizeId = '*'
			//                                                              )
			//                                                              AND itemhlys.CateCode = 'NursingCognition'
			//                                                              AND itemhlys.Code = a.hlys
			//LEFT JOIN NewtouchHIS_Base..V_C_Sys_ItemsDetail itembrfood ON ( itembrfood.OrganizeId = a.OrganizeId
			//                                                              OR itembrfood.OrganizeId = '*'
			//                                                              )
			//                                                              AND itembrfood.CateCode = 'NursingFood'
			//                                                              AND itembrfood.Code = a.brfood
			//LEFT JOIN NewtouchHIS_Base..V_C_Sys_ItemsDetail itempfqk ON ( itempfqk.OrganizeId = a.OrganizeId
			//                                                              OR itempfqk.OrganizeId = '*'
			//                                                              )
			//                                                              AND itempfqk.CateCode = 'NursingSkin'
			//                                                              AND itempfqk.Code = a.pfqk
			//LEFT JOIN NewtouchHIS_Base..V_C_Sys_ItemsDetail itemgdhl ON ( itemgdhl.OrganizeId = a.OrganizeId
			//                                                              OR itemgdhl.OrganizeId = '*'
			//                                                              )
			//                                                              AND itemgdhl.CateCode = 'PipelineNursing'
			//                                                              AND itemgdhl.Code = a.gdhl
			//left join #tmp i on i.num=b.hljb
			//left join NewtouchHIS_Base..V_S_xt_sfxm j on j.sfxmCode=i.col and j.OrganizeId=a.OrganizeId and j.zt='1'
			//where a.OrganizeId = @orgId and a.zt = '1' ");
			//            var pars = new List<SqlParameter>();
			//            pars.Add(new SqlParameter("@orgId", orgId));
			//            if (kssj.HasValue)
			//            {
			//                sb.Append(" and a.rq >= @kssj");
			//                pars.Add(new SqlParameter("@kssj", kssj.Value.Date));
			//            }
			//            if (jssj.HasValue)
			//            {
			//                sb.Append(" and a.rq < @jssj");
			//                pars.Add(new SqlParameter("@jssj", jssj.Value.Date.AddDays(1)));
			//            }
			//            if (!string.IsNullOrWhiteSpace(zyh))
			//            {
			//                sb.Append(" and a.zyh like @searchZyh");
			//                pars.Add(new SqlParameter("@searchZyh", "%" + zyh.Trim() + "%"));
			//            }
			//            sb.Append(@" order by zyh,rq,sj 
			//                drop table #tmp");
			//            return this.FindList<InpatientVitalSignsDto>(sb.ToString(), pars.ToArray());

			//2021-2-2 解决Chinese_PRC_CI_AS 改为查询存储过程
			var pars = new List<SqlParameter>();
			pars.Add(new SqlParameter("@orgId", orgId));
			pars.Add(new SqlParameter("@kssj", kssj.Value.Date));
			pars.Add(new SqlParameter("@jssj", jssj.Value.Date.AddDays(1)));
			pars.Add(new SqlParameter("@zyh", "%" + zyh.Trim() + "%"));
			pars.Add(new SqlParameter("@wardCode", "%" + wardCode.Trim() + "%"));
			pars.Add(new SqlParameter("@isShowDelete", isShowDelete));
			return this.FindList<InpatientVitalSignsDto>("EXEC [dbo].[InpatientVitalSigns] @orgId=@orgId,@kssj=@kssj,@jssj=@jssj,@zyh=@zyh ,@wardCode=@wardCode,@isShowDelete=@isShowDelete", pars.ToArray());

		}

		/// <summary>
		/// 护理查询(不分页)
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="orgId"></param>
		/// <param name="kssj"></param>
		/// <param name="jssj"></param>
		/// <param name="zyh"></param>
		/// <returns></returns>
		public IList<InpatientVitalSignsDto> GetList(string orgId, DateTime? kssj, DateTime? jssj, string zyh)
        {
            var sb = new StringBuilder();
            sb.Append(@"select a.*,b.xm,b.hljb from zy_brsmtz(nolock) a left join zy_brxxk b on a.zyh=b.zyh and a.OrganizeId=b.OrganizeId
where a.OrganizeId = @orgId and a.zt = '1'");
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            if (kssj.HasValue)
            {
                sb.Append(" and a.rq >= @kssj");
                pars.Add(new SqlParameter("@kssj", kssj.Value.Date));
            }
            if (jssj.HasValue)
            {
                sb.Append(" and a.rq < @jssj");
                pars.Add(new SqlParameter("@jssj", jssj.Value.Date.AddDays(1)));
            }
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                sb.Append(" and a.zyh like @searchZyh");
                pars.Add(new SqlParameter("@searchZyh", "%" + zyh.Trim() + "%"));
            }

            return this.FindList<InpatientVitalSignsDto>(sb.ToString(), pars.ToArray());
        }

        /// <summary>
        /// 护理查询
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<InpatientVitalSignsEntity> GetValidList(string orgId, DateTime? kssj, DateTime? jssj, string zyh)
        {
            var sb = new StringBuilder();
            sb.Append(@"select * from zy_brsmtz(nolock) 
where OrganizeId = @orgId and zt = '1'");
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            if (kssj.HasValue)
            {
                sb.Append(" and rq >= @kssj");
                pars.Add(new SqlParameter("@kssj", kssj.Value.Date));
            }
            if (jssj.HasValue)
            {
                sb.Append(" and rq < @jssj");
                pars.Add(new SqlParameter("@jssj", jssj.Value.Date.AddDays(1)));
            }
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                sb.Append(" and zyh like @searchZyh");
                pars.Add(new SqlParameter("@searchZyh", "%" + zyh.Trim() + "%"));
            }

            //按时间排序
            sb.Append(" order by rq,sj");

            return this.FindList<InpatientVitalSignsEntity>(sb.ToString(), pars.ToArray());
        }


        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(InpatientVitalSignsEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable(p => p.zyh == entity.zyh && p.rq == entity.rq && p.sj == entity.sj && p.Id != keyValue && p.zt == "1").Any())
                {
                    throw new FailedException("InpatientVitalSigns_Repeated_Error", "重复录入");
                }
                var dbEntity = this.FindEntity(keyValue);
                if (dbEntity.OrganizeId != entity.OrganizeId)
                {
                    throw new FailedException("Error", "操作异常");
                }
                //properties
                var ignoreProps = new List<string>() { "Id", "CreateTime", "CreatorCode" };
                entity.MapperTo(dbEntity, ignoreProps);
                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
            }
            else
            {
                if (this.IQueryable(p => p.zyh == entity.zyh && p.rq == entity.rq && p.sj == entity.sj && p.zt == "1").Any())
                {
                    throw new FailedException("InpatientVitalSigns_Repeated_Error", "重复录入");
                }
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
            var dbEntity = this.FindEntity(keyValue);
            dbEntity.zt = "0";  //无效
            dbEntity.Modify(keyValue);
            this.Update(dbEntity);
        }


        /*****************************************************************/
        /// <summary>
        /// 获取三测单绘制所需数据
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<TemperatureGraphData> GetTempratureData(string orgId, DateTime? kssj, DateTime? jssj, string zyh, int pagesize =6)
        {
            string sql = @"exec usp_tempraturedata @orgId=@orgId,@kssj=@kssj,@jssj=@jssj,@zyh=@zyh,@pagesize=@pagesize ";
            return FindList<TemperatureGraphData>(sql, new SqlParameter[] {
                new SqlParameter("orgId",orgId),
                new SqlParameter("kssj",kssj.ToDateString()),
                new SqlParameter("jssj",jssj.ToDateString()),
                new SqlParameter("zyh",zyh??""),
                new SqlParameter("pagesize",pagesize.ToString()),
            });
        }

    }
}