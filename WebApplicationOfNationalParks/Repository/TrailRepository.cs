using WebApplicationOfNationalParks.Models;
using WebApplicationOfNationalParks.Repository.IRepository;

namespace WebApplicationOfNationalParks.Repository
{
    public class TrailRepository:Repository<Trail>,ITrailRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TrailRepository(IHttpClientFactory httpClientFactory)
            :base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
