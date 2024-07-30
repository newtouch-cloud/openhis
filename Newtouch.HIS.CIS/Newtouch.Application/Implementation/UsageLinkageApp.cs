using FrameworkBase.MultiOrg.Application;
using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.Application.Implementation
{
    /// <summary>
    /// 用法联动
    /// </summary>
    public class UsageLinkageApp : AppBase, IUsageLinkageApp
    {
        private readonly ISysUsageLinkageRepo sysUsageLinkageRepo;

        /// <summary>
        /// 提交用法绑定
        /// </summary>
        /// <param name="sysUsageLinkageEntity"></param>
        /// <returns></returns>
        public string SubmitForm(SysUsageLinkageEntity sysUsageLinkageEntity)
        {
            if (sysUsageLinkageEntity == null) return "保存内容不能为空";
            if (string.IsNullOrWhiteSpace(sysUsageLinkageEntity.yfCode)) return "用法不能为空";
            if (string.IsNullOrWhiteSpace(sysUsageLinkageEntity.sfxmCode)) return "收费项目不能为空";
            if (string.IsNullOrWhiteSpace(sysUsageLinkageEntity.Id))
            {
                sysUsageLinkageEntity.Create(true);
                return sysUsageLinkageRepo.Insert(sysUsageLinkageEntity) > 0 ? "" : "新增失败";
            }
            sysUsageLinkageEntity.Modify();
            return sysUsageLinkageRepo.Update(sysUsageLinkageEntity) > 0 ? "" : "修改失败";
        }
    }
}
