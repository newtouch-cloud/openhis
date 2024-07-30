using Newtouch.HIS.Domain.DTO.InputDTO;
using Newtouch.HIS.Domain.DTO.OutputDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Application.Interface
{
    public interface IPurchaseApp
    {
        Output_YY003 Purchase_YY003(Input_YY003 input);
    }
}
