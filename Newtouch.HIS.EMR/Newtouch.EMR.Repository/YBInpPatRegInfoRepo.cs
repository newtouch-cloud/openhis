using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using FrameworkBase.MultiOrg.Repository;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Infrastructure;
using System;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.EMR.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2021-02-03 15:08
    /// 描 述：入院记录信息
    /// </summary>
    public class YBInpPatRegInfoRepo : RepositoryBase<YBInpPatRegInfoEntity>, IYBInpPatRegInfoRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public YBInpPatRegInfoRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        public void SubmitRegInfo(YBInpPatRegInfoEntity ety)
        {
            if (ety != null)
            {
                try
                {


                    if (!string.IsNullOrWhiteSpace(ety.BKF263))
                    {
                        if (!string.IsNullOrWhiteSpace(ety.BKC192))
                        {
                            ety.BKC192 = ety.BKC192 == "00010101000000" ? null : Convert.ToDateTime(ety.BKC192).ToString("yyyyMMddHHmmss");
                        }
                        ety.Modify(ety.Id);

                        this.Update(ety);
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(ety.BKC192))
                        {
                            ety.BKC192 = ety.BKC192 == "00010101000000" ? null : Convert.ToDateTime(ety.BKC192).ToString("yyyyMMddHHmmss");
                        }
                        ety.BKF263 = EFDBBaseFuncHelper.Instance.MedicalRecordLSH(ety.OrganizeId);
                        ety.Create(true, ety.Id);
                        this.Insert(ety);
                    }
                }
                catch (Exception ex)
                {
                    throw new FailedException("保存失败，" + ex.Message);
                }
            }
            
            //if (!string.IsNullOrWhiteSpace(keyValue))
            //{
            //    YBInpPatRegInfoEntity etyold = FindEntity(keyValue);
            //    if (etyold != null)
            //    {
            //        #region update
            //        etyold.AKE021 = ety.AKE021;
            //        etyold.AKF001 = ety.AKF001;
            //        etyold.AKF002 = ety.AKF002;
            //        etyold.BKB410 = ety.BKB410;
            //        etyold.BKC192 = ety.BKC192;
            //        etyold.BKF110 = ety.BKF110;
            //        etyold.BKF654 = ety.BKF654;
            //        etyold.BKF125 = ety.BKF125;
            //        etyold.BKF476 = ety.BKF476;
            //        etyold.BKF360 = ety.BKF360;
            //        etyold.BKF385 = ety.BKF385;
            //        etyold.BKF740 = ety.BKF740;
            //        etyold.BKF185 = ety.BKF185;
            //        etyold.BKF150 = ety.BKF150;
            //        etyold.BKF409 = ety.BKF409;
            //        etyold.BKF285 = ety.BKF285;
            //        etyold.BKF299 = ety.BKF299;
            //        etyold.BKF742 = ety.BKF742;
            //        etyold.BKF189 = ety.BKF189;
            //        etyold.BKF410 = ety.BKF410;
            //        etyold.BKF744 = ety.BKF744;
            //        etyold.BKF352 = ety.BKF352;
            //        etyold.BKF339 = ety.BKF339;
            //        etyold.BKF350 = ety.BKF350;
            //        etyold.BKF343 = ety.BKF343;
            //        etyold.BKF344 = ety.BKF344;
            //        etyold.BKF351 = ety.BKF351;
            //        etyold.BKF353 = ety.BKF353;
            //        etyold.BKF349 = ety.BKF349;
            //        etyold.BKF340 = ety.BKF340;
            //        etyold.BKF341 = ety.BKF341;
            //        etyold.BKF346 = ety.BKF346;
            //        etyold.BKF338 = ety.BKF338;
            //        etyold.BKF348 = ety.BKF348;
            //        etyold.BKF335 = ety.BKF335;
            //        etyold.BKF336 = ety.BKF336;
            //        etyold.BKF347 = ety.BKF347;
            //        etyold.BKF337 = ety.BKF337;
            //        etyold.BKF345 = ety.BKF345;
            //        etyold.BKF342 = ety.BKF342;
            //        etyold.BKF547 = ety.BKF547;
            //        etyold.BKF746 = ety.BKF746;
            //        etyold.BKF458 = ety.BKF458;
            //        etyold.BKF097 = ety.BKF097;
            //        etyold.BKF099 = ety.BKF099;
            //        etyold.AKE021 = ety.AKE021;
            //        etyold.AKE021 = ety.AKE021;
            //        etyold.AKE021 = ety.AKE021;
            //        etyold.AKE021 = ety.AKE021;
            //        etyold.AKE021 = ety.AKE021;
            //        etyold.AKE021 = ety.AKE021;
            //        etyold.AKE021 = ety.AKE021;
            //        etyold.AKE021 = ety.AKE021;
            //        etyold.AKE021 = ety.AKE021;
            //        etyold.AKE021 = ety.AKE021;
            //        etyold.AKE021 = ety.AKE021;
            //        etyold.AKE021 = ety.AKE021;
            //        etyold.AKE021 = ety.AKE021;
            //        etyold.AKE021 = ety.AKE021;
            //        etyold.AKE021 = ety.AKE021;
            //        etyold.AKE021 = ety.AKE021;
            //        etyold.AKE021 = ety.AKE021;
            //        etyold.AKE021 = ety.AKE021;
            //        etyold.AKE021 = ety.AKE021;
            //        etyold.AKE021 = ety.AKE021;
            //        etyold.AKE021 = ety.AKE021;
            //        etyold.AKE021 = ety.AKE021;
            //        #endregion
            //    }
            //}
        }

    }
}