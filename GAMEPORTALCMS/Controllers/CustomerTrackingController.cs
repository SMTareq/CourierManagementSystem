using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Repository.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GAMEPORTALCMS.Controllers
{
    public class CustomerTrackingController : Controller
    {

        readonly CustomerTrackRepository _customerTrackRepository;
        public CustomerTrackingController(CustomerTrackRepository data) {

            _customerTrackRepository = data;
        
        }
        public IActionResult Index()
        {
            return View();
        }

      
        [HttpPost]
        public JsonResult GetTrackingStatus(string trackingNumber)
        {
            var stepsStatus = _customerTrackRepository.GetStepsStatus(trackingNumber);
            return Json(stepsStatus);
        }
    }
}
