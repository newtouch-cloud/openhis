using Newtouch.Domain.DTO.InputDto;

namespace Newtouch.Domain.DTO.OutputDto
{
    public class InpMedReturnResultDataDto : InpMedReturnRequestDto
    {
        public bool IsSucceed { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
    }
}
