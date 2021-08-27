
namespace TomsonsApp.Settings
{
    public static class ApiSettings
    {
        public const bool IsApiProduction = false;
        public const string ApiUrl = IsApiProduction ? ApiProduction : ApiSandbox;

        private const string ApiProduction = "";
        private const string ApiSandbox = "";
    }
}
