using System;
namespace Planckx.Sdk.Request
{
    public class PlanckxOption : IOption
    {
        private string requestUrl;

        public string ApiKey { get; set; }
        public string SecretKey { get; set; }
        public string RestHost { get { return "http://pre-server.biteye.info:10005/open"; } }
        public string RequestUrl
        {
            get
            {
                return string.Concat(RestHost, requestUrl);
            }
            set
            {
                requestUrl = value;
            }
        }

        public bool Signature { get { return true; } }
    }
}
