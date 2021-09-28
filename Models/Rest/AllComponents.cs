using CourseWeb.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CourseWeb.Models.Rest
{
    [DataContract(Name = "AllComponents")]
    public class RestAllComponents
    {
        public RestAllComponents() { }
        public RestAllComponents(ComponentsLink clink)
        {
            ID = clink.Id;
            ID_COMPONENT = (int)clink.ComponentId;
            ID_RECIPIE = (int)clink.RecipyId;
            COMPONENT = new RestComponent(clink.Component);
        }
        [DataMember(Name = "ID")]
        public int ID { get; set; }
        [DataMember(Name = "ID_COMPONENT")]
        public int ID_COMPONENT { get; set; }
        [DataMember(Name = "ID_RECIPIE")]
        public int ID_RECIPIE { get; set; }

        [DataMember(Name = "COMPONENT")]
        public RestComponent COMPONENT { get; set; }
    }
}