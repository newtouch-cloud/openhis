using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicineDosageRepo : RepositoryBase<SysMedicineDosageEntity>, ISysMedicineDosageRepo
    {
        public SysMedicineDosageRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 提交药品剂型对应关系
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="yfbmCode"></param>
        public void submitUsage(string jxCode, string yfCode)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                //先 del
                db.Delete<SysMedicineDosageEntity>(p => p.jxCode == jxCode);
                //再添加
               // var yfCodeArr = (yfCode ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

				var userYfbmentity = new SysMedicineDosageEntity();
				userYfbmentity.Id = Guid.NewGuid().ToString();
				userYfbmentity.jxCode = jxCode;
				userYfbmentity.yfCode = yfCode;
				userYfbmentity.Create();
				db.Insert(userYfbmentity);
				//foreach (var YfCode in yfCodeArr)
				//{
				//	//string[] str = YfCode.Split(new string[] { "a88123jkjfwe13120j" }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
				//	//if (str.Length != 2)
				//	//{
				//	//    continue;
				//	////}
				//	//string organizeId = str[0];
				//	//string yfbmcode = str[1];
				//	var userYfbmentity = new SysMedicineDosageEntity();
				//	userYfbmentity.Id = Guid.NewGuid().ToString();
				//	userYfbmentity.jxCode = jxCode;
				//	userYfbmentity.yfCode = YfCode;
				//	userYfbmentity.Create();
				//	db.Insert(userYfbmentity);
				//}
				db.Commit();
            }
        }

    }
}
