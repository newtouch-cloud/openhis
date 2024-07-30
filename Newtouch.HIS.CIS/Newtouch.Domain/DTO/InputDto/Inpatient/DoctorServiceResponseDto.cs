using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.InputDto.Inpatient
{
    /// <summary>
    /// 判断医嘱是否重复
    /// </summary>
    public class DSrepeatResponseDto
    {
        public string Id { get; set; }
        //0 临时医嘱 1 长期医嘱
        public int clbz { get; set; }
        public string xmdm { get; set; }
        public string  xmmc { get; set; }
    }
}
