'use strict';

angular.module('dexComplete.menu', ['ngRoute'])
.controller('MenuCtrl', ['$scope', '$rootScope', '$routeParams', 'DexComplete', '$cookieStore', function ($scope, $rootScope, $routeParams, DexComplete, $cookieStore) {
    $scope.GameName = $routeParams.gameName;
    $scope.User = $routeParams.userId;
    $scope.loggedInUser = $cookieStore.get('user');
    $scope.dashboardText = '';

    $rootScope.$watch('gameIdentifier', function (newVal, oldVal) {
        if (newVal != null) {
            DexComplete.Games.GetGameTools(
                { GameName: newVal }, function (Result) {
                    if (Result.Result == 0) {
                        $scope.GameName = $routeParams.gameName;
                        $scope.User = $routeParams.userId;
                        $scope.dexes = Result.Value.Pokedex;
                        $scope.data = Result.Value;
                    }
                })
        }
        else {
            $scope.GameName = null;
            $scope.User = null;
            $scope.dexes = null;
            $scope.data = null;
        }

    }, true);

}])

.directive('menu', function () {
    return {
        scope: { entry: '=' },
        templateUrl: '/app/dashboard/menu.html',
        controller: 'MenuCtrl'
    }
}
);

