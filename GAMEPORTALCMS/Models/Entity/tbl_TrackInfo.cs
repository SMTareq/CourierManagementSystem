using System.ComponentModel.DataAnnotations.Schema;

namespace GAMEPORTALCMS.Models.Entity
{

    [Table("tbl_TrackInfo", Schema = "dbo")]
    public class tbl_TrackInfo : BaseEntity<int>
    {
        public string? TrackId { get; set; }
        public string? ConsigmentNumber { get; set; }
        public string? CurrentLocation { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public string? Status { get; set; }
    }
}
