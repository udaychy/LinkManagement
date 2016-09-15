/// <reference path="angular.min.js" />
var HomeApp = angular
    .module("HomeModule", [])
    .controller("Topic", function ($scope, $http, $log, $q) {

        var url = "/Home/GetTopicListTree";
        var deferred = $q.defer();

        function GetTopicList() {
            $http.get(url)
                .then(function (response) {

                    deferred.resolve(response.data);
                    $log.info(response);
                },

                function (response) {

                    $scope.error = response.data;
                    $log.info(response);
                });
            return deferred.promise;
        }

        var promise = GetTopicList();
        promise
            .then(function (list) {

                $scope.topicList = list;
            })
            .then(function () {
                alert("hello");
            });

    });

    
