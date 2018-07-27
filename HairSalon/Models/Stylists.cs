using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
  public class Stylist
  {
    private int id;
    private string stylistName;
    private string stylistDescription;

    public Stylist(string newStylistName, string newStylistDescription = "", int newId = 0)
    {
      stylistName = newStylistName;
      stylistDescription = newStylistDescription;
      id = newId;
    }
    public string GetName()
    {
      return stylistName;
    }
    public string GetDescription()
    {
      return stylistDescription;
    }
    public int GetId()
    {
      return id;
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
        bool idEquality = (this.GetId() == newStylist.GetId());
        bool nameEquality = (this.GetName() == newStylist.GetName());
        bool descriptionEquality = (this.GetDescription() == newStylist.GetDescription());
        return (idEquality && nameEquality && descriptionEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists (stylist_name, stylist_description) VALUES (@inputName, @inputDescription);";
      MySqlParameter newName = new MySqlParameter();
      newName.ParameterName = "@inputName";
      newName.Value = this.stylistName;
      cmd.Parameters.Add(newName);
      MySqlParameter newDescription = new MySqlParameter();
      newDescription.ParameterName = "@inputDescription";
      newDescription.Value = this.stylistDescription;
      cmd.Parameters.Add(newDescription);
      cmd.ExecuteNonQuery();
      id = (int) cmd.LastInsertedId;
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
      cmd.CommandText = @"SELECT * FROM stylists;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string SName = rdr.GetString(1);
        string SDescription = rdr.GetString(2);
        Stylist newStylist = new Stylist(SName, SDescription, id);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }

    public static Stylist FindById(int searchId)
    {
      int id = 0;
      string stylistName = "";
      string stylistDescription = "";
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE id = @idMatch;";
      MySqlParameter parameterId = new MySqlParameter();
      parameterId.ParameterName = "@idMatch";
      parameterId.Value = searchId;
      cmd.Parameters.Add(parameterId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        stylistName = rdr.GetString(1);
        stylistDescription = rdr.GetString(2);
      }
      Stylist foundStylist =  new Stylist(stylistName, stylistDescription, id);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundStylist;
    }

    public static List<Stylist> FindByName(string searchName)
    {
      List<Stylist> allStylists = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE stylist_name LIKE @nameMatch;";
      MySqlParameter paraName = new MySqlParameter();
      paraName.ParameterName = "@nameMatch";
      paraName.Value = searchName + '%';
      cmd.Parameters.Add(paraName);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string description = rdr.GetString(2);
        Stylist foundStylist =  new Stylist(name, description, id);
        allStylists.Add(foundStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }

    public List<Client> GetClients()
    {
      List<Client> myClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @idParameter;";
      MySqlParameter parameterId = new MySqlParameter();
      parameterId.ParameterName = "@idParameter";
      parameterId.Value =this.id;
      cmd.Parameters.Add(parameterId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int stylistID = rdr.GetInt32(2);
        Client foundClient = new Client(clientName, stylistID, id);
        myClients.Add(foundClient);
      }
      if (conn != null)
      {
        conn.Dispose();
      }
      return myClients;
    }

    public List<Specialties> GetSpecialties()
    {
      List<Specialties> allSpecialties = new List<Specialties> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT specialties.* FROM stylists JOIN stylist_specialties ON (stylists.id = stylist_specialties.stylist_id) JOIN specialties ON (stylist_specialties.specialties_id = specialties.id) WHERE stylists.id = @IdMatch;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@IdMatch";
      searchId.Value = this.id;
      cmd.Parameters.Add(searchId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string specialties = rdr.GetString(1);
        Specialties foundSpecialties = new Specialties(specialties, id);
        allSpecialties.Add(foundSpecialties);
      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return allSpecialties;
    }

    public void EditName(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET stylist_name = @newName WHERE id = @searchId;";
      MySqlParameter editName = new MySqlParameter();
      editName.ParameterName = "@newName";
      editName.Value = newName;
      cmd.Parameters.Add(editName);
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = this.id;
      cmd.Parameters.Add(searchId);
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
      cmd.CommandText = @"DELETE FROM stylists WHERE id = @idMatch; DELETE FROM stylist_specialties WHERE stylist_id = @idMatch;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@idMatch";
      searchId.Value = this.id;
      cmd.Parameters.Add(searchId);
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
      cmd.CommandText = @"DELETE FROM stylists;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
