using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MusicStore.WebApi.Controllers
{
    public abstract class GenericController<I, M> : ControllerBase
        where M : Transfer.Models.TransferObject, I, Contracts.ICopyable<I>, new()
        where I : Contracts.IIdentifiable
    {
        protected Logic.IController<I> CreateController()
        {
            return Logic.Factory.CreateController<I>();
        }
        public IEnumerable<I> GetAll()
        {
            using var ctrl = CreateController();

            return ctrl.GetAll();
        }

        public I GetById(int id)
        {
            using var ctrl = CreateController();

            return ctrl.GetById(id);
        }

        public void Insert([FromBody] M model)
        {
            using var ctrl = CreateController();
            ctrl.Insert(model);
            ctrl.SaveChanges();
        }

        public void Update(int id, [FromBody] M model)
        {
            using var ctrl = CreateController();
            ctrl.Update(model);
            ctrl.SaveChanges();
        }

        public void DeleteById(int id)
        {
            using var ctrl = CreateController();
            ctrl.Delete(id);
            ctrl.SaveChanges();
        }
    }
}
