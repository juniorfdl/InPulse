namespace Controllers
{
    using Infra.Base.Interface.Base;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Infra.Base;
    using System.IO;
    using System.Drawing;
    using System.Web;

    public class dashboardController : CrudControllerBase<v_operadores_status>
    {
        private Context dblocal = new Context();
        private int DISCADAS = 0;
        private int CONTATOS = 0;
        private int PEDIDOS = 0;
        private double PRODUTIVIDADE = 0;
        private double APROVEITAMENTO = 0;
        private dynamic VendasPorEstado;
        private dynamic MetasXVendas;

        //public dashboardController()
        //{
        //    ControllerBaseIP IP = new ControllerBaseIP();
        //    var x = IP.GetClientIp(this.Request);
        //    x = "";
        //}

        protected override IOrderedQueryable<v_operadores_status> Ordenar(IQueryable<v_operadores_status> query)
        {
            return query.OrderBy(e => e.NOME);
        }

        protected override IQueryable<v_operadores_status> TrazerDadosParaLista(IQueryable<v_operadores_status> query)
        {
            int i = 0;
            foreach (var item in query)
            {
                i++;
                var foto = dblocal.Set<operadores_foto>().Where(q => q.id == item.id).FirstOrDefault();

                if (foto != null)
                {
                    var diretorio = HttpContext.Current.Server.MapPath("~") + @"\img\" + item.id.ToString() + ".png";
                    item.FOTO = item.id.ToString() + ".png";
                    var f = byteArrayToImage(foto.FOTO);
                    f.Save(diretorio);
                }

                GetValores(item.id, null, null);
                item.APROVEITAMENTO = APROVEITAMENTO;
                item.CONTATOS = CONTATOS;
                item.LIGACOES = DISCADAS;
                item.PRODUTIVIDADE = PRODUTIVIDADE;
                item.PEDIDOS = PEDIDOS;

                if (i == 1)
                    item.VendasPorEstado = VendasPorEstado;

            }

            dblocal.Database.Connection.Close();

            return query;
        }

        private void GetValores(int id, DateTime? datainicial, DateTime? datafinal)
        {
            if (datainicial == null)
                datainicial = DateTime.Now;

            if (datafinal == null)
                datafinal = DateTime.Now;

            var x = " select (AVG(XX.PRODUTIVIDADE)) PRODUTIVIDADE, SUM(XX.DISCADAS) DISCADAS, "
   + " SUM(XX.CONTATOS) CONTATOS,  AVG(XX.CONTATOS * 100 / XX.DISCADAS) APROVEITAMENTO, SUM(XX.PEDIDOS) PEDIDOS FROM "
   + " (SELECT ((( (if ((select sum(time_to_sec(cr.LIGACAO_FINALIZADA) - time_to_sec(cr.LIGACAO_RECEBIDA)) "
   + "          from chamadas_receptivo cr where cr.operador = o.codigo and DATE(cr.LIGACAO_RECEBIDA) between '" + String.Format("{0:yyyy-MM-dd}", datainicial) + "' AND '" + String.Format("{0:yyyy-MM-dd}", datafinal) + "' "
   + "        ) + ( "
   + "          select(sum(time_to_sec(data_hora_fim) - time_to_sec(data_hora_lig))) as tempo_falando "
   + "            from campanhas_clientes "
   + "            where "
   + "          OPERADOR_LIGACAO = o.codigo "
   + "          and DATE(data_hora_lig)between '" + String.Format("{0:yyyy-MM-dd}", datainicial) + "' AND '" + String.Format("{0:yyyy-MM-dd}", datafinal) + "' "
   + "        ) "
   + "        is null,if ( "
   + "            (select sum(time_to_sec(cr.LIGACAO_FINALIZADA) - time_to_sec(cr.LIGACAO_RECEBIDA)) "
   + "              from chamadas_receptivo cr "
   + "             where cr.operador = o.codigo "
   + "              and DATE(cr.LIGACAO_RECEBIDA)between '" + String.Format("{0:yyyy-MM-dd}", datainicial) + "' AND '" + String.Format("{0:yyyy-MM-dd}", datafinal) + "' "
   + "            ) is null,(select(sum(time_to_sec(data_hora_fim) - time_to_sec(data_hora_lig))) as tempo_falando "
   + "            from campanhas_clientes where OPERADOR_LIGACAO = o.codigo "
   + "          and DATE(data_hora_lig)between '" + String.Format("{0:yyyy-MM-dd}", datainicial) + "' AND '" + String.Format("{0:yyyy-MM-dd}", datafinal) + "' "
   + "        ),( "
   + "              select sum(time_to_sec(cr.LIGACAO_FINALIZADA) - time_to_sec(cr.LIGACAO_RECEBIDA)) "
   + "              from chamadas_receptivo cr "
   + "              where cr.operador = o.codigo "
   + "              and DATE(cr.LIGACAO_RECEBIDA)between '" + String.Format("{0:yyyy-MM-dd}", datainicial) + "' AND '" + String.Format("{0:yyyy-MM-dd}", datafinal) + "' "
   + "            )),( "
   + "          select sum(time_to_sec(cr.LIGACAO_FINALIZADA) - time_to_sec(cr.LIGACAO_RECEBIDA)) "
   + "          from chamadas_receptivo cr "
   + "          where cr.operador = o.codigo "
   + "          and DATE(cr.LIGACAO_RECEBIDA)between '" + String.Format("{0:yyyy-MM-dd}", datainicial) + "' AND '" + String.Format("{0:yyyy-MM-dd}", datafinal) + "' "
   + "        ) + ( "
   + "          select(sum(time_to_sec(data_hora_fim) - time_to_sec(data_hora_lig))) as tempo_falando "
   + "            from campanhas_clientes  where "
   + "          OPERADOR_LIGACAO = o.codigo "
   + "          and DATE(data_hora_lig)between '" + String.Format("{0:yyyy-MM-dd}", datainicial) + "' AND '" + String.Format("{0:yyyy-MM-dd}", datafinal) + "')))))*100)/ "
   + "    (if (((select sum(time_to_sec(l.tempo_logado)) as tempo_logado from login_ativo_receptivo l "
   + "      where "
   + "        o.codigo = l.operador "
   + "        and modulo = 'Ativo' "
   + "        and DATE(entrada)between '" + String.Format("{0:yyyy-MM-dd}", datainicial) + "' AND '" + String.Format("{0:yyyy-MM-dd}", datafinal) + "' "
   + "    ) -(( "
   + "      select sum(ligacoes_ok) "
   + "      from login_ativo_receptivo l "
   + "      where modulo ='Ativo' "
   + "      and l.OPERADOR = o.codigo "
   + "      and DATE(entrada)between '" + String.Format("{0:yyyy-MM-dd}", datainicial) + "' AND '" + String.Format("{0:yyyy-MM-dd}", datafinal) + "' "
   + "       ) *1)) is null,0,(( "
   + "     select "
   + "        sum(time_to_sec(l.tempo_logado)) as tempo_logado from login_ativo_receptivo l "
   + "      where "
   + "        o.codigo = l.operador "
   + "        and modulo = 'Ativo' "
    + "       and DATE(entrada)between '" + String.Format("{0:yyyy-MM-dd}", datainicial) + "' AND '" + String.Format("{0:yyyy-MM-dd}", datafinal) + "' "
   + "    ) -(( "
   + "      select sum(ligacoes_ok) "
   + "      from login_ativo_receptivo l "
   + "      where modulo = 'Ativo' "
   + "      and l.OPERADOR = o.codigo "
   + "      and DATE(entrada)between '" + String.Format("{0:yyyy-MM-dd}", datainicial) + "' AND '" + String.Format("{0:yyyy-MM-dd}", datafinal) + "' "
   + "       ) *1))) + if ((select "
   + "        sum(time_to_sec(l.tempo_logado)) as tempo_logado from login_ativo_receptivo l "
   + "      where  o.codigo = l.operador "
   + "        and modulo = 'Receptivo' "
   + "        and DATE(entrada)between '" + String.Format("{0:yyyy-MM-dd}", datainicial) + "' AND '" + String.Format("{0:yyyy-MM-dd}", datafinal) + "')is null,0,(select "
   + "        sum(time_to_sec(l.tempo_logado)) as tempo_logado from login_ativo_receptivo l "
   + "      where  o.codigo = l.operador "
   + "        and modulo = 'Receptivo' "
   + "        and DATE(entrada)between '" + String.Format("{0:yyyy-MM-dd}", datainicial) + "' AND '" + String.Format("{0:yyyy-MM-dd}", datafinal) + "')) "
   + "        - (SELECT IFNULL(SUM(TIME_TO_SEC(p.DATA_HORA_FIM) - TIME_TO_SEC(p.DATA_HORA)), 0) AS TEMPO_PAUSA FROM pausas_realizadas p inner join motivos_pausa mp on mp.CODIGO = p.COD_PAUSA and mp.PRODUTIVIDADE = 'SIM'  WHERE DATE(p.DATA_HORA)between '" + String.Format("{0:yyyy-MM-dd}", datainicial) + "' AND '" + String.Format("{0:yyyy-MM-dd}", datafinal) + "' and p.OPERADOR = o.codigo)  "
   + "        ) AS PRODUTIVIDADE, "
   + " CAST(COUNT(distinct cc.CODIGO) AS CHAR) AS DISCADAS, "
   + " CAST(SUM(IF(r.ECONTATO = 'SIM', 1, 0)) AS CHAR) AS CONTATOS, "
   + "      CAST(SUM(IF(r.EPEDIDO = 'SIM', 1, 0)) AS CHAR) AS PEDIDOS FROM operadores o "
   + "   LEFT JOIN campanhas_clientes cc ON (cc.OPERADOR_LIGACAO = o.CODIGO  AND cc.OPERADOR_LIGACAO > 0 "
   + "   AND DATE(cc.DT_RESULTADO) BETWEEN '" + String.Format("{0:yyyy-MM-dd}", datainicial) + "' AND '" + String.Format("{0:yyyy-MM-dd}", datafinal) + "') "
   + "  LEFT JOIN resultados r ON(r.CODIGO = cc.RESULTADO) "
   + "  LEFT JOIN campanhas c ON(c.CODIGO = cc.CAMPANHA) "
   + " WHERE o.CODIGO = " + id.ToString() + ") as XX ";

            FuncoesBanco f = new FuncoesBanco(dblocal);
            List<dynamic> MyList = f.CollectionFromSql(x,
               new Dictionary<string, object> { }).ToList();

            foreach (dynamic item in MyList)
            {
                if (!DBNull.Equals(item.PRODUTIVIDADE, DBNull.Value))
                    PRODUTIVIDADE = Convert.ToDouble(item.PRODUTIVIDADE);
                else PRODUTIVIDADE = 0;
                if (!DBNull.Equals(item.DISCADAS, DBNull.Value))
                    DISCADAS = Convert.ToInt32(item.DISCADAS);
                else
                    DISCADAS = 0;
                if (!DBNull.Equals(item.CONTATOS, DBNull.Value))
                    CONTATOS = Convert.ToInt32(item.CONTATOS);
                else
                    CONTATOS = 0;
                if (!DBNull.Equals(item.APROVEITAMENTO, DBNull.Value))
                    APROVEITAMENTO = Convert.ToDouble(item.APROVEITAMENTO);
                else
                    APROVEITAMENTO = 0;
                if (!DBNull.Equals(item.PEDIDOS, DBNull.Value))
                    PEDIDOS = Convert.ToInt32(item.PEDIDOS);
                else
                    PEDIDOS = 0;
            }

            x = "SELECT cli.ESTADO, SUM(cc.VALOR) as VALOR FROM compras cc "
              + " JOIN clientes cli on cli.CODIGO = cc.CLIENTE AND cli.ESTADO <> '' "
              + " WHERE cc.OPERADOR > 0 AND cc.DATA BETWEEN '" + String.Format("{0:yyyy-MM-dd}", datainicial) 
              + "' AND '" + String.Format("{0:yyyy-MM-dd}", datafinal) + "' "
              + " group by cli.ESTADO having SUM(cc.VALOR) > 0 ORDER BY COUNT(cc.CODIGO) DESC ";

            if (VendasPorEstado == null)
            {
                MyList = f.CollectionFromSql(x,
                   new Dictionary<string, object> { }).ToList();

                VendasPorEstado = MyList;
            }

            x = "SELECT meta.OPERADOR,o.LOGIN, sum(meta.VALOR_META) as META "
            + " FROM operadores_meta meta JOIN operadores o on o.CODIGO = meta.OPERADOR WHERE CAST(CONCAT(meta.ANO, '-', meta.MES, '-01') AS DATE) BETWEEN '" + String.Format("{0:yyyy-MM-dd}", datainicial) + "' AND '" + String.Format("{0:yyyy-MM-dd}", datafinal) + "' "
            + " group by meta.OPERADOR,o.LOGIN ";

            if (MetasXVendas == null)
            {
                MyList = f.CollectionFromSql(x,
                   new Dictionary<string, object> { }).ToList();

                MetasXVendas = MyList;

                foreach(var m in MetasXVendas)
                {
                    x = "SELECT SUM(cc.VALOR) as VALOR FROM compras cc "
                     + " WHERE cc.OPERADOR = "+ m.OPERADOR + " AND cc.DATA BETWEEN '" + String.Format("{0:yyyy-MM-dd}", datainicial)
                     + "' AND '" + String.Format("{0:yyyy-MM-dd}", datafinal) + "' ";

                    var valor = f.ExecSql(x);

                    if (valor != null && valor.Count > 0)
                        m.VALOR_VENDA = Convert.ToDouble(valor[0]);
                    else
                        m.VALOR_VENDA = 0;

                    x = " select p.VALOR from campanhas_clientes a inner join propostas p on p.LIGACAO = a.CODIGO "
                     + " WHERE a.OPERADOR = " + m.OPERADOR + " AND a.DT_RESULTADO BETWEEN '" + String.Format("{0:yyyy-MM-dd}", datainicial)
                     + "' AND '" + String.Format("{0:yyyy-MM-dd}", datafinal) + "' ";
                    valor = f.ExecSql(x);

                    if (valor != null && valor.Count > 0)
                        m.VALOR_PROPOSTA = Convert.ToDouble(valor[0]) ;
                    else
                        m.VALOR_PROPOSTA = 0;
                }

            }
        }

        [Route("api/dashboard/localizar")]
        [HttpGet]
        public IHttpActionResult Localizar([FromUri]filtros filtros)
        {
            var ope = db.Set<v_operadores_status>().ToList();
            var i = 0;

            foreach (var item in ope)
            {
                i++;
                if (filtros != null)
                    GetValores(item.id, filtros.DATAINICIAL, filtros.DATAFINAL);
                else
                    GetValores(item.id, null, null);

                item.APROVEITAMENTO = APROVEITAMENTO;
                item.CONTATOS = CONTATOS;
                item.LIGACOES = DISCADAS;
                item.PRODUTIVIDADE = PRODUTIVIDADE;
                item.PEDIDOS = PEDIDOS;

                if (i == 1)
                {
                    item.VendasPorEstado = VendasPorEstado;
                    item.MetasXVendas = MetasXVendas;
                }

                var foto = dblocal.Set<operadores_foto>().Where(q => q.id == item.id).FirstOrDefault();

                if (foto != null)
                {
                    var diretorio = HttpContext.Current.Server.MapPath("~") + @"\img\" + item.id.ToString() + ".png";
                    item.FOTO =  item.id.ToString() + ".png";
                    var f = byteArrayToImage(foto.FOTO);
                    f.Save(diretorio);
                }
            }

            dblocal.Database.Connection.Close();

            if (ope == null)
            {
                return NotFound();
            }

            return Ok(ope);
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

    }

    public class filtros
    {
        public DateTime? DATAINICIAL { get; set; }
        public DateTime? DATAFINAL { get; set; }
    }
    
}