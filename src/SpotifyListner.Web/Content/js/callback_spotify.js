$(document).ready(function () {

	var apiUrlBase = "http://"+ window.location.host;

    var callbackString = window.location.href;
    var array = callbackString.split("=");
    var spotifyCode = array[1];

    if (spotifyCode) {
       
        var codeResponse = { type: "access_code_spotify", code: spotifyCode};
       

        window.opener.postMessage(JSON.stringify(codeResponse), apiUrlBase);
        window.close();
    }

});