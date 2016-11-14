namespace WebApplication1.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication1.Models.LibraryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApplication1.Models.LibraryContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            if (!context.Members.Any())
            {
                context.Members.Add(new Member { FullName = "Peter Jones" });
                context.Members.Add(new Member { FullName = "Ann Smith" });
            }

            if (!context.Books.Any())
            {
                context.Books.Add(new Book { Title = "The Adventures of Huckleberry Finn", ReferenceNo = "A0001", Status = BookStatus.Available });
                context.Books.Add(new Book { Title = "The Adventures of Huckleberry Finn", ReferenceNo = "A0001", Status = BookStatus.Available });
                context.Books.Add(new Book { Title = "The Adventures of Huckleberry Finn", ReferenceNo = "A0001", Status = BookStatus.Available });
                context.Books.Add(new Book { Title = "Pride and Prejudice", ReferenceNo = "A0002", Status = BookStatus.Available });
                context.Books.Add(new Book { Title = "Pride and Prejudice", ReferenceNo = "A0002", Status = BookStatus.Available });
                context.Books.Add(new Book { Title = "The Great Gatsby", ReferenceNo = "A0003", Status = BookStatus.Available });
                context.Books.Add(new Book { Title = "The Great Gatsby", ReferenceNo = "A0003", Status = BookStatus.Available });
                context.Books.Add(new Book { Title = "The Great Gatsby", ReferenceNo = "A0003", Status = BookStatus.Available });
                context.Books.Add(new Book { Title = "Oliver Twist", ReferenceNo = "A0004", Status = BookStatus.Available });
                context.Books.Add(new Book { Title = "Oliver Twist", ReferenceNo = "A0004", Status = BookStatus.Available });
                context.Books.Add(new Book { Title = "Oliver Twist", ReferenceNo = "A0004", Status = BookStatus.Available });
            }
        }
    }
}
