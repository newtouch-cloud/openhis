using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects;

namespace Newtouch.HIS.Application
{
    public interface ISysDiagnoseApp
    {

        List<ZDSelect> GetzdSelect(string zd);
    }
}
