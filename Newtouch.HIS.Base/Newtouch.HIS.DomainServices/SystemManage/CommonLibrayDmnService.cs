
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Tools;




namespace Newtouch.HIS.DomainServices
{
	public class CommonLibraryDmnService : DmnServiceBase, ICommonLibraryDmnService
	{
		
		private readonly ISysMedicineRepo _sysMedicineRepository;
		private readonly ISysMedicinePropertyRepo _sysMedicinePropertyRepo;
		private readonly ISysMedicineBaseRepo _sysMedicineBaseRepo;
		private readonly ISysMedicinePropertyBaseRepo _sysMedicinePropertyBaseRepo;
		private readonly ISysChargeCategoryRepo _sysChargeCategoryRepository;
		private readonly IGzybBaseInfoDmnService _gzybBaseInfoDmnService;
		private readonly ISysChargeItemRepo _sysChargeItemRepository;


		public CommonLibraryDmnService(IBaseDatabaseFactory databaseFactory, ISysMedicineRepo sysMedicineRepository
			, ISysMedicinePropertyRepo sysMedicinePropertyRepo, ISysMedicineBaseRepo sysMedicineBaseRepository,
			ISysMedicinePropertyBaseRepo sysMedicinePropertyBaseRepo,
			ISysChargeCategoryRepo sysChargeCategoryRepository, IGzybBaseInfoDmnService gzybBaseInfoDmnService,
			ISysChargeItemRepo sysChargeItemRepository)
			: base(databaseFactory)
		{
			_sysMedicineRepository = sysMedicineRepository;
			_sysMedicinePropertyRepo = sysMedicinePropertyRepo;
			_sysMedicineBaseRepo = sysMedicineBaseRepository;
			_sysMedicinePropertyBaseRepo = sysMedicinePropertyBaseRepo;
			_sysChargeCategoryRepository = sysChargeCategoryRepository;
			_gzybBaseInfoDmnService = gzybBaseInfoDmnService;
			_sysChargeItemRepository = sysChargeItemRepository;
		}


		public IList<SysMedicineVO> GetPaginationList(string organizeId, Pagination pagination, string zt,
			string ypflCode, string keyword = null, string ypgg = null, string ycmc = null, string dlCode = null)
		{
			if (string.IsNullOrWhiteSpace(organizeId))
			{
				return null;
			}

			var sql = @"
SELECT x.ypId, x.zxdw, x.ypCode, x.ypmc, x.spm, x.py, x.bzdw, x.mzcls, x.mzcldw, x.zycls, x.zycldw,
x.djdw, x.lsj, x.pfj, x.zfbl, x.zfxz, x.dlCode, x.ycmc, x.zt, 0 bgbz, x.ypbzdm, x.cxjje,
x.nbdl, x.mzzybz, x.CreateTime, d.pzwh,
d.yptssx, d.ypflCode, d.zlff, d.jzlx, d.mrbzq, d.sjap, d.zbbz, d.yl, d.yldw, d.zjtzsj,
d.ghdw, d.ypcd, d.ypgg, d.ybdm, d.xnhybdm, d.gjybdm, c.dlmc sfdlMc, e.jxmc jxmc, f.ypflmc ypflMc, 
case when d.ybdm IS NULL then '2' when d.ybdm IS NOT NULL AND d.LastYBUploadTime IS NOT NULL AND d.LastYBUploadTime >= d.LastModifyTime then '1' else '0' end isSynch, 
d.gjybmc,d.ypzsm 
FROM xt_yp_base as x  
left join xt_ypsx_base as d on x.ypId = d.ypId
left join xt_sfdl_base c on x.dlCode = c.dlCode and c.OrganizeId = @organizeId
left join xt_ypjx e on x.jx = e.jxCode
left join xt_ypfl f on d.ypflCode = f.ypflCode
where x.OrganizeId = @organizeId and d.OrganizeId = @organizeId";

			DbParameter[] par;
			if (!string.IsNullOrEmpty(zt))
			{
				sql += " and x.zt = @zt";
			}

			if (!string.IsNullOrEmpty(ypflCode))
			{
				sql += " and d.ypflCode = @ypflCode";
			}

			if (!string.IsNullOrEmpty(keyword))
			{
				sql += @"
    and (x.ypCode like @keyword or x.ypmc like @keyword or d.gjybdm like @keyword or d.ybdm like @keyword or x.py like @keyword or ypzsm like @keyword)";
			}

			if (!string.IsNullOrEmpty(ypgg))
			{
				sql += " and d.ypgg like @ypgg";
			}

			if (!string.IsNullOrEmpty(ycmc))
			{
				sql += " and X.ycmc like @ycmc";
			}
			if (!string.IsNullOrEmpty(dlCode))
			{
				sql += " and X.dlCode = @dlCode";
			}

			par = new DbParameter[]
			{
				new SqlParameter("@organizeId", organizeId),
				new SqlParameter("@zt", zt ),
				new SqlParameter("@ypflCode", ypflCode ),
				new SqlParameter("@keyword", "%" + keyword  + "%"),
				new SqlParameter("@ypgg", "%" + ypgg  + "%"),
				new SqlParameter("@ycmc", "%" + ycmc  + "%"),
				new SqlParameter("@dlCode",dlCode )
			};

			return QueryWithPage<SysMedicineVO>(sql, pagination, par);
		}
		
