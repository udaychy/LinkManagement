/// <reference path="../../angular.js" />
linkApp.controller("SubTopicController", function ($scope, $http, $routeParams, $location) {

    var parentID = parseInt($routeParams.parentID);
    
    $http({

        method: "GET",
        url: "/SubTopics/GetImmediateChildren",

        params: {
            parentID: parentID
        }

    }).then(function (response) {

            $scope.subTopicList = response.data;
            
            if ($scope.subTopicList.length < 1) {
                goBack();
            }
           
        },
          function (response) {

                $scope.error = response.data;
        });


    goBack = function () {
    
       window.history.back();
    };

    /* Get all the parents list*/
    $http({
        method: "GET",
        url: "/SubTopics/GetAllParents",
        params: {
            topicID: parentID
        }
    }).then(function (response) {

        $scope.parentsList = response.data;
    },
         function (response) {

             $scope.error = response.data;
         });

    var UpdateLinkStatus = function (topicID, linkID) {

        $http({
            method: "GET",
            url: "/SubTopics/UpdateLinkStatus",
            params: {
                topicID: topicID,
                linkID: linkID,
                isChecked : true
            }
        }).then(function (response) {

            alert("updated");
        },
        function (response) {

           
        });
    }

    var absolutePosition = function (element) {

        var top = 0, left = 0;

        do {
            top += element.offsetTop || 0;
            left += element.offsetLeft || 0;
            element = element.offsetParent;
        } while (element);

        return {
            top: top,
            left: left
        };
    };


    $scope.scrollTo = function (idName) {
 
        var element = document.getElementById(idName);
        var pos = absolutePosition(element);

        $('html, body').animate({
            scrollTop : pos.top-60
        }, 1000);
        
    };

});
