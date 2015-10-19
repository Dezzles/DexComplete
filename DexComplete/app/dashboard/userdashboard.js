'use strict';

angular.module('dexComplete.userDashboard', ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/user/:user', {
        templateUrl: 'app/dashboard/userdashboard.html',
        controller: 'UserDashboardCtrl',

    });
}

])/**/

.controller('UserDashboardCtrl', ['$scope', '$cookieStore', 'DexComplete', '$rootScope', '$routeParams', function ($scope, $cookieStore, DexComplete, $rootScope, $routeParams) {
    $scope.viewType = 'pokedex'
    var user = $cookieStore.get('user');
    if ($routeParams.user != null)
        $scope.user = $routeParams.user;
    $scope.CanAddGame = false;
    if (user != null) {
        $scope.CanAddGame = (user.Username == $scope.user);
    }
    $rootScope.gameIdentifier = null;
    DexComplete.Users.GetAllGames($scope.user, function (Result) {
        if (Result.Result == 0) {
            $scope.games = Result.Value;
        }

    })

}]);

    