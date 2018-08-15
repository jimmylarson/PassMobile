using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Security.Cryptography;

using PassMobile.Models;

namespace PassMobile.Controllers
{
    public class LoginController
    {
        private MobileSessionModel _session { get; set; }

        public LoginController()
        {
            _session = new MobileSessionModel();
            _session.authenticated = "false";
            _session.anti = "authenticate";
            _session.scram = "saltnvinegar";
            _session.activity = "authorize";
            _session.pass = "management";
            _session.mas = "system";
            _session.faults = "false";
        }

        public MobileSessionModel Login(string username, string password)
        {
            string ip = " ";
            HashSet<LoginModel> models = new HashSet<LoginModel>();
            string mas = string.Empty;
            string usersId = string.Empty;
            string activityId = string.Empty;
            string roleId = string.Empty;
            string groupId = string.Empty;
            string scram = string.Empty;
            string compId = string.Empty;
            string locationId = string.Empty;
            string code = string.Empty;

            if (username.Length <= 128 && password.Length <= 128)
            {
                var userHash = Hasher(username.ToLower());
                var passHash = Hasher(password.ToLower());

                var antiToken = AntiForgery("127.0.0.1");
                //returnValue = CheckLogin(antiToken, "127.0.0.1", userHash, passHash);
                //private bool CheckLogin(string token, string ip, string user, string pass)

                ip = "127.0.0.1";
                SqlConnection con = null;
                SqlCommand cmd = null;
                try
                {
                    con = new SqlConnection("Server=tcp:ye96cc8h82.database.windows.net,1433;Database=PASSMS;User ID=app@ye96cc8h82;Password=Unibase1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
                    con.Open();
                    cmd = new SqlCommand("SP_AUTH_AUTHENTICATE", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_Token", antiToken);
                    cmd.Parameters.AddWithValue("@p_UserHash", userHash);
                    cmd.Parameters.AddWithValue("@p_PassHash", passHash);
                    var outScram = new SqlParameter();
                    outScram.ParameterName = "@o_Scram";
                    outScram.SqlDbType = SqlDbType.VarChar;
                    outScram.Size = 255;
                    outScram.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outScram);
                    int spReturn = cmd.ExecuteNonQuery();
                    scram = outScram.Value.ToString();
                    if (scram.Length > 0)
                    {
                        _session.scram = scram.ToString();
                        cmd = new SqlCommand("SP_AUTH_LOGIN", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_Token", antiToken);
                        cmd.Parameters.AddWithValue("@p_Scram", scram);
                        var outId = new SqlParameter();
                        outId.ParameterName = "@o_Id";
                        outId.SqlDbType = SqlDbType.Int;
                        outId.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outId);
                        spReturn = cmd.ExecuteNonQuery();
                        usersId = outId.Value.ToString();
                        if (Int32.Parse(usersId) > 0)
                        {
                            _session.pass = usersId;
                            var g = Guid.NewGuid();
                            mas = g.ToString();
                            _session.mas = mas;

                            if (cmd != null)
                                cmd.Dispose();

                            cmd = new SqlCommand("SP_AUTH_ACTIVITY", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@p_MAS", mas);
                            cmd.Parameters.AddWithValue("@p_Token", antiToken);
                            cmd.Parameters.AddWithValue("@p_DevIp", ip);
                            cmd.Parameters.AddWithValue("@p_UsersId", usersId);
                            outId = null;
                            outId = new SqlParameter();
                            outId.ParameterName = "@o_Id";
                            outId.SqlDbType = SqlDbType.Int;
                            outId.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(outId);
                            spReturn = cmd.ExecuteNonQuery();
                            activityId = outId.Value.ToString();
                            _session.activity = activityId.ToString();
                            if (Int32.Parse(activityId) > 0)
                            {
                                //get the role
                                cmd = new SqlCommand("SP_COMP_USERS_ROLE_GET", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@p_MAS", mas);
                                cmd.Parameters.AddWithValue("@p_UsersId", usersId);
                                outId = null;
                                outId = new SqlParameter();
                                outId.ParameterName = "@o_Id";
                                outId.SqlDbType = SqlDbType.Int;
                                outId.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(outId);
                                spReturn = cmd.ExecuteNonQuery();
                                roleId = string.IsNullOrWhiteSpace(outId.Value.ToString()) ? "0" : outId.Value.ToString();
                                _session.role = roleId.ToString();

                                //get the group
                                cmd = new SqlCommand("SP_COMP_USERS_GROUP_GET", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@p_MAS", mas);
                                cmd.Parameters.AddWithValue("@p_UsersId", usersId);
                                outId = null;
                                outId = new SqlParameter();
                                outId.ParameterName = "@o_Id";
                                outId.SqlDbType = SqlDbType.Int;
                                outId.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(outId);
                                spReturn = cmd.ExecuteNonQuery();
                                groupId = string.IsNullOrWhiteSpace(outId.Value.ToString()) ? "0" : outId.Value.ToString();
                                _session.group = groupId.ToString();

                                //get the company info
                                cmd = new SqlCommand("SP_COMP_USERS_COMP_GET", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@p_MAS", mas);
                                cmd.Parameters.AddWithValue("@p_Id", usersId);
                                outId = null;
                                outId = new SqlParameter();
                                outId.ParameterName = "@o_Id";
                                outId.SqlDbType = SqlDbType.Int;
                                outId.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(outId);
                                spReturn = cmd.ExecuteNonQuery();
                                compId = string.IsNullOrWhiteSpace(outId.Value.ToString()) ? "0" : outId.Value.ToString();
                                if (System.Int32.Parse(compId) > 0)
                                {
                                    _session.comp = compId;
                                    _session.authenticated = "true";

                                    cmd = new SqlCommand("SP_COMP_USERS_LOCATION_GET", con);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@p_MAS", mas);
                                    cmd.Parameters.AddWithValue("@p_Id", usersId);
                                    outId = null;
                                    outId = new SqlParameter();
                                    outId.ParameterName = "@o_Id";
                                    outId.SqlDbType = SqlDbType.Int;
                                    outId.Direction = ParameterDirection.Output;
                                    cmd.Parameters.Add(outId);
                                    spReturn = cmd.ExecuteNonQuery();
                                    locationId = string.IsNullOrWhiteSpace(outId.Value.ToString()) ? "0" : outId.Value.ToString();
                                    if (System.Int32.Parse(locationId) > 0)
                                    {
                                        _session.campus = locationId;

                                        cmd = new SqlCommand("SP_COMP_CODE_GET", con);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@p_MAS", mas);
                                        cmd.Parameters.AddWithValue("@p_Id", compId);
                                        outId = null;
                                        outId = new SqlParameter();
                                        outId.ParameterName = "@o_Code";
                                        outId.SqlDbType = SqlDbType.VarChar;
                                        outId.Size = 50;
                                        outId.Direction = ParameterDirection.Output;
                                        cmd.Parameters.Add(outId);
                                        spReturn = cmd.ExecuteNonQuery();
                                        code = string.IsNullOrWhiteSpace(outId.Value.ToString()) ? "" : outId.Value.ToString();
                                        if (code.Length > 0)
                                        {
                                            _session.code = code;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    e.ToString();
                    _session.authenticated = "false";
                }
                finally
                {
                    if (cmd != null)
                        cmd.Dispose();
                    if (con != null)
                        con.Close();
                }
            }

            return _session;
        }


        private string AntiForgery(string ip)
        {
            Guid g = Guid.NewGuid();
            var token = g.ToString();
            _session.anti = token;
            return token;
        }


        static string Hasher(string password)
        {
            const string SALT = "Aston";
            const string VINEGAR = "VILLA";

            HashAlgorithm algorithm = new SHA256Managed();

            byte[] candidate = Encoding.UTF8.GetBytes(SALT + password + VINEGAR);
            byte[] computed = algorithm.ComputeHash(candidate);

            return Convert.ToBase64String(computed);
        }
    }
}

