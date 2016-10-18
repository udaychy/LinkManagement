linkApp.config(function ($routeProvider) {

    $routeProvider
        // route for the home page
        .when('/', {
            templateUrl: '/Scripts/ngApp/Template/MainTopic.html',
            controller: 'HomeController'
        })

       .when('/SubTopics/:parentID', {
           templateUrl: '/Scripts/ngApp/Template/SubTopicList.html',
           controller: 'SubTopicController'
       })

       .when('/SignIn', {
           templateUrl: '/Scripts/ngApp/Template/SignIn.html',
           controller: 'UserController'
       })

       .when('/Editor', {
           templateUrl: '/Scripts/ngApp/Template/Editor.html',
           controller: 'EditorController'
       },
       
       function (localStorageServiceProvider) {
           localStorageServiceProvider
             .setPrefix('lm')
             .setStorageType('sessionStorage')
             .setNotify(true, true)
       });
     
}).filter('trustUrl', function ($sce) {

    return function (url) {
        return $sce.trustAsResourceUrl(url);
    };
});