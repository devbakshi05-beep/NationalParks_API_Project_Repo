using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplicationOfNationalParks.Models.ViewModels
{
    public class TrailVM
    {
        public Trail Trail { get; set; }
        public IEnumerable<SelectListItem> NationalParkList { get; set; }
    }
}
