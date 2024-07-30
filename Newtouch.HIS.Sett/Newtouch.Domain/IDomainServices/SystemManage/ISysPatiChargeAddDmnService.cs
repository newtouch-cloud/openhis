using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysPatiChargeAddDmnService
    {
        List<SysPatiChargeAddVo> GetSysPatiChargeAddVoList(string keyword, int? bh = null);
    }
}
