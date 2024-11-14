using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Web;
using Newtouch.Core.Common;
using Newtouch.Domain.DTO.InputDto;
using Newtouch.Domain.DTO.InputDto.Inpatient;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices.Inpatient;
using Newtouch.Domain.IDomainServices.InterfaceSync;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Infrastructure;
using Newtouch.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.DomainServices.Inpatient
{
	public class WardMaintenanceDmnService : DmnServiceBase, IWardMaintenanceDmnService
	{
        private readonly IHospInterfaceSyncDmnService _hospInterfaceSyncDmnService;
        public WardMaintenanceDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
		{ }
		public List<BedItemsVO> GetBedItems(WardMaintenanceRequestDto req)
		{
			var sqlstr = new StringBuilder();
			var sqlstr2 = new StringBuilder();//床位添加等级绑定的收费费用
			var par = new List<SqlParameter>();
			sqlstr = sqlstr.Append(@" SELECT [Id] AS ChargeId
      ,[ChargeCode]
      ,[ChargeName]
      ,[ChargeItem]
      ,[ChargeUtity]
      ,[ChargeNum]
	  ,b.dj ChargePrice
      ,'0' djbd
  FROM [Newtouch_CIS].[dbo].[zy_cwbdfy] a
  LEFT JOIN NewtouchHIS_Base.[dbo].[V_S_xt_sfxm] b ON a.ChargeCode=b.sfxmCode AND a.OrganizeId=b.OrganizeId
  WHERE 1=1  AND a.zt='1' AND b.zt='1' ");
			sqlstr2 = sqlstr2.Append(@"SELECT   CAST(cw.cwId AS VARCHAR(50)) Id ,
                    lc.sfxmCode [ChargeCode] ,
                    b.sfxmmc ChargeName ,
                    b.sfdlCode [ChargeItem] ,
                    b.dw [ChargeUtity] ,
                    lc.sl [ChargeNum] ,
                    b.dj ChargePrice,
                    '1' djbd
            FROM    NewtouchHIS_Base..V_S_xt_cw cw
                    LEFT JOIN dbo.zy_LevelChargedy lc ON cw.cwdj = lc.LevelCode
                                                         AND cw.OrganizeId = lc.OrganizeId
                                                         AND lc.zt = '1'
                    LEFT JOIN NewtouchHIS_Base.[dbo].[V_S_xt_sfxm] b ON lc.sfxmCode = b.sfxmCode
                                                                        AND cw.OrganizeId = b.OrganizeId
            WHERE   cw.zt = '1'
                    AND b.zt = '1'");
			if (req != null)
			{
				if (!string.IsNullOrWhiteSpace(req.OrganizeId))
				{
					sqlstr.Append("  AND a.OrganizeId=@OrganizeId ");
					sqlstr2.Append("  AND cw.OrganizeId=@OrganizeId ");
				}
				if (!string.IsNullOrWhiteSpace(req.bedCode))
				{
					sqlstr.Append("  AND a.BedCode=@bedCode ");
					sqlstr2.Append("  AND cw.cwCode=@bedCode ");
				}
			}
			par.Add(new SqlParameter("@OrganizeId", req.OrganizeId));
			par.Add(new SqlParameter("@bedCode", req.bedCode));
			var sqlstr3 = sqlstr + " union all " + sqlstr2;
			return this.FindList<BedItemsVO>(sqlstr3.ToString(), par.ToArray());
		}
        /// <summary>
        /// 医嘱发药查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public IList<Dispensing> GetDispensings(Pagination pagination, DispensingMXRequestDto req)
        {
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            sqlstr = sqlstr.Append(@" select CONVERT(varchar(50),czjl.createtime ,25)bysj,ks.Name hldy,czjl.creatorcode tjz,count(czjl.Id) yzsm
from [NewtouchHIS_PDS].dbo.zy_ypyzczjl czjl
inner join NewtouchHIS_PDS.dbo.zy_ypyzxx yzxx ON czjl.zxId=yzxx.zxId and czjl.yzid=yzxx.yzid AND czjl.ypCode=yzxx.ypCode AND czjl.OrganizeId=yzxx.OrganizeId 
inner join [NewtouchHIS_Base].dbo.V_S_Sys_Department ks on ks.Code=yzxx.ksCode and ks.OrganizeId=yzxx.OrganizeId 
where   czjl.operateType='1' and czjl.createtime>=@kssj and czjl.createtime<=@jssj 
and czjl.OrganizeId=@OrganizeId
group by czjl.createtime,czjl.creatorcode,ks.Name ");
            par.Add(new SqlParameter("@OrganizeId", req.OrganizeId));
            par.Add(new SqlParameter("@kssj", req.kssj));
            par.Add(new SqlParameter("@jssj", req.jssj));
            return this.QueryWithPage<Dispensing>(sqlstr.ToString(), pagination, par.ToArray());
        }
        public IList<DispensingMX> GetDispensingMXs(Pagination pagination, string bysj)
        {
            var sqlstr = new StringBuilder();
            var par = new List<SqlParameter>();
            sqlstr = sqlstr.Append(@" 
select yzxx.zh zh,yzxx.cw,yzxx.patientName xm,yzxx.zyh,yp.ypmc yznr,convert(varchar,yzxx.yl)+yzxx.yldw jl,pc.yzpcmcsm pc,CONVERT(varchar(50),yzxx.zxrq,25) zxsj,yp.ypmc,yp.ypgg,
Convert(decimal(18,2),(czjl.sl/yp.bzs)) as sl,
yp.bzdw dw,yp.ycmc,yzxx.je ys,yzxx.je ss,CONVERT(varchar(50),yzxx.CreateTime,25)  kssj,CONVERT(varchar(50),cqyz.tzsj,25) tzsj,yzxx.zlff jytj,brxx.brxzmc fb
from [NewtouchHIS_PDS].dbo.zy_ypyzczjl czjl
inner join NewtouchHIS_PDS.dbo.zy_ypyzxx yzxx ON czjl.zxId=yzxx.zxId and czjl.yzid=yzxx.yzid AND czjl.ypCode=yzxx.ypCode AND czjl.OrganizeId=yzxx.OrganizeId 
inner join [NewtouchHIS_Base].dbo.V_S_Sys_Department ks on ks.Code=yzxx.ksCode and ks.OrganizeId=yzxx.OrganizeId 
inner join [NewtouchHIS_Base].[dbo].[V_C_xt_yp] yp on yp.ypcode=yzxx.ypCode and yp.OrganizeId=yzxx.OrganizeId 
inner join [NewtouchHIS_Base].[dbo].[xt_yzpc] pc on pc.yzpcmc=yzxx.pcmc and pc.OrganizeId=yzxx.OrganizeId 
left join  zy_brxxk brxx on brxx.zyh=yzxx.zyh and  brxx.OrganizeId=yzxx.OrganizeId 
left join zy_cqyz cqyz on cqyz.Id=czjl.yzid and czjl.OrganizeId=cqyz.OrganizeId 
where czjl.createtime=@bysj
");
            par.Add(new SqlParameter("@bysj", bysj));
            return this.QueryWithPage<DispensingMX>(sqlstr.ToString(), pagination, par.ToArray());
        }
        public void SaveBedItems(List<BedItemsVO> mxList, WardMaintenanceRequestDto req,string zyh)
		{
			using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
			{
                //取护理级别对应编码
                string configsql = @"select value from [Newtouch_CIS]..[Sys_Config] where code = 'NursinLevelChargeItemMapp' and organizeId='"+ req.OrganizeId + "' and zt=1";//查看是否具有开药的权限,此表的value 0为未开启此权限，1为开启
                var value = this.FirstOrDefault<string>(configsql);
                var hljbList = value.Split(',').ToList();

                //先删除，再新增
                db.Delete<InpatientBedChargeItemEntity>(a => a.BedCode == req.bedCode && a.OrganizeId == req.OrganizeId);

				if (mxList != null && mxList.Count() > 0)
				{
					foreach (var item in mxList)
					{
						InpatientBedChargeItemEntity bedCharge = new InpatientBedChargeItemEntity();
						bedCharge.Id = Guid.NewGuid().ToString();
						bedCharge.OrganizeId = req.OrganizeId;
						bedCharge.BedCode = req.bedCode;
						bedCharge.ChargeCode = item.ChargeCode;
						bedCharge.ChargeName = item.ChargeName;
						bedCharge.ChargeItem = item.ChargeItem;
						bedCharge.ChargeUtity = item.ChargeUtity;
						bedCharge.ChargeNum = item.ChargeNum;

						bedCharge.Create();
						db.Insert(bedCharge);

                        //判断护理级别,更新zy_brxxk表hljb字段
                        for (var i = 0; i < hljbList.Count; i++) {
                            if (hljbList[i] == item.ChargeCode) {
                                //更新
                                var hljb = i + 1;
                                var hujbsql = @"update zy_brxxk set hljb=@hljb where OrganizeId=@orgId and zyh=@zyh and zt=1";
                                ExecuteSqlCommand(hujbsql, new SqlParameter("@hljb", hljb), new SqlParameter("@zyh", zyh), new SqlParameter("@orgId", req.OrganizeId));
                            }
                        }
                    }
                }


				db.Commit();
			}
		}

		public List<BedDocsVO> GetBedDocs(patRequestDto req)
		{
			var sqlstr = new StringBuilder();
			var par = new List<SqlParameter>();
			sqlstr = sqlstr.Append(@" SELECT  [Id]
              ,[ysgh]
              ,[ysmc]
              ,[Type]
              ,[TypeName]
            FROM [Newtouch_CIS].[dbo].[zy_PatDocInfo]
            WHERE zt='1'");
			if (req != null)
			{
				if (!string.IsNullOrWhiteSpace(req.OrganizeId))
				{
					sqlstr.Append("  AND OrganizeId=@OrganizeId ");
				}
				if (!string.IsNullOrWhiteSpace(req.zyh))
				{
					sqlstr.Append("  AND zyh=@zyh ");
				}
			}
			par.Add(new SqlParameter("@OrganizeId", req.OrganizeId));
			par.Add(new SqlParameter("@zyh", req.zyh));
			return this.FindList<BedDocsVO>(sqlstr.ToString(), par.ToArray());
		}


		public void SaveBedDocs(List<BedDocsVO> mxList, patRequestDto req)
		{
			using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
			{
				//先作废，再新增
				var patDocsList = db.IQueryable<InpatientPatientDoctorEntity>().Where(p => p.zyh == req.zyh && p.OrganizeId == req.OrganizeId && p.zt == "1").ToList();
				foreach (var item in patDocsList)
				{
					item.zt = "0";
					item.Modify();
					db.Update(item);
				}
				InpatientPatientInfoEntity patInfo = db.IQueryable<InpatientPatientInfoEntity>().Where(a => a.zyh == req.zyh && a.zt == "1" && a.OrganizeId == req.OrganizeId).FirstOrDefault();
				foreach (var item in mxList)
				{
					InpatientPatientDoctorEntity patDoc = new InpatientPatientDoctorEntity();
					patDoc.Id = Guid.NewGuid().ToString();
					patDoc.OrganizeId = req.OrganizeId;
					patDoc.zyh = req.zyh;
					patDoc.ysmc = item.ysmc;
					patDoc.ysgh = item.ysgh;
					patDoc.Type = item.Type;
					patDoc.TypeName = item.TypeName;

					patDoc.Create();
					db.Insert(patDoc);
					//如果是住院医生，则同步更新到患者信息表
					if (patInfo != null && item.Type == Convert.ToInt32(EnumYslx.ZyDoc))
					{
						patInfo.ysgh = item.ysgh;
						patInfo.Modify();
						db.Update(patInfo);
						InpatientPatientInfoDTO patInfoReq = new InpatientPatientInfoDTO()
						{
							zybz = Convert.ToInt32(EnumZYBZ.Bqz),
							zyh = req.zyh,
							doctor = item.ysgh
						};
						//更新患者住院医生
						SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", patInfoReq);
					}
				}

				db.Commit();
			}
		}
		/// <summary>
		/// 获取患者诊断信息
		/// </summary>
		/// <param name="req"></param>
		/// <returns></returns>
		public List<PatDiagnosisVO> GetPatDiagnosis(patRequestDto req)
		{
			var sqlstr = new StringBuilder();
			var par = new List<SqlParameter>();
			sqlstr = sqlstr.Append(@" SELECT  [Id]
              ,[zddl]
              ,[zdlb]
              ,[zdlx]
              ,[zddm]
              ,[zdmc]
              ,[zdyzdmc]
              ,convert(varchar(2),cyqk) cyqk
              ,isnull(px,1) px
            FROM [Newtouch_CIS].[dbo].[zy_PatDxInfo]
            WHERE zt='1' and zdlb='2' ");
			if (req != null)
			{
				if (!string.IsNullOrWhiteSpace(req.OrganizeId))
				{
					sqlstr.Append("  AND OrganizeId=@OrganizeId ");
				}
				if (!string.IsNullOrWhiteSpace(req.zyh))
				{
					sqlstr.Append("  AND zyh=@zyh ");
				}
			}
            sqlstr.Append("  order by zddl, convert(int,zdlx),px  ");
            par.Add(new SqlParameter("@OrganizeId", req.OrganizeId));
			par.Add(new SqlParameter("@zyh", req.zyh));
			return this.FindList<PatDiagnosisVO>(sqlstr.ToString(), par.ToArray());
		}
		/// <summary>
		/// 判断患者能否出区
		/// </summary>
		/// <param name="req"></param>
		/// <returns></returns>
		public string GetPatIsOutArea(patBedFeeRequestDto req)
		{
			var inParameters = new List<SqlParameter>();
			inParameters.Add(new SqlParameter("@OrganizeId", req.OrganizeId));
			inParameters.Add(new SqlParameter("@zyh", req.zyh));
			inParameters.Add(new SqlParameter("@rq", req.rq));
            inParameters.Add(new SqlParameter("@rygh", req.user));
            var outParameter = new SqlParameter("@outMsg", System.Data.SqlDbType.VarChar, 300);
			outParameter.Direction = System.Data.ParameterDirection.Output;
			inParameters.Add(outParameter);

			_databaseFactory.Get().Database.ExecuteSqlCommand("exec usp_zy_PatOutArea @zyh,@OrganizeId,@rq,@rygh,@outMsg out ", inParameters.ToArray());
			return outParameter.Value.ToString();
		}
		/// <summary>
		/// 患者出区
		/// </summary>
		/// <param name="patOutAreaVO"></param>
		/// <param name="req"></param>
		public string SavaPatDiagnosis(PatOutAreaInfoVO patOutAreaVO, patRequestDto req)
		{
			using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
			{
				//获取历史出院诊断
				//var patOutDiaList = db.IQueryable<InpatientPatientDiagnosisEntity>().Where(p => p.zyh == req.zyh && p.OrganizeId == req.OrganizeId && p.zt == "1" && p.zdlb == "2").ToList();
				//获取患者信息
				InpatientPatientInfoEntity patInfo = db.IQueryable<InpatientPatientInfoEntity>().Where(a => a.zyh == req.zyh && a.zt == "1" && a.OrganizeId == req.OrganizeId).FirstOrDefault();
				//获取最近一次病床记录
				InpatientBedUseRecordEntity bedUsedEntity = db.IQueryable<InpatientBedUseRecordEntity>().Where(a => a.zyh == req.zyh && a.zt == "1" && a.OrganizeId == req.OrganizeId).FirstOrDefault();

				if (patInfo == null || bedUsedEntity == null)
				{
					return "F|获取患者信息或床位使用记录失败";
				}
                #region 更新床位病人状态
                //var reqcwObj = new
                //{
                //	TimeStamp = DateTime.Now.ToString(),
                //	cwCode = patInfo.BedCode,
                //	isOccupy = false
                //};
                InpatientPatientInfoDTO patInfoReq = new InpatientPatientInfoDTO()
                {
                    zyh = patOutAreaVO.zyh,
                    zybz = Convert.ToInt32(EnumZYBZ.Djz),
                    cyrq = patOutAreaVO.cqsj
                };
                //更新床位状态（是否占用）
                var apiRespCw = _hospInterfaceSyncDmnService.UpdateOccupyByCode(bedUsedEntity.BedCode, bedUsedEntity.WardCode, false, bedUsedEntity.OrganizeId, req.user);

                //更新患者住院状态
                var apiRespbr = SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", patInfoReq);
                #endregion

                if (apiRespCw.code != APIRequestHelper.ResponseResultCode.SUCCESS || apiRespbr.code != APIRequestHelper.ResponseResultCode.SUCCESS)
				{
					InpatientPatientInfoDTO patInfoBackReq = new InpatientPatientInfoDTO()
					{
						zyh = patOutAreaVO.zyh,
						zybz = Convert.ToInt32(EnumZYBZ.Zq)
					};
                    //更新床位状态（是否占用）
                    _hospInterfaceSyncDmnService.UpdateOccupyByCode(bedUsedEntity.BedCode, bedUsedEntity.WardCode, true, bedUsedEntity.OrganizeId, req.user); 
					//更新患者住院状态
					SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", patInfoBackReq);
					return "F|调用更新换位或患者状态失败";
				}
				try
				{
					#region 更新病床使用表
					bedUsedEntity.OccEndDate = patOutAreaVO.cqsj;
					bedUsedEntity.zt = "0";
					bedUsedEntity.Modify();
					db.Update(bedUsedEntity);

					InpatientBedUseRecordEntity newbedUsedEntity = new InpatientBedUseRecordEntity();
					newbedUsedEntity.Id = Guid.NewGuid().ToString();
					newbedUsedEntity.OrganizeId = bedUsedEntity.OrganizeId;
					newbedUsedEntity.blh = bedUsedEntity.blh;
					newbedUsedEntity.zyh = bedUsedEntity.zyh;
					newbedUsedEntity.BedCode = bedUsedEntity.BedCode;
					newbedUsedEntity.BedNo = bedUsedEntity.BedNo;
					newbedUsedEntity.WardCode = bedUsedEntity.WardCode;
					newbedUsedEntity.RoomCode = bedUsedEntity.RoomCode;
					newbedUsedEntity.zt = "1";
					newbedUsedEntity.Status = Convert.ToInt32(EnumCwjlzt.Cq);
					newbedUsedEntity.Create();
					db.Insert(newbedUsedEntity);
					#endregion

					#region 作废该患者历史床位使用记录，只保留一条最新记录
					var patBedUsedList = db.IQueryable<InpatientBedUseRecordEntity>().Where(p => p.zyh == req.zyh && p.OrganizeId == req.OrganizeId && p.zt == "1").ToList();
					foreach (var item in patBedUsedList)
					{
						item.zt = "0";
						item.Modify();
						db.Update(item);
					}
					#endregion

					#region 作废患者历史出院诊断
					//foreach (var item in patOutDiaList)
					//{
					//    item.zt = "0";
					//    item.Modify();
					//    db.Update(item);
					//}
					#endregion

					#region 新增诊断记录
					//主要诊断
					//if (!string.IsNullOrEmpty(patOutAreaVO.zyzddm))
					//{
					//    InpatientPatientDiagnosisEntity diaEntity = new InpatientPatientDiagnosisEntity();
					//    diaEntity.Id = Guid.NewGuid().ToString();
					//    diaEntity.OrganizeId = req.OrganizeId;
					//    diaEntity.zyh = req.zyh;
					//    diaEntity.zdlb = "2";
					//    diaEntity.zdlx = "0";
					//    diaEntity.zddm = patOutAreaVO.zyzddm;
					//    diaEntity.zdmc = patOutAreaVO.zyzd;
					//    diaEntity.Create();
					//    db.Insert(diaEntity);
					//}
					//辅助第一诊断
					//if (!string.IsNullOrEmpty(patOutAreaVO.fzzd1dm) && !string.IsNullOrEmpty(patOutAreaVO.fzzd1))
					//{
					//    InpatientPatientDiagnosisEntity diaEntity = new InpatientPatientDiagnosisEntity();
					//    diaEntity.Id = Guid.NewGuid().ToString();
					//    diaEntity.OrganizeId = req.OrganizeId;
					//    diaEntity.zyh = req.zyh;
					//    diaEntity.zdlb = "2";
					//    diaEntity.zdlx = "1";
					//    diaEntity.zddm = patOutAreaVO.fzzd1dm;
					//    diaEntity.zdmc = patOutAreaVO.fzzd1;
					//    diaEntity.Create();
					//    db.Insert(diaEntity);
					//}
					//辅助第二诊断
					//if (!string.IsNullOrEmpty(patOutAreaVO.fzzd2dm) && !string.IsNullOrEmpty(patOutAreaVO.fzzd2))
					//{
					//    InpatientPatientDiagnosisEntity diaEntity = new InpatientPatientDiagnosisEntity();
					//    diaEntity.Id = Guid.NewGuid().ToString();
					//    diaEntity.OrganizeId = req.OrganizeId;
					//    diaEntity.zyh = req.zyh;
					//    diaEntity.zdlb = "2";
					//    diaEntity.zdlx = "2";
					//    diaEntity.zddm = patOutAreaVO.fzzd2dm;
					//    diaEntity.zdmc = patOutAreaVO.fzzd2;
					//    diaEntity.Create();
					//    db.Insert(diaEntity);
					//}
					//辅助第三诊断
					//if (!string.IsNullOrEmpty(patOutAreaVO.fzzd3dm) && !string.IsNullOrEmpty(patOutAreaVO.fzzd3))
					//{
					//    InpatientPatientDiagnosisEntity diaEntity = new InpatientPatientDiagnosisEntity();
					//    diaEntity.Id = Guid.NewGuid().ToString();
					//    diaEntity.OrganizeId = req.OrganizeId;
					//    diaEntity.zyh = req.zyh;
					//    diaEntity.zdlb = "2";
					//    diaEntity.zdlx = "3";
					//    diaEntity.zddm = patOutAreaVO.fzzd3dm;
					//    diaEntity.zdmc = patOutAreaVO.fzzd3;
					//    diaEntity.Create();
					//    db.Insert(diaEntity);
					//}
					//自定义诊断
					//if (!string.IsNullOrEmpty(patOutAreaVO.zdyzd))
					//{
					//    InpatientPatientDiagnosisEntity diaEntity = new InpatientPatientDiagnosisEntity();
					//    diaEntity.Id = Guid.NewGuid().ToString();
					//    diaEntity.OrganizeId = req.OrganizeId;
					//    diaEntity.zyh = req.zyh;
					//    diaEntity.zdlb = "2";
					//    diaEntity.zdlx = "9";
					//    diaEntity.zddm = "999999999";//自定义诊断代码默认999999999
					//    diaEntity.zdmc = patOutAreaVO.zdyzd;
					//    diaEntity.Create();
					//    db.Insert(diaEntity);
					//}
					#endregion

					#region 更新患者信息表
					if (patInfo != null)
					{
						patInfo.cqrq = patOutAreaVO.cqsj;
						patInfo.cyfs = patOutAreaVO.cyfs;
                        //patInfo.zddm = patOutAreaVO.zyzddm;
                        //patInfo.zdmc = patOutAreaVO.zyzd;
                        patInfo.cyzddm = patOutAreaVO.zyzddm;
                        patInfo.cyzdmc = patOutAreaVO.zyzd;
                        patInfo.zybz = Convert.ToInt32(EnumZYBZ.Djz);
						patInfo.Modify();
						db.Update(patInfo);
					}
					#endregion
					db.Commit();
					return "T|出区成功";
				}
				catch (Exception ex)
				{
					InpatientPatientInfoDTO patInfoBackReq = new InpatientPatientInfoDTO()
					{
						zyh = patOutAreaVO.zyh,
						zybz = Convert.ToInt32(EnumZYBZ.Zq)
					};
                    //更新床位状态（是否占用）
                    _hospInterfaceSyncDmnService.UpdateOccupyByCode(bedUsedEntity.BedCode, bedUsedEntity.WardCode, true, bedUsedEntity.OrganizeId, req.user);
                    //更新患者住院状态
                    SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", patInfoBackReq);
					return "F|出区失败：" + ex.Message;
				}


			}
		}
		/// <summary>
		/// 患者出区诊断
		/// </summary>
		/// <param name="patOutAreaVO"></param>
		/// <param name="req"></param>
		/// <returns></returns>
		public string SavaOutPatDiagnosis(PatOutAreaVO patOutAreaVO, patRequestDto req)
		{
			InpatientPatientInfoDTO patInfoReq = new InpatientPatientInfoDTO()
			{
				zybz = Convert.ToInt32(EnumZYBZ.Bqz),
				zyh = patOutAreaVO.zyh,
				cyzd = patOutAreaVO.zyzddm
			};
			//var apiRespzd= SiteSettAPIHelper.Request<APIRequestHelper.DefaultResponse<RequestMsgDto>>("/api/patient/UpdateInpatientOutInfo", patInfoReq);
			//if (apiRespzd.code != APIRequestHelper.ResponseResultCode.SUCCESS)
			//{
			//    return "F|调用接口失败：更新出院诊断";
			//}
			using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
			{
				//获取历史出院诊断
				var patOutDiaList = db.IQueryable<InpatientPatientDiagnosisEntity>().Where(p => p.zyh == req.zyh && p.OrganizeId == req.OrganizeId && p.zt == "1" && p.zdlb == "2").ToList();
				//获取患者信息
				InpatientPatientInfoEntity patInfo = db.IQueryable<InpatientPatientInfoEntity>().Where(a => a.zyh == req.zyh && a.zt == "1" && a.OrganizeId == req.OrganizeId).FirstOrDefault();
				try
				{
					#region 作废患者历史出院诊断
					foreach (var item in patOutDiaList)
					{
						item.zt = "0";
						item.Modify();
						db.Update(item);
					}
					#endregion

					#region 新增诊断记录
					//主要诊断
					if (!string.IsNullOrEmpty(patOutAreaVO.zyzddm))
					{
						InpatientPatientDiagnosisEntity diaEntity = new InpatientPatientDiagnosisEntity();
						diaEntity.Id = Guid.NewGuid().ToString();
						diaEntity.OrganizeId = req.OrganizeId;
						diaEntity.zyh = req.zyh;
						diaEntity.zdlb = "2";
						diaEntity.zdlx = "0";
						diaEntity.zddm = patOutAreaVO.zyzddm;
						diaEntity.zdmc = patOutAreaVO.zyzd;
						diaEntity.cyqk = Convert.ToInt32(patOutAreaVO.cyqk);
						diaEntity.Create();
						db.Insert(diaEntity);
					}
					//辅助第一诊断
					if (!string.IsNullOrEmpty(patOutAreaVO.fzzd1dm) && !string.IsNullOrEmpty(patOutAreaVO.fzzd1))
					{
						InpatientPatientDiagnosisEntity diaEntity = new InpatientPatientDiagnosisEntity();
						diaEntity.Id = Guid.NewGuid().ToString();
						diaEntity.OrganizeId = req.OrganizeId;
						diaEntity.zyh = req.zyh;
						diaEntity.zdlb = "2";
						diaEntity.zdlx = "1";
						diaEntity.zddm = patOutAreaVO.fzzd1dm;
						diaEntity.zdmc = patOutAreaVO.fzzd1;
						diaEntity.cyqk = Convert.ToInt32(patOutAreaVO.cyqk1);
						diaEntity.Create();
						db.Insert(diaEntity);
					}
					//辅助第二诊断
					if (!string.IsNullOrEmpty(patOutAreaVO.fzzd2dm) && !string.IsNullOrEmpty(patOutAreaVO.fzzd2))
					{
						InpatientPatientDiagnosisEntity diaEntity = new InpatientPatientDiagnosisEntity();
						diaEntity.Id = Guid.NewGuid().ToString();
						diaEntity.OrganizeId = req.OrganizeId;
						diaEntity.zyh = req.zyh;
						diaEntity.zdlb = "2";
						diaEntity.zdlx = "2";
						diaEntity.zddm = patOutAreaVO.fzzd2dm;
						diaEntity.zdmc = patOutAreaVO.fzzd2;
						diaEntity.cyqk = Convert.ToInt32(patOutAreaVO.cyqk2);
						diaEntity.Create();
						db.Insert(diaEntity);
					}
					//辅助第三诊断
					if (!string.IsNullOrEmpty(patOutAreaVO.fzzd3dm) && !string.IsNullOrEmpty(patOutAreaVO.fzzd3))
					{
						InpatientPatientDiagnosisEntity diaEntity = new InpatientPatientDiagnosisEntity();
						diaEntity.Id = Guid.NewGuid().ToString();
						diaEntity.OrganizeId = req.OrganizeId;
						diaEntity.zyh = req.zyh;
						diaEntity.zdlb = "2";
						diaEntity.zdlx = "3";
						diaEntity.zddm = patOutAreaVO.fzzd3dm;
						diaEntity.zdmc = patOutAreaVO.fzzd3;
						diaEntity.cyqk = Convert.ToInt32(patOutAreaVO.cyqk3);
						diaEntity.Create();
						db.Insert(diaEntity);
					}
					//自定义诊断
					if (!string.IsNullOrEmpty(patOutAreaVO.zdyzd))
					{
						InpatientPatientDiagnosisEntity diaEntity = new InpatientPatientDiagnosisEntity();
						diaEntity.Id = Guid.NewGuid().ToString();
						diaEntity.OrganizeId = req.OrganizeId;
						diaEntity.zyh = req.zyh;
						diaEntity.zdlb = "2";
						diaEntity.zdlx = "9";
						diaEntity.zddm = "999999999";//自定义诊断代码默认999999999
						diaEntity.zdmc = patOutAreaVO.zdyzd;
						diaEntity.cyqk = Convert.ToInt32(patOutAreaVO.cyqkzdy);
						diaEntity.Create();
						db.Insert(diaEntity);
					}
					#endregion

					#region 更新患者信息表
					if (patInfo != null)
					{
						//  patInfo.cqrq = patOutAreaVO.cqsj;
						//  patInfo.cyfs = patOutAreaVO.cyfs;
						//patInfo.zddm = patOutAreaVO.zyzddm;
						//patInfo.zdmc = patOutAreaVO.zyzd;
						//patInfo.zybz = Convert.ToInt32(EnumZYBZ.Djz);
						patInfo.cyzddm = patOutAreaVO.zyzddm;
						patInfo.cyzdmc = patOutAreaVO.zyzd;
						patInfo.Modify();
						db.Update(patInfo);
					}
					#endregion
					db.Commit();
					return "T|保存成功";
				}
				catch (Exception ex)
				{
					return "F|保存失败：" + ex.Message;
				}


			}
		}

		/// <summary>
		/// 保存出区诊断List
		/// </summary>
		/// <param name="patOutAreaVO"></param>
		/// <param name="req"></param>
		/// <returns></returns>
		public string SavaPatDxList(List<InpatientPatientDiagnosisEntity> PatDxList,string OrganizeId)
		{
			var zyh=""; 
			if (PatDxList.Count > 0) { 
				zyh = PatDxList[0].zyh;
			}
			using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
			{
				//获取历史出院诊断
				var patOutDiaList = db.IQueryable<InpatientPatientDiagnosisEntity>().Where(p => p.zyh == zyh && p.OrganizeId == OrganizeId && p.zt == "1" && p.zdlb == "2").ToList();
				//获取患者信息
				InpatientPatientInfoEntity patInfo = db.IQueryable<InpatientPatientInfoEntity>().Where(a => a.zyh == zyh && a.zt == "1" && a.OrganizeId == OrganizeId).FirstOrDefault();
				try
				{
					#region 作废患者历史出院诊断
					foreach (var item in patOutDiaList)
					{
						item.zt = "0";
						item.Modify();
						db.Update(item);
					}
					#endregion
					
					#region 新增诊断记录
					foreach (var PatDxEntity in PatDxList)
					{
						//InpatientPatientDiagnosisRepo repo = new InpatientPatientDiagnosisRepo();
						//repo.SubmitForm()
						//PatDxEntity.Create(true);
						//this.Insert(PatDxEntity);
						InpatientPatientDiagnosisEntity diaEntity = new InpatientPatientDiagnosisEntity();
						diaEntity.Id = Guid.NewGuid().ToString();
						diaEntity.OrganizeId = OrganizeId;
						diaEntity.zyh = PatDxEntity.zyh;
                        diaEntity.zddl = PatDxEntity.zddl;
						diaEntity.zdlb = PatDxEntity.zdlb;
						diaEntity.zdlx = PatDxEntity.zdlx;
                        diaEntity.px = PatDxEntity.px;
						diaEntity.zddm = PatDxEntity.zddm;
						diaEntity.zdmc = PatDxEntity.zdmc;
						diaEntity.cyqk = Convert.ToInt32(PatDxEntity.cyqk);
						diaEntity.Create();
						db.Insert(diaEntity);
						//this.Insert(diaEntity);
					}
					#endregion

					#region 更新患者信息表
					if (patInfo != null)
					{
						//  patInfo.cqrq = patOutAreaVO.cqsj;
						//  patInfo.cyfs = patOutAreaVO.cyfs;
						//patInfo.zddm = patOutAreaVO.zyzddm;
						//patInfo.zdmc = patOutAreaVO.zyzd;
						//patInfo.zybz = Convert.ToInt32(EnumZYBZ.Djz);
						patInfo.cyzddm = PatDxList[0].zddm;
						patInfo.cyzdmc = PatDxList[0].zdmc;
						patInfo.Modify();
						db.Update(patInfo);
					}
					#endregion
					db.Commit();
					return "T|保存成功";
				}
				catch (Exception ex)
				{
					return "F|保存失败：" + ex.Message;
				}


			}
		}


		///// <summary>
		///// 保存出院诊断Entity
		///// </summary>
		///// <param name="PatDxEntity"></param>
		///// <returns></returns>
		//public string SavaPatDxEntity(InpatientPatientDiagnosisEntity PatDxEntity)
		//{
		//	using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
		//	{
		//		try
		//		{
		//			PatDxEntity.Create();
		//			db.Insert(PatDxEntity);
		//			db.Commit();
		//			return "T|保存成功";
		//		}
		//		catch (Exception ex)
		//		{
		//			return "F|保存失败：" + ex.Message;
		//		}
		//	}
		//}

		/// <summary>
		/// 获取住院床卡
		/// </summary>
		/// <param name="zyh"></param>
		/// <param name="OrganizeId"></param>
		/// <returns></returns>
		public List<InpatientBedCardVo> GetBedCard(string zyh,string OrganizeId)
		{
			var sql = "select a.organizeId,a.zt,xm,sex, (case when datediff(yy, a.birth, getdate()) >= 1 then convert(varchar(3), datediff(yy, a.birth, getdate())) + '岁' else convert(varchar(2), datediff(mm, a.birth, getdate())) + '个月' end)nl, rqrq,blh,ysgh zgys, a.zyh, b.xh,b.jb,b.zrhs,b.zrzz,b.gms,b.fbsx,b.sjys,b.hsz,b.kzr, c.name zgysname, d.name zrhsname, e.name zrzzname, f.name sjysname, g.name hszname, h.name kzrname from zy_brxxk a left join [zy_bedCard] b on a.zyh = b.zyh and a.OrganizeId = b.OrganizeId and a.zt = 1 and b.zt = 1 left join[NewtouchHIS_Base].[dbo].[Sys_Staff] c on a.organizeId=c.organizeId and a.ysgh=c.gh and a.zt=1 and c.zt=1 left join [NewtouchHIS_Base].[dbo].[Sys_Staff] d on b.organizeId= d.organizeId and b.zrhs= d.gh and b.zt= 1 and d.zt= 1 left join [NewtouchHIS_Base].[dbo].[Sys_Staff] e on b.organizeId= e.organizeId and b.zrzz= e.gh and b.zt= 1 and e.zt= 1 left join [NewtouchHIS_Base].[dbo].[Sys_Staff] f on b.organizeId= f.organizeId and b.sjys= f.gh and b.zt= 1 and f.zt= 1 left join [NewtouchHIS_Base].[dbo].[Sys_Staff] g on b.organizeId= g.organizeId and b.hsz= g.gh and b.zt= 1 and g.zt= 1 left join [NewtouchHIS_Base].[dbo].[Sys_Staff] h on b.organizeId= h.organizeId and b.kzr= h.gh and b.zt= 1 and h.zt= 1 where a.zt=1 ";

			var par = new List<SqlParameter>();
			if (!string.IsNullOrWhiteSpace(zyh)) {
				sql += " and a.zyh=@zyh";
			}
			if (!string.IsNullOrWhiteSpace(OrganizeId))
			{
				sql += " and a.OrganizeId=@OrganizeId";
			}
			par.Add(new SqlParameter("@zyh", zyh));
			par.Add(new SqlParameter("@OrganizeId", OrganizeId));
			return this.FindList<InpatientBedCardVo>(sql.ToString(), par.ToArray());
			
		}

        public InpatientVo GetZyPatInfo(string zyh,string orgId)
        {
            string sql = @"select zyh,wzjb,rqrq,ysgh,b.Name ysName from zy_brxxk a 

 left join[NewtouchHIS_Base].[dbo].[Sys_Staff] b on a.organizeId=b.organizeId and a.ysgh=b.gh and a.zt=1 and b.zt=1 

  where a.zyh=@zyh  and a.zybz<>'9' and a.zt='1' and a.OrganizeId=@orgId ";

            return this.FindList<InpatientVo>(sql.ToString(), new[]{ new SqlParameter("@zyh", zyh),
                new SqlParameter("@orgId",orgId) }).FirstOrDefault();
        }
        public InpatiContinuePrintVo GetZyPatContinuePrint(string zyh, string orgId,string yzlx)
        {
            string sql = @"select xdsj,ys,yznr,zxsj,hs,xh,pageCnt
                           from [Zy_YzContiniuPrintRecord] 
                           where zyh=@zyh and yzlx=@yzlx and OrganizeId=@orgId order by createtime desc";

            return this.FindList<InpatiContinuePrintVo>(sql.ToString(), new[]{ new SqlParameter("@zyh", zyh),
                new SqlParameter("@orgId",orgId),new SqlParameter("@yzlx",yzlx) }).FirstOrDefault();
        }
        public InpatiContinuePrintPageVo GetZyPatContinuePrintPage(string zyh, string orgId, string yzlx,string xh,string page)
        {
            string sql = @" exec usp_Inp_Report_Prt_LsyzContinue @zyh,@orgId,'1','',@xh,@page,'N' ";
            if (yzlx=="2")
            {
                sql = @" exec usp_Inp_Report_Prt_CqyzContinue @zyh,@orgId,'1',@xh,@page,'N' ";
            }
            return this.FindList<InpatiContinuePrintPageVo>(sql.ToString(), new[]{ new SqlParameter("@zyh", zyh),
                new SqlParameter("@orgId",orgId),new SqlParameter("@xh",xh),new SqlParameter("@page",page) }).FirstOrDefault();
        }
    }
}
