using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Planckx.Sdk.Bean
{
    public class NftAsset
    {
        [JsonPropertyName("gameId")]
        public int GameId { get; set; }

        // Asset name
        [JsonPropertyName("nftName")]
        public string Name { get; set; }

        // Asset type
        [JsonPropertyName("nftType")]
        public string Type { get; set; }

        // Creator address
        [JsonPropertyName("authorAddress")]
        public string CreatorAddress { get; set; }

        [JsonPropertyName("ownerAddress")]
        public string OwnerAddress { get; set; }

        [JsonPropertyName("nftContent")]
        public string Content { get; set; }

        [JsonPropertyName("nftDescription")]
        public string Description { get; set; }

        [JsonPropertyName("tokenId")]
        public string TokenId { get; set; }

        [JsonPropertyName("nftData")]
        public string Addition { get; set; }

        public override string ToString() => new StringBuilder()
                .Append("gameId=").Append(GameId)
                .Append("name=").Append(Name)
                .Append("type=").Append(Type)
                .Append("creatorAddress=").Append(CreatorAddress)
                .Append("ownerAddress=").Append(OwnerAddress)
                .Append("content=").Append(Content)
                .Append("description=").Append(Description)
                .Append("tokenId=").Append(TokenId)
                .Append("addition=").Append(Addition)
                .ToString();
    }
}
