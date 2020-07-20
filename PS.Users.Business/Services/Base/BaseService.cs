using AutoMapper;
using PS.Users.Domain.Interfaces.Repositories.Commands.Base;
using PS.Users.Domain.Interfaces.Repositories.Queries.Base;
using PS.Users.Domain.Interfaces.Services.Base;

namespace PS.Users.Business.Services.Base
{
    public abstract class BaseService<E> : IService<E> where E : class
    {
        protected readonly ICommand<E> Command;
        protected readonly IQuery<E> Query;
        protected readonly IMapper Mapper;

        public BaseService(ICommand<E> command, IQuery<E> query, IMapper mapper)
        {
            Command = command;
            Query = query;
            Mapper = mapper;
        }
    }
}
