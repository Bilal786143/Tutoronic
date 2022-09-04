namespace Tutoronic.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Admin")]
    public partial class Admin
    {
        [Key]
        public int Admin_id { get; set; }

        [Required]
        [StringLength(50)]
        public string admin_name { get; set; }

        [Required]
        public string admin_email { get; set; }

        [Required]
        [StringLength(500)]
        public string admin_password { get; set; }

        [Required]
        public string admin_pic { get; set; }

        [Required]
        [StringLength(500)]
        public string admin_adress { get; set; }

        [Required]
        [StringLength(15)]
        public string admin_contact { get; set; }
    }
}
