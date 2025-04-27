using Microsoft.EntityFrameworkCore;
using QwiikAppointmentService.Application.Repositories;
using QwiikAppointmentService.Domain.Common;
using QwiikAppointmentService.Domain.Entities;
using QwiikAppointmentService.EfPostgreSQL.Context;

namespace QwiikAppointmentService.EfPostgreSQL.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected QwiikAppointmentServiceDataContext Context { get; }

        protected BaseRepository(
            QwiikAppointmentServiceDataContext context)
        {
            Context = context;
        }

        public virtual Task<List<TEntity>> GetAll(CancellationToken cancellationToken)
        {
            return Context.Query<TEntity>()
                .Where(x => !x.IsActive)
                .ToListAsync(cancellationToken);
        }

        public virtual Task<TEntity?> Get(int id, CancellationToken cancellationToken)
        {
            var result = Context.Get<TEntity>(id);
            
            if (result is not null && result.IsActive)
            {
                return Task.FromResult<TEntity?>(result);
            }
            return Task.FromResult<TEntity?>(null);

        }

        public virtual void Create(TEntity entity)
        {
            Context.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            Context.Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            Context.Remove(entity);
        }
    }
}
