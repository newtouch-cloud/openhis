using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Quartz;
using YiBaoScheduling.Common;

namespace YiBaoScheduling.QuartzJobs
{
    /// <summary>
    /// job base
    /// </summary>
    public abstract class JobBase<T> : IJob
    {
#if !DEBUG
         private static bool _firstTime = true; //首次运行不执行
#endif

        /// <summary>
        /// tags
        /// </summary>
        protected readonly Dictionary<string, string> Tags;

        public bool RuningInFisrtTime { get; set; }   //首次执行，从配置文件初始化

        public static bool IsRunning;


        /// <summary>
        /// 初始化
        /// </summary>
        protected JobBase()
        {
            Tags = new Dictionary<string, string>
            {
                {Constants.Tags_ClientNo, CommonUtility.GetNewGuid()}
            };
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {

#if !DEBUG
            if (!RuningInFisrtTime && _firstTime) { _firstTime = false; return; } //首次运行不执行
#endif
            if (IsRunning) { return; }  //运行状态，不继续开

            IsRunning = true;

            try
            {
                AppLogger.Info("Execute.Start：" + typeof(T).Name);
                Body();

            }
            catch (Exception ex)
            {
                AppLogger.Info("Execute.catched error：" + typeof(T).Name);
                AppLogger.Info(ex.Message);
            }
            finally
            {
                AppLogger.Info("Execute.End：" + typeof(T).Name);
                IsRunning = false;
            }
        }

        /// <summary>
        /// Job 执行内容
        /// </summary>
        public abstract void Body();

        /// <summary>
        /// 
        /// </summary>
        public virtual string GetJobKey()
        {
            return typeof(T).Name;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileAccess"></param>
        /// <param name="fileShare"></param>
        public string ReadFileContent(string filePath, FileAccess fileAccess, FileShare fileShare)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, fileAccess, fileShare))
            {
                var buffer = new byte[fs.Length];
                fs.Position = 0;
                fs.Read(buffer, 0, buffer.Length);
                return Encoding.Default.GetString(buffer);  //用UTF-8乱码
            }
        }

    }
}
