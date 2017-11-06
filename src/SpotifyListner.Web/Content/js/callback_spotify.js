$(document).ready(function () {

    var accepted_origin = "http://localhost:1337/";

    var callbackString = window.location.href;
    var array = callbackString.split("=");
    var spotifyCode = array[1];

    if (spotifyCode) {
       
        var codeResponse = { type: "access_code_spotify", code: spotifyCode};
       

        window.opener.postMessage(JSON.stringify(codeResponse), accepted_origin);
        window.close();
    }

});