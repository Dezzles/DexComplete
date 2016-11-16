angular.module('HttpCommunicate', [ ] )

.factory('HttpCommunicate', [ '$http', 'Cache', function($http, cache) {
	var address = '';
	
	var service = {};
	
	service.post = function(path, data, headers, onComplete) {
		if (cache.has('POST' + path)) { 
			setTimeout(onComplete(cache.get('POST' + path)), 0);
		}
		else {
			var req = {
				method: 'POST',
				url : address + path
			};
			if (data != null) req.data = data;
			if (headers != null) req.headers = headers;
			$http(req).then( function(result) {
				if (result.data.Status == 0) {
					if (!result.data.volatile) {
						cache.set('POST:' + path, result.data);
					}
				}
				onComplete(result.data);
			});
		 }
	};
	
	service.get = function(path, headers, onComplete) {
		if (cache.has('GET:' + path)) { 
			setTimeout(onComplete(cache.get('GET:' + path)), 0);
		}
		else {
			var req = {
				method: 'GET',
				url : address + path
			};
			if (headers != null) req.headers = headers;
			$http(req).then( function(result) {
				if (result.data.Status == 0) {
					if (!result.data.volatile) {
						cache.set('GET:' + path, result.data);
					}
				}
				onComplete(result.data);
			});
		 }
	};
		
	return service;
} ] );


