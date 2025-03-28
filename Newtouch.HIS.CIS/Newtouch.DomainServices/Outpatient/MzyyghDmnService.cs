using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.DTO.OutputDto.Outpatient.API;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ViewModels.Outpatient;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Newtouch.DomainServices
{
    /// <summary>
    /// 门诊预约挂号
    /// </summary>
    public class MzyyghDmnService : DmnServiceBase, IMzyyghDmnService
    {
        private readonly IMzyyghRepo _mzyyghRepo;
        private readonly ISysBespeakRegisterRepo _sysBespeakRegisterRepo;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="databaseFactory"></param>
        public MzyyghDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 根据病历号查询门诊预约挂号信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="blh"></param>
        /// <param name="organizeId"></param>
        /// <param name="regDate"></param>
        /// <returns>返回今天和今天以后的预约挂号</returns>
        public IList<MzyyghVO> SelectMzyyghDetail(Pagination pagination, string blh, string organizeId, DateTime regDate)
        {
            var sql = @"
SELECT yygh.Id, yygh.OrganizeId, br.departmentCode ksCode, dept.Name ksmc, br.regDate, br.regTime, br.ysgh gh, staff.Name zjmc,yygh.bespeakNo
,(CASE br.mzlx WHEN '1' THEN '普通门诊' WHEN '2' THEN '急症' WHEN '3' THEN '专家门诊' ELSE '' END) mzlx, yygh.CreateTime,yygh.CreatorCode
FROM dbo.mz_yygh(NOLOCK) yygh
INNER JOIN dbo.xt_bespeakRegister(NOLOCK) br ON br.Id=yygh.brId AND br.OrganizeId=yygh.OrganizeId AND br.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Department(NOLOCK) dept ON dept.Code=br.departmentCode AND dept.OrganizeId=br.OrganizeId AND dept.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Staff(NOLOCK) staff ON staff.gh=br.ysgh AND staff.OrganizeId=br.OrganizeId AND staff.zt='1'
WHERE yygh.OrganizeId=@OrganizeId
AND yygh.blh=@blh
AND br.regDate>=@regDate ";
            var param = new SqlParameter[] {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@blh", blh),
                new SqlParameter("@regDate", regDate),
            };
            return QueryWithPage<MzyyghVO>(sql, pagination, param);
        }

        /// <summary>
        /// 门诊预约查询
        /// </summary>
        /// <param name="blh">病历号</param>
        /// <param name="kh">卡号</param>
        /// <param name="zjh">证件号</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<MzBespeakRegisterQueryResponseDTO> MzBespeakRegisterQuery(string blh, string kh, string zjh, string organizeId)
        {
            const string sql = @"
SELECT yygh.Id mzyyghId, yygh.zjlx, yygh.zjh, yygh.blh, br.mzlx, br.regDate, br.regTime, br.departmentCode ksCode, dept.Name ksmc, br.ysgh, staff.Name ysmc
,br.bespeakMaxCount, yygh.bespeakNo, yygh.CreateTime,yygh.CreatorCode,yygh.arrivalDate,yygh.arrivalOpereater
FROM dbo.mz_yygh(NOLOCK) yygh
INNER JOIN dbo.xt_bespeakRegister(NOLOCK) br ON br.Id=yygh.brId AND br.OrganizeId=yygh.OrganizeId AND br.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Department(NOLOCK) dept ON dept.Code=br.departmentCode AND dept.OrganizeId=br.OrganizeId AND dept.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.Sys_Staff(NOLOCK) staff ON staff.gh=br.ysgh AND staff.OrganizeId=br.OrganizeId AND staff.zt='1'
WHERE yygh.OrganizeId=@OrganizeId
AND (yygh.blh=@blh OR yygh.zjh=@zjh OR yygh.kh=@kh)
AND yygh.zt='1' ";
            var param = new DbParameter[] {
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@blh", blh??""),
                new SqlParameter("@zjh", zjh??""),
                new SqlParameter("@kh", kh??"")
            };
            return FindList<MzBespeakRegisterQueryResponseDTO>(sql, param);
        }

        /// <summary>
        /// 获取当前科室或专家已预约挂号总数
        /// </summary>
        /// <param name="ksCode"></param>
        /// <param name="ysgh"></param>
        /// <param name="regDate"></param>
        /// <param name="regTime"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public MzAlreadyBespeakRegisterCountQueryResponseDTO SelectAlreadyBespeakRegisterCount(string ksCode, string ysgh, DateTime regDate, string regTime, string organizeId)
        {
            var alreadyBespeakRegisterCount = 0;
            if (string.IsNullOrWhiteSpace(regTime))
            {
                var sysbr = _sysBespeakRegisterRepo.IQueryable(p => p.regDate == regDate && p.ysgh == (ysgh ?? "") && p.zt == "1" && p.OrganizeId == organizeId);
                if (sysbr == null || !sysbr.Any()) return null;
                sysbr.ToList().ForEach(p =>
                {
                    alreadyBespeakRegisterCount += _mzyyghRepo.IQueryable(q => q.brId == p.Id && q.zt == "1").Count();
                });
            }
            else
            {
                var sysbr = _sysBespeakRegisterRepo.IQueryable(p => p.regDate == regDate && p.regTime == regTime && p.ysgh == (ysgh ?? "") && p.zt == "1" && p.OrganizeId == organizeId).FirstOrDefault();
                if (sysbr == null || string.IsNullOrWhiteSpace(sysbr.Id)) return null;
                alreadyBespeakRegisterCount = _mzyyghRepo.IQueryable(p => p.brId == sysbr.Id && p.zt == "1").Count();
            }
            return new MzAlreadyBespeakRegisterCountQueryResponseDTO
            {
                ksCode = ksCode,
                regDate = regDate,
                regTime = regTime,
                ysgh = ysgh,
                alreadyBespeakRegisterCount = alreadyBespeakRegisterCount
            };
        }

        /// <summary>
        /// 查询已预约挂号
        /// </summary>
        /// <param name="mzlx">门诊住院标志 1：普通门诊  2：急诊   3：专家门诊</param>
        /// <param name="departmentCode"></param>
        /// <param name="ysgh"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<MzyyghDetailVo> SelectMzyyghDetail(string mzlx, string departmentCode, string ysgh, DateTime beginTime, DateTime endTime, string organizeId)
        {
            var sql = new StringBuilder(@"
WITH xtyygh AS (
	SELECT SUM(bespeakMaxCount) bespeakMaxCount, regDate, departmentCode, ysgh, OrganizeId
	FROM dbo.xt_bespeakRegister(NOLOCK)
	WHERE zt='1'
	AND OrganizeId=@OrganizeId ");
            if (!string.IsNullOrWhiteSpace(mzlx)) sql.AppendLine("	AND mzlx=@mzlx");
            sql.AppendLine(@"
	AND regDate BETWEEN @beginTime AND @endTime
	AND departmentCode=@departmentCode
	AND ysgh=@ysgh
	GROUP BY regDate,departmentCode,ysgh,OrganizeId
)
SELECT pb.bespeakMaxCount, ISNULL(mzyy.yyzs,0) bespeakedCount, pb.regDate, pb.departmentCode, pb.ysgh
FROM xtyygh pb
LEFT JOIN (
	SELECT COUNT(mzyy.Id) yyzs, br.departmentCode, br.regDate, br.ysgh
	FROM  dbo.mz_yygh(NOLOCK) mzyy 
	INNER JOIN dbo.xt_bespeakRegister(NOLOCK) br ON br.Id=mzyy.brId AND  br.zt='1' AND br.OrganizeId=mzyy.OrganizeId
	WHERE mzyy.OrganizeId=@OrganizeId AND mzyy.zt='1'
	AND br.regDate BETWEEN @beginTime AND @endTime
	AND departmentCode=@departmentCode
	AND ysgh=@ysgh ");
            if (!string.IsNullOrWhiteSpace(mzlx)) sql.AppendLine("	AND mzlx=@mzlx");
            sql.AppendLine(@"
	GROUP BY br.departmentCode, br.regDate, br.ysgh
) mzyy ON mzyy.ysgh=pb.ysgh AND mzyy.departmentCode = pb.departmentCode AND mzyy.regDate = pb.regDate
");
            var param = new DbParameter[]
            {
                new SqlParameter("@mzlx",mzlx),
                new SqlParameter("@departmentCode",departmentCode),
                new SqlParameter("@ysgh",ysgh),
                new SqlParameter("@beginTime",beginTime),
                new SqlParameter("@endTime",endTime),
                new SqlParameter("@OrganizeId",organizeId)
            };
            return FindList<MzyyghDetailVo>(sql.ToString(), param);
        }
    }
}