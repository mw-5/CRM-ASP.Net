using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using CRM_web.Models.Model;
using System.IO;
using System.Reflection;

namespace CRM_web.Tests.Model
{
    [TestClass]
    public class ModelTests
    {
        [TestInitialize]
        public void Init()
        {
            Config.PathFileConfig = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Config.xml");
        }

        [TestMethod()]
        public void LoadCustomersTest()
        {
            CRM_web.Models.Model.Model m = CRM_web.Models.Model.Model.GetModel();
            DataView dv = null;
            dv = m.Customers;
            Assert.IsTrue(dv != null);
        }

        [TestMethod()]
        public void LoadContactPersonsTest()
        {
            CRM_web.Models.Model.Model m = CRM_web.Models.Model.Model.GetModel();
            DataView dv = null;
            dv = m.ContactPersons;
            Assert.IsTrue(dv != null);
        }

        [TestMethod()]
        public void LoadNotesTest()
        {
            CRM_web.Models.Model.Model m = CRM_web.Models.Model.Model.GetModel();
            DataView dv = null;
            dv = m.Notes;
            Assert.IsTrue(dv != null);
        }
    }
}
