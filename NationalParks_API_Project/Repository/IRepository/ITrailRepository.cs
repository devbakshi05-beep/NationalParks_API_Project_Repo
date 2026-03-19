using NationalParks_API_Project.Models;

namespace NationalParks_API_Project.Repository.IRepository
{
    public interface ITrailRepository
    {
        ICollection<Trail> GetTrails();
        Trail GetTrail(int trailId);
        bool TrailExists(int trailId);
        bool TrailExists(string trailName);
        bool CreateTrail(Trail trail);
        bool UpdateTrail(Trail trail);
        bool DeleteTrail(Trail trail);
        bool Save();
        ICollection<Trail> GetTrailsInNationalPark(int nationalParkId);
    }
}
