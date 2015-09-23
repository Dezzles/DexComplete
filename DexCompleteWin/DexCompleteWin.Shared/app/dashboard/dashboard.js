'use strict';

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
}

])/**/

.controller('DashboardCtrl', ['$scope', '$cookieStore', 'DexComplete', '$rootScope', function ($scope, $cookieStore, DexComplete, $rootScope) {
    $scope.viewType = 'pokedex'
    var user = $cookieStore.get('user');
    $scope.user = user.Username;
    $rootScope.gameIdentifier = null;
    DexComplete.Users.GetAllGames(user.Username, function (Result) {
        if (Result.Result == 0) {
            $scope.games = Result.Value;
        }

    })

}]);

    