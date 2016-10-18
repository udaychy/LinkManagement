linkApp.factory('LoginService', function ($http) {
    var fac = {};
    fac.AuthenticateUser = function (data) {
        return $http({
            url: '/User/AuthenticateUser',
            method: 'POST',
            data: JSON.stringify(data),
            headers: {'content-type':'application/json'}
        });
    };


    fac.LogOut = function () {
        return $http.get('/User/LogOut');
    };


    fac.IsLogedIn = function () {
        return $http.get('/User/IsLogedIn');
    };

    return fac;

});   