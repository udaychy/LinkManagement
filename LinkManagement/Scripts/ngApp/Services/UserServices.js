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

    fac.AuthenticateToken = function(token){
        return $http({
            method: "GET",
            url: '/User/AuthenticateToken',
            params: {
                token: token
            }
           
        });
    }
    return fac;

});   