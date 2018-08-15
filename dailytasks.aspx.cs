using System;
using PassMobile.Controllers;
using PassMobile.Models;
using System.Collections.Generic;
namespace PassMobile
{
    public partial class dailytasks : System.Web.UI.Page
    {
        protected ScheduleController contSchedule { get; set; }
        protected PersonnelController contPersonnel { get; set; }
        protected List<PersonnelSelectionsModel> modCompanyPersonnelSelections { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                contPersonnel = new PersonnelController();
                modCompanyPersonnelSelections = contPersonnel.GetPersonnelSelectionsList(Session["mas"].ToString(), Session["comp"].ToString());
                ModalEventDetailsCompletedBy.DataSource = modCompanyPersonnelSelections;
                ModalEventDetailsCompletedBy.DataTextField = "NameFull";
                ModalEventDetailsCompletedBy.DataValueField = "PersonnelId";
                ModalEventDetailsCompletedBy.DataBind();
            }
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