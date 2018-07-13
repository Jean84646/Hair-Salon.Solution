using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
  public class Stylist
  {
    private string Name;
    private string Description;
    private int Id;

    public Stylist(string newName, string newDescription = "", int newId = 0)
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
    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool nameEqual = (this.GetName() == newStylist.GetName());
        bool descriptionEqual = (this.GetDescription() == newStylist.GetDescription());
        bool idEqual = (this.GetId() == newStylist.GetId());
        return (nameEqual && descriptionEqual && idEqual);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylist (name, description) VALUES (@inputName, @inputDescription);";
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

    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist> {};
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
        Stylist newStylist = new Stylist(name, description, id);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }

    public static List<Stylist> FindByStylist(string byStylist)
    {
      List<Stylist> foundStylists = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylist WHERE name LIKE @Stylist;";
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
        Stylist newStylist = new Stylist(name, description, id);
        foundStylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundStylists;
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
