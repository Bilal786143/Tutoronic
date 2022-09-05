using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tutoronic.Models
{
    public partial class Course_Video
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course_Video()
        {
            Course_video_comment = new HashSet<Course_video_comment>();
        }

        [Key]
        public int Course_vid_id { get; set; }

        public int course_fid { get; set; }

        public int teacher_fid { get; set; }

        [Required]
        public string video { get; set; }

        [Required]
        [StringLength(100)]
        public string video_title { get; set; }

        [Required]
        [StringLength(1000)]
        public string video_description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course_video_comment> Course_video_comment { get; set; }

        public virtual Cours Cours { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
