using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models.DTO;
using GAMEPORTALCMS.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace GAMEPORTALCMS.Repository.Implementation
{
    public class CustomerTrackRepository
    {

        private readonly DataContext _dbContext;

        public CustomerTrackRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<tbl_Order>> GetShipmentOrder()
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
                         .Where(joined => joined.Order.OrderPlaced == true)
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

                         })
                         .ToListAsync();

            return data;
        }

        public async Task<bool> SaveOrder(Order cat, string sessinUser)
        {
            bool result = true;
            try
            {


                var data = await _dbContext.tbl_Orders.FirstOrDefaultAsync(x => x.Id == cat.Id);
                if (data != null)
                {

                    data.SenderName = cat.SenderName;
                    data.SenderAddress = cat.SenderAddress;
                    data.SenderContact = cat.SenderContact;

                    data.RecipientAddress = cat.RecipientAddress;
                    data.RecipientContact = cat.RecipientContact;
                    data.RecipientName = cat.RecipientName;
                    data.ParcelDescription = cat.ParcelDescription;

                    data.FromCourier = 1;
                    data.ToCourier = 1;

                    data.UpdatedBy = sessinUser;
                    data.UpdatedDate = DateTime.Now;
                    var resultt = await _dbContext.SaveChangesAsync();

                    if (resultt > 0)
                    {

                        var track = new tbl_TrackInfo
                        {

                            CurrentLocation = cat.SenderAddress,
                            //   EstimatedDeliveryDate = cat.EstimatedDeliveryDate,
                            Status = "In Order",
                        };

                        _dbContext.tbl_Tracks.Add(track);
                        await _dbContext.SaveChangesAsync();
                    }
                }

                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }


        public bool[] GetStepsStatus(string trackingNumber)
        {
   
            var trackingInfo = _dbContext.tbl_Orders
               .Where(t => t.ConsigmentNumber == trackingNumber)
               .FirstOrDefault();

            if (trackingInfo != null)
            {
                return new bool[]
                {
                trackingInfo.OrderPlaced,
                trackingInfo.OrderShipped,
                trackingInfo.OrderReceived,
                trackingInfo.Delivered
                };
            }

            return new bool[0]; // Return an empty array if tracking number is not found
        }
    }
}
