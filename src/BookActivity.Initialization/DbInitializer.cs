using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.EF;
using BookActivity.Infrastructure.Data.Intefaces;
using BookActivity.Shared.Constants;
using BookActivity.Shared.Helpers;
using Microsoft.AspNetCore.Identity;

namespace BookActivity.Initialization
{
    internal sealed class DbInitializer : IDbInitializer
    {
        public async Task InitializeAsync(BookActivityContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            AddBookCategories(context);
            AddBooks(context);

            await AddUsersAsync(userManager, roleManager);
            await context.SaveChangesAsync();
        }

        private async Task AddUsersAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            AppUser admin = new()
            {
                UserName = "Admin",
                AvatarImage = GetImageData("Man with beard.jpg"),
                Email = "adminBookActivity@gmail.com"
            };
            await userManager.CreateAsync(admin, "adminPassword123#");

            AppRole adminRole = new() { Name = RoleNamesConstants.Admin };
            await roleManager.CreateAsync(adminRole);
            await userManager.AddToRoleAsync(admin, RoleNamesConstants.Admin);

            await userManager.CreateAsync(new AppUser
            {
                UserName = "Nastya",
                AvatarImage = GetImageData("Women red.jpg"),
                Email = "nastyaBookActivity@gmail.com"
            }, "Password123#");

            await userManager.CreateAsync(new AppUser
            {
                UserName = "Nikita",
                AvatarImage = GetImageData("Nikita.jpeg"),
                Email = "nikitaBookActivity@gmail.com"
            }, "Password123#");

            await userManager.CreateAsync(new AppUser
            {
                UserName = "Vlad",
                AvatarImage = GetImageData("Vlad.jpeg"),
                Email = "vladBookActivity@gmail.com"
            }, "Password123#");

            await userManager.CreateAsync(new AppUser
            {
                UserName = "Evelina",
                AvatarImage = GetImageData("Evelina.jpeg"),
                Email = "evelinaBookActivity@gmail.com"
            }, "Password123#");

            await userManager.CreateAsync(new AppUser
            {
                UserName = "Anton",
                AvatarImage = GetImageData("Anton.jpeg"),
                Email = "antonBookActivity@gmail.com"
            }, "Password123#");

            await userManager.CreateAsync(new AppUser
            {
                UserName = "Jeck",
                AvatarImage = GetImageData("Jeck.jpeg"),
                Email = "jeckBookActivity@gmail.com"
            }, "Password123#");

            await userManager.CreateAsync(new AppUser
            {
                UserName = "Tony",
                AvatarImage = GetImageData("Tony.jpeg"),
                Email = "tonyBookActivity@gmail.com"
            }, "Password123#");

            await userManager.CreateAsync(new AppUser
            {
                UserName = "Andrew",
                AvatarImage = GetImageData("Andrew.jpeg"),
                Email = "andrewBookActivity@gmail.com"
            }, "Password123#");
        }

        private void AddBookCategories(BookActivityContext context)
        {
            context.Categories.AddRange(
                new Category("IT"),
                new Category("Philosophy"),
                new Category("Psychology"),
                new Category("Artistic literature"),
                new Category("Fantasy"),
                new Category("Business"));

            context.SaveChanges();
        }

