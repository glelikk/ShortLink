var ListFactory = function ($http, $q) {
    return function () {

        var deferredObject = $q.defer();

        $http.get('/api/links').
            success(function (data) {
                if (data != null) {
                    deferredObject.resolve({
                        success: true,
                        data: data
                    });
                } else {
                    deferredObject.resolve({ success: false });
                }
            }).
            error(function () {
                deferredObject.reject({ success: false });
            });

        return deferredObject.promise;
    }
}

ListFactory.$inject = ['$http', '$q'];