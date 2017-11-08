'use spotify';
if (!window.console) console = {};
console.log = console.log || function () { };
 var mode = "";
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

function SetPlayMode(mode) {
    alert(mode);
    GetPlayingSong(songIdReturned);
}



function GetPlayingSong(callback) {
    return httpGetRequest(apiUrlBase + '/url?token=' + tokenResponse.access_token+"&mode="+mode, callback);
}
function GetSongIdPlayedWithSpotify(callback) {
    return httpGetRequest(apiUrlBase + '/id?token=' + tokenResponse.access_token, callback);
}
function RefreshToken(callback) {
    return httpGetRequest(apiUrlBase + '/refreshToken?refreshToken=' + tokenResponse.refresh_token, callback);
}
function songIdReturned(songId) {
 
	var newUrl = createYoutubeUrl(songId);
	if(document.getElementById('Myframe').src !== newUrl){

		document.getElementById('Myframe').src = newUrl;
	}  
}

function spotifySongIdReturned(songId) {
 
	if(songId != null){

		var storedCurrentlyPlaingId = localStorage.getItem("currentlyPlayingSpotifyId");
		if(storedCurrentlyPlaingId == null){
			localStorage.setItem("currentlyPlayingSpotifyId", songId);
			GetPlayingSong(songIdReturned);
			return;
		}
		if(loopCounter<2){
			localStorage.setItem("currentlyPlayingSpotifyId", songId);
			GetPlayingSong(songIdReturned);
			return;
		}
		if(songId === storedCurrentlyPlaingId){
			return;
		}
		localStorage.setItem("currentlyPlayingSpotifyId", songId);
		GetPlayingSong(songIdReturned);  
	}
	
}

function tokenReturned(token) {

	var responseString = localStorage.getItem("responseString");

	var storedToken = JSON.parse(responseString);

	var recievedToken = JSON.parse(token);

	var newToken = new Object();
	newToken.access_token = recievedToken.access_token;
	newToken.token_type = recievedToken.token_type;
	newToken.expires_in = recievedToken.expires_in;
	newToken.refresh_token = storedToken.refresh_token;
	newToken.scope = recievedToken.scope;

	localStorage.setItem("responseString", JSON.stringify(newToken));
    localStorage.setItem("timeTicketFetched", JSON.stringify(Date.now()));
}
 
var tokenResponse;
var isLoaded = false;
var loopCounter = 0;
function loadedFrame() {
    var responseString = localStorage.getItem("responseString");
    tokenResponse = JSON.parse(responseString); // har access_token her
    
    function loop() {
	loopCounter ++;
	   GetSongIdPlayedWithSpotify(spotifySongIdReturned);
		var timeTicketFetchedString = localStorage.getItem("timeTicketFetched");
        var timeTicketFetched = JSON.parse(timeTicketFetchedString);
        if (timeTicketFetchedString === null) {
             RefreshToken(tokenReturned);
        } else
        {
            var timeDifference = (Date.now() - timeTicketFetched)/60000;

            if (timeDifference>50) {
		        RefreshToken(tokenReturned);
            }
        }
    }

    if (!isLoaded && tokenResponse !== null) {
 
      
        isLoaded = true;
        try {
           
            setInterval(function () {
                loop();
            }, 1000);
           
            
            
            if (tokenResponse !== null) {
				GetSongIdPlayedWithSpotify(spotifySongIdReturned);
            } else {
                console.log("token response from server is null");
            }
           
        } catch (e) {
            document.getElementById('Myframe').src = createYoutubeUrl("zS5kvbe9Sv4");
        }
 
       
        
    }
}