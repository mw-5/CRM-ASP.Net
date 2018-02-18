using CRM_web.Models.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM_web.Models
{
    public class ContactPersonViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Cid { get; set; }

        public String Forename { get; set; }

        public String Surname { get; set; }

        [RegularExpression("[m|f]")]
        public String Gender { get; set; }

        //private List<GenderItem> genders;
        public IEnumerable<SelectListItem> GenderItems
        {
            get
            {
                List<GenderItem>  genders = new List<GenderItem>();

                GenderItem m = new GenderItem();
                m.Id = "m";
                m.Text = Resources.Strings.Male;
                genders.Add(m);

                GenderItem f = new GenderItem();
                f.Id = "f";
                f.Text = Resources.Strings.Female;
                genders.Add(f);

                return new SelectList(genders, "Id", "Text");
            }
        }

        [EmailAddress(/*ErrorMessage = "Please enter valid email address."*/)]
        public String Email { get; set; }

        public String Phone { get; set; }
        
        public bool MainContact { get; set; }
      



        public Dictionary<ColDef, object> getMap()
        {
            DefTblContactPersons tblDef = Model.Model.GetModel().TblContactPersons;
            Dictionary<ColDef, object> map = new Dictionary<ColDef, object>();

            map.Add(tblDef.Id, Id);
            map.Add(tblDef.Cid, Cid);
            map.Add(tblDef.Forename, Forename);
            map.Add(tblDef.Surname, Surname);
            map.Add(tblDef.Gender, Gender);
            map.Add(tblDef.Email, Email);
            map.Add(tblDef.Phone, Phone);
            map.Add(tblDef.MainContact, MainContact);

            return map;
        }

    }

    public class GenderItem
    {
        public String Id { get; set; }
        public String Text { get; set; }
    }
}