var UrlFactory = function ($http, $q) {
    return function (url) {

        var deferredObject = $q.defer();

        $http.post(
                '/api/links', {
                    url: url
                }
            ).
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

UrlFactory.$inject = ['$http', '$q'];