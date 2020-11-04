using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PlaceMyBet.Models;

namespace PlaceMyBet.Controllers
{
    public class ApuestaController : ApiController
    {
        // GET: api/Apuesta
        public IEnumerable<ApuestaDTO> Get()
        {
            var repo = new ApuestaRepository();
            List<ApuestaDTO> apuestaDTO = repo.RetrieveList();
            return apuestaDTO;
        }

        // GET: api/Apuesta/?Id=id
        public ApuestaDTO Get(int Id)
        {

            var repo = new ApuestaRepository();
            ApuestaDTO a = repo.Retrieve(Id);
            return a;

        }


        // POST: api/Apuesta
        public void Post([FromBody]ApuestaDTO apuesta)
        {
            var repoMercado = new MercadoRepository();
            var repo = new ApuestaRepository();

           var cuota = repo.RetrieveCuotas(apuesta);

            repoMercado.UpdateDinero(apuesta);
           var dinero = repoMercado.RetrieveDinero();

            repoMercado.Calculos(apuesta, dinero);
            repo.Save(apuesta, cuota);
        }

        // PUT: api/Apuesta/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Apuesta/5
        public void Delete(int id)
        {
        }
    }
}
