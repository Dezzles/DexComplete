'use strict';

angular.module('dexComplete.gameItem', ['ngRoute'])

.controller('GameItemCtrl', ['$scope', function ($scope) {

}])

.directive('gameItem', function () {
    return {
        scope: { entry:'=', user:'=' },
        templateUrl: '/app/games/gameitem.html',
        controller: 'GameItemCtrl'
    }
}
);

