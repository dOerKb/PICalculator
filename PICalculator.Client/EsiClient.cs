using Microsoft.Extensions.Configuration;
using PICalculator.Data;

namespace PICalculator.Client
{
    public class EsiClient
    {
        private readonly AuthorizationClient authorizationClient;

        public EsiClient()
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<EsiClient>()
                .Build();

            this.authorizationClient = new AuthorizationClient(config);
        }

        public async Task<Character> Authenticate()
        {
            Character character = null;

            await Task.Run(() => character = this.authorizationClient.Authenticate());

            return character;
        }
    }
}