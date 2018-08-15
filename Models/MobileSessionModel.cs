using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassMobile.Models
{
    public class LoginModel
    {
        public string MAS { get; set; }
        public int ActivityId { get; set; }
        public int UsersId { get; set; }
    }

    public class MobileSessionModel
    {
        public string authenticated { get; set; }
        public string anti { get; set; } //?
        public string scram { get; set; } //?
        public string activity { get; set; } //?
        public string pass { get; set; } //users id
        public string mas { get; set; } //?
        public string faults { get; set; } //set to false;
        public string role { get; set; } //role id
        public string group { get; set; } //group id
        public string comp { get; set; } //company id
        public string campus { get; set; } //location id
        public string code { get; set; } //code
    }
}