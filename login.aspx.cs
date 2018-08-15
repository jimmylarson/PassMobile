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
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            var username = TextUsername.Text;
            var password = TextPassword.Text;
            contLogin = new LoginController();
            mobileSession = contLogin.Login(username, password);

            if (mobileSession.authenticated == "true")
            {
                Response.Redirect("dailytasks.html");
            }
        }
    }
}