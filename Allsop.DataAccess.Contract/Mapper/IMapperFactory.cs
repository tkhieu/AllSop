using Allsop.DataAccess.Model.Base;
using Allsop.Service.Contract.Model.Base;

namespace Allsop.DataAccess.Contract.Mapper
{
    public interface IMapperFactory
    {
        public IMapper<TDto, TEntityModel> GetMapper<TDto, TEntityModel>()
            where TDto : BaseModel, new()
            where TEntityModel : BaseEntityDb, new();
    }
}
