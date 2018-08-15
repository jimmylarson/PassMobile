using System;

using PassMobile.Models;
using PassMobile.Controllers;

namespace PassMobile
{
    public partial class login : System.Web.UI.Page
    {
        private LoginController contLogin { get; set; }
        private MobileSessionModel mobileSession { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Remove("mas");
            Session.Remove("comp");
            Session.Remove("role");
            Session.Remove("group");
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            var username = TextUsername.Text;
            var password = TextPassword.Text;
            contLogin = new LoginController();
            mobileSession = contLogin.Login(username, password);

            if (mobileSession.authenticated == "true")
            {
                Session["mas"] = mobileSession.mas;
                Session["comp"] = mobileSession.comp;
                Session["role"] = mobileSession.role;
                Session["group"] = mobileSession.group;
                Response.Redirect("dailytasks.aspx");
            }
        }
    }
}