﻿'use strict';

angular.module('dexComplete.dashboard', ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/', {
        templateUrl: 'app/dashboard/dashboard.html',
        controller: 'DashboardCtrl',
        auth: function (user) {
            return user != null;
        }
    });
    $routeProvider.when('', {
        templateUrl: 'app/dashboard/dashboard.html',
        controller: 'DashboardCtrl',
        auth: function (user) {
            return user != null;
        }
    });
    $routeProvider.when('/user/:user', {
        templateUrl: 'app/dashboard/dashboard.html',
        controller: 'DashboardCtrl',

    });
}

])/**/

.controller('DashboardCtrl', ['$scope', '$cookieStore', 'DexComplete', '$rootScope', '$routeParams', function ($scope, $cookieStore, DexComplete, $rootScope, $routeParams) {
    $scope.viewType = 'pokedex'
    var user = $cookieStore.get('user');
    $scope.user = user.Username;
    if ($routeParams.user != null)
        $scope.user = $routeParams.user;
    $rootScope.gameIdentifier = null;
    $scope.CanAddGame = (user.Username == $scope.user);
    DexComplete.Users.GetAllGames($scope.user, function (Result) {
        if (Result.Result == 0) {
            $scope.games = Result.Value;
        }

    })

}]);

    