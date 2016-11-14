using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.DataAccess
{
    public interface IDataAccessFactory
    {
        ILoanUnitOfWork GetLoanUnitOfWork();
        IMemberUnitOfWork GetMemberUnitOfWork();
        IMemberDataAccess GetMemberDataAccess();
    }

    public class DataAccessFactory : IDataAccessFactory
    {
        #region Singleton

        private static readonly Lazy<DataAccessFactory> _instance =
            new Lazy<DataAccessFactory>(() => new DataAccessFactory());

        private DataAccessFactory()
        {
        }

        public static DataAccessFactory Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        #endregion Singleton

        public ILoanUnitOfWork GetLoanUnitOfWork()
        {
            return new LoanUnitOfWork();
        }

        public IMemberUnitOfWork GetMemberUnitOfWork()
        {
            return new MemberUnitOfWork();
        }

        public IMemberDataAccess GetMemberDataAccess()
        {
            return new MemberDataAccess();
        }
    }
}