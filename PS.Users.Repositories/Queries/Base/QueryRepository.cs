using Microsoft.EntityFrameworkCore;
using PS.Users.Domain.Interfaces.Repositories.Queries.Base;
using PS.Users.Repositories.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PS.Users.Repositories.Queries.Base
{
    public class QueryRepository<E> : IQuery<E> where E : class
    {
        protected readonly UsersContext Context;

        public QueryRepository(UsersContext context)
        {
            Context = context;
        }

        public bool Exists(Expression<Func<E, bool>> predicate)
        {
            IQueryable<E> query = Context.Set<E>();
            return query.Any(predicate);
        }

        public virtual IQueryable<E> FindBy(Expression<Func<E, bool>> predicate, string[] includeProperties = null)
        {
            IQueryable<E> query = Context.Set<E>();

            if (includeProperties != null)
                foreach (string includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }

            return query.Where(predicate);
        }

        public virtual E FindById(int id)
        {
            return Context.Set<E>().Find(id);
        }
    }
}
