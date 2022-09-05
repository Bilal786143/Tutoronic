using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutoronic.Models
{
    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        [Key]
        public int order_detail_id { get; set; }

        public int course_fid { get; set; }

        public int order_fid { get; set; }

        [Column(TypeName = "numeric")]
        public decimal purchase_price { get; set; }

        [Column(TypeName = "numeric")]
        public decimal sale_price { get; set; }

        public virtual Cours Cours { get; set; }

        public virtual Order Order { get; set; }
    }
}
