using PICalculator.Client.Events;
using PICalculator.Client.Properties;
using System.Net;
using System.Text;

namespace PICalculator.Client.AuthenticationCallback
{
    internal class AuthenticationListener
    {
        private readonly HttpListener httpListener;

        public AuthenticationListener()
        {
            this.httpListener = new HttpListener();
            this.httpListener.Prefixes.Add($"{Constants.CALLBACK_BASE_URL}");
        }

        public event EventHandler<AuthenticationCallbackEventArgs> OnIncomingRequest;

        public void Start()
        {
            this.httpListener.Start();
            Task.Run(async () => await this.Listen());
        }

        public void Stop()
        {
            if (!this.httpListener.IsListening)
            {
                throw new InvalidOperationException("The listener cannot be stopped as it is not listening.");
            }

            this.httpListener.Stop();
        }

        private async Task Listen()
        {
            HttpListenerContext context = await this.httpListener.GetContextAsync();

            if (string.IsNullOrEmpty(context.Request.RawUrl))
            {
                throw new InvalidOperationException("The callback URL is empty.");
            }

            var index = context.Request.RawUrl.IndexOf(Constants.CALLBACK_QUERY_STRING);
            var acessToken = context.Request.RawUrl.Substring(index + Constants.CALLBACK_QUERY_STRING.Length);
            this.AsnwerRequest(context.Response);

            this.OnIncomingRequest?.Invoke(this, new AuthenticationCallbackEventArgs(acessToken));
        }

        private void AsnwerRequest(HttpListenerResponse response)
        {
            using (Stream responseStream = response.OutputStream)
            {
                byte[] content = Encoding.UTF8.GetBytes(Resources.Authentication);

                response.ContentLength64 = content.Length;
                response.ContentType = "text/html; charset=UTF-8";
                response.KeepAlive = false;

                responseStream.Write(content, 0, content.Length);
                responseStream.Close();
            }
        }
    }
}
