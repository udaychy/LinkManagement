linkApp.controller("HomeController", function ($scope, $http, $rootScope) {
    var url = "/Home/GetRootTopicList";
    $http.get(url)
           .then(function (response) {
              
               $scope.rootTopicList = response.data;
           },

           function (response) {

               $scope.error = response.data;
           });

});
