'use strict';

angular.module('dexComplete.abilities', ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/user/:userId/game/:gameName/abilities', {
        templateUrl: 'app/abilities/abilities.html',
        controller: 'AbilitiesCtrl',
        auth: function (user) {
            return true;
        },
        hasSortMode: function () {
            return true;
        }
    });

}

])
.controller('AbilitiesCtrl', ['$scope', 'RouteData', '$cookieStore', 'DexComplete', function ($scope, RouteData, $cookieStore, DexComplete) {
    $scope.GameName = RouteData.gameName();
    var user = $cookieStore.get('user');
    $scope.currentStats = [];
    $scope.saveBits = 2;
    $scope.maxValue = 3;
    BaseStorage($scope);

    $scope.getValue = function (v) {
        return $scope.data[v];
    }
    $scope.updateValue = function (v) {
        if (user.Username.toLowerCase() != RouteData.currentViewUser().toLowerCase())
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
        User: RouteData.currentViewUser(),
        Save: RouteData.gameName()
    }, function (Result) {
        if (Result.Result == 0) {
            $scope.GameName = Result.Value.SaveName;
            $scope.SaveData = Result.Value.AbilityData;
            $scope.DecryptCode(Result.Value.AbilityData);
            $scope.GameTitle = Result.Value.GameTitle;
            RouteData.setGameIdentifier(Result.Value.GameIdentifier);
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
    $scope.sortMode = RouteData.sortMode;


}])

;

