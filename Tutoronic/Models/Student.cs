using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutoronic.Models
{
    [Table("Student")]
    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            Course_Student_Registration = new HashSet<Course_Student_Registration>();
            Course_video_comment = new HashSet<Course_video_comment>();
            Orders = new HashSet<Order>();
        }

        [Key]
        public int Student_id { get; set; }

        [Required]
        [StringLength(500)]
        public string student_name { get; set; }

        [Required]
        public string student_email { get; set; }

        [Required]
        [StringLength(50)]
        public string student_password { get; set; }

        [Required]
        [StringLength(15)]
        public string student_contact { get; set; }

        [Required]
        public string student_pic { get; set; }

        public int total_enrolment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course_Student_Registration> Course_Student_Registration { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course_video_comment> Course_video_comment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
