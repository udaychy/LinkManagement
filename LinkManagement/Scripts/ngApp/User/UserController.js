linkApp.controller("UserController", function ($scope, $rootScope, $window, localStorageService, LoginService) {
    
    $rootScope.IsLogedIn = false;
    $scope.Message = '';
    $scope.Submitted = false;
    $scope.IsFormValid = false;

    $scope.LoginData = {
        UserName: '',
        Password: ''
    };


    $scope.$watch('SignInForm.$valid', function (newVal) {
        $scope.IsFormValid = newVal;
    });

    $scope.Login = function () {
        $scope.Submitted = true;

        if ($scope.IsFormValid) {
            LoginService.AuthenticateUser($scope.LoginData).then(function (response) {

                if (response.data.UserID != null) {

                    localStorageService.set('UserData', response.data);
                    $rootScope.IsLogedIn = true;
                    $rootScope.UserData = localStorageService.get('UserData');
                    $rootScope.UserData.AccessToken = "";

                    window.history.back();
                }
                else {
                    alert('Invalid Credential!');
                }
            });
        }
    };

    $scope.SignOut = function () {

        $rootScope.UserData = null;
        $rootScope.IsLogedIn = false;
        localStorageService.remove('UserData');
    }

    $scope.init = function () {

        var userData = localStorageService.get('UserData');
        if (userData != null) {

            LoginService.AuthenticateToken(userData.AccessToken).then(function (response) {

                if (response.data == false) {
                    $rootScope.IsLogedIn = false;
                    $rootScope.UserData = null;
                }
                else {
                    $rootScope.IsLogedIn = true;
                    $rootScope.UserData = localStorageService.get('UserData');
                    $rootScope.UserData.AccessToken = "";
                }
            });
        }
    }

});
