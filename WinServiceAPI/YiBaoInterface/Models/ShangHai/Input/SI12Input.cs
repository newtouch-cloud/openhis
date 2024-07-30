using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Input
{
    public class SI12Input:InputBase
    {

        public string cardtype { get; set; }
        public string carddata { get; set; }
        public string deptid { get; set; }

        public string personspectag { get; set; }

        public string yllb { get; set; }

 
        public string persontype { get; set; }

    
        public string gsrdh { get; set; }

        public List<Zdnos> zdnos { get; set; }

  
        public string dbtype { get; set; }

 
        public string jsksrq { get; set; }

  
        public string jsjsrq { get; set; }

        public int jzcs { get; set; }


        public string jzdyh { get; set; }

    
        public string xsywlx { get; set; }

        public string jssqxh { get; set; }

        public List<Mxzdhs> mxzdhs { get; set; }
    }
}
