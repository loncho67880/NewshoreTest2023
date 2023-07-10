using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

namespace Infrastructure
{
    public class PersistenceContext : DbContext
    {
        private readonly IConfiguration Config;

        public DbSet<Flight> Flight { get; set; }
        public DbSet<Journey> Journey { get; set; }
        public DbSet<Transport> Transport { get; set; }

        public PersistenceContext(DbContextOptions<PersistenceContext> options, IConfiguration config) : base(options)
        {
            this.Config = config;
        }

        public async Task CommitAsync()
        {
            PersistenceContext persistenceContext = this;
            CancellationToken cancellationToken = new CancellationToken();
            ConfiguredTaskAwaitable<int> configuredTaskAwaitable = ((DbContext)persistenceContext).SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            await configuredTaskAwaitable;
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    RelationalModelBuilderExtensions.HasDefaultSchema( .GetValue<string>(this.Config, "SchemaName"));
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}