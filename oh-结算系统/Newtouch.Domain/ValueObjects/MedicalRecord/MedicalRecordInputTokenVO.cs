using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 住院项目计费 项目 的 详细信息
    /// </summary>
    public class MedicalRecordInputTokenVO
    {
       
        public int HttpStatus { get; set; }
        public string Message { get; set; }
        public BusData BusData { get; set; }
        
    }
    public class BusData
    {
        public int code { get; set; }
        public string msg { get; set; }
        public Tokendata data { get; set; }
    }
    public class Tokendata
    {
        public string Token { get; set; }
    }
}
