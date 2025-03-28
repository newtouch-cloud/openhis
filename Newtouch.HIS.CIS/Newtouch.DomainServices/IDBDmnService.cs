using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.DTO;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Newtouch.DomainServices
{
    /// <summary>
    /// 中间库相关
    /// </summary>
    public class IDBDmnService : DmnServiceBase, IIDBDmnService
    {
        public IDBDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 查询患者挂号信息
        /// </summary>
        /// <param name="ksCode"></param>
        /// <param name="ysgh">//筛选专家号 仅看挂自己号的</param>
        /// <param name="mjzbz">//1普通门诊 2急诊 3专家门诊</param>
        /// <param name="jiuzhenbz"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<TreatmentEntity> GetRegistrationInfoList(Pagination pagination, string orgId, string ksCode, string ysgh, string mjzbz, int jiuzhenbz, string keyword)
        {
            if (jiuzhenbz < 0)
            {
                throw new FailedException("就诊标志为空");
            }
            if (jiuzhenbz == (int)EnumJzzt.NotYetTreate)
            {
                jiuzhenbz = 0;  //对应中间库待就诊0
            }

            const  string sqlstr = @"
SELECT  --CAST(NEWID() AS VARCHAR(50)) jzId ,
	@orgId OrganizeId ,
	zjk.RegID mzh ,
	1 mjzbz,--需要修改确认
	xxk.blh ,
	zjk.PatName xm ,
		CASE zjk.PatSex
		WHEN 1 THEN '男'
		WHEN 2 THEN '女'
		ELSE '不详'
	END xb ,
	CONVERT(DATETIME, zjk.PatBirth, 112) csny,
	zjk.PatTypeName brxzmc ,
	zjk.PatTypeCode brxzCode,
	1 zjlx ,
	zjk.PatIDNum zjh ,
	zjk.RegDeptName ghksmc ,
	zjk.DoctorName ghys ,
	CONVERT(DATETIME, zjk.RegDateTime, 0) ghsj ,
	CONVERT(DATETIME, zjk.InsertDateTime, 0) ghczsj ,
	--待就诊 中间库jzzt 0，CIS 1   --else 异常
	case zjk.jzzt when 0 then 1 else -1 end jzzt
FROM NNHIS_INTERFACE_Basic.dbo.MZ_REGISTER_HIS(NOLOCK) zjk
     LEFT JOIN NNHIS_INTERFACE_Basic.dbo.ZY_BRXXK(NOLOCK) xxk ON zjk.PatID = xxk.patid
WHERE DATEDIFF(HOUR, CAST(zjk.RegDateTime AS DATETIME), GETDATE()) < 24 
AND ( zjk.RegDeptCode IN (select col from [dbo].f_split(@kscode, ',')) OR ISNULL(@kscode, '') = '')
AND ( zjk.DoctorCode = @ysgh OR ISNULL(@ysgh, '') = '')
AND ( zjk.jzzt = @jzbz)
AND ( ISNULL(@mjzbz, '') = ''OR '1' = @mjzbz)
AND (@keyword = '%%' OR zjk.PatName LIKE @keyword
	OR xxk.blh LIKE @keyword
	OR zjk.RegID LIKE @keyword
	OR zjk.PatID LIKE @keyword
	OR xxk.py LIKE @keyword
)
";
            var par = new DbParameter[]
            {
                new SqlParameter("@keyword", "%" + keyword.Trim() + "%"),
                new SqlParameter("@kscode", ksCode),
                new SqlParameter("@ysgh", ysgh),
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@mjzbz", mjzbz),
                new SqlParameter("@jzbz", jiuzhenbz)
            };
            var voList = this.QueryWithPage<SysIDBVO>(sqlstr.ToString(), pagination, par);
            var resultList = new List<TreatmentEntity>();
            foreach (var t in voList)
            {
                var entity = new TreatmentEntity();
                t.MapperTo(entity);
                resultList.Add(entity);
            }
            return resultList;
        }

        /// <summary>
        /// 将mzh的未收费处方等信息 同步至中间库   
        /// （且同步处方成功后要回写处方同步状态）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public void SyncTo(string orgId, string mzh,string cfId= null)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("组织机构为空");
            }
            if (string.IsNullOrWhiteSpace(mzh))
            {
                throw new FailedException("门诊号为空");
            }
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@mzh", mzh));
            _databaseFactory.Get().Database.ExecuteSqlCommand("EXEC dbo.skd_Syncmzcf @orgId=@orgId,@mzh=@mzh ", par.ToArray());
        }

        /// <summary>
        /// 作废his单次就诊的所有处方
        /// </summary>
        /// <param name="jzId"></param>
        /// <param name="orgId"></param>
        public void ObsoleteAllPresToHIS(string jzId, string orgId)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("组织机构为空");
            }
            if (string.IsNullOrWhiteSpace(jzId))
            {
                throw new FailedException("就诊号为空");
            }
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@jzId", jzId));
            _databaseFactory.Get().Database.ExecuteSqlCommand("EXEC dbo.skd_SynAllPres @orgId=@orgId,@jzId=@jzId ", par.ToArray());
        }

        /// <summary>
        /// 作废HIS单张处方
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="cfId"></param>
        /// <param name="apicflx"></param>
        /// <param name="apicfh"></param>
        public void cancelSinglePresToHIS(string orgId, string mzh, string cfId
            , int apicflx, string apicfh)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("组织机构为空");
            }
            if (string.IsNullOrWhiteSpace(cfId)&& string.IsNullOrWhiteSpace(apicfh))
            {
                throw new FailedException("缺少处方信息");
            }
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@cfId", cfId));
            par.Add(new SqlParameter("@mzh", mzh));
            par.Add(new SqlParameter("@cfh", apicfh));
            _databaseFactory.Get().Database.ExecuteSqlCommand("EXEC dbo.skd_SynSinglePres @orgId=@orgId,@mzh=@mzh,@cfId=@cfId,@cfh=@cfh ", par.ToArray());
        }


        /// <summary>
        /// CIS处方数据
        /// </summary>
        /// <param name="Opt"></param>
        /// <param name="Brwym"></param>
        /// <param name="Zdlx"></param>
        /// <param name="Brxz"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<PrescriptionDto> GetCisPrescription(string Opt, string Brwym, string Zdlx, string Brxz, DateTime? startDate, DateTime? endDate)
        {
            var inParameters = new List<SqlParameter>();
            inParameters.Add(new SqlParameter("@Opt", Opt));
            inParameters.Add(new SqlParameter("@Brwym", Brwym));
            inParameters.Add(new SqlParameter("@Zdlx", Zdlx??""));
            inParameters.Add(new SqlParameter("@Brxz", Brxz));
            inParameters.Add(new SqlParameter("@startDate", startDate));
            inParameters.Add(new SqlParameter("@endDate", endDate));
           return this.FindList<PrescriptionDto>("exec skd_QhdSvscis @Opt,@Brwym,@Zdlx,@Brxz,@startDate,@endDate", inParameters.ToArray());

        }
        /// <summary>
        /// 中间库 同步处方收费状态
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="orgId"></param>
        public void Refreshcfsfbz(string blh,string orgId)
        {
            if (string.IsNullOrWhiteSpace(blh))
            {
                throw new FailedException("缺少病历号");
            }
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@orgId", orgId));
            par.Add(new SqlParameter("@blh", blh));
            _databaseFactory.Get().Database.ExecuteSqlCommand("EXEC dbo.skd_SyncRefreshcfsfbz @orgId=@orgId,@blh=@blh ", par.ToArray());

        }
    }
}
