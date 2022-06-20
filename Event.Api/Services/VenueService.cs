using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Event.Api.Services.Models;
using Event.Common.Models;
using Event.Common.Utility;

namespace Event.Api.Services
{
    public class VenueService : IVenueService
    {
        private readonly RestClient _restClient;

        public VenueService()
        {
            _restClient = new RestClient();
        }
        public async Task<List<Venue>> GetVenuesAsync(CancellationToken httpContextRequestAborted)
        {
            var list = new List<Venue>();

            var url = "https://api.openbrewerydb.org/breweries";
            var breweries = await _restClient.MakeTheCall<List<Brewery>>(new Uri(url), HttpMethod.Get);

            if (breweries != null)
            {
                foreach (var brewery in breweries)
                {
                    list.Add(new Venue()
                    {
                        VenueId = brewery.Id,
                        Name = brewery.Name,
                        City = brewery.City,
                        PostalCode = brewery.PostalCode
                    });
                }
            }
            
            return list;
        }
    }
}
