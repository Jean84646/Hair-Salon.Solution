using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class StylistsControllerTest
    {
        [TestMethod]
        public void Index_ReturnsCorrectView_True()
        {
            //Arrange
            StylistsController controller = new StylistsController();

            //Act
            ActionResult indexView = controller.Index();

            //Assert
            Assert.IsInstanceOfType(indexView, typeof(ViewResult));
        }
        [TestMethod]
        public void Index_HasCorrectModelType_StylistList()
        {
            //Arrange
            ViewResult indexView = new StylistsController().Index() as ViewResult;

            //Act
            var result = indexView.ViewData.Model;

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<Stylist>));
        }
        [TestMethod]
        public void Detail_ReturnsCorrectView_True()
        {
            //Arrange
            StylistsController controller = new StylistsController();

            //Act
            ActionResult detailView = controller.Detail(1);

            //Assert
            Assert.IsInstanceOfType(detailView, typeof(ViewResult));
        }
        [TestMethod]
        public void Detail_HasCorrectModelType_Stylist()
        {
            //Arrange
            ViewResult detailView = new StylistsController().Detail(1) as ViewResult;

            //Act
            var result = detailView.ViewData.Model;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Stylist));
        }
        [TestMethod]
        public void EditForm_ReturnsCorrectView_True()
        {
            //Arrange
            StylistsController controller = new StylistsController();

            //Act
            ActionResult editView = controller.EditForm(1);

            //Assert
            Assert.IsInstanceOfType(editView, typeof(ViewResult));
        }
        [TestMethod]
        public void EditForm_HasCorrectModelType_Stylist()
        {
            //Arrange
            ViewResult editView = new StylistsController().EditForm(1) as ViewResult;

            //Act
            var result = editView.ViewData.Model;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Stylist));
        }
    }
}
