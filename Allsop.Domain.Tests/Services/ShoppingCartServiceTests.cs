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
    public class ShoppingCartServiceTests
    {
        private IShoppingCartService _shoppingCartService;
        private Mock<IShoppingCartRepository> _shoppingCartRepositoryMock;
        private IPromotionService _promotionService;
        private ProductService _productService;
        private ShoppingCartItemService _shoppingCartItemService;
        private Mock<IShoppingCartItemRepository> _shoppingCartItemRepositoryMock;
        private Mock<IPromotionRepository> _promotionRepositoryMock;
        private Mock<IProductRepository> _productRepositoryMock;
        private IMemoryCache _cache;
        private Guid _percentDiscountCategryId = Guid.NewGuid();
        private Guid _amountDiscountCategryId = Guid.NewGuid();
        private Guid _userId = Guid.NewGuid();
        private Guid _productId1 = Guid.NewGuid();
        private Guid _productId2 = Guid.NewGuid();
        private Guid _categoryId = Guid.NewGuid();
        private string _voucher = "20OFFPROMO";

        [SetUp]
        public void Setup()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
            _shoppingCartRepositoryMock = new Mock<IShoppingCartRepository>();
            _shoppingCartRepositoryMock.Setup(m => m.GetShoppingCartByUserIdAsync(It.IsAny<Guid>())).Returns(() => Task.FromResult(new ShoppingCart()
            {
                Id = Guid.NewGuid(),
                UserId = _userId,
                ShoppingCartItems = new List<ShoppingCartItem>()
                {
                    new ShoppingCartItem()
                    {
                        Id = Guid.NewGuid(),
                        ProductId = _productId1,
                        Quantity = 9
                    }
                }
            }));
            _promotionRepositoryMock = new Mock<IPromotionRepository>();
            _promotionRepositoryMock.Setup(m => m.GetAllPromotionsAsync()).Returns(() => Task.FromResult(new List<Promotion>()
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
            _productRepositoryMock = new Mock<IProductRepository>();
            _productRepositoryMock.Setup(m => m.GetProductAsync(It.IsAny<Guid>())).Returns(() =>
                Task.FromResult(new Product()
                {
                    Id = _productId1,
                    Name = "Test",
                    Price = 10,
                    Quantity = 10,
                    CategoryId = _categoryId,
                    Category = new Category() { Id = _categoryId, Name = "Category" }

                }));
            _productRepositoryMock.Setup(m => m.IncreaseQuantity(It.IsAny<Guid>(), It.IsAny<byte>())).Returns(() => Task.FromResult(new Product()
            {
                Id = _productId1,
                Quantity = 1,
                Price = 10m,
                Name = "P1",
                CategoryId = _categoryId,
                Category = new Category() { Id = _categoryId, Name = "test" }

            }));
            _productRepositoryMock.Setup(m => m.DecreaseQuantity(It.IsAny<Guid>(), It.IsAny<byte>())).Returns(() => Task.FromResult(new Product()
            {
                Id = _productId1,
                Quantity = 1,
                Price = 10m,
                Name = "P2",
                CategoryId = _categoryId,
                Category = new Category() { Id = _categoryId, Name = "test" }
            }));
            _promotionService = new PromotionService(_promotionRepositoryMock.Object, _cache);
            _productService = new ProductService(_productRepositoryMock.Object, _cache);
            _shoppingCartItemRepositoryMock = new Mock<IShoppingCartItemRepository>();
            _shoppingCartItemService = new ShoppingCartItemService(_shoppingCartItemRepositoryMock.Object);
            _shoppingCartService = new ShoppingCartService(_shoppingCartRepositoryMock.Object, _shoppingCartItemService, _promotionService, _productService);
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public async Task AddProductToShoppingCart_WithExistingProduct_ShouldAddSuccessfullyAsync()
        {
            var shoppingCart = await _shoppingCartService.AddProductAsync(_productId1, 1, _userId);

            Assert.IsNotNull(shoppingCart);
            Assert.AreEqual(10, shoppingCart.TotalItems);
            Assert.AreEqual(1000, shoppingCart.SubTotal);
            Assert.AreEqual(100, shoppingCart.DiscountAmount);
            Assert.AreEqual(900, shoppingCart.Total);
        }

        [Test]
        public async Task ApplyVoucherToShoppingCart_WithValidVoucher_ShouldApplySuccessfullyAsync()
        {
            var shoppingCart = await _shoppingCartService.ApplyVoucherAsync(_voucher, _userId);

            Assert.IsNotNull(shoppingCart);
            Assert.AreEqual(9, shoppingCart.TotalItems);
            Assert.AreEqual(900, shoppingCart.SubTotal);
            Assert.AreEqual(20, shoppingCart.DiscountAmount);
            Assert.AreEqual(_voucher, shoppingCart.Voucher);
        }

        [Test]
        public async Task ApplyVoucherToShoppingCart_WithInvalidVoucher_ShouldApplyThrowExceptionAsync()
        {
            Assert.ThrowsAsync<VoucherNotFoundException>(() => _shoppingCartService.ApplyVoucherAsync("InvalidVoucher", _userId));
        }

        [Test]
        public async Task AddProductToShoppingCart_PercentDiscount_ShouldUpdateSuccessfullyAsync()
        {
            var shoppingCart = await _shoppingCartService.AddProductAsync(_productId1, 1, _userId);

            Assert.IsNotNull(shoppingCart);
            Assert.AreEqual(1, shoppingCart.ShoppingCartItems.Count);
            Assert.AreEqual(10, shoppingCart.TotalItems);
            Assert.AreEqual(1000, shoppingCart.SubTotal);
            Assert.AreEqual(100, shoppingCart.DiscountAmount);
            Assert.AreEqual(null, shoppingCart.Voucher);
        }

        [Test]
        public async Task RemoveProductOutOfShoppingCart_PercentDiscount_ShouldUpdateSuccessfullyAsync()
        {
            _shoppingCartRepositoryMock.Setup(m => m.GetShoppingCartByUserIdAsync(It.IsAny<Guid>())).Returns(() => Task.FromResult(new ShoppingCart()
            {
                Id = Guid.NewGuid(),
                UserId = _userId,
                DiscountAmount = 10,
                ShoppingCartItems = new List<ShoppingCartItem>()
                {
                    new ShoppingCartItem()
                    {
                        Id = Guid.NewGuid(),
                        ProductId = _productId1,
                        Quantity = 9
                    },
                    new ShoppingCartItem()
                    {
                        Id = Guid.NewGuid(),
                        ProductId = _productId2,
                        Quantity = 1
                    }
                }
            }));

            var shoppingCart = await _shoppingCartService.RemoveProductAsync(_productId2, 1, _userId);

            Assert.IsNotNull(shoppingCart);
            Assert.AreEqual(1, shoppingCart.ShoppingCartItems.Count);
            Assert.AreEqual(9, shoppingCart.TotalItems);
            Assert.AreEqual(900, shoppingCart.SubTotal);
            Assert.AreEqual(0, shoppingCart.DiscountAmount);
            Assert.AreEqual(null, shoppingCart.Voucher);

        }

        [Test]
        public async Task AddProductToShoppingCart_AmountDiscount_ShouldApplySuccessfullyAsync()
        {
            _shoppingCartRepositoryMock.Setup(m => m.GetShoppingCartByUserIdAsync(It.IsAny<Guid>())).Returns(() => Task.FromResult(new ShoppingCart()
            {
                Id = Guid.NewGuid(),
                UserId = _userId,
                ShoppingCartItems = new List<ShoppingCartItem>()
                {
                    new ShoppingCartItem()
                    {
                        Id = Guid.NewGuid(),
                        ProductId = _productId1,
                        Quantity = 9
                    }
                }
            }));

            var shoppingCart = await _shoppingCartService.AddProductAsync(_productId1, 1, _userId);

            Assert.IsNotNull(shoppingCart);
            Assert.AreEqual(1, shoppingCart.ShoppingCartItems.Count);
            Assert.AreEqual(10, shoppingCart.TotalItems);
            Assert.AreEqual(1000, shoppingCart.SubTotal);
            Assert.AreEqual(5, shoppingCart.DiscountAmount);
            Assert.AreEqual(null, shoppingCart.Voucher);
        }

        [Test]
        public async Task RemoveProductOutOfShoppingCart_AmountDiscount_ShouldUpdateSuccessfullyAsync()
        {
            _shoppingCartRepositoryMock.Setup(m => m.GetShoppingCartByUserIdAsync(It.IsAny<Guid>())).Returns(() => Task.FromResult(new ShoppingCart()
            {
                Id = Guid.NewGuid(),
                UserId = _userId,
                DiscountAmount = 5,
                ShoppingCartItems = new List<ShoppingCartItem>()
                {
                    new ShoppingCartItem()
                    {
                        Id = Guid.NewGuid(),
                        ProductId = _productId1,
                        Quantity = 3
                    }
                }
            }));

            var shoppingCart = await _shoppingCartService.RemoveProductAsync(_productId1, 1, _userId);

            Assert.IsNotNull(shoppingCart);
            Assert.AreEqual(1, shoppingCart.ShoppingCartItems.Count);
            Assert.AreEqual(2, shoppingCart.TotalItems);
            Assert.AreEqual(40, shoppingCart.SubTotal);
            Assert.AreEqual(0, shoppingCart.DiscountAmount);
            Assert.AreEqual(null, shoppingCart.Voucher);
        }

        [Test]
        public async Task AddProductToShoppingCart_WithOutOfStock_ShouldThrowExceptionAsync()
        {
            _shoppingCartRepositoryMock.Setup(m => m.GetShoppingCartByUserIdAsync(It.IsAny<Guid>())).Returns(() => Task.FromResult(new ShoppingCart()
            {
                Id = Guid.NewGuid(),
                UserId = _userId,
                DiscountAmount = 0m,
                ShoppingCartItems = new List<ShoppingCartItem>()
                {
                    new ShoppingCartItem()
                    {
                        Id = Guid.NewGuid(),
                        ProductId = _productId1,
                        Quantity = 10
                    }
                }
            }));

            var product = new Product()
            {
                Id = _productId1,
                Quantity = 1,
                Price = 100m,
                Name = "Test",
                CategoryId = _amountDiscountCategryId
            };

            Assert.ThrowsAsync<OutOfStockException>(() => _shoppingCartService.AddProductAsync(_productId1, 1, _userId));
        }
    }
}
