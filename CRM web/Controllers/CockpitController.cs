using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM_web.Models.Model;
using System.Data;
using CRM_web.Models;

namespace CRM_web.Controllers
{
    [Authorize]
    public class CockpitController : Controller
    {
        Model model = Model.GetModel();

        // GET: Cockpit
        public ActionResult Cockpit()
        {
            return View();
        }

        public ActionResult Search(int cid)
        {
            SearchByCid(cid);
            
            return View("Cockpit");
        }

        private void SearchByCid(int cid)
        {            
            try
            {
                DataRow masterData = (from md in model.Customers.Table.AsEnumerable()
                                      where md[model.TblCustomers.Cid.Name].ToString().Equals(cid.ToString())
                                      select md).First();

                ViewBag.Cid = cid;
                ViewBag.Address = masterData[model.TblCustomers.Address.Name].ToString();
                ViewBag.Zip = masterData[model.TblCustomers.Zip.Name].ToString();
                ViewBag.City = masterData[model.TblCustomers.City.Name].ToString();
                ViewBag.Country = masterData[model.TblCustomers.Country.Name].ToString();
                ViewBag.ContractId = masterData[model.TblCustomers.ContractId.Name].ToString();
                ViewBag.ContractDate = (DateTime?)masterData[model.TblCustomers.ContractDate.Name];
                ViewBag.Company = masterData[model.TblCustomers.Company.Name].ToString();
            }
            catch(Exception e)
            {
                Console.Write(e.StackTrace);
                ViewBag.Cid = "";
                ViewBag.Address = "";
                ViewBag.Zip = "";
                ViewBag.City = "";
                ViewBag.Country = "";
                ViewBag.ContractId = "";
                ViewBag.ContractDate = null;
                ViewBag.Company = "";
            }            
        }       
    }
}