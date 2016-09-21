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

});