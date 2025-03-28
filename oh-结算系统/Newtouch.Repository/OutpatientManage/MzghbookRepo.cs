using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-12-16 17:20
    /// 描 述：预约
    /// </summary>
    public class MzghbookRepo : RepositoryBase<MzghbookEntity>, IMzghbookRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public MzghbookRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public int SubmitForm(MzghbookEntity entity, decimal? keyValue)
        {
            if (keyValue.HasValue && keyValue.Value > 0)
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties
                
                dbEntity.Modify(keyValue);
                return Update(dbEntity);
            }
            entity.Create();
            throw new NotImplementedException(); //主键 非string 非标识，未给主键赋值
            return Insert(entity);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public int DeleteForm(decimal keyValue)
        {
            return Delete(p => p.BookId == keyValue);
        }
        /// <summary>
        /// 修改预约挂号预约状态
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="mzh"></param>
        /// <param name="patid"></param>
        /// <param name="ks"></param>
        /// <param name="zfje"></param>
        /// <param name="AppId"></param>
        /// <param name="orgId"></param>
        public void UpdateMzGhAppointment(string jsnm, string mzh, int patid, string ks, string zfje,string AppId,string orgId,string outdate,string mzlx,string ghf)
        {
            var outdatetime = Convert.ToDateTime(outdate);
            var yyzt = ((int)EnumMzyyzt.book).ToString();
            var enty = this.FindEntity(p => p.patid ==patid && p.zt == "1" && p.ks==ks &&p.OutDate== outdatetime && p.yyzt== yyzt && p.OrganizeId == orgId && p.RegType==mzlx);
            //enty.AppId = AppId;
            enty.mzh = mzh;
            enty.PayFee = Convert.ToDecimal(zfje);
            enty.PayLsh = jsnm;
            enty.RegFee = Convert.ToDecimal(zfje);
            enty.yyzt = ((int)EnumMzyyzt.reg).ToString();
            enty.ghrq = enty.OutDate;
            if (enty != null)
            {
                enty.Modify();
                this.Update(enty);
            }
        }
        public void SigninAppointment(string mzh,string orgId)
        {
            try
            {
            this.ExecuteSqlCommand("EXEC dbo.rpt_PatientAppointment @OrgId,@Mzh", new[] {
                        new SqlParameter("@OrgId", orgId)
                        ,new SqlParameter("@Mzh", mzh)
                    });
            }
            catch (Exception  ex)
            {
            }
        }

		public int FZsjTB(string ghrq, string rygh,string orgid)
		{
			return this.ExecuteSqlCommand("EXEC Newtouch_CIS..mz_分诊页面刷新保存 @ghrq,@orgId,@rygh", new[] {
						new SqlParameter("@ghrq", ghrq)
						,new SqlParameter("@orgId", orgid)
						,new SqlParameter("@rygh", rygh)
					});
		}

	}
}