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
            List<Mercado> mercado = repo.RetrieveList();
            return mercado;
        }

        // GET: api/Mercado/?Id=id
        public Mercado Get(int Id)
        {
            var repo = new MercadoRepository();
            Mercado m = repo.Retrieve(Id);
            return m;
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
