using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using Newtouch.HIS.Sett.Request.SelfService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices.OutpatientManage
{
	public interface ISelfServiceDmnService
	{
		zzjCardInfoVO queryAppCardInfo(queryAppCardInfoReqDTO dto);
		OutpfeeMasterInfoVO queryOutpfeeMasterInfo(queryOutpfeeMasterInfoReqDTO dto);
        List<OutpfeeMasterInfoList> queryOutpfeeMasterInfoList(queryOutpfeeMasterInfoReqDTO dto);
		OutpfeeDetailVO queryOutpfeeDetailInfo(queryOutpfeeDetailInfoReqDTO dto);
		InPatientInfoVO QueryInPatientInfo(queryAppCardInfoReqDTO dto);
	}
}
