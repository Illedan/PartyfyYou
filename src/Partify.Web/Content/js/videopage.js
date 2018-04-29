'use spotify';
if (!window.console) console = {};
console.log = console.log || function () { };
var mode = "";
var apiUrlBase = window.location.protocol + "//" + window.location.host;

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
    switch (songMode) {
        case '':
            sessionStorage.setItem("modeGuid", "763BFA3C-60A2-483A-A0A2-3D70A46B45D1");
            break;
        case 'cover':
            sessionStorage.setItem("modeGuid", "763BFA3C-60A2-483A-A0A2-3D73A47B45D1");
            break;
        case 'karaoke':
            sessionStorage.setItem("modeGuid", "763BFA3C-60A2-483A-A0A2-3D71A47B45D1");
            break;
        case 'lyrics':
            sessionStorage.setItem("modeGuid", "24F97CA9-2797-44CD-8BF6-340C5274A1FE");
            break;
    }
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
    toastr.success('Thank you for making Partify better!', 'Saved');
    //document.getElementById("searchResult").innerHTML = null;
}

function GenerateSearchResult() {
    GetSearchResult(searchResultReturned);

}



function GetPlayingSong(callback) {
    // var playingVideoId = sessionStorage.getItem("currentlyPlayingVideoId");
    var storedUser = sessionStorage.getItem("storedUser");
    var user = JSON.parse(storedUser);
    var modeGuid = sessionStorage.getItem("modeGuid");
    if (user != null) {

        return httpGetRequest(apiUrlBase + '/url?token=' + tokenResponse.access_token + "&mode=" + mode + "&userId=" + user.Id + "&modeId=" + modeGuid, callback);


    }

    return httpGetRequest(apiUrlBase + '/url?token=' + tokenResponse.access_token + "&mode=" + mode, callback);
}
function StoreSuggestion() {
    // var playingVideoId = sessionStorage.getItem("currentlyPlayingVideoId");
    var storedUser = sessionStorage.getItem("storedUser");
    var user = JSON.parse(storedUser);
    var modeGuid = sessionStorage.getItem("modeGuid");
    var videoId = sessionStorage.getItem("currentlyPlayingVideoId");
    var songId = sessionStorage.getItem("currentlyPlayingSpotifyId");
    if (user != null && modeGuid && videoId && songId) {
        httpGetRequest(apiUrlBase + '/store?songId=' + songId + "&userId=" + user.Id + "&modeId=" + modeGuid + "&videoId=" + videoId);
    }


}
function GetSearchResult(callback) {
    return httpGetRequest(apiUrlBase + '/search?token=' + tokenResponse.access_token + "&mode=" + mode, callback);
}
function GetSongIdPlayedWithSpotify(callback) {
    return httpGetRequest(apiUrlBase + '/id?token=' + tokenResponse.access_token, callback);
}
function GetUser(callback) {
    var storedSpotifyUser = sessionStorage.getItem("storedSpotifyUser");
    var spotifyUser = JSON.parse(storedSpotifyUser);

    return httpGetRequest(apiUrlBase + '/user?Name=' + spotifyUser.display_name + "&Contry=" + spotifyUser.country + "&SpotifyUserId=" + spotifyUser.id, callback);
}
function GetSpotifyUser(callback) {
    return httpGetRequest(apiUrlBase + '/spotifyUser?token=' + tokenResponse.access_token, callback);
}
function RefreshToken(callback) {
    return httpGetRequest(apiUrlBase + '/refreshToken?refreshToken=' + tokenResponse.refresh_token, callback);
}

function spotifyUserReturned(userString) {
    sessionStorage.setItem("storedSpotifyUser", userString);
}
function userReturned(userString) {
    sessionStorage.setItem("storedUser", userString);
}

function searchResultReturned(searchResultsText) {

    var text, fLen, i;
    var searchResults = JSON.parse(searchResultsText);

    fLen = searchResults.length;
    text = "<ul>";
    for (i = 0; i < fLen; i++) {
        text += "<li>" + "<div onclick=\"VideoClicked('" + searchResults[i].VideoId + "')\"> " + "<p style=\"font- family:courier;color: white;\">" + searchResults[i].Name + "</p> " + " <img  src='" + searchResults[i].ThumbnailUrl + "'> </div></li>";
    }
    text += "</ul>";
    document.getElementById("searchResult").innerHTML = text;
}

function songIdReturned(songId) {

    var newUrl = createYoutubeUrl(songId);
    if (document.getElementById('Myframe').src !== newUrl) {
        sessionStorage.setItem("currentlyPlayingVideoId", songId);
        StoreSuggestion();
        document.getElementById('Myframe').src = newUrl;
        document.getElementById("searchResult").innerHTML = null;
    }
}

function spotifySongIdReturned(songId) {

    if (songId != null) {

        var storedCurrentlyPlaingId = sessionStorage.getItem("currentlyPlayingSpotifyId");
        if (storedCurrentlyPlaingId == null) {
            sessionStorage.setItem("currentlyPlayingSpotifyId", songId);
            GetPlayingSong(songIdReturned);
            return;
        }
        if (loopCounter < 2) {
            sessionStorage.setItem("currentlyPlayingSpotifyId", songId);
            GetPlayingSong(songIdReturned);
            return;
        }
        if (songId === storedCurrentlyPlaingId) {
            return;
        }
        sessionStorage.setItem("currentlyPlayingSpotifyId", songId);
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

sessionStorage.setItem("modeGuid", "763BFA3C-60A2-483A-A0A2-3D70A46B45D1");
var tokenResponse;
var isLoaded = false;
var loopCounter = 0;

function loadedFrame() {
    var responseString = localStorage.getItem("responseString");
    tokenResponse = JSON.parse(responseString); // har access_token her

    function loop() {
        loopCounter++;
        GetSongIdPlayedWithSpotify(spotifySongIdReturned);
        var timeTicketFetchedString = localStorage.getItem("timeTicketFetched");
        var timeTicketFetched = JSON.parse(timeTicketFetchedString);
        if (timeTicketFetchedString === null) {
            RefreshToken(tokenReturned);
        } else {
            var timeDifference = (Date.now() - timeTicketFetched) / 60000;

            if (timeDifference > 50) {
                RefreshToken(tokenReturned);
            }
        }
        var storedSpotifyUser = sessionStorage.getItem("storedSpotifyUser");
        var spotifyUser = JSON.parse(storedSpotifyUser);


        if (spotifyUser == null) {
            GetSpotifyUser(spotifyUserReturned);
        }
        var storedUser = sessionStorage.getItem("storedUser");
        var user = JSON.parse(storedUser);
        if (user == null) {
            GetUser(userReturned);
        }

        if (loopCounter % 5 == 1) {
            GetPlayingSong(songIdReturned);
            console.log("Hello " + user.Name);
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