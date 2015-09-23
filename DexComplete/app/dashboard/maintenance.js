'use strict';

angular.module('dexComplete.maintenance', ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/maintenance', {
        templateUrl: 'app/dashboard/maintenance.html',
        controller: 'MaintenenceCtrl',
        auth: function (user) {
            return user != null;
        }
    });

}

])/**/

.controller('MaintenenceCtrl', ['$scope', '$cookieStore', 'DexComplete', '$rootScope', function ($scope, $cookieStore, DexComplete, $rootScope) {


}]);

