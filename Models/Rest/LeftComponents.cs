using CourseWeb.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CourseWeb.Models.Rest
{
    [DataContract(Name = "LeftComponents")]
    public class RestLeftComponents
    {
        public RestLeftComponents() { }
        public RestLeftComponents(LeftComponentsLink left)
        {
            ID = left.Id;
            ID_RECIPIE = left.RecipyId;
            ID_COMPONENT = left.ComponentId;
            ID_USER = (int)left.UserId;
            COMPONENT = new RestComponent(left.Component);
        }
        [DataMember(Name = "ID")]
        public int ID { get; set; }
        [DataMember(Name = "ID_COMPONENT")]
        public int ID_COMPONENT { get; set; }
        [DataMember(Name = "ID_RECIPIE")]
        public int ID_RECIPIE { get; set; }
        [DataMember(Name = "ID_USER")]
        public int ID_USER { get; set; }
        [DataMember(Name = "COMPONENT")]
        public RestComponent COMPONENT { get; set; }
    }
}