using GAMEPORTALCMS.Repository.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace GAMEPORTALCMS.Controllers
{
    public class DashboardController : Controller
    {

        readonly Dashboard _dashboard;

        public DashboardController(Dashboard dashboard) {

            _dashboard = dashboard;

        }
        public IActionResult Index()
        {

            string userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
            {
              return  RedirectToAction("Index", "Login");
            }
            ViewBag.UserName = userName;
            return View();
        }

      
        public IActionResult ClearSession()
        {
            HttpContext.Session.Clear();
            return Ok("Session cleared.");
        }

        public async ValueTask<IActionResult> GetDashBoardInfo()
        {
            var data = await _dashboard.GetDashBoardInfo();
            return Ok(data);
        }


        public async ValueTask<IActionResult> GetReportList()
        {
            var data = await _dashboard.GetReport();
            return Ok(data);
        }

    }
}
