using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using WebApplication1.DataAccess;

namespace WebApplication1.Services
{
    public interface IBookLoanService
    {
        IEnumerable<Book> GetBooks(string title);
        Loan LoanABook(long memberId, long bookId);
    }

    public class BookLoanService : IBookLoanService
    {
        #region Private

        private IDataAccessFactory _uowFactory;

        private const int LoanDuration = 21;
        private const int MaxNumberOfBook = 3;

        #endregion Private
        
        public BookLoanService() : this(DataAccessFactory.Instance)
        {
        }

        internal BookLoanService(IDataAccessFactory uowFactory)
        {
            if (uowFactory == null)
            {
                throw new ArgumentNullException("uowFactory");
            }

            _uowFactory = uowFactory;
        }
        
        public IEnumerable<Book> GetBooks(string title)
        {
            using (ILoanUnitOfWork uow = _uowFactory.GetLoanUnitOfWork())
            {
                return uow.GetBooks(b => b.Title == title);
            }
        }

        public Loan LoanABook(long memberId, long bookId)
        {
            using (ILoanUnitOfWork uow = _uowFactory.GetLoanUnitOfWork())
            {
                Book bookToLoan = uow.FindBook(bookId);
                if (bookToLoan == null)
                {
                    throw new InvalidOperationException("The book does not exist.");
                }

                if (bookToLoan.Status == BookStatus.OnLoan)
                {
                    throw new InvalidOperationException("The book is already on loan.");
                }

                if (HasMemberBorrowedMaxNumberOfBooks(uow, memberId))
                {
                    throw new InvalidOperationException("You have borrowed the maximum number of books allowed.");
                }

                Loan newLoan = CreateNewLoanRecord(uow, memberId, bookToLoan.BookId);
                bookToLoan.Status = BookStatus.OnLoan;

                uow.Save();

                return newLoan;
            }
        }

        #region Private Methods

        private bool HasMemberBorrowedMaxNumberOfBooks(ILoanUnitOfWork uow, long memberId)
        {
            int numberOfBooksOnLoan = uow.GetLoans(loan => loan.MemberId == memberId && loan.Status == LoanStatus.Open).Count();
            return (numberOfBooksOnLoan == MaxNumberOfBook);
        }

        private Loan CreateNewLoanRecord(ILoanUnitOfWork uow, long memberId, long bookId)
        {
            var newLoan = new Loan
            {
                MemberId = memberId,
                BookId = bookId,
                Status = LoanStatus.Open,
                LoanDate = DateTime.UtcNow.Date,
                ExpectedReturnDate = DateTime.UtcNow.Date.AddDays(LoanDuration)
            };
            uow.InsertLoanRecord(newLoan);

            return newLoan;
        }
        
        #endregion Private Methods
    }
}