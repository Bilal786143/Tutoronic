using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Tutoronic.Request
{
    public class CreateNewSubCategoryRequest
    {
        public string SubCategoryName { get; set; }
        public string SubCategoryImage { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<SelectList> cat_fid { get; set; }
    }
}