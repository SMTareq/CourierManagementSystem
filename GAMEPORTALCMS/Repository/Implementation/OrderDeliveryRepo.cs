using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models.DTO;
using GAMEPORTALCMS.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GAMEPORTALCMS.Repository.Implementation
{
    public class OrderDeliveryRepo
    {

        private readonly DataContext _dbContext;

        public OrderDeliveryRepo(DataContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<tbl_Order>> GetReceivedOrder()
        {

            var data = await _dbContext.tbl_Orders
                         .Join(_dbContext.Users,
                             p => p.CreatedBy,
                             q => q.UserId,
                             (p, q) => new
                             {
                                 Order = p,
                                 User = q
                             })
                         .Where(joined => joined.Order.OrderReceived == true && joined.Order.Delivered == false)
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

        public async Task<bool> SaveOrderDelivery(Order cat, string sessinUser)
        {
            bool result = true;
            try
            {

                var data = await _dbContext.tbl_Orders.FirstOrDefaultAsync(x => x.Id == cat.Id);
                if (data != null)
                {

                    data.Delivered = true;
                    data.DeliveredDate = DateTime.Now;

                    var resultt = await _dbContext.SaveChangesAsync();

                    if (resultt > 0)
                    {

                        var track = await _dbContext.tbl_Tracks.FirstOrDefaultAsync(x => x.ConsigmentNumber == cat.ConsigmentNumber);
                        if (track != null)
                        {
                            track.Id = 0;
                            track.TrackId = track.TrackId;
                            track.ConsigmentNumber = track.ConsigmentNumber;
                            track.CurrentLocation = "Hand over to the Customer";
                            track.Status = "Order Delivered";
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
