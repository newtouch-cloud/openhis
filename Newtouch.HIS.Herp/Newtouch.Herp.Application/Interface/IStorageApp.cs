using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// ���ά��
    /// </summary>
    public interface IStorageApp
    {
        /// <summary>
        /// �ύ�ⲿ���
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns>error message</returns>
        string InStorageSubmit(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx);

        /// <summary>
        /// �ύ�ⲿ����
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns>error message</returns>
        string OutStorageSubmit(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx);

        /// <summary>
        /// �ύֱ�ӳ���
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns></returns>
        string DirectDeliverySubmit(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx);

        /// <summary>
        /// �ύ�ڲ������˻�
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns></returns>
        string DeliveryOfReturnSubmit(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx);
    }
}