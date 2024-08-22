using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_D003 : OutputBase
    {
        public string hiRxno { get; set; }
        public string rxStasCodg { get; set; }//1 	有效 2 	已失效    3 	已撤销 
        public string rxStasName { get; set; }
    }
}
