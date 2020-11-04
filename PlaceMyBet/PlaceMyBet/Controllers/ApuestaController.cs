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
        public IEnumerable<Apuesta> Get()
        {
            var repo = new ApuestaRepository();
            List<Apuesta> apuesta = repo.RetrieveList();
            return apuesta;
        }

        // GET: api/Apuesta/?Id=id
        public ApuestaDTO Get(int Id)
        {

            var repo = new ApuestaRepository();
            ApuestaDTO a = repo.Retrieve(Id);
            return a;

        }
        [Authorize(Roles ="admin")]
        // GET: api/Apuesta/?Email=email&tipoMercado=tipo
        public IEnumerable<ApuestaUser> GetApuestasUser(string Email, double tipoMercado)
        {

            var repo = new ApuestaRepository();
            List<ApuestaUser> apuestasUser = repo.getApuestaUser(Email, tipoMercado);
            return apuestasUser;

        }





        // POST: api/Apuesta
        public void Post([FromBody]ApuestaDTO apuesta)
        {
            var repoMercado = new MercadoRepository();
            var repo = new ApuestaRepository();

            double cuota = repo.RetrieveCuotas(apuesta);

            repoMercado.UpdateDinero(apuesta);

            MercadoDinero dinero = repoMercado.RetrieveDinero();

            repoMercado.Calculos(dinero);
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
