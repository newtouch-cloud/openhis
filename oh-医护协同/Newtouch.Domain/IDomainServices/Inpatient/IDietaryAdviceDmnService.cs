using Newtouch.Domain.ValueObjects.Inpatient;
using System.Collections.Generic;

namespace Newtouch.Domain.IDomainServices
{
    public interface IDietaryAdviceDmnService
    {
        DAFormVO GetFormJson(string keyvalue);

        void SubmitService(string orgId, List<DAMXFormVO> reqdietaryservices, List<string> delData);

        List<DAmxGridList> GetmxList(string Id, string orgId);
    }
}