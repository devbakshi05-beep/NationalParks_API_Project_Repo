using NationalParks_API_Project.Data;
using NationalParks_API_Project.Models;
using NationalParks_API_Project.Repository.IRepository;

namespace NationalParks_API_Project.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext _context;
        public NationalParkRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _context.NationalParks.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _context.NationalParks.Remove(nationalPark);
            return Save();
        }

        public NationalPark GetNationalPark(int nationalParkId)
        {
            return _context.NationalParks.Find(nationalParkId);
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _context.NationalParks.ToList(); 
        }

        public bool NationalParkExists(int nationalParkId)
        {
            return _context.NationalParks.Any(n=>n.Id == nationalParkId);
        }

        public bool NationalParkExists(string nationalParkName)
        {
            return _context.NationalParks.Any(n => n.Name == nationalParkName);
        }

        public bool Save()
        {
            return _context.SaveChanges()==1?true:false;
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            _context.NationalParks.Update(nationalPark);
            return Save();
        }
    }
}
