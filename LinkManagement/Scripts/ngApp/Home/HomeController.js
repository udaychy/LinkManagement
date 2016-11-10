linkApp.controller("HomeController", function ($scope, $http, $rootScope) {
    var url = "/Home/GetRootTopicList";
    $http.get(url)
           .then(function (response) {
              
               $scope.rootTopicList = response.data;
           },

           function (response) {

               $scope.error = response.data;
           });

    $rootScope.ShowMessage = function (msg, msgType) {
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
