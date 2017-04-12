var UrlController = function ($scope, UrlFactory) {
    $scope.urlForm = {
        url: '',
        shortUrl: '',
        error: ''
    };

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
}

UrlController.$inject = ['$scope', 'UrlFactory'];