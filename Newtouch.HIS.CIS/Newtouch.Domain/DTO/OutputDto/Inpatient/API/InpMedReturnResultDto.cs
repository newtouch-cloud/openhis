using Newtouch.Domain.DTO.InputDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.OutputDto
{
    public class InpMedReturnResultDto 
    {
        public string ResultCode { get; set; }
        public string ResultMsg { get; set; }
        public bool IsSucceed { get; set; }
        public IList<InpMedReturnResultDataDto> Data { get; set; }
    }
}
