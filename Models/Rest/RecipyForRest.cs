using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using CourseWeb.Models.DBModels;
using CourseWeb.Models.Rest;

namespace CourseWeb.Models
{
    [DataContract(Name = "Recipie")]
    public class RecipyForRest
    {
        public RecipyForRest() { }
        public RecipyForRest(Recipy res)
        {
            this.ID = res.Id;
            this.NAME = res.Name;
            USER_CREATOR = res.UserCreator;
            this.STATE = res.State;
            if(res.Image != null)
            IMAGE = res.Image;
            ADRESS = res.Adress;
            TIME = res.Time;
            AllComp = new List<RestAllComponents>();
            LeftComp = new List<RestLeftComponents>();
            foreach (ComponentsLink link in res.ComponentsLink)
            {
                AllComp.Add(new RestAllComponents(link));
            }foreach(LeftComponentsLink link in res.LeftComponentsLink)
            {
                LeftComp.Add(new RestLeftComponents(link));
            }
        }

        [DataMember(Name = "ID")]
        public int ID { get; set; }
        [DataMember(Name = "NAME")]
        public string NAME { get; set; }
        [DataMember(Name = "USER_CREATOR")]
        public int USER_CREATOR { get; set; }
        [DataMember(Name = "STATE")]
        public int STATE { get; set; }
        [DataMember(Name = "IMAGE")]
        public String IMAGE { get; set; }
        [DataMember(Name = "ADRESS")]
        public string ADRESS { get; set; }
        [DataMember(Name = "TIME")]
        public string TIME { get; set; }

        [DataMember(Name = "AllComponentsList")]
        public ICollection<RestAllComponents> AllComp { get; set; }
        [DataMember(Name = "LeftComponentsList")]
        public ICollection<RestLeftComponents> LeftComp { get; set; }

        
    }
}