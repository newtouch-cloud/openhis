using AutoMapper;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.WebAPI.Manage.Areas.ExtClinic;
using NewtouchHIS.WebAPI.Manage.Models;
using NewtouchHIS.WebAPI.Manage.Models.OutPatient;

namespace NewtouchHIS.WebAPI.Manage
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {

            #region 
            CreateMap<BookingReqBase, BookingRequest>();
            CreateMap<ScheduleRequest, SchedulePageRequest>();
            CreateMap<OutpPresInfoRequest, OutpPresInfoHisApiRequest>();

            CreateMap<MsgNoticeEntity, MsgNoticeVO>();
            CreateMap<MsgNoticeQueueEntity, MsgNoticeQueueVO>();
           
            CreateMap<SysOrgVo, SysOrgIndexVo>();
            //CreateMap<ClinicPatMedicalRecordDTO,PatClinicMedicalRecordResponse>();
            #endregion
        }

    }
}
