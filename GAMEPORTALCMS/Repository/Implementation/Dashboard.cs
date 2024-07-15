using GAMEPORTALCMS.Data;
using GAMEPORTALCMS.Models.DTO;
using GAMEPORTALCMS.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace GAMEPORTALCMS.Repository.Implementation
{
    public class Dashboard
    {

        public readonly DataContext _context;

        public Dashboard(DataContext context)
        {
            this._context = context;
        }


        public async Task<DashboardDTO> GetDashBoardInfo()
        {
         
                List<DashboardDTO> dTOs = new List<DashboardDTO>();

                DashboardDTO data = new DashboardDTO();

                int Customer = _context.tbl_Orders.Count(order => order.Status == true);
                int order = _context.tbl_Orders.Count(order => order.Status == true && order.OrderPlaced == true);
                int Shipment = _context.tbl_Orders.Count(order => order.Status == true && order.OrderShipped == true);
                int Delivery = _context.tbl_Orders.Count(order => order.Status == true && order.Delivered == true);


                data.TotalCustomer = Customer;
                data.TotalOrder = order;
                data.TotalShipment = Shipment;
                data.TotalDelivery = Delivery;
                dTOs.Add(data);

                return data;        

        }

        public async Task<List<Report>> GetReport()
        {
        
            var data = await _context.tbl_Orders
                         .Join(_context.Users,
                             p => p.CreatedBy,
                             q => q.UserId,
                             (p, q) => new
                             {
                                 Order = p,
                                 User = q
                             })
                         .Where(joined => joined.Order.OrderPlaced == true)
                         .Select(joined => new Report
                         {
      
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
                             OrderPlaced = joined.Order.OrderPlaced,
                             OrderShipped = joined.Order.OrderShipped,
                             OrderReceived = joined.Order.OrderReceived,
                             Delivered = joined.Order.Delivered,

                         })
                         .ToListAsync();

            return data;
        }

    }
}
