using Allsop.Common.Enums;
using Allsop.Common.Exception;
using Allsop.DataAccess.Contract.Repository;
using Allsop.Domain.Services;
using Allsop.Service.Contract;
using Allsop.Service.Contract.Model;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using NUnit.Framework;

namespace Allsop.Domain.Tests.Services
{
    [TestFixture]
    public class PromotionServiceTests
    {
        private IPromotionService _promotionService;
        private Mock<IPromotionRepository> _promotionRepositoryMock;
        private const string _voucher = "20OFFPROMO";
        private Guid _categoryId = Guid.NewGuid();
        private Guid _percentDiscountCategryId = Guid.NewGuid();
        private Guid _amountDiscountCategryId = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            _promotionRepositoryMock = new Mock<IPromotionRepository>();
            _promotionRepositoryMock.Setup(m => m.GetAllPromotionsAsync())
                .Returns(() => Task.FromResult(new List<Promotion>()
                {
                    new Promotion()
                    {
                        Id = Guid.NewGuid(),
                        PromotionType = PromotionType.PromotionCode,
                        Voucher = _voucher,
                        DiscountAmount = 20m,
                        SpendAmount = 100m
                    },
                    new Promotion()
                    {
                        Id = Guid.NewGuid(),
                        PromotionType = PromotionType.Percent,
                        CategoryId = _percentDiscountCategryId,
                        DiscountPercent = 10m,
                        SpendQuantity = 10
                    },
                    new Promotion()
                    {
                        Id = Guid.NewGuid(),
                        PromotionType = PromotionType.Amount,
                        CategoryId = _amountDiscountCategryId,
                        DiscountAmount = 5m,
                        SpendAmount = 50m
                    }
                }.AsEnumerable()));
            _promotionRepositoryMock.Setup(m => m.GetPromotionByVoucherAsync(It.Is<string>(x=> string.Equals(x, _voucher, StringComparison.InvariantCultureIgnoreCase))))
                .Returns(() => Task.FromResult(new Promotion()
                {
                    Id = Guid.NewGuid(),
                    PromotionType = PromotionType.PromotionCode,
                    Voucher = _voucher,
                    DiscountAmount = 20m,
                    SpendAmount = 100m
                }));
            _promotionRepositoryMock.Setup(m => m.GetPromotionByVoucherAsync(It.Is<string>(x => !string.Equals(x, _voucher, StringComparison.InvariantCultureIgnoreCase))))
                .Returns(() => throw new InvalidVoucherException("Invalid Voucher"));
            _promotionService = new PromotionService(_promotionRepositoryMock.Object, new MemoryCache(
                new MemoryCacheOptions()
            ));
        }

        [Test]
        public async Task GetAllPromotions_WithOutParams_ShouldReturnAllPromotionsAsync()
        {
            var promotions = await _promotionService.GetAllPromotionsAsync();

            Assert.IsNotNull(promotions);
            Assert.AreEqual(3, promotions.Count());
        }

        [Test]
        public async Task GetPromotionByVoucher_WithValidVoucher_ShouldReturnPromotionAsync()
        {
            var promotion = await _promotionService.GetPromotionByVoucherAsync(_voucher);

            Assert.IsNotNull(promotion);
            Assert.AreEqual(_voucher, promotion.Voucher);
        }

        [Test]
        public async Task GetPromotionByVoucher_WithInvalidVoucher_ShouldThrowInvalidVoucherExceptionAsync()
        {
            Assert.ThrowsAsync<InvalidVoucherException>(()=> _promotionService.GetPromotionByVoucherAsync("Invalid voucher"));
        }

        [Test]
        public async Task CheckValidVoucher_WithValidVoucher_ShouldReturnTrueAsync()
        {
            var check = await _promotionService.CheckValidVoucherAsync(_voucher);

            Assert.True(check);
        }

        [Test]
        public async Task CheckValidVoucher_WithInvalidVoucher_ShouldThrowInvalidVoucherExceptionAsync()
        {
            Assert.ThrowsAsync<InvalidVoucherException>(() => _promotionService.CheckValidVoucherAsync("Invalid voucher"));
        }

