using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JSViewer_MVC_Core.OutputModels
{
    /// <summary>
    /// 名    称：请求返回代码
    /// </summary>
    /// <remarks>
    /// 版    本：1.0
    /// 作    者：曾林
    /// 创建时间：2020年12月8日
    /// 描    述：
    /// ------------------------修改记录----------------
    /// </remarks>
    public enum WqsjResponseCode
    {
        /// <summary>
        /// 请求成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 需要登录
        /// </summary>
        NeedLogin = 1,

        /// <summary>
        /// 服务错误，通常是代码已经处理的错误
        /// </summary>
        ServiceError = 2,

        /// <summary>
        /// 权限不足
        /// </summary>
        PermissionDenied = 3,

        /// <summary>
        /// 未处理的系统异常
        /// </summary>
        SystemError = 4
    }
}
