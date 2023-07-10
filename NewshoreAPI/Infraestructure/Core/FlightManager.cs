using Core.Manager.Base;
using Core.Manager.ICore;
using Domain.Models;
using Repository.Base;

namespace Core.Manager.Core
{
    public class FlightManager : EntityManager<Flight>, IFlightManager, IEntityManager<Flight>
    {
        public FlightManager(IUnitOfWork unitOfWork, IRepository<Flight> repository) : base(unitOfWork, repository)
        {
        }
    }
}