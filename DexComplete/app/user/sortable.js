'use strict';

angular.module('dexComplete.sortBox', ['ngRoute'])

.controller('SortBoxCtrl', ['$scope', 'RouteData', function ($scope, RouteData) {
    $scope.SortModes = ["All", "Marked", "Unmark"];
    $scope.Mode = 0;
    
    $scope.setAll = function () {
        RouteData.setSortMode(0);
    }
    $scope.setMarked = function () {
        RouteData.setSortMode(1);
    }
    $scope.setUnmarked = function () {
        RouteData.setSortMode(2);
    }
}])

.directive('sortBox', function () {
    return {
        scope: {},
        templateUrl: '/app/user/sortable.html',
        controller: 'SortBoxCtrl'
    }
}
);

