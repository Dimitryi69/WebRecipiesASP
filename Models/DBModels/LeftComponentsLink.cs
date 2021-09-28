﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseWeb.Models.DBModels
{
    public partial class LeftComponentsLink
    {
        [Key]
        public int Id { get; set; }
        public int ComponentId { get; set; }
        public int RecipyId { get; set; }
        public int? UserId { get; set; }

        public virtual Component Component { get; set; }
        public virtual Recipy Recipy { get; set; }
        public virtual User User { get; set; }
    }
}