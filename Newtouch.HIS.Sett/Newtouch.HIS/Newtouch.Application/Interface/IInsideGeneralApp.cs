﻿using Newtouch.HIS.Sett.Request;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 内部接口调用业务
    /// </summary>
    public interface IInsideGeneralApp
    {
        /// <summary>
        /// 公共入口
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        void PublicPortal(MqGeneralTaskRequestDto requestDto);

    }
}