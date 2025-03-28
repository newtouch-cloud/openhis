using Newtouch.Herp.Domain.DTO.OutputDto;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// 处理方法
    /// </summary>
    public class IProcessor : AppBase
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
