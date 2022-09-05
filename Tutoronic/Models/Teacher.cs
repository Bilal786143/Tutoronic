using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutoronic.Models
{
    [Table("Teacher")]
    public partial class Teacher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Teacher()
        {
            Course_teacher_registration = new HashSet<Course_teacher_registration>();
            Course_Video = new HashSet<Course_Video>();
            Courses = new HashSet<Cours>();
        }

        [Key]
        public int Teacher_id { get; set; }

        [Required]
        [StringLength(500)]
        public string teacher_name { get; set; }

        [Required]
        public string teacher_email { get; set; }

        [Required]
        [StringLength(500)]
        public string teacher_password { get; set; }

        [Required]
        [StringLength(15)]
        public string teacher_contact { get; set; }

        public string teacher_pic { get; set; }

        [StringLength(500)]
        public string teacher_address { get; set; }

        [Required]
        [StringLength(500)]
        public string teacher_intro { get; set; }

        public int total_enrolment { get; set; }

        public virtual Comment_reply Comment_reply { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course_teacher_registration> Course_teacher_registration { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course_Video> Course_Video { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cours> Courses { get; set; }
    }
}
