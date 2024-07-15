using GAMEPORTALCMS.Models.DTO;
using GAMEPORTALCMS.Models.Response;
using GAMEPORTALCMS.Repository.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace GAMEPORTALCMS.Controllers
{
    public class OrderReceiveController : Controller
    {

        readonly OrderReceivedRepo _orderReceivedRepo;

        public OrderReceiveController(OrderReceivedRepo repo)
        {
            _orderReceivedRepo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async ValueTask<IActionResult> GetReceivedOrderList()
        {

            var data = await _orderReceivedRepo.GetReceivedOrder();
            return Ok(data);
        }

        public async ValueTask<IActionResult> SaveReceivedOrder(Order data)
        {
            string userName = HttpContext.Session.GetString("UserName");
            var result = await _orderReceivedRepo.SaveOrderReceived(data, userName);
            return new JsonResult(new ResponseModel { Success = result, Message = result == true ? "Added Successfully" : "Something went wrong" });
        }

    }
}
