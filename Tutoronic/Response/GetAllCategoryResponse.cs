using System.Collections.Generic;
using Tutoronic.ViewModels;

namespace Tutoronic.Response
{
    public class GetAllCategoryResponse
    {
        public List<CategoryVM> Categories { get; set; }
    }
}