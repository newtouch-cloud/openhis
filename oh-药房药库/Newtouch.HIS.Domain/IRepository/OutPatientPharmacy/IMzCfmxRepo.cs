using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// ���ﴦ��
    /// </summary>
    public interface IMzCfmxRepo : IRepositoryBase<MzCfmxEntity>
    {
        /// <summary>
        /// ���ݴ����Ż�ȡ������ϸ
        /// </summary>
        /// <param name="cfh"></param>
        /// <returns></returns>
        List<MzCfmxEntity> GetCfmxByCfh(string cfh);

        /// <summary>
        /// �ж��Ƿ��Ѵ���
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="gg">���</param>
        /// <returns></returns>
        bool IsExist(string cfh, string ypCode, string organizeId, string gg = "");

        /// <summary>
        /// ɾ���ϵĴ�����ϸ
        /// </summary>
        /// <param name="cfh">������</param>
        /// <returns></returns>
        int DeleteCfmx(string cfh);

        /// <summary>
        /// ɾ���ϵĴ�����ϸ
        /// </summary>
        /// <param name="cfhs">�������б�</param>
        /// <returns></returns>
        int DeleteCfmx(List<string> cfhs);
    }
}