using Allsop.DataAccess.Contract.Mapper;
using Allsop.DataAccess.Model.Base;
using Allsop.Service.Contract.Model.Base;

namespace Allsop.DataAccess.Repository.Mapper.Base
{
    public abstract class BaseMapper<TModel, TEntity> : IMapper<TModel, TEntity>
        where TModel : BaseModel, new()
        where TEntity : BaseEntityDb, new()
    {
        protected virtual void Map(TModel model, TEntity entity)
        {
            entity.Id = model.Id;
        }

        protected virtual void Map(TEntity entity, TModel model)
        {
            model.Id = entity.Id;
        }

        public TModel From(TEntity entity)
            => From(entity, null);

        public TEntity From(TModel model)
            => From(model, null);

        public TModel From(TEntity entity, TModel model = null)
        {
            if (entity == null)
            {
                return null;
            }

            if (model == null)
            {
                model = new TModel();
            }
            Map(entity, model);

            return model;
        }
        public TEntity From(TModel model, TEntity entity = null)
        {
            if (model == null)
            {
                return null;
            }

            if (entity == null)
            {
                entity = new TEntity();
            }

            Map(model, entity);

            return entity;
        }
    }
}
