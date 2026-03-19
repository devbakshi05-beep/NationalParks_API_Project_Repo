using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using WebApplicationOfNationalParks.Models;
using WebApplicationOfNationalParks.Models.ViewModels;
using WebApplicationOfNationalParks.Repository.IRepository;

namespace WebApplicationOfNationalParks.Controllers
{
    public class TrailController : Controller
    {
        private readonly ITrailRepository _trailRepository;
        private readonly INationalParkRepository _nationalParkRepository;
        public TrailController(ITrailRepository trailRepository,
            INationalParkRepository nationalParkRepository)
        {
            _trailRepository = trailRepository;
            _nationalParkRepository = nationalParkRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region APIs
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data =await _trailRepository.GetAllAsync(SD.TrailAPIPath) });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status =await _trailRepository.DeleteAsync(SD.TrailAPIPath, id);
            if(status)
                return Json(new { success = true, message = "data deleted successfully !!" });
            return Json(new { success = false, message = "something went wrong while delete data !!!" });

        }
        #endregion
        public async Task<IActionResult> Upsert(int? id)
        {
            var nationalParkList = await _nationalParkRepository.GetAllAsync(SD.NationalParkAPIPath);
            TrailVM trailVM = new TrailVM()
            {
                Trail = new Trail(),
                NationalParkList = nationalParkList.Select(n1 => new SelectListItem()
                {
                    Text = n1.Name,
                    Value = n1.Id.ToString(),
                })
            };
            if (id == null) return View(trailVM);
            trailVM.Trail = await _trailRepository.GetAsync(SD.TrailAPIPath, id.GetValueOrDefault());
            if(trailVM.Trail == null ) return NotFound();
            return View(trailVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TrailVM trailVM)
        {
            if(ModelState.IsValid)
            {
                if(trailVM.Trail.Id == 0)
                  await   _trailRepository.CreateAsync(SD.TrailAPIPath, trailVM.Trail);
                else
                    await _trailRepository.UpdateAsync(SD.TrailAPIPath,trailVM.Trail);
                return RedirectToAction(nameof(Index));
            }
            
            else
            {
                var nationalParkList = await _nationalParkRepository.GetAllAsync(SD.NationalParkAPIPath);
                 trailVM = new TrailVM()
                {
                    Trail = new Trail(),
                    NationalParkList = nationalParkList.Select(n1 => new SelectListItem()
                    {
                        Text = n1.Name,
                        Value = n1.Id.ToString(),
                    })
                };
                return View(trailVM);
            }
        }
    }
}
