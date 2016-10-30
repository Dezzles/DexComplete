'use strict';

angular.module('dexComplete.berries', ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/user/:userId/game/:gameName/berries', {
        templateUrl: 'app/berries/berries.html',
        controller: 'BerriesCtrl',
        auth: function (user) {
            return true;
        },
        hasSortMode: function () {
            return true;
        }
    });

}

])
.controller('BerriesCtrl', ['$scope', 'RouteData', '$cookieStore', 'DexComplete', function ($scope, RouteData, $cookieStore, DexComplete) {
    $scope.GameName = RouteData.gameName();
    var user = $cookieStore.get('user');
    $scope.currentStats = [];
    $scope.saveBits = 1;
    $scope.maxValue = 1;
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
                BerryData: $scope.getCode()
            }, function (Result) { })

    }
    DexComplete.Users.GetSaveData(
    {
        User: RouteData.currentViewUser(),
        Save: RouteData.gameName()
    }, function (Result) {
        if (Result.Result == 0) {
            $scope.GameName = Result.Value.SaveName;
            $scope.SaveData = Result.Value.BerryData;
            $scope.DecryptCode(Result.Value.BerryData);
            $scope.GameTitle = Result.Value.GameTitle;
            RouteData.setGameIdentifier(Result.Value.GameIdentifier);
            DexComplete.Berries.GetBerryList(
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

