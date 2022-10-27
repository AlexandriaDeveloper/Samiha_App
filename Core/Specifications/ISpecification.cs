using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Specifications
{
    public interface ISpecification<T> where T : BaseEntity
    {
        int Take { get; }
        int Skip { get; }
        bool IsPagination { get; }
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, bool>>> Criteries { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
    }
}