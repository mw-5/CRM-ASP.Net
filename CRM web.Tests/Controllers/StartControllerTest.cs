using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRM_web.Controllers;
using System.Web.Mvc;

namespace CRM_web.Tests.Controllers
{
    [TestClass]
    public class StartControllerTest
    {
        [TestMethod]
        public void Start()
        {
            // Arrange
            StartController controller = new StartController();

            // Act
            ViewResult result = controller.Start() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }          
    }
}
