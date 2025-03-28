using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class JSONPRequestBase : RequestBase
    {
        /// <summary>
        /// jsonp回调函数
        /// </summary>
        //[Required]    //支持jsonp
        public string jsonpCallback { get; set; }
    }
}
