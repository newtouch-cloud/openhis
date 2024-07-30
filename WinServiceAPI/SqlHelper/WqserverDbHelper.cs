using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlHelper
{
    public class WqserverDbHelper:DBHelper
    {
        public override string connectionString
        {
            get { return GetConnectionString("SQLConnectionStringWQSERVER"); }
        }
    }
}
