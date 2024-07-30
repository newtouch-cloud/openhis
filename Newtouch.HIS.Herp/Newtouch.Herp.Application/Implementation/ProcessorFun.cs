using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.ExException;
using Newtouch.Herp.Infrastructure.Log;

namespace Newtouch.Herp.Application.Implementation
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
            Tags = new Dictionary<string, string>();
            try
            {
                if (HttpContext.Current != null)
                {
                    Tags.Add(Constants.OrganizeId, OperatorProvider.GetCurrent().OrganizeId);
                    Tags.Add(Constants.KfId, Constants.CurrentKf.currentKfId);
                    Tags.Add(Constants.CreatorCode, OperatorProvider.GetCurrent().UserCode);
                }
            }
            catch (Exception ex)
            {
                LogCore.Error("ProcessorFun error", ex, "构造方法异常");
            }
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
                PerformanceMonitoring.RequestMonitor(_ => Action(result), result, "ProcessorFun Process Action", Tags);
                if (!result.IsSucceed)
                {
                    return result;
                }
                PerformanceMonitoring.RequestMonitor(_ => AfterAction(result), result, "ProcessorFun Process AfterAction", Tags);
                if (!result.IsSucceed)
                {
                    return result;
                }
                PerformanceMonitoring.RequestMonitor(_ => AsyncProcess(result), result, "ProcessorFun Process AsyncAction", Tags);
            }
            catch (FailedException ex)
            {
                result.IsSucceed = false;
                result.ResultCode = (int)ResultCode.ValidationFailure;
                result.ResultMsg = ex.Msg;
                LogCore.Error("ProcessorFun Process error", ex, message: result.ResultMsg, addInfo: Tags);
            }
            catch (ProcessException ex)
            {
                result.IsSucceed = false;
                result.ResultCode = (int)ResultCode.InternalProcessingAbnormality;
                result.ResultMsg = ex.Msg;
                LogCore.Error("ProcessorFun Process error", ex, message: result.ResultMsg, addInfo: Tags);
            }
            catch (Exception ex)
            {
                result.IsSucceed = false;
                result.ResultCode = (int)ResultCode.Other;
                result.ResultMsg = "内部处理失败,请联系接口管理员";
                LogCore.Error("ProcessorFun Process error", ex, message: string.Format("Message:\n{0}\nStackTrace:\n{1}", ex.Message, ex.StackTrace), addInfo: Tags);
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
