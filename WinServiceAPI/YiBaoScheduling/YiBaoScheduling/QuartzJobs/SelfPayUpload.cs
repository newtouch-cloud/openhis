using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiBaoScheduling.Common;
using YiBaoScheduling.Model;
using YiBaoScheduling.Proxy;

namespace YiBaoScheduling.QuartzJobs
{
    public sealed class SelfPayUpload:JobBase<SelfPayUpload>
    {
        public override void Body()
        {
            string errmsg = "";
            #region 医保费用自动上传   暂不使用
            Console.WriteLine("1.1.1.查询轮询数据Start...");
            List<string> sqlList = new List<string>();
            List<string> preSqlList = new List<string>();
            var queryData = DBHelper.Query();
            //queryData.ForEach(p=> {
            //    Task.Run(()=>
            //    {
            //        var result = RequestHelper.GetInstance().InpatientCost(p);
            //    }).Wait();
            //});
            AppLogger.Info("住院费用【2301】上传star");
            foreach (var item in queryData)
            {
                var result = RequestHelper.GetInstance().InpatientCost(item);
                Log_CqYbScheduling rs = new Log_CqYbScheduling();
                rs = Function.Mapping<Log_CqYbScheduling, ResponseDTO>(result);
                rs.zyh = item.hisId;
                rs.createtime = DateTime.Now;
                rs.infno = "2301";
                sqlList.Add(rs.ToAddSql());
            }

            System.Threading.Thread.Sleep(new TimeSpan(0, 0, 0, 10));

            var preSettData = DBHelper.PreSettQuery();
            AppLogger.Info("住院费用【2303】预结算");
            foreach (var item in preSettData)
            {
                if (item.medfee_sumamt > 0)
                {
                    var result = RequestHelper.GetInstance().InpatientPreSett(item);
                    Log_CqYbScheduling rs = new Log_CqYbScheduling();
                    rs = Function.Mapping<Log_CqYbScheduling, ResponseDTO>(result);
                    rs.zyh = item.hisId;
                    rs.createtime = DateTime.Now;
                    rs.infno = "2303";
                    sqlList.Add(rs.ToAddSql());
                    if (result.setlinfo != null)
                    {
                        Drjk_prejs_output yjs = new Drjk_prejs_output();
                        yjs = Function.Mapping<Drjk_prejs_output, setlinfo2304>(result.setlinfo);
                        yjs.prejs_id = Guid.NewGuid().ToString();
                        yjs.czrq = DateTime.Now;
                        yjs.czydm = "admin";
                        yjs.zt = 1;
                        yjs.zyh = item.hisId;
                        yjs.zt_czy = "admin";
                        yjs.zt_rq = DateTime.Now;
                        preSqlList.Add(yjs.ToAddSql());
                    }

                }

            }
            DBHelper.ExecuteSqlTran(sqlList, "N",out errmsg);
            DBHelper.ExecuteSqlTran(preSqlList, "Y",out errmsg);

            Console.WriteLine("查询轮询数据End");
            #endregion
        }
    }
}
