using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects.MedicalRecord
{
    public class LisReportSqdhValueMxVo
    {
        public string xmmc { get; set; } //项目名称
        public string jyjg { get; set; } //检验结果
        public string gdbj { get; set; } //高低标记
        public string ckzfw { get; set; } //参考值范围
        public string dw { get; set; } //单位
    }
}
