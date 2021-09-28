using CourseWeb.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CourseWeb.Models.Rest
{
    [DataContract(Name = "User")]
    public class RestUser
    {
        public RestUser() { }
        public RestUser(User user)
        {
            ID = user.ID;
            USERNAME = user.Username;
            PASSWORD = user.Password;
            TYPE = user.Type;
        }
        [DataMember(Name = "ID")]
        public int ID { get; set; }
        [DataMember(Name = "USERNAME")]
        public string USERNAME { get; set; }
        [DataMember(Name = "PASSWORD")]
        public string PASSWORD { get; set; }
        [DataMember(Name = "TYPE")]
        public int TYPE { get; set; }
    }
}