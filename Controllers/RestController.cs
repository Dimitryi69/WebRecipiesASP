using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using CourseWeb.Models;
using CourseWeb.Models.DBModels;
using CourseWeb.Models.Rest;

namespace CourseWeb.Controllers
{
    public class RestController : ApiController
    {
        RECIPIESCONTEXT db = new RECIPIESCONTEXT();
        // GET: Rest
        public ICollection<RecipyForRest> GetRecipies()
        {
            var list = new Collection<RecipyForRest>();
            foreach(Recipy r in db.Recipies.ToList())
            {
                list.Add(new RecipyForRest(r));
            }
            return list;
        }

        [System.Web.Http.HttpPut]
        public string PutRecipie( RecipyForRest model)
        {
            List<Component> ComponentList = new List<Component>();
            Recipy resipy = db.Recipies.Add(new Recipy()
            {
                Name = model.NAME,
                Adress = model.ADRESS,
                Image = Convert.FromBase64String(model.IMAGE),
                UserCreator = model.USER_CREATOR,
                State = model.STATE,
                Time = model.TIME
            });
            foreach (RestAllComponents component in model.AllComp)
            {
                ComponentList.Add(db.Components.Add(new Component() {
                    Name = component.COMPONENT.NAME,
                    Value = component.COMPONENT.VALUE
                }));
            }
            db.SaveChanges();
            foreach (Component component in ComponentList)
            {
                db.ComponentsLinks.Add(new ComponentsLink()
                {
                    ComponentId = component.Id,
                    RecipyId = resipy.Id
                });
            }
            
            db.SaveChanges();
            return String.Format("Recipie created!");
        }

        public string Delete(int id)
        {
            db.Recipies.Remove(db.Recipies.Find(id));
            db.SaveChanges();
            return String.Format("Recipie {0} deleted!", id);
        }

        public string PostLeft( ICollection<RestLeftComponents> link)
        {

            foreach (RestLeftComponents item in link)
            {
                db.LeftComponentsLink.Add(new LeftComponentsLink() { ComponentId = item.ID_COMPONENT,
                RecipyId = item.ID_RECIPIE,
                UserId = item.ID_USER
                });
            }
            db.SaveChanges();
            return String.Format("Components added");
        }

        public string PostState(int id)
        {
            Recipy rECIPIES = db.Recipies.Find(id);
            if (rECIPIES == null)
            {
                return String.Format("Failed");
            }
            Recipy res = new Recipy() { Id = (int)id, State = 1 };
            res = db.Recipies.Find(id);
            res.State = 1;
            db.Entry(res).State = EntityState.Modified;
            db.SaveChanges();
            return String.Format("Changed to ready");
        }
        
    }
}
