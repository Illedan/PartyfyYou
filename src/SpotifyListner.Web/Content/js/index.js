if (!window.console) console = {};
console.log = console.log || function () { };
var isLoaded = false;
var currentSong = "id";

function httpGet(theUrl, callback) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", theUrl, true); // false for synchronous request
    xmlHttp.onload = function (e) {
        callback(xmlHttp.responseText);
    };
    xmlHttp.onerror = function (e) {
    };
    xmlHttp.send(null);
}

function getPath(callback) {

    httpGet(window.location.href + "/url", callback);
}

function createYoutubeUrl(id) {
    return "https://www.youtube.com/embed/" + id + "?autoplay=1";
}

function pauseVideo() {
    httpGet(window.location.href + "/pause/" + currentSong, function (ignored) { });
}

function OnNewPath(newPath) {
    if (currentSong !== newPath) {
        document.getElementById('Myframe').src = createYoutubeUrl(newPath);
        currentSong = newPath;
        pauseVideo();
    }
}

function changeSrc() {
    if (!isLoaded) {
        loop();
        isLoaded = true;
        setInterval(function () {
            loop();
        }, 1000);
    }

    function loop() {
        getPath(OnNewPath);
    }
}