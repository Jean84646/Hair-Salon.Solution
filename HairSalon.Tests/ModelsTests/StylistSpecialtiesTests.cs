using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistSpecialtiesTests : IDisposable
  {
    public void Dispose()
    {
      StylistSpecialties.DeleteAll();
    }
    public StylistSpecialtiesTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=jean_jia_test;";
    }
    [TestMethod]
    public void GetTest_ReturnDataField()
    {
      // arrange
      int stylistId = 1;
      int specialtiesId = 1;
      int id = 1;
      StylistSpecialties testStylistSpecialties = new StylistSpecialties(stylistId, specialtiesId, id);

      // act
      int resultStylistId = testStylistSpecialties.GetStylistID();
      int resultSpecialtiesId = testStylistSpecialties.GetSpecialtiesID();
      int resultId = testStylistSpecialties.GetId();

      // assert
      Assert.AreEqual(stylistId, resultStylistId);
      Assert.AreEqual(specialtiesId, resultSpecialtiesId);
      Assert.AreEqual(id, resultId);
    }
    [TestMethod]
    public void Equals_ReturnsTrueIfIdAndNameAreTheSame_StylistSpecialty()
    {
      // Arrange, Act
      StylistSpecialties firstStylistSpecialty = new StylistSpecialties(1, 1, 1);
      StylistSpecialties secondStylistSpecialty = new StylistSpecialties(1, 1, 1);

      // Assert
      Assert.AreEqual(firstStylistSpecialty, secondStylistSpecialty);
    }
    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      StylistSpecialties testStylistSpecialty = new StylistSpecialties(1, 1);

      //Act
      testStylistSpecialty.Save();
      StylistSpecialties savedStylistSpecialty = StylistSpecialties.GetAll()[0];

      int result = savedStylistSpecialty.GetId();
      int testId = testStylistSpecialty.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void SaveAndGetAll_SavesToDatabaseAndReturnAll_StylistSpecialty()
    {
      //Arrange
      StylistSpecialties testStylistSpecialty = new StylistSpecialties(1, 1);

      //Act
      testStylistSpecialty.Save();
      List<StylistSpecialties> result = StylistSpecialties.GetAll();
      List<StylistSpecialties> testList = new List<StylistSpecialties>{testStylistSpecialty};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
  }
}