		public SysMedicineVO GetFormJson(int keyValue)
		{
			const string sql = @"
SELECT TOP 1000 x.ypId, x.zxdw,x.ypCode,x.ypmc,x.spm,x.py,x.bzdw,x.mzcls,x.mzcldw,x.zycls,x.zycldw,x.cxjje,
x.djdw,Convert(decimal(18,4),x.lsj) lsj,Convert(decimal(18,4),x.pfj) pfj,x.zfbl,x.zfxz,x.dlCode,x.ycmc,x.zt,0 bgbz,x.ypbzdm,x.nbdl,x.mzzybz,x.CreateTime,d.pzwh,
d.yptssx,d.ypflCode,d.zlff,d.jzlx,d.mrbzq,d.sjap,d.zbbz,d.yl,d.yldw,d.zjtzsj,d.ghdw,d.ypcd,d.ypgg,d.ybdm,d.xnhybdm,
x.jx,x.jl,x.jldw,x.OrganizeId,x.bzs,x.CreatorCode, c.dlmc sfdlMc, e.jxmc jxmc, f.ypflmc ypflMc ,d.mrjl,d.mrpc,
x.isKss,x.kssId,x.jybz,d.ybbz,x.bz,d.gjybdm,x.tsypbz,d.gjybmc,d.dcxl,d.mbxl,d.mryf,g.yfmc mryfmc,d.ybgg ybgg
FROM xt_yp_base(NOLOCK) x  
LEFT JOIN xt_ypsx_base(NOLOCK) d on x.ypId=d.ypId 
LEFT JOIN xt_sfdl_base(NOLOCK) c ON x.dlCode = c.dlCode and c.OrganizeId = x.OrganizeId
LEFT JOIN xt_ypjx(NOLOCK) e ON x.jx = e.jxCode
LEFT JOIN xt_ypfl(NOLOCK) f ON d.ypflCode = f.ypflCode
LEFT JOIN xt_ypyf(NOLOCK) g ON g.yfCode=d.mryf
WHERE x.ypId =@ypId
";
			DbParameter[] par =
			{
				new SqlParameter("@ypId", keyValue)
			};
			return FirstOrDefault<SysMedicineVO>(sql, par);
		}


		public void SyncYbyp(string organizeId)
		{
			_databaseFactory.Get().Database.ExecuteSqlCommand("EXEC InsertIntoYpTables");
		}


		public void SyncCommonDrug(SysMedicineBaseEntity sysMedicineBaseEntity, string organizeId)
		{
			//插入药品信息yp
			var sysMedicinePropertyBaseEntity =
				_sysMedicinePropertyBaseRepo.FindEntity(p => p.ypId == sysMedicineBaseEntity.ypId);
			sysMedicineBaseEntity.ypId = 0;
			sysMedicineBaseEntity.OrganizeId = organizeId;
			sysMedicineBaseEntity.Create();
			var entity = sysMedicineBaseEntity.ToJson().ToObject<SysMedicineEntity>().Clone();
			_sysMedicineRepository.Insert(entity);
			//药品属性ypsx
			sysMedicinePropertyBaseEntity.ypsxId = 0;
			sysMedicinePropertyBaseEntity.ypId = entity.ypId;
			sysMedicinePropertyBaseEntity.OrganizeId = organizeId;
			sysMedicinePropertyBaseEntity.Create();
			_sysMedicinePropertyRepo.Insert(
				sysMedicinePropertyBaseEntity.ToJson().ToObject<SysMedicinePropertyEntity>());

		}

		public void SyncCommonSfdl(SysChargeCategoryBaseEntity sysChargeCategoryBaseEntity, string organizeId)
		{
			sysChargeCategoryBaseEntity.dlId = 0;
			sysChargeCategoryBaseEntity.OrganizeId = organizeId;
			sysChargeCategoryBaseEntity.Create();
			_sysChargeCategoryRepository.SubmitForm(sysChargeCategoryBaseEntity.ToJson().ToObject<SysChargeCategoryEntity>(), 0);
		}
		
		public void SyncCommonSfxm(SysChargeItemBaseEntity sysChargeItemBaseEntity, string organizeId)
		{
			sysChargeItemBaseEntity.sfxmId = 0;
			sysChargeItemBaseEntity.OrganizeId = organizeId;
			sysChargeItemBaseEntity.Create();
			_sysChargeItemRepository.SubmitForm(sysChargeItemBaseEntity.ToJson().ToObject<SysChargeItemEntity>(), 0);
		}

		
		
		
		
		
	}
}