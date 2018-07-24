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
      int stylistID = 1;
      int specialtiesID = 1;
      int Id = 1;
      StylistSpecialties testStylistSpecialties = new StylistSpecialties(stylistID, specialtiesID, Id);

      // act
      int resultStylistID = testStylistSpecialties.GetStylistID();
      int resultSpecialtiesID = testStylistSpecialties.GetSpecialtiesID();
      int resultId = testStylistSpecialties.GetId();

      // assert
      Assert.AreEqual(stylistID, resultStylistID);
      Assert.AreEqual(specialtiesID, resultSpecialtiesID);
      Assert.AreEqual(Id, resultId);
    }
  }
}
