using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.ValueObjects;
using Newtouch.EMR.Domain.ValueObjects.DocumentManage;
using Newtouch.EMR.Domain.ValueObjects.MedicalRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.IDomainServices
{
    public interface IBlmblbDmnService
    {
        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="mbqx"></param>
        /// <param name="ksdm"></param>
        /// <param name="ysgh"></param>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        IList<BlmbListVO> MedRecordTmpList(int mbqx, string bllx, string ksbm, OperatorModel user, string OrgId, string keyword = null);
        /// <summary>
        /// 病历文件转模板
        /// </summary>
        /// <param name="OrgId"></param>
        /// <param name="blId"></param>
        /// <param name="bllxId"></param>
        /// <param name="mbqx"></param>
        /// <returns></returns>
        BlConvertToTemplateVO BlConvertToTemplate(string OrgId, string blId, string bllx, int? mbqx);
        /// <summary>
        /// 病历文件转模板文件操作
        /// </summary>
        /// <param name="ety"></param>
        void BlConvertToTemplateProcess(BlmblbEntity ety);
        /// <summary>
        /// 文件迁移
        /// </summary>
        /// <param name="srcPath"></param>
        /// <param name="aimPath"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        bool CopyDir(string srcPath, string aimPath, string newName,int type);
        /// <summary>
        /// 模板岗位权限控制维护
        /// </summary>
        /// <param name="list"></param>
        /// <param name="user"></param>
        void TempCtrlAssigned(string list,string mbId, OperatorModel user);
        IList<TemplateQxkzVO> Getqxkz(string staffId, string mbId,string bllx);

        /// <summary>
        /// 获取病历模板列表（含病历大类）
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<BlmbListVO> GetBlmbList(Pagination pagination, string OrganizeId, string keyword);
        IList<BlmbListVO> MedRecordTmpListTree( string OrgId,string keyValue,string mbqx);
        /// <summary>
        /// 患者已有模板的病历
        /// </summary>
        /// <param name="OrgId"></param>
        /// <param name="zyh"></param>
        /// <param name="bllx"></param>
        /// <returns></returns>
        IList<blmbmcVO> getRepeatBl(string OrgId, string zyh, string bllx);

        string CheckBlRules(string OrgId, string zyh, string bllx);
    }
}
