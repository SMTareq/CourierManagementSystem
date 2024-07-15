namespace GAMEPORTALCMS.Models.DTO
{
    public class Report
    {
        public string? OrderId { get; set; }
        public string? SenderName { get; set; }
        public string? SenderAddress { get; set; }
        public string? SenderContact { get; set; }
        public string? RecipientName { get; set; }
        public string? RecipientAddress { get; set; }
        public string? RecipientContact { get; set; }
        public string? ConsigmentNumber { get; set; }
        public string? ParcelDescription { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public bool OrderPlaced { get; set; }
        public bool OrderShipped { get; set; }
        public bool Delivered { get; set; }
        public bool OrderReceived { get; set; }
   
    }
}
