using CleanArchitecture.Core.Data;
using CleanArchitecture.Core.Data.Entity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.WASM.Infrastructure
{
    public class LocalStorageDbInitializer
    {
        private readonly IGenericRepository<ArticleCategoryEntity> _categories;
        private readonly IGenericRepository<ArticleEntity> _articles;

        public LocalStorageDbInitializer(
            IGenericRepository<ArticleCategoryEntity> categories,
            IGenericRepository<ArticleEntity> articles)
        {
            _categories = categories;
            _articles = articles;
        }

        public async Task EnsureSeededAsync()
        {
            if ((await _categories.GetAll()).Any())
                return;

            await SeedCategoriesAsync();
            await SeedArticlesAsync();
        }

        private async Task SeedCategoriesAsync()
        {
            var cats = new ArticleCategoryEntity[]
            {
                new() { Id = 1, DateCreated = DateTime.UtcNow, Title = "Mountains" },
                new() { Id = 2, DateCreated = DateTime.UtcNow, Title = "Waterscapes" },
                new() { Id = 3, DateCreated = DateTime.UtcNow, Title = "Seasons" },
                new() { Id = 4, DateCreated = DateTime.UtcNow, Title = "Technique" },
                new() { Id = 5, DateCreated = DateTime.UtcNow, Title = "Nature" },
                new() { Id = 6, DateCreated = DateTime.UtcNow, Title = "Other" },
            };
            foreach (var cat in cats)
                await _categories.Create(cat);
        }

        private async Task SeedArticlesAsync()
        {
            // CategoryId: 1=Mountains, 2=Waterscapes, 3=Seasons, 4=Technique, 5=Nature, 6=Other
            var articles = new ArticleEntity[]
            {
                new()
                {
                    Id = 1, CategoryId = 6,
                    Title = "There Are No Mistakes, Only Happy Accidents",
                    ContentPreview = "In painting — as in life — what looks like a mistake is often the beginning of something beautiful.",
                    Content = @"In painting — as in life — what looks like a mistake is often the beginning of something beautiful. Bob Ross taught us that imperfection is just an invitation to create something unexpected.

Every brushstroke that goes 'wrong' opens a door to a new direction. The tree that leans too far becomes a windswept pine on a clifftop. The sky that muddies becomes a stormy drama above mountain peaks. The secret is not to fight what happens — it's to follow where it leads.

This is the whole philosophy of wet-on-wet oil painting: you commit to the stroke, and you work with what emerges. There is no Ctrl+Z on a canvas, and that is the point. Constraints are the engine of creativity.

Bob spent thirty-one seasons teaching not just painting, but a way of seeing. Every 'happy accident' is really the moment your subconscious takes over from your plan — and your subconscious often knows better.",
                    DateCreated = new DateTime(2026, 4, 20), CreatedBy = "Bob Ross",
                    Tags = "philosophy,painting,mindset", ImageFileName = "",
                },
                new()
                {
                    Id = 2, CategoryId = 1,
                    Title = "Painting the Majestic Peaks of Autumn",
                    ContentPreview = "How to capture snowcaps, pine shadows, and golden hour light.",
                    Content = @"How to capture snowcaps, pine shadows, and golden hour light on canvas.

Start with the sky — always the sky. A warm Cadmium Yellow horizon fading to Phthalo Blue at the top sets the mood for everything below. Let it dry slightly before blocking in the distant mountains with a grey-purple mix. These far peaks should be lighter, cooler, softer — atmospheric perspective does the work.

For the foreground pines, load your fan brush with Sap Green and Van Dyke Brown. Press firmly at the base of each tree and lift toward the tip. One stroke per branch. Resist the urge to go back and fuss. The trees paint themselves.

The snow on the peaks comes last — pure Titanium White applied with the edge of a palette knife. Tap and drag. One pass. The rougher the edge, the more it looks like real snow catching winter light.",
                    DateCreated = new DateTime(2026, 4, 15), CreatedBy = "Bob Ross",
                    Tags = "mountains,painting,autumn", ImageFileName = "",
                },
                new()
                {
                    Id = 3, CategoryId = 4,
                    Title = "The Fan Brush: Secret Weapon for Happy Trees",
                    ContentPreview = "One fan brush loaded with two colors produces an entire forest.",
                    Content = @"One fan brush loaded with two colors produces an entire forest in under three minutes.

Load the fan brush with a dark value on one side (Van Dyke Brown or Alizarin Crimson) and a light value on the other (Cadmium Yellow or Sap Green). The two colors should NOT mix fully on the brush — that gradient is the tree.

Press the brush to the canvas base-first, then pull straight up while slightly twisting your wrist. The dark side becomes shadow, the light side becomes sunlight hitting the leaves. Stack these motions from the ground up, each layer slightly narrower than the last. A full tree in four strokes.

This is one of Bob's most-copied techniques — and for good reason. It turns beginners into confident painters in about sixty seconds.",
                    DateCreated = new DateTime(2026, 4, 10), CreatedBy = "Bob Ross",
                    Tags = "technique,trees,brush", ImageFileName = "",
                },
                new()
                {
                    Id = 4, CategoryId = 2,
                    Title = "Reflections: Painting Still Water in the Forest",
                    ContentPreview = "Still water mirrors the world above. Master reflections.",
                    Content = @"Still water mirrors the world above. Learning to paint reflections turns a good painting into an unforgettable one.

The key insight: reflections are not copies. They are slightly darker, slightly more blurred, and always pulled straight down — never at an angle. The world is reflected as if you laid a mirror flat on the water's surface.

Paint the scene above the water first. Once the upper half is complete, take your fan brush and pull the colors straight downward into the water area with long vertical strokes. The colors blend and soften — that blurring is what sells the illusion.

Then, while everything is still wet, take a clean, dry fan brush and use a single horizontal sweep across the reflection to create the subtle ripple. One stroke. Maybe two.",
                    DateCreated = new DateTime(2026, 4, 5), CreatedBy = "Bob Ross",
                    Tags = "water,reflections,forest", ImageFileName = "",
                },
                new()
                {
                    Id = 5, CategoryId = 3,
                    Title = "Winter White: Snow Scenes from Wet-on-Wet",
                    ContentPreview = "Blend snow into sky seamlessly with the wet-on-wet technique.",
                    Content = @"Blend snow into sky seamlessly with the wet-on-wet technique.

Winter paintings live or die on the relationship between sky and snow. They need to feel like the same cold light is touching both. Wet-on-wet oil painting makes this possible: because the sky is still wet when you add the snow, the edges bleed together naturally.

Start with a cool, light sky — Titanium White tinted with a touch of Phthalo Blue and a whisper of Alizarin Crimson. Work this across the entire canvas, including where the ground will be.

Into this wet ground, drop your snow masses. Load a palette knife with pure Titanium White and apply in thick, flat strokes — ground planes are flat, so the strokes should be horizontal.",
                    DateCreated = new DateTime(2026, 3, 29), CreatedBy = "Bob Ross",
                    Tags = "winter,snow,seasons", ImageFileName = "",
                },
                new()
                {
                    Id = 6, CategoryId = 5,
                    Title = "The Joy of Painting Wildflowers",
                    ContentPreview = "Wildflowers bring life and color to any landscape painting.",
                    Content = @"Wildflowers bring life and color to any landscape painting.

Wildflowers are Bob's favorite finishing touch — a few quick strokes that transform a quiet landscape into something alive. The technique is simple and fast, which is the point.

Load a small round brush with a bright color — Cadmium Yellow, Alizarin Crimson, or Bright Red. Touch the canvas lightly, just the tip of the brush. One dot, then move. Scatter them through the foreground, clustering where they would naturally cluster, thinning where they wouldn't.

The contrast of small bright dots against a dark green or brown ground is what makes them pop. You don't need many. Ten carefully placed flowers outperform fifty random ones.",
                    DateCreated = new DateTime(2026, 3, 22), CreatedBy = "Bob Ross",
                    Tags = "nature,flowers,color", ImageFileName = "",
                },
                new()
                {
                    Id = 7, CategoryId = 1,
                    Title = "How to Paint Dramatic Storm Clouds Over Peaks",
                    ContentPreview = "Storm clouds add emotion and tension to mountain scenery.",
                    Content = @"Storm clouds add emotion and tension to mountain scenery.

A clear sky is beautiful. A storm sky is memorable. The difference is in the dark values you're willing to commit to.

Start with a dark base layer across the sky area — Midnight Black and Prussian Blue mixed. While this is wet, add Titanium White in thick clouds by dabbing and lifting with a crumpled cloth or a large bristle brush. The dark base bleeds into the white as you lift, creating the grey interior of a stormcloud.

A strip of lighter sky at the horizon — Phthalo Blue lightened with white — suggests the storm is passing. This one detail turns a forbidding scene into a hopeful one.",
                    DateCreated = new DateTime(2026, 3, 15), CreatedBy = "Bob Ross",
                    Tags = "mountains,clouds,drama", ImageFileName = "",
                },
                new()
                {
                    Id = 8, CategoryId = 4,
                    Title = "Wet-on-Wet: The Secret Behind Bob's Smooth Skies",
                    ContentPreview = "The wet-on-wet oil technique is what makes Bob's skies magical.",
                    Content = @"The wet-on-wet oil technique is what makes Bob's skies magical.

Wet-on-wet means painting into a still-wet base layer, so colors blend directly on the canvas rather than being mixed on the palette first. Bob used a liquid white base coat on every canvas before starting — this is the secret.

The liquid white coat keeps the entire surface workable for hours, which means every sky color you add blends seamlessly with what's already there. Blue into white gives you soft gradients. Yellow near the horizon melts into blue at the top.

For smooth gradients, use a large, soft-bristle brush and work in long horizontal strokes. You're not applying — you're moving.",
                    DateCreated = new DateTime(2026, 3, 8), CreatedBy = "Bob Ross",
                    Tags = "technique,sky,wet-on-wet", ImageFileName = "",
                },
                new()
                {
                    Id = 9, CategoryId = 2,
                    Title = "Painting a Peaceful Cabin by the Lake",
                    ContentPreview = "A lakeside cabin at dusk is the perfect peaceful scene.",
                    Content = @"A lakeside cabin at dusk is the perfect peaceful scene.

A cabin in a landscape is a promise: someone lives here, someone is warm, the world is small and manageable. That feeling — safety, simplicity, refuge — is what you're painting, not just the structure.

The cabin itself is painted with a palette knife for the walls and a small fan brush for the roof. Keep it simple: two or three planes of color. No elaborate windows — one vertical line of yellow-orange for a lit window is enough to complete the story.

Place the cabin slightly off-center. Let the horizon be low. Give the sky room to breathe. A painting of a cabin by a lake should feel like a deep breath.",
                    DateCreated = new DateTime(2026, 3, 1), CreatedBy = "Bob Ross",
                    Tags = "water,cabin,peace", ImageFileName = "",
                },
            };
            foreach (var article in articles)
                await _articles.Create(article);
        }
    }
}
