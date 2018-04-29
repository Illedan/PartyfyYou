'use spotify';
if (!window.console) console = {};
console.log = console.log || function () { };
var awsome = "";
var originalToken = "";
var apiUrlBase = window.location.protocol+ "//" + window.location.host;
function ExtractData(str) {
    return str.split("=")[1];
}
function login() {

    var CLIENT_ID = '8e6ef2d8a02843bc963ff9d6d2990fdc';
    var REDIRECT_URI = apiUrlBase + '/callback/';
    function getLoginURL(scopes) {
        console.log(REDIRECT_URI);
        return 'https://accounts.spotify.com/authorize?client_id=' + CLIENT_ID +
            '&redirect_uri=' + encodeURIComponent(REDIRECT_URI) +
            '&scope=' + encodeURIComponent(scopes.join(' ')) +
            '&response_type=code';
    }

    var loginUrl = getLoginURL([
        'user-read-private',
        'user-read-birthdate',
        'user-read-email',
        'user-read-currently-playing',
        'user-read-playback-state'
    ]);


    var width = 450,
        height = 730,
        left = (screen.width / 2) - (width / 2),
        top = (screen.height / 2) - (height / 2);

    var w = window.open(
        loginUrl,
        'Spotify',
        'menubar=no,location=no,resizable=no,scrollbars=no,status=no, width=' + width + ', height=' + height + ', top=' + top + ', left=' + left
    );

    window.addEventListener("message", function (event) {
        var codeResponse = JSON.parse(event.data);
        if (codeResponse.type == 'access_code_spotify') {
            callback(codeResponse.code);
        }
    }, false);

    var callback = function (token) {
        GetAccessToken(tokenReturned, token)


    };

}

function tokenReturned(responseString) {

    localStorage.setItem("responseString", responseString);

    window.location.replace(apiUrlBase + "/videopage");

}
function GetAccessToken(callback, token) {
    return httpGetRequest(apiUrlBase + '/token?code=' + token, callback);
}

function httpGet(theUrl) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", theUrl, true);
    xmlHttp.send(null);
    xmlHttp.onload = function () {
        document.getElementById('myfield').innerHTML = xmlHttp.responseText;
    };
}
function httpGetRequest(theUrl, callback) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", theUrl, true); // false for synchronous request
    xmlHttp.onload = function (e) {
        callback(xmlHttp.responseText);
    };
    xmlHttp.onerror = function (e) {
    };
    xmlHttp.send(null);
}


