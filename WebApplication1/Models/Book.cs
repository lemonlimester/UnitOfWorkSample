using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public enum BookStatus
    {
        Available,
        OnLoan
    }

    public class Book
    {
        public long BookId { get; set; }
        public string ReferenceNo { get; set; }
        public string Title { get; set; }
        public BookStatus Status { get; set; }
    }
}