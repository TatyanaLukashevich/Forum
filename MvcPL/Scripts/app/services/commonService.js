flatApp.service('commonService', ['$http', '$q', function ($http, $q) {
    this.getCoordinates = function () {
        return $http.get("http://freegeoip.net/json/");
    };
}]);