using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity
{
    public class AccessAuth
    {
        public string AccessType { get; set; }

        /// <summary>
        /// AccessRoute
        /// </summary>
        /// <returns></returns>
        public string AccessRoute { get; set; }

        /// <summary>
        /// AppId
        /// </summary>
        /// <returns></returns>
        public string AppId { get; set; }

        /// <summary>
        /// AccessName
        /// </summary>
        /// <returns></returns>
        public string AccessName { get; set; }

        /// <summary>
        /// AccessDesc
        /// </summary>
        /// <returns></returns>
        public string AccessDesc { get; set; }

        /// <summary>
        /// AccessMode
        /// </summary>
        /// <returns></returns>
        public string AccessMode { get; set; }

        /// <summary>
        /// enablelog
        /// </summary>
        /// <returns></returns>
        public int? enablelog { get; set; }

        /// <summary>
        /// tags
        /// </summary>
        /// <returns></returns>
        public string tags { get; set; }
    }
}
