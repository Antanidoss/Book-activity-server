using BookActivity.Domain.Models;
using BookActivity.Infrastructure.Data.Context;
using BookActivity.Infrastructure.Data.Intefaces;
using BookActivity.Shared;
using Microsoft.AspNetCore.Identity;

namespace BookActivity.Initialization
{
    internal sealed class DbInitializer : IDbInitializer
    {
        public async Task InitializeAsync(BookActivityContext context, UserManager<AppUser> userManager)
        {
            AddBooks(context);
            await AddUsersAsync(userManager);

            await context.SaveChangesAsync();
        }

        private async Task AddUsersAsync(UserManager<AppUser> userManager)
        {
            await userManager.CreateAsync(new AppUser
            {
                UserName = "Admin book-activity",
                AvatarImage = GetImageData("Man with beard.jpg"),
                Email = "adminBookActivity@gmail.com"
            }, "adminPassword123#");

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

        private void AddBooks(BookActivityContext context)
        {
            context.Books.Add(new Book(
                "CLR VIA C#",
                "Dig deep and master the intricacies of the common language runtime, C#, and .NET development. " +
                "Led by programming expert Jeffrey Richter, a longtime consultant to the Microsoft .NET team - you’ll gain pragmatic " +
                "insights for building robust, reliable, and responsive apps and components.",
                GetImageData("CLR VIA C#.jpg"),
                new List<BookAuthor>() {
                    new BookAuthor { Author = new Author("Jeffrey", "Richter") }
                }
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
                new List<BookAuthor>() {
                    new BookAuthor { Author = new Author("Mike", "Lupica") },
                    new BookAuthor { Author = new Author("James", "Patterson") },
                }
            ));

            context.Books.Add(new Book(
                "I Love You to the Moon and Back",
                "Show a child just how strong your love is every minute of the day! Features a \"To\" and \"From\" personalization page, making this sweet offering an ideal gift for baby showers, birthdays, and new parents. " +
                "\r\n\r\nThe sun rises, and a bear and cub begin their day together. They splash in the water, climb mountains, watch the colorful lights in the shimmering sky, and play with friends. They show their love for each " +
                "other by touching noses, chasing each other, and, of course, hugging and snuggling before bed. A sweet, gentle rhyme, perfect for sharing with a special little one that also includes a “To” and \"" +
                "From” personalization page in the front of the book, making this heartwarming book an ideal gift.",
                GetImageData("I Love You to the Moon and Back.jpg"),
                new List<BookAuthor>() {
                    new BookAuthor { Author = new Author("Amelia", "Hepworth") }
                }
            ));

            context.Books.Add(new Book(
                "Spare",
                "t was one of the most searing images of the twentieth century: two young boys, two princes, walking behind their mother’s " +
                "coffin as the world watched in sorrow—and horror. As Princess Diana was laid to rest, billions wondered what Prince William " +
                "and Prince Harry must be thinking and feeling—and how their lives would play out from that point on.",
                GetImageData("Spare.jpeg"),
                new List<BookAuthor>() {
                    new BookAuthor { Author = new Author("Henry", "Charles") }
                }
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
                new List<BookAuthor>() {
                    new BookAuthor { Author = new Author("Alex", "Michaelides") }
                }
            ));

            context.Books.Add(new Book(
                "The Subtle Art of Not Giving a F*ck: A Counterintuitive",
                "In this generation-defining self-help guide, a superstar blogger cuts through the crap to show us how to stop trying to be \"positive\" all the time so that we can truly become better, happier people.",
                GetImageData("The Subtle Art of Not Giving.jpeg"),
                new List<BookAuthor>() {
                    new BookAuthor { Author = new Author("Mark", "Manson") }
                }
            ));

            context.Books.Add(new Book(
                "A Man Called Ove: A Novel",
                "Meet Ove. He’s a curmudgeon—the kind of man who points at people he dislikes as if they were burglars caught outside his bedroom window. He has staunch principles, strict routines, and a short fuse. " +
                "People call him “the bitter neighbor from hell.” But must Ove be bitter just because he doesn’t walk around with a smile plastered to his face all the time?",
                GetImageData("A Man Called Ove A Novel.jpg"),
                new List<BookAuthor>() {
                    new BookAuthor { Author = new Author("Henning", "Koch") },
                    new BookAuthor { Author = new Author("Fredrik", "Backman") }
                }
            ));

            context.Books.Add(new Book(
                "The Sea-Wolf",
                "The Sea-Wolf is a 1904 psychological adventure novel by American writer Jack London. The book's protagonist, Humphrey van Weyden, is a literary critic who is a survivor of an ocean collision and who comes under " +
                "the dominance of Wolf Larsen, the powerful and amoral sea captain who rescues him.",
                GetImageData("The Sea-Wolf.jpg"),
                new List<BookAuthor>() {
                    new BookAuthor { Author = new Author("Jack", "London") }
                }
            ));
        }

        private byte[] GetImageData(string imageName)
        {
            var imagePath = Path.Combine(AssemblyHelper.AssemblyDirectory, "Images", imageName);

            return File.ReadAllBytes(imagePath);
        }
    }
}
