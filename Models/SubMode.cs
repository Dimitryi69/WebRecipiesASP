using CourseWeb.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseWeb.Models
{
    public class SubMode
    {
        public List<Component> Components { get; set; }
        public User User { get; set; }
        public Recipy Recipy { get; set; }

        public List<CompModel> Comp { get; set; }
        public SubMode() { Comp = new List<CompModel>(); }
    }
    public class CompModel {
        public CompModel()
        {
            this.ComponentsLink = new HashSet<ComponentsLink>();
            this.LeftComponentsLink = new HashSet<LeftComponentsLink>();
        }

        public int ID { get; set; }
        public string NAME { get; set; }
        public string VALUE { get; set; }

        public bool isChecked { get; set; }

        public virtual ICollection<ComponentsLink> ComponentsLink { get; set; }
        public virtual ICollection<LeftComponentsLink> LeftComponentsLink { get; set; }
    }
}