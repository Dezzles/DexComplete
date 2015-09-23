'use strict';

angular.module('dexComplete.login', ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/login', {
        templateUrl: 'app/login/login.html',
        controller: 'LoginCtrl',
        auth: function (user) {
            return true;
        }
    });

}

])/**/

.controller('LoginCtrl', ['$scope', '$http', '$cookieStore', '$location','DexComplete',function ($scope, $http, $cookieStore, $location, DexComplete) {
    $scope.tryLogin = function() {
        DexComplete.Users.Login({ username: $scope.username, password: $scope.password }, $scope.OnComplete);
    }

    $scope.OnComplete = function (Result) {
        if (Result.Result == 0)
            $location.path('$/');
        else
            $scope.Message = Result.Message;

    }
}]);

