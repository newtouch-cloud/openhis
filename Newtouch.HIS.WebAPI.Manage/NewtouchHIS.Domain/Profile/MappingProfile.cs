using AutoMapper;
using NewtouchHIS.Domain.Entity.PatientCenter;
using NewtouchHIS.Domain.InterfaceObjets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SysPatientBasicInfoEntity, SysPatInfoVO>();
        }
    }
}
