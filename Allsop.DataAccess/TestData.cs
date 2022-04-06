using Allsop.Common.Enums;
using Allsop.DataAccess.Model;

namespace Allsop.DataAccess
{
    public static class TestData
    {
        public static IEnumerable<UserDb> Users()
        {
            return new List<UserDb>()
            {
                new UserDb()
                {
                    Id = new Guid("1163ECBA-656F-4A4E-BB81-52E9F81954BA"),
                    UserName = "user1",
                    Password = "password"
                },
                new UserDb()
                {
                    Id = new Guid("3DF98345-AD3E-4ECA-AC83-7BF12FBFF3D2"),
                    UserName = "user2",
                    Password = "password"
                },
                new UserDb()
                {
                    Id = new Guid("DFE72135-05EB-41A3-8C96-26728B0DA83B"),
                    UserName = "user3",
                    Password = "password"
                }
            };
        }

        public static IEnumerable<PromotionDb> Promotions()
        {
           return new List<PromotionDb>()
            {
                new PromotionDb()
                {
                    Id = Guid.NewGuid(),
                    PromotionType = PromotionType.PromotionCode,
                    Voucher = "20OFFPROMO",
                    DiscountAmount = 20m,
                    SpendAmount = 100m
                },
                new PromotionDb()
                {
                    Id = Guid.NewGuid(),
                    PromotionType = PromotionType.Percent,
                    CategoryId = new Guid("28458754-096F-4238-B4BC-98C31D7FCD5A"),
                    DiscountPercent = 10m,
                    SpendQuantity = 10
                },
                new PromotionDb()
                {
                    Id = Guid.NewGuid(),
                    PromotionType = PromotionType.Amount,
                    CategoryId = new Guid("322886CA-D633-4C89-A975-6626C6F32E8A"),
                    DiscountAmount = 5m,
                    SpendAmount = 50m
                }
            };
        }

        public static IEnumerable<CategoryDb> Categories()
        {
            return new List<CategoryDb>()
            {
                new CategoryDb()
                {
                    Id = new Guid("28458754-096F-4238-B4BC-98C31D7FCD5A"),
                    Name = "Drinks"
                },
                new CategoryDb()
                {
                    Id = new Guid("8FD884CD-E251-4195-AF3B-342D86CDE8E4"),
                    Name = "Meat & Poultry"
                },
                new CategoryDb()
                {
                    Id = new Guid("6F75EE04-2B4B-4686-8449-FC6FB1E385A2"),
                    Name = "Fruit & Vegetables"
                },
                new CategoryDb()
                {
                    Id = new Guid("299688E8-EC06-44B0-A17B-A572F7D43A11"),
                    Name = "Confectionary & Desserts"
                },
                new CategoryDb()
                {
                    Id = new Guid("322886CA-D633-4C89-A975-6626C6F32E8A"),
                    Name = "Baking/Cooking Ingredients"
                },
                new CategoryDb()
                {
                    Id = new Guid("27166FB8-B922-4FA4-A4AC-784E1F280168"),
                    Name = "Miscellaneous Items"
                }
            };
        }

