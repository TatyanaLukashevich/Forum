'use strict';

/* Services */

var siteServices = angular.module('siteServices', ['ngResource']);

siteServices.factory('Promotions', ['$resource',
  function ($resource) {
      return $resource('api/promotion/GetPromotions', {}, {
          query: { method: 'GET', isArray: true }
      });
  }]);

siteServices.factory('Books', ['$resource',
  function ($resource) {
      return $resource('api/book/GetBooks', {}, {
          query: { method: 'GET', isArray: true }
      });
  }]);

siteServices.factory('Genres', ['$resource',
  function ($resource) {
      return $resource('api/genre/GetGenres', {}, {
          query: { method: 'GET', isArray: true }
      });
  }]);

siteServices.factory('Authors', ['$resource',
  function ($resource) {
      return $resource('api/author/GetAuthors', {}, {
          query: { method: 'GET', isArray: true }
      });
  }]);

siteServices.factory('User', ['$resource',
  function ($resource) {
      return $resource('api/Acoount/GetUser', {}, {
          query: { method: 'GET', isArray: false }
      });
  }]);