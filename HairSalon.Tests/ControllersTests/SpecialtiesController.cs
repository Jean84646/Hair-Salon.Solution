using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class SpecialtiesControllerTest
    {
      [TestMethod]
        public void Index_ReturnsCorrectView_True()
        {
            //Arrange
            SpecialtiesController controller = new SpecialtiesController();

            //Act
            ActionResult indexView = controller.Index();

            //Assert
            Assert.IsInstanceOfType(indexView, typeof(ViewResult));
        }
        [TestMethod]
        public void Index_HasCorrectModelType_ItemList()
        {
            //Arrange
            ViewResult indexView = new SpecialtiesController().Index() as ViewResult;

            //Act
            var result = indexView.ViewData.Model;

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<Stylist>));
        }
      [TestMethod]
        public void CreateForm_ReturnsCorrectView_True()
        {
            //Arrange
            SpecialtiesController controller = new SpecialtiesController();

            //Act
            ActionResult indexView = controller.CreateForm();

            //Assert
            Assert.IsInstanceOfType(indexView, typeof(ViewResult));
        }
        [TestMethod]
          public void CreateSearch_ReturnsCorrectView_True()
          {
              //Arrange
              SpecialtiesController controller = new SpecialtiesController();

              //Act
              ActionResult indexView = controller.SearchForm();

              //Assert
              Assert.IsInstanceOfType(indexView, typeof(ViewResult));
          }
    }
}
