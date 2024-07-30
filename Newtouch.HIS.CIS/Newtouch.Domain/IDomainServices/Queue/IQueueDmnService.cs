using Newtouch.Core.Common;
using Newtouch.Domain.ValueObjects.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IDomainServices.Queue
{
    public interface IQueueDmnService 
    {
        IList<QueueInfo> GetQueue(string orgId, DateTime qdkssj, DateTime qdjssj, string ks, string ys, string ywbz, string ywlx, int? calledstu, string keyword);
        IList<QueueInfo> GetQueue(Pagination pagination, string orgId, DateTime qdkssj, DateTime qdjssj, string ks, string ys, string ywbz, string ywlx, int? calledstu, string keyword);
        QueueInfo GetQueueByMzh(string mzh, string orgId);
    }
}
