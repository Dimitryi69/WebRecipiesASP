using CourseWeb.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CourseWeb.Models.Rest
{
    [DataContract(Name = "Component")]
    public class RestComponent
    {
        public RestComponent(){}
        public RestComponent(Component comp)
        {
            ID = comp.Id;
            NAME = comp.Name;
            VALUE = comp.Value;
        }
        [DataMember(Name = "ID")]
        public int ID { get; set; }
        [DataMember(Name = "NAME")]
        public string NAME { get; set; }
        [DataMember(Name = "VALUE")]
        public string VALUE { get; set; }
    }
}