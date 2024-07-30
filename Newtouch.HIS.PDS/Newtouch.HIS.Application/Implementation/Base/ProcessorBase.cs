using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using FrameworkBase.MultiOrg.Application;
using Newtouch.Common.Operator;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.ExException;
using Newtouch.Infrastructure.Log;
using Newtouch.PDS.Requset;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 处理过程
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ProcessorFun<T> : ProcessorBase, IProcess
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
                if (HttpContext.Current == null) return;
                if (OperatorProvider.GetCurrent() == null) return;
                Tags.Add(Constants.OrganizeId, OperatorProvider.GetCurrent().OrganizeId);
                Tags.Add(Constants.CreatorCode, OperatorProvider.GetCurrent().UserCode);
                Tags.Add(Constants.Yfbm, Constants.CurrentYfbm == null ? "" : Constants.CurrentYfbm.yfbmCode);
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
            finally
            {
                Dispose(result);
            }
            return result;
        }

        /// <summary>
        /// 异步处理
        /// </summary>
        /// <param name="result"></param>
        protected virtual void AsyncProcess(ActResult result)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                Thread.CurrentThread.IsBackground = false;
                AsyncAction(result);
            });
        }

        /// <summary>
        /// 销毁处理
        /// </summary>
        /// <param name="result"></param>
        protected virtual void Dispose(ActResult result)
        {
        }
    }

    /// <summary>
    /// 处理方法
    /// </summary>
    public abstract class ProcessorBase : AppBase
    {
        /// <summary>
        /// 验证Request
        /// </summary>
        protected virtual ActResult Validata()
        {
            return new ActResult();
        }

        /// <summary>
        /// 预处理
        /// </summary>
        protected virtual void BeforeAction(ActResult actResult)
        {
        }

        /// <summary>
        /// 主处理
        /// </summary>
        /// <returns></returns>
        protected virtual void Action(ActResult actResult)
        {
        }

        /// <summary>
        /// 后处理
        /// </summary>
        protected virtual void AfterAction(ActResult actResult)
        {
        }

        /// <summary>
        /// 异步处理
        /// </summary>
        protected virtual void AsyncAction(ActResult actResult)
        {
        }
    }
}
