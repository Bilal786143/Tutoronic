using System.Web.Mvc;

namespace Tutoronic.Services.Interface
{
    public interface IBaseDropDownList
    {
        SelectList DropDownList(int? primaryId);
    }
}
