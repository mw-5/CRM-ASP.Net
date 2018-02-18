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
    public class FrmCustomerController : Controller
    {
        Model model = Model.GetModel();
        DefTblCustomers tblDef = Model.GetModel().TblCustomers;

        // GET: FrmCustomer
        public ActionResult New()
        {
            CustomerViewModel vm = new CustomerViewModel();
            vm.Cid = 0;
            return View("FrmCustomer", vm);
        }

        public ActionResult Edit(int cid)
        {
            CustomerViewModel vm = new CustomerViewModel();
            vm.Cid = cid;

            try
            {
                DataRow dr = (from d in model.GetCustomer(cid).Table.AsEnumerable()
                              select d).First();

                vm.Company = dr[tblDef.Company.Name].ToString();
                vm.Address = dr[tblDef.Address.Name].ToString();
                vm.Zip = dr[tblDef.Zip.Name].ToString();
                vm.City = dr[tblDef.City.Name].ToString();
                vm.Country = dr[tblDef.Country.Name].ToString();
                vm.ContractId = dr[tblDef.ContractId.Name].ToString();
                vm.ContractDate = dr[tblDef.ContractDate.Name] as DateTime?;
            }
            catch (Exception e)
            {
                ModelState.AddModelError("noData", e);
            }

            return View("FrmCustomer", vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit(CustomerViewModel vm)
        {
            // example how to receive data directly from request:
            //String company = Request["company"].ToString();

            if (!ModelState.IsValid)
            {               
                return View("FrmCustomer", vm);
            }

            EntryMode em = vm.Cid == 0 ? EntryMode.New : EntryMode.Edit;
            model.Submit(vm.getMap(), model.TblCustomers.TblName, new Tuple<ColDef, object>(model.TblCustomers.Cid, vm.Cid), em);
            
            return RedirectToAction("ListOfCustomers", "ListOfCustomers");
        }
    }
}