'use strict';

angular.module('dexComplete.menu', ['ngRoute'])
.controller('MenuCtrl', ['$scope', 'RouteData', 'DexComplete', '$cookieStore', '$rootScope', function ($scope, RouteData, DexComplete, $cookieStore, $rootScope) {
    $scope.GameName = RouteData.gameName();
    $scope.User = RouteData.currentViewUser();
    $scope.RouteData = RouteData;
    $rootScope.$watch('user', function (newVal, oldVal) {
        $scope.loggedInUser = $cookieStore.get('user');
    });
    $scope.dashboardText = '';
    $scope.ViewUser = RouteData.currentViewUser();
    if ($scope.ViewUser != null) {
        if ($scope.loggedInUser != null) {
            if ($scope.loggedInUser.Username == $scope.ViewUser) {
                $scope.ViewUser = null;
            }
        }
    }

    $scope.$watch('RouteData', function (newVal, oldVal) {
        $scope.ViewUser = null;
        if ((RouteData.currentViewUser() != null) && (RouteData.currentViewUser() != $scope.loggedInUser.Username)) {
            $scope.ViewUser = RouteData.currentViewUser();
        }
    }, true);


    $scope.$watch('RouteData', function (newVal, oldVal) {
        $scope.updateGameMenu();

    }, true);

    $scope.updateGameMenu = function () {
        if (RouteData.gameIdentifier() != null) {
            var identifier = RouteData.gameIdentifier();
            $scope.GameName = RouteData.gameName();
            $scope.User = RouteData.currentViewUser();
            DexComplete.Games.GetGameTools(
                { GameName: RouteData.gameIdentifier() }, function (Result) {
                    if (Result.Result == 0) {
						$scope.dexes = [];
						$scope.regional = []
						for (var u = 0; u < Result.Value.Pokedex.length; ++u) {
							if (Result.Value.Pokedex[u].Regional) {
								$scope.regional.push( Result.Value.Pokedex[u] );
							}
							else {
								$scope.dexes.push( Result.Value.Pokedex[u] );
							}
						}
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
    }


    $scope.updateGameMenu();
}])

.directive('menu', function () {
    return {
        scope: { entry: '=' },
        templateUrl: '/app/dashboard/menu.html',
        controller: 'MenuCtrl'
    }
}
);

