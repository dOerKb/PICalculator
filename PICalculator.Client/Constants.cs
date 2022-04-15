namespace PICalculator.Client
{
    internal static class Constants
    {
        #region General
        internal const string APP_CLIENT_ID = "4507dafd133d48e5a77ee7ccf184fa51";
        internal const string APP_SCOPES = "publicData esi-location.read_location.v1 esi-skills.read_skills.v1 esi-skills.read_skillqueue.v1 esi-planets.manage_planets.v1 esi-planets.read_customs_offices.v1 esi-characterstats.read.v1";
        //internal const string APP_SCOPES = "esi-skills.read_skills.v1";
        #endregion

        #region Authorization
        internal const string TQ_AUTHORIZTAION_BASE_URL = "https://login.eveonline.com";
        internal const string OAUTH_RESPONSE_TYPE = "code";
        internal const string CALLBACK_BASE_URL = "http://localhost:50001/";
        internal const string CALLBACK_URL = $"{CALLBACK_BASE_URL}oauth-callback";

        internal const string CALLBACK_QUERY_STRING = "?code=";

        internal const string REQUSET_AUTHORIZATION_URL = $"{TQ_AUTHORIZTAION_BASE_URL}/oauth/authorize?response_type={OAUTH_RESPONSE_TYPE}&redirect_uri={CALLBACK_URL}&client_id={APP_CLIENT_ID}&scope={APP_SCOPES}";
        internal const string GRANT_ACCESS_TOKEN_URL = $"{TQ_AUTHORIZTAION_BASE_URL}/oauth/token";
        internal const string VERIFY_ACCES_URL = $"{TQ_AUTHORIZTAION_BASE_URL}/oauth/verify";
        #endregion
    }
}
