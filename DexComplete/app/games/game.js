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
    $scope.chartData = [];
    DexComplete.Users.GetSaveData(
        {
            User: $routeParams.userId,
            Save: $routeParams.gameName
        }, function (Result) {
            if (Result.Result == 0) {
                $scope.GameName = Result.Value.SaveName;
                $scope.SaveData = Result.Value.SaveData;
                $scope.GameTitle = Result.Value.GameTitle;
                $rootScope.gameIdentifier = Result.Value.GameIdentifier;

            }
        })
    $('.national6').easyPieChart({
        //your configuration goes here
        size: 110
    });


    DexComplete.Users.GetSaveProgress(
    {
        User: $routeParams.userId,
        Save: $routeParams.gameName
    }, function (Result) {
        if (Result.Result == 0) {
            $scope.Progress = Result.Value;
            var dexes = $scope.Progress.Pokedexes;
            for (var u in dexes) {
                var n = {
                    Total: dexes[u].Total,
                    Completion: dexes[u].Completion,
                    Identifier: dexes[u].Identifier
                }
                $scope.chartData.push(n);
            }
            var dexes = $scope.Progress.Collections;
            for (var u in dexes) {
                var n = {
                    Total: dexes[u].Total,
                    Completion: dexes[u].Completion,
                    Identifier: dexes[u].Identifier
                }
                $scope.chartData.push(n);
            }

        }

    })

    $scope.renderGraph = function (identifier) {
        //       setTimeout(function() { // You might need this timeout to be sure its run after DOM render.
        /*        $('.' + identifier).easyPieChart({
                    //your configuration goes here
                    size: 100
                });
                */
        //        }, 2000)
        var ctx = document.getElementById('chart-' + identifier).getContext("2d");
        var options = {

        };
        var amounts = $scope.chartData[0];
        for (var u in $scope.chartData) {
            if ($scope.chartData[u].Identifier == identifier) {
                amounts = $scope.chartData[u];
            }

        }
        var data = [
            {
                value: amounts.Total - amounts.Completion,
                color: "#F7464A",
                highlight: "#FF5A5E",
                label: "Need"
            },
            {
                value: amounts.Completion,
                color: "#4660BD",
                highlight: "#5A60D1",
                label: "Collected"
            }
        ]

        var myNewChart = new Chart(ctx).Pie(data);
    }

}])

.directive('game', function () {
    return {
        scope: { entry: '=' },
        templateUrl: '/app/games/game.html',
        controller: 'GameCtrl'
    }
}
);

