namespace SyncLib.Model.Common
{
    public class ApplicationSettingsModel
    {
        public const string ApplicationSettings = "ApplicationSettings";
        public string BaseUrl { get; set; }

        public JwtToken Jwt { get; set; }
    }

    public class JwtToken
    {
        public string SecretKey { get; set; }
    }

}
