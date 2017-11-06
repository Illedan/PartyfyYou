if (!window.console) console = {};
console.log = console.log || function () { };

function createYoutubeUrl(id) {
    return "https://www.youtube.com/embed/" + id + "?autoplay=1";
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
function GetPlayingSong(callback) {
    console.log(tokenResponse);
    return httpGetRequest("http://localhost:1337" + '/url?token=' + tokenResponse.access_token, callback);
}
function songIdReturned(songId) {

   
    document.getElementById('Myframe').src = createYoutubeUrl(songId);

}

var tokenResponse;
var isLoaded = false;
function loadedFrame() {
    
    //localStorage.setItem("token", token);
    //localStorage.setItem("token_type", token_type);
    //localStorage.setItem("expires_in", expires_in);
    if (!isLoaded) {

       
        isLoaded = true;
        try {
            
            var responseString = localStorage.getItem("responseString");
            
            tokenResponse = JSON.parse(responseString); // har access_token her
            
            if (tokenResponse !== null) {
                GetPlayingSong(songIdReturned);
            } else {
                console.log("token response from server is null");
            }
            
            document.getElementById("myspan").innerHTML = code;
        } catch (e) {
            document.getElementById('Myframe').src = createYoutubeUrl("aeWmdojEJf0");
        } 

        
        
    }
}