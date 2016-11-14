using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using WebApplication1.DataAccess;

namespace WebApplication1.Services
{
    public class MemberInfo
    {
        public string FullName { get; set; }
    }

    public interface IMemberService
    {
        Member CreateNewMember(MemberInfo newMemberInfo);
        Member FindMember(long memberId);
    }

    public class MemberService : IMemberService
    {
        private IDataAccessFactory _uowFactory;

        public MemberService() : this(DataAccessFactory.Instance)
        {
        }

        internal MemberService(IDataAccessFactory uowFactory)
        {
            if (uowFactory == null)
            {
                throw new ArgumentNullException("uowFactory");
            }

            _uowFactory = uowFactory;
        }

        public Member CreateNewMember(MemberInfo newMemberInfo)
        {
            Member newMember = new Member
            {
                FullName = newMemberInfo.FullName
            };

            using (IMemberUnitOfWork uow = _uowFactory.GetMemberUnitOfWork())
            {
                uow.InsertNewMember(newMember);
                uow.Save();
            }

            return newMember;
        }

        public Member FindMember(long memberId)
        {
            //using (IMemberUnitOfWork uow = _uowFactory.GetMemberUnitOfWork())
            using (IMemberDataAccess memberDA = _uowFactory.GetMemberDataAccess())
            {
                return memberDA.FindMember(memberId);
            }
        }
    }
}