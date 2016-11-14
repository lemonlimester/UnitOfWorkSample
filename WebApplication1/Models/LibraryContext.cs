using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class LibraryContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }

        public LibraryContext() : base()
        {
            Configuration.LazyLoadingEnabled = false;
        }
    }
}