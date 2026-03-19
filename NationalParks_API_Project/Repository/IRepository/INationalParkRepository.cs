using NationalParks_API_Project.Models;

namespace NationalParks_API_Project.Repository.IRepository
{
    public interface INationalParkRepository
    {
        ICollection<NationalPark> GetNationalParks(); // Display
        NationalPark GetNationalPark(int nationalParkId); // Find
        bool NationalParkExists(int nationalParkId);
        bool NationalParkExists(string nationalParkName);
        bool CreateNationalPark(NationalPark nationalPark); // means save pe jo value return karega
        bool UpdateNationalPark(NationalPark nationalPark);
        bool DeleteNationalPark(NationalPark nationalPark);
        bool Save();
    }
}
