using Allsop.DataAccess.Model;
using Allsop.DataAccess.Repository.Mapper.Base;
using Allsop.Service.Contract.Model;

namespace Allsop.DataAccess.Repository.Mapper
{
    public class CategoryMapper : BaseMapper<Category, CategoryDb>
    {
        protected override void Map(Category model, CategoryDb entity)
        {
            base.Map(model, entity);

            entity.Name = model.Name;
        }

        protected override void Map(CategoryDb entity, Category model)
        {
            base.Map(entity, model);

            model.Name = entity.Name;
        }
    }
}
