using WebApplicationOfNationalParks.Models;
using WebApplicationOfNationalParks.Repository.IRepository;

namespace WebApplicationOfNationalParks.Repository
{
    public class NationalParkRepository:Repository<NationalPark>, INationalParkRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public NationalParkRepository(IHttpClientFactory httpClientFactory)
            :base(httpClientFactory) 
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
