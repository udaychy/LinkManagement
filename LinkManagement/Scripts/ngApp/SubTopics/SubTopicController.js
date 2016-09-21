/// <reference path="../../angular.js" />
linkApp.controller("SubTopicController", function ($scope, $http, $routeParams) {
    var parentID = parseInt( $routeParams.parentID);
    $http({
        method: "GET",
        url: "/SubTopics/GetImmediateChildren",
        params: {
            parentID: parentID
        }
    }).then(function (response) {

        $scope.subTopicList = response.data;
         },

           function (response) {

               $scope.error = response.data;
           });
});
