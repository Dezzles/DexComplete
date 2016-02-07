'use strict';

angular.module('dexComplete.reset', ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
	$routeProvider.when('/reset/:token', {
		templateUrl: 'app/register/reset.html',
		controller: 'ResetCtrl',
		auth: function (user) {
			return true;
		}
	});

}

])/**/

.controller('ResetCtrl', ['$scope', 'DexComplete', '$routeParams', '$location', function ($scope, DexComplete, $routeParams, $location) {
	$scope.register = function () {
		if ($scope.username == null || $scope.username.trim() == '') {
			$scope.Message = "You must enter a username";
			return;
		}
		if ($scope.password1 == null || $scope.password1.trim() == '') {
			$scope.Message = "You must enter a password";
			return;
		}
		if ($scope.password1 != $scope.password2) {
			$scope.Message = "Passwords must match";
			return;
		}
		document.getElementById("btnRegister").disabled = true;
		DexComplete.Users.ResetPassword({
			username: $scope.username,
			password: $scope.password1,
			token: $routeParams.token
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

