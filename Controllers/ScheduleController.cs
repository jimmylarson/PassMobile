using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using PassMobile.Models;

namespace PassMobile.Controllers
{
    public class ScheduleController
    {
        public List<EventModel> GetScheduleEvents(string mas, string companyId, string role, string group, string begin, string end)
        {
            string _mas = " ";
            string _companyId = " ";
            string _begin = " ";
            string _end = " ";
            string _filter = " ";
            var models = new List<EventModel>();
            _mas += mas;
            _mas = _mas.Trim();
            _companyId += companyId;
            _companyId = _companyId.Trim();
            _begin += begin;
            _begin = _begin.Trim();
            _begin.Replace("-", ".");
            _end += end;
            _end = _end.Trim();
            _end.Replace("-", ".");
            _filter += group;
            _filter = _filter.Trim();

            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            try
            {
                var proc = (role == "101") ? @"SP_SCHD_COMP_EVENTS_ROLE_GET" : @"SP_SCHD_COMP_EVENTS_ALL_GET";
                con = new SqlConnection("Server=tcp:ye96cc8h82.database.windows.net,1433;Database=PASSMS;User ID=app@ye96cc8h82;Password=Unibase1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
                con.Open();
                cmd = new SqlCommand(proc, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_MAS", _mas);
                cmd.Parameters.AddWithValue("@p_CompanyId", _companyId);
                cmd.Parameters.AddWithValue("@p_Begin", _begin);
                cmd.Parameters.AddWithValue("@p_End", _end);
                cmd.Parameters.AddWithValue("@p_Filter", _filter);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    models.Add(new EventModel
                    {
                        Id = rdr["event_id"].ToString(),
                        CompanyId = rdr["company_id"].ToString(),
                        EventHistId = rdr["event_hist_id"].ToString(),
                        EventType = rdr["event_type"].ToString(),
                        EventFrequency = rdr["event_frequency"].ToString(),
                        EventAssigned = rdr["event_assigned"].ToString(),
                        Title = rdr["fc_title"].ToString(),
                        AllDay = rdr["fc_allday"].ToString(),
                        Start = rdr["fc_start"].ToString(),
                        End = "",
                        Url = "",
                        ClassName = "",
                        Editable = "",
                        StartEditable = "",
                        DurationEditable = "",
                        ResourceEditable = "",
                        Rendering = "",
                        Overlap = "",
                        Constraint = "",
                        Source = "",
                        Color = rdr["fc_color"].ToString(),
                        BackgroundColor = "",
                        BorderColor = "",
                        TextColor = rdr["fc_textcolor"].ToString(),
                        Description = rdr["event_hist_id"].ToString(),
                        InfoStatus = rdr["info_status"].ToString(),
                        NominatedIndex = rdr["nominated_index"].ToString()
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

            return ScrubScheduleEvents(models);
        }


        private List<EventModel> ScrubScheduleEvents(List<EventModel> models)
        {
            var scrubbed = new List<EventModel>();
            foreach (EventModel rec in models)
            {
                var title = rec.Title;
                var color = rec.Color;
                var textColor = rec.TextColor;

                // Holiday (H) scrubbing
                if (rec.EventType == "H")
                {
                    title = "- " + rec.Title + " -";
                    color = "#FFA500";
                    textColor = "#000000";
                }
                // Compliance (C) scrubbing
                else if (rec.EventType == "C" || rec.EventType == "G")
                {
                    var start = DateTime.Parse(rec.Start);
                    if (rec.Color != "#8032CD")
                    {
                        if (start < System.DateTime.Today)
                            color = "#FF0000";
                        else
                            color = "#32CD32";
                    }
                    else
                    {
                        color = "#8032CD";
                    }
                    textColor = "#FFFFFF";
                }
                // Jobs (J) scrubbing
                else if (rec.EventType == "J")
                {
                    var start = DateTime.Parse(rec.Start);
                    if (start < System.DateTime.Today)
                        //color = "#FF0000";
                        color = "#E1B541";
                    else
                        color = "#E1B541";
                    textColor = "#FFFFFF";
                }
                // Repairs (R) scrubbing
                else if (rec.EventType == "R" || rec.EventType == "J")
                {
                    var start = DateTime.Parse(rec.Start);
                    if (start < System.DateTime.Today)
                        color = "#FF0000";
                    else
                        color = "#4169E1";
                    textColor = "#FFFFFF";
                }

                scrubbed.Add(new EventModel
                {
                    Id = rec.Id,
                    CompanyId = rec.CompanyId,
                    EventHistId = rec.EventHistId,
                    EventType = rec.EventType,
                    EventFrequency = rec.EventFrequency,
                    EventAssigned = rec.EventAssigned,
                    Title = title,
                    AllDay = rec.AllDay,
                    Start = rec.Start,
                    End = rec.End,
                    Url = rec.Url,
                    ClassName = rec.ClassName,
                    Editable = rec.Editable,
                    StartEditable = rec.StartEditable,
                    DurationEditable = rec.DurationEditable,
                    ResourceEditable = rec.ResourceEditable,
                    Rendering = rec.Rendering,
                    Overlap = rec.Overlap,
                    Constraint = rec.Constraint,
                    Source = rec.Source,
                    Color = color,
                    BackgroundColor = rec.BackgroundColor,
                    BorderColor = rec.BorderColor,
                    TextColor = textColor,
                    Description = rec.Description,
                    InfoStatus = rec.InfoStatus,
                    NominatedIndex = rec.NominatedIndex
                });
            }

            return scrubbed;
        }


        public List<string> GetScheduledTasksDateRange(string mas, string companyId, string begin, string end)
        {
            string _mas = " ";
            string _companyId = " ";
            string _begin = " ";
            string _end = " ";
            var list = new List<string>();
            _mas += mas;
            _mas = _mas.Trim();
            _companyId += companyId;
            _companyId = _companyId.Trim();
            _begin += begin;
            _begin = _begin.Trim();
            _begin.Replace("-", ".");
            _end += end;
            _end = _end.Trim();
            _end.Replace("-", ".");

            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            try
            {
                con = new SqlConnection("Server=tcp:ye96cc8h82.database.windows.net,1433;Database=PASSMS;User ID=app@ye96cc8h82;Password=Unibase1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
                con.Open();
                cmd = new SqlCommand("SP_SCHD_COMP_TASKS_DATE_RANGE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_MAS", _mas);
                cmd.Parameters.AddWithValue("@p_CompanyId", _companyId);
                cmd.Parameters.AddWithValue("@p_Begin", _begin);
                cmd.Parameters.AddWithValue("@p_End", _end);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    list.Add(rdr["task_hist_id"].ToString());
                }
            }
            catch (Exception)
            {
                list = null;
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

            return list;
        }

        public TaskScheduledDateRangeModel GetTaskScheduledForDateRange(string mas, string taskHistId, string begin, string end)
        {
            string _mas = " ";
            string _taskHistId = " ";
            string _begin = " ";
            string _end = " ";
            var data = new TaskScheduledDateRangeModel();
            data.Scheduled = new List<string>();
            _mas += mas;
            _mas = _mas.Trim();
            _taskHistId += taskHistId;
            _taskHistId = _taskHistId.Trim();
            _begin += begin;
            _begin = _begin.Trim();
            _end += end;
            _end = _end.Trim();

            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            try
            {
                con = new SqlConnection("Server=tcp:ye96cc8h82.database.windows.net,1433;Database=PASSMS;User ID=app@ye96cc8h82;Password=Unibase1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
                con.Open();
                cmd = new SqlCommand("SP_SCHD_COMP_GET_TASK_SCHEDULED_DATE_RANGE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_MAS", _mas);
                cmd.Parameters.AddWithValue("@p_TaskHistId", _taskHistId);
                cmd.Parameters.AddWithValue("@p_Begin", _begin);
                cmd.Parameters.AddWithValue("@p_End", _end);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    data.TaskHistId = rdr["task_hist_id"].ToString();
                    data.TaskDefId = rdr["task_def_id"].ToString();
                    data.EventId = rdr["event_id"].ToString();
                    data.EventType = rdr["event_type"].ToString();
                    data.Frequency = rdr["frequency"].ToString();
                    data.Location = rdr["location"].ToString();
                    data.Assigned = rdr["assigned"].ToString();
                    data.Title = rdr["title"].ToString();
                    data.Instructions = rdr["task_instructions"].ToString();
                    data.Scheduled.Add(rdr["scheduled"].ToString());
                }
            }
            catch (Exception)
            {
                data = null;
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

            return data;
        }


        public bool CompleteEvent(string mas, string companyId, string eventId, string eventType, string dateActioned, string timeActioned, string completedById, string workNotes)
        {
            bool retVal = false;

            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection("Server=tcp:ye96cc8h82.database.windows.net,1433;Database=PASSMS;User ID=app@ye96cc8h82;Password=Unibase1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
                con.Open();
                cmd = new SqlCommand("SP_SCHD_COMP_COMPLETE_EVENT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_MAS", mas);
                cmd.Parameters.AddWithValue("@p_CompanyId", companyId);
                cmd.Parameters.AddWithValue("@p_EventId", eventId);
                cmd.Parameters.AddWithValue("@p_EventType", eventType);
                cmd.Parameters.AddWithValue("@p_DateActioned", dateActioned);
                cmd.Parameters.AddWithValue("@p_TimeActioned", timeActioned);
                cmd.Parameters.AddWithValue("@p_CompletedById", completedById);
                cmd.Parameters.AddWithValue("@p_WorkNotes", workNotes);
                int spReturn = cmd.ExecuteNonQuery();
                if (spReturn > 0)
                {
                    retVal = true;
                }
                else
                {
                    retVal = false;
                }
            }
            catch (Exception)
            {
                return retVal;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                if (con != null)
                    con.Close();
            }

            return retVal;
        }


        public bool ChangeEvent(string mas, string companyId, string eventId, string eventType, string changeDate)
        {
            bool retVal = false;

            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection("Server=tcp:ye96cc8h82.database.windows.net,1433;Database=PASSMS;User ID=app@ye96cc8h82;Password=Unibase1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
                con.Open();
                cmd = new SqlCommand("SP_SCHD_COMP_CHANGE_EVENT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_MAS", mas);
                cmd.Parameters.AddWithValue("@p_CompanyId", companyId);
                cmd.Parameters.AddWithValue("@p_EventId", eventId);
                cmd.Parameters.AddWithValue("@p_EventType", eventType);
                cmd.Parameters.AddWithValue("@p_ChangeDate", changeDate);
                int spReturn = cmd.ExecuteNonQuery();
                if (spReturn > 0)
                {
                    retVal = true;
                }
                else
                {
                    retVal = false;
                }
            }
            catch (Exception)
            {
                return retVal;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                if (con != null)
                    con.Close();
            }

            return retVal;
        }



        public bool CancelEvent(string mas, string companyId, string eventId)
        {
            bool retVal = false;

            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection("Server=tcp:ye96cc8h82.database.windows.net,1433;Database=PASSMS;User ID=app@ye96cc8h82;Password=Unibase1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
                con.Open();
                cmd = new SqlCommand("SP_SCHD_COMP_CANCEL_EVENT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_MAS", mas);
                cmd.Parameters.AddWithValue("@p_CompanyId", companyId);
                cmd.Parameters.AddWithValue("@p_EventId", eventId);
                int spReturn = cmd.ExecuteNonQuery();
                if (spReturn > 0)
                {
                    retVal = true;
                }
                else
                {
                    retVal = false;
                }
            }
            catch (Exception)
            {
                return retVal;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                if (con != null)
                    con.Close();
            }

            return retVal;
        }



        public bool SetEventInfo(string mas, string companyId, string eventId, string refId, string personnelId, string notes, string status)
        {
            bool retVal = false;
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection("Server=tcp:ye96cc8h82.database.windows.net,1433;Database=PASSMS;User ID=app@ye96cc8h82;Password=Unibase1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
                con.Open();
                cmd = new SqlCommand("SP_SCHD_EVENT_SET_INFO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_MAS", mas);
                cmd.Parameters.AddWithValue("@p_CompanyId", companyId);
                cmd.Parameters.AddWithValue("@p_EventId", eventId);
                cmd.Parameters.AddWithValue("@p_RefId", refId);
                cmd.Parameters.AddWithValue("@p_PersonnelId", personnelId);
                cmd.Parameters.AddWithValue("@p_Notes", notes);
                cmd.Parameters.AddWithValue("@p_Status", status);
                int spReturn = cmd.ExecuteNonQuery();
                if (spReturn > 0)
                {
                    retVal = true;
                }
                else
                {
                    retVal = false;
                }
            }
            catch (Exception)
            {
                return retVal;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                if (con != null)
                    con.Close();
            }

            return retVal;
        }

        public ScheduleInfoModel GetEventInfo(string mas, string companyId, string eventId)
        {
            string _mas = " ";
            string _companyId = " ";
            string _eventId = " ";
            var model = new ScheduleInfoModel();
            _mas += mas;
            _mas = _mas.Trim();
            _companyId += companyId;
            _companyId = _companyId.Trim();
            _eventId += eventId;
            _eventId = _eventId.Trim();

            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            try
            {
                con = new SqlConnection("Server=tcp:ye96cc8h82.database.windows.net,1433;Database=PASSMS;User ID=app@ye96cc8h82;Password=Unibase1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
                con.Open();
                cmd = new SqlCommand("SP_SCHD_EVENT_INFO_GET", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_MAS", _mas);
                cmd.Parameters.AddWithValue("@p_CompanyId", _companyId);
                cmd.Parameters.AddWithValue("@p_EventId", _eventId);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    model.NotesText = rdr["notes_text"].ToString();
                }
            }
            catch (Exception)
            {
                model = null;
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

            return model;
        }


        public bool ScheduleJob(string mas, string companyId, string jobDefHistId, string jobType, string jobStatus, string jobFrequency, string jobName, string jobScheduleDate, string jobAssigned)
        {
            bool retVal = false;
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection("Server=tcp:ye96cc8h82.database.windows.net,1433;Database=PASSMS;User ID=app@ye96cc8h82;Password=Unibase1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
                con.Open();
                cmd = new SqlCommand("SP_SCHD_SCHEDULE_JOB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_CompanyId", companyId);
                cmd.Parameters.AddWithValue("@p_JobDefHistId", jobDefHistId);
                cmd.Parameters.AddWithValue("@p_JobType", jobType);
                cmd.Parameters.AddWithValue("@p_JobStatus", jobStatus);
                cmd.Parameters.AddWithValue("@p_JobFrequency", jobFrequency);
                cmd.Parameters.AddWithValue("@p_JobName", jobName);
                cmd.Parameters.AddWithValue("@p_JobScheduleDate", jobScheduleDate);
                cmd.Parameters.AddWithValue("@p_JobAssigned", jobAssigned);
                int spReturn = cmd.ExecuteNonQuery();
                if (spReturn > 0)
                {
                    retVal = true;
                }
                else
                {
                    retVal = false;
                }
            }
            catch (Exception)
            {
                return retVal;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                if (con != null)
                    con.Close();
            }

            return retVal;
        }

        public bool RescheduleJob(string mas, string companyId, string jobDefHistId, string jobType, string jobStatus, string jobName, string jobScheduleDate, string jobAssigned, string jobFrequency)
        {
            bool retVal = false;
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection("Server=tcp:ye96cc8h82.database.windows.net,1433;Database=PASSMS;User ID=app@ye96cc8h82;Password=Unibase1;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
                con.Open();
                cmd = new SqlCommand("SP_SCHD_RESCHEDULE_JOB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_CompanyId", companyId);
                cmd.Parameters.AddWithValue("@p_JobDefHistId", jobDefHistId);
                cmd.Parameters.AddWithValue("@p_JobType", jobType);
                cmd.Parameters.AddWithValue("@p_JobStatus", jobStatus);
                cmd.Parameters.AddWithValue("@p_JobName", jobName);
                cmd.Parameters.AddWithValue("@p_JobScheduleDate", jobScheduleDate);
                cmd.Parameters.AddWithValue("@p_JobAssigned", jobAssigned);
                cmd.Parameters.AddWithValue("@p_JobFrequency", jobFrequency);
                int spReturn = cmd.ExecuteNonQuery();
                if (spReturn > 0)
                {
                    retVal = true;
                }
                else
                {
                    retVal = false;
                }
            }
            catch (Exception)
            {
                return retVal;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                if (con != null)
                    con.Close();
            }

            return retVal;
        }
    }
}