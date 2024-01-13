using AgendamentoHoteis.Business.Interfaces;
using AgendamentoHoteisService.Data.Repositorio;

namespace AgendamentoHoteis.Service.Configuration
{
    public static class AddRepositoryCollections
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient<IAgendamentoRepositorio, AgendamentoRepositorio>();

            return services;
        }
    }
}
