using Core.Models;

namespace Core.Specifications
{
    public class FormWithSpecification : Specification<Form>
    {
        public FormWithSpecification(FormParam parm) : base()
        {

            if (parm.DailyBoxId.HasValue)
                AddCriteries(x => x.DailyBoxId == parm.DailyBoxId);
            if (!string.IsNullOrEmpty(parm.Num224))
                AddCriteries(x => x.Num224.Equals(parm.Num224));
            if (!string.IsNullOrEmpty(parm.Name))
                AddCriteries(x => x.Name.Contains(parm.Name));

        }
    }
    public class FormCountAsyncWithSpecification : Specification<Form>
    {
        public FormCountAsyncWithSpecification(FormParam parm) : base()
        {

            if (parm.DailyBoxId.HasValue)
                AddCriteries(x => x.DailyBoxId == parm.DailyBoxId);
            if (!string.IsNullOrEmpty(parm.Num224))
                AddCriteries(x => x.Num224.Equals(parm.Num224));
            if (!string.IsNullOrEmpty(parm.Name))
                AddCriteries(x => x.Name.Contains(parm.Name));


        }
    }
}