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
      string Name = "Feng";
      string Description = "Best of Americana and Asian.";
      int Id = 1;
      Stylist testStylist = new Stylist(Name, Description, Id);

      // act
      string resultName = testStylist.GetName();
      string resultDescription = testStylist.GetDescription();
      int resultId = testStylist.GetId();

      // assert
      Assert.AreEqual(Name, resultName);
      Assert.AreEqual(Description, resultDescription);
      Assert.AreEqual(Id, resultId);
    }
  }
}
