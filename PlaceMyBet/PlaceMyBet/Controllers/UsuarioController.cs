using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PlaceMyBet.Models;

namespace PlaceMyBet.Controllers
{
    public class UsuarioController : ApiController
    {
        // GET: api/Usuario
        public IEnumerable<Usuario> Get()
        {
            var repo = new UsuarioRepository();
            List<Usuario> usuario = repo.RetrieveList();
            return usuario;
        }

        // GET: api/Usuario/?Email=email
        public Usuario Get(string Email)
        {
            var repo = new UsuarioRepository();
            Usuario u = repo.Retrieve(Email);
            return u;
        }

        // POST: api/Usuario
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Usuario/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Usuario/5
        public void Delete(int id)
        {
        }
    }
}
