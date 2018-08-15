$(function () {
    $('#calendar').fullCalendar({
        defaultView: 'listDay',
        aspectRatio: 1,
        header: {
            left: 'title',
            center: '',
            right: 'prev,next'
        },
        eventClick: function (event) {
            if (event.url) {
                window.open(event.url);
                return false;
            }
            if (event.id == 1 || event.id == 2) {
                debugger;
                var first = event.start._i;
                var second = first.replace('-', '.');
                var start = second.replace('-', '.');
                $('body').css('cursor', 'default');
                $(location).attr('href', 'ManageJobsRepairsFaults.aspx?start=' + start + '&type=' + event.id);
            }
            else if (event.id) {
                $('#ModalEventDetailsCompletedBy')[0].selectedIndex = event.nominatedIndex;
                $('#ModalEventDetailsId').val(event.id);
                $('#ModalEventInfoId').val(event.id);
                $('#ModalEventDetailsName').text(event.title);
                $('#ModalEventInfoName').text(event.title);
                var eventType = 'Compliance Task';
                $('#ModalEventDetailsType').val(event.eventType);
                $('#ModalEventInfoType').val(event.eventType);
                $('#ModalEventDetailsTypeText').text(eventType);
                $('#ModalEventInfoTypeText').text(eventType);
                $('#ModalEventDetailsFrequency').text(event.eventFrequency);
                $('#ModalEventInfoFrequency').text(event.eventFrequency);
                $('#ModalEventDetailsActionBy').text(event.eventAssigned);
                $('#ModalEventInfoBookedCheckBox').prop('checked', (event.infoStatus == 'B'));
                $('#ModalEventInfoBooked').val(event.infoStatus == 'B');
                $.ajax({
                    type: 'GET',
                    url: './webapi/tasks_get_notes.aspx?id=' + event.id,
                    success: function (data) { $('#ModalEventInfoOriginalNotes').val(data); $('#ModalEventInfoNotes').text(data); }
                });
                $.ajax({
                    type: 'GET',
                    url: './webapi/tasks_get_document_count.aspx?id=' + event.id,
                    success: function (data) { $('#ModalEventInfoDocuments').val(data); $('#ModalEventInfoDocuments').text(data); }
                });
                $('#modal-event-task-select').modal();
            }
        },
        events: './webapi/events.aspx?month=0',
    });

});