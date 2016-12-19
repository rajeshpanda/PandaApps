$(document).ready(function () {
    $('#otherApps').load('http://apps.rajeshpanda.in/Apps#appList');
});

function getTerroistCount() {
    $('.sample-presentation-container').css('display', 'none');
    $('.hide-on-generate').css('display', 'none');
    $('.show-on-fetching').css('display', 'block');
    $('.loader').css('display', 'block');
    checkLoginState();
    if (!$.cookie("panda-apps-data")) {
        appLogin();
    }
}


function postLogin(method) {
    if ($.cookie("panda-apps-data")) {
        $('#main-content-img').css('display', 'none');
        $('.show-on-generating').css('display', 'block');
        $.ajax({
            url: "/Apps/GetSecretArmyMissionData",
            type : "GET",
            success: function (result) {
                
                if (result) {
                    $("#main-content-img").attr('src', result.url);
                    $("#descriptionText").html(result.description);
                    $('.show-on-fetching').css('display', 'none');
                    $('.show-on-generating').css('display', 'none');
                    $('.loader').css('display', 'none');
                    $('#main-content-img').css('display', 'block');
                    $('#descriptionText').css('display', 'block');
                    $('#page_message').html('Publish your results to Facebook');
                    $('#page_message').css('display', 'block');
                    $('.publish').css('display', 'block');
                }

            },
            error: function (err) {
                $('#page_message').html('Error occured while generating your report. Retry');
                $('.hide-on-generate').css('display', 'block');
            }
        });
    }
    else {

    }
}