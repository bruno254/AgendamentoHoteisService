using AgendamentoHoteis.Business.Interfaces;
using AgendamentoHoteis.Business.Services;

namespace AgendamentoHoteis.Service.Configuration
{
    public static class AddServicesCollections
    {
        public static IServiceCollection AddServiceAplication(this IServiceCollection services)
        {
            services.AddTransient<IAgendamentoService, AgendamentoService>();

            return services;
        }
    }
}
