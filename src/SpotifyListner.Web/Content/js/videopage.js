'use spotify';
if (!window.console) console = {};
console.log = console.log || function () { };
 var mode = "";
var apiUrlBase = "http://"+ window.location.host;
 
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

function SetPlayMode(songMode) {
    mode = songMode;
    GetPlayingSong(songIdReturned);
}

function HandleWrongVideo() {
   
    
}

function VideoClicked(videoId) {
    songIdReturned(videoId);
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-left",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
    toastr.success('Thank you for making Partify better!','Saved');
    //document.getElementById("searchResult").innerHTML = null;
}

function GenerateSearchResult() {
    GetSearchResult(searchResultReturned);
    
}



function GetPlayingSong(callback) {
    return httpGetRequest(apiUrlBase + '/url?token=' + tokenResponse.access_token+"&mode="+mode, callback);
}
function GetSearchResult(callback) {
    return httpGetRequest(apiUrlBase + '/search?token=' + tokenResponse.access_token + "&mode=" + mode, callback);
}
function GetSongIdPlayedWithSpotify(callback) {
    return httpGetRequest(apiUrlBase + '/id?token=' + tokenResponse.access_token, callback);
}
function RefreshToken(callback) {
    return httpGetRequest(apiUrlBase + '/refreshToken?refreshToken=' + tokenResponse.refresh_token, callback);
}

function searchResultReturned(searchResultsText) {

    var text, fLen, i;
    var searchResults = JSON.parse(searchResultsText);

    fLen = searchResults.length;
    text = "<ul>";
    for (i = 0; i < fLen; i++) {
        text += "<li>" + "<div onclick=\"VideoClicked('" + searchResults[i].videoId + "')\"> " + "<p style=\"font- family:courier;color: white;\">" + searchResults[i].name + "</p> " + " <img  src='" + searchResults[i].thumbnailUrl + "'> </div></li>";
    }
    text += "</ul>";
    document.getElementById("searchResult").innerHTML = text;
}

function songIdReturned(songId) {
 
	var newUrl = createYoutubeUrl(songId);
	if(document.getElementById('Myframe').src !== newUrl){

        document.getElementById('Myframe').src = newUrl;
	    document.getElementById("searchResult").innerHTML = null;
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