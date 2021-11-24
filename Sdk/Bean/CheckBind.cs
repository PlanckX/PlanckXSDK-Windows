using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Planckx.Sdk.Bean
{
    public class CheckBind
    {
        [JsonPropertyName("isBind")]
        public bool Bind { get; set; }

        [JsonPropertyName("openUrl")]
        public string AuthAddress { get; set; }


        public override string ToString() => string.Concat("Bind=", Bind, ", AuthAddress=", AuthAddress ?? "null");
    }
}
