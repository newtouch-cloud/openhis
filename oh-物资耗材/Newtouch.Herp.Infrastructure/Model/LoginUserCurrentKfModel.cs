using System.Collections.Generic;

namespace Newtouch.Herp.Infrastructure.Model
{
    /// <summary>
    /// 当前登录帐号信息
    /// </summary>
    public class LoginUserCurrentKfModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// 员工名称
        /// </summary>
        public string staffName { get; set; }

        /// <summary>
        /// 当前库房ID
        /// </summary>
        public string currentKfId { get; set; }

        /// <summary>
        /// 当前库房名称
        /// </summary>
        public string currentKfName { get; set; }

        /// <summary>
        /// 库房等级
        /// </summary>
        public int currentKfLevel { get; set; }

        private List<KfInfoModel> _kfList = new List<KfInfoModel>();
        /// <summary>
        /// 归属库房
        /// </summary>
        public List<KfInfoModel> kfList
        {
            get { return _kfList; }
            set { _kfList = value; }
        }

        /// <summary>
        /// 职位
        /// </summary>
        public string dutyName { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }
    }

    /// <summary>
    /// 库房信息
    /// </summary>
    public class KfInfoModel
    {
        /// <summary>
        /// 当前库房ID
        /// </summary>
        public string kfId { get; set; }

        /// <summary>
        /// 当前库房名称
        /// </summary>
        public string kfName { get; set; }

        /// <summary>
        /// 库房等级
        /// </summary>
        public int kfLeve { get; set; }
    }
}
