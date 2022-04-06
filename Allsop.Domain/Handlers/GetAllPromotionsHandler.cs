using Allsop.Domain.Contract.Queries;
using Allsop.Service.Contract;
using Allsop.Service.Contract.Model;
using MediatR;

namespace Allsop.Domain.Handlers
{
    public class GetAllPromotionsHandler : IRequestHandler<GetAllPromotionsQuery, IEnumerable<Promotion>>
    {
        private readonly IPromotionService _promotionService;

        public GetAllPromotionsHandler(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }
        public Task<IEnumerable<Promotion>> Handle(GetAllPromotionsQuery request, CancellationToken cancellationToken)
            => _promotionService.GetAllPromotionsAsync();
    }
}
