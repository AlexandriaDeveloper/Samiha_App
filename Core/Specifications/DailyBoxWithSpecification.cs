namespace Core.Specifications
{
    public class DailyBoxWithSpecification : Specification<Models.DailyBoxes>
    {
        public DailyBoxWithSpecification(DailyBoxParam param) : base()
        {

            if (param.Id.HasValue)
            {
                AddCriteries(x => x.Id == param.Id);
            }

            if (param.DailyId.HasValue)
            {
                AddCriteries(x => x.DailyId.Equals(param.DailyId.Value));
            }
            if (param.BoxId.HasValue)
            {
                AddCriteries(x => x.BoxId == param.BoxId.Value);
            }
            if (!string.IsNullOrEmpty(param.Name))
            {
                AddCriteries(x => x.Name == param.Name);
            }

            if (param.CollageId.HasValue)
            {
                AddInclude(x => x.Box.Collage);
                AddCriteries(x => x.Box.Collage.Id == param.CollageId);
            }
            if (param.BoxId.HasValue)
            {
                AddInclude(x => x.Box);
                AddCriteries(x => x.Box.Id == param.BoxId);
            }

            if (param.IsPagination)
                ApplyPaging(param.PageIndex * param.PageSize, param.PageSize);

        }
    }
    public class DailyBoxCountAsyncWithSpecification : Specification<Models.DailyBoxes>
    {
        public DailyBoxCountAsyncWithSpecification(DailyBoxParam param) : base()
        {

            if (param.Id.HasValue)
            {
                AddCriteries(x => x.Id == param.Id);
            }

            if (param.DailyId.HasValue)
            {
                AddCriteries(x => x.DailyId.Equals(param.DailyId.Value));
            }
            if (param.BoxId.HasValue)
            {
                AddCriteries(x => x.BoxId == param.BoxId.Value);
            }
            if (!string.IsNullOrEmpty(param.Name))
            {
                AddCriteries(x => x.Name == param.Name);
            }

            if (param.CollageId.HasValue)
            {
                AddInclude(x => x.Box.Collage);
                AddCriteries(x => x.Box.Collage.Id == param.CollageId);
            }
            if (param.BoxId.HasValue)
            {
                AddInclude(x => x.Box);
                AddCriteries(x => x.Box.Id == param.BoxId);
            }

        }
    }
}