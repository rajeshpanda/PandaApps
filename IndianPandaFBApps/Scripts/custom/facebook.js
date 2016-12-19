
function statusChangeCallback(response,method) {
    if (response.status === 'connected' && response.authResponse.accessToken) {
        $.cookie("panda-apps-data", response.authResponse.accessToken);
        if (method == 'login') {
            window.location = "/Apps/Index";
        }
    } else if (response.status === 'not_authorized') {
        appLogin(method);
    } else {
        appLogin(method);
    }
}

function checkLoginState(method) {
    FB.getLoginStatus(function (response) {
        statusChangeCallback(response, method);
        postLogin(method);
    });

}

function appLogin(method) {
    FB.login(function (response) {
        statusChangeCallback(response, method);
        postLogin(method);
        
    }, { scope: 'public_profile,publish_actions' });
}

function publish(link,src,desc) {

    FB.ui({
        method: "feed",
        link: link,
        caption: "apps.RajeshPanda.in",
        description: desc,
        picture: src
    },
    function (response) {
        if (response && !response.error_message) {
            $('#page_message').css('color', 'green');
            $('#page_message').html('Post is published');
            $('.publish').css('display', 'none');
        } else {
            $('#page_message').css('color', 'red');
            $('#page_message').html('Failed to publish!!');
        }
    });
}


window.fbAsyncInit = function () {
    FB.init({
        appId: '963044273824652',
        cookie: true,
        xfbml: true,
        version: 'v2.8'
    });

};

(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) { return; }
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

function goToAllApps() {
    window.location.href = "http://apps.rajeshpanda.in/Apps/Index";
}