        private void AddBooks(BookActivityContext context)
        {
            context.Books.Add(new Book(
                "CLR VIA C#",
                "Dig deep and master the intricacies of the common language runtime, C#, and .NET development. " +
                "Led by programming expert Jeffrey Richter, a longtime consultant to the Microsoft .NET team - you’ll gain pragmatic " +
                "insights for building robust, reliable, and responsive apps and components.",
                GetImageData("CLR VIA C#.jpg"),
                new[] {
                    new BookAuthor { Author = new Author("Jeffrey", "Richter") }
                },
                new BookCategory { CategoryId = context.Categories.First(c => c.Title == "IT").Id }
            ));

            context.Books.Add(new Book(
                "The House of Wolves",
                "Instant New York Times Bestseller! \r\n\r\n" +
                "James Patterson and Mike Lupica are the thriller dream team! Jenny Wolf’s murdered father leaves her in charge of a billion-dollar empire—and a family more ruthless than Succession's Roys and Yellowstone’s Duttons." +
                "\r\n \r\nThe Wolfs, the most powerful family in California, have a new head–thirty-six-year-old former high school teacher Jenny Wolf. " +
                "\r\n\r\nThat means Jenny now runs the prestigious San Francisco Tribune." +
                "\r\n\r\nShe also controls the legendary pro football team, the Wolves." +
                "\r\n\r\nAnd she has a murdered father to avenge—if she can survive the killers all around her." +
                "\r\n\r\nAn unforgettable family drama by two writers at the top of their craft.",
                GetImageData("The House of Wolves.jpeg"),
                new[] {
                    new BookAuthor { Author = new Author("Mike", "Lupica") },
                    new BookAuthor { Author = new Author("James", "Patterson") },
                },
                new BookCategory { CategoryId = context.Categories.First(c => c.Title == "Artistic literature").Id }
            ));

            context.Books.Add(new Book(
                "I Love You to the Moon and Back",
                "Show a child just how strong your love is every minute of the day! Features a \"To\" and \"From\" personalization page, making this sweet offering an ideal gift for baby showers, birthdays, and new parents. " +
                "\r\n\r\nThe sun rises, and a bear and cub begin their day together. They splash in the water, climb mountains, watch the colorful lights in the shimmering sky, and play with friends. They show their love for each " +
                "other by touching noses, chasing each other, and, of course, hugging and snuggling before bed. A sweet, gentle rhyme, perfect for sharing with a special little one that also includes a “To” and \"" +
                "From” personalization page in the front of the book, making this heartwarming book an ideal gift.",
                GetImageData("I Love You to the Moon and Back.jpg"),
                new[] {
                    new BookAuthor { Author = new Author("Amelia", "Hepworth") }
                },
                new BookCategory { CategoryId = context.Categories.First(c => c.Title == "Psychology").Id },
                new BookCategory { CategoryId = context.Categories.First(c => c.Title == "Philosophy").Id }
            ));

            context.Books.Add(new Book(
                "Spare",
                "t was one of the most searing images of the twentieth century: two young boys, two princes, walking behind their mother’s " +
                "coffin as the world watched in sorrow—and horror. As Princess Diana was laid to rest, billions wondered what Prince William " +
                "and Prince Harry must be thinking and feeling—and how their lives would play out from that point on.",
                GetImageData("Spare.jpeg"),
                new[] {
                    new BookAuthor { Author = new Author("Henry", "Charles") }
                },
                new BookCategory { CategoryId = context.Categories.First(c => c.Title == "Psychology").Id },
                new BookCategory { CategoryId = context.Categories.First(c => c.Title == "Philosophy").Id }
            ));

            context.Books.Add(new Book(
                "The Silent Patient",
                "Alicia Berenson’s life is seemingly perfect. A famous painter married to an in-demand fashion photographer, she lives in a grand house " +
                "with big windows overlooking a park in one of London’s most desirable areas. One evening her husband Gabriel returns home late from a fashion " +
                "shoot, and Alicia shoots him five times in the face, and then never speaks another word.\r\n\r\nAlicia’s refusal to talk, or give any kind of " +
                "explanation, turns a domestic tragedy into something far grander, a mystery that captures the public imagination and casts Alicia into notoriety. " +
                "The price of her art skyrockets, and she, the silent patient, is hidden away from the tabloids and spotlight at the Grove, a secure forensic unit in North London." +
                "\r\n\r\nTheo Faber is a criminal psychotherapist who has waited a long time for the opportunity to work with Alicia. His determination to get her to talk and unravel " +
                "the mystery of why she shot her husband takes him down a twisting path into his own motivations―a search for the truth that threatens to consume him....",
                GetImageData("The Silent Patient.jpeg"),
                new[] {
                    new BookAuthor { Author = new Author("Alex", "Michaelides") }
                },
                new BookCategory { CategoryId = context.Categories.First(c => c.Title == "Artistic literature").Id }
            ));

            context.Books.Add(new Book(
                "The Subtle Art of Not Giving a F*ck: A Counterintuitive",
                "In this generation-defining self-help guide, a superstar blogger cuts through the crap to show us how to stop trying to be \"positive\" all the time so that we can truly become better, happier people.",
                GetImageData("The Subtle Art of Not Giving.jpeg"),
                new[] {
                    new BookAuthor { Author = new Author("Mark", "Manson") }
                },
                new BookCategory { CategoryId = context.Categories.First(c => c.Title == "Artistic literature").Id }
            ));

            context.Books.Add(new Book(
                "A Man Called Ove: A Novel",
                "Meet Ove. He’s a curmudgeon—the kind of man who points at people he dislikes as if they were burglars caught outside his bedroom window. He has staunch principles, strict routines, and a short fuse. " +
                "People call him “the bitter neighbor from hell.” But must Ove be bitter just because he doesn’t walk around with a smile plastered to his face all the time?",
                GetImageData("A Man Called Ove A Novel.jpg"),
                new[] {
                    new BookAuthor { Author = new Author("Henning", "Koch") },
                    new BookAuthor { Author = new Author("Fredrik", "Backman") }
                },
                new BookCategory { CategoryId = context.Categories.First(c => c.Title == "Artistic literature").Id }
            ));

            context.Books.Add(new Book(
                "The Sea-Wolf",
                "The Sea-Wolf is a 1904 psychological adventure novel by American writer Jack London. The book's protagonist, Humphrey van Weyden, is a literary critic who is a survivor of an ocean collision and who comes under " +
                "the dominance of Wolf Larsen, the powerful and amoral sea captain who rescues him.",
                GetImageData("The Sea-Wolf.jpg"),
                new[] {
                    new BookAuthor { Author = new Author("Jack", "London") }
                },
                new BookCategory { CategoryId = context.Categories.First(c => c.Title == "Artistic literature").Id }
            ));

            context.Books.Add(new Book(
                "Lady Tan's Circle of Women: A Novel Kindle Edition",
                "According to Confucius, “an educated woman is a worthless woman,” but Tan Yunxian—born into an elite family, yet haunted by death, separations, and loneliness—is being raised by her grandparents to be of use. " +
                "Her grandmother is one of only a handful of female doctors in China, and she teaches Yunxian the pillars of Chinese medicine, the Four Examinations—looking, listening, touching, and asking—something " +
                "a man can never do with a female patient.",
                GetImageData("Lady Tan's Circle of Women.jpg"),
                new[] {
                    new BookAuthor { Author = new Author("Lisa", "See") }
                },
                new BookCategory { CategoryId = context.Categories.First(c => c.Title == "Artistic literature").Id }
            ));

            context.Books.Add(new Book(
                "Good Night, Irene: A Novel Kindle Edition",
                "This “powerful, uplifting, and deeply personal novel” (Kristin Hannah, #1 NYT bestselling author of The Four Winds), at once “a heart-wrenching wartime drama” (Christina Baker Kline, #1 NYT bestselling author of Orphan Train) " +
                "and “a moving and graceful tribute to heroic women” (Publishers Weekly, starred review), asks the question: What if a friendship forged on the front lines of war defines a life forever?",
                GetImageData("Good Night Irene.jpg"),
                new[] {
                    new BookAuthor { Author = new Author("Luis", "Alberto") }
                },
                new BookCategory { CategoryId = context.Categories.First(c => c.Title == "Artistic literature").Id }
            ));

            context.Books.Add(new Book(
                "The Wager: A Tale of Shipwreck, Mutiny and Murder",
                "On January 28, 1742, a ramshackle vessel of patched-together wood and cloth washed up on the coast of Brazil. Inside were thirty emaciated men, barely alive, and they had an extraordinary tale to tell. They were " +
                "survivors of His Majesty’s Ship the Wager, a British vessel that had left England in 1740 on a secret mission during an imperial war with Spain. While the Wager had been chasing a Spanish treasure-filled galleon known as “the prize of " +
                "all the oceans,” it had wrecked on a desolate island off the coast of Patagonia. The men, after being marooned for months and facing starvation, built the flimsy craft and sailed for more than a hundred days, " +
                "traversing nearly 3,000 miles of storm-wracked seas. They were greeted as heroes.",
                GetImageData("The Wager.jpg"),
                new[] {
                    new BookAuthor { Author = new Author("David", "Grann") }
                },
                new BookCategory { CategoryId = context.Categories.First(c => c.Title == "Artistic literature").Id }
            ));

            context.Books.Add(new Book(
                "King: A Life Kindle Edition",
                "Vividly written and exhaustively researched, Jonathan Eig’s King: A Life is the first major biography in decades of the civil rights icon Martin Luther King Jr.—and the first to include recently declassified FBI files. " +
                "In this revelatory new portrait of the preacher and activist who shook the world, the bestselling biographer gives us an intimate view of the courageous and often emotionally troubled human being who demanded " +
                "peaceful protest for his movement but was rarely at peace with himself. He casts fresh light on the King family’s origins as well as MLK’s complex relationships with his wife, father, and fellow activists. " +
                "King reveals a minister wrestling with his own human frailties and dark moods, a citizen hunted by his own government, and a man determined to fight for justice even if it proved to be a fight to the death. " +
                "As he follows MLK from the classroom to the pulpit to the streets of Birmingham, Selma, and Memphis, Eig dramatically re-creates the journey of a man who recast American race relations and became our only " +
                "modern-day founding father—as well as the nation’s most mourned martyr.",
                GetImageData("King a life.jpg"),
                new[] {
                    new BookAuthor { Author = new Author("Jonathan", "Eig") }
                },
                new BookCategory { CategoryId = context.Categories.First(c => c.Title == "Artistic literature").Id }
            ));
        }

        private byte[] GetImageData(string imageName)
        {
            var imagePath = Path.Combine(AssemblyHelper.CurrentAssemblyDirectory, "Images", imageName);

            return File.ReadAllBytes(imagePath);
        }
    }
}
