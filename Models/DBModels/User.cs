using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseWeb.Models.DBModels
{
    public partial class User
    {
        public User()
        {
            this.LeftComponents = new HashSet<LeftComponentsLink>();
        }
        [Key]
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Type { get; set; }
        public virtual ICollection<LeftComponentsLink> LeftComponents { get; set; }
    }
}