using System;
using System.Linq;
using System.Linq.Expressions;

namespace PS.Users.Domain.Interfaces.Repositories.Queries.Base
{
    public interface IQuery<E> where E : class
    {
        bool Exists(Expression<Func<E, bool>> predicate);
        IQueryable<E> FindBy(Expression<Func<E, bool>> predicate, string[] includeProperties = null);
        E FindById(int id);
    }
}
