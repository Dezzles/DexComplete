'use strict';

angular.module('dexComplete.requestReset', ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
	$routeProvider.when('/requestreset', {
		templateUrl: 'app/register/resetrequest.html',
		controller: 'RequestResetCtrl',
		auth: function (user) {
			return true;
		}
	});

}

])/**/

.controller('RequestResetCtrl', ['$scope', 'DexComplete', '$rootScope', '$location', function ($scope, DexComplete, $rootScope, $location) {
	$scope.requestReset = function () {
		if ($scope.username == null || $scope.username.trim() == '') {
			$scope.Message = "You must enter a username";
			return;
		}
		document.getElementById("btnRegister").disabled = true;
		DexComplete.Users.RequestReset({
			User: $scope.username,
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

