using Newtouch.Herp.Domain.Entity.Product;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// 物资收费项目对照
    /// </summary>
    public interface IRelProductAndsfxmApp
    {
        /// <summary>
        /// 提交物资收费项目对照
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        string SubmitRelProductAndsfxm(RelProductAndsfxmEntity entity);
    }
}