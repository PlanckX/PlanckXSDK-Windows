using Planckx.Sdk.Bean;
using Planckx.Sdk.Request;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using static Planckx.Sdk.Bean.ResponseCode;

namespace Planckx.Sdk.Client
{
    public class PlanckxAccountClient
    {
        private const string ACCOUNT_BIND_URL = "/api/v1/sdk/checkBind/";

        private IOption Option;
        
        public PlanckxAccountClient(IOption option)
        {
            Option = option ?? throw new ArgumentNullException(nameof(option));
        }

        public ResponseEnum BindState(string playerId, out CheckBind accountBind)
        {
            accountBind = null;
            Option.RequestUrl = string.Concat(ACCOUNT_BIND_URL, playerId);

            Task<String> result;
            try
            {
                result = HttpClientUtils.Get(Option);
                result.Wait();
            }
            catch (AggregateException ae)
            {
                ResponseEnum respEnum = ResponseEnum.Forbidden;
                ae.Handle((x) =>
                {
                    if (x is RequestException re) // This we know how to handle.
                    {
                        respEnum = ResponseCode.Find(re.Code.ToString());
                        throw re;
                    }
                    return false; // Let anything else stop the application.
                });

                return respEnum;
            }

            ResponseEnum responseEnum = HttpClientUtils.ParseResponse(result.Result, out string dataJson);
            if (responseEnum == ResponseEnum.Successful && !string.IsNullOrEmpty(dataJson))
            {
                accountBind = JsonSerializer.Deserialize<CheckBind>(dataJson.ToString());
            }

            return responseEnum;
        }
    }
}
