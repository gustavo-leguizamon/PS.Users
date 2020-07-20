namespace PS.Users.Domain.Interfaces.Repositories.Commands.Base
{
    public interface ICommand<E> where E : class
    {
        void Insert(E entity);
    }
}
