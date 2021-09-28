using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using CourseWeb.Models.Rest;
using CourseWeb.Models;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using CourseWeb.Models.DBModels;

namespace CourseWeb.Controllers
{
    public class UserRestController : ApiController
    {
        RECIPIESCONTEXT db = new RECIPIESCONTEXT();
        // GET: UserRest
        public ICollection<RestUser> Get()
        {
            var list = new Collection<RestUser>();
            foreach (User r in db.Users.ToList())
            {
                list.Add(new RestUser(r));
            }
            return list;
        }
        public RestUser Put(User model)
        {
            User AddUser = new User() {
                Username = model.Username,
                Password = model.Password,
                Type = 2,
            };
            AddUser = db.Users.Add(AddUser);
            db.SaveChangesAsync();
            return new RestUser(AddUser);
        }
    }
}