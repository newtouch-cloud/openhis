
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 门诊挂号
    /// </summary>
    public class OutpatientRegistRepo : RepositoryBase<OutpatientRegistEntity>, IOutpatientRegistRepo
    {
        public OutpatientRegistRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        /// <summary>
        /// 门诊挂号
        /// </summary>
        /// <param name="ghnm"></param>
        /// <returns></returns>
        public OutpatientRegistEntity SelectOutPatientReg(int ghnm, string orgId)
        {
            return this.IQueryable().Where(a => a.ghnm == ghnm && a.zt == "1" && a.OrganizeId == orgId).FirstOrDefault();
        }

        /// <summary>
        /// 根据mzh获取挂号实体
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public OutpatientRegistEntity SelectOutPatientReg(string mzh, string orgId)
        {
            return IQueryable().FirstOrDefault(a => a.mzh == mzh && a.zt == "1" && a.OrganizeId == orgId);
        }

        /// <summary>
        /// 根据mzh获取挂号实体
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public OutpatientRegistEntity SelectData(string mzh, string orgId)
        {
            const string sql = @"
SELECT * FROM dbo.mz_gh(NOLOCK) 
WHERE mzh=@mzh AND OrganizeId=@OrganizeId AND zt='1'
";
            var param = new DbParameter[]
            {
                new SqlParameter("@mzh", mzh),
                new SqlParameter("@OrganizeId", orgId),
            };
            return FirstOrDefault<OutpatientRegistEntity>(sql, param);
        }


        /// <summary>
        /// 根据门诊号获取患者信息
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public OutpatAccInfoDto GetBasicInfoPatInfoInRegister(string mzh, string orgId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select a.patid,a.mzh,(a.ghf+a.zlf+a.ckf+a.gbf) ghzje,mjzbz,case mjzbz
           when 1 then '普通门诊'
           when 2 then '急诊'
           when 3 then '专家门诊'
           when 4 then '特病门诊'
           when 5 then '重大疾病门诊'
           when 6 then '慢性病门诊'
           when 7 then '居民两病'
           when 8 then '意外伤害门诊'
           when 10 then '生育门诊'
           when 11 then '耐多药结核门诊'
           when 9 then '儿童两病门诊' end                                 mzlx,
       a.blh,
       a.xm,
       a.ghnm,
       c.xb,case c.xb when '1' then '男' when '2' then '女' else '' end Gender,
       c.csny,
       c.zjlx,
       c.zjh,
       a.ks,
       (select top 1 Name from NewtouchHIS_Base..V_S_Sys_Department ks where ks.Code = a.ks and ks.OrganizeId = a.OrganizeId) ksmc,
       CAST(FLOOR(DATEDIFF(DY, c.csny, GETDATE()) / 365.25) AS INT) nl,
       a.brxz,
       xz.brxzmc,
       kh,
       CardTypeName,
       ''                                                           dh,
       ''                                                           phone,
       c.brly,
       (SELECT COUNT(1)
        FROM dbo.mz_gh
        WHERE patid = a.patid
          and zt = 1
          AND OrganizeId = a.OrganizeId
          AND ghrq = CONVERT(varchar(10), GETDATE(), 23))           sycs,
       ''                                                           lxrgx,
       ''                                                           lxrdh,
       ''                                                           lxr
from mz_gh a
         LEFT JOIN xt_brxz xz ON xz.brxz = a.brxz
         left join xt_brjbxx c on a.patid = c.patid and a.OrganizeId = c.OrganizeId
    AND xz.OrganizeId = a.OrganizeId
where a.OrganizeId = @OrganizeId
  AND a.mzh = @mzh ");
            DbParameter[] param =
            {
                new SqlParameter("@mzh",mzh),
                new SqlParameter("@OrganizeId",orgId)
            };
            return FirstOrDefault<OutpatAccInfoDto>(strSql.ToString(), param);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ghnmList"></param>
        /// <returns></returns>
        public List<OutpatientRegistEntity> SelectOutPatientRegList(List<int> ghnmList, string orgId)
        {
            return this.IQueryable().Where(a => a.OrganizeId == orgId && a.zt == "1" && ghnmList.Contains(a.ghnm)).ToList();
        }

        /// <summary>
        /// 初复诊
        /// </summary>
        /// <param name="kh">挂号</param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public OutpatientRegistEntity SelectCFZ(string kh, string orgId)
        {
            return IQueryable().Where(a => a.OrganizeId == orgId && a.zt == "1" && a.kh == kh.Trim() && a.ghzt != "2").OrderByDescending(a => a.CreateTime).FirstOrDefault();
        }

        /// <summary>
        /// 根据卡号查询患者当天挂号信息
        /// add by HLF
        /// </summary>
        /// <param name="kh">卡号</param>
        /// <returns></returns>
        public List<OutpatientRegistEntity> GetOutPatRegEntityList(string kh, string orgId)
        {
            var nextDay = DateTime.Today.AddDays(1);
            var entityList = this.IQueryable().Where(a => a.OrganizeId == orgId && a.zt == "1" && a.kh == kh.Trim() && a.CreateTime >= DateTime.Today && a.CreateTime < nextDay).ToList();
            return entityList;
        }

        #region GRS门诊挂号
        /// <summary>
        ///门诊登记时，挂号
        /// </summary>
        /// <param name="OutpatientRegistEntity"></param>
        /// <param name="keyValue"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        public string SubmitForm(OutpatientRegistEntity OutpatientRegistEntity, int? keyValue, string pzzy, string orgId, string curUserCode, out string mzh)
        {

            if (OutpatientRegistEntity.brly != null)
            {
                ExecuteSqlCommand(@"UPDATE  dbo.xt_brjbxx
                            SET     brly = @brly
                            WHERE   patid = @patid
                                    AND OrganizeId = @orgId", new[] {
                        new SqlParameter("@brly", OutpatientRegistEntity.brly)
                        ,new SqlParameter("@patid", OutpatientRegistEntity.patid)
                        ,new SqlParameter("@orgId", orgId)
                    });

            }

            string res;
            if (string.IsNullOrWhiteSpace(pzzy))
            {
                throw new FailedException("请先配置开关");
            }

            if (OutpatientRegistEntity.csny.HasValue)
            {
                var nage = DateTimeHelper.getAgeFromBirthTime(OutpatientRegistEntity.csny);
                OutpatientRegistEntity.nlshow = nage.text;
            }

            if (keyValue > 0)
            {

                var entity = FindEntity(OutpatientRegistEntity.ghnm);
                entity.Modify(keyValue);
                Update(entity);
                Insert(OutpatientRegistEntity);
                res = "修改成功";
            }
            else
            {
                if (pzzy == "OFF" && !string.IsNullOrWhiteSpace(OutpatientRegistEntity.mzh))
                {
                    var extis = IQueryable().FirstOrDefault(p => p.mzh == OutpatientRegistEntity.mzh && p.OrganizeId == orgId);
                    if (extis != null)
                    {
                        throw new FailedException("门诊号已存在");
                    }
                }

                if (pzzy == "ON")
                {
                    var inParameters = new Dictionary<string, object>();
                    inParameters.Add("@ghlx", "");
                    inParameters.Add("@ks", OutpatientRegistEntity.ks);
                    inParameters.Add("@ys", OutpatientRegistEntity.ys == null ? "" : OutpatientRegistEntity.ys);
                    inParameters.Add("@ghzb", "");
                    inParameters.Add("@CreatorCode", curUserCode);
                    inParameters.Add("@OrganizeId", orgId);
                    var outParameter = new SqlParameter("@NumStr", System.Data.SqlDbType.SmallInt);

                    Tools.DB.DbHelper.ExecuteProcedure("spGetJZXH", inParameters, outParameter);

                    short jzxh = Convert.ToInt16(outParameter.Value);

                    var mzh2 = DateTime.Now.ToString("yyMMdd") + jzxh.ToString().PadLeft(7, '0');  //门诊号
                    bool mzhexits = true;

                    while (mzhexits)
                    {
                        var extis = IQueryable().FirstOrDefault(p => p.mzh == mzh2 && p.OrganizeId == orgId);
                        if (extis != null)
                        {
                            jzxh++;
                            mzh2 = DateTime.Now.ToString("yyMMdd") + jzxh.ToString().PadLeft(7, '0');  //门诊号
                            continue;
                        }
                        else
                        {
                            OutpatientRegistEntity.mzh = mzh2;
                            mzhexits = false;
                        }
                    }
                }
                OutpatientRegistEntity.jzbz = ((int)EnumOutpatientJzbz.Djz).ToString();  //待就诊
                OutpatientRegistEntity.CardType = ((int)EnumCardType.XNK).ToString();
                OutpatientRegistEntity.CardTypeName = EnumCardType.XNK.GetDescription();
                OutpatientRegistEntity.OrganizeId = orgId;
                OutpatientRegistEntity.Create(true, EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("mz_gh"));
                Insert(OutpatientRegistEntity);
                res = "添加成功";
            }
            mzh = OutpatientRegistEntity.mzh;
            return res;
        }

        /// <summary>
        /// 病人今天是否已挂号
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public bool AllowRegh(string blh, string orgId)
        {
            bool result = false;
            DateTime dtnow = DateTime.Parse(DateTime.Now.ToShortDateString());
            var data = this.IQueryable().Where(a => a.blh == blh && a.zt == "1" && a.OrganizeId == orgId && a.ghrq == dtnow);
            if (data != null && data.Count() > 0)
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 取消挂号
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ghnm"></param>
        public void SaveCancelRegister(string orgId, int ghnm)
        {
            if (string.IsNullOrEmpty(orgId) || ghnm == 0)
            {
                return;
            }
            var entity = this.IQueryable().Where(a => a.OrganizeId == orgId && a.zt == "1" && a.ghnm == ghnm).FirstOrDefault();
            if (entity == null)
            {
                throw new FailedCodeException("ABNORMAL_CANCEL_THE_REGISTRATION_ERROR_DID_NOT_QUERY_THE_REGISTRATION_NUMBER");
            }
            entity.zt = "0";
            entity.Modify();
            this.Update(entity);
        }

        #endregion

        /// <summary>
        /// 更新门诊挂号 就诊状态、就诊医生
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        public void UpdateConsultationStatus(string orgId, string mzh, string jzbz, string jzys)
        {
            var entity = this.IQueryable().Where(p => p.OrganizeId == orgId && p.mzh == mzh && p.zt == "1").FirstOrDefault();
            if (entity != null)
            {
                entity.jzbz = jzbz;
                if (!string.IsNullOrWhiteSpace(jzys) && string.IsNullOrWhiteSpace(entity.ys))
                {
                    //挂号时排班未到医生，则用医生站过来的看诊医生更新
                    entity.ys = jzys;
                }
                entity.Modify();
                this.Update(entity);
            }
        }

        /// <summary>
        /// 更新门诊挂号 诊断信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <param name="zdicd10"></param>
        /// <param name="zdmc"></param>
        public void UpdateDiagnosis(string orgId, string mzh, string zdicd10, string zdmc)
        {
            var entity = this.IQueryable().Where(p => p.OrganizeId == orgId && p.mzh == mzh && p.zt == "1").FirstOrDefault();
            if (entity != null)
            {
                entity.zdicd10 = zdicd10;
                entity.zdmc = zdmc;
                entity.Modify();
                this.Update(entity);
                AppLogger.Instance.InfoFormat("UpdateDiagnosis更新成功mzh:{0},zdicd10:{1},zdmc:{2}", mzh, zdicd10, zdmc);
            }
            else
            {
                AppLogger.Instance.InfoFormat("UpdateDiagnosis门诊挂号纪录没找到mzh:{0}", mzh);
            }
        }

        public void updatePatBrxzInfo(string orgId, string mzh, string brxz)
        {
            var entity = this.IQueryable().Where(p => p.OrganizeId == orgId && p.mzh == mzh && p.zt == "1").FirstOrDefault();
            if (entity != null)
            {
                if (!string.IsNullOrWhiteSpace(brxz))
                {
                    //挂号时排班未到医生，则用医生站过来的看诊医生更新
                    entity.brxz = brxz;
                }
                entity.Modify();
                this.Update(entity);
            }
        }

        /// <summary>
        /// 记录补偿序号
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="outpId">补偿序号</param>
        /// <param name="userCode">修改人</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public int RecordOutpId(string mzh, string outpId, string userCode, string organizeId)
        {

            const string sql = @"
UPDATE dbo.mz_gh
SET outpId=@outpId, LastModifyTime=GETDATE(), LastModifierCode=@userCode
WHERE mzh=@mzh
AND zt='1'
AND OrganizeId=@OrganizeId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@mzh", mzh),
                new SqlParameter("@outpId",outpId ),
                new SqlParameter("@userCode",userCode ),
                new SqlParameter("@OrganizeId", organizeId),
            };
            return ExecuteSqlCommand(sql, param);
        }
        /// <summary>
        /// 修改病人手机号
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public int UpdatePatPhone(string patid, string phone, string userCode, string organizeId)
        {
            const string sql = @"
UPDATE dbo.xt_brjbxx
SET phone=@phone, LastModifyTime=GETDATE(), LastModifierCode=@userCode
WHERE patid=@patid
AND zt='1'
AND OrganizeId=@OrganizeId
";
            var param = new DbParameter[]
            {
                new SqlParameter("@patid", patid),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@userCode",userCode ),
                new SqlParameter("@phone", phone),
            };
            return ExecuteSqlCommand(sql, param);
        }
    }
}
