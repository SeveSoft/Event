using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Event.Api.Services.Models;
using Event.Common.Models;
using Event.Common.Utility;
using Microsoft.Extensions.Configuration;

namespace Event.Api.Services
{

    public class TopicService : ITopicService
    {
        private readonly string _apiKeyPublic;
        private readonly string _apiKeyPrivate;
        private readonly RestClient _restClient;

        public TopicService(IConfiguration configuration)
        {
            _restClient = new RestClient();
            _apiKeyPublic = Encryptor.DecryptString(configuration["TopicService:PublicApiKey"]);
            _apiKeyPrivate = Encryptor.DecryptString(configuration["TopicService:PrivateApiKey"]);
        }
        public async Task<List<Topic>> GetTopicsAsync(CancellationToken httpContextRequestAborted)
        {
            var list = new List<Topic>();

            var ts = DateTime.Now.Ticks;
            var hash = $"{ts}{_apiKeyPrivate}{_apiKeyPublic}".ToMd5Hash();

            var url = $"https://gateway.marvel.com:443/v1/public/characters?ts={ts}&apikey={_apiKeyPublic}&hash={hash}";
            var marvelData = await _restClient.MakeTheCall<MarvelData>(new Uri(url), HttpMethod.Get);

            if (marvelData != null && marvelData.Data != null)
            {
                foreach (var result in marvelData.Data.Results)
                {
                    foreach (var comicItem in result.Comics.Items)
                    {
                        list.Add(new Topic()
                        {
                            Id = Int32.Parse(comicItem.ResourceUrl.Split("/").Last()),
                            Name = comicItem.Name

                        });
                    }
                }
            }
            return list;
        }
    }
}
