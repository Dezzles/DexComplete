'use strict';

// Declare app level module which depends on views, and components
var dexApp = angular.module('dexComplete', [
  'ngRoute',
  'ngCookies',
  'RouteData',
  'DexCompleteService',
  'HttpCommunicate',
  'Cache',
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
  'dexComplete.requestReset',
  'dexComplete.reset',
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
dexApp.run(['$location', '$cookieStore', 'DexComplete', 'RouteData', '$rootScope', function ($location, $cookieStore, DexComplete, RouteData, $rootScope) {
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
                RouteData.setSortable(true);
            }
            else {
                RouteData.setSortMode(0);
                RouteData.setSortable(false);
            }

        }
    })
    $rootScope.$on('$routeChangeSuccess', function (ev, next, curr) {
        RouteData.update();
    })
} ])