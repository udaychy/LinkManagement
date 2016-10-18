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

                    $rootScope.IsLogedIn = true;
                    $rootScope.UserData = response.data;
                    localStorageService.set('userData', response.data);
                    localStorageService.set('isLogedIn', true);
                   
                    window.history.back();
                }
                else {
                    alert('Invalid Credential!');
                }
            });
        }
    };


    $scope.SignOut = function () {

        LoginService.LogOut().then(function (response) {
            $rootScope.UserData = null;
            $rootScope.IsLogedIn = false;
            localStorageService.remove('userData', 'isLogedIn');

            window.location.href = "#/";
        });
    }

    $scope.init = function () {
        LoginService.IsLogedIn().then(function (response) {
            if (response.data) {
                $rootScope.UserData = localStorageService.get('userData');
                $rootScope.IsLogedIn = localStorageService.get('isLogedIn');
            }
            else {
                $rootScope.UserData = null;
                $rootScope.IsLogedIn = false;
            }
        });
    }

});
