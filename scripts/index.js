    function authenticateLogin() {
        var user = document.getElementById("username").value;
        var password = document.getElementById("password").value;
        var urlpath = "https://passmsclient.azurewebsites.net/webapi/mobile_login?user=" + user + "&pass=" + password;
        console.log(urlpath);


        $.ajax({
            type: 'GET',
            dataType: 'json',
            url: urlpath,
            success: function (data) {
                window.got

            }
        });
        console.log("BLEH");
        window.location.href = "http://google.com"
    }