namespace Controllers
{
    using Infra.Base;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Dynamic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Description;

    public class ativoController : ApiController
    {
        private DbContext dblocal = new DbContext("name=crm_sgr");
        private dynamic retorno = new ExpandoObject();
        FuncoesBanco funcdb;
        private bool LigaRepresentante = false;
        private string OrderBy = "";
        private string FiltroDDD = " ";
        private string FiltroEstado = " ";

        [Route("api/Ativo/localizar/{id}")]
        [HttpGet]
        public dynamic Localizar(int id)
        {
            funcdb = new FuncoesBanco(dblocal);
            retorno.CODIGO = 0;
            retorno.id = id;
            GetParamOperador();
            GetProximaLigacao();
            return Json(retorno);
        }

        private void GetProximaLigacao()
        {
            GetLigacaoNaoFinalizada();

            if (retorno.CODIGO == 0)
            {
                ExecSql_Q1();
            }

            if (retorno.CODIGO == 0)
            {
                ExecSql_Q0();
            }
        }

        private void GetLigacaoNaoFinalizada()
        {
            retorno.SqlExec = "q7"; //busca ligação que iniciou e não teve resultado            

            var dados = funcdb.ExecSql("SELECT cc.CODIGO FROM campanhas_clientes cc inner join clientes cli on cli.CODIGO = cc.CLIENTE "
             + " WHERE cc.CONCLUIDO = 'SIM' AND coalesce(cc.RESULTADO,0) = 0 AND cc.DATA_HORA_LIG >= 0 "
             + "  AND cc.OPERADOR_LIGACAO = " + retorno.id.ToString() + " and cli.ATIVO = 'SIM' LIMIT 1; ");

            if (dados.Count > 0)
            {
                retorno.CODIGO = Convert.ToInt32(dados[0]);
            }
        }

        private void ExecSql_Q1()
        {
            retorno.SqlExec = "Q1";

            var xsql = " set @CODIGO = 0; UPDATE campanhas_clientes CCC SET  CCC.concluido = 'SIM', CCC.OPERADOR_LIGACAO = "
                + retorno.id.ToString() + " WHERE CCC.CODIGO = (@CODIGO := (SELECT cc.CODIGO FROM (SELECT CODIGO,CLIENTE, "
                + " OPERADOR, CAMPANHA,DT_AGENDAMENTO,OPERADOR_LIGACAO,AGENDA, ORDEM,FONE1 FROM campanhas_clientes "
                + " WHERE CONCLUIDO = 'NAO' AND DT_AGENDAMENTO <= NOW() AND LENGTH(FONE1) >= 10 AND COALESCE(OPERADOR_LIGACAO, 0) = 0 "
                + " AND AGENDA IN(-200, -102, -250, -240, -260, -270) ";

            if (LigaRepresentante)
            {
                xsql = xsql + " AND (OPERADOR = " + retorno.id.ToString() + " OR OPERADOR = -2) ";
            }
            else
            {
                xsql = xsql + " AND OPERADOR = " + retorno.id.ToString();
            }

            xsql = xsql + " ) cc, campanhas c, clientes c_ WHERE c_.CODIGO = cc.CLIENTE AND c_.ATIVO = 'SIM' AND cc.CAMPANHA = c.CODIGO "
                + " AND NOT EXISTS(SELECT ec.CIDADE from excecoes_cidade ec WHERE ec.CIDADE = c_.CIDADE) "
                + " AND NOT EXISTS(SELECT ee.ESTADO from excecoes_estado ee WHERE ee.ESTADO = c_.ESTADO) "
                + " AND NOT EXISTS(SELECT e.COD_ERP from excecoes e WHERE e.COD_ERP = c_.COD_ERP) "
                + ValidaFusoHorario()
                + " ORDER BY " + OrderBy + " LIMIT 1 )); "
                + " SELECT COALESCE(@CODIGO,0); ";

            var dados = funcdb.ExecSql(xsql);

            if (dados.Count > 0)
            {
                retorno.CODIGO = Convert.ToInt32(dados[0]);
            }

        }

