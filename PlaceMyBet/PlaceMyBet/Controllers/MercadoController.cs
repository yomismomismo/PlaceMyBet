using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PlaceMyBet.Models;

namespace PlaceMyBet.Controllers
{
    public class MercadoController : ApiController
    {
        // GET: api/Mercado
        public IEnumerable<Mercado> Get()
        {
            var repo = new MercadoRepository();
            List<Mercado> evento = repo.RetrieveList();
            return evento;
        }

        // GET: api/Mercado/?Id=id
        public MercadoDTO Get(int Id)
        {
            var repo = new MercadoRepository();
            MercadoDTO m = repo.Retrieve(Id);

            return m;
        }

        // GET: api/Apuesta/?tipoMercado=tipo&Email=email
        public IEnumerable<ApuestaMercado> GetApuestasMercado(double tipoMercado, string Email)
        {

            var repo = new MercadoRepository();
            List<ApuestaMercado> apuestasUser = repo.GetApuestasMercado(tipoMercado, Email);
            return apuestasUser;

        }

        // POST: api/Mercado
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Mercado/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Mercado/5
        public void Delete(int id)
        {
        }
    }
}
