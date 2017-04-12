var UrlController = function ($scope, UrlFactory, ListFactory) {
    $scope.urlForm = {
        url: '',
        shortUrl: '',
        error: ''
    };

    $scope.urlList = {};

    

    $scope.getLink = function () {
        var result = UrlFactory($scope.urlForm.url);
        result.then(function (result) {
            if (result.success) {
                $scope.urlForm.shortUrl = result.data.shortLink;

            } else {
                $scope.urlForm.shortUrl = '';
                $scope.urlForm.error = 'Error. Try again.';
            }
        });
    }

    $scope.getList = function () {
        var result = ListFactory();
        result.then(function (result) {
            if (result.success) {
                $scope.urlList = result.data;
            } 
        });
    }
}

UrlController.$inject = ['$scope', 'UrlFactory', 'ListFactory'];