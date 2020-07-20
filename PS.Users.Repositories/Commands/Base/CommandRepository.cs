using PS.Users.Domain.Interfaces.Repositories.Commands.Base;
using PS.Users.Repositories.Contexts;

namespace PS.Users.Repositories.Commands.Base
{
    public class CommandRepository<E> : ICommand<E> where E : class
    {
        protected readonly UsersContext Context;

        public CommandRepository(UsersContext context)
        {
            Context = context;
        }

        public virtual void Insert(E entity)
        {
            Context.Set<E>().Add(entity);
            Context.SaveChanges();
        }
    }
}
