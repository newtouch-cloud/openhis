using Newtouch.HIS.Domain.DTO.OutputDto;
using System.Threading;

namespace Newtouch.HIS.Application.Strategy
{
    /// <summary>
    /// 策略
    /// </summary>
    public interface ICommitStrategy
    {
        /// <summary>
        /// 执行策略
        /// </summary>
        /// <param name="cts"></param>
        /// <param name="request"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool Process(CancellationTokenSource cts, dynamic request, ActResult result);

        /// <summary>
        /// 后处理
        /// </summary>
        /// <param name="cts"></param>
        /// <param name="request"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool After(CancellationTokenSource cts, dynamic request, ActResult result);
    }
}
