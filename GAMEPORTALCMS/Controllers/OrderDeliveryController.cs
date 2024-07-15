using GAMEPORTALCMS.Models.DTO;
using GAMEPORTALCMS.Models.Response;
using GAMEPORTALCMS.Repository.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace GAMEPORTALCMS.Controllers
{
    public class OrderDeliveryController : Controller
    {

        readonly OrderDeliveryRepo _orderDeliveryRepo;
        public OrderDeliveryController(OrderDeliveryRepo repo)
        {
            _orderDeliveryRepo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async ValueTask<IActionResult> GetReceivedOrderList()
        {

            var data = await _orderDeliveryRepo.GetReceivedOrder();
            return Ok(data);
        }

        public async ValueTask<IActionResult> SaveOrderDelivery(Order data)
        {
            string userName = HttpContext.Session.GetString("UserName");
            var result = await _orderDeliveryRepo.SaveOrderDelivery(data, userName);
            return new JsonResult(new ResponseModel { Success = result, Message = result == true ? "Added Successfully" : "Something went wrong" });
        }
    }
}
