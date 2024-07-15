using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models.DTO;
using GAMEPORTALCMS.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace GAMEPORTALCMS.Repository.Implementation
{
    public class OrderReceivedRepo
    {
        private readonly DataContext _dbContext;

        public OrderReceivedRepo(DataContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<tbl_Order>> GetReceivedOrder()
        {
            // add tut tut
            var data = await _dbContext.tbl_Orders
                         .Join(_dbContext.Users,
                             p => p.CreatedBy,
                             q => q.UserId,
                             (p, q) => new
                             {
                                 Order = p,
                                 User = q
                             })
                         .Where(joined => joined.Order.OrderShipped == true && joined.Order.OrderReceived == false)
                         .Select(joined => new tbl_Order
                         {
                             Id = joined.Order.Id,
                             OrderId = joined.Order.OrderId,
                             OrderDate = joined.Order.OrderDate,
                             SenderName = joined.Order.SenderName,
                             SenderAddress = joined.Order.SenderAddress,
                             SenderContact = joined.Order.SenderContact,
                             RecipientName = joined.Order.RecipientName,
                             RecipientAddress = joined.Order.RecipientAddress,
                             RecipientContact = joined.Order.RecipientContact,
                             ParcelDescription = joined.Order.ParcelDescription,
                             ConsigmentNumber = joined.Order.ConsigmentNumber,

                         })
                         .ToListAsync();

            return data;
        }

        public async Task<bool> SaveOrderReceived(Order cat, string sessinUser)
        {
            bool result = true;
            try
            {


                var data = await _dbContext.tbl_Orders.FirstOrDefaultAsync(x => x.Id == cat.Id);
                var address = data.RecipientAddress;
                if (data != null)
                {

                    data.OrderReceived = true;
                    data.OrderDate = DateTime.Now;
     
                    var resultt = await _dbContext.SaveChangesAsync();

                    if (resultt > 0)
                    {
                        var track = await _dbContext.tbl_Tracks.FirstOrDefaultAsync(x => x.ConsigmentNumber == cat.ConsigmentNumber);
                    
                        if (track != null)
                        {
                            track.Id = 0;
                            track.TrackId = track.TrackId;
                            track.ConsigmentNumber = track.ConsigmentNumber;
                            track.CurrentLocation = address;
                            track.Status = "Shipment Received";
                            track.CreatedBy = sessinUser;
                            track.CreatedDate = DateTime.Now;
                            _dbContext.tbl_Tracks.Add(track);
                            await _dbContext.SaveChangesAsync();
                        }

                    }
                }

                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
