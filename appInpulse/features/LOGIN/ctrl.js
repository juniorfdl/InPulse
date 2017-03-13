var App;
(function (App) {
    var Controllers;
    (function (Controllers) {
        'use strict';
        App.modules.Controllers.controller('LoginCtrl', function ($location, toaster, security, $scope, luarApp, $rootScope) {
            this.userName = localStorage.getItem("userName");
            this.password = "";
            this.Empresas = false;
            var _this = this;
            _this.EmpresaSelecionada = { CEMP: "" };
            this.NivelLook = [{ NIVEL: 'ATIVO'}, {NIVEL: 'RECEPTIVO'}, {NIVEL: 'ADMIN' }];                      

            $rootScope.cssLogin = 'bodyLogin';

            this.loginOK = function () {
                $rootScope.currentUser.userEmpresa = _this.EmpresaSelecionada.FANTASIA;
                $rootScope.currentUser.userCEMP = _this.EmpresaSelecionada.CEMP;
                var logo = "app/Logo" + $rootScope.currentUser.userEmpresa + ".jpg";
                $rootScope.EmpresaSelecionadaLogo = logo;
            };

            this.login = function () {
                security.login(this.userName, this.password)
                    .then(function (result) {

                        $rootScope.cssLogin = '';
                        _this.loginOK();
                        $location.path('/home');
                        toaster.clear();

                    }, function (error) {
                        var mensagem = error[0];
                        if (mensagem) {
                            toaster.warning("Atenção", mensagem);
                        } else { toaster.warning("Atenção", "Usuário não encontrado"); }
                    });
            };
        })
       .controller('Error404Ctrl', function ($location, $window) {
           $scope.$on('$viewContentLoaded', function () {
               $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
           });
       });
    })(Controllers = App.Controllers || (App.Controllers = {}));
})(App || (App = {}));
//# sourceMappingURL=ctrl.js.map