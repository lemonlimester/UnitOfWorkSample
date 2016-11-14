using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApplication1.Controllers;
using WebApplication1.Services;
using WebApplication1.DataAccess;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UnitTestProject1
{
    [TestClass]
    public class BookLoanServiceTest
    {
        [TestMethod]
        public void BookLoanServiceTest_LoanABook()
        {
            // Arrange
            var mockFactory = new Mock<IDataAccessFactory>();
            var bookLoanService = new BookLoanService(mockFactory.Object);
            var memberService = new MemberService(mockFactory.Object);

            long memberId = 10;
            Book book1 = new Book { BookId = 1, ReferenceNo = "#1", Status = BookStatus.Available };
            
            #region Loan Uow

            var mockLoanUow = new Mock<ILoanUnitOfWork>();
            mockFactory.Setup(f => f.GetLoanUnitOfWork()).Returns(mockLoanUow.Object);
            mockLoanUow.Setup(uow => uow.FindBook(book1.BookId)).Returns(book1);
            mockLoanUow.Setup(uow => uow.GetLoans(It.IsAny<Expression<Func<Loan, bool>>>())).Returns(new List<Loan>());

            Loan newLoan = null;
            mockLoanUow.Setup(uow => uow.InsertLoanRecord(It.IsAny<Loan>()))
                .Callback<Loan>(loan => newLoan = loan);
            
            #endregion Loan Uow
            
            // Act
            var service = new BookLoanService(mockFactory.Object);
            service.LoanABook(memberId, book1.BookId);

            // Assert
            Assert.IsNotNull(newLoan);
            Assert.AreEqual(memberId, newLoan.MemberId);
            Assert.AreEqual(book1.BookId, newLoan.BookId);
            Assert.AreEqual(LoanStatus.Open, newLoan.Status);
            Assert.AreEqual(DateTime.UtcNow.Date, newLoan.LoanDate);
            Assert.AreEqual(DateTime.UtcNow.Date.AddDays(21), newLoan.ExpectedReturnDate);
            Assert.IsNull(newLoan.ActualReturnDate);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "You have borrowed the maximum number of books allowed.")]
        public void BookLoanServiceTest_LoanABook_ShouldFail_IfMemberHasBorrowedMaxNumberOfBooks()
        {
            // Arrange
            var mockFactory = new Mock<IDataAccessFactory>();
            var bookLoanService = new BookLoanService(mockFactory.Object);
            
            long memberId = 10;
            Book book1 = new Book { BookId = 1, ReferenceNo = "#1", Status = BookStatus.Available };

            var mockLoanUow = new Mock<ILoanUnitOfWork>();
            mockFactory.Setup(f => f.GetLoanUnitOfWork()).Returns(mockLoanUow.Object);
            mockLoanUow.Setup(uow => uow.FindBook(book1.BookId)).Returns(book1);
            mockLoanUow.Setup(uow => uow.GetLoans(It.IsAny<Expression<Func<Loan, bool>>>()))
                .Returns(new List<Loan>{
                            new Loan { LoanId = 100, MemberId = memberId },
                            new Loan { LoanId = 101, MemberId = memberId },
                            new Loan { LoanId = 102, MemberId = memberId }
                        });

            // Act
            var service = new BookLoanService(mockFactory.Object);
            service.LoanABook(memberId, book1.BookId);
        }
    }
}
