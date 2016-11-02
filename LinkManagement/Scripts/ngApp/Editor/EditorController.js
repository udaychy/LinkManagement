linkApp.controller("EditorController", function ($scope, AjaxService, $rootScope) {
    $scope.newTopic = {};
    $scope.newLink = [];
    $scope.breadCrumbs = [];
    var selectedTopicID = 0;
    var parentIDOfNewTopic = 0;
    var tmpLinksList = [];// for the sortable ui of links inside topic
    var tmpTopicsList = [];// for the sortable ui of topics
    $scope.notification = {};// object contains notification messages 

    $scope.editorOptions = {
        language: 'en'
    };

    $scope.GetSubTopicsList = function (topicID) {
        AjaxService.Get('Editor/GetSubtopics', { topicID: topicID })
               .then(function (response) {
                   $scope.subTopicList = response.data;
                   tmpTopicsList = $scope.subTopicList;
               },
               function (response) {
                   alert("error occured");
               });
    };

    $scope.BringTopicContent = function (topicID, index) {
       
        if (topicID == 0) {
            $scope.showEditor = false;
            $scope.selectedTopicContent = null;
        }
        var params = { topicID: topicID };

        if (index != undefined) {
            $scope.breadCrumbs.splice(index);
        }

        if (topicID > 0) {
            AjaxService.Get("Editor/GetTopicContents", params)
                .then(function (response) {
                    $scope.showEditor = true;
                    $scope.selectedTopicContent = response.data;
                    tmpLinksList = $scope.selectedTopicContent.Links;
                    $scope.breadCrumbs.push({
                        TopicName: $scope.selectedTopicContent.TopicName,
                        TopicID: $scope.selectedTopicContent.TopicID
                    });

                }, function (response) {
                    alert("error occured");
                });
        }
    }
    

    $scope.AddNewTopic = function () {
        
        if ($scope.newTopic.TopicName == "" || $scope.newTopic.TopicName == null) {
            ShowMessage("Topic name cannot be empty", "alert");
            return;
        }

        if ($scope.selectedTopicContent == null || $scope.selectedTopicContent == undefined) {
            $scope.newTopic.ParentID = 0;
        }
        else {
            $scope.newTopic.ParentID = $scope.selectedTopicContent.TopicID;
        }
        $scope.newTopic.UserID = 1;
        $scope.newTopic.Order = $scope.subTopicList.length + 1;
        
        AjaxService.Get('Editor/AddNewTopic', $scope.newTopic)
            .then(function (response) {

                $scope.newTopic.TopicID = response.data;
                var newTopic = angular.copy($scope.newTopic);
                $scope.subTopicList.push(newTopic);

                $scope.showTopicInput = false;
                $scope.newTopic = {};

                ShowMessage(newTopic.TopicName + " is succesfully added", "notify");
                
            },
            function (response) {
                alert("error occured");
            });
    }


    $scope.CancelAddingTopic = function () {
        $scope.showTopicInput = false;
        $scope.newTopic = {};
    }


    $scope.AddLink = function () {

        if ($scope.selectedTopicContent == null || $scope.selectedTopicContent == undefined){
            ShowMessage("No topic selected! Please choose a topic first", "alert");
            return;
        }

        var newLink = {
            LinkHeading: "",
            LinkDetail: "",
            TopicID: $scope.selectedTopicContent.TopicID,
            Link: "",
            Description: "",
            LinkType: "url",
            Order: tmpLinksList.length + 1
        };
        //** tmpLinksList automatically sinks with $scope.selectedTopicContent.Links
        $scope.selectedTopicContent.Links.push(newLink);
        
        $("#editor-div > div:nth-child(2) > .topic-content-div").animate({
            scrollTop: 12000
        }, 500);
       
    }


    $scope.SaveChanges = function () {
        
        if (!IsValidatedSelectedTopic()) {
            return;
        }

        $scope.selectedTopicContent.Links.forEach(function (value, index) {
            value.Order = index + 1;
        });
        
        var params = { updatedTopic: $scope.selectedTopicContent }

        AjaxService.Post("Editor/UpdateTopicContent", params)
            .then(function (response) {
                $scope.BringTopicContent($scope.selectedTopicContent.TopicID, $scope.breadCrumbs.length - 1)
                ShowMessage("Changes saved successfully", "notify");
            }, function () {
                alert("error occured");
            });
    }


    $scope.DeleteTopic = function () {
        if ($scope.selectedTopicContent == null || $scope.selectedTopicContent == undefined) {
            ShowMessage("No topic selected! Please choose a topic first", "alert");
            return;
        }

        var params = { topicID: $scope.selectedTopicContent.TopicID }
        AjaxService.Post("Editor/DeleteTopic", params)
            .then(function (response) {
                if ($scope.breadCrumbs.length > 1) {
                    $scope.breadCrumbs.splice($scope.breadCrumbs.length - 2);
                }
                else {
                    $scope.breadCrumbs = [];
                }
                $scope.GetSubTopicsList($scope.selectedTopicContent.ParentID);
                $scope.BringTopicContent($scope.selectedTopicContent.ParentID);
                
                ShowMessage("Topic Deleted successfully", "notify");

            }, function () {
                ShowMessage("Some error occured", "alert");
            });
    }


    $scope.DeleteLink = function (index) {
        $scope.selectedTopicContent.Links[index].IsDeleted = true;
    }
    

    //sortable contents
    $scope.sortedOrder = null;

    $scope.sortableLinks = {
        update: function (e, ui) {
            var logEntry = tmpLinksList.map(function (i) {
                return i.Order;
            });
        },
        stop: function (e, ui) {
            // this callback has the changed model
            $scope.sortedOrder = tmpLinksList.map(function (i) {
                return i.Order;
            });           
        }
    };

    $scope.sortableTopics = {
        update: function (e, ui) {
            var orderList = $scope.subTopicList.map(function (i) {
                return i.Order;
            });
        },
        stop: function (e, ui) {
            // this callback has the changed model
            var order = 1;
            $scope.topicSortedOrder = tmpTopicsList.map(function (i) {
                return { TopicID: i.TopicID, Order: order++ };
            });
            var params = { topicOrder: $scope.topicSortedOrder };
            AjaxService.Post("Editor/UpdateTopicOrder", params)
           .then(function (response) {
               ShowMessage("Topics are reordered", "notify");
           }, function () {

           });
        }
    };

    var ShowMessage = function (msg, msgType) {
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

    var IsValidatedSelectedTopic = function ()
    {
        if ($scope.selectedTopicContent.TopicName == "" || $scope.selectedTopicContent.TopicName == null) {
            ShowMessage("Topic Name cannot be empty", "alert");
            return false;
        }
        return $scope.selectedTopicContent.Links.every(function (link) {
            if (link.LinkHeading == null || link.LinkHeading == "") {
                ShowMessage("Link Heading cannot be empty", "alert");
                return false;
            }
            else if ((link.LinkType == "url" || link.LinkType == "video") && (link.Link1 == "" || link.Link1 == null)) {
                ShowMessage("Url cannot be empty! You can select Url type 'none' ", "alert");
                return false;
            }
            else {
                return true;
            }
        });
        return true;
    }

});