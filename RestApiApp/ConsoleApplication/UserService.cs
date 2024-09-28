using System.Net;
using System.Net.Http.Json;
using System.Security.Authentication;
using ConsoleApplication.Contracts;
using ConsoleApplication.Contracts.Requests;
using ConsoleApplication.Contracts.Responses;

namespace ConsoleApplication
{

    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(string link)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(link);
            _httpClient.Timeout = new TimeSpan(0, 0, 10);
        }
        public async Task<LoginResponse> LoginUser(UserCredential userCredential)
        {
            var response = await _httpClient.PostAsJsonAsync("account/login", userCredential);

            if (response.StatusCode is HttpStatusCode.Unauthorized)
                throw new AuthenticationException();

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error: {response.ReasonPhrase}");

            var authenticatedUser = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return authenticatedUser!;
        }
        public async Task RegisterUser(RegisterRequest registerRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("account/register", registerRequest);
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error: {response.ReasonPhrase}");
        }
    }

}
