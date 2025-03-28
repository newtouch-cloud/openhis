using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects.PatientManage;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class HospPatientBasicInfoRepo : RepositoryBase<HospPatientBasicInfoEntity>, IHospPatientBasicInfoRepo
    {
        public HospPatientBasicInfoRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary> 
        /// 获取有效的住院基本信息
        /// </summary>
        /// <param name="zyh">住院号</param>
        /// <param name="kh">卡号</param>
        /// <param name="xm">姓名</param>
        /// <returns></returns>
        public List<HospPatientBasicInfoEntity> GetHosPatInfoList(string zyh, string kh, string xm, string OrganizeId)
        {
            var hospatInfoEntityList = new List<HospPatientBasicInfoEntity>();

            hospatInfoEntityList = this.IQueryable().Where(p => p.zt == "1" && p.OrganizeId == OrganizeId && (p.zybz == ((int)EnumZYBZ.Xry).ToString() || p.zybz == ((int)EnumZYBZ.Bqz).ToString() || p.zybz == ((int)EnumZYBZ.Djz).ToString())).ToList();

            if (!string.IsNullOrEmpty(zyh))
            {
                hospatInfoEntityList = hospatInfoEntityList.Where(t => t.OrganizeId == OrganizeId && t.zyh.Contains(zyh)
                && t.zybz == ((int)EnumZYBZ.Bqz).ToString()
                ).ToList(); //枚举EnumZYBZ
            }
            if (!string.IsNullOrEmpty(xm))
            {
                hospatInfoEntityList = hospatInfoEntityList.Where(p => p.OrganizeId == OrganizeId && p.xm.Contains(xm)).ToList();
            }
            if (!string.IsNullOrEmpty(kh))
            {
                hospatInfoEntityList = hospatInfoEntityList.Where(p => p.kh == kh.ToUpper() && p.OrganizeId == OrganizeId).ToList();
                if (hospatInfoEntityList.Count == 0)
                {
                    throw new FailedCodeException("OUTPAT_CARDNO_ISINVALID"); //卡号无效
                }
            }

            return hospatInfoEntityList.ToList();
        }

        /// <summary>
        /// 根据病人内码获取有效的住院基本信息
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        public HospPatientBasicInfoEntity GetFirstPatiInfoByPatid(int? patid)
        {
            if (patid <= 0)
            {
                return null;
            }
            return this.IQueryable().Where(p => (p.zybz == ((int)EnumZYBZ.Xry).ToString() || p.zybz == ((int)EnumZYBZ.Bqz).ToString() || p.zybz == ((int)EnumZYBZ.Djz).ToString() || p.zybz == ((int)EnumZYBZ.Zq).ToString()) && p.zt == "1" && p.patid == patid).FirstOrDefault();
        }

        /// <summary>
        /// 根据zyh判断是否存在病人基本信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string GetBlhByZyh(string zyh, string orgId)
        {
            var sce = this.IQueryable().Where(p => p.OrganizeId == orgId && (p.zybz == ((int)EnumZYBZ.Xry).ToString() || p.zybz == ((int)EnumZYBZ.Bqz).ToString() || p.zybz == ((int)EnumZYBZ.Djz).ToString()));
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                int cnt = sce.Where(p => p.zyh == zyh).Count();
                if (cnt == 0)
                {
                    throw new FailedCodeException("OUTPAT_ZYH_ISINVALID"); //住院号无效
                }
                if (cnt == 1)
                {
                    var firentity = sce.Where(p => p.zyh == zyh).ToList().FirstOrDefault();
                    if (firentity != null)
                    {
                        return firentity.blh.ToString();
                    }
                }
                if (cnt > 1)
                {
                    throw new FailedCodeException("MOREINFO_IS_INVALID"); //当前住院号存在多条住院记录
                }
            }

            return "0";
        }

        /// <summary>
        /// 获取机构病人列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<PendingExecutionPatientVO> GetWardPatientVOListByOrg(string orgId, string bqCode = null)
        {
            var sql = @"select b.bqCode, a.xm, a.zyh, a.patid from zy_brjbxx a
left join [NewtouchHIS_Base]..V_S_xt_bq b
on a.bq = b.bqCode and b.OrganizeId = @orgId
where a.OrganizeId = @orgId and a.zybz in ('1','2','3')";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            if (!string.IsNullOrWhiteSpace(bqCode))
            {
                sql += " and a.bq = @bqCode";
                pars.Add(new SqlParameter("@bqCode", bqCode));
            }
            return this.FindList<PendingExecutionPatientVO>(sql, pars.ToArray());
        }

        /// <summary>
        /// 根据zyh获取病人信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public HospPatientBasicInfoEntity GetInpatientInfoByZyh(string zyh, string orgId)
        {
            if (string.IsNullOrEmpty(zyh))
            {
                throw new FailedCodeException("HOSP_ZYH_IS_EMPTY");
            }
            var entity = this.IQueryable().Where(a => a.zyh == zyh && a.OrganizeId == orgId && (a.zybz == ((int)EnumZYBZ.Xry).ToString() || a.zybz == ((int)EnumZYBZ.Djz).ToString() || a.zybz == ((int)EnumZYBZ.Bqz).ToString())).ToList();   //注:条件zybz为病区中是暂时的
            if (entity == null)
            {
                throw new FailedCodeException("HOSP_INPATIENT_BASICINFO_IS_NOT_EXIST");
            }
            if (entity.Count() > 1)
            {
                throw new FailedCodeException("MOREINFO_IS_INVALID");
            }
            return entity.FirstOrDefault();
        }

        /// <summary>
        /// 筛选住院病人
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="keyword"></param>
        /// <param name="zybz"></param>
        /// <returns></returns>
        public List<HospPatientBasicInfoEntity> GetInpatientList(string orgId, string keyword = null, string zybz = "1")
        {
            var query = this.IQueryable().Where(p => p.zt == "1" && p.OrganizeId == orgId);

            if (zybz == ((int)EnumZYBZ.Bqz).ToString())
            {
                query = query.Where(p => p.zybz == ((int)EnumZYBZ.Bqz).ToString());
            }
            else
            {

            }

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                query = query.Where(p => p.zyh.Contains(keyword) || p.blh.Contains(keyword));
            }

            return query.ToList();
        }

        /// <summary>
        /// 更新住院病人在院标志
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <param name="zybz"></param>
        public void UpdateInpatientStatus(string orgId, string zyh, string zybz)
        {
            if (string.IsNullOrWhiteSpace(zyh) || string.IsNullOrWhiteSpace(zybz))
            {
                return;
            }
            var entity = this.IQueryable().Where(p => p.OrganizeId == orgId && p.zyh == zyh && p.zt == "1").FirstOrDefault();
            if (entity != null)
            {
                if (entity.zybz == zybz)
                {
                    return;
                }
                if (entity.zybz == ((int)EnumZYBZ.Ycy).ToString() || entity.zybz == ((int)EnumZYBZ.Wry).ToString())
                {
                    //已出院或取消入院记录不能通过接口更新
                    throw new FailedCodeException("CURRENT_YCY_OR_ZFWRY_UPDATE_FORBIDDEN");
                }
                entity.zybz = zybz;
                entity.Modify();
                this.Update(entity);
            }
        }

        /// <summary>
        /// 通过出区、转区更新患者基本信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="bq"></param>
        /// <param name="cw"></param>
        /// <param name="cyrq"></param>
        /// <param name="cyzd"></param>
        /// <param name="cybq"></param>
        /// <param name="doctor"></param>
        /// <param name="ryzd"></param>
        /// <param name="rybq"></param>
        public void UpdateInpatientOutInfoRequest(string orgId,string zyh,string zybz, string bq, string cw, DateTime? cyrq, string doctor, string cyzd)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedCodeException("HOSP_ZYH_IS_EMPTY");
            }
            else
            {
                var entity = this.IQueryable().Where(p => p.OrganizeId == orgId && p.zyh == zyh && p.zt == "1").FirstOrDefault();
                if (entity != null)
                {
                    if (entity.zybz != zybz && !string.IsNullOrWhiteSpace(zybz))
                    {
                        if (entity.zybz == ((int)EnumZYBZ.Ycy).ToString() || entity.zybz == ((int)EnumZYBZ.Wry).ToString())
                        {
                            //已出院或取消入院记录不能通过接口更新
                            throw new FailedCodeException("CURRENT_YCY_OR_ZFWRY_UPDATE_FORBIDDEN");
                        }
                        entity.zybz = zybz;
                    }
                    if (entity.bq != bq && bq != null)
                    {
                        entity.bq = bq;
                    }
                    if (entity.cw != cw && cw != null)
                    {
                        entity.cw = cw;
                    }
                    if (entity.cyrq != cyrq && cyrq != null)
                    {
                        entity.cyrq = cyrq;
                    }
                    if (entity.doctor != doctor && doctor != null)
                    {
                        entity.doctor = doctor;
                    }
                    if (entity.cyzd != cyzd && cyzd != null)
                    {
                        entity.cyzd = cyzd;
                    }

                    entity.Modify();
                    this.Update(entity);
                }
                else
                {
                    throw new FailedCodeException("HOSP_INPATIENT_BASICINFO_IS_NOT_EXIST");
                }
            }
        }
        /// <summary>
        /// 通过出区召回更新患者基本信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="bq"></param>
        /// <param name="cw"></param>
        /// <param name="cyrq"></param>
        /// <param name="cyzd"></param>
        /// <param name="cybq"></param>
        /// <param name="doctor"></param>
        /// <param name="ryzd"></param>
        /// <param name="rybq"></param>
        public void UpdateInpatientOutRecallInfoRequest(string orgId, string zyh, string zybz, string bq, string cw, DateTime? cyrq, string doctor, string cyzd)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedCodeException("HOSP_ZYH_IS_EMPTY");
            }
            else
            {
                var entity = this.IQueryable().Where(p => p.OrganizeId == orgId && p.zyh == zyh && p.zt == "1").FirstOrDefault();
                if (entity != null)
                {
                    if (entity.zybz != zybz && !string.IsNullOrWhiteSpace(zybz))
                    {
                        if (entity.zybz == ((int)EnumZYBZ.Ycy).ToString() || entity.zybz == ((int)EnumZYBZ.Wry).ToString())
                        {
                            //已出院或取消入院记录不能通过接口更新
                            throw new FailedCodeException("CURRENT_YCY_OR_ZFWRY_UPDATE_FORBIDDEN");
                        }
                        entity.zybz = zybz;
                    }
                    if (entity.bq != bq && bq != null)
                    {
                        entity.bq = bq;
                    }
                    if (entity.cw != cw && cw != null)
                    {
                        entity.cw = cw;
                    }
                    //if (entity.cyrq != cyrq && cyrq != null)
                    if (entity.cyrq != cyrq)
                    {
                        entity.cyrq = cyrq;
                    }
                    if (entity.doctor != doctor && doctor != null)
                    {
                        entity.doctor = doctor;
                    }
                    if (entity.cyzd != cyzd && cyzd != null)
                    {
                        entity.cyzd = cyzd;
                    }

                    entity.Modify();
                    this.Update(entity);
                }
                else
                {
                    throw new FailedCodeException("HOSP_INPATIENT_BASICINFO_IS_NOT_EXIST");
                }
            }
        }


    }
}
