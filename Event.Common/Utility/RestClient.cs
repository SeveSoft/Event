using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Event.Common.Utility
{
    public class RestClient
    {
        public async Task<T> MakeTheCall<T>(Uri url, HttpMethod method, string apiKey = "", string jsonBody = "") where T : new()
        {
            try
            {
                var client = new HttpClient();
                var message = new HttpRequestMessage(method, url);

                //Only add apiKey if we have one
                if (!string.IsNullOrEmpty(apiKey))
                {
                    client.DefaultRequestHeaders.Add("ApiKey", apiKey);
                }
                //Only add jsonBody if we have one
                if (!string.IsNullOrEmpty(jsonBody))
                {
                    message.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                }

                var response = client.SendAsync(message).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    return string.IsNullOrEmpty(responseString) ? new T() : JsonConvert.DeserializeObject<T>(responseString);
                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new T();
                }

                if (!response.IsSuccessStatusCode)
                {
                    var httpRequestException = new HttpRequestException(response.ReasonPhrase);
                    httpRequestException.Data.Add(nameof(url), url);
                    httpRequestException.Data.Add(nameof(response.StatusCode), response.StatusCode);
                    httpRequestException.Data.Add(nameof(jsonBody), jsonBody);
                    throw httpRequestException;
                }
            }
            catch (Exception exception)
            {
                LogError(exception);
            }
            return new T();
        }

        private void LogError(Exception exception)
        {
            //Do something
        }
    }
}