        public static IEnumerable<ProductDb> Products()
        {
            return new List<ProductDb>()
            {
                //Drinks
                new ProductDb()
                {
                    Id = new Guid("A5546E3D-7ACE-422E-886B-99F2A91090C7"),
                    Name = "Coca-Cola, 6 x 2L",
                    CategoryId = new Guid("28458754-096F-4238-B4BC-98C31D7FCD5A"),
                    Price = 4.5m,
                    Quantity = 12
                },
                new ProductDb()
                {
                    Id = new Guid("234DF2B9-89E7-4881-9120-A52D3CC0A6E6"),
                    Name = "Still Mineral Water, 6 x 24 x 500ml",
                    CategoryId = new Guid("28458754-096F-4238-B4BC-98C31D7FCD5A"),
                    Price = 45.7m,
                    Quantity = 6
                },
                new ProductDb()
                {
                    Id = new Guid("5F9D8F34-5703-494D-A961-5E6A3147D0DA"),
                    Name = "Chicken Fillets, 6 x 100g",
                    CategoryId = new Guid("28458754-096F-4238-B4BC-98C31D7FCD5A"),
                    Price = 43.2m,
                    Quantity = 8
                },

                //Fruit
                new ProductDb()
                {
                    Id = new Guid("837C78F3-0299-49CE-A8F9-A51FA5D7DEFD"),
                    Name = "Granny Smith Apples, 4 x 16 each Fruit & Vegetables",
                    CategoryId = new Guid("6F75EE04-2B4B-4686-8449-FC6FB1E385A2"),
                    Price = 3.75m,
                    Quantity = 0
                },
                new ProductDb()
                {
                    Id = new Guid("539AA645-8AE8-44AF-83CA-4893E32C2BBA"),
                    Name = "Loose Carrots, 4 x 20 each Fruit & Vegetables",
                    CategoryId = new Guid("6F75EE04-2B4B-4686-8449-FC6FB1E385A2"),
                    Price = 2.67m,
                    Quantity = 2
                },
                new ProductDb()
                {
                    Id = new Guid("5F4C10FF-A692-49A8-AEEB-876D8D845586"),
                    Name = "Mandarin Oranges, 6 x 10 x 12",
                    CategoryId = new Guid("6F75EE04-2B4B-4686-8449-FC6FB1E385A2"),
                    Price = 12.23m,
                    Quantity = 8
                },
                new ProductDb()
                {
                    Id = new Guid("F757F0C0-2CC6-4D70-82C3-71F0EE278190"),
                    Name = "Cauliflower Florets, 10 x 500g",
                    CategoryId = new Guid("6F75EE04-2B4B-4686-8449-FC6FB1E385A2"),
                    Price = 5m,
                    Quantity = 5
                },

                //Meat
                new ProductDb()
                {
                    Id = new Guid("C75CB909-35AA-40EF-BD6F-D60A2205F33A"),
                    Name = "Chicken Fillets, 6 x 100g",
                    CategoryId = new Guid("8FD884CD-E251-4195-AF3B-342D86CDE8E4"),
                    Price = 100,
                    Quantity = 10
                },
                new ProductDb()
                {
                    Id = new Guid("094D5686-F649-4854-8358-A0274DAC7B9F"),
                    Name = "Sirloin Steaks, 4 x 6-8oz",
                    CategoryId = new Guid("8FD884CD-E251-4195-AF3B-342D86CDE8E4"),
                    Price = 4.50m,
                    Quantity = 12
                },
                new ProductDb()
                {
                    Id = new Guid("5B71B066-9E80-4B2C-A7B2-0ED6016DBAE5"),
                    Name = "Whole Free-Range Turkey, 1 x 16-18lbs",
                    CategoryId = new Guid("8FD884CD-E251-4195-AF3B-342D86CDE8E4"),
                    Price = 45.70m,
                    Quantity = 6
                },

                //Confectionary 
                new ProductDb()
                {
                    Id = new Guid("3F2D59F8-5E17-4482-BE22-7ADBF89F4E4B"),
                    Name = "Mars Bar, 6 x 24 x 50g",
                    CategoryId = new Guid("8FD884CD-E251-4195-AF3B-342D86CDE8E4"),
                    Price = 42.82m,
                    Quantity = 4
                },
                new ProductDb()
                {
                    Id = new Guid("87684642-2F7A-4817-963D-8C55C5098705"),
                    Name = "Peppermint Chewing Gum, 6 x 50 x 30g",
                    CategoryId = new Guid("8FD884CD-E251-4195-AF3B-342D86CDE8E4"),
                    Price = 35.70m,
                    Quantity = 6
                },
                new ProductDb()
                {
                    Id = new Guid("50566EFD-4E6B-4BAE-A920-0F0DA24BFE76"),
                    Name = "Strawberry Cheesecake, 4 x 12 portions",
                    CategoryId = new Guid("8FD884CD-E251-4195-AF3B-342D86CDE8E4"),
                    Price = 8.52m,
                    Quantity = 0
                },
                new ProductDb()
                {
                    Id = new Guid("D8BA3ADD-9F58-4F2F-BB41-6CE6220A9F2B"),
                    Name = "Vanilla Ice Cream, 6 x 4L",
                    CategoryId = new Guid("8FD884CD-E251-4195-AF3B-342D86CDE8E4"),
                    Price = 3.8m,
                    Quantity = 2
                },
                //Baking/Cooking Ingredients 
                new ProductDb()
                {
                    Id = new Guid("9A8EDC2B-8362-44AC-8081-C74D2A977F8F"),
                    Name = "Plain Flour, 10 x 1kg",
                    CategoryId = new Guid("322886CA-D633-4C89-A975-6626C6F32E8A"),
                    Price = 42.82m,
                    Quantity = 4
                },
                new ProductDb()
                {
                    Id = new Guid("11F53620-F8AD-4EC8-8711-363987FEB80F"),
                    Name = "Icing Sugar, 12 x 500g",
                    CategoryId = new Guid("322886CA-D633-4C89-A975-6626C6F32E8A"),
                    Price = 35.70m,
                    Quantity = 6
                },
                new ProductDb()
                {
                    Id = new Guid("9321AFC7-0545-4ECA-BB56-23354A2EB77F"),
                    Name = "Free-Range Eggs, 10 x 12 each",
                    CategoryId = new Guid("322886CA-D633-4C89-A975-6626C6F32E8A"),
                    Price = 8.52m,
                    Quantity = 0
                },
                new ProductDb()
                {
                    Id = new Guid("D52E3153-8B4A-4BDD-BCA3-115F6ABFD81E"),
                    Name = "Caster Sugar, 16 x 750g",
                    CategoryId = new Guid("322886CA-D633-4C89-A975-6626C6F32E8A"),
                    Price = 3.8m,
                    Quantity = 2
                }
            }.AsEnumerable();
        }

