using Tutoronic.Models;

namespace Tutoronic.Response
{

    public class EditSubCategoryByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}