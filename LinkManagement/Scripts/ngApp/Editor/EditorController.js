﻿linkApp.controller("EditorController", function ($scope, AjaxService, $rootScope) {
    $scope.topics = [];// array of all the topics selected 
    $scope.newTopic = {};
    $scope.newLink = [];
    $scope.breadCrumbs = [];
    var selectedTopicID = 0;
    var parentIDOfNewTopic = 0;
    var tmpLinksList = [];// for the sortable ui of links inside topic
    var tmpTopicsList = [];// for the sortable ui of topics

    $scope.GetSubTopics = function (topicID, selectedTopic, index) {
        if (topicID == undefined) {
            $scope.topics.splice(index + 1);
            selectedTopicID = 0;
        }
        else {
            if(selectedTopic != null)
            {
                selectedTopicID = selectedTopic.TopicID;
            }

            parentIDOfNewTopic = topicID;
            AjaxService.Get('Editor/GetSubtopics', { topicID: topicID })
                .then(function (response) {

                    $scope.topics.splice(index + 1);
                    $scope.topics.push(response.data);
                },
                function (response) {
                    alert("error occured");
                });
        };
    }

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

        var len = $scope.topics.length;
        $scope.newTopic.ParentID = parentIDOfNewTopic;//change
        $scope.newTopic.UserID = 1;
        $scope.newTopic.Order = $scope.topics[len - 1].length + 1; // initially setting order to last
        $scope.topics[len - 1].push($scope.newTopic);

        AjaxService.Get('Editor/AddNewTopic', $scope.newTopic)
            .then(function (response) {

                $scope.newTopic.TopicID = response.data;
                alert("Topic Added");
            },
            function (response) {
                alert("error occured");
            });
    }

    $scope.AddLink = function () {

        if ($scope.selectedTopicContent == null || $scope.selectedTopicContent == undefined){
            alert("choose a topic first");
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
        
        $("#editor-div > div:nth-child(2)").animate({
            scrollTop: 12000
        }, 500);
       
    }

    $scope.AddVideoLink = function () {

        if ($scope.selectedTopicContent == null || $scope.selectedTopicContent == undefined) {
            alert("choose a topic first");
            return;
        }
        var newVideoLink = {
            LinkHeading: "",
            LinkDetail: "",
            TopicID: $scope.selectedTopicContent.TopicID,
            Link: "",
            Description: "",
            LinkType: "video",
            Order: tmpLinksList.length + 1,
            IsDeleted: false
        };
        $scope.selectedTopicContent.Links.push(newVideoLink);

        $("#editor-div > div:nth-child(2)").animate({
            scrollTop: 12000
        }, 500);
    }
    

    $scope.StartEditing = function () {
        $scope.showEditor = true;

        if (selectedTopicID == 0) {
            var len = $scope.topics.length;
            selectedTopicID = $scope.topics[len - 1][0].ParentID;
        }

        var params = { topicID: selectedTopicID };
        AjaxService.Get("Editor/GetTopicContents", params)
            .then(function (response) {
                $scope.selectedTopicContent = response.data;
                tmpLinksList = $scope.selectedTopicContent.Links;

            }, function (response) {
                alert("error occured");
            });
    }

    $scope.SaveChanges = function () {

        $scope.selectedTopicContent.Links.forEach(function (value, index) {
            value.Order = index + 1;
        });
        
        var params = { updatedTopic: $scope.selectedTopicContent }

        AjaxService.Post("Editor/UpdateTopicContent", params)
            .then(function (response) {
                alert("updated");
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

    $scope.MakeBlock = function () {

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
            var logEntry = tmpTopicsList.map(function (i) {
                return i.Order;
            });
        },
        stop: function (e, ui) {
            // this callback has the changed model
            $scope.topicSortedOrder = tmpTopicsList.map(function (i) {
                return i.Order;
            });
        }
    };

});