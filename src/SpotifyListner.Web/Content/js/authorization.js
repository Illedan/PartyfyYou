'use spotify';
if (!window.console) console = {};
console.log = console.log || function () { };
var awsome = "";
var originalToken = "";
function login() {
    var CLIENT_ID = 'dfce289f6499436bbd1d60033ac14957';
    var REDIRECT_URI = 'http://localhost:1337/';
    function getLoginURL(scopes) {
        return 'https://accounts.spotify.com/authorize?client_id=' + CLIENT_ID +
            '&redirect_uri=' + encodeURIComponent(REDIRECT_URI) +
            '&scope=' + encodeURIComponent(scopes.join(' ')) +
            '&response_type=code';
    }

    var url = getLoginURL([
        'playlist-modify-public'
    ]);

    var width = 450,
        height = 730,
        left = (screen.width / 2) - (width / 2),
        top = (screen.height / 2) - (height / 2);

    
    var w = window.open(url,
        'Spotify',
        'menubar=no,location=no,resizable=no,scrollbars=no,status=no, width=' + width + ', height=' + height + ', top=' + top + ', left=' + left
    );
    
    w.onbeforeunload = function() {
        var text = w.location.href.split("=");
        var token = text[1];
        document.getElementById('myfield').innerHTML = 'loading';
        httpGet(window.location.href + '/asd?token=' + token);
		originalToken = token;

		GetNewToken(tokenReturned);
      
    };
}
function tokenReturned(token){
	var	newToken = token;
	console.log(newToken);
}
function GetNewToken(callback) {
	return httpGetRequest("http://localhost:1337" + '/token?code=' + originalToken, callback); //usikker på om man trenger å sende inn token, shit is strange
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


var url = "https://accounts.spotify.com/authorize/?client_id=dfce289f6499436bbd1d60033ac14957&response_type=token&redirect_uri=http://localhost:1337/&state=XSS&scope=playlist-read-private%20user-read-private%20user-read-email%20user-library-read%20user-follow-read%20user-read-birthdate%20user-top-read%20playlist-read-collaborative%20user-read-recently-played%20user-read-playback-state%20user-modify-playback-state&show_dialog=False";

login();

