using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebApplication1.Models;


namespace WebApplication1.DataAccess
{
    public interface ILoanUnitOfWork : IDisposable
    {
        Book FindBook(long bookId);
        IEnumerable<Book> GetBooks(Expression<Func<Book, bool>> searchConditions);
        IEnumerable<Loan> GetLoans(Expression<Func<Loan, bool>> searchConditions);
        void InsertLoanRecord(Loan newLoan);
        void Save();
    }

    public class LoanUnitOfWork : ILoanUnitOfWork
    {
        private LibraryContext _dbContext = null;

        public LoanUnitOfWork()
        {
            _dbContext = new LibraryContext();
        }

        internal LoanUnitOfWork(LibraryContext libraryContext)
        {
            if (libraryContext == null)
            {
                throw new ArgumentNullException("libraryContext");
            }

            _dbContext = libraryContext;
        }

        public Book FindBook(long bookId)
        {
            return _dbContext.Books.Find(bookId);
        }

        public IEnumerable<Book> GetBooks(Expression<Func<Book, bool>> searchConditions)
        {
            return _dbContext.Books.Where(searchConditions).ToList();
        }

        public IEnumerable<Loan> GetLoans(Expression<Func<Loan, bool>> searchConditions)
        {
            return _dbContext.Loans.Where(searchConditions).ToList();
        }

        public void InsertLoanRecord(Loan newLoan)
        {
            _dbContext.Loans.Add(newLoan);
        }
        
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                _dbContext.Dispose();

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~LoanUnitOfWork() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}