'use strict';

angular.module('dexComplete.logout', ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/user/logout', {
        templateUrl: 'app/login/logout.html',
        controller: 'LogoutCtrl',
        auth: function (user) {
            return true;
        }
    });

}

])
.controller('LogoutCtrl', ['$scope', '$rootScope', '$location', '$cookieStore', 'DexComplete', function ($scope, $rootScope, $location, $cookieStore, DexComplete) {
    var user = $cookieStore.get('user');/**/

    $scope.logout = function () {
        DexComplete.Users.Logout(
        {
        }, function (Result) {
                $location.path('$/');

        });
    }
}]);