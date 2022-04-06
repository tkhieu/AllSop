namespace Allsop.DataAccess
{
    internal static class GenerateTestData
    {
        private static bool isAlreadyGenerateData = false;

        public static void GenerateData(AllsopDbContext dbContext)
        {
            if (isAlreadyGenerateData)
            {
                return;
            }

            isAlreadyGenerateData = true;

            dbContext.Users.AddRange(TestData.Users());
            dbContext.Promotions.AddRange(TestData.Promotions());
            dbContext.Categories.AddRange(TestData.Categories());
            dbContext.Products.AddRange(TestData.Products());
            dbContext.ShoppingCarts.AddRange(TestData.ShoppingCarts());

            dbContext.SaveChanges();
        }
    }
}
