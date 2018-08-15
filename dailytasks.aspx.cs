using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PassMobile
{
    public partial class dailytasks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ModalEventDetailsComplete_Click(object sender, EventArgs e)
        {
            contSchedule = new ScheduleController();
            string eventId = ModalEventDetailsId?.Value.ToString();
            string eventType = ModalEventDetailsType?.Value.ToString();
            string dateActioned = ModalEventDetailsActionDate?.Text;
            string timeActioned = ModalEventDetailsActionTime?.Text;
            string completedById = ModalEventDetailsCompletedBy?.SelectedValue.ToString();
            string workNotes = ModalEventDetailsNotes?.Text;
            contSchedule.CompleteEvent(Session["mas"].ToString(), Session["comp"].ToString(), eventId, eventType, dateActioned, timeActioned, completedById, workNotes);

            Response.Redirect(Request.RawUrl);
        }

        protected void ModalEventDetailsDelete_Click(object sender, EventArgs e)
        {
            contSchedule = new ScheduleController();
            string eventId = ModalEventDetailsId?.Value.ToString();
            contSchedule.CancelEvent(Session["mas"].ToString(), Session["comp"].ToString(), eventId);

            Response.Redirect(Request.RawUrl);
        }

        protected void ModalEventInfoSave_Click(object sender, EventArgs e)
        {
            contSchedule = new ScheduleController();
            string eventId = ModalEventInfoId?.Value.ToString();
            string eventType = ModalEventInfoType?.Value.ToString();
            var booked = (ModalEventInfoBooked.Value == "true");
            var checkbox = ModalEventInfoBookedCheckBox.Checked;
            string changeDate = ModalEventInfoDate?.Text;
            var sqlDate = contSchedule.ConvertToSQLDateString(changeDate);
            string originalNotes = ModalEventInfoOriginalNotes?.Value;
            string changeNotes = ModalEventInfoNotes?.Text;
            if (booked != checkbox) contSchedule.SetEventInfo(Session["mas"].ToString(), Session["comp"].ToString(), eventId, "0", "0", "", "B");
            if (!string.IsNullOrWhiteSpace(eventId) && !string.IsNullOrWhiteSpace(sqlDate)) contSchedule.ChangeEvent(Session["mas"].ToString(), Session["comp"].ToString(), eventId, eventType, sqlDate);
            if (!string.IsNullOrWhiteSpace(eventId) && !string.IsNullOrWhiteSpace(changeNotes) && originalNotes != changeNotes) contSchedule.SetEventInfo(Session["mas"].ToString(), Session["comp"].ToString(), eventId, "0", "0", changeNotes, " ");
            Response.Redirect(Request.RawUrl);
        }
    }
}