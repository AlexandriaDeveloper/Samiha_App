using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.Criteries.Count > 0)
            {
                foreach (var criteria in spec.Criteries)
                {
                    query = query.Where(criteria);
                }
            }


            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
            if (spec.IsPagination)
            {

                if (spec.OrderBy == null && spec.OrderByDescending == null)

                    query = query.OrderBy(x => x.Id).Skip(spec.Skip).Take(spec.Take);
                else
                {
                    query = query.Skip(spec.Skip).Take(spec.Take);
                }



            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            return query;
        }
    }
}