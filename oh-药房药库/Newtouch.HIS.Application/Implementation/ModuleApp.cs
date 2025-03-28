using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 菜单App
    /// </summary>
    public class ModuleApp : AppBase, IModuleApp
    {
        private readonly ISysModuleRepo _moduleRepository;

        /// <summary>
        /// 获取有效菜单列表
        /// </summary>
        /// <returns></returns>
        public List<SysModuleEntity> GetValidList()
        {
            return _moduleRepository.GetValidList().OrderBy(t => t.px).ToList();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysModuleEntity GetForm(string keyValue)
        {
            return _moduleRepository.FindEntity(keyValue);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            if (_moduleRepository.IQueryable().Count(t => t.ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                _moduleRepository.Delete(t => t.Id == keyValue);
            }
        }

    }
}
