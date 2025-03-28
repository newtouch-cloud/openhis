using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.VEntity;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// ����ⵥ��
    /// </summary>
    public interface IKfCrkdjDmnService
    {
        /// <summary>
        /// ��ȡ����ⵥ��������Ϣ
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="param"></param>
        /// <param name="alldjlx">���û�����ӵ�е����е�������</param>
        /// <param name="warehouseId">��ǰ�ⷿ</param>
        /// <param name="ope">����  query����ѯ  approval�����</param>
        /// <returns></returns>
        IList<VCrkdjEntity> GetCrkdjMainList(Pagination pagination, CrkdjSearchParamDTO param, string[] alldjlx, string warehouseId, string ope = "");

        /// <summary>
        /// ��ȡ�������ϸ
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VCrkdjmxEntity> GetCrkdjmxList(string crkId, string organizeId);

        /// <summary>
        /// ��ȡ��Ӧ�̺ͷ�Ʊ��Ϣ
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VFphAndGysEntity> GetFphAndGysInfo(string keyWord, string warehouseId, string organizeId);


        /// <summary>
        /// ɾ�������������ϸ�� ����
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int DeleteDjById(long crkId, string organizeId);

        /// <summary>
        /// ���浥��
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="mxList"></param>
        /// <returns></returns>
        string SaveDj(KfCrkdjEntity dj, List<KfCrkmxEntity> mxList);

        /// <summary>
        /// ȡ���ѳɹ�������ϸ
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="successList"></param>
        void CancelCrkmx(KfCrkdjEntity dj, List<KfCrkmxEntity> successList);

        /// <summary>
        /// ��ѯ������͵���
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="warehouseId"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeid"></param>
        /// <param name="shzt"></param>
        /// <returns></returns>
        List<VInStorageDeliveryInfoEntity> SelectInStorageDeliveryInfo(string keyword, string warehouseId, string userCode, string organizeid, string shzt = "4");

        /// <summary>
        /// ͨ�����͵��Ż�ȡ�������ϸ
        /// </summary>
        /// <param name="deliveryNo"></param>
        /// <param name="djh"></param>
        /// <param name="warehouseId"></param>
        /// <param name="organizeid"></param>
        /// <param name="gysId"></param>
        /// <returns></returns>
        List<VCrkdjmxInfoEntity> SelectCrkmxByDeliveryNo(string deliveryNo, string djh, string warehouseId, string organizeid, string gysId = "");

        /// <summary>
        /// ɾ��ָ������ⵥ�ݺ���ϸ
        /// </summary>
        /// <param name="crkId"></param>
        /// <returns></returns>
        string DeleteCrkdj(long crkId);
    }
}