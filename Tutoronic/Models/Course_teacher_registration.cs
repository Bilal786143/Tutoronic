namespace Tutoronic.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Course_teacher_registration
    {
        [Key]
        public int Course_tch_reg_id { get; set; }

        public int teacher_fid { get; set; }

        public int course_fid { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [Column(TypeName = "numeric")]
        public decimal course_price { get; set; }

        public virtual Cours Cours { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
