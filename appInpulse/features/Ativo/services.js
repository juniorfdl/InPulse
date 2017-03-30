

var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};

var App;
(function (App) {
    var Services;
    (function (Services) {
        "use strict";
        var CrudAtivoService = (function (_super) {
            __extends(CrudAtivoService, _super);

            function CrudAtivoService($q, api) {
                _super.apply(this, arguments);

                this.GetProximaLigacao = function (operador) {
                    debugger;
                    return this.api.allLook(null, 'ativo/localizar/' + operador);
                };

            }

            Object.defineProperty(CrudAtivoService.prototype, "baseEntity", {
                /// @override
                get: function () {
                    return 'Ativo';
                },
                enumerable: true,
                configurable: true
            });            
   
            return CrudAtivoService;
        })(Services.CrudBaseService);
        Services.CrudAtivoService = CrudAtivoService;
        App.modules.Services
            .service('CrudAtivoService', CrudAtivoService);
    })(Services = App.Services || (App.Services = {}));
})(App || (App = {}));
//# sourceMappingURL=services.js.map