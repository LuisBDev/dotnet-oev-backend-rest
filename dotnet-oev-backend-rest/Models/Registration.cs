using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_oev_backend_rest.Models
{
    [Table("tbl_registration")]
    public class Registration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public long UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = string.Empty;

        [Column("registration_date")]
        public DateTime RegistrationDate { get; set; }

        // Navigation Property
        public virtual User User { get; set; } = null!;
    }
}
