using Newtouch.HIS.API.Common;
using Newtouch.PDS.Requset;

namespace Newtouch.PDS.API.Common
{
    /// <summary>
    /// 简单的业务处理
    /// </summary>
    public class BusinessManage
    {
        #region instance

        private static readonly BusinessManage _businessManage = new BusinessManage();

        private BusinessManage()
        {
        }

        /// <summary>
        /// 对外访问
        /// </summary>
        /// <returns></returns>
        public static BusinessManage GetInstance()
        {
            return _businessManage;
        }

        #endregion

        /// <summary>
        /// 组装返回报文
        /// </summary>
        /// <param name="result"></param>
        /// <param name="response"></param>
        public void BuildResponse(ActResult result, DefaultResponse response)
        {
            response = new DefaultResponse();
            if (result == null)
            {
                response.data = null;
                response.code = ResponseResultCode.ERROR;
                return;
            }
            response.data = result.Data;
            response.code = result.IsSucceed ? ResponseResultCode.SUCCESS : ResponseResultCode.FAIL;
        }
    }
}