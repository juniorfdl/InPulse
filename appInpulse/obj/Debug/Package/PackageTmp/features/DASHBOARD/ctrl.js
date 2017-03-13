
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
        var CrudDashboardCtrl = (function (_super) {

            __extends(CrudDashboardCtrl, _super);
            function CrudDashboardCtrl($rootScope, api, CrudDashboardService, lista, $q, $scope) {
                var _this = this;
                _super.call(this);

                this.$rootScope = $rootScope;

                this.api = api;
                this.crudSvc = CrudDashboardService;
                this.lista = lista;
                this.ApenasConsulta = true;
                this.ITENS_POR_PAGINA = 10;
                this.CamposNotOrderBy = [{ NOME: 'APROVEITAMENTO' },
                { NOME: 'PRODUTIVIDADE' },
                { NOME: 'PEDIDOS' },
                { NOME: 'LIGACOES' },
                { NOME: 'CONTATOS' }];

                this.Pesquisar = function () {
                    this.mostrarFiltros = false;
                }

                this.GraficoVendasPorEstado = function () {
                    _this.estados = [];
                    _this.estadosvalor = [];
                    _this.backgroundColor = [];
                    _this.hoverBackgroundColor = [];

                    if (_this.lista.length > 0) {
                        if (_this.lista[0].VendasPorEstado != null) {

                            var VendasPorEstado = _this.lista[0].VendasPorEstado;

                            for (var i = 0; i < VendasPorEstado.length; i++) {
                                if (VendasPorEstado[i].VALOR > 0) {
                                    _this.estados.push(VendasPorEstado[i].ESTADO);
                                    _this.estadosvalor.push(VendasPorEstado[i].VALOR);
                                    _this.backgroundColor.push("#5307e8");
                                    _this.hoverBackgroundColor.push("#66A2EB");
                                }
                            }
                        }
                    }

                    if (_this.MeSeChart != null) {
                        _this.MeSeChart.clear();
                        _this.MeSeChart.destroy();
                    }

                    _this.MeSeContext = document.getElementById("GraficoBarra").getContext("2d");

                    _this.MeSeData = {
                        labels: _this.estados,
                        datasets: [{
                            label: "Vendas por estado",
                            data: _this.estadosvalor,
                            backgroundColor: _this.backgroundColor,
                            hoverBackgroundColor: _this.hoverBackgroundColor
                        }]
                    };

                    _this.MeSeChart = new Chart(_this.MeSeContext, {
                        type: 'horizontalBar',
                        data: _this.MeSeData,
                        options: {
                            scales: {
                                xAxes: [{
                                    ticks: {
                                        min: 1
                                    }
                                }],
                                yAxes: [{ stacked: true }]
                            }
                        }
                    });

                };

                this.GraficoMetasXVendas = function () {
                    _this.MetasXVendasOPE = [];
                    _this.MetasXVendasVALOR = [];
                    _this.MetasXVendasCor1 = [];
                    _this.MetasXVendasCor2 = [];

                    if (_this.lista.length > 0) {
                        if (_this.lista[0].MetasXVendas != null) {

                            var MetasXVendas = _this.lista[0].MetasXVendas;

                            for (var i = 0; i < MetasXVendas.length; i++) {
                                if (MetasXVendas[i].META > 0) {
                                    _this.MetasXVendasOPE.push("Meta: " + MetasXVendas[i].LOGIN);
                                    _this.MetasXVendasVALOR.push(MetasXVendas[i].META);
                                    _this.MetasXVendasCor1.push("#008000");
                                    _this.MetasXVendasCor2.push("#66A2EB");

                                    _this.MetasXVendasOPE.push("Vendas: " + MetasXVendas[i].LOGIN);
                                    _this.MetasXVendasVALOR.push(120);
                                    _this.MetasXVendasCor1.push("#5307e8");
                                    _this.MetasXVendasCor2.push("#66A2EB");
                                }
                            }
                        }
                    }

                    if (_this.MeSeChartMeta != null) {
                        _this.MeSeChartMeta.clear();
                        _this.MeSeChartMeta.destroy();
                    }

                    _this.MeSeContextMeta = document.getElementById("GraficoMetasXVendas").getContext("2d");

                    _this.MeSeChartMeta = {
                        labels: _this.MetasXVendasOPE,
                        datasets: [{
                            label: "Metas X Vendas",
                            data: _this.MetasXVendasVALOR,
                            backgroundColor: _this.MetasXVendasCor1,
                            hoverBackgroundColor: _this.MetasXVendasCor2
                        }]
                    };

                    _this.MeSeChartMeta = new Chart(_this.MeSeContextMeta, {
                        type: 'bar',
                        data: _this.MeSeChartMeta,
                        options: {
                            scales: {
                                xAxes: [{
                                    ticks: {
                                        min: 1
                                    }
                                }],
                                yAxes: [{ stacked: true }]
                            }
                        }
                    });

                };

                this.BuscarDados = function () {

                    if (this.DATAINICIAL == null) {
                        var now = new Date();
                        this.DATAINICIAL = now;
                        this.DATAFINAL = now;
                    }

                    this.crudSvc.BuscarDados(this.DATAINICIAL, this.DATAFINAL).then(function (dados) {
                        _this.lista = dados;
                        _this.mostrarFiltros = false;
                        _this.GraficoVendasPorEstado();
                        _this.GraficoMetasXVendas();
                    });
                }

                this.GraficoVendasPorEstado();

                this.GraficoMetasXVendas();

                this.GetTempo = function (Tempo) {
                    if (Tempo != null) {
                        try {
                            var now = new Date();
                            _this.hora = now.getTime();
                            Tempo = "Thu Mar 09 2017 " + Tempo + " GMT-0300";
                            var t = Date.parse(Tempo);
                            var diff = _this.hora - t;
                            var msec = diff;
                            var hh = Math.floor(msec / 1000 / 60 / 60);
                            msec -= hh * 1000 * 60 * 60;
                            var mm = Math.floor(msec / 1000 / 60);
                            msec -= mm * 1000 * 60;
                            var ss = Math.floor(msec / 1000);
                            msec -= ss * 1000;
                            Tempo = this.pad2(hh) + ":" + this.pad2(mm) + ":" + this.pad2(ss);
                        } catch (err) {
                            Tempo = "";
                        }

                        return Tempo;
                    } else {
                        return "";
                    }
                };

                $scope.$on('timer-tick', function (event, args) {

                    if (event.targetScope.interval == 1000) {
                        for (var i = 0; i < _this.lista.length; i++) {
                            _this.lista[i].TEMPO_CALC = _this.GetTempo(_this.lista[i].TEMPO);
                        }

                        try {
                            if (args.millis > 1000)
                                $scope.$apply();
                        } catch (err) {

                        }
                    } else {
                        if (!_this.mostrarFiltros)
                          _this.BuscarDados();
                    }
                });

                this.pad2 = function (number) {
                    return (number < 10 ? '0' : '') + number
                }
            }

            CrudDashboardCtrl.prototype.crud = function () {
                return "Dashboard";
            };

            CrudDashboardCtrl.prototype.overrideApenasConsulta = function () {
                return true;
            }

            return CrudDashboardCtrl;
        })(Controllers.CrudBaseEditCtrl);
        Controllers.CrudDashboardCtrl = CrudDashboardCtrl;

        App.modules.Controllers.controller('CrudDashboardCtrl', CrudDashboardCtrl);


    })(Controllers = App.Controllers || (App.Controllers = {}));
})(App || (App = {}));
//# sourceMappingURL=ctrl.js.map