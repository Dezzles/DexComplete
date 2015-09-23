'use strict';

angular.module('dexComplete.addGame', ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/user/addgame', {
        templateUrl: 'app/games/addgame.html',
        controller: 'AddGameCtrl',
        auth: function (user) {
            return true;
        }
    });

}

])
.controller('AddGameCtrl', ['$scope', '$rootScope', '$location', '$cookieStore', 'DexComplete', function ($scope, $rootScope, $location, $cookieStore, DexComplete) {
    var user = $cookieStore.get('user');/**/
    DexComplete.Games.GetGameList(
        {
        }, function (Result) {
            if (Result.Result == 0) {
                $scope.Games = Result.Value;
            }
        })/**/

    $scope.tryAddGame = function () {
        DexComplete.Users.AddGame(
        {

            SaveName: $scope.gameName,
            Identifier: $scope.gameIdentifier
        }, function (Result) {
            if (Result.Result == 0) 
                $location.path('$/');
            else
                $scope.Message = Result.Message;
            
        });
    }
}]);