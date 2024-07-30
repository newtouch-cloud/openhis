using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.PatientManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Proxy.guian.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Application.Interface
{
    public interface IInpatientApp
    {

        #region ZFToYB steps

        object ZFToYB_Step_1(string zyh);

        object ZFToYB_Step_2(string zyh);

        object ZFToYB_Step_4(string zyh, string sbbh, string xm);

        object ZFToYB_Step_6(string zyh, int patid, GACardReadInfoDTO cardInfo, GuianRybl21OutInfoEntity ryblInfo);

	    object CQZFToYB_Step_6(string zyh, int patid, ZYToYBDto patInfo, CqybMedicalReg02Entity ryblInfo);

		object ZFToYB_Step_8(string zyh);

        #endregion

        #region YBToZF steps

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        object YBToZF_Step_1(string zyh);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="sbbh"></param>
        /// <param name="xm"></param>
        /// <returns></returns>
        object YBToZF_Step_3(string zyh, string sbbh, string xm);

	    object CqYBToZF_Step_3(string zyh);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="zyh"></param>
		/// <returns></returns>
		object YBToZF_Step_4(string zyh);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        object YBToZF_Step_5(string zyh, int patid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        object YBToZF_Step_7(string zyh);

        #endregion

        #region XNHToZF steps
        object XNHToZF_Step_1(string zyh);
        #endregion

        #region ZFToXNH

        bool ZFToXNH_Step_4(string zyh, string grbm, string xm, out string msg);
        
        object ZFToXNH_Step_8(string zyh1);

        S04RequestDTO GetZfToXnhPatInfo(string zyh);

        #endregion

    }
}
