angular.module('DexCompleteService', [
        'ngCookies'
])
    .factory('DexComplete', ['$cookieStore', '$http', function ($cookieStore, $http) {
        var address = '';
        var _user = {};

        return {
            Users: {
                Login: function (params, onComplete) {
                    $http.post(address + "/api/v1/user/login", { username: params.username, password: params.password }).success(function (summary) {
                        if (summary.Status == 0) {
                            $cookieStore.put('user', { Username: summary.Value.Username, Token: summary.Value.Token });
                            onComplete({ Result: 0 });
                        }
                        else
                            onComplete({ Result: 1, Message: summary.Message });
                    });

                },

                Register: function (params, onComplete) {
                    $http.post(address + "/api/v1/user/register",
                        {
                            username: params.username,
                            password: params.password,
                            email: params.email
                        }).success(function (summary) {
                            if (summary.Status == 0) {
                                $cookieStore.put('user', { Username: params.username, Token: summary.Value.Token });
                                onComplete({ Result: 0 });
                            }
                            else {
                                onComplete({ Result: 1, Message: summary.Message });
                            }
                        });
                },

                AddGame: function (params, onComplete) {
                    var user = $cookieStore.get('user')
                    var req = {
                        method: 'POST',
                        url: address + '/api/v1/user/games/add',
                        headers: {
                            'username': user.Username,
                            'token': user.Token
                        },
                        data: {
                            SaveName: params.SaveName,
                            Identifier: params.Identifier
                        }
                    };

                    $http(req).then(function (summary) {
                        if (summary.data.Status == 0) {
                            onComplete({ Result: 0 });
                        }
                        else {
                            onComplete({ Result: 1, Message: summary.data.Message });
                        }
                    });

                },

                Logout: function (params, onComplete) {
                    var user = $cookieStore.get('user')
                    var req = {
                        method: 'POST',
                        url: address + '/api/v1/user/logout',
                        headers: {
                            'username': user.Username,
                            'token': user.Token
                        }
                    };
                    $cookieStore.put('user', null);

                    $http(req).then(function (summary) {
                        if (summary.data.Status == 0) {
                            onComplete({ Result: 0 });
                        }
                        else {
                            onComplete({ Result: 0, Message: summary.data.Message });
                        }
                    });

                },

                GetAllGames: function (params, onComplete) {
                    var req = {
                        method: 'GET',
                        url: address + '/api/v1/user/' + params + '/games/list'
                    };

                    $http(req).then(function (summary) {
                        if (summary.data.Status == 0) {
                            onComplete({ Result: 0, Value: summary.data.Value });
                        }
                        else {
                            onComplete({ Result: 1, Message: summary.data.Message });
                        }
                    });


                },

                GetSaveData: function (params, onComplete) {
                    var req = {
                        method: 'GET',
                        url: address + '/api/v1/user/' + params.User + '/game/' + params.Save
                    };

                    $http(req).then(function (summary) {
                        if (summary.data.Status == 0) {
                            onComplete({ Result: 0, Value: summary.data.Value });
                        }
                        else {
                            onComplete({ Result: 1, Message: summary.data.Message });
                        }
                    });


                },

                GetSaveProgress: function (params, onComplete) {
                    var req = {
                        method: 'GET',
                        url: address + '/api/v1/user/' + params.User + '/game/' + params.Save + '/progress'
                    };

                    $http(req).then(function (summary) {
                        if (summary.data.Status == 0) {
                            onComplete({ Result: 0, Value: summary.data.Value });
                        }
                        else {
                            onComplete({ Result: 1, Message: summary.data.Message });
                        }
                    });


                },

                SetSaveData: function (params, onComplete) {
                    var user = $cookieStore.get('user')
                    var req = {
                        method: 'POST',
                        url: address + '/api/v1/user/' + user.Username + '/game/' + params.Save,
                        headers: {
                            'username': user.Username,
                            'token': user.Token
                        },
                        data: {
                            SaveData: params.Code,
                            AbilityData: params.AbilityData,
                            DittoData: params.DittoData,
                            TMData: params.TMData,
                            EggGroupData: params.EggGroupData,
                            BerryData: params.BerryData
                        }
                    };

                    $http(req).then(function (summary) {
                        if (summary.data.Status == 0) {
                            onComplete({ Result: 0, Value: summary.data.Value });
                        }
                        else {
                            onComplete({ Result: 1, Message: summary.data.Message });
                        }
                    });


                },
                GetGameIdentifier: function (params, onComplete) {
                    var req = {
                        method: 'GET',
                        url: address + '/api/v1/user/' + params.Username + '/game/' + params.Save + "/identifier",
                        data: {
                            Identifier: params
                        }
                    };

                    $http(req).then(function (summary) {
                        if (summary.data.Status == 0) {
                            onComplete({ Result: 0, Value: summary.data.Value });
                        }
                        else {
                            onComplete({ Result: 1, Message: summary.data.Message });
                        }
                    });


                }

            },


            Games: {
                GetGameList: function (params, onComplete) {
                    var req = {
                        method: 'GET',
                        url: address + '/api/v1/games/list'
                    };

                    $http(req).then(function (summary) {
                        if (summary.data.Status == 0) {
                            onComplete({ Result: 0, Value: summary.data.Value });
                        }
                        else {
                            onComplete({ Result: 1, Message: summary.data.Message });
                        }
                    });
                },

                GetGamePokedexes: function (params, onComplete) {
                    var req = {
                        method: 'GET',
                        url: address + '/api/v1/game/' + params + '/dex'
                    };

                    $http(req).then(function (summary) {
                        if (summary.data.Status == 0) {
                            onComplete({ Result: 0, Value: summary.data.Value });
                        }
                        else {
                            onComplete({ Result: 1, Message: summary.data.Message });
                        }
                    });
                },

                GetPokedexEntries: function (params, onComplete) {
                    var req = {
                        method: 'GET',
                        url: address + '/api/v1/game/' + params.GameName + '/dex/' + params.DexName
                    };

                    $http(req).then(function (summary) {
                        if (summary.data.Status == 0) {
                            onComplete({ Result: 0, Value: summary.data.Value });
                        }
                        else {
                            onComplete({ Result: 1, Message: summary.data.Message });
                        }
                    });
                },

                GetGameTools: function (params, onComplete) {
                    var req = {
                        method: 'GET',
                        url: address + '/api/v1/game/' + params.GameName + '/allTools'
                    };

                    $http(req).then(function (summary) {
                        if (summary.data.Status == 0) {
                            onComplete({ Result: 0, Value: summary.data.Value });
                        }
                        else {
                            onComplete({ Result: 1, Message: summary.data.Message });
                        }
                    });
                }


            },

            Pokedexes: {
                GetPokedexEntries: function (params, onComplete) {
                    var req = {
                        method: 'GET',
                        url: address + '/api/v1/pokedex/' + params.GameId + '/' + params.DexId
                    };

                    $http(req).then(function (summary) {
                        if (summary.data.Status == 0) {
                            onComplete({ Result: 0, Value: summary.data.Value });
                        }
                        else {
                            onComplete({ Result: 1, Message: summary.data.Message });
                        }
                    });
                }
            },

            Abilities: {
                GetAbilityList: function (params, onComplete) {
                    var req = {
                        method: 'GET',
                        url: address + '/api/v1/ability/' + params.GameId
                    };

                    $http(req).then(function (summary) {
                        if (summary.data.Status == 0) {
                            onComplete({ Result: 0, Value: summary.data.Value });
                        }
                        else {
                            onComplete({ Result: 1, Message: summary.data.Message });
                        }
                    });
                }
            },

            Berries: {
                GetBerryList: function (params, onComplete) {
                    var req = {
                        method: 'GET',
                        url: address + '/api/v1/berries/' + params.GameId
                    };

                    $http(req).then(function (summary) {
                        if (summary.data.Status == 0) {
                            onComplete({ Result: 0, Value: summary.data.Value });
                        }
                        else {
                            onComplete({ Result: 1, Message: summary.data.Message });
                        }
                    });
                }
            },

            EggGroups: {
                GetEggGroupList: function (params, onComplete) {
                    var req = {
                        method: 'GET',
                        url: address + '/api/v1/eggGroups/' + params.GameId
                    };

                    $http(req).then(function (summary) {
                        if (summary.data.Status == 0) {
                            onComplete({ Result: 0, Value: summary.data.Value });
                        }
                        else {
                            onComplete({ Result: 1, Message: summary.data.Message });
                        }
                    });
                }
            },

            TMs: {
                GetTMList: function (params, onComplete) {
                    var req = {
                        method: 'GET',
                        url: address + '/api/v1/tms/' + params.GameId
                    };

                    $http(req).then(function (summary) {
                        if (summary.data.Status == 0) {
                            onComplete({ Result: 0, Value: summary.data.Value });
                        }
                        else {
                            onComplete({ Result: 1, Message: summary.data.Message });
                        }
                    });
                }
            },
            

            Server: {
                IsMaintenance: function (params, onComplete) {
                    var req = {
                        method: 'GET',
                        url: address + '/api/v1/server/ping'
                    };

                    $http(req).then(function (summary) {
                        if (summary.data.Status == 3) {
                            onComplete({ Result: 3, Value: summary.data.Value });
                        }
                        else {
                            onComplete({ Result: 0, Message: summary.data.Message });
                        }
                    });

                },
                GetUpdates: function (params, onComplete) {
                    var req = {
                        method: 'GET',
                        url: address + '/api/v1/server/updates'
                    };

                    $http(req).then(function (summary) {
                        if (summary.data.Status == 0) {
                            onComplete({ Result: 0, Value: summary.data.Value });
                        }
                        else {
                            onComplete({ Result: 1, Message: summary.data.Message });
                        }
                    });

                }
            }
        };
    }])
;
