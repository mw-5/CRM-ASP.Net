using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRM_web.Controllers;
using System.Web.Mvc;
using System.IO;
using System.Reflection;
using CRM_web.Tests.TestCases;
using System.Threading;

namespace CRM_web.Tests.Controllers
{
    [TestClass]
    public class CockpitControllerTest
    {
        [TestInitialize]
        public void Init()
        {
            Config.PathFileConfig = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Config.xml");
        }

        [TestMethod]
        public void Cockpit()
        {
            // Arrange
            CockpitController controller = new CockpitController();

            // Act
            ViewResult result = controller.Cockpit() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Search()
        {
            // Arrange
            TestCase.PopulateTestData();
            Thread.Sleep(1000);
            CockpitController controller = new CockpitController();

            // Act
            ViewResult result = controller.Search(TestCustomer.cid) as ViewResult;

            // Assert
            int cid = result.ViewBag.Cid;
            Assert.AreEqual(TestCustomer.cid, cid);
            Assert.AreEqual(TestCustomer.address, result.ViewBag.Address);
            Assert.AreEqual(TestCustomer.zip, result.ViewBag.Zip);
            Assert.AreEqual(TestCustomer.city, result.ViewBag.City);
            Assert.AreEqual(TestCustomer.country, result.ViewBag.Country);
            Assert.AreEqual(TestCustomer.contractId, result.ViewBag.ContractId);
            Assert.AreEqual(TestCustomer.contractDate, result.ViewBag.ContractDate);
            Assert.AreEqual(TestCustomer.company, result.ViewBag.Company);           

            TestCase.CleanUp();
        }
    }
}
