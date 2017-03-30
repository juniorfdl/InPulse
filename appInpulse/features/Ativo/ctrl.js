
var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var App;
(function (App) {
    var Controllers;
    (function (Controllers) {
        'use strict';
        var CrudAtivoCtrl = (function (_super) {

            __extends(CrudAtivoCtrl, _super);
            function CrudAtivoCtrl($rootScope, api, CrudAtivoService, $q, $scope) {
                var _this = this;
                var _rootScope = $rootScope;
                _super.call(this);

                this.GetProximaLigacao = function () {
                    this.crudSvc.GetProximaLigacao($rootScope.currentUser.id).then(function (dados) {
                        _this.dadosLigacao = dados;
                    });
                }

                ExecutaStart();
                function ExecutaStart() {
                    _this.$rootScope = $rootScope;
                    _this.api = api;
                    _this.crudSvc = CrudAtivoService;
                    //_this.lista = lista;
                    _this.ApenasConsulta = true;
                    _this.GetProximaLigacao();
                }

                

            }

            CrudAtivoCtrl.prototype.crud = function () {
                return "Ativo";
            };

            CrudAtivoCtrl.prototype.overrideApenasConsulta = function () {
                return true;
            }

            return CrudAtivoCtrl;
        })(Controllers.CrudBaseEditCtrl);
        Controllers.CrudAtivoCtrl = CrudAtivoCtrl;

        App.modules.Controllers.controller('CrudAtivoCtrl', CrudAtivoCtrl);

    })(Controllers = App.Controllers || (App.Controllers = {}));
})(App || (App = {}));
//# sourceMappingURL=ctrl.js.map