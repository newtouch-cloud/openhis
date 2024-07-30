using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.TherapeutistCompleteManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.SystemManage
{
    public interface ITreatmentportfolioRepo : IRepositoryBase<TreatmentportfolioEntity>
    {

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        //void Insert( string OrganizeId,string zhmc, string zhcode, string ord, string zlxm, string zlxmmc, string price, string zlxmpy, string zhje);
        void ADDInsert(TreatmentportfolioEntity entity, string keyValue);


        /// <summary>
        /// 加载数据
        /// </summary>
        /// <returns></returns>
        //IList<TreatmentportfolioEntity> Listselect();

        //IList<TreatmentportfolioEntity> Keyword(string keyword);

        //IList<TreamentviewVO> Loginview();



        IList<TreatmentportfolioEntity> selectid(TreatmentportfolioEntity entity, string keyValue);



        void Updatexg(TreatmentportfolioEntity entity, string keyValue);
        

        void deleteid(string keyValue);

        void deletemc(string keyValue,string zhcodemc);



        void ADDrowdata(TreatmentportfolioEntity entity);

        IList<TreatmentportfolioEntity> TJchaxun(string zhcodetj,string sfxmmc123);

        IList<TreatmentportfolioEntity> Codecx(TreatmentportfolioEntity entity, string zhcode);

        

        string sfdlcx(string sfxmmc123);


        IList<TreamentviewVO> Loginview(Pagination pagination);
        IList<TreamentviewVO> Keyword(Pagination pagination,string keyword);
    }
}
