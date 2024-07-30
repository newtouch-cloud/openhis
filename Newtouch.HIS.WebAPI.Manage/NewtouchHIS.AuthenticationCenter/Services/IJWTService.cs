using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.AuthenticationCenter.Services
{
    public interface IJWTService
    {
        Task<BusResult<JWTToken>> GetTokenAsync(JWTTokenRequest request);
        Task<BusResult<JWTToken>> TokenRefreshAsync(JWTTokenRequest request);
        #region auth
        ///// <summary>
        ///// 获取授权码
        ///// </summary>
        ///// <param name="userName"></param>
        ///// <param name="password"></param>
        ///// <returns></returns>
        ///// <exception cref="NotImplementedException"></exception>
        //Task<BusResult<string>> GetCode(string clientId, string userName, string password);
        ///// <summary>
        ///// 根据会话Code获取授权码
        ///// </summary>
        ///// <param name="clientId"></param>
        ///// <param name="sessionCode"></param>
        ///// <returns></returns>
        //Task<BusResult<string>> GetCodeBySessionCode(string clientId, string sessionCode);

        ///// <summary>
        ///// 根据授权码获取Token+RefreshToken
        ///// </summary>
        ///// <param name="authCode"></param>
        ///// <returns>Token+RefreshToken</returns>
        //Task<BusResult<JWTToken>> GetTokenWithRefresh(string authCode);

        ///// <summary>
        ///// 根据RefreshToken刷新Token
        ///// </summary>
        ///// <param name="refreshToken"></param>
        ///// <param name="clientId"></param>
        ///// <returns></returns>
        //Task<string> GetTokenByRefresh(string refreshToken, string clientId);
        #endregion
    }
}
