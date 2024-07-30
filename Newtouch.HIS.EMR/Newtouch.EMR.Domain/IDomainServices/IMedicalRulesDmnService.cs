using Newtouch.EMR.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.IDomainServices
{
    public interface IMedicalRulesDmnService
    {
        void SubmitForm(RulesEntityVo entity);
    }
}
