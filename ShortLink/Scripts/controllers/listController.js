var ListController = function ($scope, ListFactory) {
    $scope.urlList = {};
    $scope.getList = function () {
        var result = ListFactory();
        result.then(function (result) {
            if (result.success) {
                $scope.urlList = result.data;
            }
        });
    }
    $scope.getList();
}

ListController.$inject = ['$scope', 'ListFactory'];