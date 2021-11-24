using Planckx.Sdk.Bean;
using System;
using System.Collections.Generic;
using System.Text;

namespace Planckx.Sdk.Request
{
    interface IRequestAPI
    {
        CheckBind CheckAccount(string playerId);
        
        IList<NftAsset> AssetsByGame();

        IList<NftAsset> AssetsByPlayer(string playerId);

        NftAsset AssetById(long tokenId);
    }
}