        private void ExecSql_Q0()
        {
            retorno.SqlExec = "Q0";

            var dados = funcdb.ExecSql(" set @CODIGO = 0; UPDATE campanhas_clientes CCC SET  CCC.concluido = 'SIM', CCC.OPERADOR_LIGACAO = "
                + retorno.id.ToString() + " WHERE CCC.CODIGO = (@CODIGO:= (SELECT cc.CODIGO FROM (SELECT CODIGO, CLIENTE, CAMPANHA, "
                + " DT_AGENDAMENTO, OPERADOR_LIGACAO, AGENDA, ORDEM, OPERADOR, FONE1 FROM campanhas_clientes WHERE "
                + " CONCLUIDO = 'NAO' AND DT_AGENDAMENTO <= NOW() "
                + " AND LENGTH(FONE1) >= 10 AND((OPERADOR_LIGACAO IS NULL)OR(OPERADOR_LIGACAO = 0)) "
                + " AND((OPERADOR IS NULL) OR(OPERADOR = 0)) "
                + " AND AGENDA IN(-200, -102, -250, -240, -260, -270) "
                + " AND OPERADOR <> -2) cc, "
                + " campanhas c, campanhasxoperadores co, clientes c_ WHERE c_.CODIGO = cc.CLIENTE "
                + " AND DATE(c.DATA_INI) <= DATE(NOW()) "
                + " AND cc.CAMPANHA = c.CODIGO "
                + " AND co.CAMPANHA = cc.CAMPANHA "
                + " AND co.OPERADOR = 2 "
                + " AND c_.OPERADOR <> -2 "
                + " AND c_.ATIVO = 'SIM' "
                + " AND NOT EXISTS(SELECT ec.CIDADE FROM excecoes_cidade ec WHERE ec.CIDADE = c_.CIDADE) "
                + " AND NOT EXISTS(SELECT ee.ESTADO FROM excecoes_estado ee WHERE ee.ESTADO = c_.ESTADO) "
                + " AND NOT EXISTS(SELECT e.COD_ERP FROM excecoes e WHERE e.COD_ERP = c_.COD_ERP) "
                + " AND NOT EXISTS(SELECT es.CODIGO FROM excecoes_segmentos es WHERE es.SEGMENTO = c_.SEGMENTO) "
                + ValidaFusoHorario()
                + FiltroDDD + FiltroEstado
                + " ORDER BY " + OrderBy + " LIMIT 1)); SELECT COALESCE(@CODIGO,0); ");

            if (dados.Count > 0)
            {
                retorno.CODIGO = Convert.ToInt32(dados[0]);
            }

        }

        private string GetOrderBy()
        {
            var dados = funcdb.ExecSql("SELECT ORDERBY FROM parametros");

            if (dados.Count > 0)
            {
                return dados[0];
            }
            else
            {
                return "";
            }
        }

        private string ValidaFusoHorario()
        {
            var param = funcdb.ExecSql("SELECT FUSO_HORARIO FROM parametros");

            if (param.Count > 0)
            {
                if (param[0] == "S")
                {
                    var hrs = funcdb.ExecSql(" SELECT H.ENTRADA_1, H.SAIDA_1 FROM  HORARIOS H, OPERADORES O  WHERE O.CODIGO = "
                        + retorno.id.ToString() + " AND O.HORARIO = H.CODIGO ");

                    if (hrs.Count > 0)
                    {
                        return " AND (coalesce(c_.ESTADO,'') = '' OR (SELECT ADDTIME(CURTIME(), TIME_FORMAT(CONCAT(FUSOHORARIO,':00:00'), '%H:%i:%s')) HORA FROM uf WHERE uf.UF = c_.ESTADO) "
                        + " BETWEEN TIME_FORMAT('" + hrs[0] + "', '%H:%i:%s') AND TIME_FORMAT('" + hrs[1] + "', '%H:%i:%s')) ";
                    }
                }
            }

            return " ";

        }

        private void GetParamOperador()
        {
            var dados = funcdb.ExecSql("SELECT ORDERBY FROM parametros");

            if (dados.Count > 0 && dados[0] != null)
            {
                OrderBy = dados[0];
            }

            dados = funcdb.ExecSql("SELECT LIGA_REPRESENTANTE FROM operadores WHERE CODIGO = " + retorno.id.ToString());

            if (dados.Count > 0 && dados[0] != null)
            {
                LigaRepresentante = dados[0] == "SIM";
            }

            dados = funcdb.ExecSql("SELECT GROUP_CONCAT(DDD) FROM ddd_operadores WHERE OPERADOR = " + retorno.id.ToString());

            if (dados.Count > 0 && dados[0] != null)
            {
                FiltroDDD = " AND LEFT(cc.FONE1,2) IN (" + dados[0].ToString() + ") ";
            }

            dados = funcdb.ExecSql("SELECT GROUP_CONCAT(ESTADO) FROM estados_operadores WHERE OPERADOR = " + retorno.id.ToString());
            
            if (dados.Count > 0 && dados[0] != null)
            {
                FiltroEstado = dados[0].ToString().Replace(",", "','");
                FiltroEstado = " AND c_.ESTADO IN  (" + FiltroEstado + ") ";
            }            
        }
    }
}