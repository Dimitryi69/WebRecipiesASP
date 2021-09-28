using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseWeb.Models.DBModels
{
    public partial class Recipy
    {
        public Recipy()
        {
            this.ComponentsLink = new HashSet<ComponentsLink>();
            this.LeftComponentsLink = new HashSet<LeftComponentsLink>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserCreator { get; set; }
        public int State { get; set; }
        public byte[] Image { get; set; }
        public string Adress { get; set; }
        public string Time { get; set; }
        public virtual ICollection<ComponentsLink> ComponentsLink { get; set; }
        public virtual ICollection<LeftComponentsLink> LeftComponentsLink { get; set; }
    }
}