using EzzePay.BLL.Services;
using EzzePay.Model.Interfaces;

namespace EzzePay.Api.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAuthServices(this IServiceCollection services)
        {
            services.AddSingleton<IAuthService, AuthService>();
        }
    }
}
