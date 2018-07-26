using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class ClientsControllerTest
    {
        [TestMethod]
        public void Index_ReturnsCorrectView_True()
        {
            //Arrange
            ClientsController controller = new ClientsController();

            //Act
            ActionResult indexView = controller.Index();

            //Assert
            Assert.IsInstanceOfType(indexView, typeof(ViewResult));
        }
        [TestMethod]
        public void Index_HasCorrectModelType_ClientList()
        {
            //Arrange
            ViewResult indexView = new ClientsController().Index() as ViewResult;

            //Act
            var result = indexView.ViewData.Model;

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<Client>));
        }
        [TestMethod]
        public void Detail_ReturnsCorrectView_True()
        {
            //Arrange
            ClientsController controller = new ClientsController();

            //Act
            ActionResult detailView = controller.Detail(1);

            //Assert
            Assert.IsInstanceOfType(detailView, typeof(ViewResult));
        }
        [TestMethod]
        public void Detail_HasCorrectModelType_Client()
        {
            //Arrange
            ViewResult detailView = new ClientsController().Detail(1) as ViewResult;

            //Act
            var result = detailView.ViewData.Model;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Client));
        }
        [TestMethod]
        public void EditForm_ReturnsCorrectView_True()
        {
            //Arrange
            ClientsController controller = new ClientsController();

            //Act
            ActionResult editView = controller.EditForm(1);

            //Assert
            Assert.IsInstanceOfType(editView, typeof(ViewResult));
        }
        [TestMethod]
        public void EditForm_HasCorrectModelType_Client()
        {
            //Arrange
            ViewResult editView = new ClientsController().EditForm(1) as ViewResult;

            //Act
            var result = editView.ViewData.Model;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Client));
        }
    }
}
