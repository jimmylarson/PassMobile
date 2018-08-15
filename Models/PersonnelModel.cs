using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassMobile.Models
{
    public class PersonnelModel
    {
        public string PersonnelId { get; set; }
        public string CompanyId { get; set; }
        public string LocationId { get; set; }
        public string UsersId { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public string PersCategoryId { get; set; }
        public string PersCategoryName { get; set; }
        public string NameFirst { get; set; }
        public string NameLast { get; set; }
        public string NameFull { get; set; }
        public string PersTitle { get; set; }
        public string PhonePri { get; set; }
        public string PhoneAlt { get; set; }
        public string EmailPri { get; set; }
        public string EmailAlt { get; set; }
        public string SortIndex { get; set; }
    }

    public class PersonnelSelectionsModel
    {
        public string PersonnelId { get; set; }
        public string NameFull { get; set; }
        public string SortIndex { get; set; }
    }
}