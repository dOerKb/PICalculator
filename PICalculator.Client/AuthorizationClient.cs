using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PICalculator.Client.AuthenticationCallback;
using PICalculator.Client.Models;
using PICalculator.Data;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace PICalculator.Client
{
    internal class AuthorizationClient
    {
        private readonly IConfiguration config;
        private readonly AuthenticationListener authenticationListener;

        private Character character;
        private bool volatileCompletionFlag;

        internal AuthorizationClient(IConfiguration config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));

            this.authenticationListener = new AuthenticationListener();
            this.authenticationListener.OnIncomingRequest += this.OnIncomingAuthorizationResponse;

            volatileCompletionFlag = false;
        }

        public Character Authenticate()
        {
            this.authenticationListener.Start();

            var processStartInfo = new ProcessStartInfo()
            {
                FileName = Constants.REQUSET_AUTHORIZATION_URL,
                UseShellExecute = true
            };

            Process.Start(processStartInfo);

            while (!volatileCompletionFlag)
            {
                Task.Delay(1000);
            }

            this.authenticationListener.Stop();

            return this.character;
        }

        private async void OnIncomingAuthorizationResponse(object? sender, Events.AuthenticationCallbackEventArgs e)
        {
            if (string.IsNullOrEmpty(e.AuthorizationCode))
            {
                throw new ArgumentException("There is no access token.");
            }

            AccessTokenDto accessToken = await this.GetAccessToken(e.AuthorizationCode);
            CharacterTokenDto characterToken = await this.GetCharacterToken(accessToken.AccessToken);

            this.character = new Character()
            {
                Name = characterToken.CharacterName
            };

            this.volatileCompletionFlag = true;
        }

        private async Task<AccessTokenDto> GetAccessToken(string code)
        {
            if (code == null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            if (code == String.Empty)
            {
                throw new ArgumentException($"{nameof(code)} must not be an empty string.");
            }

            AccessTokenDto accessToken;

            using (var request = new HttpRequestMessage(HttpMethod.Post, Constants.GRANT_ACCESS_TOKEN_URL))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", this.ToBase64String($"{Constants.APP_CLIENT_ID}:{this.config["clientSecret"]}"));
                request.Content = new StringContent(this.CreateAccessTokenJson(code), Encoding.UTF8, "application/json");

                var client = new HttpClient();
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                accessToken = JsonConvert.DeserializeObject<AccessTokenDto>(responseContent);
            }

            return accessToken;
        }

        private async Task<CharacterTokenDto> GetCharacterToken(string accessToken)
        {
            if (accessToken == null)
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            if (accessToken == String.Empty)
            {
                throw new ArgumentException($"{nameof(accessToken)} must not be an empty string.");
            }

            CharacterTokenDto characterToken;

            using (var request = new HttpRequestMessage(HttpMethod.Get, Constants.VERIFY_ACCES_URL))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var client = new HttpClient();
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                characterToken = JsonConvert.DeserializeObject<CharacterTokenDto>(responseContent);
            }

            return characterToken;
        }

        private string CreateAccessTokenJson(string code)
        {
            if (code == null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            if (code == String.Empty)
            {
                throw new ArgumentException($"{nameof(code)} must not be an empty string.");
            }

            StringBuilder @string = new StringBuilder();
            @string.Append("{");
            @string.Append("\"grant_type\":\"authorization_code\",");
            @string.Append($"\"code\":\"{code}\"");
            @string.Append("}");
            return @string.ToString();
        }

        private string ToBase64String(string authenticationValue)
        {
            var bytes = Encoding.UTF8.GetBytes(authenticationValue);
            return Convert.ToBase64String(bytes);
        }
    }
}

