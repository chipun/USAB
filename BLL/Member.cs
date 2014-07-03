using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USAB.Model;
using USAB.IDAL;
using System.Data;

namespace USAB.BLL
{
    public class Member
    {
        // Get an instance of the User DAL using the DALFactory
        // Making this static will cache the DAL instance after the initial load
        private static readonly IMember dal = USAB.DALFactory.DataAccess.CreateMember();

        public MemberInfo getMember(int id)
        {
            if (id <= 0)
                return null;

            return dal.getMember(id);
        }

        public bool updateMember(MemberInfo member)
        {
            if (member == null)
            {
                return false;
            }

            return dal.updateMember(member);
        }

        public bool saveMember(MemberInfo member)
        {
            if (member == null)
            {
                return false;
            }

            return dal.saveMember(member);
        }

        public bool SecurityMember(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(password))
                return false;

            return dal.SecurityMember(email, password);
        }

        

    }
}
