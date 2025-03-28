using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.InputDto
{
    public class InpMedReturnNeedDto:InpMedReturnRequestDto
    {
        public string fyId { get; set; }
        public string ypmc { get; set; }
    }
}
