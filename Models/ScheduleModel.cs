using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassMobile.Models
{
    public class FCEventModel
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string EventHistId { get; set; }
        public string EventType { get; set; }
        public string EventFrequency { get; set; }
        public string EventAssigned { get; set; }
        public string Title { get; set; }
        public string AllDay { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Url { get; set; }
        public string ClassName { get; set; }
        public string Editable { get; set; }
        public string StartEditable { get; set; }
        public string DurationEditable { get; set; }
        public string ResourceEditable { get; set; }
        public string Rendering { get; set; }
        public string Overlap { get; set; }
        public string Constraint { get; set; }
        public string Source { get; set; }
        public string Color { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public string TextColor { get; set; }
        public string Description { get; set; }
    }

    public class TaskScheduledDateRangeModel
    {
        public string TaskHistId { get; set; }
        public string TaskDefId { get; set; }
        public string EventId { get; set; }
        public string EventType { get; set; }
        public string Frequency { get; set; }
        public string Location { get; set; }
        public string Assigned { get; set; }
        public string Title { get; set; }
        public string Instructions { get; set; }
        public List<string> Scheduled { get; set; }
    }

    public class ScheduleInfoModel
    {
        public string SchdInfoId { get; set; }
        public string CompanyId { get; set; }
        public string EventId { get; set; }
        public string NotesText { get; set; }
    }
}
