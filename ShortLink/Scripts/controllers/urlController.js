var UrlController = function ($scope, UrlFactory) {
    $scope.urlForm = {
        url: '',
        shortUrl: '',
        error: ''
    };

    $scope.getLink = function () {
        $scope.urlForm.error = '';
        var result = UrlFactory($scope.urlForm.url);
        result.then(function (result) {
            if (result.success) {
                $scope.urlForm.shortUrl = result.data.shortLink;

            } else {
                console.log("error");
                $scope.urlForm.shortUrl = '';
                $scope.urlForm.error = result.message;
            }
        });
    }
}

UrlController.$inject = ['$scope', 'UrlFactory'];