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
        public void Index_HasCorrectModelType_SpecialtiesList()
        {
            //Arrange
            ViewResult indexView = new SpecialtiesController().Index() as ViewResult;

            //Act
            var result = indexView.ViewData.Model;

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<Specialties>));
        }
        [TestMethod]
        public void Detail_ReturnsCorrectView_True()
        {
            //Arrange
            SpecialtiesController controller = new SpecialtiesController();

            //Act
            ActionResult detailView = controller.Detail(1);

            //Assert
            Assert.IsInstanceOfType(detailView, typeof(ViewResult));
        }
        [TestMethod]
        public void Detail_HasCorrectModelType_Specialties()
        {
            //Arrange
            ViewResult detailView = new SpecialtiesController().Detail(1) as ViewResult;

            //Act
            var result = detailView.ViewData.Model;

            //Assert
            Assert.IsInstanceOfType(result, typeof(Specialties));
        }
        [TestMethod]
        public void CreatePairs_ReturnsCorrectView_True()
        {
            //Arrange
            SpecialtiesController controller = new SpecialtiesController();

            //Act
            ActionResult pairView = controller.CreatePairs();

            //Assert
            Assert.IsInstanceOfType(pairView, typeof(ViewResult));
        }
    }
}
