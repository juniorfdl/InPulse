
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
                                    _this.MetasXVendasVALOR.push(MetasXVendas[i].VALOR_VENDA);
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

                this.GraficoPropostaXVendas = function () {
                    _this.PropostaXVendasOPE = [];
                    _this.PropostaXVendasVALOR = [];
                    _this.PropostaXVendasCor1 = [];
                    _this.PropostaXVendasCor2 = [];

                    if (_this.lista.length > 0) {
                        if (_this.lista[0].MetasXVendas != null) {

                            var PropostaXVendas = _this.lista[0].MetasXVendas;

                            for (var i = 0; i < PropostaXVendas.length; i++) {
                                if (PropostaXVendas[i].VALOR_PROPOSTA > 0) {
                                    _this.PropostaXVendasOPE.push("Proposta: " + PropostaXVendas[i].LOGIN);
                                    _this.PropostaXVendasVALOR.push(PropostaXVendas[i].VALOR_PROPOSTA);
                                    _this.PropostaXVendasCor1.push("#008000");
                                    _this.PropostaXVendasCor2.push("#66A2EB");

                                    _this.PropostaXVendasOPE.push("Vendas: " + PropostaXVendas[i].LOGIN);
                                    _this.PropostaXVendasVALOR.push(PropostaXVendas[i].VALOR_VENDA);
                                    _this.PropostaXVendasCor1.push("#5307e8");
                                    _this.PropostaXVendasCor2.push("#66A2EB");
                                }
                            }
                        }
                    }

                    if (_this.MeSeChartProposta != null) {
                        _this.MeSeChartProposta.clear();
                        _this.MeSeChartProposta.destroy();
                    }

                    _this.MeSeContextProposta = document.getElementById("GraficoPropostaXVendas").getContext("2d");

                    _this.MeSeChartProposta = {
                        labels: _this.PropostaXVendasOPE,
                        datasets: [{
                            label: "Proposta X Vendas",
                            data: _this.PropostaXVendasVALOR,
                            backgroundColor: _this.PropostaXVendasCor1,
                            hoverBackgroundColor: _this.PropostaXVendasCor2
                        }]
                    };

                    _this.MeSeChartProposta = new Chart(_this.MeSeContextProposta, {
                        type: 'bar',
                        data: _this.MeSeChartProposta,
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

                this.GetOperadoresPorPagina = function () {

                    if (this.ITENS_POR_PAGINA > 0) {
                        localStorage.setItem("OperadoresPorPagina", this.ITENS_POR_PAGINA);
                    } else {
                        if (localStorage.OperadoresPorPagina != null)
                            this.ITENS_POR_PAGINA = parseInt(localStorage.OperadoresPorPagina);

                        if (this.ITENS_POR_PAGINA == null) {
                            this.ITENS_POR_PAGINA = 5;
                            localStorage.setItem("OperadoresPorPagina", this.ITENS_POR_PAGINA);
                        }
                    }

                    if (localStorage.TEMPO_ATUALIZACAO_MIN != null) {
                        this.TEMPO_ATUALIZACAO_MIN = parseInt(localStorage.TEMPO_ATUALIZACAO_MIN);
                    }

                }

                ExecutaStart();
                function ExecutaStart() {
                    _this.$rootScope = $rootScope;
                    _this.api = api;
                    _this.crudSvc = CrudDashboardService;
                    _this.lista = lista;
                    _this.ApenasConsulta = true;

                    _this.GraficoVendasPorEstado();
                    _this.GraficoMetasXVendas();
                    _this.GraficoPropostaXVendas();
                    _this.GetOperadoresPorPagina();
                }

                this.Pesquisar = function () {
                    this.mostrarFiltros = false;
                }                

                this.BuscarDados = function () {

                    if (this.DATAINICIAL == null) {
                        var now = new Date();
                        this.DATAINICIAL = now;
                        this.DATAFINAL = now;
                    }

                    this.GetOperadoresPorPagina();

                    this.crudSvc.BuscarDados(this.DATAINICIAL, this.DATAFINAL).then(function (dados) {                        
                        var VendasPorEstado = null;
                        var MetasXVendas = null;

                        for (var i in dados) {
                            var ii = _this.lista.filter(x => x.id == dados[i].id);

                            if (ii.length > 0) {
                                ii[0].PRODUTIVIDADE = dados[i].PRODUTIVIDADE;
                                ii[0].STATUS_ATUAL = dados[i].STATUS_ATUAL;
                                ii[0].PEDIDOS = dados[i].PEDIDOS;
                                ii[0].LIGACOES = dados[i].LIGACOES;
                                ii[0].CONTATOS = dados[i].CONTATOS;
                                ii[0].APROVEITAMENTO = dados[i].APROVEITAMENTO;
                                ii[0].VendasPorEstado = dados[i].VendasPorEstado;
                                ii[0].MetasXVendas = dados[i].MetasXVendas;
                                //ii[0] = dados[i];

                                if (dados[i].MetasXVendas != null) {
                                    MetasXVendas = dados[i].MetasXVendas;
                                }

                                if (dados[i].VendasPorEstado != null) {
                                    VendasPorEstado = dados[i].VendasPorEstado;
                                }
                            }

                            _this.lista[0].MetasXVendas = MetasXVendas;
                            _this.lista[0].VendasPorEstado = VendasPorEstado;
                        }

                        _this.mostrarFiltros = false;
                        _this.GraficoVendasPorEstado();
                        _this.GraficoMetasXVendas();
                        _this.GraficoPropostaXVendas();
                    });
                }               

                this.GetTempo = function (Tempo) {
                    if (Tempo != null) {
                        try {
                            var now = new Date();
                            var hora = now.getTime();
                            var Dia = new Date(now.getFullYear(), now.getMonth(), now.getDate(),
                                Tempo.substring(0, 2), Tempo.substring(3, 5), Tempo.substring(6, 8), "01");

                            var t = Date.parse(Dia);
                            var diff = hora - t;
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
                
                this.GetTEMPO_ATUALIZACAO_MIN = function () {

                    if (this.TEMPO_ATUALIZACAO_MIN == null || this.TEMPO_ATUALIZACAO_MIN < 5) {
                        this.TEMPO_ATUALIZACAO_MIN = 5;
                    }

                    localStorage.setItem("TEMPO_ATUALIZACAO_MIN", this.TEMPO_ATUALIZACAO_MIN);
                    return this.TEMPO_ATUALIZACAO_MIN * 1000;
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