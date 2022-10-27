using Core.Models;

namespace Core.Specifications
{
    public class BoxWithSpecification : Specification<Box>
    {
        public BoxWithSpecification(BoxParam parma) : base()
        {
            if (parma.CollageId.HasValue)
                AddCriteries(x => x.CollageId == parma.CollageId);

        }
    }

    public class BoxCountAsyncWithSpecification : Specification<Box>
    {
        public BoxCountAsyncWithSpecification(BoxParam parma) : base()
        {
            if (parma.CollageId.HasValue)
                AddCriteries(x => x.CollageId == parma.CollageId);

        }
    }

}