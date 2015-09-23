'use strict';

angular.module('dexComplete.pokedex', ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/user/:userId/game/:gameName/pokedex/:dexId', {
        templateUrl: 'app/pokedex/pokedex.html',
        controller: 'PokedexCtrl',
        auth: function (user) {
            return true;
        },
        hasSortMode: function () {
            return true;
        }
    });

}

])
.controller('PokedexCtrl', ['$scope', '$rootScope', '$routeParams', '$cookieStore', 'DexComplete', function ($scope, $rootScope, $routeParams, $cookieStore, DexComplete) {
    $scope.GameName = $routeParams.gameName;
    var user = $cookieStore.get('user');
    $scope.currentStats = [];
    $scope.saveBits = 2;
    $scope.maxValue = 3;
    BaseStorage($scope);

    $scope.getValue = function (v) {
        return $scope.data[v];
    }
    $scope.updateValue = function (v) {
        if (user.Username != $routeParams.userId)
            return;
        var val = $scope.getValue(v);
        val++;
        $scope.setValue(v, val);

        DexComplete.Users.SetSaveData(
            {
                Save: $scope.GameName,
                Code: $scope.getCode()
            }, function (Result) { })

    }
    DexComplete.Users.GetSaveData(
    {
        User: $routeParams.userId,
        Save: $routeParams.gameName
    }, function (Result) {
        if (Result.Result == 0) {
            $scope.GameName = Result.Value.SaveName;
            $scope.SaveData = Result.Value.SaveData;
            $scope.DecryptCode(Result.Value.SaveData);
            $scope.GameTitle = Result.Value.GameTitle;
            $rootScope.gameIdentifier = Result.Value.GameIdentifier;
            DexComplete.Pokedexes.GetPokedexEntries(
            {
                GameId: Result.Value.GameIdentifier,
                DexId: $routeParams.dexId
            }, function (Result2) {
                if (Result2.Result == 0) {
                    $scope.Data = Result2.Value;
                }
            });
        }
    });

    $rootScope.$watch('sortMode', function (newVal, oldVal) {
        if (newVal != null) {
            $scope.sortMode = newVal;
        }
        else {
            $scope.sortMode = 0;
        }
    }, true);

}])

;

