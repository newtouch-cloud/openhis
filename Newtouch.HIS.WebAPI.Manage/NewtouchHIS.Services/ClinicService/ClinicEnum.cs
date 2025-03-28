using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Services.ClinicService
{
    /// <summary>
    /// 诊所字典：远程诊疗申请状态
    /// （0:未提交, 1:待确认、2:就诊中、3:已结束、4:已发药、5:已退回）
    /// </summary>
    public enum EmunClinicRemoteTreatedStu
    {
        [Description("未提交")]
        wtj = 0,
        [Description("待确认")]
        dqr = 1,
        [Description("就诊中")]
        jzz = 2,
        [Description("已结束")]
        yjs = 3,
        [Description("已发药")]
        yfy = 4,
        [Description("已退回")]
        yth = 5,
    }
}
