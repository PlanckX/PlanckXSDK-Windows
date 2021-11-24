using Planckx.Sdk.Bean;
using Planckx.Sdk.Request;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using static Planckx.Sdk.Bean.ResponseCode;

namespace Planckx.Sdk.Client
{
    public class PlanckxNftClient
    {
        private const string ALL_NFT_URL = "/api/v1/sdk/NFT/list";

        private const string PLAYER_NFT_URL = "/api/v1/sdk/NFT/player/list/";

        private const string TOKEN_NFT_URL = "/api/v1/sdk/NFT/token/";

        private IOption Option;

        public PlanckxNftClient(IOption option)
        {
            Option = option ?? throw new ArgumentNullException(nameof(option));
        }

        public ResponseEnum AllNfts(out IList<NftAsset> resultNfts)
        {
            resultNfts = null;
            Option.RequestUrl = ALL_NFT_URL;

            string result = this.Execution(Option);

            ResponseEnum responseEnum = HttpClientUtils.ParseResponse(result, out string dataJson);
            if (responseEnum == ResponseEnum.Successful && !string.IsNullOrEmpty(dataJson))
            {
                resultNfts = JsonSerializer.Deserialize<IList<NftAsset>>(dataJson.ToString());
            }

            return responseEnum;
        }

        public ResponseEnum NftByPlayer(string playerId, out IList<NftAsset> resultNfts)
        {
            resultNfts = null;
            Option.RequestUrl = string.Concat(PLAYER_NFT_URL, playerId);

            string result = this.Execution(Option);

            ResponseEnum responseEnum = HttpClientUtils.ParseResponse(result, out string dataJson);
            if (responseEnum == ResponseEnum.Successful && !string.IsNullOrEmpty(dataJson))
            {
                resultNfts = JsonSerializer.Deserialize<IList<NftAsset>>(dataJson.ToString());
            }

            return responseEnum;
        }

        public ResponseEnum NftByTokenId(string tokenId, out NftAsset resultNft)
        {
            resultNft = null;
            Option.RequestUrl = string.Concat(TOKEN_NFT_URL, tokenId);

            string result = this.Execution(Option);

            ResponseEnum responseEnum = HttpClientUtils.ParseResponse(result, out string dataJson);
            if (responseEnum == ResponseEnum.Successful && !string.IsNullOrEmpty(dataJson))
            {
                resultNft = JsonSerializer.Deserialize<NftAsset>(dataJson.ToString());
            }

            return responseEnum;
        }


        private string Execution(IOption option)
        {
            try
            {
                Task<string> result = HttpClientUtils.Get(option);
                result.Wait();
                return result.Result;
            }
            catch (AggregateException ae)
            {
                ae.Handle((x) =>
                {
                    if (x is RequestException re) // This we know how to handle.
                    {
                        //respEnum = ResponseCode.Find(re.Code.ToString());
                        throw re;
                    }
                    return false; // Let anything else stop the application.
                });
            }

            return null;
        }

    }
}
