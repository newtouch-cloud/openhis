using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSViewer_MVC_Core.Models.Enums
{
    public enum ReportType
    {
        /// <summary>
        /// 区域报表
        /// </summary>
        AREA=1,

        /// <summary>
        /// 页面报表
        /// </summary>
        PAGE,

        /// <summary>
        /// RDL报表
        /// </summary>
        RDL
    }
    /// <summary>
    /// 状态
    /// </summary>
    public enum Status
    { 
        /// <summary>
        /// 停用
        /// </summary>
        ty=0,
        /// <summary>
        /// 启用
        /// </summary>
        qy=1
    }
}
