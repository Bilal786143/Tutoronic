namespace Tutoronic.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Course_video_comment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course_video_comment()
        {
            Comment_reply = new HashSet<Comment_reply>();
        }

        [Key]
        public int Comment_id { get; set; }

        public int course_vid_fid { get; set; }

        [Required]
        public string comment { get; set; }

        public int std_fid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment_reply> Comment_reply { get; set; }

        public virtual Course_Video Course_Video { get; set; }

        public virtual Student Student { get; set; }
    }
}
