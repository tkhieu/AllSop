namespace Allsop.Common.Cache
{
    public class CacheItemModel<TModel>
    {
        public CacheItemModel(TModel data)
        {
            Data = data;
        }

        public TModel Data { get; set; }
    }
}
