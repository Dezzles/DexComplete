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
  'dexComplete.tms',
  'dexComplete.dittoStat',
  'dexComplete.addGame',
  'dexComplete.maintenance',
  'dexComplete.logout',
  'dexComplete.userDashboard',
  'dexComplete.sortBox'

]);
dexApp.config(['$routeProvider', '$compileProvider', function ($routeProvider, $compileProvider) {
    $routeProvider.otherwise({ redirectTo: '/' });
    $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|file|ms-appx):/);
}

]);
dexApp.run(['$rootScope', '$location', '$cookieStore', 'DexComplete', '$routeParams', function ($rootScope, $location, $cookieStore, DexComplete, $routeParams) {
    $rootScope.$on('$routeChangeStart', function (ev, next, curr) {
        if (next.$$route) {
            var user = $cookieStore.get('user')
            $rootScope.user = user;
            var auth = next.$$route.auth
            if (auth && !auth(user)) { $location.path('/login') }
            if (next.$$route.originalPath != '/maintenance') {
                DexComplete.Server.IsMaintenance({}, function (result) {
                    if (result.Result == 3) {
                        $location.path('/maintenance');
                    }
                });
            }
            var hasSortMode = next.$$route.hasSortMode;
            if (hasSortMode && hasSortMode()) {
                $rootScope.sortable = true;
            }
            else {
                $rootScope.sortMode = 0;
                $rootScope.sortable = false;
            }

        }
    })
    $rootScope.$on('$routeChangeSuccess', function (ev, next, curr) {
        if ($routeParams.gameName == null) {
            $rootScope.gameIdentifier = null;
        }
        $rootScope.currentViewUser = $routeParams.userId;
    })
} ])