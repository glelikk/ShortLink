var app = angular.module('app', ['ngRoute', 'ngclipboard']);
app.controller('HomeController', HomeController);
app.controller('HeaderController', HeaderController);
app.controller('UrlController', UrlController);
app.controller('ListController', ListController);
app.factory('UrlFactory', UrlFactory);
app.factory('ListFactory', ListFactory);
var config = function($routeProvider) {
    $routeProvider.when('/links',
        {
            templateUrl: 'scripts/views/linksView.html',
            controller: ListController
        }).when('/',
        {
            templateUrl: 'scripts/views/homeView.html',
            controller: UrlController
        }).otherwise({
            templateUrl: 'scripts/views/homeView.html',
            controller: UrlController
    });
};
config.$inject = ['$routeProvider'];

app.config(config);