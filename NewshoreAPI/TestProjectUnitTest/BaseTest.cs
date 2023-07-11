
using Automapper.Application;
using Automapper.WebApi.Core.Extensions;
using Core;
using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Repository.Base;
using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "Test Comments")]
[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.ClassLevel)]
namespace UnitTest
{
    public abstract class BaseTest
    {
        protected IServiceProvider serviceProvider;
        protected SqliteConnection sqliteConnection;
        protected ServiceCollection serviceCollection;

        [TestInitialize]
        public void Init()
        {
            serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

            #region DbContext
            // In-memory database only exists while the connection is open
            //Create Database
            sqliteConnection = new SqliteConnection("DataSource=:memory:");
            sqliteConnection.Open();

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<PersistenceContext>()
                    .UseInMemoryDatabase("TestDatabase");
            var options = dbContextOptionsBuilder.Options;

            var context = new PersistenceContext(options);
            context.Database.EnsureCreated();
            serviceCollection.AddSingleton<DbContext>(context);
            #endregion DbContext

            serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            #region add identity
            serviceCollection.AddDbContext<PersistenceContext>(op => { op.UseSqlite(sqliteConnection); });
            serviceCollection.AddLogging();
            #endregion

            //Add appsetting
            var configurationBuilder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = configurationBuilder.Build();

            //Add mock Enviroment
            var mockEnvironment = new Mock<IHostingEnvironment>();
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");

            serviceCollection.AddSingleton<IHostingEnvironment>(mockEnvironment.Object);

            serviceCollection.AddMemoryCache();
            serviceCollection.AddAutoMapperApi(typeof(MapperProfile)); //Automapping

            Register.RegisterDI<PersistenceContext>(serviceCollection, configuration);

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public void InitDataSql(string file)
        {
            string script = File.ReadAllText(string.Format(@".\Scripts\{0}.sql", file));
            var dbContext = serviceProvider.GetService<PersistenceContext>();
            dbContext?.Database.ExecuteSqlRaw(script);
        }

        [TestCleanup]
        public void CleanUp()
        {
            sqliteConnection.Close();
        }
    }
}
