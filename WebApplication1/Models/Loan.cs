using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public enum LoanStatus
    {
        Open,
        Close
    }

    public class Loan
    {
        public long LoanId { get; set; }
        
        public LoanStatus Status { get; set; }

        public DateTime LoanDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }

        public long MemberId { get; set; }
        public long BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}