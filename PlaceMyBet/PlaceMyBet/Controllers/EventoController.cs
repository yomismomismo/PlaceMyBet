using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PlaceMyBet.Models;

namespace PlaceMyBet.Controllers
{
    public class EventoController : ApiController
    {
        // GET: api/Evento
        public IEnumerable<Evento> Get()
        {
            var repo = new EventoRepository();
            List<Evento> evento = repo.RetrieveList();
            return evento;
        }

        // GET: api/Evento/?Id=id
        public EventoDTO Get(int Id)
        {

            var repo = new EventoRepository();
            EventoDTO e = repo.Retrieve(Id);
            return e;

        }


        // GET: api/Evento/?idEvento=id&tipoMercado=tipo
        public Mercado GetMercado(int idEvento, double tipoMercado)
        {
            var repoMercado = new MercadoRepository();
            Mercado e = repoMercado.getMercado(idEvento, tipoMercado);
            return e;

        }

        // POST: api/Evento
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Evento/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Evento/5
        public void Delete(int id)
        {
        }
    }
}
