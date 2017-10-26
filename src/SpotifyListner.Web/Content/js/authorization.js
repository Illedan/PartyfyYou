'use spotify';
if (!window.console) console = {};
console.log = console.log || function () { };
var awsome = "";

function ExtractData(str) {
    return str.split("=")[1];
}

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
        //document.getElementById('myfield').innerHTML = w.location.href;
        localStorage.setItem("code", w.location.href);
        //window.location.assign("http://localhost:1337/videopage");
        window.location.href = 'http://localhost:1337/videopage';
        //window.location = "/videopage";
        //TODO: make this work.
        window.document.getElementById("myspan").innerHTML = w.location.href;
        //window.document.getElementById('button').style.visibility = 'hidden';
        //var content = w.location.href.split("?")[1];
        //var items = content.split("&");
        //var code = ExtractData(items[0]);
        ////var token_type = ExtractData(items[1]);
        ////var expires_in = ExtractData(items[2]);
        ////httpGet(window.location.href + '/asd?token=' + token);
        //localStorage.setItem("token_type", token_type);
        //localStorage.setItem("expires_in", expires_in);
        //window.location = '/videopage/';

        //var spotify = new SpotifyWebApi();
        //spotify.setAccessToken(token);
        //spotify.setVolume(40);
        //console.log('hert');
    };
}

function httpGet(theUrl) {
        var xmlHttp = new XMLHttpRequest();
        xmlHttp.open("GET", theUrl, true); // false for synchronous request
        //xmlHttp.withCredentials = true;
        //xmlHttp.setRequestHeader('Authorization', 'Bearer ' + authorization);
        //xmlHttp.setRequestHeader('Access-Control-Allow-Headers', '*');
        //xmlHttp.setRequestHeader('Access-Control-Allow-Origin', '*');
        xmlHttp.send(null);
        xmlHttp.onload = function () {
            document.getElementById('myfield').innerHTML = xmlHttp.responseText;
        };
    }



//var url = "https://accounts.spotify.com/authorize/?client_id=dfce289f6499436bbd1d60033ac14957&response_type=code&redirect_uri=https://localhost:1337/callback&scope=user-read-private%20user-read-email&state=34fFs29kd09";

//var url = "https://accounts.spotify.com/authorize/?client_id=dfce289f6499436bbd1d60033ac14957&response_type=token&redirect_uri=http://localhost:1337/&state=XSS&scope=playlist-read-private%20user-read-private%20user-read-email%20user-library-read%20user-follow-read%20user-read-birthdate%20user-top-read%20playlist-read-collaborative%20user-read-recently-played%20user-read-playback-state%20user-modify-playback-state&show_dialog=False";
//var result = httpGet(url);
//console.log(result);



//var x = window.open(url);


//while (asd = ! "about:blank") {
   
//}
//var asd = x.location.href;
//alert(asd);
//console.log(asd);

//console.log(x);
