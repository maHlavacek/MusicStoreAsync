using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Contract = MusicStore.Contracts.Persistence.IGenre;
using Model = MusicStore.Transfer.Models.Persistence.Genre;

namespace MusicStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : GenericController<Contract, Model>
    {
        // GET: api/Album
        [HttpGet]
        public IEnumerable<Contract> Get()
        {
            return GetAll();
        }

        // GET: api/Album/5
        [HttpGet("{id}")]
        public Contract Get(int id)
        {
            return GetById(id);
        }

        // POST: api/Album
        [HttpPost]
        public void Post([FromBody] Model model)
        {
            Insert(model);
        }

        // PUT: api/Album/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Model model)
        {
            Update(id, model);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            DeleteById(id);
        }
    }
}
