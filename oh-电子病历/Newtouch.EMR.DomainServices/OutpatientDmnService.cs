using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.EMR.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Newtouch.Infrastructure;
using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.EMR.DomainServices
{
    public class OutpatientDmnService:DmnServiceBase, IOutpatientDmnService
    {
        public OutpatientDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {

        }

        /// <summary>
        /// 已就诊 就诊中 列表( 已就诊： 只能查看自己名下的患者)
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="jzzt"></param>
        /// <param name="mjzbz"></param>
        /// <param name="rygh"></param>
        /// <param name="kzrq"></param> 
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<TreatEntityVO> GetTreatingOrTreatedList(Pagination pagination, string orgId,int mjzbz, string rygh, DateTime? kzrq, string keyword)
        {
            var sql = @"
SELECT jzId,jz.OrganizeId,mzh,mjzbz,jz.blh,jz.xm,jz.xb,jz.csny,brxzmc,jz.zjlx
,jz.zjh,ghksmc,ghys,ghsj,tizhong,tiwen,maibo,xueya,xuetang,fzbz,zlkssj,zljssj,jzks,jzys
,jzzt,jz.CreateTime,jz.CreatorCode,jz.LastModifyTime,jz.LastModifierCode,jz.zt
,ghczsj,jzysmc,brxzCode,ybjsh,cfzbz,jz.sbbh,jz.cbdbm,shengao,shousuoya,shuzhangya,jz.py
,ContactNum,kh,xx.xian_sheng province,xx.xian_shi city,xx.xian_xian county,xx.xian_dz address,jz.nlshow,jz.ghlybz,'"+((int)EnumRecordStu.wtj).ToString()+@"' RecordStu
FROM [Newtouch_CIS].dbo.xt_jz(NOLOCK) jz
LEFT JOIN NewtouchHIS_Sett.dbo.xt_brjbxx(NOLOCK)  xx on jz.blh=xx.blh and jz.OrganizeId=xx.OrganizeId and xx.zt='1'
--LEFT JOIN NewtouchHIS_Base.dbo.Sys_Staff(NOLOCK) ss ON ss.gh=@rygh AND ss.OrganizeId=jz.OrganizeId 
WHERE jz.zt='1' 
AND jz.OrganizeId=@orgId 
AND jzzt<>" + ((int)EnumJzzt.NotYetTreate).ToString()+@"
--AND ss.DepartmentCode=jz.jzks 
";
            var parlist = new List<SqlParameter>
            {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@rygh", rygh)
            };
            if (mjzbz > 0)
            {
                sql += "AND mjzbz=@mjzbz ";
                parlist.Add(new SqlParameter("@mjzbz", mjzbz));
            }
            //if (kzrq.HasValue)
            //{
            //    parlist.Add(new SqlParameter("@kzrq", kzrq.Value.ToString("yyyyMMdd")));
            //    switch (jzzt)
            //    {
            //        case (int)EnumJzzt.Treated:
            //            sql += "AND CONVERT(varchar, zljssj, 112 ) = @kzrq ";
            //            break;
            //        case (int)EnumJzzt.Treating:
            //            sql += "AND CONVERT(varchar, zlkssj, 112 ) = @kzrq ";
            //            break;
            //    }
            //}
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += "AND (jz.blh like @searchKeywrod or jz.mzh like @searchKeywrod or jz.xm like @searchKeywrod or jz.py like @searchKeywrod) ";
                parlist.Add(new SqlParameter("@searchKeywrod", "%" + keyword.Trim() + "%"));
            }

            return QueryWithPage<TreatEntityVO>(sql, pagination, parlist.ToArray());
        }

        public TreatEntityVO GetPatMzbymzh(string orgId, string mzh,string rygh)
        {
            var sql = @"
SELECT jzId,jz.OrganizeId,mzh,mjzbz,jz.blh,jz.xm,jz.xb,jz.csny,brxzmc,jz.zjlx
,jz.zjh,ghksmc,ghys,ghsj,tizhong,tiwen,maibo,xueya,xuetang,fzbz,zlkssj,zljssj,jzks,jzys
,jzzt,jz.CreateTime,jz.CreatorCode,jz.LastModifyTime,jz.LastModifierCode,jz.zt
,ghczsj,jzysmc,brxzCode,ybjsh,cfzbz,jz.sbbh,jz.cbdbm,shengao,shousuoya,shuzhangya,jz.py
,ContactNum,kh,xx.xian_sheng province,xx.xian_shi city,xx.xian_xian county,xx.xian_dz address,jz.nlshow,jz.ghlybz,'" + ((int)EnumRecordStu.wtj).ToString() + @"' RecordStu
FROM [Newtouch_CIS].dbo.xt_jz(NOLOCK) jz
LEFT JOIN NewtouchHIS_Sett.dbo.xt_brjbxx(NOLOCK)  xx on jz.blh=xx.blh and jz.OrganizeId=xx.OrganizeId and xx.zt='1'
--LEFT JOIN NewtouchHIS_Base.dbo.Sys_Staff(NOLOCK) ss ON ss.gh=@rygh AND ss.OrganizeId=jz.OrganizeId 
WHERE jz.zt='1' and mzh=@mzh
AND jz.OrganizeId=@orgId 
AND jzzt<>" + ((int)EnumJzzt.NotYetTreate).ToString() + @"
--AND ss.DepartmentCode=jz.jzks 
";
            var parlist = new List<SqlParameter>
            {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@mzh", mzh),
                new SqlParameter("@rygh", rygh)
            };
            return FirstOrDefault<TreatEntityVO>(sql, parlist.ToArray());
        }


        /// <summary>
        /// 获取患者病历树
        /// </summary>
        /// <param name="OrgId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<PatMedRecordTreeVO> GetOutPatMedRecordTree(string OrgId, string mzh, string rygh)
        {
            try
            {
                SqlParameter[] para = new SqlParameter[] {
                            new SqlParameter("@mzh", mzh??""),
                            new SqlParameter("@OrgId", OrgId),
                            new SqlParameter("@rygh", rygh)
                };

                string sql = "exec  usp_Pat_MedRecordTree_MZ @OrgId=@OrgId,@mzh=@mzh,@rygh=@rygh ";
                return this.FindList<PatMedRecordTreeVO>(sql, para);

            }
            catch (Exception ex)
            {
                throw new FailedException("获取病历列表异常，" + ex.InnerException);
            }
        }
    }

}