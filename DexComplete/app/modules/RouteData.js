angular.module('RouteData', [
        'ngRoute'
])
    .factory('RouteData', ['$routeParams', 'DexComplete', function ($routeParams, DexComplete) {

        var _currentViewUser = null;
        var _gameName = null;
        var _gameIdentifier = null;
        var _sortable = false;
        var _sortMode = 0;
        return {
            marker: 0,
            currentViewUser: function () { return _currentViewUser; },
            gameName: function() { return _gameName; },
            gameIdentifier: function () { return _gameIdentifier; },
            sortable: function () { return _sortable; },
            sortMode: function () { return _sortMode; },

            update: function () {
                var changed = false;
                if ((_currentViewUser != $routeParams.userId) || (_gameName != $routeParams.gameName)) {

                    _currentViewUser = $routeParams.userId;
                    _gameName = $routeParams.gameName;
                    this.marker = this.marker + 1;
                    var current = this;
                    if ((_currentViewUser != null) || (_gameName != null)) {
                        DexComplete.Users.GetGameIdentifier({ Username: _currentViewUser, Save: _gameName }, function (val) {
                            _gameIdentifier = val.Value;
                            current.marker = current.marker + 1;
                        });
                    }
                    else {
                        _gameIdentifier = null;
                    }
                }
            },

            setSortable: function (val) { _sortable = val; this.marker = this.marker + 1; },
            setSortMode: function (val) { _sortMode = val; this.marker = this.marker + 1; },
            setGameIdentifier: function (val) { _gameIdentifier = val; this.marker = this.marker + 1; }
        };
    }])
;