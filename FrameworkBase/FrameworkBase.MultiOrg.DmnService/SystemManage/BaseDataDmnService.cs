using FrameworkBase.MultiOrg.Domain.DTO;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.ValueObjects;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common.Interface;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;

namespace FrameworkBase.MultiOrg.DmnService.SystemManage
{
    /// <summary>
    /// 基础数据
    /// </summary>
    public class BaseDataDmnService : DmnServiceBase, IBaseDataDmnService
    {
        private readonly ICache _cache;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseFactory"></param>
        public BaseDataDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 检索药品项目
        /// </summary>
        /// <param name="reqDto"></param>
        /// <returns></returns>
        public IList<SfxmYpSelectResultVO> SelectSfxmYp(SelectSfxmYpFilterDTO reqDto)
        {
            var paraList = new List<SqlParameter>() { };
            paraList.Add(new SqlParameter("@topCount", reqDto.topCount));
            paraList.Add(new SqlParameter("@orgId", reqDto.orgId ?? ""));
            paraList.Add(new SqlParameter("@mzzybz", reqDto.mzzybz ?? ""));
            paraList.Add(new SqlParameter("@dllb", reqDto.dllb ?? ""));
            paraList.Add(new SqlParameter("@sfdllx", reqDto.sfdllx ?? ""));
            paraList.Add(new SqlParameter("@keyword", reqDto.keyword ?? ""));
            paraList.Add(new SqlParameter("@dlCode", reqDto.dlCode ?? ""));
            paraList.Add(new SqlParameter("@isContansChildDl", reqDto.isContansChildDl ? 1 : 0));
            paraList.Add(new SqlParameter("@useypckflag", reqDto.useypckflag ? 1 : 0));
            paraList.Add(new SqlParameter("@ypyfbmCode", reqDto.ypyfbmCode ?? ""));
            paraList.Add(new SqlParameter("@containyp0ck", reqDto.containyp0ck ? 1 : 0));
            paraList.Add(new SqlParameter("@onlyybflag", reqDto.onlyybflag ? 1 : 0));
            paraList.Add(new SqlParameter("@isQyKssKZ", reqDto.isQyKssKZ ? 1 : 0));
            paraList.Add(new SqlParameter("@qxjb", reqDto.qxjb ?? "0"));

            var list = this.FindList<SfxmYpSelectResultVO>(@"exec usp_SelectSfxmYp @topCount, @orgId, @mzzybz
, @dllb, @sfdllx, @keyword, @dlCode, @isContansChildDl, @useypckflag, @ypyfbmCode, @containyp0ck, @onlyybflag,@isQyKssKZ,@qxjb", paraList.ToArray());

            return list;
        }

        /// <summary>
        /// 根据人员工号检索权限内病区
        /// </summary>
        /// <param name="gh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysWardVO> GetWardListByStaffGh(string gh, string orgId)
        {
            var sql = @"
select bqCode, bqmc from [NewtouchHIS_Base]..V_C_Sys_StaffWard
where StaffGh = @gh and zt = '1' and OrganizeId = @orgId";
            return this.FindList<SysWardVO>(sql, new[] { new SqlParameter("gh", gh ?? ""), new SqlParameter("orgId", orgId ?? "") });
        }

        #region 药品相关

        /// <summary>
        /// 获取药品用法列表
        /// </summary>
        /// <returns></returns>
        public IList<SysMedicineUsageVEntity> GetMediUsageList()
        {
            var sql = @"select * from [NewtouchHIS_Base]..V_S_xt_ypyf where zt = '1'";
            return this.FindList<SysMedicineUsageVEntity>(sql);
        }

        /// <summary>
        /// 获取药品剂型用法对照关系
        /// </summary>
        /// <returns></returns>
        public IList<SysMedicineFormulationUsageVEntity> GetMediFormlUsageList()
        {
            var sql = @"select * from [NewtouchHIS_Base]..V_S_xt_yp_jxyfdy where zt = '1'";
            return this.FindList<SysMedicineFormulationUsageVEntity>(sql);
        }

        #endregion

        /// <summary>
        /// 医嘱频次 检索
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<SysMedicalOrderFrequencyVEntity> GetOrderFrequencyList(string orgId)
        {
            return _cache.Get(string.Format(CacheKeyBase.ValidSysMedicalOrderFrequencyListSetKey, orgId), () =>
            {
                var sql = @"
SELECT yzpcId,OrganizeId,yzpcCode,yzpcmc,zxcs,zxzq,zxzqdw,zbz,zxsj,zt,yzpcmcsm
FROM [NewtouchHIS_Base]..[V_S_xt_yzpc]
WHERE zt='1'
        AND organizeId=@orgId
                        ";
                return this.FindList<SysMedicalOrderFrequencyVEntity>(sql, new[] { new SqlParameter("@orgId", orgId) });
            });
        }

        /// <summary>
        /// 获取收费项目的执行科室列表xt_sfxm_zxks（未绑定时返回所有科室）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="sfxmCode"></param>
        /// <returns></returns>
        public IList<SysDepartmentVEntity> GetSfxmZxksList(string orgId, string sfxmCode)
        {
            var sql = @"select ks.*
from [NewtouchHIS_Base]..V_S_xt_sfxm_zxks mapp
inner
join [NewtouchHIS_Base]..V_S_Sys_Department ks
on ks.Code = mapp.ksCode and ks.OrganizeId = mapp.OrganizeId and ks.zt = '1'
where mapp.zt = '1' and mapp.sfxmCode = @sfxmCode and mapp.OrganizeId = @orgId";
            return this.FindList<SysDepartmentVEntity>(sql, new[] {
                new SqlParameter("@orgId", orgId)
                //-111111111让查不到
                ,new SqlParameter("@sfxmCode", sfxmCode ?? "-1111111111")
            });
        }

        /// <summary>
        /// 查询药品详情
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        public SysMedicineComplexVEntity GetYpDetails(string orgId, string ypCode)
        {
            var sql = @"select * from [NewtouchHIS_Base]..V_C_xt_yp where zt = '1' and ypCode = @ypCode and OrganizeId = @orgId";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            pars.Add(new SqlParameter("@ypCode", ypCode));
            return this.FirstOrDefault<SysMedicineComplexVEntity>(sql, pars.ToArray());
        }

        /// <summary>
        /// 获取有效民族List
        /// </summary>
        /// <returns></returns>
        public IList<SysNationVEntity> GetmzList()
        {
            var sql = "select mzCode, mzmc, py from [NewtouchHIS_Base]..V_S_xt_mz width(nolock) where zt = '1'";
            return this.FindList<SysNationVEntity>(sql);
        }

        /// <summary>
        /// 获取所有收费大类
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<SysChargeCategoryVEntity> SelectSfdl(string organizeId)
        {
            const string sql = @"
SELECT dlId, dlCode, ParentId, dlmc, OrganizeId, py, mzprintreportcode, mzprintbillcode
, reportdlcode, dllb, zt, mzzybz 
FROM NewtouchHIS_Base.dbo.V_S_xt_sfdl
WHERE zt='1'
AND OrganizeId=@OrganizeId ";
            var param = new DbParameter[]
            {
                new  SqlParameter("@OrganizeId", organizeId??"")
            };
            return FindList<SysChargeCategoryVEntity>(sql, param);
        }

        /// <summary>
        /// 获取所有收费项目
        /// </summary>
        /// <param name="sfdlCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<SysChargeItemVEntity> SelectSfxm(string sfdlCode, string organizeId)
        {
            const string sql = @"
SELECT [sfxmId],[sfxmCode],[sfxmmc],[sfdlCode],[badlCode]
,[nbdlCode],[OrganizeId],[py],[dw],[dj]
,[zfbl],[zfxz],[mzzybz],[ssbz],[tsbz],[sfbz]
,[ybdm],[wjdm],[CreatorCode],[CreateTime]
,[LastModifyTime],[LastModifierCode],[px]
,[zt],[duration],[bz],[dwjls],[jjcl],[zxks],[gg]
FROM NewtouchHIS_Base.dbo.V_S_xt_sfxm
WHERE zt='1'
AND OrganizeId=@OrganizeId
AND (sfdlCode=@sfdlCode OR ''=@sfdlCode) ";
            var param = new DbParameter[]
            {
                new  SqlParameter("@OrganizeId", organizeId??""),
                new  SqlParameter("@sfdlCode", sfdlCode??"")
            };
            return FindList<SysChargeItemVEntity>(sql, param);
        }

    }
}
