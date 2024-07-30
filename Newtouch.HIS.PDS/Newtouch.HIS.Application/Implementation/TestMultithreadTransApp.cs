using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application.Implementation
{
    public class TestMultithreadTransApp : AppBase, ITestMultithreadTransApp
    {
        public string test1()
        {
            var errorMsg = "";
            try
            {
                using (var db = new EFDbTransaction(new DefaultDatabaseFactory()).BeginTrans())
                {
                    var li = new List<string> { "11111111", "22222222", "3333333", "4444444" };
                    var cts = new CancellationTokenSource();
                    var parent = new Task(() =>
                    {
                        var childTask = new TaskFactory<bool>(cts.Token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
                        foreach (var item in li)
                        {
                            childTask.StartNew(() =>
                            {
                                if (cts.IsCancellationRequested)
                                {
                                    return false;
                                }

                                #region  exec part1

                                //                                const string sql1 = @"
                                //INSERT INTO dbo.EntitySerialNo
                                //        ( EntityName ,
                                //          SerialNoMin ,
                                //          SerialNoMax ,
                                //          CurrentSerialNo
                                //        )
                                //VALUES  ( @EntityName , -- EntityName - varchar(50)
                                //          1212 , -- SerialNoMin - int
                                //          2111234 , -- SerialNoMax - int
                                //          23455235  -- CurrentSerialNo - int
                                //        )
                                //";
                                //                                var para1 = new DbParameter[]
                                //                                {
                                //                               new SqlParameter("@EntityName", item)
                                //                                };
                                //                                var execResult1 = db.ExecuteSqlCommandNoLog(sql1, para1);
                                //                                if (execResult1 >= 0)
                                //                                {
                                //                                    return true;
                                //                                }

                                #endregion

                                #region exec part2

                                const string sql2 = "SELECT TOP 1 * FROM dbo.EntitySerialNo(NOLOCK)";
                                var execResult2 = db.ExecuteSqlCommandNoLog(sql2);
                                return true;

                                #endregion
                                errorMsg = string.Format("执行item {0} 失败", item);
                                cts.Cancel();
                                return false;
                            }, cts.Token);
                        }
                    });
                    parent.Start();
                    parent.Wait();
                    if (cts.IsCancellationRequested || !string.IsNullOrWhiteSpace(errorMsg))
                    {
                        return errorMsg;
                    }

                    db.Commit();
                    errorMsg = "success";
                }
            }
            catch (Exception e)
            {
                errorMsg = e.Message + (e.InnerException == null ? "" : e.InnerException.Message);
            }

            return errorMsg;
        }

        public string testEFDbTransaction()
        {
            try
            {
                using (var db = new EFDbTransaction(new DefaultDatabaseFactory()).BeginTrans())
                {
                    const string sql = "insert into aa VALUES (12312, 23, 123);";
                    var cf = db.FindList<MzCfEntity>(sql);
                    db.Commit();
                    return "成功";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}