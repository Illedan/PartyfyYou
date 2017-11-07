if (!window.console) console = {};
console.log = console.log || function () { };
 
var apiUrlBase = "http://localhost:1337";
 
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
    return httpGetRequest(apiUrlBase + '/url?token=' + tokenResponse.access_token, callback);
}
function songIdReturned(songId) {
 
	var newUrl = createYoutubeUrl(songId);
	if(document.getElementById('Myframe').src !== newUrl){

		document.getElementById('Myframe').src = newUrl;
	}  
}
 
var tokenResponse;
var isLoaded = false;
function loadedFrame() {
    var responseString = localStorage.getItem("responseString");
    tokenResponse = JSON.parse(responseString); // har access_token her
 
    var timeTicketFetchedString = localStorage.getItem("timeTicketFetched");
    var timeTicketFetched = JSON.parse(timeTicketFetchedString);
    if (timeTicketFetched === null) {
        localStorage.setItem("timeTicketFetched", Date.now());
    } else {
        var timeDifference = Date.now() - timeTicketFetched;
        if (timeDifference<60000) {
            //get new token
            //set tokenResponse to the new token
           
            //set localstorage with new token
 
            //set localstorage with timeTicketFetched
        }
 
    }
    function loop() {
        GetPlayingSong(songIdReturned);
    }
    if (!isLoaded && tokenResponse !== null) {
 
      
        isLoaded = true;
        try {
           
            setInterval(function () {
                loop();
            }, 1000);
           
            
            
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