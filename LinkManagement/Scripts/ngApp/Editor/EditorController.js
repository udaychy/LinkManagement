linkApp.controller("EditorController", function ($scope, AjaxService, $rootScope) {
    $scope.newTopic = {};
    $scope.newLink = [];
    $scope.breadCrumbs = [];
    var selectedTopicID = 0;
    var parentIDOfNewTopic = 0;
    var tmpLinksList = [];// for the sortable ui of links inside topic
    var tmpTopicsList = [];// for the sortable ui of topics
    $scope.notification = {};// object contains notification messages 


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
                $scope.newTopic = null;

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
            ShowMessage("Please choose a topic first", "alert");
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
                ShowMessage("Changes saved successfully", "notify");
            }, function () {
                alert("error occured");
            });
    }


    $scope.DeleteTopic = function () {
        var params = { topicID: $scope.selectedTopicContent.TopicID }
        AjaxService.Post("Editor/DeleteTopic", params)
            .then(function (response) {
                ShowMessage("Topic Deleted successfully", "notify");
            }, function () {
                alert("error occured");
            });
    }


    $scope.DeleteLink = function (index) {
        $scope.selectedTopicContent.Links[index].IsDeleted = true;
    }
    //--------------------Editor Controls---------------------------------
   
    var focussedEvent = null;
    $scope.FocusedTextarea = function (event) {
        focussedEvent = event;
        
    }


    $scope.MakeBold = function (isControlsEnabled) {
        
        if (focussedEvent != null && isControlsEnabled)
        {
            var sel = $(focussedEvent.target).getSelection();
            if (sel.text == "") {
                sel.text = "strong text";
            }
            $(focussedEvent.target).replaceSelectedText("<b>" + sel.text + "</b>");
            $(focussedEvent.target).setSelection(sel.end + 3, sel.end + 14);
        }
    }

    $scope.MakeItalic = function (isControlsEnabled) {

        if (focussedEvent != null && isControlsEnabled) {
            var sel = $(focussedEvent.target).getSelection();
            if (sel.text == "") {
                sel.text = "Italic text";
            }
            $(focussedEvent.target).replaceSelectedText("<em>" + sel.text + "</em>");
            $(focussedEvent.target).setSelection(sel.end + 4, sel.end + 15);
        }
    }

    $scope.AddCode = function (isControlsEnabled) {

        if (focussedEvent != null && isControlsEnabled) {
            var sel = $(focussedEvent.target).getSelection();
            if (sel.text == "") {
                sel.text = "code here";
            }
            $(focussedEvent.target).replaceSelectedText("<pre><xmp>" + sel.text + "</xmp></pre>");
            $(focussedEvent.target).setSelection(sel.end + 10, sel.end + 19);
        }
    }

    $scope.MakeBlock = function (isControlsEnabled) {

        if (focussedEvent != null && isControlsEnabled) {
            var sel = $(focussedEvent.target).getSelection();
            if (sel.text == "") {
                sel.text = "write here";
            }
            $(focussedEvent.target).replaceSelectedText("<blockquote>" + sel.text + "</blockquote>");
            $(focussedEvent.target).setSelection(sel.end + 12, sel.end + 22);
        }
    }

    $scope.ShowPreview = function (previewContent) {
        $scope.currentPreview = previewContent;
    };


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
            $scope.topicSortedOrder = tmpTopicsList.map(function (i) {
                return i.Order;
            });
            alert($scope.subTopicList.map(function (i) { return i.Order}));
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
        $scope.selectedTopicContent.Links.forEach(function (link) {
            if (link.LinkHeading == null || link.LinkHeading == "") {
                ShowMessage("Link Heading cannot be empty", "alert");
                return false;
            }
        });
        return true;
    }

});