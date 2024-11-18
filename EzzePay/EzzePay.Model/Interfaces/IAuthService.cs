using EzzePay.Model.DTO;

namespace EzzePay.Model.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseAuth> GetAccessTokenAsync();
    }
}
