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

    }
}