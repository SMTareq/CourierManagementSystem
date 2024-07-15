using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models.DTO;
using GAMEPORTALCMS.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace GAMEPORTALCMS.Repository.Implementation
{
    public class OrderRepository
    {
        private readonly DataContext _dbContext;

        public OrderRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public async Task<GamePatch> GetPatchById(int id)
        //{
        //    return await _dbContext.GamePatchs.FindAsync(id);
        //}

        //public async Task<List<GameType>> GetgameType()
        //{
        //    return await _dbContext.GameTypes.ToListAsync();
        //}


        public async Task<List<tbl_Order>> GetAllOrder()
        {
            var data = await _dbContext.tbl_Orders
                         .Join(_dbContext.Users,
                             p => p.CreatedBy,
                             q => q.UserId,
                             (p, q) => new tbl_Order
                             {
                                 Id = p.Id,
                                 OrderId = p.OrderId,
                                 OrderDate = p.OrderDate,
                                 SenderName = p.SenderName,
                                 SenderAddress = p.SenderAddress,
                                 SenderContact = p.SenderContact,
                                 RecipientName = p.RecipientName,
                                 RecipientAddress = p.RecipientAddress,
                                 RecipientContact =p.RecipientContact,
                                 ParcelDescription =p.ParcelDescription,
                                 EstimatedDeliveryDate = p.EstimatedDeliveryDate,
                                 FromCourier = p.FromCourier,
                                 ToCourier = p.ToCourier
                             })
                         .ToListAsync();
            return data;
        }

        public async Task<bool> SaveOrder(Order cat, string sessinUser)
        {
            bool result = true;
            try
            {

                var ConsigmentNumberl = string.Empty;

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

                    data.FromCourier = cat.FromCourier;
                    data.ToCourier = cat.ToCourier;

                    data.UpdatedBy = sessinUser;
                    data.UpdatedDate = DateTime.Now;
                    await _dbContext.SaveChangesAsync();

                }
                else
                {

                    string orderConsigmentNumber = "MQC" + Generate8DigitNumber();

                    var gmp = new tbl_Order
                    {
                        OrderId = "MORD" + Generate8DigitNumber(),
                        SenderName = cat.SenderName,
                        SenderAddress = cat.SenderAddress,
                        SenderContact = cat.SenderContact,

                        RecipientAddress = cat.RecipientAddress,
                        RecipientContact = cat.RecipientContact,
                        RecipientName = cat.RecipientName,
                        ParcelDescription = cat.ParcelDescription,

                        CreatedBy = sessinUser,
                        CreatedDate = DateTime.Now,

                        ConsigmentNumber = orderConsigmentNumber,

                        FromCourier = cat.FromCourier,
                        ToCourier = cat.FromCourier,
                        OrderDate = DateTime.Now,
                        EstimatedDeliveryDate = cat.EstimatedDeliveryDate,
                        Status = true,
                        OrderPlaced = true,
   
                    };
                    _dbContext.tbl_Orders.Add(gmp);
                    var resultt = await _dbContext.SaveChangesAsync();


                    if (resultt > 0) {

                        var track = new tbl_TrackInfo
                        {
                            ConsigmentNumber = orderConsigmentNumber,
                            TrackId = "MTRT"+ Generate8DigitNumber(),                  
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




        private string Generate8DigitNumber()
        {
            var random = new Random();
            string timeStamp = DateTime.Now.ToString("HHmmssff"); // Using hours, minutes, seconds, and fractional seconds
            int randomNumber = random.Next(0, 100); // Generates a number between 0 and 99
            string uniqueNumber = (int.Parse(timeStamp) + randomNumber).ToString("D8"); // Ensures it's 8 digits

            return uniqueNumber.Substring(0, 8); // Ensures it's exactly 8 digits
        }
    }
}
