using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiBaoScheduling.Common
{
    public class CommonUtility
    {
        public static string GetNewGuid()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
