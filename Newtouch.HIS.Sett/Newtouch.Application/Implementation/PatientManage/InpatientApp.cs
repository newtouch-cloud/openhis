using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.PatientManage;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.IRepository.PatientManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Proxy.guian.DTO;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Application.Implementation
{
    public class InpatientApp : AppBase, IInpatientApp
    {
        private readonly ISysPatientBasicInfoRepo _sysPatientBasicInfoRepo;
        private readonly IHospPatientBasicInfoRepo _hospPatientBasicInfoRepo;
        private readonly IHospFeeDmnService _hospFeeDmnService;
        private readonly IHospMultiDiagnosisRepo _hospMultiDiagnosisRepo;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly IPatientBasicInfoDmnService _patientBasicInfoDmnService;
        private readonly IGuianRybl21OutInfoRepo _guianRybl21OutInfoRepo;
	    private readonly ICqybMedicalReg02Repo _cqybMedicalReg02Repo;
        private readonly IOutPatientSettleDmnService _outPatientSettleDmnService;
        private readonly ISysCardRepo _SysCardRepository;
        #region ZFToYB steps

        public object ZFToYB_Step_1(string zyh)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedException("缺少参数.住院号");
            }
            var orgId = this.OrganizeId;
            var zybrxx = _hospPatientBasicInfoRepo.IQueryable().Where(p => p.zyh == zyh && p.OrganizeId == orgId).FirstOrDefault();
            if (zybrxx == null)
            {
                throw new FailedException("住院信息未找到");
            }
            if (zybrxx.zybz == ((int)EnumZYBZ.Ycy).ToString())
            {
                throw new FailedException("患者已出院");
            }
            if (zybrxx.zybz == ((int)EnumZYBZ.Wry).ToString())
            {
                throw new FailedException("已作废记录");
            }
            if (!(zybrxx.brxz == "0"))
            {
                throw new FailedException("当前非自费");
            }

            return new object();
        }

        public object ZFToYB_Step_2(string zyh)
        {
            var orgId = this.OrganizeId;
            //var hasNonYbFee = _hospFeeDmnService.CheckHasNonYbFee(zyh, orgId);
            //if (hasNonYbFee)
            //{
            //    throw new FailedException("已产生非医保相关费用");
            //}

            return new object();
        }

        public object ZFToYB_Step_4(string zyh, string sbbh, string xm)
        {
            var orgId = this.OrganizeId;
            var zybrxx = _hospPatientBasicInfoRepo.IQueryable().Where(p => p.zyh == zyh && p.OrganizeId == orgId).FirstOrDefault();

            int patid = zybrxx.patid;
            SysPatientBasicInfoEntity xtbrxx = null;
            //if (sbbh.Count()>15)
            //{
            //    var sfsobj = _sysPatientBasicInfoRepo.IQueryable().Where(p => p.zjh == sbbh && p.OrganizeId == orgId && p.brxz == "1" && p.zt == "1").FirstOrDefault();
            //    sbbh = sfsobj.sbbh;
            //}

            //var xtbrxxList = _sysPatientBasicInfoRepo.IQueryable().Where(p => p.sbbh == sbbh && p.OrganizeId == orgId && p.brxz == "1" && p.zt == "1").ToList();
            //if (xtbrxxList.Count == 1)
            //{
            //    //if (xtbrxxList[0].patid != zybrxx.patid)
            //    //{
            //    //    throw new FailedException("该社保卡在系统里已绑定其他身份,不能再用来转该患者医保信息，请联系his！社保编号："+ sbbh.ToString());
            //    //}
            //    xtbrxx = xtbrxxList[0];
            //}
            //else if (xtbrxxList.Count == 0)
            //{
            //    xtbrxx = _sysPatientBasicInfoRepo.IQueryable().Where(p => p.patid == patid && p.OrganizeId == orgId && p.zt == "1").First();
            //    if (!string.IsNullOrWhiteSpace(xtbrxx.sbbh) && xtbrxx.sbbh != sbbh)
            //    {
            //        throw new FailedException("该住院患者已绑定其他社保卡");
            //    }
            //}
            //else
            //{
            //    throw new FailedException("数据异常，该社保卡在基础信息中存在多条，无法定位");
            //}

            if (zybrxx.xm != xm)
            {
                throw new FailedException("异常，姓名不一致，请先修改患者姓名");
            }

            var ryzd1 = _hospMultiDiagnosisRepo.IQueryable().Where(p => p.zyh == zyh && p.zt == "1" && p.OrganizeId==orgId).OrderBy(p => p.zdpx).FirstOrDefault();

            var ksmc = _sysDepartmentRepo.GetNameByCode(zybrxx.ks, orgId);
            //国家编码
            var ysInfo = _outPatientSettleDmnService.GetDepartmentDoctorIdC(orgId, zybrxx.ks, zybrxx.ys);

            return new
            {
                patid = zybrxx.patid,//patid,
                zybrxx = zybrxx,
                xtbrxx = xtbrxx,
                ryzd1 = ryzd1,
                ksmc = ksmc,
                rygjysbm = ysInfo
            };
        }

        public object ZFToYB_Step_6(string zyh, int patid, GACardReadInfoDTO cardInfo, GuianRybl21OutInfoEntity ryblInfo)
        {
            var orgId = this.OrganizeId;

            _patientBasicInfoDmnService.InpatientZFchangetoYB(orgId, zyh, patid, cardInfo, ryblInfo);

            return new object();
        }
	    public object CQZFToYB_Step_6(string zyh, int patid, ZYToYBDto patInfo, CqybMedicalReg02Entity ryblInfo)
	    {
		    var orgId = this.OrganizeId;

		    _patientBasicInfoDmnService.InPatZFchangetoYB(orgId, zyh, patid, patInfo, ryblInfo);

		    return new object();
	    }

		public object ZFToYB_Step_8(string zyh1)
        {
            UpdatebrxzRequest par = new UpdatebrxzRequest { zyh = zyh1,brxzCode="1",brxzmc="医保病人"};
            //同步数据至CPOE
            SiteCISAPIHelper.UpdatebrxzInfo(par);
            return new object();
        }

        #endregion


        #region YBToZF steps

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public object YBToZF_Step_1(string zyh)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedException("缺少参数.住院号");
            }
            var orgId = this.OrganizeId;
            var zybrxx = _hospPatientBasicInfoRepo.IQueryable().Where(p => p.zyh == zyh && p.OrganizeId == orgId).FirstOrDefault();
            if (zybrxx == null)
            {
                throw new FailedException("住院信息未找到");
            }
            if (zybrxx.zybz == ((int)EnumZYBZ.Ycy).ToString())
            {
                throw new FailedException("患者已出院");
            }
            if (zybrxx.zybz == ((int)EnumZYBZ.Wry).ToString())
            {
                throw new FailedException("已作废记录");
            }
            //if (!(zybrxx.brxz == "1"))
            //{
            //    throw new FailedException("当前非医保");
            //}

            return new object();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="sbbh"></param>
        /// <param name="xm"></param>
        /// <returns></returns>
        public object YBToZF_Step_3(string zyh, string sbbh, string xm)
        {
            var orgId = this.OrganizeId;
            var zybrxx = _hospPatientBasicInfoRepo.IQueryable().Where(p => p.zyh == zyh && p.OrganizeId == orgId).FirstOrDefault();

            int patid = zybrxx.patid;
            var xtbrxx = _sysPatientBasicInfoRepo.IQueryable().Where(p => p.patid == patid && p.OrganizeId == orgId && p.zt == "1").First();

            if (zybrxx.xm != xm)
            {
                throw new FailedException("异常，姓名不一致");
            }
            //if (xtbrxx.sbbh != sbbh)
            //{
            //    throw new FailedException("异常，社保个人编号不一致");
            //}

            var garybl21 = _guianRybl21OutInfoRepo.GetInfoByZyh(zyh, orgId);

            return new
            {
                patid = patid,
                zybrxx = zybrxx,
                xtbrxx = xtbrxx,
                garybl21 = garybl21,
            };
        }

	    public object CqYBToZF_Step_3(string zyh)
	    {
		    var orgId = this.OrganizeId;
			var zybrxx = _hospPatientBasicInfoRepo.IQueryable().Where(p => p.zyh == zyh && p.OrganizeId == orgId).FirstOrDefault();
		    var medicalReg = _cqybMedicalReg02Repo.IQueryable().Where(p => p.zymzh == zyh && p.OrganizeId == orgId && p.zt=="1").FirstOrDefault();
            var xtbrxx = _sysPatientBasicInfoRepo.IQueryable().Where(p=>p.patid== zybrxx.patid&&p.OrganizeId==orgId&&p.zt=="1").FirstOrDefault();
            var card = _SysCardRepository.IQueryable().Where(p => p.patid == zybrxx.patid && p.OrganizeId == orgId).FirstOrDefault();
            return new
			{
				zybrxx = zybrxx,
				MedicalReg = medicalReg,
                xtbrxx= xtbrxx,
                cardxx= card,
            };
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="zyh"></param>
		/// <returns></returns>
		public object YBToZF_Step_3(string zyh)
        {
            return new object();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public object YBToZF_Step_4(string zyh)
        {
            return new object();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public object YBToZF_Step_5(string zyh, int patid)
        {
            var orgId = this.OrganizeId;

            _patientBasicInfoDmnService.InpatientYBchangetoZF(orgId, zyh, patid);

            return new object();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public object YBToZF_Step_7(string zyh1)
        {
            //同步数据至CPOE
            UpdatebrxzRequest par = new UpdatebrxzRequest { zyh = zyh1, brxzCode = "0", brxzmc = "自费" };
            //同步数据至CPOE
            SiteCISAPIHelper.UpdatebrxzInfo(par);
            return new object();
        }

        #endregion

        #region XNHToZF steps

        public object XNHToZF_Step_1(string zyh)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                throw new FailedException("缺少参数.住院号");
            }
            var orgId = this.OrganizeId;
            var zybrxx = _hospPatientBasicInfoRepo.IQueryable().Where(p => p.zyh == zyh && p.OrganizeId == orgId).FirstOrDefault();
            if (zybrxx == null)
            {
                throw new FailedException("住院信息未找到");
            }
            if (zybrxx.zybz == ((int)EnumZYBZ.Ycy).ToString())
            {
                throw new FailedException("患者已出院");
            }
            if (zybrxx.zybz == ((int)EnumZYBZ.Wry).ToString())
            {
                throw new FailedException("已作废记录");
            }
            if (!(zybrxx.brxz == "8"))
            {
                throw new FailedException("当前非新农合");
            }

            return new object();
        }
        #endregion

        #region ZFToXNH 
        public bool ZFToXNH_Step_4(string zyh, string grbm, string xm,out string msg)
        {
            var orgId = this.OrganizeId;
            msg = "";
            var zybrxx = _hospPatientBasicInfoRepo.IQueryable().Where(p => p.zyh == zyh && p.OrganizeId == orgId).FirstOrDefault();

            int patid = zybrxx.patid;
            SysPatientBasicInfoEntity xtbrxx = null;

            //var xtbrxxList = _sysPatientBasicInfoRepo.IQueryable().Where(p => p.xnhgrbm == grbm && p.OrganizeId == orgId && p.zt == "1").ToList();
            //if (xtbrxxList.Count == 1)
            //{
            //    if (xtbrxxList[0].patid != zybrxx.patid)
            //    {
            //        msg = "该农合卡在系统里已绑定其他身份,不能再用来转该患者新农合信息，请联系his！个人编码：" + grbm.ToString();
            //        return false;
            //    }
            //    xtbrxx = xtbrxxList[0];
            //}
            //else if (xtbrxxList.Count == 0)
            //{
            //    xtbrxx = _sysPatientBasicInfoRepo.IQueryable().Where(p => p.patid == patid && p.OrganizeId == orgId && p.zt == "1").First();
            //    if (!string.IsNullOrWhiteSpace(xtbrxx.xnhgrbm) && xtbrxx.xnhgrbm != grbm)
            //    {
            //        msg = "该住院患者已绑定其他农合卡";
            //        return false;
            //    }
            //}
            //else
            //{
            //    msg = "数据异常，该社保卡在基础信息中存在多条，无法定位";
            //    return false;
            //}

            if (zybrxx.xm != xm)
            {
                msg = "异常，住院患者与农合卡姓名不一致，请先修改患者姓名";
                return false;
            }
            return true;
        }

        public S04RequestDTO GetZfToXnhPatInfo(string zyh)
        {
            var orgId = this.OrganizeId;

            var sysHosBasicInfoVO= _patientBasicInfoDmnService.GetZfToXnhPatInfo(zyh,orgId);
            return _patientBasicInfoDmnService.ComposeS04par(sysHosBasicInfoVO, orgId);
        }

        public object ZFToXNH_Step_8(string zyh1)
        {
            UpdatebrxzRequest par = new UpdatebrxzRequest { zyh = zyh1, brxzCode = "8", brxzmc = "新农合" };
            //同步数据至CPOE
            SiteCISAPIHelper.UpdatebrxzInfo(par);
            return new object();
        }
        #endregion

        #region 新农合

        #endregion

    }
}
