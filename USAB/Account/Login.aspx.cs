using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using USAB.BLL;
using USAB.Model;
using Twilio;

public partial class Account_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void submit_Click(object sender, EventArgs e)
    {
        //Member MemberBLL = new Member();
        //if (MemberBLL.SecurityMember(email.Text, passwordASP.Text))
        //{
        //    string test = "ASDFsdfdaF";
        //}
        //else
        //{
        //    string test = "ASDFsddf3333333333333333fdaF";
        //}

        string AccountSid = "AC45b00a5504e242b8a486ebf4cad405c9";
        string AuthToken = "d6caf7ba87eb06448f762ec61bbaa71c";
        TwilioRestClient twilio = new TwilioRestClient(AccountSid, AuthToken);

        try
        {
            var message = twilio.SendMessage("+12405475100", "+15714558169", "Hello"); 
        }
        catch (Exception ex)
        {
            string test = ex.Message;
        }
    }
}