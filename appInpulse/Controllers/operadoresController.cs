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

    public class operadoresController : CrudControllerBase<operadores>
    {

        protected override IOrderedQueryable<operadores> Ordenar(IQueryable<operadores> query)
        {
            return query.OrderBy(e => e.id);
        }


        [Route("api/operadores/localizar")]
        [HttpGet]
        public IHttpActionResult Localizar([FromUri]operadores usuario)
        {
            operadores item = db.Set<operadores>().Where(e => e.LOGIN == usuario.LOGIN & e.SENHA == usuario.SENHA).FirstOrDefault();


            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }


    }
}