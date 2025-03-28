using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.IDomainServices
{
    public interface IZybrjbxxDmnService
    {
        BabasyVO GetPatBasicInfo_basy(string orgId, string zyh);
    }
}
