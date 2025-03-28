using FrameworkBase.MultiOrg.Application;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.DTO.InputDTO;
using Newtouch.HIS.Domain.DTO.OutputDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Application.Implementation
{
    public class PurchaseApp : AppBase, IPurchaseApp
    {
        public Output_YY003 Purchase_YY003(Input_YY003 input) {
            Output_YY003 output = new Output_YY003();
            output.JLS = 2;
            output.SFWJ = "1";
            List<Detail_YY003> list = new List<Detail_YY003>();
            for (var i = 0; i < 3; i++)
            {
                Detail_YY003 obj = new Detail_YY003();
                obj.PSDH = "PSDH00"+i;
                obj.YQBM = "00000001";
                obj.GG = "100ml:5g*1袋";
                obj.PSL = 2;
                obj.CPM = "5%葡萄糖注射液 (直立式软袋双口管)";
                list.Add(obj);
            }
            output.Detail_YY003 = list;
            return output;
        }
    }
}
