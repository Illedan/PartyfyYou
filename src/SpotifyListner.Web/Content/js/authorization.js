if (!window.console) console = {};
console.log = console.log || function () { };

function httpGet(theUrl) {
    var xmlHttp = new XMLHttpRequest();
    
    xmlHttp.open("GET", theUrl, false); // false for synchronous request
    xmlHttp.setRequestHeader('Access-Control-Allow-Headers', '*');
    xmlHttp.setRequestHeader('Access-Control-Allow-Origin', '*');
    xmlHttp.send(null);
    return xmlHttp.responseText;
}


//var url = "https://accounts.spotify.com/authorize/?client_id=dfce289f6499436bbd1d60033ac14957&response_type=code&redirect_uri=https://localhost:8000/callback&scope=user-read-private%20user-read-email&state=34fFs29kd09";

var url = "https://accounts.spotify.com/authorize/?client_id=dfce289f6499436bbd1d60033ac14957&response_type=token&redirect_uri=http://localhost:1337/&state=XSS&scope=playlist-read-private%20user-read-private%20user-read-email%20user-library-read%20user-follow-read%20user-read-birthdate%20user-top-read%20playlist-read-collaborative%20user-read-recently-played%20user-read-playback-state%20user-modify-playback-state&show_dialog=False";
//var result = httpGet(url);
//console.log(result);

//access_token=BQAWjNeJdSGRlf5tCkQdPPzUYin5RXi9ym-RVNANAiJzQdtItiFMypgvr1k-UpVuSrUROw6c8s5iDvYCTcbWtopjic5rjX53JLTKajZo1xpJlCqM8B8fsITIs9yQMM6EkFdhuZ4hlrHH-oISBP04h6wYTPZs7HOfG17KJMyg4gF2Dsv51F7sYSfb2XUcGYDd9ZFoPOaLYyYAvwoOjpw1jxs4UF1RiA&token_type=Bearer&expires_in=3600&state=XSS

var result = httpGet(url);

console.log(result);

//var x = window.open(url);


//while (asd = ! "about:blank") {
   
//}
//var asd = x.location.href;
//alert(asd);
//console.log(asd);

//console.log(x);
