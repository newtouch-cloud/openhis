using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects.InsuranceManage;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository.InsuranceManage;
using Newtouch.Tools;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.HIS.DomainServices.NewFolder1
{
    public class SysCommercialInsuranceDmnService : DmnServiceBase, ISysCommercialInsuranceDmnService
    {
        private readonly ICommercialInsuranceRepo _commercialInsuranceRepo;

        public SysCommercialInsuranceDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取商保备案列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <param name="sbbabId"></param>
        /// <returns></returns>
        public List<SysCommercialInsuranceFilingVO> SelectCommercialInsuranceFilingList(string keyword, string orgId, string sbbabId = null)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                return null;
            }
            StringBuilder sqlStr = new StringBuilder();
            IList<SqlParameter> inSqlParameterList = new List<SqlParameter>();
            sqlStr.Append(@"
                            SELECT sbbab.* ,
                            brjbxx.blh ,
                            brjbxx.xm ,
                            brjbxx.csny ,
                            brjbxx.zjh,
							bx.Name bxName
                     FROM   xt_sbbab sbbab
                            LEFT JOIN xt_brjbxx brjbxx ON brjbxx.patId = sbbab.patId
                                                          AND brjbxx.OrganizeId = @orgId
                            LEFT JOIN dbo.xt_sybx bx ON bx.bxCode=sbbab.bxcode
														  AND bx.OrganizeId =  @orgId
                     WHERE  sbbab.OrganizeId = @orgId
                        ");
            inSqlParameterList.Add(new SqlParameter("@orgId", orgId.Trim()));
            if (!string.IsNullOrEmpty(keyword))
            {
                sqlStr.Append(" and (brjbxx.blh like @keyword or brjbxx.xm like @keyword) ");
                inSqlParameterList.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(sbbabId))
            {
                sqlStr.Append(" and sbbab.sbbabId=@sbbabId");
                inSqlParameterList.Add(new SqlParameter("@sbbabId", sbbabId.Trim()));
            }
            var list = FindList<SysCommercialInsuranceFilingVO>(sqlStr.ToString(), inSqlParameterList.ToArray()).ToList();
            return list;
        }

        /// <summary>
        /// 提交商保
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="KeyValue"></param>
        public void SubmitForm(SysCommercialInsuranceVO entity, string KeyValue, string orgId)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                var bxEntity = new CommercialInsuranceEntity();
                if (!string.IsNullOrWhiteSpace(KeyValue))
                {
                    bxEntity = _commercialInsuranceRepo.FindEntity(KeyValue);
                }
                if (bxEntity != null && !string.IsNullOrWhiteSpace(bxEntity.Id))
                {
                    bxEntity.OrganizeId = orgId;
                    bxEntity.Name = entity.Name;
                    bxEntity.EnglishName = entity.EnglishName;
                    bxEntity.bxbl = entity.bxbl;
                    bxEntity.remark = entity.remark;
                    bxEntity.zt = "1";
                    db.Update(bxEntity);
                }
                else
                {
                    bxEntity.bxCode = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("xt_sybx").ToString();
                    bxEntity.OrganizeId = orgId;
                    bxEntity.Name = entity.Name;
                    bxEntity.EnglishName = entity.EnglishName;
                    bxEntity.bxbl = entity.bxbl;
                    bxEntity.remark = entity.remark;
                    bxEntity.zt = "1";
                    bxEntity.Create(true);
                    db.Insert(bxEntity);
                }



                #region 删除之前存在的可报项目
                if (!string.IsNullOrWhiteSpace(bxEntity.Id))
                {
                    var bxsql = new StringBuilder();
                    bxsql.Append(@"DELETE  dbo.xt_sbkbxm
                        WHERE   sybxId =@Id
                                AND OrganizeId = @orgId");
                    SqlParameter[] bxpar = {
                        new SqlParameter("@orgId",orgId),
                        new SqlParameter("@Id",bxEntity.Id)
                };
                    ExecuteSqlCommand(bxsql.ToString(), bxpar);
                }
                #endregion

                #region 添加可报项目
                if (!string.IsNullOrWhiteSpace(entity.kbxm))
                {
                    //var dllist = "'" + string.Join("','", entity.kbxm.Split(',').Distinct().ToList()) + "'";
                    //dllist = dllist.Substring(dllist.Length - 2, 2);

                    var dllist = entity.kbxm.Split(new char[1] { ',' });
                    foreach (var item in dllist)
                    {
                        if (!string.IsNullOrWhiteSpace(item))
                        {
                            SysCommercialInsuranceChargeItemEntity kbxmEntity = new SysCommercialInsuranceChargeItemEntity();
                            kbxmEntity.Create(true);
                            kbxmEntity.kbxm = item;
                            kbxmEntity.OrganizeId = orgId;
                            kbxmEntity.sybxId = bxEntity.Id;
                            kbxmEntity.zt = "1";
                            db.Insert(kbxmEntity);
                        }

                    }
                    #region 

                    //    StringBuilder sql = new StringBuilder();
                    //    sql.Append(@"SELECT  CAST(sfxmId AS VARCHAR(50)) sfxmId ,
                    //          *
                    //        FROM    NewtouchHIS_Base..V_S_xt_sfxm
                    //        WHERE   OrganizeId = @OrganizeId
                    //               ");
                    //    SqlParameter[] par = {
                    //        new SqlParameter("@OrganizeId",orgId)
                    //};
                    //    if (!string.IsNullOrEmpty(dllist))
                    //    {
                    //        sql.Append("  AND sfdlCode IN ( " + dllist + " )");
                    //    }
                    //    var list = FindList<SysChargeItemVO>(sql.ToString(), par);
                    //    foreach (var item in list)
                    //    {
                    //        SysCommercialInsuranceChargeItemEntity kbxmEntity = new SysCommercialInsuranceChargeItemEntity();
                    //        kbxmEntity.Create(true);
                    //        kbxmEntity.kbxm = item.sfxmCode;
                    //        kbxmEntity.OrganizeId = orgId;
                    //        kbxmEntity.sybxId = bxEntity.Id;
                    //        kbxmEntity.zt = "1";
                    //        db.Insert(kbxmEntity);
                    //    }
                }
                #endregion

                #endregion

                db.Commit();
            }

        }
    }
}
