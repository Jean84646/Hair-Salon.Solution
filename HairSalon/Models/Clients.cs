using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
  public class Client
  {
    private int id;
    private string clientName;
    private int stylistID;

    public Client(string newClientName, int newStylistID, int newId = 0)
    {
      clientName = newClientName;
      stylistID = newStylistID;
      id = newId;
    }
    public string GetName()
    {
      return clientName;
    }
    public int GetStylistID()
    {
      return stylistID;
    }
    public int GetId()
    {
      return id;
    }
    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = (this.GetId() == newClient.GetId());
        bool clientEquality = (this.GetName() == newClient.GetName());
        bool stylistIDEquality = (this.GetStylistID() == newClient.GetStylistID());
        return (idEquality && clientEquality && stylistIDEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (client_name, stylist_id) VALUES (@inputClient, @inputStylistID);";
      MySqlParameter newName = new MySqlParameter();
      newName.ParameterName = "@inputClient";
      newName.Value = this.clientName;
      cmd.Parameters.Add(newName);
      MySqlParameter newStylistID = new MySqlParameter();
      newStylistID.ParameterName = "@inputStylistID";
      newStylistID.Value = this.stylistID;
      cmd.Parameters.Add(newStylistID);
      cmd.ExecuteNonQuery();
      id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
    }

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string client = rdr.GetString(1);
        int stylistId = rdr.GetInt32(2);
        Client newClient = new Client(client, stylistId, id);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }

    public static Client FindById(int searchId)
    {
      int id = 0;
      string clientName = "";
      int stylistID = 0;
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE id = @idMatch;";
      MySqlParameter parameterId = new MySqlParameter();
      parameterId.ParameterName = "@idMatch";
      parameterId.Value = searchId;
      cmd.Parameters.Add(parameterId);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        clientName = rdr.GetString(1);
        stylistID = rdr.GetInt32(2);
      }
      Client foundClient =  new Client(clientName, stylistID, id);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundClient;
    }

    // public Stylist GetStylist()
    // {
    //   int id = 0;
    //   string name = "";
    //   string description = "";
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"SELECT * FROM stylists WHERE id = @MatchID;";
    //   MySqlParameter searchMatchID = new MySqlParameter();
    //   searchMatchID.ParameterName = "@MatchID";
    //   searchMatchID.Value = this.stylistID;
    //   cmd.Parameters.Add(searchMatchID);
    //   MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
    //   while(rdr.Read())
    //   {
    //     id = rdr.GetInt32(0);
    //     name = rdr.GetString(1);
    //     description = rdr.GetString(2);
    //   }
    //   Stylist newStylist = new Stylist(name, description, id);
    //   conn.Close();
    //   if (conn !=null)
    //   {
    //     conn.Dispose();
    //   }
    //   return newStylist;
    // }

    public void EditName(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET client_name = @newName WHERE id = @searchId;";
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

    public void EditStylistId(int newStylistId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET stylist_id = @newStylistId WHERE id = @searchId;";
      MySqlParameter editStylistId = new MySqlParameter();
      editStylistId.ParameterName = "@newStylistId";
      editStylistId.Value = newStylistId;
      cmd.Parameters.Add(editStylistId);
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
      cmd.CommandText = @"DELETE FROM clients WHERE id = @idMatch;";
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
      cmd.CommandText = @"DELETE FROM clients;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
