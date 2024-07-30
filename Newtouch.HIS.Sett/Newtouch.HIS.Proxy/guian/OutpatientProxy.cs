using System.Collections.Generic;
using Newtouch.HIS.Proxy.guian.DTO;
using Newtouch.HIS.Proxy.guian.DTO.S18;
using Newtouch.HIS.Proxy.guian.DTO.S19;
using Newtouch.HIS.Proxy.guian.DTO.S20;
using Newtouch.HIS.Proxy.guian.DTO.S21;
using Newtouch.HIS.Proxy.guian.DTO.S22;
using Newtouch.HIS.Proxy.guian.DTO.S23;
using Newtouch.HIS.Proxy.guian.DTO.S24;
using Newtouch.HIS.Proxy.guian.DTO.S25;
using Newtouch.HIS.Proxy.guian.DTO.S26;
using Newtouch.HIS.Proxy.guian.DTO.S27;

namespace Newtouch.HIS.Proxy.guian
{
    /// <summary>
    /// 门诊相关
    /// </summary>
    public class OutpatientProxy
    {
        #region 单例
        private static readonly OutpatientProxy _outpatientProxy = new OutpatientProxy();

        private OutpatientProxy()
        {
        }

        public static OutpatientProxy GetInstance(string organizeId)
        {
            OrganizeId = organizeId;
            return _outpatientProxy;
        }

        #endregion

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public static string OrganizeId { get; set; }

        /// <summary>
        /// 根据获取的家庭参合列表进行门诊上传 跨省登记修改也是这个接口，使用uploadType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<S18ResponseDTO> S18(S18RequestDTO request)
        {
            return new GuiAnWebServiceDefaultFactory<S18RequestDTO, Response<S18ResponseDTO>>(request, OrganizeId).Call() as Response<S18ResponseDTO>;
        }

        /// <summary>
        /// 根据S18上传门诊返回的补偿序号outpId进行门诊回退
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<S19ResponseDTO> S19(S19RequestDTO request)
        {
            return new GuiAnWebServiceDefaultFactory<S19RequestDTO, Response<S19ResponseDTO>>(request, OrganizeId).Call() as Response<S19ResponseDTO>;
        }

        /// <summary>
        /// 根据S18上传门诊返回的补偿序号outpId进行门诊信息修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<S20ResponseDTO> S20(S20RequestDTO request)
        {
            return new GuiAnWebServiceDefaultFactory<S20RequestDTO, Response<S20ResponseDTO>>(request, OrganizeId).Call() as Response<S20ResponseDTO>;
        }

        /// <summary>
        /// 根据S18门诊上传返回的补偿序号outpId进行门诊费用上传,结算后上传无效
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<S21ResponseDTO> S21(S21RequestDTO request)
        {
            return new GuiAnWebServiceDefaultFactory<S21RequestDTO, Response<S21ResponseDTO>>(request, OrganizeId).Call() as Response<S21ResponseDTO>;
        }

        /// <summary>
        /// 查询当此门诊已上传费用明细
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<List<DTO.S22.item>> S22(S22RequestDTO request)
        {
            return new GuiAnWebServiceDefaultFactory<S22RequestDTO, Response<List<DTO.S22.item>>>(request, OrganizeId).Call() as Response<List<DTO.S22.item>>;
        }

        /// <summary>
        /// 根据S18门诊上传返回的补偿序号outpId门诊费用明细查询S22进行门诊费用明细退单；跨省如果只传补偿序号，不传明细ID 就是把所有明细删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<S23ResponseDTO> S23(S23RequestDTO request)
        {
            return new GuiAnWebServiceDefaultFactory<S23RequestDTO, Response<S23ResponseDTO>>(request, OrganizeId).Call() as Response<S23ResponseDTO>;
        }

        /// <summary>
        /// 根据S18门诊上传返回的补偿序号outpId进行门诊模拟结算
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<S24ResponseDTO> S24(S24RequestDTO request)
        {
            return new GuiAnWebServiceDefaultFactory<S24RequestDTO, Response<S24ResponseDTO>>(request, OrganizeId).Call() as Response<S24ResponseDTO>;
        }

        /// <summary>
        /// 根据S18门诊上传返回的补偿序号outpId进行门诊结算
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<S25ResponseDTO> S25(S25RequestDTO request)
        {
            return new GuiAnWebServiceDefaultFactory<S25RequestDTO, Response<S25ResponseDTO>>(request, OrganizeId).Call() as Response<S25ResponseDTO>;
        }

        /// <summary>
        /// 根据S18门诊上传返回的补偿序号outpId进行门诊冲红
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<S26ResponseDTO> S26(S26RequestDTO request)
        {
            return new GuiAnWebServiceDefaultFactory<S26RequestDTO, Response<S26ResponseDTO>>(request, OrganizeId).Call() as Response<S26ResponseDTO>;
        }

        /// <summary>
        /// 根据S18门诊上传返回的补偿序号outpId进行获取门诊结算单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response<S27ResponseDTO> S27(S27RequestDTO request)
        {
            return new GuiAnWebServiceDefaultFactory<S27RequestDTO, Response<S27ResponseDTO>>(request, OrganizeId).Call() as Response<S27ResponseDTO>;
        }
    }
}
