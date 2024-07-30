using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.Infrastructure.ExException;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 处理过程
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ProcessorFun<T> : IProcessor, IProcess
    {
        protected T Request;
        protected Dictionary<string, string> Tags;

        /// <summary>
        /// 基类初始化
        /// </summary>
        /// <param name="request"></param>
        protected ProcessorFun(T request)
        {
            Request = request;
        }

        /// <summary>
        /// 处理过程
        /// </summary>
        /// <returns></returns>
        public ActResult Process()
        {
            var result = new ActResult();
            try
            {
                result = Validata();
                if (!result.IsSucceed)
                {
                    return result;
                }
                BeforeAction(result);
                Action(result);
                if (!result.IsSucceed)
                {
                    return result;
                }
                AfterAction(result);
                if (!result.IsSucceed)
                {
                    return result;
                }
                AsyncProcess(result);
            }
            catch (FailedException ex)
            {
                result.IsSucceed = false;
                result.ResultCode = (int)ResultCode.ValidationFailure;
                result.ResultMsg = ex.Msg;
            }
            catch (ProcessException ex)
            {
                result.IsSucceed = false;
                result.ResultCode = (int)ResultCode.InternalProcessingAbnormality;
                result.ResultMsg = ex.Msg;
            }
            catch (Exception ex)
            {
                result.IsSucceed = false;
                result.ResultCode = (int)ResultCode.Other;
                result.ResultMsg = "内部处理失败,请联系接口管理员";
            }
            return result;
        }

        /// <summary>
        /// 异步处理
        /// </summary>
        /// <param name="result"></param>
        private void AsyncProcess(ActResult result)
        {
            ThreadPool.QueueUserWorkItem(_ => AsyncAction(result));
        }
    }
}
