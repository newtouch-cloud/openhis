using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.HeaSecReadInfo
{
    public class ReadCardBas: OutputBase
    {
        public string IssuingAreaCode { get; set; }        // 发卡地区行政区划代码
        public string SocialSecurityNumber { get; set; }   // 社会保障号码
        public string CardNumber { get; set; }             // 卡号
        public string CardIdentificationCode { get; set; } // 卡识别码
        public string Name { get; set; }                   // 姓名
        public string CardResetInfo { get; set; }          // 卡复位信息（仅取历史字节）
        public string SpecificationVersion { get; set; }   // 规范版本
        public string IssuingDate { get; set; }            // 发卡日期
        public string ExpirationDate { get; set; }         // 卡有效期
        public string TerminalNumber { get; set; }         // 终端机编号
        public string TerminalDeviceNumber { get; set; }   // 终端设备号

    }
}
