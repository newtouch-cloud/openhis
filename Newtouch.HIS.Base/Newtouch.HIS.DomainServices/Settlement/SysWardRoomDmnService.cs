using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.HIS.DomainServices
{
    public class SysWardRoomDmnService: DmnServiceBase,ISysWardRoomDmnService
    {

        public SysWardRoomDmnService(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取房间列表（分页）
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysWardRoomVO> GetPagintionList(Pagination pagination, string organizeId, string keyword)
        {
            //关联项若无效也显示以做提示
            string sql = @"select a.[bfId],a.[OrganizeId],a.[bfCode],a.[bfNo],a.[bqCode],a.[ksCode],a.[bz],a.[zt],
                            a.[CreatorCode],a.[CreateTime],a.[LastModifyTime],a.[LastModifierCode],a.[px],b.bqmc,c.Name ksmc 
                            from xt_bf a 
                            left join xt_bq b on a.bqCode=b.bqCode and a.organizeid=b.organizeid 
                            left join Sys_Department c on a.organizeid=c.organizeid and a.kscode=c.code 
                            where  a.OrganizeId=@organizeId ";
            if (keyword != "")
            {
                sql += " and (charindex(@keyword,a.bfCode,1) >0 or charindex(@keyword,a.bfNo,1) >0 or charindex(@keyword,a.bqCode,1) >0)";
            }

            SqlParameter[] param = new SqlParameter[] {
                 new SqlParameter("@keyword",keyword ?? ""),
                 new SqlParameter("@organizeId",organizeId)
                           
            };
            return this.QueryWithPage<SysWardRoomVO>(sql, pagination, param);
        }

        /// <summary>
        /// 获取房间列表
        /// </summary>
        /// <param name="bfId"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysWardRoomVO> GetWardRoomList(int? bfId, string organizeId, string keyword)
        {
            //关联项若无效也显示以做提示
            string sql = @"select a.[bfId],a.[OrganizeId],a.[bfCode],a.[bfNo],a.[bqCode],a.[ksCode],a.[bz],a.[zt],
                            a.[CreatorCode],a.[CreateTime],a.[LastModifyTime],a.[LastModifierCode],a.[px],b.bqmc,c.Name ksmc 
                            from xt_bf a 
                            left join xt_bq b on a.bqCode=b.bqCode and a.organizeid=b.organizeid 
                            left join Sys_Department c on a.organizeid=c.organizeid and a.kscode=c.code 
                            where  a.OrganizeId=@organizeId ";
            if (bfId != null && bfId > 0)
            {
                sql += " and a.[bfId]=@bfId ";
            }
            if (keyword != "")
            {
                sql += " and (charindex(@keyword,a.bfCode,1) >0 or charindex(@keyword,a.bfNo,1) >0 or charindex(@keyword,a.bqCode,1) >0)";
            }

            SqlParameter[] param = new SqlParameter[] {
                 new SqlParameter("@keyword",keyword ?? ""),
                 new SqlParameter("@organizeId",organizeId),
                 new SqlParameter("@bfId",bfId)
            };
            return this.FindList<SysWardRoomVO>(sql, param);
        }

        /// <summary>
        /// 根据病区查找病房
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="bqCode"></param>
        /// <returns></returns>
        public IList<SysWardRoomVO> GetWardRoomListValid(string organizeId, string bqCode)
        {
            //关联项若无效也显示以做提示
            string sql = @"select a.[bfId],a.[OrganizeId],a.[bfCode],a.[bfNo],a.[bqCode],a.[ksCode],a.[bz],a.[zt],
                            a.[CreatorCode],a.[CreateTime],a.[LastModifyTime],a.[LastModifierCode],a.[px]
                            from xt_bf a 
                            where  a.OrganizeId=@organizeId and a.[zt]=1 ";
            if (bqCode != null && bqCode !="")
            {
                sql += " and a.[bqCode]=@bqCode ";
            }

            SqlParameter[] param = new SqlParameter[] {
                 new SqlParameter("@organizeId",organizeId),
                 new SqlParameter("@bqCode",bqCode)
            };
            return this.FindList<SysWardRoomVO>(sql, param);
        }
    }
}
