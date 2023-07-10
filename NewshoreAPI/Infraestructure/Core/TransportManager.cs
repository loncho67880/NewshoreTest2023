using Core.Manager.Base;
using Core.Manager.ICore;
using Domain.Models;
using Repository.Base;

namespace Core.Manager.Core
{
    public class TransportManager : EntityManager<Transport>, ITransportManager, IEntityManager<Transport>
    {
        public TransportManager(IUnitOfWork unitOfWork, IRepository<Transport> repository) : base(unitOfWork, repository)
        {
        }
    }
}