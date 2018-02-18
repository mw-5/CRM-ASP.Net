using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using CRM_web.Controllers;
using System.Web.Mvc;
using CRM_web.Models;
using CRM_web.Tests.TestCases;
using CRM_web.Models.Model;
using System.Threading;
using System.Data;
using System.Linq;

namespace CRM_web.Tests.Controllers
{
    [TestClass]
    public class FrmCustomerControllerTest
    {
        [TestInitialize]
        public void Init()
        {
            Config.PathFileConfig = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Config.xml");
        }

        [TestMethod]
        public void NewCustomer()
        {
            // Arrange
            FrmCustomerController controller = new FrmCustomerController();

            // Act
            ViewResult result = controller.New() as ViewResult;

            // Assert
            CustomerViewModel vm = result.Model as CustomerViewModel;
            Assert.AreEqual(0, vm.Cid);
        }

        [TestMethod]
        public void EditCustomer()
        {
            // Arrange
            TestCase.PopulateTestData();
            FrmCustomerController controller = new FrmCustomerController();

            // Act
            ViewResult result = controller.Edit(TestCustomer.cid) as ViewResult;

            // Assert
            CustomerViewModel vm = result.Model as CustomerViewModel;
            Assert.AreEqual(TestCustomer.cid, vm.Cid);
            Assert.AreEqual(TestCustomer.company, vm.Company);
            Assert.AreEqual(TestCustomer.address, vm.Address);
            Assert.AreEqual(TestCustomer.zip, vm.Zip);
            Assert.AreEqual(TestCustomer.city, vm.City);
            Assert.AreEqual(TestCustomer.country, vm.Country);
            Assert.AreEqual(TestCustomer.contractId, vm.ContractId);
            Assert.AreEqual(TestCustomer.contractDate, vm.ContractDate);

            TestCase.CleanUp();
        }

        [TestMethod]
        public void SubmitCustomer()
        {
            // ---- Arrange ----
            // set up model state
            CRM_web.Models.Model.Model m = CRM_web.Models.Model.Model.GetModel();
            DefTblCustomers def = new DefTblCustomers();            
            TestCase.PopulateTestData();
            // set up controller and view model
            FrmCustomerController controller = new FrmCustomerController();
            CustomerViewModel vm = new CustomerViewModel();
            vm.Cid = TestCustomer.cid;
            vm.Company = TestCustomer.company;
            vm.Address = TestCustomer.address;
            vm.Zip = TestCustomer.zip;
            vm.City = TestCustomer.city;
            vm.Country = TestCustomer.country;
            vm.ContractId = TestCustomer.contractId;
            vm.ContractDate = TestCustomer.contractDate;

            // ---- Act ----
            RedirectToRouteResult result = controller.Submit(vm) as RedirectToRouteResult;
            Thread.Sleep(1000);

            // ---- Assert ----
            DataRow dr = (from d in m.GetCustomer(TestCustomer.cid).Table.AsEnumerable()
                          select d).First();
            // check new entries
            Assert.AreEqual(TestCustomer.cid, dr[def.Cid.Name]);
            Assert.AreEqual(TestCustomer.company, dr[def.Company.Name]);
            Assert.AreEqual(TestCustomer.address, dr[def.Address.Name]);
            Assert.AreEqual(TestCustomer.zip, dr[def.Zip.Name]);
            Assert.AreEqual(TestCustomer.city, dr[def.City.Name]);
            Assert.AreEqual(TestCustomer.country, dr[def.Country.Name]);
            Assert.AreEqual(TestCustomer.contractId, dr[def.ContractId.Name]);
            Assert.AreEqual(TestCustomer.contractDate, dr[def.ContractDate.Name]);


            TestCase.CleanUp();
        }

    }
}
