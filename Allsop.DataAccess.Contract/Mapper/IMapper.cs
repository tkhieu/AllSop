using Allsop.DataAccess.Model.Base;
using Allsop.Service.Contract.Model.Base;

namespace Allsop.DataAccess.Contract.Mapper
{
    public interface IMapper<TModel, TEntity>
        where TModel : BaseModel, new()
        where TEntity : BaseEntityDb, new()
    {
        TModel From(TEntity entity, TModel model);
        TEntity From(TModel model, TEntity entity);
        TModel From(TEntity entity);
        TEntity From(TModel model);
    }
}