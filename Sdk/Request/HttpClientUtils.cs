using Planckx.Sdk.Bean;
using Planckx.Sdk.Client;
using System;

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using static Planckx.Sdk.Bean.ResponseCode;

namespace Planckx.Sdk.Request
{
    internal class HttpClientUtils
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        private static readonly HttpClient HttpClientInstance;

        static HttpClientUtils()
        {
            HttpClientInstance = new HttpClient();
        }

        public static async Task<string> Post(IOption option, IList<KeyValuePair<string, string>> formData = null, string charset = "UTF-8", string mediaType = "application/x-www-form-urlencoded")
        {
            HttpClientInstance.BaseAddress = new Uri(option.RestHost);
            HttpContent content = new FormUrlEncodedContent(formData ?? new List<KeyValuePair<string, string>>());
            content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);

            content.Headers.ContentType.CharSet = charset;
            HmacSha1Utils.AddSign(content.Headers, null, option);

            HttpResponseMessage resp = await HttpClientInstance.PostAsync(option.RequestUrl, content);
            try
            {
                resp.EnsureSuccessStatusCode();
                //Console.WriteLine("Response Code: {0} - {1}", (int)resp.StatusCode, resp.StatusCode.ToString());
                string token = await resp.Content.ReadAsStringAsync();
                return token;
            }
            catch (HttpRequestException e)
            {
                if (resp != null)
                    throw new RequestException((int)resp.StatusCode, resp.StatusCode.ToString(), e);

                throw new RequestException(e.Message, e);
            }
        }

        public static async Task<string> Get(IOption option, IList<KeyValuePair<string, string>> formData = null)
        {
            HttpContent content = new FormUrlEncodedContent(formData ?? new List<KeyValuePair<string, string>>());
            if (formData != null)
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                content.Headers.ContentType.CharSet = "UTF-8";
            }

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(option.RequestUrl),
                Method = HttpMethod.Get,
                Content = content
            };

            HmacSha1Utils.AddSign(request.Headers, formData, option);

            HttpResponseMessage resp = await HttpClientInstance.SendAsync(request);
            try
            {
                resp.EnsureSuccessStatusCode();
                // Console.WriteLine("Response Code: {0} - {1}", (int)resp.StatusCode, resp.StatusCode.ToString());

                string token = await resp.Content.ReadAsStringAsync();
                return token;
            }
            catch (HttpRequestException e)
            {
                if (resp != null)
                    throw new RequestException((int)resp.StatusCode, resp.StatusCode.ToString(), e);

                throw new RequestException(e.Message, e);
            }
        }

        public static ResponseCode.ResponseEnum ParseResponse(string jsonResult, out string dataJson)
        {
            //Console.WriteLine("Response Json: {0}", jsonResult);
            IDictionary<string, JsonElement> jsonObject =
                JsonSerializer.Deserialize<IDictionary<string, JsonElement>>(jsonResult);

            ResponseEnum response = ResponseCode.Find(jsonObject["code"].ToString());
            dataJson = null;
            if (response == ResponseEnum.Successful)
                dataJson = jsonObject["data"].ToString();

            return response;
        }

    }

}
