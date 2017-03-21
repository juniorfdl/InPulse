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
        private Context dblocal = new Context();

        [Route("api/Ativo/localizar/{id}")]
        [HttpGet]
        public dynamic Localizar(int id)
        {
            dynamic retorno = new ExpandoObject();
            retorno.id = id;
            retorno.SqlExec = "q7";
            retorno.CODIGO = 0;
            FuncoesBanco f = new FuncoesBanco(dblocal);
            var dados = f.ExecSql( "SELECT cc.CODIGO FROM campanhas_clientes cc inner join clientes cli on cli.CODIGO = cc.CLIENTE "
             + " WHERE cc.CONCLUIDO = 'SIM' AND(cc.RESULTADO = 0 OR cc.RESULTADO IS NULL) AND DATE(cc.DATA_HORA_LIG) = DATE(NOW()) "
             + "  AND cc.OPERADOR_LIGACAO = "+id.ToString()+" and cli.ATIVO = 'SIM' LIMIT 1; ");

            if(dados.Count > 0) {
                retorno.CODIGO = dados[0];
            }           


            return Json(retorno); 
        }


    }
}