using GAMEPORTALCMS.Models.DTO;
using GAMEPORTALCMS.Models.Response;
using GAMEPORTALCMS.Repository.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace GAMEPORTALCMS.Controllers
{
    public class OrderController : Controller
    {


        readonly OrderRepository _orderRepository;
        public OrderController(OrderRepository repo)
        {
            _orderRepository = repo;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async ValueTask<IActionResult> GetOrderList()
        {

            var data = await _orderRepository.GetAllOrder();
            return Ok(data);
        }

        //public async ValueTask<IActionResult> GetGameType()
        //{

        //    var data = await _patchService.GetgameType();
        //    return Ok(data);
        //}

        public async ValueTask<IActionResult> SaveOrder(Order data)
        {
            string userName = HttpContext.Session.GetString("UserName");
            var result = await _orderRepository.SaveOrder(data, userName);
            return new JsonResult(new ResponseModel { Success = result, Message = result == true ? "Added Successfully" : "Something went wrong" });
        }
    }
}
