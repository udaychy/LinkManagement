/// <reference path="../../angular.js" />
HomeApp.controller("HomeController", function ($scope, $http) {
    var url = "/Home/GetRootTopicList";
    $http.get(url)
           .then(function (response) {

               $scope.rootTopicList = response.data;
           },

           function (response) {

               $scope.error = response.data;
           });

});
