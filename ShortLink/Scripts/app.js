var app = angular.module('app', ['ngRoute']);
app.controller('HomeController', HomeController);
app.controller('UrlController', UrlController);
app.factory('UrlFactory', UrlFactory);
app.factory('ListFactory', ListFactory);
var config = function($routeProvider) {
    $routeProvider.when('/links',
        {
            templateUrl: 'home/list',
            controller: UrlController
        }).when('/',
        {
            templateUrl: 'home/add',
            controller: UrlController
        }).otherwise({
            templateUrl: 'home/add',
            controller: UrlController
    });
};
config.$inject = ['$routeProvider'];

app.config(config);