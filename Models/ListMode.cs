using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseWeb.Models.DBModels;

namespace CourseWeb.Models
{
    public class ListMode
    {
        public List<Component> Components { get; set; }
        public User User { get; set; }
        public Recipy Recipy { get; set; }

        public HttpPostedFileBase Img { get; set; }
    }
}