using Newtouch.Herp.Domain.Entity;
using System.Web;
using Newtouch.Herp.Domain.ValueObjects;
using Newtouch.Herp.Domain.Entity.VEntity;

namespace Newtouch.Herp.Application.Interface
{
    public interface IProductApp
    {
        /// <summary>
        /// 物资维护表单提交
        /// </summary>
        /// <param name="wzProductEntity"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        int SubmitForm(VProductInfoEntity wzProductEntity, string keyWord);

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="file"></param>
        /// <returns>图片上传路径</returns>
        string UploadImag(HttpPostedFileBase file);

        /// <summary>
        /// 提交物资单位
        /// </summary>
        /// <param name="rel"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        int SubmitRelProductUnit(ProductUnitRelVo rel, string organizeId);
    }
}