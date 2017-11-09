'use spotify';
if (!window.console) console = {};
console.log = console.log || function () { };
var awsome = "";
var originalToken = "";

function ExtractData(str) {
    return str.split("=")[1];
}
function login() {

        var CLIENT_ID = 'dfce289f6499436bbd1d60033ac14957';
        var REDIRECT_URI = 'http://tegster.asuscomm.com:1337/callback/';
        function getLoginURL(scopes) {
            return 'https://accounts.spotify.com/authorize?client_id=' + CLIENT_ID +
                '&redirect_uri=' + encodeURIComponent(REDIRECT_URI) +
                '&scope=' + encodeURIComponent(scopes.join(' ')) +
                '&response_type=code';
        }

        var loginUrl = getLoginURL([
            //'playlist-modify-public'
            'user-read-currently-playing',
            'user-read-playback-state'
        ]);


    var width = 450,
        height = 730,
        left = (screen.width / 2) - (width / 2),
        top = (screen.height / 2) - (height / 2);
   
   // http://localhost:1337/callback/
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
        GetAccessToken(tokenReturned,token)
        
       
    };

}

function tokenReturned(responseString){
    
    localStorage.setItem("responseString", responseString);
    
    window.location.replace("http://tegster.asuscomm.com:1337/videopage");
   
}
function GetAccessToken(callback, token) {
    return httpGetRequest("http://tegster.asuscomm.com:1337" + '/token?code=' + token, callback);
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


