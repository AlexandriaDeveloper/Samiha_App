using System.Linq;
using Core.Models;

namespace Core.Specifications
{
    // public class SearchWithSpecification : Specification<Form>
    // {
    //     public SearchWithSpecification(SearchParam parm) : base()
    //     {
    //         if (parm.DailyDate.HasValue)
    //         {
    //             AddInclude(x => x.DailyBoxes.Daily);
    //             AddCriteries(x => x.Daily.DailyDate == parm.DailyDate);
    //         }
    //         if (string.IsNullOrEmpty(parm.FileName))
    //         {
    //             AddInclude(x => x.Forms);
    //             AddCriteries(x => { });
    //         }
    //     }
    // }

}