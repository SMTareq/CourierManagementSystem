using GAMEPORTALCMS.Models.DTO;
using GAMEPORTALCMS.Models.Response;
using GAMEPORTALCMS.Repository.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace GAMEPORTALCMS.Controllers
{
    public class ShipmentController : Controller
    {

        readonly ShipmentRepository _shipmentRepository;

        public ShipmentController(ShipmentRepository repo)
        {
            _shipmentRepository = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async ValueTask<IActionResult> GetShipmentOrderList()
        {

            var data = await _shipmentRepository.GetShipmentOrder();
            return Ok(data);
        }
    
        public async ValueTask<IActionResult> OrderShipment(Order data)
        {
            string userName = HttpContext.Session.GetString("UserName");
            var result = await _shipmentRepository.SaveShipmentOrder(data, userName);
            return new JsonResult(new ResponseModel { Success = result, Message = result == true ? "Added Successfully" : "Something went wrong" });
        }
    }
}
