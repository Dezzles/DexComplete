'use strict';

angular.module('dexComplete.userDropdown', ['ngRoute'])

.controller('UserDropdownCtrl', ['$scope', '$http', '$cookieStore', '$location', '$rootScope', function ($scope, $http, $cookieStore, $location, $rootScope) {
    $scope.validate = function () {
        var user = $cookieStore.get('user');
        $http.post("/api/v1/user/validate", user ).success(function (summary) {
            $scope.loggedIn = false;
            if (summary.Status == 0) {
                $scope.loggedIn = true;
            }
        });
    }
    $rootScope.$watch('user', function (newVal, oldVal) {
        if (newVal != null) {
            $scope.loggedIn = true;
            $scope.username = newVal.Username;
        }
        else {
            $scope.loggedIn = false;
            $scope.username = null;
        }
    }, true);

}])

.directive('userDropdown', function () {
    return {
        scope: {  },
        templateUrl: '/app/user/dropdown.html',
        controller: 'UserDropdownCtrl'
    }
}
);

