using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutoronic.Models
{
    [Table("Courses")]
    public partial class Cours
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cours()
        {
            Course_Student_Registration = new HashSet<Course_Student_Registration>();
            Course_teacher_registration = new HashSet<Course_teacher_registration>();
            Course_Video = new HashSet<Course_Video>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int Course_id { get; set; }

        [Required]
        [StringLength(100)]
        public string course_name { get; set; }

        [Required]
        public Boolean approve { get; set; }

        [Required]
        [StringLength(1000)]
        public string course_description { get; set; }

        

        [Required]
        public string course_pic { get; set; }

        [Column(TypeName = "numeric")]
        public decimal course_price { get; set; }

        public int Subcat_fid { get; set; }

        public int? teacher_fid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course_Student_Registration> Course_Student_Registration { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course_teacher_registration> Course_teacher_registration { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course_Video> Course_Video { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public virtual Teacher Teacher { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
