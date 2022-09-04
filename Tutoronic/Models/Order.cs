namespace Tutoronic.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int order_id { get; set; }

        [Required]
        [StringLength(50)]
        public string Customer_Name { get; set; }

        [Required]
        public string Customer_Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Payment_Method { get; set; }

        [Required]
        [StringLength(50)]
        public string Customer_Phone { get; set; }

        [Required]
        public string Customer_Address { get; set; }

        [Required]
        [StringLength(50)]
        public string Order_Type { get; set; }

        public DateTime Order_Date { get; set; }

        public int Student_fid { get; set; }

        public virtual Student Student { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
