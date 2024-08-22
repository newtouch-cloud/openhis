using System;

namespace Newtouch.HIS.Domain.DTO.InputDto.OutpatientAccounting
{
    public class mxzxDto
    {
        public int sl { get; set; }
        public int zll { get; set; }
    }

    public class zxGirdDto
    {
        public string jzjhmxId { get; set; }
        public string kflb { get; set; }
        public string zlsgh { get; set; }
    }

    public class cxzxGirdDto
    {
        public string zxId { get; set; }
        public DateTime zxsj { get; set; }
        public string zlsgh { get; set; }
        public string kflb { get; set; }
    }
}
