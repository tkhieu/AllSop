using Allsop.Common.Exception;

namespace Allsop.Common
{
    public class DuplicatedIdException<T> : BaseCustomException
    {
        public DuplicatedIdException(Guid id)
            : base($"The entity of type {typeof(T)} with id {id} already exists", Array.Empty<string>(), ErrorCode.DuplicatedId)
        {
        }
    }
}