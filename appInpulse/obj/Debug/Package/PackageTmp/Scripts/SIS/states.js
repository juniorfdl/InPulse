var App;
(function (App) {
    'use strict';

    App.modules.App.config(function ($stateProvider) {

        $stateProvider.state('home', {
            url: '/home',
            templateUrl: 'views/index.html'
        }).state('login', {
            url: '/login',
            layout: 'basic',
            templateUrl: 'views/Login.html',
            controller: 'LoginCtrl',
            controllerAs: 'ctrl',
            data: {
                title: "Entrar"
            }
        }).state('dashboard', {
            url: '',
            templateUrl: 'features/DASHBOARD/edit.html',
            controller: 'CrudDashboardCtrl',
            controllerAs: 'ctrl',
            resolve: {
                lista: function (CrudDashboardService) {
                    return CrudDashboardService.buscar('', 1, 'NOME', true, localStorage.OperadoresPorPagina, '');
                }
            }
        }).state("otherwise",
          {
              url: '/home',
              templateUrl: 'views/index.html'
          }
        );

    });

})(App || (App = {}));
//# sourceMappingURL=app.js.map