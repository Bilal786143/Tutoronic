namespace Tutoronic.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Course_Student_Registration
    {
        [Key]
        public int Std_reg_id { get; set; }

        public int std_fid { get; set; }

        public int course_fid { get; set; }

        [Column(TypeName = "numeric")]
        public decimal course_price { get; set; }

        public virtual Cours Cours { get; set; }

        public virtual Student Student { get; set; }
    }
}
