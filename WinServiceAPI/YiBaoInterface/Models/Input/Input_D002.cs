using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class Input_D002 : InputBase
    {
        public string fixmedinsCode { get; set; }//定点机构代码  定点机构唯一标识，用于识别机构对应的医保数字证书，CertId 和其保持一致
        public string originalValue { get; set; }//原始待签名处方信息  JSONString序列化（对第3.3.4.1中1-20字段进行JSONString）后的base64字符值
        public string originalRxFile { get; set; }//原始待签名处方文件 文件base64的字符值 
        public string extras { get; set; }//扩展字段 
    }
}
