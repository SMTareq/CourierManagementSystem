using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models.DTO;
using GAMEPORTALCMS.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace GAMEPORTALCMS.Repository.Implementation
{
    public class ShipmentRepository
    {

        private readonly DataContext _dbContext;

        public ShipmentRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

     
        public async Task<List<tbl_Order>> GetShipmentOrder()
        {



          //  && joined.Order.OrderShipped == false

            var data = await _dbContext.tbl_Orders
                         .Join(_dbContext.Users,
                             p => p.CreatedBy,
                             q => q.UserId,
                             (p, q) => new
                             {
                                 Order = p,
                                 User = q
                             })
                         .Where(joined => joined.Order.OrderPlaced == true && joined.Order.OrderShipped == false)
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

        public async Task<bool> SaveShipmentOrder(Order cat, string sessinUser)
        {          
            try
            {
                bool result = true;
                var data = await _dbContext.tbl_Orders.FirstOrDefaultAsync(x => x.Id == cat.Id);
                if (data != null)
                {        
                    data.OrderShipped = true;
                    data.OrderShippeddate = DateTime.Now;
                    var resultt =  await _dbContext.SaveChangesAsync();
                    if (resultt > 0)
                    {

                        var track = await _dbContext.tbl_Tracks.FirstOrDefaultAsync(x => x.ConsigmentNumber == cat.ConsigmentNumber);

                        if (track != null)
                        {
                            track.Id = 0;
                            track.TrackId = track.TrackId;
                            track.ConsigmentNumber = track.ConsigmentNumber;
                            track.CurrentLocation = "By Road";
                            track.Status = "In Shipment";
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
