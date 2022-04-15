namespace PICalculator.Client.Events
{
    internal class AuthenticationCallbackEventArgs : EventArgs
    {
        public AuthenticationCallbackEventArgs(string authorizationCode)
        {
            if (authorizationCode == null)
            {
                throw new ArgumentNullException(nameof(authorizationCode));
            }

            if (authorizationCode == String.Empty)
            {
                throw new ArgumentException($"{nameof(authorizationCode)} must not be an empty string.");
            }

            this.AuthorizationCode = authorizationCode;
        }

        public string AuthorizationCode { get; set; }
    }
}
