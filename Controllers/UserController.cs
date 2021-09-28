using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CourseWeb.Models;
using CourseWeb.Models.DBModels;

namespace CourseWeb.Controllers
{

    public class UserController : Controller
    {

        RECIPIESCONTEXT db = new RECIPIESCONTEXT();
        // GET: User
        public async Task<ActionResult> Index()
        {
            List<Recipy> res = new List<Recipy>();
            //res = db.Recipies.ToList();
            foreach (Recipy Recipy in db.Recipies.ToList())
            {

                foreach (LeftComponentsLink item in Recipy.LeftComponentsLink.ToList())
                {
                    if (item.UserId == LogController.Current.ID && Recipy.UserCreator != LogController.Current.ID && Recipy.State == 1)
                    {
                        res.Add(Recipy);
                        break;
                    }
                }
            }
            ViewBag.Messages = res;
            return View(await db.Recipies.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var mymodel = new SubMode();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipy rECIPIES = await db.Recipies.FindAsync(id);
            if (rECIPIES == null)
            {
                return HttpNotFound();
            }
            mymodel.User = LogController.Current;
            mymodel.Recipy = rECIPIES;
            for(int k = 0; k< rECIPIES.ComponentsLink.Count; k++)
            {
                mymodel.Comp.Add(new CompModel()
                {
                    ID = rECIPIES.ComponentsLink.ToList()[k].Component.Id,
                    NAME = rECIPIES.ComponentsLink.ToList()[k].Component.Name,
                    VALUE = rECIPIES.ComponentsLink.ToList()[k].Component.Value,
                    isChecked = false
                });
                for (int o = 0; o < rECIPIES.LeftComponentsLink.ToList().Count; o++)
                {
                    
                    if (rECIPIES.LeftComponentsLink.ToList().ElementAt(o).Component.Id == rECIPIES.ComponentsLink.ToList().ElementAt(k).Component.Id)
                    {
                        mymodel.Comp[k].isChecked = true;

                        break;
                    }
                }
            }

            return View(mymodel);
        }

        [HttpPost]  
        [ValidateAntiForgeryToken]
        public ActionResult Details(SubMode collection)
        {
            try
            {
                IEnumerable<Component> selected = collection.Components;

                Recipy res = collection.Recipy;
                bool check = true;
                for (int i = 0; i < collection.Comp.Count; i++)
                {
                    check = true;
                    if (collection.Comp[i].isChecked == false)
                    {
                        check = false;
                    }
                    foreach (LeftComponentsLink id in res.LeftComponentsLink.ToList())
                    {

                        if (id.Component.Id == collection.Comp[i].ID)
                        {
                            check = false;
                            break;
                        }
                    }
                    
                    if (check) db.LeftComponentsLink.Add(new LeftComponentsLink()
                    {
                        ComponentId = collection.Comp[i].ID,
                        RecipyId = collection.Recipy.Id,
                        UserId = LogController.Current.ID
                    });
                }
                db.SaveChanges();
                ViewBag.Message = "Subscribed successfully";
                return RedirectToAction("Index");
            }

            catch
            {
                return View("User/Details/"+ collection.Recipy.Id);
            }
        }
        // GET: User/Create
        public ActionResult Create()
        {
            var mymodel = new ListMode();
            Recipy Recipy = new Recipy();
            mymodel.Recipy = Recipy;
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
                    if (!string.IsNullOrEmpty(id.Name))
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

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Recipy RecipyRemove = await db.Recipies.FindAsync(id);
            var compList = new List<Component>();
            for(int i =0; i< RecipyRemove.ComponentsLink.Count; i++)
            {
                compList.Add(db.Components.Find(RecipyRemove.ComponentsLink.ToList()[i].Component.Id));
            }
            db.Recipies.Remove(RecipyRemove);
            db.Components.RemoveRange(compList);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Ready(int? id)
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
            Recipy res = new Recipy() { Id = (int)id, State = 1 };
            res = db.Recipies.Find(id);
            res.State = 1;
            db.Entry(res).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
