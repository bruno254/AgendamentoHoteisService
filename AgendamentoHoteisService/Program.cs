using AgendamentoHoteis.Service.Configuration;
using AgendamentoHoteisService;
using AgendamentoHoteisService.Data.Context;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddServiceAplication();
        services.AddRepository();
        IConfiguration config = hostContext.Configuration;

        string mySqlConnection =
              config.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options => 
            options.UseMySql(mySqlConnection,
                      ServerVersion.AutoDetect(mySqlConnection)));
    })
    .Build();




await host.RunAsync();
