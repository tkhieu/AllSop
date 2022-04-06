using Allsop.Common;
using Allsop.Common.Exception;
using Allsop.DataAccess.Contract.Mapper;
using Allsop.DataAccess.Model.Base;
using Allsop.Service.Contract.Model.Base;
using Allsop.Service.Contract.Model.Common;
using Microsoft.EntityFrameworkCore;

namespace Allsop.DataAccess.Repository.Repositories.Base
{
    public abstract class BaseRepository
    {
        protected AllsopDbContext DbContext { get; }
        protected IMapperFactory MapperFactory { get; }

        protected BaseRepository(
            AllsopDbContext dbContext,
            IMapperFactory mapperFactory)
        {
            DbContext = dbContext;
            MapperFactory = mapperFactory;
        }

        protected async Task<TModel> GetEntityAsync<TModel, TEntity>(Guid id)
            where TEntity : BaseEntityDb, new()
            where TModel : BaseModel, new()
        {
            var mapper = MapperFactory.GetMapper<TModel, TEntity>();
            var entities = DbContext.Set<TEntity>().AsQueryable();

            var entity = await entities.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                throw new EntityNotFoundException<TEntity>(id);
            }

            return mapper.From(entity);
        }

        protected async Task<IEnumerable<TModel>> GetEntitiesAsync<TModel, TEntity>(IEnumerable<Guid> ids)
            where TEntity : BaseEntityDb, new()
            where TModel : BaseModel, new()
        {
            var mapper = MapperFactory.GetMapper<TModel, TEntity>();
            var entities = DbContext.Set<TEntity>().AsQueryable();

            entities = entities.Where(x => ids.Contains(x.Id));

            return entities.Select(mapper.From).ToList();
        }

        protected async Task<IEnumerable<TModel>> GetAllEntitiesAsync<TModel, TEntity>()
            where TEntity : BaseEntityDb, new()
            where TModel : BaseModel, new()
        {
            var mapper = MapperFactory.GetMapper<TModel, TEntity>();
            var entities = DbContext.Set<TEntity>().AsQueryable();

            entities = entities.AsNoTracking();

            return entities.Select(mapper.From).ToList();
        }

        protected async Task<TModel> CreateEntityAsync<TModel, TEntity>(TModel model)
            where TEntity : BaseEntityDb, new()
            where TModel : BaseModel, new()
        {
            if (model.Id == Guid.Empty)
            {
                throw new InvalidArgumentException("Id", "cannot be null or empty.");
            }

            if (await DbContext.Set<TEntity>().AnyAsync(x => x.Id == model.Id))
            {
                throw new DuplicatedIdException<TEntity>(model.Id);
            }

            var mapper = MapperFactory.GetMapper<TModel, TEntity>();
            var entity = mapper.From(model);

            DbContext.Set<TEntity>().Add(entity);
            await DbContext.SaveChangesAsync();

            return mapper.From(entity);
        }

        protected async Task<IEnumerable<TModel>> CreateEntitiesAsync<TModel, TEntity>(IEnumerable<TModel> models)
            where TEntity : BaseEntityDb, new()
            where TModel : BaseModel, new()
        {
            foreach (var entity in models)
            {
                if (await DbContext.Set<TEntity>().AnyAsync(x => x.Id == entity.Id))
                {
                    throw new DuplicatedIdException<TEntity>(entity.Id);
                }
            }
            var mapper = MapperFactory.GetMapper<TModel, TEntity>();
            var entities = models.Select(x => mapper.From(x));

            DbContext.Set<TEntity>().AddRange(entities);
            await DbContext.SaveChangesAsync();

            return entities.Select(mapper.From).ToList();
        }

        protected async Task<TModel> UpdateEntityAsync<TModel, TEntity>(TModel model)
            where TEntity : BaseEntityDb, new()
            where TModel : BaseModel, new()
        {
            var mapper = MapperFactory.GetMapper<TModel, TEntity>();
            var entity = DbContext.Set<TEntity>().FirstOrDefault(x => x.Id == model.Id);

            DbContext.Entry(entity).CurrentValues.SetValues(model);
            await DbContext.SaveChangesAsync();

            return mapper.From(entity);
        }

        protected async Task<TModel> CreateOrUpdateEntityAsync<TModel, TEntity>(TModel model)
            where TEntity : BaseEntityDb, new()
            where TModel : BaseModel, new()
        {
            if (model.Id == Guid.Empty)
            {
                throw new InvalidArgumentException("Id", "cannot be null or empty.");
            }

            var mapper = MapperFactory.GetMapper<TModel, TEntity>();
            var entity = DbContext.Set<TEntity>().FirstOrDefault(x => x.Id == model.Id);

            if (entity == null)
            {
                entity = mapper.From(model);
                DbContext.Set<TEntity>().Add(entity);
            }
            else
            {
                DbContext.Entry(entity).CurrentValues.SetValues(model);
            }

            await DbContext.SaveChangesAsync();

            return mapper.From(entity);
        }

        protected async Task<DeleteResult> DeleteEntityAsync<TEntity>(Guid id)
            where TEntity : BaseEntityDb
        {
            if (id == Guid.Empty)
            {
                throw new InvalidArgumentException("Id", "Guid cannot be empty");
            }

            var entries = DbContext.Set<TEntity>();
            var entity = await entries.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                throw new EntityNotFoundException<TEntity>(id);
            }

            DbContext.Set<TEntity>().Remove(entity);
            await DbContext.SaveChangesAsync();

            return new DeleteResult()
            {
                Id = id,
                Success = true
            };
        }
    }
}
