using AgendamentoHoteis.Business.Interfaces;
using AgendamentoHoteis.Business.Services;
using AgendamentoHoteis.Service.Configuration;
using AgendamentoHoteisService;
using AgendamentoHoteisService.Data.Context;
using AgendamentoHoteisService.Data.Repositorio;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddScoped<IAgendamentoService, AgendamentoService>();
        services.AddScoped<IAgendamentoRepositorio, AgendamentoRepositorio>();
        IConfiguration config = hostContext.Configuration;

        string mySqlConnection =
              config.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options => 
            options.UseMySql(mySqlConnection,
                      ServerVersion.AutoDetect(mySqlConnection)));

        //services.AddServiceAplication();
        //services.AddRepository();

    })
    .Build();




await host.RunAsync();
