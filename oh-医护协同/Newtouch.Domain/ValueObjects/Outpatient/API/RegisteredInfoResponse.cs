using Newtouch.Core.Common;
using Newtouch.Domain.ValueObjects.API;
using System.Collections.Generic;

namespace Newtouch.Domain.ValueObjects
{
    public class SignInStateRequest
    {
        public string mzh { get; set; }
        public string calledstu { get; set; }
        public string yhcode { get; set; }
    }
}
