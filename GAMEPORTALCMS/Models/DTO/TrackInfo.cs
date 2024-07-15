namespace GAMEPORTALCMS.Models.DTO
{
    public class TrackInfo
    {
        public string? TrackId { get; set; }
        public string? ConsigmentNumber { get; set; }
        public string? CurrentLocation { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public bool Status { get; set; }
    }
}
