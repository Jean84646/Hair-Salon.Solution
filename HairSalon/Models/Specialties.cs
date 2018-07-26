using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
  public class Specialties
  {
    private int id;
    private string specialties;

    public Specialties(string newSpecialties, int newId = 0)
    {
      specialties = newSpecialties;
      id = newId;
    }
    public string GetSpecialties()
    {
      return specialties;
    }
    public int GetId()
    {
      return id;
    }
    public override bool Equals(System.Object otherSpecialties)
    {
      if (!(otherSpecialties is Specialties))
      {
        return false;
      }
      else
      {
        Specialties newSpecialties = (Specialties) otherSpecialties;
        bool idEquality = (this.GetId() == newSpecialties.GetId());
        bool specialtiesEquality = (this.GetSpecialties() == newSpecialties.GetSpecialties());
          return (idEquality && specialtiesEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO specialties (specialties) VALUES (@inputSpecialties);";
      MySqlParameter newName = new MySqlParameter();
      newName.ParameterName = "@inputSpecialties";
      newName.Value = this.specialties;
      cmd.Parameters.Add(newName);
      cmd.ExecuteNonQuery();
      id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
    }

    public static List<Specialties> GetAll()
    {
      List<Specialties> allSpecialties = new List<Specialties> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialties;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string specialties = rdr.GetString(1);
        Specialties newSpecialties = new Specialties(specialties, id);
        allSpecialties.Add(newSpecialties);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allSpecialties;
    }

    public static Specialties FindById(int searchId)
    {
      int id = 0;
      string specialties = "";
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialties WHERE id = @idMatch;";
      MySqlParameter parameterId = new MySqlParameter();
      parameterId.ParameterName = "@idMatch";
      parameterId.Value = searchId;
      cmd.Parameters.Add(parameterId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        specialties = rdr.GetString(1);
      }
      Specialties foundSpecialties =  new Specialties(specialties, id);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundSpecialties;
    }

    public static List<Specialties> FindByName(string findSpecialty)
    {
      List<Specialties> foundSpecialties = new List<Specialties> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialties WHERE specialties LIKE @specialties;";
      MySqlParameter searchSpecialty = new MySqlParameter();
      searchSpecialty.ParameterName = "@specialties";
      searchSpecialty.Value = findSpecialty + "%";
      cmd.Parameters.Add(searchSpecialty);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string specialties = rdr.GetString(1);
        Specialties foundSpecialty = new Specialties(specialties, id);
        foundSpecialties.Add(foundSpecialty);
      }

      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return foundSpecialties;
    }

    public List<Stylist> GetStylists()
    {
      List<Stylist> allStylists = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT stylists.* FROM stylists JOIN stylist_specialties ON (stylists.id = stylist_specialties.stylist_id) JOIN specialties ON (stylist_specialties.specialties_id = specialties.id) WHERE specialties.id = @IdMatch;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@IdMatch";
      searchId.Value = this.id;
      cmd.Parameters.Add(searchId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string stylist = rdr.GetString(1);
        string description = rdr.GetString(2);
        Stylist foundStylist = new Stylist(stylist, description, id);
        allStylists.Add(foundStylist);
      }
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return allStylists;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialties;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
