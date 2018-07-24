using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UniversityRegistrar;

namespace UniversityRegistrar.Models
{
  public class StylistSpecialties
  {
    private int id;
    private int stylistID;
    private int specialtiesID;

    public StylistSpecialties (int newStylistID, int newSpecialtiesID, int newId = 0)
    {
      id = newId;
      stylistID = newStylistID;
      specialtiesID = newSpecialtiesID;
    }
    public int GetId()
    {
      return id;
    }
    public int GetStylistID()
    {
      return stylistID;
    }
    public int GetSpecialtiesID()
    {
      return specialtiesID;
    }
    public override bool Equals(System.Object otherStylistSpecialties)
    {
      if (!(otherStylistSpecialties is StylistSpecialties))
      {
        return false;
      }
      else
      {
        StylistSpecialties newStylistSpecialties = (StylistSpecialties) otherStylistSpecialties;
        bool idEquality = (this.GetId() == newStylistSpecialties.GetId());
        bool stylistIDEquality = (this.GetStylistID() == newStylistSpecialties.GetStylistID());
        bool specialtiesIDEquality = (this.GetSpecialtiesID() == newStylistSpecialties.GetSpecialtiesID());
        return (idEquality && stylistIDEquality && specialtiesIDEquality);
      }
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylist_specialties (stylist_id, specialties_id) VALUES (@inputStylistID, @inputSpecialtiesID);";
      MySqlParameter newStylistID = new MySqlParameter();
      newStylistID.ParameterName = "@inputStylistID";
      newStylistID.Value = this.stylistID;
      cmd.Parameters.Add(newStylistID);
      MySqlParameter newSpecialtiesID = new MySqlParameter();
      newSpecialtiesID.ParameterName = "@inputSpecialtiesID";
      newSpecialtiesID.Value = this.specialtiesID;
      cmd.Parameters.Add(newSpecialtiesID);
      cmd.ExecuteNonQuery();
      id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
    }
    public static StylistSpecialties FindById(int searchId)
    {
      int id = 0;
      int stylistId = 0;
      int specialtiesId = 0;
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylist_specialties WHERE id = @idMatch;";
      MySqlParameter parameterId = new MySqlParameter();
      parameterId.ParameterName = "@idMatch";
      parameterId.Value = searchId;
      cmd.Parameters.Add(parameterId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        stylistId = rdr.GetInt32(1);
        specialtiesId = rdr.GetInt32(2);
      }
      StylistSpecialties foundStylistSpecialties =  new StylistSpecialties(stylistId, specialtiesId, id);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundStylistSpecialties;
    }
    public static List<StylistSpecialties> GetAll()
    {
      List <StylistSpecialties> newStylistSpecialtiess = new List<StylistSpecialties> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists_specialties;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int stylistId = rdr.GetInt32(1);
        int specialtiesId = rdr.GetInt32(2);
        StylistSpecialties newStylistSpecialties = new StylistSpecialties(stylistId, specialtiesId, id);
        newStylistSpecialtiess.Add(newStylistSpecialties);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newStylistSpecialtiess;
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists_specialties;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
