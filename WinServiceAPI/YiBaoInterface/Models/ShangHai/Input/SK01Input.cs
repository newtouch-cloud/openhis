using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Input
{
    public class SK01Input:InputBase
    {
        public string cardtype { get; set; }
        public string carddata { get; set; }
      
        public string translsh { get; set; }

        
        public decimal totalexpense { get; set; }

       
        public string xsywlx { get; set; }
    }
}
