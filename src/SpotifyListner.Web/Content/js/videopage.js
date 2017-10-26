
function createYoutubeUrl(id) {
    return "https://www.youtube.com/embed/" + id + "?autoplay=1";
}
var isLoaded = false;
function loadedFrame() {
    //localStorage.setItem("token", token);
    //localStorage.setItem("token_type", token_type);
    //localStorage.setItem("expires_in", expires_in);
    if (!isLoaded) {

        isLoaded = true;
        document.getElementById('Myframe').src = createYoutubeUrl("aeWmdojEJf0");
        try {
            var code = localStorage.getItem("code");
            document.getElementById("myspan").innerHTML = code;
        } catch (e) {

        } 
    }
}