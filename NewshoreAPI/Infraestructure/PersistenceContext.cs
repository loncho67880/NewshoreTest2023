using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

namespace Infrastructure
{
    public class PersistenceContext : DbContext
    {

        public DbSet<Flight> Flight { get; set; }
        public DbSet<Journey> Journey { get; set; }
        public DbSet<Transport> Transport { get; set; }

        public PersistenceContext(DbContextOptions<PersistenceContext> options) : base(options)
        {
        }

        public async Task CommitAsync()
        {
            PersistenceContext persistenceContext = this;
            CancellationToken cancellationToken = new CancellationToken();
            ConfiguredTaskAwaitable<int> configuredTaskAwaitable = ((DbContext)persistenceContext).SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            await configuredTaskAwaitable;
        }
    }
}