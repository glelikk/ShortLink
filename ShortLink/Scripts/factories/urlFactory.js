var UrlFactory = function ($http, $q) {
    return function (url) {

        var deferredObject = $q.defer();

        $http.post(
                '/api/links', {
                    url: url
                }
            ).
            success(function (data) {
                if (data != null && data.success == undefined) {
                    deferredObject.resolve({
                        success: true,
                        data: data
                    });
                } else {
                    deferredObject.resolve(data);
                }
            }).
            error(function (data) {
                deferredObject.resolve(data);
            });

        return deferredObject.promise;
    }
}

UrlFactory.$inject = ['$http', '$q'];