﻿'use spotify';
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
    return httpGetRequest(apiUrlBase + '/url?token=' + tokenResponse.access_token, callback);
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
function loadedFrame() {
    var responseString = localStorage.getItem("responseString");
    tokenResponse = JSON.parse(responseString); // har access_token her
    
    function loop() {
        GetPlayingSong(songIdReturned);
		var timeTicketFetchedString = localStorage.getItem("timeTicketFetched");
        var timeTicketFetched = JSON.parse(timeTicketFetchedString);
        if (timeTicketFetchedString === null) {
             RefreshToken(tokenReturned);
        } else
        {
            var timeDifference = (Date.now() - timeTicketFetched)/60000;
            //console.log("timeDifference: " + timeDifference);
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