/// <reference path="angular.min.js" />
var HomeApp = angular
    .module("HomeModule", [])
    .controller("TopicList", function ($scope, $http) {

            var url = "/Home/GetTopicListTree"
            $http.get(url).success(function (response) {
                $scope.topicList = response;
            
            });
           
    });

