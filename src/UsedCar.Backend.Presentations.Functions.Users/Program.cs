using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Http;
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
                   //worker.UseMiddleware<AuthenticationMiddleware>();

                })
                //.ConfigureOpenApi()
                .ConfigureServices(s =>
                {
                    ServiceProvider provider = s.BuildServiceProvider();
                    IConfiguration configuration = provider.GetRequiredService<IConfiguration>();

                    s.AddTransient<IUserCreateUseCase, UserCreateUseCase>();


//                    s.AddDbContext<MsprDBContext>(options =>
//                    {
//#if DEBUG
//                    options.UseSqlServer("Data Source=localhost\\sqlexpress;Integrated Security=True;Initial Catalog=UsedCarDB");
//#else
//                    options.UseSqlServer(configuration.GetConnectionString("DBContext"));
//#endif
//                });
                })
                .Build();

            host.Run();
        }
    }

}