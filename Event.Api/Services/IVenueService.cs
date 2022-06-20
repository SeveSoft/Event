﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Event.Common.Models;

namespace Event.Api.Services
{
    public interface IVenueService
    {
        Task<List<Venue>> GetVenuesAsync(CancellationToken httpContextRequestAborted);

    }
}
