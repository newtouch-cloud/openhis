using System;
using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 菜单App
    /// </summary>
    public class ModuleApp : IModuleApp
    {
        private readonly ISysModuleRepo _moduleRepository;

        public ModuleApp(ISysModuleRepo moduleRepository)
        {
            this._moduleRepository = moduleRepository;
        }

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
