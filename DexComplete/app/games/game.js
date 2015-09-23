'use strict';

angular.module('dexComplete.game', ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/user/:userId/game/:gameName', {
        templateUrl: 'app/games/game.html',
        controller: 'GameCtrl',
        auth: function (user) {
            return true;
        }
    });

}

])
.controller('GameCtrl', ['$scope', '$rootScope', '$routeParams', '$cookieStore', 'DexComplete', function ($scope, $rootScope, $routeParams, $cookieStore, DexComplete) {
    $scope.GameName = $routeParams.gameName;
    var user = $cookieStore.get('user');
    DexComplete.Users.GetSaveData(
        {
            User: user.Username,
            Save: $routeParams.gameName
        }, function (Result) {
            if (Result.Result == 0) {
                $scope.GameName = Result.Value.SaveName;
                $scope.SaveData = Result.Value.SaveData;
                $scope.GameTitle = Result.Value.GameTitle;
                $rootScope.gameIdentifier = Result.Value.GameIdentifier;

            }
        })
}])

.directive('game', function () {
    return {
        scope: { entry: '=' },
        templateUrl: '/app/games/game.html',
        controller: 'GameCtrl'
    }
}
);

