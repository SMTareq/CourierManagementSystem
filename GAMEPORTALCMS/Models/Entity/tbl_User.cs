using System.ComponentModel.DataAnnotations.Schema;

namespace GAMEPORTALCMS.Models.Entity
{

    [Table("tbl_User", Schema = "dbo")]
    public class tbl_User:BaseEntity<int>
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set;}
        public bool IsActive { get; set; }
    }
}
