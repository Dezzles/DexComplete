'use strict';

angular.module('dexComplete.dashboard', ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/', {
        templateUrl: 'app/dashboard/dashboard.html',
        controller: 'DashboardCtrl',
        auth: function (user) {
            return true;
        }
    });
    $routeProvider.when('', {
        templateUrl: 'app/dashboard/dashboard.html',
        controller: 'DashboardCtrl',
        auth: function (user) {
            return true;
        }
    });

}

])/**/

.controller('DashboardCtrl', ['$scope', '$cookieStore', 'DexComplete', '$rootScope', '$routeParams', function ($scope, $cookieStore, DexComplete, $rootScope, $routeParams) {
    DexComplete.Server.GetUpdates(
        { }, function (Result) {
            if (Result.Result == 0) {
                $scope.Updates = Result.Value.Updates;
                $scope.ComingSoon = Result.Value.ComingSoon;
            }
        })


}]);

    