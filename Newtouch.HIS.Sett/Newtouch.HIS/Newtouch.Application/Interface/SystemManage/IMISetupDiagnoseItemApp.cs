using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    public interface IMISetupDiagnoseItemApp
    {
        List<MIDiagnosisAndTreatmentItemEntity> GetList();

        MIDiagnosisAndTreatmentItemEntity GetForm(string keyValue);

        void DeleteForm(string keyValue);

        void SubmitForm(MIDiagnosisAndTreatmentItemEntity zlxmEntity, string keyValue);
    }
}
