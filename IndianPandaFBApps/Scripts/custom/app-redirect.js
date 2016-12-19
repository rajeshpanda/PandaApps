//function appRedirect(method) {
//    checkLoginState(method);
//    //get cookie
//    if (!$.cookie("panda-apps-data")) {
//        appLogin(method);
//    }
    
//}

function appRedirect(method) {
    if (method) {
        window.location.href = 'http://apps.rajeshpanda.in/Apps/' + method;
    }
}