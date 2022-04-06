namespace Allsop.Common.Exception
{
    public class EntityNotFoundException<T> : BaseCustomException
    {
        public Type Type { get; }

        public EntityNotFoundException(Guid id)
            : base($"The entity of type {typeof(T)} with id {id} does not exist", Array.Empty<string>(), ErrorCode.InvalidId)
        {
            Type = typeof(T);
        }

        public EntityNotFoundException()
            : base($"The requested entity of type {typeof(T)} does not exist", Array.Empty<string>(), ErrorCode.InvalidId)
        {
            Type = typeof(T);
        }
    }
}
