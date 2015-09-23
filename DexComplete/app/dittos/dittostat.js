'use strict';

angular.module('dexComplete.dittoStat', ['ngRoute'])

.controller('DittoStatCtrl', ['$scope', '$routeParams', function ($scope, $routeParams) {
    $scope.natid = parseInt($scope.natureid);

    $scope.updateValue = function () {
        $scope.$parent.updateValue($scope.natid);
    }

    $scope.getValue = function () {
        return $scope.$parent.getValue($scope.natid);
    }/**/


}])

.directive('dittoStat', function () {
    return {
        scope: { resource: '=', natureid: '@', title: '@' },
        templateUrl: '/app/dittos/dittostat.html',
        controller: 'DittoStatCtrl'
    }
}
)
;

