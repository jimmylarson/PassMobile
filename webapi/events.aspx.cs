using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using PassMobile.Models;
using PassMobile.Controllers;

namespace PassMobile.webapi
{
    public partial class events : System.Web.UI.Page
    {
        protected List<EventModel> modEvents { get; set; }
        protected ScheduleController contSchedule { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var start = (string.IsNullOrEmpty(Request["start"]?.ToString())) ? DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() : Request["start"].ToString();
            var end = (string.IsNullOrEmpty(Request["end"]?.ToString())) ? DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() : Request["end"].ToString();
            contSchedule = new ScheduleController();
            modEvents = contSchedule.GetScheduleEvents(Session["mas"].ToString(), Session["comp"].ToString(), Session["role"].ToString(), Session["group"].ToString(), start, end);
            var json = new StringBuilder();
            json.Append("[");
            foreach (EventModel rec in modEvents)
            {
                json.Append("{");
                json.Append("'id':" + rec.Id + ",");
                json.Append("'title':'" + Regex.Replace(rec.Title, @"([^a-zA-Z0-9_\-\$\?\*\+\(\)\[\]\\%,:;#=@ ])", string.Empty) + "',");
                json.Append("'allDay':true,");
                json.Append("'start':'" + rec.Start + "',");
                //json.Append("'editable':true,");
                json.Append("'color':'" + rec.Color + "',");
                var color = rec.TextColor.Trim();
                if (color.Length > 0)
                    json.Append("'textColor':'" + color + "',");
                json.Append("'description':'" + rec.EventHistId + "',");
                json.Append("'infoStatus':'" + rec.InfoStatus + "',");
                json.Append("'nominatedIndex':'" + rec.NominatedIndex + "',");
                json.Append("'eventType':'" + rec.EventType + "',");
                json.Append("'eventFrequency':'" + rec.EventFrequency + "',");
                json.Append("'eventAssigned':'" + Regex.Replace(rec.EventAssigned, @"([^a-zA-Z0-9_\-\$\?\*\+\(\)\\%,:;#=@ ])", string.Empty) + "'");
                json.Append("},");
            }
            json.Length--;
            json.Append("]");
            var resp = json.Replace("'", "\"").ToString();
            Response.Clear();
            Response.AddHeader("Content-type", "text/json");
            Response.Write(resp);
            Response.End();
        }
    }
}