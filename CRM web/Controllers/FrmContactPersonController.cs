using CRM_web.Models;
using CRM_web.Models.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM_web.Controllers
{
    [Authorize]
    public class FrmContactPersonController : Controller
    {
        Model model = Model.GetModel();
        DefTblContactPersons tblDef = Model.GetModel().TblContactPersons;

        // GET: FrmContactPerson
        public ActionResult New(int cid)
        {
            ContactPersonViewModel vm = new ContactPersonViewModel();
            vm.Id = 0;
            vm.Cid = cid;
            return View("FrmContactPerson", vm);
        }

        public ActionResult Edit(int id)
        {
            ContactPersonViewModel vm = new ContactPersonViewModel();
            vm.Id = id;

            try
            {
                DataRow dr = (from d in model.GetContactPerson(id).Table.AsEnumerable()
                              select d).First();

                vm.Cid = int.Parse(dr[tblDef.Cid.Name].ToString());
                vm.Forename = dr[tblDef.Forename.Name].ToString();
                vm.Surname = dr[tblDef.Surname.Name].ToString();
                vm.Gender = dr[tblDef.Gender.Name].ToString();
                vm.Email = dr[tblDef.Email.Name].ToString();
                vm.Phone = dr[tblDef.Phone.Name].ToString();
                vm.MainContact = (bool)dr[tblDef.MainContact.Name];
            }
            catch (Exception e)
            {
                ModelState.AddModelError("noData", e);
            }

            return View("FrmContactPerson", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit(ContactPersonViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("FrmContactPerson", vm);
            }

            EntryMode em = vm.Id == 0 ? EntryMode.New : EntryMode.Edit;            
            model.Submit(vm.getMap(), model.TblContactPersons.TblName, new Tuple<ColDef, object>(model.TblContactPersons.Id, vm.Id), em);

            return RedirectToAction("Search", "Cockpit", new { cid = vm.Cid.ToString() });
        }
    }
}