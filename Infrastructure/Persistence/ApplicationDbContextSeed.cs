using Bogus;
using Bogus.DataSets;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUser(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var user = new ApplicationUser { Name = "Tim Ruiz", UserName = "admin", Email = "truiz@test.com" };

            if (userManager.Users.All(u => u.UserName != user.UserName))
            {
                await userManager.CreateAsync(user, "T3st2023*");
                await userManager.AddToRolesAsync(user, new[] { administratorRole.Name });
            }
        }
        public static async Task SeedSampleOwner(ApplicationDbContext context)
        {
            if (!context.Owner.Any())
            {
                var faker = new Faker();
                for(int i = 0; i< 10; i++)
                {
                    var owner = new Owner()
                    {
                        Name = faker.Name.FullName(),
                        Address = faker.Address.FullAddress(),
                        Photo = faker.Image.PicsumUrl(),
                        Birthday = faker.Date.Past()
                    };
                    context.Owner.Add(owner);
                }
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedSampleProperty(ApplicationDbContext context)
        {
            if (!context.Property.Any())
            {
                var faker = new Faker();
                for (int i = 0; i < 10; i++)
                {
                    var property = new Property()
                    {
                        Name = faker.Company.CompanyName(),
                        Address = faker.Address.FullAddress(),
                        Price = faker.Random.Number(1000, 5000),
                        CodeInternal = faker.Random.AlphaNumeric(2),
                        Year = faker.Random.Number(2000, 2023),
                        IdOwner = i + 1
                    };
                    context.Property.Add(property);
                }
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedSamplePropertyImage(ApplicationDbContext context)
        {
            if (!context.PropertyImage.Any())
            {
                var faker = new Faker();
                for (int i = 0; i < 10; i++)
                {
                    var propertyImage = new PropertyImage()
                    {
                        File = faker.Image.PicsumUrl(),
                        Enabled = faker.Random.Bool(),
                        IdProperty = i + 1

                    };
                    context.PropertyImage.Add(propertyImage);
                }
                await context.SaveChangesAsync();
            }
        }


        public static async Task SeedSamplePropertyTrace(ApplicationDbContext context)
        {
            if (!context.PropertyTrace.Any())
            {
                var faker = new Faker();
                for (int i = 0; i < 10; i++)
                {
                    var propertyTrace = new PropertyTrace()
                    {
                        DateSale = faker.Date.Past(),
                        Name = faker.Commerce.ProductName(),
                        Value = faker.Random.Decimal(100000, 500000),
                        Tax = faker.Random.Decimal(10000, 50000),
                        IdProperty = i + 1

                    };
                    context.PropertyTrace.Add(propertyTrace);
                }
                await context.SaveChangesAsync();
            }
        }


        public static async Task SeedSampleData(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await ApplicationDbContextSeed.SeedDefaultUser(userManager, roleManager);
            await ApplicationDbContextSeed.SeedSampleOwner(context);
            await ApplicationDbContextSeed.SeedSampleProperty(context);
            await ApplicationDbContextSeed.SeedSamplePropertyImage(context);
            await ApplicationDbContextSeed.SeedSamplePropertyTrace(context);
        }



    }
}
