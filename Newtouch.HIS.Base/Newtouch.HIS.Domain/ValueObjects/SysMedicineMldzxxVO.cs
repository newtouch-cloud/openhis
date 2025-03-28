using System;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    ///目录对照上传
    /// </summary>
    public class SysMedicineMldzxxVO : IEntity<SysMedicineMldzxxVO>
    {
        public string code { get; set; }
        public string name { get; set; }
        public string gg { get; set; }
        public string gjybdm { get; set; }
        public string isdz { get; set; }
    }
}
