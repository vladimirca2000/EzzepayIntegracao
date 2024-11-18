using EzzePay.Model.DTO;
using EzzePay.Model.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;

namespace EzzePay.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string _accessToken;
        private DateTime _tokenExpiration;

        public AuthService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<ResponseAuth> GetAccessTokenAsync()
        {
            if (_accessToken == null || DateTime.UtcNow >= _tokenExpiration)
            {
                await RefreshTokenAsync();
            }

            return new ResponseAuth
            {
                AccessToken = _accessToken ?? string.Empty,
                TokenExpiration = _tokenExpiration,
                ExpiresIn = (int)(_tokenExpiration - DateTime.UtcNow).TotalSeconds
            };
        }

        private async Task RefreshTokenAsync()
        {
            var requestAuth = new RequestAuth();
            var baseUrl = _configuration["EzzePayments:BaseUrl"];
            var authorizationHeader = _configuration["EzzePayments:code64"];

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{baseUrl}oauth/token")
            {
                Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", requestAuth.GrantType)
                })
            };

            requestMessage.Headers.Add("Authorization", authorizationHeader);

            var response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseAuth = JsonSerializer.Deserialize<ResponseAuth>(responseContent);

                _accessToken = responseAuth?.AccessToken;
                _tokenExpiration = DateTime.UtcNow.AddSeconds(responseAuth?.ExpiresIn ?? 0);
            }
            else
            {
                throw new Exception("Failed to retrieve access token.");
            }
        }
    }
}
