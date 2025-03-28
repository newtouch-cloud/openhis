using Newtouch.Domain.Entity.Outpatient;
using Newtouch.Domain.ValueObjects.Outpatient;
using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IRepository.Outpatient
{
    public interface IOutpatientConsultDoctorRepo : IRepositoryBase<OutpatientConsultDoctorEntity>
    {
        int SubmitForm(OutpatientConsultDoctorEntity entity);
        OutpatientConsultDoctorEntity GetTodayDoctorByConsult(string zsCode, string orgId);
		int UpdateZS(OutpatientConsultDoctorVO zsov);
        List<OutpatientConsultDoctorVO> GetRepeatDoctor(string orgId, string zsStr, string ghStr);

    }
}
