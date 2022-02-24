using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Http;
using UsedCar.Backend.Domains.Users;
using UsedCar.Backend.Infrastructures.EntityFrameworkCore;
using UsedCar.Backend.Infrastructures.EntityFrameworkCore.Users;
using UsedCar.Backend.Infrastructures.Idaas;
using UsedCar.Backend.Presentations.Functions.Core.Authorizations;
using UsedCar.Backend.UseCases.Users;

namespace UsedCar.Backend.Presentations.Functions.Users
{
    public class Program
    {
        public static void Main()
        {
#if DEBUG
            HttpClient.DefaultProxy = new WebProxy();
#endif
            IHost? host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults(worker =>
                {
                    //worker.UseNewtonsoftJson();
                    //worker.UseMiddleware<UnhandledExceptionMiddleware>();
                    worker.UseMiddleware<AuthenticationMiddleware>();

                })
                .ConfigureServices(s =>
                {
                    ServiceProvider provider = s.BuildServiceProvider();
                    var configuration = provider.GetRequiredService<IConfiguration>();

                    s.AddTransient<IUserCreateUseCase, UserCreateUseCase>();
                    s.AddTransient<IUserReadUseCase, UserReadUseCase>();
                    s.AddTransient<IUserUpdateUseCase, UserUpdateUseCase>();
                    s.AddTransient<IUserDeleteUseCase, UserDeleteUseCase>();

                    s.AddTransient<IIdaasRepository, IdaasRepository>();
                    s.AddTransient<IUserRepository, UserRepository>();

                    s.AddTransient<IIdaasManagement, IdaasManagement>();


                    s.AddDbContext<UsedCarDBContext>(options =>
                    {
#if DEBUG
                        options.UseSqlServer("Data Source=localhost\\sqlexpress;Integrated Security=True;Initial Catalog=UsedCarDB");
#else
                        options.UseSqlServer(configuration.GetConnectionString("DBContext"));
#endif
                    });
                })
                .Build();

            host.Run();
        }
    }

}