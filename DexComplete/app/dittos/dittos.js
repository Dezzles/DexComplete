'use strict';

angular.module('dexComplete.dittos', ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/user/:userId/game/:gameName/dittos', {
        templateUrl: 'app/dittos/dittos.html',
        controller: 'DittosCtrl',
        auth: function (user) {
            return true;
        }
    });

}

])
.controller('DittosCtrl', ['$scope', 'RouteData', '$cookieStore', 'DexComplete', function ($scope, RouteData, $cookieStore, DexComplete) {
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
                DittoData: $scope.getCode()
            }, function (Result) { })

    }
    DexComplete.Users.GetSaveData(
    {
        User: RouteData.currentViewUser(),
        Save: RouteData.gameName()
    }, function (Result) {
        if (Result.Result == 0) {
            $scope.GameName = Result.Value.SaveName;
            $scope.SaveData = Result.Value.DittoData;
            $scope.DecryptCode(Result.Value.DittoData);
            $scope.GameTitle = Result.Value.GameTitle;
        }
    });

}])

;

