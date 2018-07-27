using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTests : IDisposable
  {
    public void Dispose()
    {
      Stylist.DeleteAll();
    }
    public StylistTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=jean_jia_test;";
    }
    [TestMethod]
    public void GetTest_ReturnDataField()
    {
      // arrange
      string name = "Feng";
      string description = "Studio in bellevue";
      int id = 1;
      Stylist testStylist = new Stylist(name, description, id);

      // act
      string resultName = testStylist.GetName();
      string resultDescription = testStylist.GetDescription();
      int resultId = testStylist.GetId();

      // assert
      Assert.AreEqual(name, resultName);
      Assert.AreEqual(description, resultDescription);
      Assert.AreEqual(id, resultId);
    }
    [TestMethod]
    public void Equals_ReturnsTrueIfIdAndNameAreTheSame_Stylist()
    {
      // Arrange, Act
      Stylist firstStylist = new Stylist("testName", "testDescription", 1);
      Stylist secondStylist = new Stylist("testName", "testDescription", 1);

      // Assert
      Assert.AreEqual(firstStylist, secondStylist);
    }
    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Stylist testStylist = new Stylist("testName", "testDescription");

      //Act
      testStylist.Save();
      Stylist savedStylist = Stylist.GetAll()[0];

      int result = savedStylist.GetId();
      int testId = testStylist.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void SaveAndGetAll_SavesToDatabaseAndReturnAll_Stylist()
    {
      //Arrange
      Stylist testStylist = new Stylist("testName", "testDescription");

      //Act
      testStylist.Save();
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Find_FindStylistInDatabase_Stylist()
    {
      //Arrange
      Stylist testStylist = new Stylist("testName", "testDescription");
      testStylist.Save();
      List<Stylist> testList = new List<Stylist> {testStylist};

      //Act
      Stylist resultById = Stylist.FindById(testStylist.GetId());
      List<Stylist> resultByName = Stylist.FindByName(testStylist.GetName());

      //Assert
      Assert.AreEqual(testStylist, resultById);
      CollectionAssert.AreEqual(testList, resultByName);
    }
    
    [TestMethod]
    public void GetSpecialties_RetrievesAllSpecialtiesWithStylistId_SpecialtiesList()
    {
      // Arrange
      Stylist testStylist = new Stylist("testStylist");
      testStylist.Save();
      Specialties testSpecialty = new Specialties("testSpecialty");
      testSpecialty.Save();
      StylistSpecialties testStylistSpecialty = new StylistSpecialties(testStylist.GetId(), testSpecialty.GetId());
      testStylistSpecialty.Save();
      List<Specialties> testSpecialties = new List<Specialties> {testSpecialty};

      // Act
      List<Specialties> resultSpecialties = testStylist.GetSpecialties();

      // Assert
      CollectionAssert.AreEqual(testSpecialties, resultSpecialties);
    }
    [TestMethod]
    public void EditName_EditStylistNameInDatabase_Stylist()
    {
      // Arrange
      Stylist testStylist = new Stylist("testName1", "testDescription");
      testStylist.Save();
      string testName = "testname2";

      // Act
      testStylist.EditName(testName);

      // Assert
      Assert.AreEqual(testName, Stylist.FindById(testStylist.GetId()).GetName());
    }
    [TestMethod]
    public void Delete_DeleteStylistFromDatabaseAndStylist_SpecialtyTable_Stylist()
    {
      // Arrange
      Stylist testStylist = new Stylist("testName", "testDescription", 1);
      testStylist.Save();
      Specialties testSpecialty = new Specialties("testSpecialty");
      testSpecialty.Save();
      StylistSpecialties testStylistSpecialty = new StylistSpecialties(testStylist.GetId(), testSpecialty.GetId());
      testStylistSpecialty.Save();

      // Act
      testStylist.Delete();

      // Assert
      Assert.AreEqual(0, Stylist.FindById(testStylist.GetId()).GetId());
      Assert.AreEqual(0, testSpecialty.GetStylists().Count);
    }
  }
}
