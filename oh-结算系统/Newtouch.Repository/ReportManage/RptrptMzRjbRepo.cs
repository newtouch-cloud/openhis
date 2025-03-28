using System;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class RptrptMzRjbRepo : RepositoryBase<RptrptMzRjbEntity>, IRptrptMzRjbRepo
	{
        public RptrptMzRjbRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

		public RptrptMzRjbEntity GetLastMzrjEntity(string orgId, string UserCode)
		{
			RptrptMzRjbEntity rjEntity = this.IQueryable()
				.Where(p => p.OrganizeId == orgId && p.czr == UserCode && p.zt == "1")
				.OrderByDescending(a => a.CreateTime)
				.FirstOrDefault();
			if (rjEntity==null)
			{
				rjEntity = new RptrptMzRjbEntity() {jssj = DateTime.Now.AddYears(-1)};
			}
			return rjEntity;
		}

		public IList<OutPatientMzrjDto> GetLastMzrjEntityList(string orgId, string UserCode, string keyword)
		{
			var entityList = this.IQueryable()
				.Where(p => p.OrganizeId == orgId && p.czr == UserCode && p.zt == "1" && (p.kssj.ToString().Contains(keyword) || p.jssj.ToString().Contains(keyword)))
				.OrderByDescending(a => a.CreateTime)
				.ToList();
			IList<OutPatientMzrjDto> outList = new List<OutPatientMzrjDto>();
			foreach (var item in entityList)
			{
				OutPatientMzrjDto dto = new OutPatientMzrjDto()
				{
					Id = item.Id,
					kssj = item.kssj.ToString("yyyy-MM-dd HH:mm:ss"),
					jssj = item.jssj.ToString("yyyy-MM-dd HH:mm:ss"),
					xjzf = item.xjzf.ToString(),
					zje = item.zje.ToString()
				};
				outList.Add(dto);
            }
			return outList;
		}
	}
}
