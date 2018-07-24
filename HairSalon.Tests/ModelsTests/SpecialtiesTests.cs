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
      string specialties = "women";
      Client testClient = new Client(specialties, id);

      // act
      string result = testClient.GetSpecialties();

      // assert
      Assert.AreEqual(specialties, result);
    }
  }
}
