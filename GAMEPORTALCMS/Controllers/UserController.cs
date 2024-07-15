using GAMEPORTALCMS.Models.DTO;
using GAMEPORTALCMS.Models.Entity;
using GAMEPORTALCMS.Models.Response;
using GAMEPORTALCMS.Repository.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace GAMEPORTALCMS.Controllers
{
    public class UserController : Controller
    {

        UserRepository _userrepository;
        public UserController(UserRepository repo)
        {
            _userrepository = repo;
        }

        public IActionResult UserList()
        {
            return View();
        }


        //public async ValueTask<IActionResult> GetCMSUser()
        //{

        //    var data = await _userrepository.GetAllCMSUser();
        //    return Ok(data);
        //}

        //public async ValueTask<IActionResult> SaveCMSuser(CMSuserDTO data)
        //{
        //    string userName = HttpContext.Session.GetString("UserName");
        //    var result = await _userrepository.SaveCMSUser(data, userName);
        //    return new JsonResult(new ResponseModel { Success = result, Message = result == true ? "Added Successfully" : "Something went wrong" });
        //}


        public IActionResult CMSUserList()
        {
            return View();
        }


     

    }
}
