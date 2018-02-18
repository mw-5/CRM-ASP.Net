using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using CRM_web.Controllers;
using System.Web.Mvc;
using CRM_web.Tests.TestCases;
using CRM_web.Models;
using System.Threading;
using CRM_web.Models.Model;

namespace CRM_web.Tests.Controllers
{
    [TestClass]
    public class FrmContactPersonControllerTest
    {
        [TestInitialize]
        public void Init()
        {
            Config.PathFileConfig = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Config.xml");
        }

        [TestMethod]
        public void NewContactPerson()
        {
            // Arrange
            FrmContactPersonController controller = new FrmContactPersonController();

            // Act
            ViewResult result = controller.New(TestCustomer.cid) as ViewResult;

            // Assert
            ContactPersonViewModel vm = result.Model as ContactPersonViewModel;
            Assert.AreEqual(0, vm.Id);
            Assert.AreEqual(TestCustomer.cid, vm.Cid);            
        }

        [TestMethod]
        public void EditContactPerson()
        {            
            // Arrange
            TestCase.PopulateTestData();
            FrmContactPersonController controller = new FrmContactPersonController();

            // Act
            ViewResult result = controller.Edit(TestContactPerson.id) as ViewResult;

            // Assert
            ContactPersonViewModel vm = result.Model as ContactPersonViewModel;
            Assert.AreEqual(TestContactPerson.id, vm.Id);
            Assert.AreEqual(TestContactPerson.cid, vm.Cid);
            Assert.AreEqual(TestContactPerson.forename, vm.Forename);
            Assert.AreEqual(TestContactPerson.surname, vm.Surname);
            Assert.AreEqual(TestContactPerson.gender, vm.Gender);
            Assert.AreEqual(TestContactPerson.email, vm.Email);
            Assert.AreEqual(TestContactPerson.phone, vm.Phone);
            Assert.AreEqual(TestContactPerson.mainContact, vm.MainContact);
            
            // clean up
            TestCase.CleanUp();
        }

        [TestMethod]
        public void SubmitContactPerson()
        {
            // ---- Arrange ----
            // set up model
            DefTblContactPersons def = new DefTblContactPersons();
            CRM_web.Models.Model.Model m = CRM_web.Models.Model.Model.GetModel();
            TestCase.CleanUp();
            m.ExecuteActionQuery(String.Format("DELETE FROM {0} WHERE {1} = {2};", def.TblName, def.Cid.Name, TestContactPerson.cid));
            TestCase.PopulateTestData();
            m.Cid = TestContactPerson.cid;
            m.LoadCustomers();
            m.LoadContactPersons(TestCustomer.cid);
            //Thread.Sleep(1000);
            // set up controller and view model
            FrmContactPersonController controller = new FrmContactPersonController();
            ContactPersonViewModel vm = new ContactPersonViewModel();
            vm.Id = TestContactPerson.id;
            vm.Cid = TestContactPerson.cid;
            vm.Forename = TestContactPerson.forename;
            vm.Surname = TestContactPerson.surname;
            vm.Gender = TestContactPerson.gender;
            vm.Email = TestContactPerson.email;
            vm.Phone = TestContactPerson.phone;
            vm.MainContact = TestContactPerson.mainContact;                        

            // ---- Act ----
            RedirectToRouteResult result = controller.Submit(vm) as RedirectToRouteResult;
            Thread.Sleep(1000);
            m.LoadContactPersons(TestCustomer.cid);
            Thread.Sleep(1000);

            // ---- Assert ----
            Assert.IsNotNull(result);
            // check new entries in database
            int lastEntry = 0;
            int max = 0;
            for (int i = 0; i < m.ContactPersons.Count; i++)
            {
                if (int.Parse(m.ContactPersons[i][def.Id.Name].ToString()) > max)
                {
                    max = int.Parse(m.ContactPersons[i][def.Id.Name].ToString());
                    lastEntry = i;
                }
            }
            Assert.AreEqual(m.ContactPersons[lastEntry][def.Cid.Name], TestContactPerson.cid);
            Assert.AreEqual(m.ContactPersons[lastEntry][def.Forename.Name], TestContactPerson.forename);
            Assert.AreEqual(m.ContactPersons[lastEntry][def.Surname.Name], TestContactPerson.surname);
            Assert.AreEqual(m.ContactPersons[lastEntry][def.Gender.Name], TestContactPerson.gender);
            Assert.AreEqual(m.ContactPersons[lastEntry][def.Email.Name], TestContactPerson.email);
            Assert.AreEqual(m.ContactPersons[lastEntry][def.Phone.Name], TestContactPerson.phone);
            Assert.AreEqual(m.ContactPersons[lastEntry][def.MainContact.Name], TestContactPerson.mainContact);

            // clean up model
            m.ExecuteActionQuery(String.Format("DELETE FROM {0} WHERE {1} = {2};", def.TblName, def.Cid.Name, TestContactPerson.cid));
            TestCase.CleanUp();

        }
    }
}
