/// <reference path="../../angular.js" />
linkApp.controller("SubTopicController", function ($scope, $routeParams, $location, AjaxService, $rootScope) {

    $scope.notification = {};
    var parentID = parseInt($routeParams.parentID);

    AjaxService.Get("/SubTopics/GetImmediateChildrenWithSubTopicCount", { parentID: parentID })
        .then(function (response) {
            $scope.subTopicList = response.data;
            
            if ($scope.subTopicList.length < 1) {
                window.history.back();
            }
        },
          function (response) {
                $scope.error = response.data;
        });


    /* Get all the parents list*/
    AjaxService.Get( "/SubTopics/GetBreadCrumbsList", {topicID: parentID})
        .then(function (response) {
            $scope.breadCrumbsList = response.data;
         },
           function (response) {
               $scope.error = response.data;
         });

    

    $scope.CountReadLinks = function (topicID) {
        var countRead = 0;
        angular.forEach($scope.subTopicList, function(value, key) {
            if (value.TopicID == topicID) {
                countRead = 0;
                angular.forEach(value.Links, function (v, k) {
                    if (v.LinkUserMappings[0] != null && v.LinkUserMappings[0].Status)
                    countRead++;
                })
            }
        });
        return countRead;
    }


    $scope.updateLinkStatus = function (linkID) {
        
        if (!$rootScope.IsLogedIn) {
            window.location.href = "/LearningPath#/SignIn";
        }
        else {
            AjaxService.Get("/SubTopics/UpdateLinkStatus", { linkID: linkID })
                .then(function (response) {
                    if (response.data == true) {
                        $scope.ShowMessage("Progress Updated", "notify");
                    }
                },
            function (response) {
                alert("some error occured");
            });
        }
    }


    $scope.AddNote = function (linkID, note) {

         params = {
                linkID: linkID,
                note:note
         }

        AjaxService.Get("/SubTopics/AddNote", params)
            .then(function (response) {
                if (response.data == true) {
                    $scope.ShowMessage("Note Updated", "notify");
                }
                else {
                    window.location.href = "#/SignIn";
                }
        },
        function (response) {
            alert("some error occured");
        });
    }


    $scope.AddRating = function (linkID, rating) {
        
        params= {
            linkID: linkID,
            rating: rating
        }
        
        AjaxService.Get("/SubTopics/AddRating", params)
            .then(function (response) {
                if (response.data == true) {
                    $scope.ShowMessage("Rating Updated", "notify");
                }
                else {
                    window.location.href = "#/SignIn";
                }
        },
        function (response) {
            alert("some error occured");
        });
    }


    $scope.CountOneMoreVisitor = function (linkID) {

        AjaxService.Get("/SubTopics/CountOneMoreVisitor", {linkID: linkID})
            .then(function (response) {
        },
        function (response) {
            alert("some error occured");
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
 
        var element = document.getElementById("topicID-"+idName);
        var pos = absolutePosition(element);

        $('html, body').animate({
            scrollTop : pos.top-60
        }, 500);

        setTimeout(function () {
            $("#list-div > ul >li").removeClass("active");
            $("[list-id=" + idName + "]").addClass("active");
        },500);
    };


    $scope.ShowMessage = function (msg, msgType) {
        $scope.notification.message = msg;

        if (msgType == "notify") {
            $scope.notification.showOkButton = false;
            $scope.notification.showUndoButton = false;
            setTimeout(function () { $("#message-modal").modal('hide') }, 2000);
        }
        else if (msgType == "confirm") {
            $scope.notification.showOkButton = true;
            $scope.notification.showUndoButton = true;
        }
        else if (msgType == "alert") {
            $scope.notification.showOkButton = true;
            $scope.notification.showUndoButton = false;
        }
        $("#message-modal").modal('show');
    }

});
