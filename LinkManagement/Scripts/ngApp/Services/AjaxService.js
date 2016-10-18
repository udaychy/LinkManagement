linkApp.factory('AjaxService', function ($http) {
    var fac = {};
    fac.Get = function ( url, params) {
        return $http({
            method: "GET",
            url: url,
            params: params

        });
    }

    fac.Post = function (url, params) {
        return $http({
            method: "POST",
            url: url,
            params: params

        });
    }

    return fac;

});