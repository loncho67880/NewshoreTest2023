using Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Repository.Base;
using Repository.Core;
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
            //serviceCollection.AddMvc();

            #region DbContext
            // In-memory database only exists while the connection is open
            //Create Database
            sqliteConnection = new SqliteConnection("DataSource=:memory:");
            sqliteConnection.Open();

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<FerContext>()
                    .UseSqlite(sqliteConnection);
            var options = dbContextOptionsBuilder.Options;

            var context = new FerContext(options);
            context.Database.EnsureCreated();
            serviceCollection.AddSingleton<DbContext>(context);
            #endregion DbContext

            serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            #region add identity
            serviceCollection.AddDbContext<FerContext>(op => { op.UseSqlite(sqliteConnection); });
            //serviceCollection.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<HrmContext>().AddDefaultTokenProviders();
            //serviceCollection.AddAuthentication();
            //serviceCollection.AddAuthorization();
            serviceCollection.AddLogging();
            #endregion

            #region Hangfire
            //// HangFire
            //serviceCollection.AddHangfire(configuration => configuration
            //    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //    .UseSimpleAssemblyNameTypeSerializer()
            //    .UseRecommendedSerializerSettings()
            //    .UseMemoryStorage()
            //);
            //GlobalConfiguration.Configuration.UseMemoryStorage();
            //serviceCollection.AddSingleton<PerformContext>(s => new PerformContextMock().Object);
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

            Register.RegisterDI<FerContext>(serviceCollection, configuration);

            serviceProvider = serviceCollection.BuildServiceProvider();
            #region  MOCK IPrincipal Claims User
            //// Creamos el httpContextAccessor
            //var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            //mockHttpContextAccessor
            //    .Setup(_ => _.HttpContext.User)
            //    .Returns(
            //    new ClaimsPrincipal(
            //        new List<ClaimsIdentity>() {
            //            new ClaimsIdentity(new List<Claim>() {
            //                new Claim(ClaimTypes.Name, "Testing"),
            //                new Claim(ClaimTypes.Role, "root"),
            //                new Claim(ClaimTypes.Role, "Nebula.Objetivos.RRHHAdmin"),
            //                new Claim(PlusCustomClaims.UserId, "1")
            //            }) }));

            //// Agregamos el servicio y regeneramos el serviceProvider
            //serviceCollection.AddSingleton<IHttpContextAccessor>(mockHttpContextAccessor.Object);

            //var random = new Random();
            //var alertPort = random.Next(11100, 11300);
            //var notificationPort = random.Next(11100, 11300);

            //serviceCollection.Configure<MailServiceConfiguration>((a) =>
            //{
            //    a.NotificationMail = "notificaciones.informes@gruposese.com";
            //    a.NotificationPassword = "password";
            //    a.NotificationServer = "localhost";
            //    a.NotificationPort = notificationPort;
            //    a.AlertMail = "notificaciones.alertas@gruposese.com";
            //    a.AlertPassword = "password";
            //    a.AlertServer = "localhost";
            //    a.AlertPort = alertPort;
            //    a.EnableSsl = false;
            //    a.DefaultEmail = "test@test.es";
            //});

            //serviceProvider = serviceCollection.BuildServiceProvider();

            #endregion MOCK IPrincipal Claims User
        }

        public void InitDataSql(string file)
        {
            string script = File.ReadAllText(string.Format(@".\Scripts\{0}.sql", file));
            var dbContext = serviceProvider.GetService<FerContext>();
            dbContext?.Database.ExecuteSqlRaw(script);
        }

        [TestCleanup]
        public void CleanUp()
        {
            sqliteConnection.Close();
        }
    }
}
