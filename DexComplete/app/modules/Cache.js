angular.module('Cache', [] )

.factory('Cache', function() {
	var service = {
		cache : {}
	};
	
	service.has = function(path) {
		return service.cache.hasOwnProperty(path);
	};
	
	service.get = function(path) {
		if (!service.has(path)) 
			return null;
		return jQuery.extend(true, {}, service.cache[path]);
	};
	
	service.set = function(path, val) {
		service.cache[path] = val;
	};
	
	return service;
});