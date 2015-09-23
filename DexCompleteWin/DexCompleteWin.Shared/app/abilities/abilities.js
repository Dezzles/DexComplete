'use strict';

angular.module('dexComplete.abilities', ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/user/:userId/game/:gameName/abilities', {
        templateUrl: 'app/abilities/abilities.html',
        controller: 'AbilitiesCtrl',
        auth: function (user) {
            return true;
        }
    });

}

])
.controller('AbilitiesCtrl', ['$scope', '$rootScope', '$routeParams', '$cookieStore', 'DexComplete', function ($scope, $rootScope, $routeParams, $cookieStore, DexComplete) {
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
                AbilityData: $scope.getCode()
            }, function (Result) { })

    }
    DexComplete.Users.GetSaveData(
    {
        User: $routeParams.userId,
        Save: $routeParams.gameName
    }, function (Result) {
        if (Result.Result == 0) {
            $scope.GameName = Result.Value.SaveName;
            $scope.SaveData = Result.Value.AbilityData;
            $scope.DecryptCode(Result.Value.AbilityData);
            $scope.GameTitle = Result.Value.GameTitle;
            $rootScope.gameIdentifier = Result.Value.GameIdentifier;
            DexComplete.Abilities.GetAbilityList(
            {
                GameId: Result.Value.GameIdentifier
            }, function (Result2) {
                if (Result2.Result == 0) {
                    $scope.Data = Result2.Value;
                }
            });
        }
    });

}])

;

