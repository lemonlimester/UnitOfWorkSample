using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/bookloan")]
    public class BookLoanController : ApiController
    {
        private IBookLoanService _loanService;
        private IMemberService _memberService;

        public BookLoanController(IBookLoanService bookLoanService, IMemberService memberService)
        {
            if (bookLoanService == null)
            {
                throw new ArgumentNullException("bookLoanService");
            }
            if (memberService == null)
            {
                throw new ArgumentNullException("memberService");
            }

            _loanService = bookLoanService;
            _memberService = memberService;
        }

        [HttpGet]
        public IEnumerable<Book> GetBooks(string title)
        {
            IEnumerable<Book> books = _loanService.GetBooks(title);
            return books;
        }

        [HttpPost]
        public Loan LoanABook(long memberId, long bookId)
        {
            Member member = _memberService.FindMember(memberId);
            if (member == null)
            {
                throw new InvalidOperationException("Invalid member");
            }

            Loan loan = _loanService.LoanABook(memberId, bookId);
            return loan;
        }
    }
}
