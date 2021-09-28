using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseWeb.Models.DBModels
{
    public partial class Component
    {
        public Component()
        {
            this.ComponentsLink = new HashSet<ComponentsLink>();
            this.LeftComponentsLink = new HashSet<LeftComponentsLink>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public virtual ICollection<ComponentsLink> ComponentsLink { get; set; }
        public virtual ICollection<LeftComponentsLink> LeftComponentsLink { get; set; }
    }
}