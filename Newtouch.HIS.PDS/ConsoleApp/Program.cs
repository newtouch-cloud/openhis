using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtouch.Common.Web;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.Log;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.ResourcesOperate;
using Newtouch.PDS.Requset.Zyypyz;
using Newtouch.Tools;
using Newtouch.Tools.Security;

namespace ConsoleApp
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {

        static SemaphoreSlim semLim = new SemaphoreSlim(3); //3表示最多只能有三个线程同时访问

        static void Main(string[] args)
        {
            Console.WriteLine("01. Start  ---");
            //LoopExec();
            //TaskTest();
            //Multithreadinglock();
            //testRef();
            //TestNewLog();
            TestToken();
            Console.WriteLine("99. End  ---");
        }

        static void TestToken()
        {
            while (true)
            {
                try
                {
                    var request = new OutpatientCommitRequestDTO
                    {
                        AppId = "newtouch.pds.test",
                        CardNo = "No.0001",
                        Jsnm = 123123,
                        Sfsj = DateTime.Now,
                        Cfh = "N00012",
                        Cfnm = 123123,
                        Timestamp = DateTime.Now
                    };
                    var vi = AESEncrypt.AESGetIv(16);
                    var ec = AESEncrypt.AesEncrypt(((DateTime)request.Timestamp).ToString("yyyyMMddHHmmss"), vi, vi);
                    request.Token = ec;
                    var viec = RSAEncrypt.EncryptByPublicKey(vi);
                    request.TokenType = viec;
                    Console.WriteLine("Request Json：\n" + request.ToJson());
                    var response = SitePDSAPIHelper.Request<OutpatientCommitRequestDTO, APIRequestHelper.DefaultResponse>("api/test/TestToken", request);
                    Console.WriteLine("Response Json：\n" + response.ToJson());
                    var inputValue = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(inputValue) && inputValue.ToLower().Trim().Equals("c")) break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        static void TestNewLog()
        {
            while (true)
            {
                Console.WriteLine("02. 输入日志内容：");
                var readLine = Console.ReadLine();
                LogCore.Info("测试日期", readLine);
            }
        }

        static void testRef()
        {
            Console.WriteLine("");

            List<string> list1 = new List<string>();
            Test1(list1);
            Console.WriteLine(" list1：" + list1.Count); // 总数量为 1

            Console.WriteLine("");
            Console.WriteLine("---- 亮瞎眼的分割线 ----");
            Console.WriteLine("");

            List<string> list2 = new List<string>();
            Test2(list2);
            Console.WriteLine(" list2：" + list2.Count); // 总数量仍为 0

            Console.WriteLine("");
        }
        static void Test1(List<string> list)
        {
            list.Add("1");
            Console.WriteLine(" Test1()：" + list.Count); // 总数量为 1
        }

        static void Test2(List<string> list)
        {
            List<string> list2 = new List<string>();
            list2.Add("1");

            list = list2;
            Console.WriteLine(" Test2()：" + list.Count); // 总数量为 1
        }

        static void Multithreadinglock()
        {
            var errorMsg = "";
            var msgList = new List<string>();
            var drityWrite = "";
            var locker = new object();
            var li = new List<string> { "111", "222", "333", "444", "555", "666", "777" };
            Parallel.ForEach(li, p =>
            {
                Console.WriteLine(string.Format("子线程【{0}】 处理事情：{1}", Thread.CurrentThread.ManagedThreadId, p));
                errorMsg = " 【命中 " + p + "】";
                var curErrorMsg = " 【命中 " + p + "】";
                msgList.Add(errorMsg);

                lock (locker)
                {
                    var drityWriteClone = drityWrite;
                    drityWriteClone += curErrorMsg;
                    if ("333".Equals(p))
                    {
                        Thread.Sleep(500);
                    }

                    drityWrite = drityWriteClone;
                }
            });
            Console.WriteLine(errorMsg);
            Console.WriteLine(drityWrite);
            //Console.WriteLine(msgList.ToJson());
            Console.ReadLine();
        }

        static void Multithreading()
        {
            var li = new List<string> { "111", "222", "333", "444", "555", "666", "777" };
            var cts = new CancellationTokenSource();
            var errorMsg = "";
            var msgList = new List<string>();
            //try
            //{
            var parent = new Task(() =>
            {
                var childFactory = new TaskFactory<bool>(cts.Token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
                li.ForEach(p =>
                {
                    var childTask = childFactory.StartNew(() =>
                    {

                        if (cts.IsCancellationRequested)
                        {
                            return true;
                        }
                        Thread.Sleep(1000);
                        Console.WriteLine(string.Format("子线程【{0}】 处理事情：{1}", Thread.CurrentThread.ManagedThreadId, p));
                        errorMsg = "命中 " + p;
                        msgList.Add(errorMsg);
                        //if (!"444".Equals(p))
                        //{
                        //    Thread.Sleep(1000);
                        //}
                        //else
                        //{
                        //    Console.WriteLine(string.Format("子线程【{0}】 处理事情：{1}", Thread.CurrentThread.ManagedThreadId, p));
                        //    errorMsg = "命中444";
                        //    cts.Cancel();
                        //    return false;
                        //}
                        return true;
                    }, cts.Token);
                });
            });
            parent.Start();
            parent.Wait();
            if (cts.IsCancellationRequested)
            {
                Console.WriteLine("子线程取消操作");
            }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message + (e.InnerException == null ? "" : e.InnerException.Message));
            //}
            Console.WriteLine(errorMsg);
            Console.WriteLine(msgList.ToJson());
            Console.ReadLine();
        }

        static void TaskTest()
        {
            Console.WriteLine(string.Format("主线程 开始，线程号：{0}", Thread.CurrentThread.ManagedThreadId));
            ////IsBackground=true，将其设置为后台线程
            ////Thread t = new Thread(Run) { IsBackground = true };
            ////t.Start();
            //ThreadPool.QueueUserWorkItem(_ => Run());
            //Console.WriteLine(string.Format("主线程 在做其他的事，线程号：{0}", Thread.CurrentThread.ManagedThreadId));
            ////主线程结束，后台线程会自动结束，不管有没有执行完成
            //Thread.Sleep(300);
            ////Thread.Sleep(1500);
            //Console.WriteLine(string.Format("主线程 结束，线程号：{0}", Thread.CurrentThread.ManagedThreadId));

            //Task<string> task = Task<string>.Run(() =>
            //{
            //    Thread.Sleep(3000);
            //    Console.WriteLine(string.Format("子线程 后台线程调用,线程号：{0}", Thread.CurrentThread.ManagedThreadId));
            //    return Thread.CurrentThread.ManagedThreadId.ToString();
            //});
            ////Thread.Sleep(300);
            //Console.WriteLine(string.Format("主线程 在做其他的事，线程号：{0}", Thread.CurrentThread.ManagedThreadId));
            //Console.WriteLine(string.Format("子线程 返回值：{0}", task.Result));
            ////task.Wait();
            //Console.WriteLine(string.Format("主线程 结束，线程号：{0}", Thread.CurrentThread.ManagedThreadId));


            //Console.WriteLine(string.Format("-------主线程 启动-------  线程号：{0}", Thread.CurrentThread.ManagedThreadId));
            //Task<int> task = GetStrLengthAsync();
            //Thread.Sleep(300);
            //Console.WriteLine("主线程 继续执行");
            //Console.WriteLine(string.Format("主线程 Task返回的值{0},  线程号：{1}", task.Result, Thread.CurrentThread.ManagedThreadId));
            //Console.WriteLine(string.Format("-------主线程 结束------- 线程号：{0}", Thread.CurrentThread.ManagedThreadId));
            //var result = new List<string>();
            //var sw = new Stopwatch();
            //sw.Start();
            //var list = new List<int> { 1, 2, 3, 4, 5, 6, 6, 7, 8, 9 };
            //Parallel.ForEach<int>(list, n =>
            //{
            //    var tmp = string.Format("value:{0}   线程：{1}", n, Thread.CurrentThread.ManagedThreadId);
            //    result.Add(tmp);
            //    Console.WriteLine(tmp);
            //    Thread.Sleep(1000);
            //});
            //sw.Stop();
            //Console.WriteLine(result.ToJson());
            //Console.WriteLine(sw.Elapsed);
            //Console.Read();

            //Console.WriteLine("主线程开始");
            //Task<string> task = Task<string>.Run(() =>
            //{
            //    Thread.Sleep(2000);
            //    return Thread.CurrentThread.ManagedThreadId.ToString();
            //});

            //task.GetAwaiter().OnCompleted(() =>
            //{
            //    Console.WriteLine(task.Result);
            //    Thread.Sleep(2000);
            //});
            //task.ContinueWith(m =>
            //{
            //    Console.WriteLine("第一个任务结束啦！我是第二个任务");
            //    Thread.Sleep(2000);
            //});
            //Console.WriteLine("主线程结束");
            //task.Wait();
            //Thread.Sleep(100);

            //Task.Run(() =>
            //{
            //    Thread.CurrentThread.IsBackground = false;
            //    Console.WriteLine("子线程 开始  线程号：{0}", Thread.CurrentThread.ManagedThreadId);
            //    Thread.Sleep(5000);
            //    Console.WriteLine("子线程 结束  线程号：{0}", Thread.CurrentThread.ManagedThreadId);
            //    Thread.Sleep(2000);
            //});
            //Thread.Sleep(1000);

            GetString();
            Thread.Sleep(1500);
            Console.WriteLine(string.Format("主线程 结束，线程号：{0}", Thread.CurrentThread.ManagedThreadId));
        }

        static async Task<int> GetStrLengthAsync()
        {
            Thread.Sleep(2000);
            Console.WriteLine(string.Format("GetStrLengthAsync方法开始执行 线程号：{0}", Thread.CurrentThread.ManagedThreadId));
            //此处返回的<string>中的字符串类型，而不是Task<string>
            //string str = await GetString();
            //string str = GetString().Result;
            Task<string> str = GetString();
            Thread.Sleep(100);
            Console.WriteLine(string.Format("GetStrLengthAsync方法执行结束 线程号：{0}", Thread.CurrentThread.ManagedThreadId));
            //return str.Length;
            return 10;
        }

        static Task<string> GetString()
        {
            Console.WriteLine(string.Format("GetString方法开始执行 线程号：{0}", Thread.CurrentThread.ManagedThreadId));
            return Task<string>.Run(() =>
            {
                Console.WriteLine(string.Format("GetString Task.Run 线程号：{0}", Thread.CurrentThread.ManagedThreadId));
                Thread.Sleep(3000);
                Console.WriteLine(string.Format("GetString Task.Run after sleep 3s 线程号：{0}", Thread.CurrentThread.ManagedThreadId));
                return "GetString的返回值";
            });
        }

        //static void SemaphoreTest()
        //{
        //    semLim.Wait();
        //    Console.WriteLine("线程" + Thread.CurrentThread.ManagedThreadId.ToString() + "开始执行");
        //    Thread.Sleep(2000);
        //    Console.WriteLine("线程" + Thread.CurrentThread.ManagedThreadId.ToString() + "执行完毕");
        //    semLim.Release();
        //}

        static void Run()
        {
            //Thread.Sleep(700);
            Console.WriteLine(string.Format("子线程 这是后台线程调用,线程号：{0}", Thread.CurrentThread.ManagedThreadId));
        }

        /// <summary>
        /// 循环执行
        /// </summary>
        public static void LoopExec()
        {
            TitleShow();
            while (true)
            {
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("请规范您的输入");
                    continue;
                }
                if (input.ToLower().Equals("close"))
                {
                    break;
                }
                switch (input.Trim().ToLower())
                {
                    case "1":
                    case "book":
                    case "1.book":
                    case "1、book":
                        Book(input);
                        break;
                    case "2":
                    case "2.yzzx":
                    case "2、yzzx":
                    case "yzzx":
                        Yzzx();
                        break;
                    case "3":
                        stockQueryTest();
                        break;
                    default:
                        Console.WriteLine("请规范您的输入");
                        TitleShow();
                        break;
                }
                TitleShow();
            }
        }

        public static void stockQueryTest()
        {
            var request = new StockQueryRequestDTO
            {
                Timestamp = DateTime.Now,
                OrganizeId = "cdbfce48-5494-42aa-a947-07815e546ce5",
                yplist = new List<DrugInfo> {
                    new DrugInfo{ lyyf="zyyf",ypCode="00000002"}
                }
            };

            var response = SiteSettAPIHelper.Request<object,APIRequestHelper.DefaultResponse>("api/Stock/query", request);
            Console.WriteLine(response.ToJson());
        }

        /// <summary>
        /// book
        /// </summary>
        /// <param name="input"></param>
        private static void Book(string input)
        {
            int sl;
            int.TryParse(input, out sl);
            var reqObj = new OutpatientBookRequestDTO
            {
                Items = new List<ItemDetail>
                {
                    new ItemDetail
                    {
                        Cfh="12312312312312",
                        ItemCode="00000002",
                        Yfbm="mzyf",
                        ItemCount=sl
                    }
                },
                OrganizeId = "cdbfce48-5494-42aa-a947-07815e546ce5",
                CreatorCode = "fymzyf",
                Timestamp = DateTime.Now
            };

            var apiResp = SiteSettAPIHelper.Request<OutpatientBookRequestDTO, APIRequestHelper.DefaultResponse>("api/ResourcesOperate/Book", reqObj);
            Console.WriteLine(apiResp.ToJson());
        }

        /// <summary>
        /// 医嘱执行
        /// </summary>
        private static void Yzzx()
        {
            var yzList = new List<YzDetail>();
            var request = new ZyypyzzxRequest
            {
                AppId = "12312",
                ClientNo = Guid.NewGuid().ToString(),
                OrganizeId = "cdbfce48-5494-42aa-a947-07815e546ce5",
                yzList = yzList,
                Timestamp = DateTime.Now
            };
            yzList.Add(new YzDetail
            {
                bqCode = "康复病区",
                cw = "2115",
                dj = 20,
                dl = "西药",
                fyyf = "zyyf",
                je = 100,
                sl = 5,
                ksrq = DateTime.Now,
                jsrq = DateTime.Now.AddHours(5),
                ksCode = "住院部",
                lyxh = 103,
                patientName = "张三",
                pcmc = "3",
                sjap = "08 12 18 22",
                yl = 2,
                yldw = "片",
                ypCode = "00000003",
                ysgh = "10001",
                //zxId = "10001212",
                yzbz = "多喝水",
                yzxz = "2",
                yzzxsqr = "yfzyyf",
                zfbl = 0,
                zlff = "口服",
                zxsl = 4,
                zyh = "123123"
            });
            yzList.Add(new YzDetail
            {
                bqCode = "康复病区",
                cw = "2115",
                dj = 60,
                dl = "西药",
                fyyf = "zyyf",
                je = 300,
                sl = 5,
                ksrq = DateTime.Now,
                jsrq = DateTime.Now.AddHours(5),
                ksCode = "住院部",
                lyxh = 103,
                patientName = "张三",
                pcmc = "3",
                sjap = "08 12 18 22",
                yl = 2,
                yldw = "片",
                ypCode = "00000002",
                ysgh = "10001",
                //zxId = "10001214",
                yzbz = "多喝水",
                yzxz = "2",
                yzzxsqr = "yfzyyf",
                zfbl = 0,
                zlff = "口服",
                zxsl = 4,
                zyh = "123123"
            });
            var apiResp = SiteSettAPIHelper.Request<ZyypyzzxRequest, APIRequestHelper.DefaultResponse>("api/Zyypyz/yzzx", request);
            Console.WriteLine(apiResp.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        private static void TitleShow()
        {
            Console.WriteLine("请输入您的指令");
            Console.WriteLine("1、Book");
            Console.WriteLine("2、Yzzx");
            Console.WriteLine("3、stockQueryTest");
        }
    }
}
