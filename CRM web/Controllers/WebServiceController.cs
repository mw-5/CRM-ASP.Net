using CRM_web.Models;
using CRM_web.Models.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace CRM_web.Controllers
{
    [Authorize]
    public class WebServiceController : ApiController
    {
        // this class is used instead of mvc controller because web api controller uses JsonNet serializer instad of Json serializer
        // which parses date to a readable string

        Model model = Model.GetModel();

        [HttpPost]
        public JsonResult<List<NoteViewModel>> Notes(int cid)
        {
            DefTblNotes tblDef = model.TblNotes;
            List<NoteViewModel> notes = new List<NoteViewModel>();
            NoteViewModel note;

            model.LoadNotes(cid);

            foreach (DataRow dr in model.Notes.Table.AsEnumerable())
            {
                note = new NoteViewModel();

                note.Id = int.Parse(dr[tblDef.Id.Name].ToString());
                note.Cid = int.Parse(dr[tblDef.Cid.Name].ToString());
                note.CreatedBy = dr[tblDef.CreatedBy.Name].ToString();
                note.EntryDate = dr[tblDef.EntryDate.Name] as DateTime?;
                note.Memo = dr[tblDef.Memo.Name].ToString();
                note.Category = dr[tblDef.Category.Name].ToString();
                note.Attachment = dr[tblDef.Attachment.Name].ToString();

                notes.Add(note);
            }

            return Json(notes);
        }

        [HttpPost]
        public JsonResult<List<ContactPersonViewModel>> ContactPersons(int cid)
        {
            DefTblContactPersons tblDef = model.TblContactPersons;
            List<ContactPersonViewModel> contactPersons = new List<ContactPersonViewModel>();
            ContactPersonViewModel cp;

            model.LoadContactPersons(cid);

            foreach (DataRow dr in model.ContactPersons.Table.AsEnumerable())
            {
                cp = new ContactPersonViewModel();

                cp.Id = int.Parse(dr[tblDef.Id.Name].ToString());
                cp.Cid = int.Parse(dr[tblDef.Cid.Name].ToString());
                cp.Forename = dr[tblDef.Forename.Name].ToString();
                cp.Surname = dr[tblDef.Surname.Name].ToString();
                cp.Gender = dr[tblDef.Gender.Name].ToString();
                cp.Email = dr[tblDef.Email.Name].ToString();
                cp.Phone = dr[tblDef.Phone.Name].ToString();
                cp.MainContact = (bool)dr[tblDef.MainContact.Name];

                contactPersons.Add(cp);
            }

            return Json(contactPersons);
        }

        [HttpPost]
        public JsonResult<List<CustomerViewModel>> Customers()
        {
            DefTblCustomers tblDef = model.TblCustomers;
            List<CustomerViewModel> customers = new List<CustomerViewModel>();
            CustomerViewModel c;

            model.LoadCustomers();

            foreach (DataRow dr in model.Customers.Table.AsEnumerable())
            {
                c = new CustomerViewModel();

                c.Cid = int.Parse(dr[tblDef.Cid.Name].ToString());
                c.Company = dr[tblDef.Company.Name].ToString();
                c.Address = dr[tblDef.Address.Name].ToString();
                c.Zip = dr[tblDef.Zip.Name].ToString();
                c.City = dr[tblDef.City.Name].ToString();
                c.Country = dr[tblDef.Country.Name].ToString();
                c.ContractId = dr[tblDef.ContractId.Name].ToString();
                c.ContractDate = dr[tblDef.ContractDate.Name] as DateTime?;

                customers.Add(c);
            }

            return Json(customers);
        }
    }
}
