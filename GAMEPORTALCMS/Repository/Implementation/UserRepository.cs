using GAMEPORTALCMS.Common;
using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models.DTO;
using GAMEPORTALCMS.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace GAMEPORTALCMS.Repository.Implementation
{
    public class UserRepository
    {
      

        private readonly DataContext _DataContext;

        public UserRepository( DataContext dataContext)
        {
    
            _DataContext = dataContext;
        }

        public async Task<tbl_User> ValidateUser(string userName,string password)
        {
           // CMSUser data = new CMSUser();

            tbl_User data = new tbl_User();
            try
            {
               //  data = await _dbContext.CMSUsers.FirstOrDefaultAsync(x => x.Username == userName && x.Password == password);

                data = await _DataContext.Users.FirstOrDefaultAsync(x => x.UserName == userName && x.Password == password);

            }
            catch (Exception ex)
            {
                string str = "dsa";

            }
            return data;
        }

     


    }
}
