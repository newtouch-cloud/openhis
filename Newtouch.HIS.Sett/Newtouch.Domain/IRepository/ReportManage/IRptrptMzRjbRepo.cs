using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.PatientManage;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRptrptMzRjbRepo : IRepositoryBase<RptrptMzRjbEntity>
	{
		RptrptMzRjbEntity GetLastMzrjEntity(string orgId, string UserCode);

		IList<OutPatientMzrjDto>  GetLastMzrjEntityList(string orgId, string UserCode, string keyword);
	}
}
