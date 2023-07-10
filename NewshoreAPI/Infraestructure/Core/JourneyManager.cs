using Core.Manager.Base;
using Core.Manager.ICore;
using Domain.Models;
using Repository.Base;

namespace Core.Manager.Core
{
    public class JourneyManager : EntityManager<Journey>, IJourneyManager, IEntityManager<Journey>
    {
        public JourneyManager(IUnitOfWork unitOfWork, IRepository<Journey> repository) : base(unitOfWork, repository)
        {
        }
    }
}