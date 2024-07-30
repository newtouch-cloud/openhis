using Quartz;
using System;

namespace YiBaoScheduling.QuartzJobs
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class TestJob : JobBase<TestJob>
    {
        public override void Body()
        {
            Console.WriteLine("TestJob13 executing...");
        }

    }

}
