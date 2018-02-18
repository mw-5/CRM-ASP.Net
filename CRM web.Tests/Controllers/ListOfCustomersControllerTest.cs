using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRM_web.Controllers;
using System.Web.Mvc;

namespace CRM_web.Tests.Controllers
{
    [TestClass]
    public class ListOfCustomersControllerTest
    {
        [TestMethod]
        public void ListOfCustomers()
        {
            // Arrange
            ListOfCustomersController controller = new ListOfCustomersController();

            // Act
            ViewResult result = controller.ListOfCustomers() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
