using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tutoronic.Request
{
    public class CreateNewCourseRequest
    {
        public int CourseId { get; set; }

        [Required]
        [StringLength(100)]
        public string CourseName { get; set; }

        [Required]
        [StringLength(1000)]
        public string CourseDescription { get; set; }

        [Required]
        public string CourseImage { get; set; }

        public decimal CoursePrice { get; set; }

        public int SubCategoryId { get; set; }
        public virtual IEnumerable<SelectList> Subcat_fid { get; set; }
    }
}