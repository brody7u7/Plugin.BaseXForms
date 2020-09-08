using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.BaseXForms.Settings
{
    public class ApiSettings
    {
        private const string API_PRODUCTION = "";
        private const string API_SANDBOX = "";
        public const string API_URL = IS_API_PRODUCTION ? API_PRODUCTION : API_SANDBOX;
        public const bool IS_API_PRODUCTION = false;
    }
}
