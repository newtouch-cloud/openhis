using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlHelper
{
    public class WqemrDBHelper:DBHelper
    {
        public override string connectionString
        {
            get { return GetConnectionString("SQLConnectionStringWQEMR"); }
        }
    }
}