        [Test]
        public async Task GetDiscountAmountByVoucher_WithValidVoucherAndEqualSpendAmount_ShouldReturnDiscountAmountAsync()
        {
            var promotion = new Promotion()
            {
                Id = Guid.NewGuid(),
                PromotionType = PromotionType.PromotionCode,
                Voucher = _voucher,
                DiscountAmount = 20m,
                SpendAmount = 100m
            };

            var discountAmount = await _promotionService.GetDiscountAmountByVoucher(_voucher, 100m, promotion);

            Assert.AreEqual(promotion.DiscountAmount, discountAmount);
        }

        [Test]
        public async Task GetDiscountAmountByVoucher_WithValidVoucherAndMoreThanSpendAmount_ShouldReturnDiscountAmountAsync()
        {
            var promotion = new Promotion()
            {
                Id = Guid.NewGuid(),
                PromotionType = PromotionType.PromotionCode,
                Voucher = _voucher,
                DiscountAmount = 20m,
                SpendAmount = 100m
            };

            var discountAmount = await _promotionService.GetDiscountAmountByVoucher(_voucher, 101m, promotion);

            Assert.AreEqual(promotion.DiscountAmount, discountAmount);
        }

        [Test]
        public async Task GetDiscountAmountByVoucher_WithValidVoucherAndLessThanSpendAmount_ShouldReturnDiscountAmountAsync()
        {
            var promotion = new Promotion()
            {
                Id = Guid.NewGuid(),
                PromotionType = PromotionType.PromotionCode,
                Voucher = _voucher,
                DiscountAmount = 20m,
                SpendAmount = 100m
            };

            var discountAmount = await _promotionService.GetDiscountAmountByVoucher(_voucher, 99m, promotion);

            Assert.AreEqual(0, discountAmount);
        }

        [Test]
        public async Task GetDiscountAmountByVoucher_WithInValidVoucherAndValidSpendAmount_ShouldReturnZeroAmountAsync()
        {
            var promotion = new Promotion()
            {
                Id = Guid.NewGuid(),
                PromotionType = PromotionType.PromotionCode,
                Voucher = _voucher,
                DiscountAmount = 20m,
                SpendAmount = 100m
            };

            var discountAmount = await _promotionService.GetDiscountAmountByVoucher("invalid voucher", 110m, promotion);

            Assert.AreEqual(0, discountAmount);
        }

        [Test]
        public async Task GetDiscountAmountByVoucher_WithInValidVoucherAndInValidSpendAmount_ShouldReturnZeroAsync()
        {
            var promotion = new Promotion()
            {
                Id = Guid.NewGuid(),
                PromotionType = PromotionType.PromotionCode,
                Voucher = _voucher,
                DiscountAmount = 20m,
                SpendAmount = 100m
            };

            var discountAmount = await _promotionService.GetDiscountAmountByVoucher("Invalid voucher", 99m, promotion);

            Assert.AreEqual(0, discountAmount);
        }

        [Test]
        public async Task GetDiscountAmountByPercent_WithValidCategoryAndEqualSpendQuantity_ShouldReturnDiscountPercentAsync()
        {
            var promotion = new Promotion()
            {
                Id = Guid.NewGuid(),
                PromotionType = PromotionType.Percent,
                CategoryId = _percentDiscountCategryId,
                DiscountPercent = 10m,
                SpendQuantity = 10
            };

            var discountPercent = await _promotionService.GetDiscountAmountByPercent(_percentDiscountCategryId, 10, promotion);

            Assert.AreEqual(promotion.DiscountPercent, discountPercent);
        }

