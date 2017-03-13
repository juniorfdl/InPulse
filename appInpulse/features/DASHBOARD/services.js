

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
        var CrudDashboardService = (function (_super) {
            __extends(CrudDashboardService, _super);

            function CrudDashboardService($q, api) {
                _super.apply(this, arguments);

                this.BuscarDados = function (DATAINICIAL, DATAFINAL) {
                    var params = { DATAINICIAL: DATAINICIAL, DATAFINAL: DATAFINAL };
                    return this.api.allLook(params, 'dashboard/localizar');
                }
            }

            Object.defineProperty(CrudDashboardService.prototype, "baseEntity", {
                /// @override
                get: function () {
                    return 'Dashboard';
                },
                enumerable: true,
                configurable: true
            });            
   
            return CrudDashboardService;
        })(Services.CrudBaseService);
        Services.CrudDashboardService = CrudDashboardService;
        App.modules.Services
            .service('CrudDashboardService', CrudDashboardService);
    })(Services = App.Services || (App.Services = {}));
})(App || (App = {}));
//# sourceMappingURL=services.js.map