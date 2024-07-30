using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects.API;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 就诊信息
    /// </summary>
    public class TreatmentRepo : RepositoryBase<TreatmentEntity>, ITreatmentRepo
    {
        public TreatmentRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
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
        public IList<TreatEntityObj> GetTreatingOrTreatedList(Pagination pagination, string orgId, int jzzt, int mjzbz, string rygh, DateTime? kzrq, string keyword, bool? zzhz)
        {
            var sql = @"
SELECT jzId,jz.OrganizeId,jz.mzh,mjzbz,jz.blh,jz.xm,jz.xb,jz.csny,brxzmc,jz.zjlx
,jz.zjh,ghksmc,ghys,ghsj,tizhong,tiwen,maibo,xueya,xuetang,fzbz,zlkssj,zljssj,jzks,jzys
,jzzt,jz.CreateTime,jz.CreatorCode,jz.LastModifyTime,jz.LastModifierCode,jz.zt
,ghczsj,jzysmc,brxzCode,ybjsh,cfzbz,jz.sbbh,jz.cbdbm,shengao,shousuoya,shuzhangya,jz.py
,ContactNum,jz.kh,xx.xian_sheng province,xx.xian_shi city,xx.xian_xian county,xx.xian_dz address,jz.nlshow,jz.ghlybz,queue.queno,case when zz.ghnm is not null then '转诊' else '' end zzbs
FROM dbo.xt_jz(NOLOCK) jz
LEFT JOIN NewtouchHIS_Sett.dbo.xt_brjbxx(NOLOCK)  xx on jz.blh=xx.blh and jz.OrganizeId=xx.OrganizeId and xx.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Staff(NOLOCK) ss ON ss.gh=@rygh AND ss.OrganizeId=jz.OrganizeId 
LEFT JOIN NewtouchHIS_Sett.dbo.queue_schedule(NOLOCK) queue on jz.mzh=queue.ywlsh
left join NewtouchHIS_Sett..mz_zzjl(nolock) zz on zz.mzh = jz.mzh and zz.OrganizeId=jz.OrganizeId
WHERE jz.zt='1' 
AND jz.OrganizeId=@orgId 
AND jzzt=@jzzt
AND ss.DepartmentCode=jz.jzks 
";
            var parlist = new List<SqlParameter>
            {
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@jzzt", jzzt),
                new SqlParameter("@rygh", rygh)
            };
            if (mjzbz > 0)
            {
                sql += "AND mjzbz=@mjzbz ";
                parlist.Add(new SqlParameter("@mjzbz", mjzbz));
            }
            if (kzrq.HasValue)
            {
                parlist.Add(new SqlParameter("@kzrq", kzrq.Value.ToString("yyyyMMdd")));
                switch (jzzt)
                {
                    case (int)EnumJzzt.Treated:
                        sql += "AND CONVERT(varchar, zljssj, 112 ) = @kzrq ";
                        break;
                    case (int)EnumJzzt.Treating:
                        sql += "AND CONVERT(varchar, zlkssj, 112 ) = @kzrq ";
                        break;
                }
            }
			if (zzhz == true)
			{
                sql += " AND zz.ghnm is not null ";
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += "AND (jz.blh like @searchKeywrod or jz.mzh like @searchKeywrod or jz.xm like @searchKeywrod or jz.py like @searchKeywrod) ";
                parlist.Add(new SqlParameter("@searchKeywrod", "%" + keyword.Trim() + "%"));
            }

            return QueryWithPage<TreatEntityObj>(sql, pagination, parlist.ToArray());
        }

		public IList<PatientRegInfoDTO> GetdjzList(Pagination pagination, string keyword, int mjzbz)
		{
			var sql = @"select * from (
select 
Mdtrt_ID,--医保就诊id
PSN_NO,--
PSN_NAME,--
CERTNO,--身份证号
PSN_CERT_TYPE,--
a.MED_TYPE,
MZHM, --社保卡号
convert(varchar(20),AGE)AGE,
convert(varchar(20),BRXB)BRXB,
convert(varchar(50),SBXH)SBXH,--挂号内码
convert(varchar(50),BRID)BRID,
convert(varchar(50),INSUTYPE)INSUTYPE,b.ecToken
from [192.168.0.100].[bshrp].[dbo].[kk_1] a
left join NewtouchHIS_Sett..mz_gh b  on b.ecToken is not null and b.ecToken=a.SBXH and b.zt='1'
)q where ecToken is null and PSN_NAME like @keyword
";
			var parlist = new List<SqlParameter>{
				 new SqlParameter("@orgId", mjzbz),
				  new SqlParameter("@keyword", '%'+keyword.Trim()+'%'),
			};

			return QueryWithPage<PatientRegInfoDTO>(sql, pagination, parlist.ToArray());
		}

		public IList<RegisteredInfoRespVO> GetghList(string xm, string blh, string orgid)
		{
			var sql = @"select 
Mdtrt_ID,--医保就诊id
PSN_NO,--
PSN_NAME,--
CERTNO,--身份证号
PSN_CERT_TYPE,--
MED_TYPE,
MZHM, --社保卡号
convert(varchar(20),AGE)AGE,
convert(varchar(20),BRXB)BRXB,
convert(varchar(50),SBXH)SBXH,--挂号内码
convert(varchar(50),BRID)BRID,
convert(varchar(50),INSUTYPE)INSUTYPE
from [192.168.0.100].[bshrp].[dbo].[kk_1]  
";
			var parlist = new List<SqlParameter>{
				 new SqlParameter("@orgId", orgid)
			};

			return FindList<RegisteredInfoRespVO>(sql, parlist.ToArray());
		}
		
		/// <summary>
		/// 恢复就诊
		/// </summary>
		/// <param name="orgId"></param>
		/// <param name="jzId"></param>
		public void ResumeTreat(string orgId, string jzId)
        {
            var entity = this.FindEntity(a => a.jzId == jzId && a.OrganizeId == orgId && a.zt == "1");
            if (entity == null)
            {
                throw new FailedException("未查询到就诊记录，恢复就诊失败");
            }
            entity.jzzt = (int)EnumJzzt.Treating;
            entity.Modify();
            this.Update(entity);
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="jzId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public TreatmentEntity SelectData(string jzId, string organizeId)
        {
            const string sql = @"
SELECT * FROM dbo.xt_jz(NOLOCK) 
WHERE jzId=@jzId AND OrganizeId=@OrganizeId AND zt='1'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@jzId", jzId),
                new SqlParameter("@OrganizeId",organizeId )
            };
            return FirstOrDefault<TreatmentEntity>(sql, param);
        }

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<TreatmentEntity> SelectDataByMzh(string mzh, string organizeId)
        {
            const string sql = @"
SELECT * FROM dbo.xt_jz(NOLOCK) 
WHERE mzh=@mzh AND OrganizeId=@OrganizeId AND zt='1'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@mzh", mzh),
                new SqlParameter("@OrganizeId",organizeId )
            };
            return FindList<TreatmentEntity>(sql, param);
        }

        /// <summary>
        ///修改CmmPatId
        /// </summary>
        /// <param name="cmmPatId"></param>
        /// <param name="jzId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="updateTime"></param>
        /// <returns></returns>
        public int UpdateCmmPatId(string cmmPatId, string jzId, string organizeId, string userCode, DateTime updateTime)
        {
            const string sql = @"
UPDATE dbo.xt_jz SET cmmPatId=@cmmPatId, LastModifyTime=@LastModifyTime, LastModifierCode=@userCode
WHERE zt='1' AND OrganizeId=@OrganizeId AND jzId=@jzId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@jzId", jzId),
                new SqlParameter("@cmmPatId", cmmPatId),
                new SqlParameter("@LastModifyTime", updateTime),
                new SqlParameter("@userCode", userCode)
            };
            return ExecuteSqlCommand(sql, param);
        }
    }
}
