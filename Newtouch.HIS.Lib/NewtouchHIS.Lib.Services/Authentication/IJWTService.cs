using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Lib.Services.Authentication
{
    public interface IJWTService
    {
        Task<BusResult<JWTToken>> GetTokenAsync(JWTTokenRequest request);

    }
}
