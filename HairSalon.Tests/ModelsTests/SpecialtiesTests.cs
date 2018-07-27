using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class SpecialtiesTests : IDisposable
  {
    public void Dispose()
    {
      Specialties.DeleteAll();
    }
    public SpecialtiesTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=jean_jia_test;";
    }
    [TestMethod]
    public void GetTest_ReturnDataField()
    {
      // arrange
      int id = 1;
      string specialties = "perm";
      Specialties testSpecialties = new Specialties(specialties, id);

      // act
      string result = testSpecialties.GetSpecialties();

      // assert
      Assert.AreEqual(specialties, result);
    }
    [TestMethod]
    public void Equals_ReturnsTrueIfIdAndNameAreTheSame_Specialties()
    {
      // Arrange, Act
      Specialties firstSpecialty = new Specialties("testName", 1);
      Specialties secondSpecialty = new Specialties("testName", 1);

      // Assert
      Assert.AreEqual(firstSpecialty, secondSpecialty);
    }
    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Specialties testSpecialty = new Specialties("testName");

      //Act
      testSpecialty.Save();
      Specialties savedSpecialty = Specialties.GetAll()[0];

      int result = savedSpecialty.GetId();
      int testId = testSpecialty.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void SaveAndGetAll_SavesToDatabaseAndReturnAll_Specialties()
    {
      //Arrange
      Specialties testSpecialty = new Specialties("testName");

      //Act
      testSpecialty.Save();
      List<Specialties> result = Specialties.GetAll();
      List<Specialties> testList = new List<Specialties>{testSpecialty};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Find_FindSpecialtyInDatabase_Specialties()
    {
      //Arrange
      Specialties testSpecialty = new Specialties("testName");
      testSpecialty.Save();
      List<Specialties> testList = new List<Specialties> {testSpecialty};

      //Act
      Specialties resultById = Specialties.FindById(testSpecialty.GetId());
      List<Specialties> resultByName = Specialties.FindByName(testSpecialty.GetSpecialties());

      //Assert
      Assert.AreEqual(testSpecialty, resultById);
      CollectionAssert.AreEqual(testList, resultByName);
    }
    [TestMethod]
    public void GetStylists_RetrievesAllStylistsWithSpecialtyId_StylistList()
    {
      // Arrange
      Specialties testSpecialty = new Specialties("testSpecialty");
      testSpecialty.Save();
      Stylist testStylist = new Stylist("testStylist");
      testStylist.Save();
      StylistSpecialties testStylistSpecialty = new StylistSpecialties(testStylist.GetId(), testSpecialty.GetId());
      testStylistSpecialty.Save();
      List<Stylist> testStylists = new List<Stylist> {testStylist};

      // Act
      List<Stylist> resultStylists = testSpecialty.GetStylists();

      // Assert
      CollectionAssert.AreEqual(testStylists, resultStylists);
    }
  }
}
