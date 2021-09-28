using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CourseWeb.Models;
using System.IO;
using System.Data.Entity.Validation;
using CourseWeb.Models.DBModels;

namespace CourseWeb.Controllers
{
    public class RECIPIESController : Controller
    {
        private RECIPIESCONTEXT db = new RECIPIESCONTEXT();


        public async Task<ActionResult> Index()
        {


            return View(await db.Recipies.ToListAsync());
        }
        public async Task<ActionResult> IndexComponents(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<ComponentsLink> link = await db.ComponentsLinks.Where(p => p.RecipyId == id).ToListAsync();
            return View(link);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipy rECIPIES = await db.Recipies.FindAsync(id);
            if (rECIPIES == null)
            {
                return HttpNotFound();
            }
            return View(rECIPIES);
        }


        public ActionResult Create()
        {
            var mymodel = new ListMode();
            Recipy rECIPy = new Recipy();
            mymodel.Recipy = rECIPy;
            var k = new List<Component>();
            for (int i = 0; i < 20; i++)
                k.Add(new Component());
            mymodel.Img = null;
            mymodel.Components = k;
            mymodel.User = LogController.Current;
            return View(mymodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ListMode collection)
        {

            byte[] bytes;
            using (BinaryReader br = new BinaryReader(collection.Img.InputStream))
            {
                bytes = br.ReadBytes(collection.Img.ContentLength);
            }
            IEnumerable<Component> selected = collection.Components;
            collection.Recipy.State = 0;
            collection.Recipy.Image = bytes;
            collection.Recipy.UserCreator = LogController.Current.ID;
            db.Recipies.Add(collection.Recipy);
            db.SaveChanges();
            Recipy res = collection.Recipy;
            foreach (Component id in selected)
            {
                if (id.Name != null)
                {
                    Component dish = new Component()
                    {
                        Name = id.Name,
                        Value = id.Value
                    };
                    db.Components.Add(dish);

                    db.SaveChanges();
                    ComponentsLink link = new ComponentsLink() { ComponentId = dish.Id, RecipyId = res.Id };
                    db.ComponentsLinks.Add(link);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: RECIPIES/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipy rECIPIES = await db.Recipies.FindAsync(id);
            if (rECIPIES == null)
            {
                return HttpNotFound();
            }
            return View(rECIPIES);
        }

        // POST: RECIPIES/Edit/5
 
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Recipy rECIPIES)
        {

            Recipy res = new Recipy() { Id = (int)rECIPIES.Id, Image = rECIPIES.Image, UserCreator = rECIPIES.UserCreator };
            res = db.Recipies.Find(rECIPIES.Id);
            res.State = rECIPIES.State;
            res.Name = rECIPIES.Name;
            res.Time = rECIPIES.Time;
            res.Adress = rECIPIES.Adress;

            db.Entry(res).State = EntityState.Modified;
            
            try
            {

                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    Response.Write("Object: " + validationError.Entry.Entity.ToString());
                    Response.Write("");
                        foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        Response.Write(err.ErrorMessage + "");
                        }
                }
            }

            return View(rECIPIES);
        }

        // GET: RECIPIES/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipy rECIPIES = await db.Recipies.FindAsync(id);
            if (rECIPIES == null)
            {
                return HttpNotFound();
            }
            return View(rECIPIES);
        }

        // POST: RECIPIES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Recipy rECIPIES = await db.Recipies.FindAsync(id);
            db.Recipies.Remove(rECIPIES);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
