using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.NationECCodeDll
{
    /// <summary>
    /// 用以初始化动态库，加载使用动态库调用其它函数必须首先调用初始化函数。
    /// </summary>
    public class InitInput 
    {
        /// <summary>
        /// IP	服务端IP地址	字符型	100	Y	医疗保障平台提供的服务端IP地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 数值  医疗保障平台提供的服务端端口
        /// </summary>
        public int PORT { get; set; }

        /// <summary>
        /// 超时 数值 单位秒，访问服务端超时时间
        /// </summary>
        public int TIMEOUT { get; set; }

        /// <summary>
        /// 字符型 100 动态库日志生成所在目录：目录不能超过三级;目录必须是英文字符，不能含有空格字符;目录有写权限
        /// </summary>
        public string LOG_PATH { get; set; }

        /// <summary>
        /// 身份证读卡器类型 数值 Y   身份证读卡器类型，每个地市根据身份证机具不同而不同（白城：1 华大，2，丰联，3 华视），没有省份证读卡器 0
        /// </summary>
        public int SFZ_DRIVER_TYPE { get; set; }
    }
}