        public static IEnumerable<ShoppingCartItemDb> ShoppingCartItems()
        {
            return new List<ShoppingCartItemDb>()
            {
                new ShoppingCartItemDb()
                {
                    Id = new Guid("9B6ADB4F-985E-4D4A-A5E5-9AEDBFCC2261"),
                    ProductId = new Guid("A5546E3D-7ACE-422E-886B-99F2A91090C7"), //Product with Category Drinks category
                    ShoppingCartId = new Guid("1148A5AF-F59B-4539-8291-452F542A6E0D"),
                    Quantity = 9
                },
                new ShoppingCartItemDb()
                {
                    Id = new Guid("3F7F3C46-A659-4359-A6B1-51281B0B0AEE"),
                    ProductId = new Guid("9A8EDC2B-8362-44AC-8081-C74D2A977F8F"),//Product with Baking/Cooking Ingredients category
                    ShoppingCartId = new Guid("4F465B3D-8F26-4519-BE5D-FC4E3C2694E0"),
                    Quantity = 1
                },
                new ShoppingCartItemDb()
                {
                    Id = new Guid("5EC1A0D3-B5FC-430D-B7F3-9AFA13DA2725"),
                    ProductId = new Guid("C75CB909-35AA-40EF-BD6F-D60A2205F33A"),
                    ShoppingCartId = new Guid("1D724CEA-ECC2-4242-973C-8E8BF656E40B"),
                    Quantity = 100
                }
            };
        }

        public static IEnumerable<ShoppingCartDb> ShoppingCarts()
        {
           return new List<ShoppingCartDb>()
            { 
                /*Test Use case
                Get 10% off bulk drinks – any drinks are 10% off the listed price (including already reduced 
                items) when buying 10 or more
                */
                new ShoppingCartDb()
                {
                    Id = new Guid("1148A5AF-F59B-4539-8291-452F542A6E0D"),
                    DiscountAmount = 0,
                    UserId = new Guid("1163ECBA-656F-4A4E-BB81-52E9F81954BA"), //user1
                    ShoppingCartItems = new List<ShoppingCartItemDb>()
                    {
                        new ShoppingCartItemDb()
                        {
                            Id = new Guid("9B6ADB4F-985E-4D4A-A5E5-9AEDBFCC2261"),
                            ProductId = new Guid("A5546E3D-7ACE-422E-886B-99F2A91090C7"), //Product with Category Drinks category
                            ShoppingCartId = new Guid("1148A5AF-F59B-4539-8291-452F542A6E0D"),
                            Quantity = 9
                        }
                    }
                },

                /*Test Use case
                £5.00 off your order when spending £50.00 or more on Baking/Cooking Ingredients
                */
                new ShoppingCartDb()
                {
                    Id = new Guid("4F465B3D-8F26-4519-BE5D-FC4E3C2694E0"),
                    DiscountAmount = 0,
                    UserId = new Guid("3DF98345-AD3E-4ECA-AC83-7BF12FBFF3D2"), //user2
                    ShoppingCartItems = new List<ShoppingCartItemDb>()
                    {
                        new ShoppingCartItemDb()
                        {
                            Id = new Guid("3F7F3C46-A659-4359-A6B1-51281B0B0AEE"),
                            ProductId = new Guid("9A8EDC2B-8362-44AC-8081-C74D2A977F8F"),//Product with Baking/Cooking Ingredients category
                            ShoppingCartId = new Guid("4F465B3D-8F26-4519-BE5D-FC4E3C2694E0"),
                            Quantity = 1
                        }
                    }
                },
                /*Test Use case
                £20.00 off your total order value when spending £100.00 or more and using the code 
                20OFFPROMO”
                */
                new ShoppingCartDb()
                {
                    Id = new Guid("1D724CEA-ECC2-4242-973C-8E8BF656E40B"),
                    DiscountAmount = 0,
                    UserId = new Guid("DFE72135-05EB-41A3-8C96-26728B0DA83B"), //user3
                    ShoppingCartItems = new List<ShoppingCartItemDb>()
                    {
                        new ShoppingCartItemDb()
                        {
                            Id = new Guid("5EC1A0D3-B5FC-430D-B7F3-9AFA13DA2725"),
                            ProductId = new Guid("C75CB909-35AA-40EF-BD6F-D60A2205F33A"),
                            ShoppingCartId = new Guid("1D724CEA-ECC2-4242-973C-8E8BF656E40B"),
                            Quantity = 100
                        }
                    }
                }
            };
        }

    }
}
