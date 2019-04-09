using CleanArchitecture.Core.Data.Entity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Database
{
    public class ApplicationDbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            // Ensure the database is created before seeding.
            context.Database.EnsureCreated();

            if (!context.Articles.Any())
                await SeedArticles(context);

            if (!context.ArticleCategories.Any())
                await SeedArticleCategories(context);

        }

        private static async Task SeedArticles(ApplicationDbContext context)
        {
            var entities = new ArticleEntity[]
            {
                new ArticleEntity()
                {
                    Title = "Bob Ross is a Boss!",
                    ContentPreview =
                        @"
                        <p>Just go back and put one little more happy tree in there. It's life. It's interesting. It's fun. Isn't that fantastic? There are no mistakes. You can fix anything that happens.</p>
                        <p>This is an example of what you can do with just a few things, a little imagination and a happy dream in your heart. Put it in, leave it alone. We have no limits to our world. We're only limited by our imagination. You can't have light without dark. You can't know happiness unless you've known sorrow. We'll put some happy little leaves here and there.</p>
                        <p>A beautiful little sunset. Work on one thing at a time. Don't get carried away - we have plenty of time. Use what you see, don't plan it. Maybe there's a little something happening right here. Follow the lay of the land. It's most important. Here we're limited by the time we have.</p><p>Let's build some happy little clouds up here. In life you need colors. We don't have to be committed. We are just playing here. Let's give him a friend too. Everybody needs a friend. Everyone is going to see things differently - and that's the way it should be...</p>
                        ",
                    Content =
                        @"
                        <p>Just go back and put one little more happy tree in there. It's life. It's interesting. It's fun. Isn't that fantastic? There are no mistakes. You can fix anything that happens.</p>
                        <p>This is an example of what you can do with just a few things, a little imagination and a happy dream in your heart. Put it in, leave it alone. We have no limits to our world. We're only limited by our imagination. You can't have light without dark. You can't know happiness unless you've known sorrow. We'll put some happy little leaves here and there.</p>
                        <p>A beautiful little sunset. Work on one thing at a time. Don't get carried away - we have plenty of time. Use what you see, don't plan it. Maybe there's a little something happening right here. Follow the lay of the land. It's most important. Here we're limited by the time we have.</p>
                        <p>Let's build some happy little clouds up here. In life you need colors. We don't have to be committed. We are just playing here. Let's give him a friend too. Everybody needs a friend. Everyone is going to see things differently - and that's the way it should be.</p>
                        <p>You don't have to be crazy to do this but it does help. This is your creation - and it's just as unique and special as you are. In this world, everything can be happy. Anything you want to do you can do here.</p>
                        <p>We don't make mistakes we just have happy little accidents. Each highlight must have it's own private shadow. It's amazing what you can do with a little love in your heart.</p>
                        <p>All you need to paint is a few tools, a little instruction, and a vision in your mind. With practice comes confidence. Let's have a happy little tree in here. Just go out and talk to a tree. Make friends with it. When things happen - enjoy them. They're little gifts.</p>
                        ",
                    DateCreated = DateTime.UtcNow,
                    CreatedBy = "BrandonTillman.com",
                    ImageFileName = "bob_ross_boss.png"
                }
            };

            await context.AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }

        private static async Task SeedArticleCategories(ApplicationDbContext context)
        {
            var entities = new ArticleCategoryEntity[]
            {
                new ArticleCategoryEntity()
                {
                    Id = 1,
                    DateCreated = DateTime.UtcNow,
                    Title = "Uncategorized"
                },
                new ArticleCategoryEntity()
                {
                    Id = 2,
                    DateCreated = DateTime.UtcNow,
                    Title = "General News"
                },
                new ArticleCategoryEntity()
                {
                    Id = 3,
                    DateCreated = DateTime.UtcNow,
                    Title = "Cool Technology"
                }
            };

            await context.AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }
    }
}
