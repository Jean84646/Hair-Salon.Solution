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
      // Arrange
      string client = "testName";
      int stylistId = 1;
      int id = 1;
      Client testClient = new Client(client, stylistId, id);

      // Act
      string resultClient = testClient.GetName();
      int resultStylistId = testClient.GetStylistID();
      int resultId = testClient.GetId();

      // Assert
      Assert.AreEqual(client, resultClient);
      Assert.AreEqual(stylistId, resultStylistId);
      Assert.AreEqual(id, resultId);
    }
    [TestMethod]
    public void Equals_ReturnsTrueIfIdAndNameAreTheSame_Client()
    {
      // Arrange, Act
      Client firstClient = new Client("testName", 1, 1);
      Client secondClient = new Client("testName", 1, 1);

      // Assert
      Assert.AreEqual(firstClient, secondClient);
    }
    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Client testClient = new Client("testName", 1);

      //Act
      testClient.Save();
      Client savedClient = Client.GetAll()[0];

      int result = savedClient.GetId();
      int testId = testClient.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }
    [TestMethod]
    public void SaveAndGetAll_SavesToDatabaseAndReturnAll_Client()
    {
      //Arrange
      Client testClient = new Client("testName", 1);

      //Act
      testClient.Save();
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    
  }
}
