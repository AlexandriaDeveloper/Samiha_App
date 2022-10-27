using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Specifications
{
    public class DailyWithSpecification : Specification<Daily>
    {

        public DailyWithSpecification(DailyParam param) : base()
        {

            AddInclude(x => x.DailyBoxes);


            //AddInclude();

            if (param.InMonth.HasValue)
            {
                AddCriteries(x => x.DailyDate.Month.Equals(param.InMonth.Value.Month));
            }
            if (param.DailyDate.HasValue)
            {
                AddCriteries(x => x.DailyDate.Equals(param.DailyDate.Value));
            }
            if (!string.IsNullOrEmpty(param.Sort))
            {
                if (param.Sort.Equals("name"))
                {
                    if (param.Order.Equals("asc"))
                        AddOrderBy(x => x.Name);
                    if (param.Order.Equals("desc"))
                        AddOrderByDescending(x => x.Name);

                }
                if (param.Sort.Equals("id"))
                {
                    if (param.Order.Equals("asc"))
                        AddOrderBy(x => x.Id);
                    if (param.Order.Equals("desc"))
                        AddOrderByDescending(x => x.Id);

                }
                if (param.Sort.Equals("dailyDate"))
                {
                    if (param.Order.Equals("asc"))
                        AddOrderBy(x => x.DailyDate);
                    if (param.Order.Equals("desc"))
                        AddOrderByDescending(x => x.DailyDate);

                }
            }
            else
            {

                AddOrderByDescending(x => x.Id);

            }



            if (param.DailyDate.HasValue)
            {
                AddCriteries(x => x.DailyDate.Month.Equals(param.DailyDate.Value.Month));
            }

            if (!string.IsNullOrEmpty(param.Name))
            {
                AddCriteries(x => x.Name.Contains(param.Name));
            }

            if (param.IsPagination)
                ApplyPaging(param.PageIndex * param.PageSize, param.PageSize);

        }
    }

    public class DailyCountAsyncWithSpecification : Specification<Daily>
    {
        public DailyCountAsyncWithSpecification(DailyParam param) : base()
        {



            if (param.InMonth.HasValue)
            {
                AddCriteries(x => x.DailyDate.Month.Equals(param.InMonth.Value.Month));
            }
            if (param.DailyDate.HasValue)
            {
                AddCriteries(x => x.DailyDate.Equals(param.DailyDate.Value));
            }
            if (!string.IsNullOrEmpty(param.Sort))
            {
                if (param.Sort.Equals("name"))
                {
                    if (param.Order.Equals("asc"))
                        AddOrderBy(x => x.Name);
                    if (param.Order.Equals("desc"))
                        AddOrderByDescending(x => x.Name);

                }
                if (param.Sort.Equals("id"))
                {
                    if (param.Order.Equals("asc"))
                        AddOrderBy(x => x.Id);
                    if (param.Order.Equals("desc"))
                        AddOrderByDescending(x => x.Id);

                }
                if (param.Sort.Equals("dailyDate"))
                {
                    if (param.Order.Equals("asc"))
                        AddOrderBy(x => x.DailyDate);
                    if (param.Order.Equals("desc"))
                        AddOrderByDescending(x => x.DailyDate);

                }
            }
            else
            {

                AddOrderByDescending(x => x.Id);

            }



            if (param.DailyDate.HasValue)
            {
                AddCriteries(x => x.DailyDate.Month.Equals(param.DailyDate.Value.Month));
            }

            if (!string.IsNullOrEmpty(param.Name))
            {
                AddCriteries(x => x.Name.Contains(param.Name));
            }
        }
    }
}