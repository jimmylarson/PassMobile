using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using PassMobile.Models;
using PassMobile.Controllers;

namespace PassMobile.webapi
{
    public partial class mobile_login : System.Web.UI.Page
    {
        private LoginController contLogin { get; set; }
        private MobileSessionModel mobileSession { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            var username = (string.IsNullOrEmpty(Request["user"]?.ToString())) ? string.Empty : Request["user"].ToString();
            var password = (string.IsNullOrEmpty(Request["pass"]?.ToString())) ? string.Empty : Request["pass"].ToString();
            contLogin = new LoginController();
            mobileSession = contLogin.Login(username, password);
            var json = new StringBuilder();
            json.Append("[");
            json.Append("{");
            json.Append("'authenticated':'" + mobileSession.authenticated + "',");
            json.Append("'anti':'" + mobileSession.anti + "',");
            json.Append("'scram':'" + mobileSession.scram + "',");
            json.Append("'activity':'" + mobileSession.activity + "',");
            json.Append("'pass':'" + mobileSession.pass + "',");
            json.Append("'mas':'" + mobileSession.mas + "',");
            json.Append("'faults':'" + mobileSession.faults + "',");
            json.Append("'role':'" + mobileSession.role + "',");
            json.Append("'group':'" + mobileSession.group + "',");
            json.Append("'comp':'" + mobileSession.comp + "',");
            json.Append("'campus':'" + mobileSession.campus + "',");
            json.Append("'code':'" + mobileSession.code + "'");
            json.Append("}");
            json.Append("]");
            var resp = json.Replace("'", "\"").ToString();
            Response.Clear();
            Response.AddHeader("Content-type", "text/json");
            Response.Write(resp);
            Response.End();
        }
    }
}
