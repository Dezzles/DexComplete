angular.module('DexCompleteService', [
        'ngCookies'
])
    .factory('DexComplete', ['$cookieStore', '$http', 'HttpCommunicate', function ($cookieStore, $http, Http) {
        var address = '';
        var _user = {};
		var compl = function(r, c) {
			if (r.Status == 0) {
				c({ Result: 0, Value: r.Value });
			}
			else {
				c({ Result: r.Status, Message: r.Message });
			}
		}

        return {
            Users: {
                Login: function (params, onComplete) {
                    $http.post(address + "/api/v1/users/login", { username: params.username, password: params.password }).success(function (summary) {
                        if (summary.Status == 0) {
                            $cookieStore.put('user', { Username: summary.Value.Username, Token: summary.Value.Token });
                            onComplete({ Result: 0 });
                        }
                        else
                            onComplete({ Result: 1, Message: summary.Message });
                    });

                },

                Register: function (params, onComplete) {
                    $http.post(address + "/api/v1/users/register",
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
                        url: address + '/api/v1/users/games/add',
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
                        url: address + '/api/v1/users/logout',
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
                RequestReset: function (params, onComplete) {
                	var req = {
                		method: 'GET',
                		url: address + '/api/v1/user/' + params.User + '/resetpassword'
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

                ResetPassword: function (params, onComplete) {
                	var req = {
                		method: 'POST',
                		url: address + '/api/v1/users/resetpassword',
                		data: {
                			Username: params.username,
                			Token: params.token,
							Password: params.password
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
					Http.get('/api/v1/games/list', null, function(results) {
						if (results.Status == 0) {
							onComplete({ Result: 0, Value: results.Value });
						}
						else {
							onComplete({ Result: results.Status, Message: results.Message });
						}
					});
                },

                GetGamePokedexes: function (params, onComplete) {
					Http.get('/api/v1/game/' + params + '/dex', null, function(results) {
						if (results.Status == 0) {
							onComplete({ Result: 0, Value: results.Value });
						}
						else {
							onComplete({ Result: results.Status, Message: results.Message });
						}
					});
                },

                GetPokedexEntries: function (params, onComplete) {
					Http.get('/api/v1/game/' + params + '/dex/' + params.DexName, null, function(results) {
						if (results.Status == 0) {
							onComplete({ Result: 0, Value: results.Value });
						}
						else {
							onComplete({ Result: results.Status, Message: results.Message });
						}
					});
                },

                GetGameTools: function (params, onComplete) {
					Http.get('/api/v1/game/' + params.GameName + '/allTools', null, function(results) {
						if (results.Status == 0) {
							onComplete({ Result: 0, Value: results.Value });
						}
						else {
							onComplete({ Result: results.Status, Message: results.Message });
						}
					});
                }
            },

            Pokedexes: {
                GetPokedexEntries: function (params, onComplete) {
					Http.get('/api/v1/pokedex/' + params.DexId, null, function(results) {
						if (results.Status == 0) {
							onComplete({ Result: 0, Value: results.Value });
						}
						else {
							onComplete({ Result: results.Status, Message: results.Message });
						}
					});
                }
            },

            Abilities: {
                GetAbilityList: function (params, onComplete) {
					Http.get('/api/v1/ability/' + params.GameId, null, function(results) {
						if (results.Status == 0) {
							onComplete({ Result: 0, Value: results.Value });
						}
						else {
							onComplete({ Result: results.Status, Message: results.Message });
						}
					});
                }
            },

            Berries: {
                GetBerryList: function (params, onComplete) {
					Http.get('/api/v1/berries/' + params.GameId, null, function(results) {
						if (results.Status == 0) {
							onComplete({ Result: 0, Value: results.Value });
						}
						else {
							onComplete({ Result: results.Status, Message: results.Message });
						}
					});
                }
            },

            EggGroups: {
                GetEggGroupList: function (params, onComplete) {
					Http.get('/api/v1/eggGroups/' + params.GameId, null, function(results) {
						if (results.Status == 0) {
							onComplete({ Result: 0, Value: results.Value });
						}
						else {
							onComplete({ Result: results.Status, Message: results.Message });
						}
					});
                }
            },

            TMs: {
                GetTMList: function (params, onComplete) {
					Http.get('/api/v1/tms/' + params.GameId, null, function(results) {
						if (results.Status == 0) {
							onComplete({ Result: 0, Value: results.Value });
						}
						else {
							onComplete({ Result: results.Status, Message: results.Message });
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
					Http.get('/api/v1/server/updates', null, function(results) {
						if (results.Status == 0) {
							onComplete({ Result: 0, Value: results.Value });
						}
						else {
							onComplete({ Result: results.Status, Message: results.Message });
						}
					});
                }
            }
        };
    }])
;
