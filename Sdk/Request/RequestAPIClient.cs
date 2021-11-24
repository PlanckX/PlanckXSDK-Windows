using System;
using System.Collections.Generic;
using Planckx.Sdk.Request;
using System.Text;
using Planckx.Sdk.Bean;
using System.Collections;

namespace Planckx.Sdk.Request
{
    public class RequestAPIClient : IRequestAPI
    {
        public NftAsset AssetById(long tokenId)
        {
            return null;
        }

        public IList<NftAsset> AssetsByGame()
        {
            return null;
        }

        public IList<NftAsset> AssetsByPlayer(string playerId)
        {
            return null;
        }

        public CheckBind CheckAccount(string playerId)
        {
            return null;
        }
    }
}
