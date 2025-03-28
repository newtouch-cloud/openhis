using Mapster;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.WebAPI.Manage.Areas.ExtClinic;

namespace NewtouchHIS.WebAPI.Manage.Utilities
{
    /// <summary>
    /// 实体类映射配置
    /// </summary>
    public class MapService : IRegister
    {
        /****************************
         * NewConfig 会覆盖已存在的类型映射配置
         * ForType 配置增强
         * 
         * 
         * 
         * **********************************/
        void IRegister.Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<ClinicPatMedicalRecordDTO, PatMedicalRecordResponse>
               .ForType()
               .Map(his => his.xm, clinic => clinic.name)
               .Map(his => his.zs, clinic => clinic.patientTell)
               .Map(his => his.xbs, clinic => clinic.nowHistory)
               .Map(his => his.jws, clinic => clinic.beforeHistory)
               .Map(his => his.gms, clinic => clinic.allergyHistory)
               .Map(his => his.ct, clinic => clinic.physiqueCheck)
               .Map(his => his.fzjc, clinic => clinic.assistCheck)
               .Map(his => his.clfa, clinic => clinic.handlingSituation)
               .Map(his => his.yjs, clinic => clinic.lunariaHistory)
               .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);
            TypeAdapterConfig<SendPatMedicalRecordRequest, ClinicPatMedicalRecordDTO>
               .ForType()
               .Map(clinic => clinic.diagnosisId, his => his.Sqlsh)
               .Map(clinic => clinic.patientTell, his => his.zs)
               .Map(his => his.nowHistory, clinic => clinic.xbs)
               .Map(his => his.beforeHistory, clinic => clinic.jws)
               .Map(his => his.allergyHistory, clinic => clinic.gms)
               .Map(his => his.physiqueCheck, clinic => clinic.ct)
               .Map(his => his.assistCheck, clinic => clinic.fzjc)
               .Map(his => his.handlingSituation, clinic => clinic.clfa)
               .Map(his => his.lunariaHistory, clinic => clinic.yjs)
               .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);
        }
    }
}
