linkApp.factory('LoginService', function (AjaxService) {
    var fac = {};
    fac.AuthenticateUser = function (data) {
        AjaxService.Get('/User/AuthenticateUser', data);
    };


    fac.LogOut = function () {
        return AjaxService.Get('/User/LogOut');
    };


    fac.IsLogedIn = function () {
        return AjaxService.Get('/User/IsLogedIn');
    };

    return fac;

});   