using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTests : IDisposable
  {
    public void Dispose()
    {
      Client.DeleteAll();
    }
    public ClientTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=jean_jia_test;";
    }
    [TestMethod]
    public void GetTest_ReturnDataField()
    {
      // arrange
      string client = "Jamie";
      string stylist = "Feng";
      Client testClient = new Client(client, stylist);

      // act
      string resultClient = testClient.GetClient();
      string resultStylist = testClient.GetStylist();

      // assert
      Assert.AreEqual(client, resultClient);
      Assert.AreEqual(stylist, resultStylist);
    }
  }
}
