using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USAB.Model;

namespace USAB.IDAL
{
    public interface IMember
    {
       MemberInfo getMember(int id);

       bool SecurityMember(string email, string password);

       bool updateMember(MemberInfo member);

       bool saveMember(MemberInfo member);
    }
}
