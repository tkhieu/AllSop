namespace Allsop.DataAccess
{
    internal static class GenerateTestData
    {
        private static bool hasGeneratePrudct = false;
        private static bool hasGeneratePromotion = false;
        private static bool hasGenerateShoppingCart = false;

        public static void GenerateProductCategoryData(ProductCatalogDbContext dbContext)
        {
            if (hasGeneratePrudct)
            {
                return;
            }

            hasGeneratePrudct = true;

            dbContext.Categories.AddRange(TestData.Categories());
            dbContext.Products.AddRange(TestData.Products());

            dbContext.SaveChanges();
        }

        public static void GeneratePromotionData(PriceCalculationDbContext dbContext)
        {
            if (hasGeneratePromotion)
            {
                return;
            }

            hasGeneratePromotion = true;

            dbContext.Promotions.AddRange(TestData.Promotions());
            
            dbContext.SaveChanges();
        }

        public static void GenerateShoppingCartData(ShoppingCartDbContext dbContext)
        {
            if (hasGenerateShoppingCart)
            {
                return;
            }

            hasGenerateShoppingCart = true;

            dbContext.Users.AddRange(TestData.Users());
            dbContext.ShoppingCarts.AddRange(TestData.ShoppingCarts());

            dbContext.SaveChanges();
        }
    }
}
