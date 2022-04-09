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
        private Guid _userId1 = Guid.NewGuid();
        private Guid _userId2 = Guid.NewGuid();
        private Guid _userId3 = Guid.NewGuid();
        private Guid _productId1 = Guid.NewGuid();
        private Guid _productId2 = Guid.NewGuid();
        private Guid _categoryId = Guid.NewGuid();
        private const string _voucher = "20OFFPROMO";

        [SetUp]
        public void Setup()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
            _shoppingCartRepositoryMock = new Mock<IShoppingCartRepository>();
            /*Test Use case
            Get 10% off bulk drinks – any drinks are 10% off the listed price (including already reduced 
            items) when buying 10 or more
            */
            _shoppingCartRepositoryMock.Setup(m => m.GetShoppingCartByUserIdAsync(It.Is<Guid>(x => x == _userId1))).Returns(() => Task.FromResult(new ShoppingCart()
            {
                Id = Guid.NewGuid(),
                UserId = _userId1,
                ShoppingCartItems = new List<ShoppingCartItem>()
                {
                    new ShoppingCartItem()
                    {
                        Id = Guid.NewGuid(),
                        ProductId = _productId1,
                        Price = 100m,
                        Quantity = 9,
                        CategoryId = _percentDiscountCategryId, //Drinks
                    }
                }
            }));
            /*Test Use case
            £5.00 off your order when spending £50.00 or more on Baking/Cooking Ingredients
            */
            _shoppingCartRepositoryMock.Setup(m => m.GetShoppingCartByUserIdAsync(It.Is<Guid>(x => x == _userId2))).Returns(() => Task.FromResult(new ShoppingCart()
            {
                Id = Guid.NewGuid(),
                UserId = _userId2,
                ShoppingCartItems = new List<ShoppingCartItem>()
                {
                    new ShoppingCartItem()
                    {
                        Id = Guid.NewGuid(),
                        ProductId = _productId2,
                        Price = 100m,
                        Quantity = 9,
                        CategoryId = _percentDiscountCategryId, //Baking/Cooking
                    }
                }
            }));
            /*Test Use case
            £20.00 off your total order value when spending £100.00 or more and using the code 
            20OFFPROMO”
            */
            _shoppingCartRepositoryMock.Setup(m => m.GetShoppingCartByUserIdAsync(It.Is<Guid>(x => x == _userId3))).Returns(() => Task.FromResult(new ShoppingCart()
            {
                Id = Guid.NewGuid(),
                UserId = _userId3,
                ShoppingCartItems = new List<ShoppingCartItem>()
                {
                    new ShoppingCartItem()
                    {
                        Id = Guid.NewGuid(),
                        ProductId = _productId1,
                        CategoryId = _percentDiscountCategryId, //Drinks
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
            _productRepositoryMock.Setup(m => m.GetProductAsync(It.Is<Guid>(x => x == _productId1))).Returns(() =>
                  Task.FromResult(new Product()
                  {
                      Id = _productId1,
                      Name = "Product 1",
                      Price = 100m,
                      Quantity = 10,
                      CategoryId = _percentDiscountCategryId,
                      Category = new Category() { Id = _percentDiscountCategryId, Name = "Drinks" }

                  }));
            _productRepositoryMock.Setup(m => m.GetProductAsync(It.Is<Guid>(x => x == _productId2))).Returns(() =>
                Task.FromResult(new Product()
                {
                    Id = _productId2,
                    Name = "Product 2",
                    Price = 100m,
                    Quantity = 10,
                    CategoryId = _amountDiscountCategryId,
                    Category = new Category() { Id = _amountDiscountCategryId, Name = "Baking/Cooking" }

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
            var shoppingCart = await _shoppingCartService.AddProductAsync(_productId1, 1, _userId1);

            Assert.IsNotNull(shoppingCart);
            Assert.AreEqual(10, shoppingCart.TotalItems);
            Assert.AreEqual(1000, shoppingCart.SubTotal);
            Assert.AreEqual(100, shoppingCart.DiscountAmount);
            Assert.AreEqual(900, shoppingCart.Total);
        }

        [Test]
        public async Task ApplyVoucherToShoppingCart_WithValidVoucher_ShouldApplySuccessfullyAsync()
        {
            var shoppingCart = await _shoppingCartService.ApplyVoucherAsync(_voucher, _userId1);

            Assert.IsNotNull(shoppingCart);
            Assert.AreEqual(9, shoppingCart.TotalItems);
            Assert.AreEqual(900, shoppingCart.SubTotal);
            Assert.AreEqual(20, shoppingCart.DiscountAmount);
            Assert.AreEqual(_voucher, shoppingCart.Voucher);
        }

        [Test]
        public async Task ApplyVoucherToShoppingCart_WithInvalidVoucher_ShouldApplyThrowExceptionAsync()
        {
            Assert.ThrowsAsync<InvalidVoucherException>(() => _shoppingCartService.ApplyVoucherAsync("InvalidVoucher", _userId1));
        }

        [Test]
        public async Task AddProductToShoppingCart_PercentDiscount_ShouldUpdateSuccessfullyAsync()
        {
            var shoppingCart = await _shoppingCartService.AddProductAsync(_productId1, 1, _userId1);

            Assert.IsNotNull(shoppingCart);
            Assert.AreEqual(1, shoppingCart.ShoppingCartItems.Count);
            Assert.AreEqual(10, shoppingCart.TotalItems);
            Assert.AreEqual(1000, shoppingCart.SubTotal);
            Assert.AreEqual(100, shoppingCart.DiscountAmount);
            Assert.AreEqual(null, shoppingCart.Voucher);
        }

        [Test]
        public async Task AddProductToShoppingCart_AmountDiscount_ShouldApplySuccessfullyAsync()
        {
            var shoppingCart = await _shoppingCartService.AddProductAsync(_productId2, 1, _userId2);

            Assert.IsNotNull(shoppingCart);
            Assert.AreEqual(1, shoppingCart.ShoppingCartItems.Count);
            Assert.AreEqual(10, shoppingCart.TotalItems);
            Assert.AreEqual(1000, shoppingCart.SubTotal);
            Assert.AreEqual(100, shoppingCart.DiscountAmount);
            Assert.AreEqual(null, shoppingCart.Voucher);
        }

        [Test]
        public async Task RemoveProductOutOfShoppingCart_AmountDiscount_ShouldUpdateSuccessfullyAsync()
        {
            var userId = Guid.NewGuid();
            _shoppingCartRepositoryMock.Setup(m => m.GetShoppingCartByUserIdAsync(It.Is<Guid>(x => x == userId))).Returns(() => Task.FromResult(new ShoppingCart()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                DiscountAmount = 100m,
                ShoppingCartItems = new List<ShoppingCartItem>()
                {
                    new ShoppingCartItem()
                    {
                        Id = Guid.NewGuid(),
                        ProductId = _productId1,
                        Price = 100m,
                        Quantity = 10,
                        CategoryId = _percentDiscountCategryId
                    }
                }
            }));

            var shoppingCart = await _shoppingCartService.RemoveProductAsync(_productId1, 1, userId);

            Assert.IsNotNull(shoppingCart);
            Assert.AreEqual(1, shoppingCart.ShoppingCartItems.Count);
            Assert.AreEqual(9, shoppingCart.TotalItems);
            Assert.AreEqual(900, shoppingCart.SubTotal);
            Assert.AreEqual(0, shoppingCart.DiscountAmount);
            Assert.AreEqual(null, shoppingCart.Voucher);
        }

        [Test]
        public async Task AddProductToShoppingCart_WithOutOfStock_ShouldThrowExceptionAsync()
        {
            Assert.ThrowsAsync<OutOfStockException>(() => _shoppingCartService.AddProductAsync(_productId1, 11, _userId1));
        }
    }
}
