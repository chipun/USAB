using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using USAB.Model;
using USAB.BLL;

public partial class Account_Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        Member MemberBLL = new Member();
        MemberInfo setMember = new MemberInfo();

        setMember.email = email.Text;
        setMember.password = passwordASP.Text;
        setMember.username = string.Empty;
        setMember.member_id = 0;
        setMember.first_name = string.Empty;
        setMember.last_name = string.Empty;
        setMember.middle_name = string.Empty;
        setMember.role = string.Empty;
        setMember.status = string.Empty;
        setMember.address1 = string.Empty;
        setMember.address2 = string.Empty;
        setMember.city = string.Empty;
        setMember.state = string.Empty;
        setMember.zip = string.Empty;
        setMember.birthdate = string.Empty;

        MemberBLL.saveMember(setMember);



    }
}