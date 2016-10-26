linkApp.factory('LoginService',['AjaxService', function (AjaxService) {
    var fac = {};
    fac.AuthenticateUser = function (data) {
       return AjaxService.Get('/User/AuthenticateUser', data);
    };


    fac.LogOut = function () {
        return AjaxService.Get('/User/LogOut');
    };


    fac.IsLogedIn = function () {
        return AjaxService.Get('/User/IsLogedIn');
    };

    return fac;

}]);   