'use strict';

angular.module('dexComplete.sortBox', ['ngRoute'])

.controller('SortBoxCtrl', ['$scope', '$rootScope', function ($scope, $rootScope) {
    $scope.SortModes = ["All", "Marked", "Unmark"];
    $scope.Mode = 0;
    $rootScope.$watch('sortable', function (newVal, oldVal) {
        if (newVal != null) {
            $scope.sortVisible = newVal;
        }
        else {
            $scope.sortVisible = false;
        }
    }, true);

    $scope.setAll = function () {
        $rootScope.sortMode = 0;
    }
    $scope.setMarked = function () {
        $rootScope.sortMode = 1;
    }
    $scope.setUnmarked = function () {
        $rootScope.sortMode = 2;
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

