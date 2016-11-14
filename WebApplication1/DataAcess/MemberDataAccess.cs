using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public interface IMemberDataAccess : IDisposable
    {
        Member FindMember(long memberId);
        void InsertNewMember(Member newMember);
    }

    public class MemberDataAccess : IMemberDataAccess
    {
        private LibraryContext _dbContext;

        public MemberDataAccess()
        {
            _dbContext = new LibraryContext();
        }

        internal MemberDataAccess(LibraryContext libraryContext)
        {
            if (libraryContext == null)
            {
                throw new ArgumentNullException("libraryContext");
            }

            _dbContext = libraryContext;
        }

        public Member FindMember(long memberId)
        {
            Member member = _dbContext.Members
                           .SqlQuery("SELECT * FROM dbo.Members WHERE MemberId = @memberId",
                                     new SqlParameter("@memberId", memberId))
                           .Single();
            return member;
        }

        public void InsertNewMember(Member newMember)
        {
            _dbContext.Database.ExecuteSqlCommand(
                "INSERT INTO dbo.Members (FullName) VALUES (@fullName)",
                new SqlParameter("@fullName", newMember.FullName));
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