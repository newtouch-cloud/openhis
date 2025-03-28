using Newtouch.Herp.Domain.Entity;
using System.Web;
using Newtouch.Herp.Domain.ValueObjects;
using Newtouch.Herp.Domain.Entity.VEntity;

namespace Newtouch.Herp.Application.Interface
{
    public interface IProductApp
    {
        /// <summary>
        /// ����ά�����ύ
        /// </summary>
        /// <param name="wzProductEntity"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        int SubmitForm(VProductInfoEntity wzProductEntity, string keyWord);

        /// <summary>
        /// �ϴ�ͼƬ
        /// </summary>
        /// <param name="file"></param>
        /// <returns>ͼƬ�ϴ�·��</returns>
        string UploadImag(HttpPostedFileBase file);

        /// <summary>
        /// �ύ���ʵ�λ
        /// </summary>
        /// <param name="rel"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int SubmitRelProductUnit(ProductUnitRelVo rel, string organizeId);
    }
}