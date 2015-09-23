'use strict';

angular.module('dexComplete.register', ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/register', {
        templateUrl: 'app/register/register.html',
        controller: 'RegisterCtrl',
        auth: function (user) {
            return true;
        }
    });

}

])/**/

.controller('RegisterCtrl', ['$scope', 'DexComplete', '$rootScope', '$location', function ($scope, DexComplete, $rootScope, $location) {
    $scope.register = function () {
        if ($scope.username == null || $scope.username.trim() == '') {
            $scope.Message = "You must enter a username";
            return;
        }
        if ($scope.email == null || $scope.email.trim() == '') {
            $scope.Message = "You must enter an email";
            return;
        }
        if ($scope.password1 == null || $scope.password1.trim() == '') {
            $scope.Message = "You must enter a password";
            return;
        }
        if ($scope.password1 != $scope.password2 ) {
            $scope.Message = "Passwords must match";
            return;
        }
        document.getElementById("btnRegister").disabled = true;
        DexComplete.Users.Register({
            username: $scope.username,
            password: $scope.password1,
            email: $scope.email
        },
            function (result) {
                if (result.Result == 0) {
                    $location.path('#/');
                }
                else {
                    $scope.Message = result.Message;
                    document.getElementById("btnRegister").disabled = false;

                }
            }
        );

    }
}]);