        [Test]
        public async Task GetDiscountAmountByPercent_WithValidCategoryAndMoreThanSpendQuantity_ShouldReturnDiscountPercentAsync()
        {
            var promotion = new Promotion()
            {
                Id = Guid.NewGuid(),
                PromotionType = PromotionType.Percent,
                CategoryId = _percentDiscountCategryId,
                DiscountPercent = 10m,
                SpendQuantity = 10
            };

            var discountPercent = await _promotionService.GetDiscountAmountByPercent(_percentDiscountCategryId, 11, promotion);

            Assert.AreEqual(promotion.DiscountPercent, discountPercent);
        }

        [Test]
        public async Task GetDiscountAmountByPercent_WithValidCategoryAndLessThanSpendQuantity_ShouldReturnDiscountPercentAsync()
        {
            var promotion = new Promotion()
            {
                Id = Guid.NewGuid(),
                PromotionType = PromotionType.Percent,
                CategoryId = _percentDiscountCategryId,
                DiscountPercent = 10m,
                SpendQuantity = 10
            };

            var discountPercent = await _promotionService.GetDiscountAmountByPercent(_percentDiscountCategryId, 9, promotion);

            Assert.AreEqual(0, discountPercent);
        }

        [Test]
        public async Task GetDiscountAmountByPercent_WithInvalidCategoryAndValidSpendQuantity_ShouldReturnDiscountPercentAsync()
        {
            var promotion = new Promotion()
            {
                Id = Guid.NewGuid(),
                PromotionType = PromotionType.Percent,
                CategoryId = _percentDiscountCategryId,
                DiscountPercent = 10m,
                SpendQuantity = 10
            };

            var discountPercent = await _promotionService.GetDiscountAmountByPercent(Guid.NewGuid(), 11, promotion);

            Assert.AreEqual(0, discountPercent);
        }

        [Test]
        public async Task GetDiscountAmountByAmount_WithValidCategoryAndEqualSpendAmount_ShouldReturnDiscountAmountAsync()
        {
            var promotion = new Promotion()
            {
                Id = Guid.NewGuid(),
                PromotionType = PromotionType.Amount,
                CategoryId = _amountDiscountCategryId,
                DiscountAmount = 5m,
                SpendAmount = 50m
            };

            var discountAmount = await _promotionService.GetDiscountAmountByAmount(_amountDiscountCategryId, 50m, promotion);

            Assert.AreEqual(promotion.DiscountAmount, discountAmount);
        }

        [Test]
        public async Task GetDiscountAmountByAmount_WithValidCategoryAndMoreThanSpendAmount_ShouldReturnDiscountAmountAsync()
        {
            var promotion = new Promotion()
            {
                Id = Guid.NewGuid(),
                PromotionType = PromotionType.Amount,
                CategoryId = _amountDiscountCategryId,
                DiscountAmount = 5m,
                SpendAmount = 50m
            };

            var discountAmount = await _promotionService.GetDiscountAmountByAmount(_amountDiscountCategryId, 51m, promotion);

            Assert.AreEqual(promotion.DiscountAmount, discountAmount);
        }

        [Test]
        public async Task GetDiscountAmountByAmount_WithValidCategoryAndLessThanSpendAmount_ShouldReturnDiscountAmountAsync()
        {
            var promotion = new Promotion()
            {
                Id = Guid.NewGuid(),
                PromotionType = PromotionType.Amount,
                CategoryId = _amountDiscountCategryId,
                DiscountAmount = 5m,
                SpendAmount = 50m
            };

            var discountAmount = await _promotionService.GetDiscountAmountByAmount(_amountDiscountCategryId, 49m, promotion);

            Assert.AreEqual(0, discountAmount);
        }

        [Test]
        public async Task GetDiscountAmountByAmount_WithInvalidCategoryAndEqualSpendAmount_ShouldReturnDiscountAmountAsync()
        {
            var promotion = new Promotion()
            {
                Id = Guid.NewGuid(),
                PromotionType = PromotionType.Amount,
                CategoryId = _amountDiscountCategryId,
                DiscountAmount = 5m,
                SpendAmount = 50m
            };

            var discountAmount = await _promotionService.GetDiscountAmountByAmount(Guid.NewGuid(), 50m, promotion);

            Assert.AreEqual(0, discountAmount);
        }
    }
}
