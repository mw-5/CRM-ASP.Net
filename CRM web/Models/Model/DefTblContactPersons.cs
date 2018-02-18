using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM_web.Models.Model
{
    public class DefTblContactPersons
    {
        public DefTblContactPersons()
        {
            TblName = "contact_persons";
            Id = new ColDef() { Name = "id", Type = ColTypes.Numeric };
            Cid = new ColDef() { Name = "cid", Type = ColTypes.Numeric };
            Forename = new ColDef() { Name = "forename", Type = ColTypes.Text };
            Surname = new ColDef() { Name = "surname", Type = ColTypes.Text };
            Gender = new ColDef() { Name = "gender", Type = ColTypes.Text };
            Email = new ColDef() { Name = "email", Type = ColTypes.Text };
            Phone = new ColDef() { Name = "phone", Type = ColTypes.Text };
            MainContact = new ColDef() { Name = "main_contact", Type = ColTypes.Boolean };
        }

        public String TblName { get; private set; }
        public ColDef Id { get; private set; }
        public ColDef Cid { get; private set; }
        public ColDef Forename { get; private set; }
        public ColDef Surname { get; private set; }
        public ColDef Gender { get; private set; }
        public ColDef Email { get; private set; }
        public ColDef Phone { get; private set; }
        public ColDef MainContact { get; private set; }
    }
}