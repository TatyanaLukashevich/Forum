var bookShopApp = angular.module('bookShopApp', [
  'ngRoute',
  'searchBooksAnimations',
  'ForumControllers',
  'siteServices'
]);

var ForumControllers = angular.module('ForumControllers', []);

bookShopApp.config(function ($routeProvider,$httpProvider) {
        $routeProvider
            .when('/',
            {
                templateUrl: '/home/start',
                //controller: 'homeCtrl'
            })
            .when('/registration',
            {
                templateUrl: '/Account/Registration',
                controller: 'registrstionCtrl'
            })
            .when('/login',
            {
                templateUrl: '/Account/Login',
                controller: 'loginCtrl'
            })
            .when('/user',
            {
                templateUrl: '/User/Сabinet',
                controller: 'cabinetCtrl'
            })
            .when('/cart',
            {
                templateUrl: '/User/Cart',
                controller: 'cartCtrl'
            })
            .when('/admin',
            {
                templateUrl: '/Admin/Administration',
                //controller: 'loginCtrl'
            })
            .when('/addAuthor',
            {
                templateUrl: '/Admin/AddAuthor',
                controller: 'addAuthorCtrl'
            })
            .when('/addBook',
            {
                templateUrl: '/Admin/AddBook',
                //controller: 'loginCtrl'
            })
            .when('/addGenre',
            {
                templateUrl: '/Admin/AddGenre',
                //controller: 'loginCtrl'
            })
            .when('/addPromotion',
            {
                templateUrl: '/Admin/AddPromotion',
                //controller: 'loginCtrl'
            })
            .when('/addPublisher',
            {
                templateUrl: '/Admin/AddPublisher',
                //controller: 'loginCtrl'
            })
            .when('/genre/books/:genreId',
            {
                templateUrl: '/Book/Books',
                controller: 'booksGenreCtrl'
            })
            .when('/order',
            {
                templateUrl: '/User/Order',
                controller: 'orderCtrl'
            })
            .when('/books/:bookId',
            {
                templateUrl: '/Book/SingleBook',
                controller: 'singleBookCtrl'
            })
            .when('/contacts',
            {
                templateUrl: '/Home/Сontacts'
            })
           .otherwise(
            {
                redirectTo: '/'
            });
    });