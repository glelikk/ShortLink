var HeaderController = function ($scope, $location) {
    $scope.models = {
        title: 'ShortLink'
    };

    $scope.isActive = function (viewLocation) {
        return viewLocation === $location.path();
    };
}

HeaderController.$inject = ['$scope', '$location'];