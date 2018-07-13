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
      cmd.CommandText = @"INSERT INTO stylist (name, description, id) VALUES (@inputName, @inputDescription);";
      MySqlParameter newName = new MySqlParameter();
      newName.ParameterName = "@inputName";
      newName.Value = this.Name;
      cmd.Parameters.Add(newName);
      MySqlParameter newDescription = new MySqlParameter();
      newDescription.ParameterName = "@inputDescription";
      newDescription.Value = this.Description;
      cmd.Parameters.Add(newDescription);
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
      List<Stylists> allStylists = new List<Stylists> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylist;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string name = rdr.GetString(0);
        string description = rdr.GetString(1);
        int id = rdr.GetInt32(2);
        Stylists newStylist = new Stylists(name, description, id);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }

    public static Stylists FindById(int byId)
    {
      int id = 0;
      string name = "";
      string description = "";
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylist WHERE id = @idPara;";
      MySqlParameter paraId = new MySqlParameter();
      paraId.ParameterName = "@idPara";
      paraId.Value = byId;
      cmd.Parameters.Add(paraId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        name = rdr.GetString(0);
        description = rdr.GetString(1);
        id = rdr.GetInt32(2);
      }
      Stylists newStylist = new Stylists(name, description, id);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newStylist;
    }

    public static List<Stylists> FindByStylist(string byStylist)
    {
      List<Stylists> foundStylists = new List<Stylists> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylist WHERE name = @Stylist;";
      MySqlParameter searchStylist = new MySqlParameter();
      searchStylist.ParameterName = "@Stylist";
      searchStylist.Value = byStylist + '%';
      cmd.Parameters.Add(searchStylist);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string name = rdr.GetString(0);
        string description = rdr.GetString(1);
        int id = rdr.GetInt32(2);
        Stylists newStylist = new Stylists(name, description, id);
        foundStylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundStylists;
    }

    public static List<Stylists> FindByClient(string byClient)
    {
      List<Stylists> foundClients = new List<Stylists> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM client WHERE client LIKE @Client;";
      MySqlParameter searchClient = new MySqlParameter();
      searchClient.ParameterName = "@Name";
      searchClient.Value = byName + '%';
      cmd.Parameters.Add(searchClient);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string name = rdr.GetString(0);
        string stylist = rdr.GetString(1);
        Clients newClient = new Clients(client, stylist);
        foundClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundClients;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylist;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
