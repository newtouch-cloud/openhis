using FrameworkBase.MultiOrg.Application;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface.OutpatientManage;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class OutPatientApp : AppBase, IOutPatientApp
    {
        private readonly IOutpatientRegistRepo _outpatientRegistRepo;
        private readonly IOutPatientUniversalDmnService _outPatientUniversalDmnService;

        /// <summary>
        /// 保存门诊记账（仅新增）
        /// </summary>
        public void SaveoutpatientAccountInfo(OutpatAccBasicInfoDto bacDto, List<OutpatAccGridInfoDto> accDto, string orgId)
        {
            if (accDto == null || accDto.Count < 1)
            {
                throw new FailedException("缺少计划数据");
            }
            var ghIsExits = _outpatientRegistRepo.IQueryable().FirstOrDefault(p => p.mzh == bacDto.mzh && p.zt == "1" && p.OrganizeId == orgId);
            if (ghIsExits == null)
            {
                throw new FailedException("门诊号不存在，请确认");
            }
            var ItemsGroupBO = new FeeItemsGroupedBO
            {
                xmList = new List<OutpatientItemEntity>(),//新增项目明细
            };
            foreach (var item in accDto)
            {
                int zsl = 0;//总计费数量
                var dcsl = 0;//单次数量=治疗量/单位计量数
                int? zzll = item.zll * item.sl;
                var zje = item.Zje;
                if (item.sfxmCode != null)
                {
                    dcsl = CommmHelper.CalcSfxmSl(item.zll, item.dwjls, item.jjcl);
                    //总数
                    zsl = (dcsl * item.sl).ToInt();
                    zje = zsl * item.dj;    //重新计算总金额
                }

                //门诊项目
                var mzxm = new OutpatientItemEntity
                {
                    xmnm = 0,
                    sfxm = item.sfxmCode,
                    dl = item.sfdlCode,
                    dj = item.dj,
                    sl = zsl,
                    je = zje,
                    dw = item.dw,
                    zzll = zzll,
                    dczll = item.zll,
                    zxcs = item.sl, //执行次数
                    bz = item.bz,
                    ssbz = "9",    //直接进入 实施过程中 这样 不可操作退费
                    sfrq = item.jzsj
                };
                mzxm.Create();
                ItemsGroupBO.xmList.Add(mzxm);
            }

            //新项目 薪结算
            if (ItemsGroupBO.xmList != null && ItemsGroupBO.xmList.Count() > 0)
            {
                IList<int> cfnmList = new List<int>();
                IList<int> xmnmList = new List<int>();
                IList<string> updateSkipSettledCfhList;
                IList<FeeItemsGroupedBO> ItemsGroupBOList = new List<FeeItemsGroupedBO>();
                ItemsGroupBOList.Add(ItemsGroupBO);
                //提交明细（处方的话更新主表信息）
                _outPatientUniversalDmnService.SubmitItems(orgId, out cfnmList, out xmnmList, out updateSkipSettledCfhList, ghIsExits.mzh, ghIsExits.ys, ItemsGroupBOList);
                // 更新门诊处方的结算信息（更新结算信息）
                var settAddBO = new OutpatientSettlementAddBO()
                {
                    cfnmList = cfnmList,
                    xmnmList = xmnmList,
                    autoGenePlan = true,    //自动生成门诊计划
                };
                _outPatientUniversalDmnService.AddSettlement(orgId, ghIsExits.mzh, settAddBO);
            }
        }

    }
}
