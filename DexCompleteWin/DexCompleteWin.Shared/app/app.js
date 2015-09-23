'use strict';

// Declare app level module which depends on views, and components
var dexApp = angular.module('dexComplete', [
  'ngRoute',
  'ngCookies',
  'DexCompleteService',
  'dexComplete.gameItem',
  'dexComplete.dashboard',
  'dexComplete.login',
  'dexComplete.userDropdown',
  'dexComplete.register',
  'dexComplete.game',
  'dexComplete.menu',
  'dexComplete.pokedex',
  'dexComplete.abilities',
  'dexComplete.berries',
  'dexComplete.dittos',
  'dexComplete.eggGroups',
  'dexComplete.tms'

]);
dexApp.config(['$routeProvider', '$compileProvider', function ($routeProvider, $compileProvider) {
    $routeProvider.otherwise({ redirectTo: '/' });
    $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|file|ms-appx):/);
}

]);
dexApp.run(function ($rootScope, $location, $cookieStore) {
    $rootScope.$on('$routeChangeStart', function (ev, next, curr) {
        if (next.$$route) {
            var user = $cookieStore.get('user')
            $rootScope.user = user;
            var auth = next.$$route.auth
            if (auth && !auth(user)) { $location.path('/login') }
        }
    })
})