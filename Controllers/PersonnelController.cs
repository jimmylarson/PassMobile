using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using PassMobile.Models;

namespace PassMobile.Controllers
{
    public class PersonnelController
    {
        public List<PersonnelSelectionsModel> GetPersonnelSelectionsList(string mas, string companyid)
        {
            string _mas = " ";
            string _companyId = " ";
            var models = new List<PersonnelSelectionsModel>();
            _mas += mas;
            _mas = _mas.Trim();
            _companyId += companyid;
            _companyId = _companyId.Trim();

            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            try
            {
                con = new SqlConnection("Server=tcp:ye96cc8h82.database.windows.net,1433;Database=PASSMS;User ID=app@ye96cc8h82;Password=Unibase1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
                con.Open();
                cmd = new SqlCommand("SP_COMP_PERSONNEL_SELECTIONS_GET", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_MAS", _mas);
                cmd.Parameters.AddWithValue("@p_CompanyId", _companyId);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    models.Add(new PersonnelSelectionsModel
                    {
                        PersonnelId = rdr["personnel_id"].ToString(),
                        NameFull = rdr["name_full"].ToString(),
                        SortIndex = rdr["sort_index"].ToString()
                    });
                }
            }
            catch (Exception)
            {
                models = null;
            }
            finally
            {
                if (rdr != null)
                    rdr.Dispose();
                if (cmd != null)
                    cmd.Dispose();
                if (con != null)
                    con.Close();
            }

            return models;
        }


        public List<PersonnelModel> GetPersonnelList(string mas, string companyId, string locationId)
        {
            string _mas = " ";
            string _companyId = " ";
            string _locationId = " ";
            var models = new List<PersonnelModel>();
            _mas += mas;
            _mas = _mas.Trim();
            _companyId += companyId;
            _companyId = _companyId.Trim();
            _locationId += locationId;
            _locationId = _locationId.Trim();

            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            try
            {
                con = new SqlConnection("Server=tcp:ye96cc8h82.database.windows.net,1433;Database=PASSMS;User ID=app@ye96cc8h82;Password=Unibase1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
                con.Open();
                cmd = new SqlCommand("SP_COMP_PERSONNEL_LIST_GET", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_MAS", _mas);
                cmd.Parameters.AddWithValue("@p_CompanyId", _companyId);
                cmd.Parameters.AddWithValue("@p_LocationId", _locationId);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    models.Add(new PersonnelModel
                    {
                        PersonnelId = rdr["personnel_id"].ToString(),
                        CompanyId = rdr["company_id"].ToString(),
                        LocationId = rdr["location_id"].ToString(),
                        UsersId = rdr["users_id"].ToString(),
                        GroupId = rdr["group_id"].ToString(),
                        GroupName = rdr["group_name"].ToString(),
                        PersCategoryId = rdr["pers_category_id"].ToString(),
                        PersCategoryName = rdr["pers_category_name"].ToString(),
                        NameFirst = rdr["name_first"].ToString(),
                        NameLast = rdr["name_last"].ToString(),
                        NameFull = rdr["name_full"].ToString(),
                        PersTitle = rdr["pers_title"].ToString(),
                        PhonePri = rdr["phone_pri"].ToString(),
                        PhoneAlt = rdr["phone_alt"].ToString(),
                        EmailPri = rdr["email_pri"].ToString(),
                        EmailAlt = rdr["email_alt"].ToString(),
                        SortIndex = rdr["sort_index"].ToString()
                    });
                }
            }
            catch (Exception)
            {
                models = null;
            }
            finally
            {
                if (rdr != null)
                    rdr.Dispose();
                if (cmd != null)
                    cmd.Dispose();
                if (con != null)
                    con.Close();
            }

            return models;
        }


        public bool PutNewPersonnel(PersonnelModel data)
        {
            bool retVal = false;

            return retVal;
        }
    }
}
