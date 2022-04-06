using Allsop.DataAccess.Contract.Mapper;
using Allsop.DataAccess.Model.Base;
using Allsop.Service.Contract.Model.Base;

namespace Allsop.DataAccess.Repository.Mapper
{
    public class MapperFactory : IMapperFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public MapperFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IMapper<TDto, TEntityModel> GetMapper<TDto, TEntityModel>()
            where TDto : BaseModel, new()
            where TEntityModel : BaseEntityDb, new()
        {
            var mapperType = typeof(IMapper<TDto, TEntityModel>);
            var mapper = _serviceProvider.GetService(mapperType) as IMapper<TDto, TEntityModel>;
            if (mapper == null)
            {
                throw new Exception($"No mapper found for <{typeof(TDto)},{typeof(TEntityModel)}> ");
            }

            return mapper;
        }
    }
}
