using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using CRM_web.Controllers;
using CRM_web.Models;
using System.Collections.Generic;
using System.Web.Http.Results;
using CRM_web.Tests.TestCases;

namespace CRM_web.Tests.Controllers
{
    [TestClass]
    public class WebServiceControllerTest
    {
        [TestInitialize]
        public void Init()
        {
            Config.PathFileConfig = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Config.xml");
        }

        [TestMethod]
        public void GetNotes()
        {
            // Arrange
            TestCase.PopulateTestData();
            WebServiceController controller = new WebServiceController();

            // Act
            JsonResult<List<NoteViewModel>> result = controller.Notes(TestCustomer.cid) as JsonResult<List<NoteViewModel>>;

            // Assert
            NoteViewModel note = result.Content.Find(n => n.Id == TestNote.id);
            Assert.AreEqual(TestNote.id, note.Id);
            Assert.AreEqual(TestNote.cid, note.Cid);
            Assert.AreEqual(TestNote.createdBy, note.CreatedBy);
            Assert.AreEqual(TestNote.entryDate, note.EntryDate);
            Assert.AreEqual(TestNote.memo, note.Memo);
            Assert.AreEqual(TestNote.category, note.Category);
            Assert.AreEqual(TestNote.attachment, note.Attachment);

            TestCase.CleanUp();
        }

        [TestMethod]
        public void GetContactPersons()
        {
            // Arrange
            TestCase.PopulateTestData();
            WebServiceController controller = new WebServiceController();

            // Act
            JsonResult<List<ContactPersonViewModel>> result = controller.ContactPersons(TestCustomer.cid) as JsonResult<List<ContactPersonViewModel>>;

            // Assert
            ContactPersonViewModel cp = result.Content.Find(c => c.Id == TestContactPerson.id);
            Assert.AreEqual(TestContactPerson.id, cp.Id);
            Assert.AreEqual(TestContactPerson.cid, cp.Cid);
            Assert.AreEqual(TestContactPerson.forename, cp.Forename);
            Assert.AreEqual(TestContactPerson.surname, cp.Surname);
            Assert.AreEqual(TestContactPerson.gender, cp.Gender);
            Assert.AreEqual(TestContactPerson.email, cp.Email);
            Assert.AreEqual(TestContactPerson.phone, cp.Phone);
            Assert.AreEqual(TestContactPerson.mainContact, cp.MainContact);

            TestCase.CleanUp();
        }

        [TestMethod]
        public void GetCustomers()
        {
            // Arrange
            TestCase.PopulateTestData();
            WebServiceController controller = new WebServiceController();

            // Act
            JsonResult<List<CustomerViewModel>> result = controller.Customers() as JsonResult<List<CustomerViewModel>>;

            // Assert
            CustomerViewModel c = result.Content.Find(e => e.Cid == TestCustomer.cid);
            Assert.AreEqual(TestCustomer.cid, c.Cid);
            Assert.AreEqual(TestCustomer.company, c.Company);
            Assert.AreEqual(TestCustomer.address, c.Address);
            Assert.AreEqual(TestCustomer.zip, c.Zip);
            Assert.AreEqual(TestCustomer.city, c.City);
            Assert.AreEqual(TestCustomer.country, c.Country);
            Assert.AreEqual(TestCustomer.contractId, c.ContractId);
            Assert.AreEqual(TestCustomer.contractDate, c.ContractDate);

            TestCase.CleanUp();
        }
    }
}
