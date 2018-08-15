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
            if (event.id) {
                $('#ModalEventDetailsCompletedBy')[0].selectedIndex = event.nominatedIndex;
                $('#ModalEventDetailsId').val(event.id);
                $('#ModalEventDetailsName').text(event.title);
                $('#ModalEventDetailsType').val(event.eventType);
                var eventType = 'Compliance Task';
                $('#ModalEventDetailsTypeText').text(eventType);
                $('#ModalEventDetailsFrequency').text(event.eventFrequency);
                $('#ModalEventDetailsActionBy').text(event.eventAssigned);
                var today = new Date();
                var date = today.getDate() + "/" + today.getMonth() + "/" + today.getFullYear();
                $('#ModalEventDetailsActionDate').val(date);
                $('#ModalEventDetailsActionTime').val(today.getHours() + ":" + today.getMinutes());
                openData();
            }
        },
        events: './webapi/events.aspx?month=0',
    });

});


function openData() {
    document.getElementById("data-container").style.height = "90%";
    document.getElementById("main").style.display = "none";
    document.getElementById('nav-gliph').style.display = "none";
}

function closeData() {
    document.getElementById("data-container").style.height = "0";
    document.getElementById("main").style.display = "initial";
    document.getElementById("main").style.marginTop = "0";
    document.getElementById('nav-gliph').style.display = "initial";
}