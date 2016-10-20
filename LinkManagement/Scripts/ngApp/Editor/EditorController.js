linkApp.controller("EditorController", function ($scope, $http, AjaxService, $rootScope) {
    $scope.topics = [];
    $scope.newTopic = {};
    var selectedTopicID = 0;
    var parentIDOfNewTopic = 0;

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

    $scope.AddNewTopic = function () {

        var len = $scope.topics.length;
        $scope.newTopic.ParentID = parentIDOfNewTopic;
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

    $scope.StartEditing = function () {
        $scope.showEditor = true;

        if (selectedTopicID == 0) {
            var len = $scope.topics.length;
            selectedTopicID = $scope.topics[len-1][0].ParentID;
        }

        var params = {topicID : selectedTopicID};
        AjaxService.Get("Editor/GetTopicContents", params)
            .then(function (response) {
                $scope.selectedTopicContent = response.data;
                
            }, function (response) {
                alert("error occured");
            });
    }

    $scope.SaveChanges = function () {


        var i = 0;
        $(".link-div").each(function () {
           
            var order = $(".link-div").index(this) + 1;
            var linkID = $(".link-div").attr("id");

            $scope.selectedTopicContent.Links.forEach(function (value, index) {
                if (value.LinkID == linkID) {
                    value.Order = order;
                }
            });
        });

        var params = { updatedTopic: $scope.selectedTopicContent }
        AjaxService.Get("Editor/UpdateTopicContent", params)
            .then(function (response) {
                alert("updated");
            }, function () {
                alert("error occured");
            });
    }

});