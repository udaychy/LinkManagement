/// <reference path="../../angular.intellisense.js" />
linkApp.controller("EditorController", function ($scope, $http, AjaxService) {
    $scope.topics = [];
    $scope.GetSubTopics = function (topicID) {
        AjaxService.Get('Editor/GetSubtopics', {topicID : topicID})
            .then(function (response) {
                var len = $scope.topics.length;
                var index = $scope.topics.forEach(FindSelectedItem);
               
                $scope.topics.push(response.data);
                
            },
            function (response) {
                alert("error occured");
            });
    };

    FindSelectedItem = function (item, index) {
       
            return index;
    };
   
});