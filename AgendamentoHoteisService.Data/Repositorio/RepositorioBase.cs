using AgendamentoHoteis.Business.Interfaces;
using AgendamentoHoteis.Business.Models;
using AgendamentoHoteisService.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Linq.Expressions;

namespace AgendamentoHoteisService.Data.Repositorio
{
    public abstract class RepositorioBase<TEntity> : IRepositorioBase<TEntity> where TEntity : Entity, new()
    {
        private readonly IConfiguration _configuration;
        protected readonly AppDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected readonly ConnectionFactory factory;

        protected RepositorioBase(AppDbContext db, IConfiguration configuration)
        {
            _configuration = configuration;
            Db = db;
            DbSet = db.Set<TEntity>();
            factory = new ConnectionFactory()
            {
                HostName = _configuration.GetSection("ConfigRabbitMQ").GetSection("HostName").Value,
                UserName = _configuration.GetSection("ConfigRabbitMQ").GetSection("UserName").Value,
                Password = _configuration.GetSection("ConfigRabbitMQ").GetSection("Password").Value
            };
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(long id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remover(long id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
