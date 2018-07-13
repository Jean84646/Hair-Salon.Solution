using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
  public class Stylists
  {
    private string Name;
    private string Description;
    private int Id;

    public Stylists(string newName, string newDescription = "", int newId = 0)
    {
      Name = newName;
      Description = newDescription;
      Id = newId;
    }
    public string GetName()
    {
      return Name;
    }
    public string GetDescription()
    {
      return Description;
    }
    public int GetId()
    {
      return Id;
    }
    public override bool Equals(System.Object otherRestaurant)
    {
      if (!(otherStylist is Stylists))
      {
        return false;
      }
      else
      {
        Stylists newStylist = (Stylists) otherStylist;
        bool nameEqual = (this.GetName() == newRestaurant.GetName());
        bool descriptionEqual = (this.GetDescription() == newRestaurant.GetDescription());
        bool idEqual = (this.GetId() == newRestaurant.GetId());
        return (nameEqual && descriptionEqual && idEqual);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO fav_restaurant (name, cuisine_id, location, description) VALUES (@inputName, @inputCuisine, @inputLocation, @inputDescription);";
      MySqlParameter newName = new MySqlParameter();
      newName.ParameterName = "@inputName";
      newName.Value = this.Name;
      cmd.Parameters.Add(newName);
      MySqlParameter newDescription = new MySqlParameter();
      newDescription.ParameterName = "@inputDescription";
      newDescription.Value = this.Description;
      cmd.Parameters.Add(newDescription);
      MySqlParameter newLocation = new MySqlParameter();
      newLocation.ParameterName = "@inputLocation";
      newLocation.Value = this.Location;
      cmd.Parameters.Add(newLocation);
      MySqlParameter newCuisine = new MySqlParameter();
      newCuisine.ParameterName = "@inputCuisine";
      newCuisine.Value = this.Cuisine;
      cmd.Parameters.Add(newCuisine);
      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
    }

    public static List<Stylists> GetAll()
    {
      List<Stylists> allRestaurants = new List<Stylists> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM fav_restaurant;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string description = rdr.GetString(2);
        string location = rdr.GetString(3);
        string cuisine = rdr.GetString(4);
        Stylists newRestaurant = new Stylists(name, cuisine, location, description, id);
        allRestaurants.Add(newRestaurant);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allRestaurants;
    }

    public static Stylists FindById(int byId)
    {
      int id = 0;
      string name = "";
      string description = "";
      string location = "";
      string cuisine = "";
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM fav_restaurant WHERE id = @idPara;";
      MySqlParameter paraId = new MySqlParameter();
      paraId.ParameterName = "@idPara";
      paraId.Value = byId;
      cmd.Parameters.Add(paraId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        name = rdr.GetString(1);
        description = rdr.GetString(2);
        location = rdr.GetString(3);
        cuisine = rdr.GetString(4);
      }
      Stylists newRestaurant = new Stylists(name, cuisine, location, description, id);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newRestaurant;
    }

    public static List<Stylists> FindByCuisine(string myCuisine)
    {
      List<Stylists> foundRestaurants = new List<Stylists> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM fav_restaurant WHERE cuisine_id = @Cuisine;";
      MySqlParameter searchCuisine = new MySqlParameter();
      searchCuisine.ParameterName = "@Cuisine";
      searchCuisine.Value = myCuisine;
      cmd.Parameters.Add(searchCuisine);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string description = rdr.GetString(2);
        string location = rdr.GetString(3);
        string cuisine = rdr.GetString(4);
        Stylists newRestaurant = new Stylists(name, cuisine, location, description, id);
        foundRestaurants.Add(newRestaurant);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundRestaurants;
    }

    public static List<Stylists> FindByName(string byName)
    {
      List<Stylists> foundRestaurants = new List<Stylists> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM fav_restaurant WHERE name LIKE @Name;";
      MySqlParameter searchName = new MySqlParameter();
      searchName.ParameterName = "@Name";
      searchName.Value = byName + '%';
      cmd.Parameters.Add(searchName);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string description = rdr.GetString(2);
        string location = rdr.GetString(3);
        string cuisine = rdr.GetString(4);
        Stylists newRestaurant = new Stylists(name, cuisine, location, description, id);
        foundRestaurants.Add(newRestaurant);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundRestaurants;
    }

    public void EditDescription(string newDescription)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE fav_restaurant SET description = @descriptionPara WHERE id = @idPara;";
      MySqlParameter paraDescription = new MySqlParameter();
      paraDescription.ParameterName = "@descriptionPara";
      paraDescription.Value = newDescription;
      cmd.Parameters.Add(paraDescription);
      MySqlParameter paraId = new MySqlParameter();
      paraId.ParameterName = "@idPara";
      paraId.Value = this.Id;
      cmd.Parameters.Add(paraId);
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM fav_restaurant WHERE id=@thisId;";
      MySqlParameter deleteId = new MySqlParameter();
      deleteId.ParameterName = "@thisId";
      deleteId.Value = this.Id;
      cmd.Parameters.Add(deleteId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM fav_restaurant;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
